using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace TrendyPlants.Pages
{
    public class KontaktModel : PageModel
    {
        private readonly TrendyPlantsOptions _options;
        readonly TelemetryClient _telemetryClient;

        public KontaktModel(IOptions<TrendyPlantsOptions> options, TelemetryClient telemetryClient)
        {
            _options = options.Value;
            _telemetryClient = telemetryClient;
        }

        [Display(Name = "Imię i Nazwisko")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [BindProperty]
        public string Name { get; set; }

        [Display(Name = "Adres email")]
        [EmailAddress(ErrorMessage = "Prosimy o prawidłowy adres email.")]
        [BindProperty]
        public string? EmailAddress { get; set; }

        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Phone(ErrorMessage = "Prosimy o prawidłowy numer telefonu.")]
        [BindProperty]
        public string MobilePhone { get; set; }

        [Display(Name = "Wiadomość")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [BindProperty]
        public string Message { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [BindProperty(Name = "g-recaptcha-response")]
        public string RecaptchaToken { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var enquiryTimestamp = DateTime.Now;
            var enquiryId = Guid.NewGuid();

            var client = new SmtpClient
            {
                Host = _options.EmailSmtpHost,
                Port = _options.EmailSmtpPort,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(_options.EmailCredentialUsername, _options.EmailCredentialPassword)
            };

            var mailMessage = new MailMessage(_options.EmailSendFrom, _options.EmailSendTo);

            if (!string.IsNullOrWhiteSpace(_options.EmailSendBcc))
            {
                var bccAddresses = _options.EmailSendBcc.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var bccAddress in bccAddresses)
                {
                    mailMessage.Bcc.Add(new MailAddress(bccAddress));
                }
            }

            var emailAddressFromForm = EmailAddress;

            if (string.IsNullOrWhiteSpace(emailAddressFromForm))
            {
                emailAddressFromForm = "not provided";
            }

            mailMessage.Subject = $"Zapytanie od klienta {_options.WebsiteAdvertisedDomainName}";
            mailMessage.Body = @$"<html><body>
Client message submitted at {enquiryTimestamp}<br />
Name : <strong>{Name}</strong><br />
Phone : <strong>{MobilePhone}</strong><br />
Email address : <strong>{emailAddressFromForm}</strong><br />
Message<br />
<strong>{Message}</strong><br /><br /><br /><br />
<em><span style=""color: silver;"">Enquiry ID : {enquiryId}</span></em>
</body></html>";

            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            client.Send(mailMessage);

            _telemetryClient.TrackEvent("EnquiryEmailSent",
                new Dictionary<string, string> {
                    { "EnquiryId", enquiryId.ToString() },
                    { "EnquiryTimestamp", enquiryTimestamp.ToString("O") },
                    { "ClientName", Name } });

            return RedirectToPage("WiadomośćWysłana");
        }
    }
}