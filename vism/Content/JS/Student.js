function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}
function PasswordChanged(txt) {
    $(txt).prev().val($(txt).val());
}

function PopulateDistrict() {
    if ($("#ddlPresentDiv :selected").val() == "0") {
        $('#<%=ddlPresentDis.ClientID %>').empty();
        $('#<%=ddlPresentThana.ClientID %>').empty();
    }
    else {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "Student.aspx/LoadDistrictByDivision",
            data: "{divId:'" + $("#ddlPresentDiv :selected").val() + "'}",

            success: OnCountriesPopulated,
            failure: function (response) {
                alert(response.d);
            }
        });
    }
}
function OnCountriesPopulated(response) {
    PopulateControl(response.d, $("#ddlPresentDis"));
    PopulateThana();
}

function PopulateThana() {
    if ($("#ddlPresentDis :selected").val() == "0") {
    }
    else {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "Student.aspx/LoadThanaByDistrict",
            data: "{disId:'" + $("#ddlPresentDis :selected").val() + "'}",
            success: OnThanaPopulated,
            failure: function (response) {
                alert(response.d);
            }
        });
    }
}
function OnThanaPopulated(response) {
    PopulateControl(response.d, $("#ddlPresentThana"));
}

function PopulatePermanentDistrict() {
    if ($("#ddlPermanentDiv :selected").val() == "0") {
        $('#<%=ddlPermanentDis.ClientID %>').empty();
        $('#<%=ddlPermanentThana.ClientID %>').empty();
    }
    else {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "Student.aspx/LoadDistrictByDivision",
            data: "{divId:'" + $("#ddlPermanentDiv :selected").val() + "'}",

            success: OnPermanentDistrictPopulated,
            failure: function (response) {
                alert(response.d);
            }
        });
    }
}
function OnPermanentDistrictPopulated(response) {
    PopulateControl(response.d, $("#ddlPermanentDis"));
    PopulatePermanentThana();
}

function PopulatePermanentThana() {
    if ($("#ddlPermanentDis :selected").val() == "0") {
        alert($("#ddlPermanentDis :selected").val());
    }
    else {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "Student.aspx/LoadThanaByDistrict",
            data: "{disId:'" + $("#ddlPermanentDis :selected").val() + "'}",
            success: OnPermanentThanaPopulated,
            failure: function (response) {
                alert(response.d);
            }
        });
    }
}
function OnPermanentThanaPopulated(response) {
    PopulateControl(response.d, $("#ddlPermanentThana"));
}


function PopulateControl(list, control) {
    control.empty();
    if (list.length > 0) {
        $.each(list, function () {
            control.append($("<option></option>").val(this['Value']).html(this['Text']));
        });
    }
    else {
        control.empty().append('<option selected="selected" value="0">  <option>');
    }
}

$(function () {
    //jQuery time
    var current_fs, next_fs, previous_fs; //fieldsets
    var left, opacity, scale; //fieldset properties which we will animate
    var animating; //flag to prevent quick multi-click glitches

    $(".next").click(function () {
        if (animating) return false;
        animating = true;

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //activate next step on progressbar using the index of next_fs
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now, mx) {
                //as the opacity of current_fs reduces to 0 - stored in "now"
                //1. scale current_fs down to 80%
                scale = 1 - (1 - now) * 0.2;
                //2. bring next_fs from the right(50%)
                left = (now * 50) + "%";
                //3. increase opacity of next_fs to 1 as it moves in
                opacity = 1 - now;
                current_fs.css({ 'transform': 'scale(' + scale + ')' });
                next_fs.css({ 'left': left, 'opacity': opacity });
            },
            duration: 800,
            complete: function () {
                current_fs.hide();
                animating = false;
            },
            //this comes from the custom easing plugin
            easing: 'easeInOutBack'
        });
    });

    $(".previous").click(function () {
        if (animating) return false;
        animating = true;

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //de-activate current step on progressbar
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now, mx) {
                //as the opacity of current_fs reduces to 0 - stored in "now"
                //1. scale previous_fs from 80% to 100%
                scale = 0.8 + (1 - now) * 0.2;
                //2. take current_fs to the right(50%) - from 0%
                left = ((1 - now) * 50) + "%";
                //3. increase opacity of previous_fs to 1 as it moves in
                opacity = 1 - now;
                current_fs.css({ 'left': left });
                previous_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
            },
            duration: 800,
            complete: function () {
                current_fs.hide();
                animating = false;
            },
            //this comes from the custom easing plugin
            easing: 'easeInOutBack'
        });
    });

    $(".submit").click(function () {
        return false;
    })

});

