<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientName.aspx.cs" Inherits="ClientName" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <script src="/Content/jquery/jquery-2.1.1.min.js"></script>

        <script type="text/javascript">
            $(function () {
                $.ajax({
                    type: "POST",
                    url: "http://172.17.4.235/ClientName.aspx/DetermineCompName?callback=?",
                    data: '{}',
                    //contentType: "application/json; charset=utf-8",
                    //dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert("Failed");
                    },
                    error: function (response) {
                        alert("Error");
                    }
                });
            });

            function OnSuccess(response) {

                var xmlDoc = $.parseXML(response.d);

                var xml = $(xmlDoc);
                var customers = xml.find("Table");
                customers.each(function () {
                    var customer = $(this);
                    alert(customer.find("name").text());
                });
            }

        </script>
    </form>
</body>
</html>
