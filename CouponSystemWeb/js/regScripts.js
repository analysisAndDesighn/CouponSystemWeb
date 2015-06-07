
$(document).ready(function () {
    $("#customerRadio").change(function () {
        if (this.checked) {
            document.getElementById("age").style.display = '';
            document.getElementById("gender").style.display = '';
            document.getElementById("CategoryOptions").style.display = '';
        }
    });

    $("#BuisOwnerRadio").change(function () {
        if (this.checked) {
            document.getElementById("age").style.display = 'none';
            document.getElementById("gender").style.display = 'none';
            document.getElementById("CategoryOptions").style.display = 'none';
        }

    });


    //function type(type) {
    //    alert('blalala');
    //    if (type == "Customer") {
    //        document.getElementById("additional_info").style.display = '';
    //    }
    //    else {
    //        document.getElementById("additional_info").style.display = 'none';
    //    }
    //}

    
    function ValidateCheckBoxList() {
        var checkBoxList = document.getElementById("categoriesLst");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        return  isValid;
    }
});



