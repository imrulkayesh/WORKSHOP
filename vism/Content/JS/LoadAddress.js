$(document).ready(function () {
    var txtdis = $("#lblPresentDis").html();
    var txtthana = $("#lblPresentThana").html();

    $("#ddlPresentDis option:contains(" + txtdis + ")").attr('selected', 'selected');

    $("#ddlPresentThana option:contains(" + txtthana + ")").attr('selected', 'selected');

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
                url: "Profile.aspx/LoadPresentDistrictByDivision",
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
                url: "Profile.aspx/LoadPresentThanaByDistrict",
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
                url: "Profile.aspx/LoadPresentDistrictByDivision",
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
        PopulateThana();
    }

    function PopulatePermanentThana() {
        if ($("#ddlPermanentDis :selected").val() == "0") {
        }
        else {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "Profile.aspx/LoadPresentThanaByDistrict",
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
});