<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintBill1.aspx.cs" Inherits="Pages_VISM_PrintBill" %>

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
                        PRAN-RFL GROUP
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center; font-size: 10px;">
                        <asp:Label runat="server" ID="lblBranchAddress"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-size: 11px">
                        Bill No:
                        <asp:Label runat="server" ID="lblBill"></asp:Label>
                    </td>
                    <td style="text-align: center; font-weight: bold; font-size: 18px;">
                        BILL</td>
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
                        Job No
                    </td>
                    <td style="width: 17%;">
                        <asp:Label runat="server" ID="lblJobNo"></asp:Label>
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
                        
                    </td>
                    <td>
                       
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
                        Written By
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
                        SL. No
                    </th>
                    <th colspan="3">
                        Subject
                    </th>
                    <th>
                        Quantity
                    </th>
                    
                    <th>
                        Amount
                    </th>
                    <th>
                        Remarks
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptCharge">
                    <ItemTemplate>
                        <tr>
                          <td style="width: 3%;">
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <td colspan="3">
                                <%#Eval("CHARGENAME") %>
                            </td>
                            <td style="text-align: center;">
                                <%#Eval("QUANTITY") %>
                            </td>
                            <td style="text-align: right;">
                                <%#Eval("AMOUNT") %>
                            </td>
                            
                            <td>
                                <%#Eval("REMARKS") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater runat="server" ID="rptParts">
                    <HeaderTemplate>
                        <tr>
                            <th colspan="7">Parts</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width: 3%;">
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <td colspan="3">
                                <%#Eval("PARTSNAME") %>
                            </td>
                            <td style="text-align: center;">
                                <%#Eval("QUANTITY") %>
                            </td>
                            <td style="text-align: right;">
                                <%#Eval("AMOUNT") %>
                            </td>
                            
                            <td>
                                <%#Eval("REMARKS") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <th colspan="5" style="text-align: right;">
                        Sub Total =
                    </th>
                    <th style="text-align: right;">
                        <asp:Label runat="server" ID="lblSubTotal"></asp:Label>.00/=
                    </th>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th colspan="5" style="text-align: right;">
                        Discount =
                    </th>
                    <th style="text-align: right;">
                        <asp:Label runat="server" ID="lblDiscount"></asp:Label>.00/=
                    </th>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="border-right: none; font-size: 9px;">
                        Included VAT 10%
                    </td>
                    <th colspan="2" style="text-align: right; border-left: none;">
                        Total =
                    </th>
                    <td style="text-align: right;">
                        <asp:Label runat="server" ID="lblTotal"></asp:Label>.00/=
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
                <table class="table table-bordered" style="font-size: 11px; margin-top: 5px;">
                <tr>
                    <td colspan="5" style="border-bottom: none">
                        <center>
                            <h5>
                                For Accounts Use Only</h5>
                            <table border="0" style="width: 100%; font-size: 11px; text-align: left;">
                                <tr>
                                    <th>
                                        Bill No.
                                    </th>
                                    <td>
                                        <asp:Label runat="server" ID="lblBillNo"></asp:Label>
                                    </td>
                                    <th style="width: 60%">
                                        Mushak Chalan No............
                                    </th>
                                </tr>
                                <tr>
                                    <th colspan="2">
                                        Money Receipt No
                                    </th>
                                    <td>
                                        <asp:Label runat="server" ID="lblMoneyReciptNo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="2">
                                        Date
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="lblMoneyReciptDate"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th colspan="2">
                                        Total Taka
                                    </th>
                                    <td>
                                        <asp:Label runat="server" ID="lblTakaInword"></asp:Label>
                                        Taka Only.
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="2">
                                        Accountant
                                    </th>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                    <th colspan="5" style="text-align: center; border-bottom: none;">
                        Received the old Parts along with<br />
                        Satisfactory Work of the Vehicle
                    </th>
                </tr>
                <tr>
                    <th colspan="5" style="border-top: none; text-align: left;">
                        Finally Checked By
                    </th>
                    <th colspan="5" style="border-top: none; text-align: left;">
                        Signature
                    </th>
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
