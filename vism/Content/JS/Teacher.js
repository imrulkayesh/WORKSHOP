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
    $("[id*=gvEducation] [id*=lnkDelete]").hide();
    $("[id*=btnAdd]").click(function () {
        //Reference the GridView.
        var gridView = $("[id*=gvEducation]");

        //Reference the first row.
        var row = gridView.find("tr").eq(1);

        //Check if row is dummy, if yes then remove.
        if ($.trim(row.find("td").eq(0).html()) == "") {
            row.remove();
        }

        //Clone the reference first row.
        row = row.clone(true);
        var ddlDegree = $("[id*=ddlDegree] :selected")
        SetValueddl(row, 0, "degree", ddlDegree);

        var txtBoard = $("[id*=tbxBoard]");
        SetValue(row, 1, "board", txtBoard);

        var txtSubject = $("[id*=tbxSubject]");
        SetValue(row, 2, "subject", txtSubject);

        var txtYear = $("[id*=tbxYear]");
        SetValue(row, 3, "year", txtYear);

        var txtGPA = $("[id*=tbxGrade]");
        SetValue(row, 4, "grade", txtGPA);

        var txtScale = $("[id*=tbxScale]");
        SetValue(row, 5, "scale", txtScale);

        var ddlRes = $("[id*=ddlResult] :selected")
        SetValueddl(row, 6, "division", ddlRes)

        //Add the row to the GridView.
        gridView.append(row);
        $("[id*=gvEducation] [id*=lnkDelete]").show();
        $('#myModal').modal('hide');
        return false;
    });

    function SetValue(row, index, name, textbox) {
        //Reference the Cell and set the value.
        row.find("td").eq(index).html(textbox.val());

        //Create and add a Hidden Field to send value to server. 
        var input = $("<input type = 'hidden' />");
        input.prop("name", name);
        input.val(textbox.val());
        row.find("td").eq(index).append(input);
        //Clear the TextBox.
        textbox.val("");
    }
    function SetValueddl(row, index, name, ddl) {
        //Reference the Cell and set the value.
        row.find("td").eq(index).html(ddl.text());

        //Create and add a Hidden Field to send value to server. 
        var input = $("<input type = 'hidden' />");
        input.prop("name", name);
        input.val(ddl.text());
        row.find("td").eq(index).append(input);
        //Clear the TextBox.
        //ddl.val("");
    }
});

$(function () {
    $("[id*=gvTraining] [id*=lnkDelete]").hide();
    $("[id*=btnTraining]").click(function () {
        //Reference the GridView.
        var gridView = $("[id*=gvTraining]");
        //Reference the first row.
        var row = gridView.find("tr").eq(1);
        //Check if row is dummy, if yes then remove.
        if ($.trim(row.find("td").eq(0).html()) == "") {
            row.remove();
        }

        //Clone the reference first row.
        row = row.clone(true);
        var txtBoard = $("[id*=tbxTrainingName]");
        SetValue(row, 0, "trainingName", txtBoard);

        var txtSubject = $("[id*=tbxInstitute]");
        SetValue(row, 1, "institute", txtSubject);

        var txtYear = $("[id*=tbxStartDate]");
        SetValue(row, 2, "startDate", txtYear);

        var txtGPA = $("[id*=tbxEndDate]");
        SetValue(row, 3, "endDate", txtGPA);

        var txtScale = $("[id*=tbxTopics]");
        SetValue(row, 4, "topics", txtScale);

        //Add the row to the GridView.
        gridView.append(row);
        $("[id*=gvTraining] [id*=lnkDelete]").show();
        $('#Training').modal('hide');
        return false;
    });

    function SetValue(row, index, name, textbox) {
        //Reference the Cell and set the value.
        row.find("td").eq(index).html(textbox.val());
        //Create and add a Hidden Field to send value to server. 
        var input = $("<input type = 'hidden' />");
        input.prop("name", name);
        input.val(textbox.val());
        row.find("td").eq(index).append(input);
        //Clear the TextBox.
        textbox.val("");
    }
});

$(function () {
    $("[id*=gvTraining]").on('click', '[id*=lnkDelete11]', function (e) {
        e.preventDefault();
        var totalRows = $("[id*=gvTraining] tr").length;
        if (totalRows > 2) {
            $("[id*=gvTraining] [id*=lnkDelete11]").show();
            var row = $(this).closest("tr");
            row.remove();
        }
        else {
            $("[id*=gvTraining] [id*=lnkDelete11]").hide();
        }
        return false;
    });
    $("[id*=gvEducation]").on('click', '[id*=lnkDelete]', function (e) {
        e.preventDefault();
        var totalRows = $("[id*=gvEducation] tr").length;
        if (totalRows > 2) {
            $("[id*=gvEducation] [id*=lnkDelete]").show();
            var row = $(this).closest("tr");
            row.remove();
        }
        else {
            $("[id*=gvEducation] [id*=lnkDelete]").hide();
        }
        return false;
    });
});
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

$(function () {
    $("#dvSubject").hide();
    $("#ddlDegree").change(function () {
        var id = $("#ddlDegree").val();
        if (id == 7 || id == 8 || id == 9) {
            $("#dvSubject").slideDown('slow');
        }
        else {
            $("#dvSubject").slideUp('slow');
        }
    });
});

$(function () {
    $("#dvGrade").hide();
    $("#ddlResult").change(function () {
        if ($(this).val() == "4") {
            $("#dvGrade").show();
        } else {
            $("#dvGrade").hide();
        }
    });
});
function load() {
    $('#chkDummy').change(function () {
        if (this.checked) {
            $("#tbxUserName").val($("#tbxRegNo").val());
            $("#tbxPassword").val($("#tbxRegNo").val());
            $("#tbxConfirmPassword").val($("#tbxRegNo").val());
        }
        else {
            $("#tbxUserName").val('');
            $("#tbxPassword").val('');
            $("#tbxConfirmPassword").val('');
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
//On UpdatePanel Refresh
var prm = Sys.WebForms.PageRequestManager.getInstance();
if (prm != null) {
    prm.add_endRequest(function (sender, e) {
        load();
    });
};