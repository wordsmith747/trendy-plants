namespace TrendyPlants
{
    public class TrendyPlantsOptions
    {
        public string WebsiteName { get; set; }

        public string WebsiteAdvertisedDomainName { get; set; }

        public string EmailSmtpHost { get; set; }

        public int EmailSmtpPort { get; set; }

        public string EmailCredentialPassword { get; set; }

        public string EmailCredentialUsername { get; set; }

        public string EmailSendFrom { get; set; }

        public string EmailSendTo { get; set; }

        public string EmailSendBcc { get; set; }

        public string GoogleMapsEmbedUrl { get; set; }

        public string ContactAddressLine1 { get; set; }

        public string ContactAddressLine2 { get; set; }

        public string ContactAddressLine3 { get; set; }
    }
}