function Validate(sender, args) {
    var txt1 = document.getElementById("<%= tbxPhnHome.ClientID %>");
    var txt2 = document.getElementById("<%= tbxMobHome.ClientID%>");
    var txt3 = document.getElementById("<%= tbxPhone.ClientID%>");
    var txt4 = document.getElementById("<%= tbxMobile.ClientID%>");
    args.IsValid = (txt1.value != "") || (txt2.value != "") || (txt3.value != "") || (txt4.value != "");
    txt1.style.borderColor = "#FF8000";
    txt2.style.borderColor = "#FF8000";
    txt3.style.borderColor = "#FF8000";
    txt4.style.borderColor = "#FF8000";
}

function load() {
    $('#pnlAssignStudent').hide();
    //$('#chkShowPassword').prop('disabled', true);
    $('#chkAssign').click(function () {
        if (this.checked) {
            $('#pnlAssignStudent').slideDown();
        }
        else {
            $('#pnlAssignStudent').slideUp();
        }
    });

    $('#chkDummy').change(function () {
        if (this.checked) {
            var password = getRandomInt(10000, 99999999);
            $("#tbxUserName").val($("#tbxRegNo").val());
            $("#tbxPassword").val(password);
            $("#tbxConfirmPassword").val(password);
            //$('#chkShowPassword').prop('disabled', false);
        }
        else {
            $("#tbxUserName").val('');
            $("#tbxPassword").val('');
            $("#tbxConfirmPassword").val('');
        }
    });

    $("#chkShowPassword").bind("click", function () {
        var txtPassword = $("[id*=tbxPassword]");
        var txtConPass = $("[id*=tbxConfirmPassword]");
        if ($(this).is(":checked")) {
            txtPassword.after('<input class="form-control" onchange = "PasswordChanged(this);" id = "txt_' + txtPassword.attr("id") + '" type = "text" value = "' + txtPassword.val() + '" />');
            txtPassword.hide();

            txtConPass.after('<input class="form-control" onchange = "PasswordChanged(this);" id = "txt_' + txtConPass.attr("id") + '" type = "text" value = "' + txtConPass.val() + '" />');
            txtConPass.hide();
        } else {
            txtPassword.val(txtPassword.next().val());
            txtPassword.next().remove();
            txtPassword.show();

            txtConPass.val(txtConPass.next().val());
            txtConPass.next().remove();
            txtConPass.show();
        }
    });

    $('#chkPresent').change(function () {
        if (this.checked) {
            $("#tbxPermanentPost").val($("#tbxPostOffice").val());
            $("#tbxPermanentPostCode").val($("#tbxPostalCode").val());
            $("#tbxPermanentAddress").val($("#tbxPresentAddress").val());
            $("#ddlPermanentDiv").val($("#ddlPresentDiv option:selected").val()).attr('selected', true);
            $("#ddlPermanentDis").val($("#ddlPresentDis option:selected").val()).attr('selected', true);
            $("#ddlPermanentThana").val($("#ddlPresentThana option:selected").val()).attr('selected', true);
        }
    });

    $("#ddlPresentDiv").change(function () {
        PopulateDistrict();
    });

    $("#ddlPresentDis").on('change', function () {
        PopulateThana();
    });

    $("#ddlPermanentDiv").on('change', function () {
        PopulatePermanentDistrict();
    });

    $("#ddlPermanentDis").on('change', function () {
        PopulatePermanentThana();
    });
}

$(function () {
    load();
});
var prm = Sys.WebForms.PageRequestManager.getInstance();
if (prm != null) {
    prm.add_endRequest(function (sender, e) {
        load();
    });
};