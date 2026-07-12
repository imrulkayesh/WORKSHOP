<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobPrint1.aspx.cs" Inherits="Pages_VISM_JobPrint1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="bootstrap.css" type="text/css" />
    <style type="text/css">
        @media print {
            table, table tr, table tr td, table th {
                font-size: 10px;
                vertical-align: middle;
            }

            .pageBreak {
                page-break-after: always;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="mydiv" style="padding: 10px;">
            <center>
            <table style="width: 100%; margin-top: 5px;" border="0">
                <tr>
                    <td colspan="3" style="text-align: center; font-size: 24px; font-weight: bolder;">
                        RFL AUTOS
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center; font-size: 10px;">
                        <asp:Label runat="server" ID="lblBranchAddress"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-size: 11px">
                        Job No:
                        <asp:Label runat="server" ID="lblJobNo"></asp:Label>
                    </td>
                    <td style="text-align: center; font-weight: bold; font-size: 18px;">
                       Job Card
                    </td>
                    <td style="text-align: right; width: 20%; font-size: 10px">
                        Date:
                        <asp:Label runat="server" ID="lblDate"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="table table-bordered" style="font-size: 11px;">
                <tr>
                    <td style="width: 12%;" colspan="2">
                        Customer's Name
                    </td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="lblCustName"></asp:Label>
                    </td>
                    <td style="width: 19%;">
                        Received Time
                    </td>
                    <td style="width: 17%;">
                        Delivery Time
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Home Address
                    </td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="lblCustAddress"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblReceiveTime"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblDeliveryTime"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Billing Address
                    </td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="lblOfficeAddress"></asp:Label>
                    </td>
                    <td>
                        Category
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblLpg"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Phone No.
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMobile"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblPhone"></asp:Label>
                    </td>
                    <td>
                        Order Written By
                    </td>
                    <td style="vertical-align: bottom;">
                        <i style="opacity: 0.3;">Signature</i>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Contact Person
                    </td>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblContactPerson"></asp:Label>
                    </td>
                    <td>
                        Phone No.
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblContactPersonMobile"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblContactPersonPhone"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Date of Birth
                    </td>
                    <td style="width: 10%;">
                    </td>
                    <td colspan="2" style="opacity: 0.3;">
                        Anniversary(If Married):
                    </td>
                    <td colspan="2">
                        Phone When Ready
                        <input type="checkbox" /> Yes
                        <input type="checkbox" /> No
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Vehicle Reg No. 
                    </td>
                    <td>
                        Chassis No.
                    </td>
                    <td>
                        Manufacturing Year
                    </td>
                    <td>
                        Engine No.
                    </td>
                    <td>
                        ODO Meter Reading
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <asp:Label runat="server" ID="lblRegNo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblChassisNo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblYearofManuf"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblEngineNo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMeterReading"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width: 3%;">
                        SL
                    </th>
                    <th colspan="3">
                        Category
                    </th>
                    <th>
                        Problem
                    </th>
                    
                    <th>
                        Quantity
                    </th>
                    <th>
                        Remarks
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptCharge">
                    <ItemTemplate>
                  <%--      <tr>
                          <td style="width: 3%;">
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <td colspan="3">
                                <%#Eval("CATNAME") %>
                            </td>
                            <td style="text-align: left;">
                                <%#Eval("ITEMNAME") %>
                            </td>
                            <td>
                                1
                            </td>
                            
                            <td>
                                <%#Eval("REMARKS") %></td>
                        </tr>--%>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="7" style="border-top: none; border-bottom: none; padding-top: 30px;
                        padding-bottom: 15px">
                        I/We hereby authorize the above repair to be done along with necessary materials
                        in a manner as you see fit. You will not be held responsible for any loss caused
                        beyond your control. You are also allowed to drive the vehicle to road testing.
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="border-top: 0px; border-right: 0px; padding-top: 20px;">
                        Checked By...........................
                    </td>
                    <td colspan="3" style="border-top: 0px; border-left: 0px; padding-top: 20px; text-align: right;">
                        Signature................................
                    </td>
                </tr>
            </table>
        </center>
        </div>
        <script src="../../Content/jquery/jquery-2.1.1.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                PrintElem('#mydiv');
            });
            function PrintElem(elem) {
                Popup($(elem).html());
            }
            function Popup(data) {
                var mywindow = window.open('', 'my div', 'height=400,width=600');
                mywindow.document.write('<html><head><title>Print JobCard</title><link rel="stylesheet" href="bootstrap.css" type="text/css" />');
                /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="../../RaffelDraw/Content/bootstrap.min.css" type="text/css" />');
                mywindow.document.write('</head><body >');
                mywindow.document.write(data);
                mywindow.document.write('</body></html>');

                mywindow.document.close(); // necessary for IE >= 10
                mywindow.focus(); // necessary for IE >= 10

                mywindow.print();
                mywindow.close();
                window.onfocus = function () { window.close(); };
                return true;
            }
        </script>
    </form>
</body>
</html>
