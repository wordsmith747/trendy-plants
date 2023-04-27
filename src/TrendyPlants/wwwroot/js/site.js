// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.




    function CalculateConiferDensity() {

        let isLengthValid = false;
        let isConiferValid = false;


        let length = Number(document.getElementById("fencelength").value);
        if (length <= 0) {
            document.getElementById("metreError").style.display = "block";
        }
        else {
            isLengthValid = true;
            document.getElementById("metreError").style.display = "none";
        }

        let selectedConifer = document.getElementById("conifers").value;

        if (selectedConifer === "iglak") {
            document.getElementById("coniferError").style.display = "block";

        }
        else {
            isConiferValid = true;
            document.getElementById("coniferError").style.display = "none";
        }

        if ((isLengthValid === false) || (isConiferValid === false)) {
            document.getElementById("resultArea").style.visibility = "hidden";
            return;

        }


        /*Validation complete. Carry on calculating. */


        let distanceThuiaSmaragd = .90;
        let distanceJodlakoreanska = 3.5;
        let distanceJalowiecSkyrocket = 0.5;

        let selectedDistance = 0.0;



        let selectedConiferNameDeclined = "";


        switch (selectedConifer) {
            case "tujaSmaragd":

                selectedDistance = distanceThuiaSmaragd;
                selectedConiferNameDeclined = "Tui Smaragd";
                document.getElementById("tuja1").style.display = "block";
                document.getElementById("jodla1").style.display = "none";
                document.getElementById("jalowiec1").style.display = "none";
                break;


            case "jodlaKoreanska":
                selectedDistance = distanceJodlakoreanska;
                selectedConiferNameDeclined = "Jodeł koreańskich";
                document.getElementById("jodla1").style.display = "block";
                document.getElementById("tuja1").style.display = "none";
                document.getElementById("jalowiec1").style.display = "none";
                break;

            case "jalowiecSkyrocket":
                selectedDistance = distanceJalowiecSkyrocket;
                selectedConiferNameDeclined = "Jałowców Skyrocket";
                document.getElementById("jalowiec1").style.display = "block";
                document.getElementById("tuja1").style.display = "none";
                document.getElementById("jodla1").style.display = "none";
                break;


        }

        let total = length / selectedDistance;

        let ceilingTotal = Math.ceil(total);

        let calculatorResult = document.getElementById("calculatorResult");
        calculatorResult.innerHTML = ceilingTotal;
        document.getElementById("resultConiferName").innerHTML = selectedConiferNameDeclined;
        document.getElementById("resultArea").style.visibility = "visible";
        scrollElement();
    }
    function scrollElement() {
        var element = document.getElementById("Recommendation");
        element.scrollIntoView();
    }

