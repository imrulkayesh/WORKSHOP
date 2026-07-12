<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="CS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .table {
            border: 1px solid #ccc;
            border-collapse: collapse;
        }

            .table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            .table th, .table td {
                padding: 5px;
                border: 1px solid #ccc;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="gvCustomers" CssClass="table" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Name" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                <ItemTemplate>
                    <%# Eval("Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Country" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                <ItemTemplate>
                    <%# Eval("Country")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <br />
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 150px">Name:<br />
                    <asp:TextBox ID="txtName"  runat="server" Width="140"/>
                </td>
                <td style="width: 150px">Country:<br />
                    <asp:TextBox ID="txtCountry" runat="server" Width="140" />
                </td>
                <td style="width: 100px">
                    <br />
                    <asp:Button ID="btnAdd" runat="server" Text="Add" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button Text="Submit" runat="server" OnClick="Submit" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            $(function () {
                $("[id*=btnAdd]").click(function () {
                    //Reference the GridView.
                    var gridView = $("[id*=gvCustomers]");

                    //Reference the first row.
                    var row = gridView.find("tr").eq(1);

                    //Check if row is dummy, if yes then remove.
                    if ($.trim(row.find("td").eq(0).html()) == "") {
                        row.remove();
                    }

                    //Clone the reference first row.
                    row = row.clone(true);

                    //Add the Name value to first cell.
                    var txtName = $("[id*=txtName]");
                    SetValue(row, 0, "name", txtName);

                    //Add the Country value to second cell.
                    var txtCountry = $("[id*=txtCountry]");
                    SetValue(row, 1, "country", txtCountry);

                    //Add the row to the GridView.
                    gridView.append(row);
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
        </script>
    </form>
</body>
</html>
