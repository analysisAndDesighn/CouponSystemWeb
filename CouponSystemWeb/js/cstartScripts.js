
$(document).ready(function () {

    function start() {
        if (document.getElementById("CategoryCbx").checked) {
            document.getElementById("CategoryOptions").style.display = 'inline';
        }
        if (document.getElementById("LocationCbx").checked) {
            document.getElementById("locationOptions").style.display = 'inline';
        }
        if (document.getElementById("BuisnessCbx").checked) {
            document.getElementById("buisNameDiv").style.display = 'inline';
        }
    }

    $("#CategoryCbx").change(function () {
        if (this.checked) {
            document.getElementById("CategoryOptions").style.display = 'inline';
        }
        else {
            document.getElementById("CategoryOptions").style.display = 'none';
        }
    });

    $("#LocationCbx").change(function () {
        if (this.checked) {
            document.getElementById("locationOptions").style.display = 'inline';
        }
        else {
            document.getElementById("locationOptions").style.display = 'none';
            document.getElementById("CitySelection").style.display = 'none';
            document.getElementById("city").checked = false;
            document.getElementById("gps").checked = false;
        }
    });

    $("#city").change(function () {
        if (this.checked) {
            document.getElementById("CitySelection").style.display = 'inline';
        }

    });

    $("#gps").change(function () {
        if (this.checked) {
            document.getElementById("CitySelection").style.display = 'none';
        }

    });

    $("#BuisnessCbx").change(function () {
        if (this.checked) {
            document.getElementById("buisNameDiv").style.display = 'inline';
        }
        else {
            document.getElementById("buisNameDiv").style.display = 'none';
        }
    });
});