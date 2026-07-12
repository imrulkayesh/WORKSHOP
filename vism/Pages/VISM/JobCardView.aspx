<%@ Page Title="Jobcard Details" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobCardView.aspx.cs" Inherits="Pages_VISM_JobCardView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <button class="btn btn-primary" onclick="javascript:PrintElem('#mydiv');">Print</button>
    <div id="mydiv">
        <div style="width: 100%">
            <div style="float: left; width: 23%;">
                <img src="../../Content/Images/Common/logoRfl.jpg" height="100" width="120" />
            </div>
            <div style="float: left; width: 50%;">
                <center>
                <asp:Label ID="lblworkshop" Font-Size="Medium" Font-Bold="true" EnableViewState="false" runat="server"></asp:Label><br />
                     <b>RFL AUTOS (Service Division)</b><br />
            <asp:Label runat="server" Font-Size="Small" ID="lblBranchAddress"></asp:Label><br /><br />
                <b>JOBCARD</b><br />
        </center>
            </div>
            <div style="float: right; width: 23%; text-align: right">
                <img src="../../Content/Images/Common/logoPhoton.jpg" height="100" width="120" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="box">
                <div class="box-body">
                    <table class="table table-responsive table-bordered">
                        <tr>
                            <th style="font-size: 13px;">Date</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Millage</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblMeterReading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Job Card No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblJobNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Chassis No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblChassisNo"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Reg No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblRegNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Engine No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblEngineNo"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Model No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblEngineModuleNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Customer</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblCustName"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Driver Name</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDriverName"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Mobile</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblMobile"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Driver Mobile</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDriverMobile"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Address</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblOfficeAddress"></asp:Label></td>
                        </tr> 
                        <tr>
                            <th style="font-size: 13px;">Service Type</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblLpg"></asp:Label></td>

                            <th style="font-size: 13px;">Company</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblcompany"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="box">
                <div class="box-header" style="font-size: 14px;">
                    Charges
                </div>
                <div class="box-body">
                    <table class="table table-responsive table-bordered">
                        <thead>
                            <tr>
                                <th style="font-size: 13px;">SL. No
                                </th>
                                <th style="font-size: 13px;">Subject
                                </th>
                                <th style="font-size: 13px;">Quantity
                                </th>
                                <th style="font-size: 13px;">Amount
                                </th>
                                <th style="font-size: 13px;">Remarks
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptParts">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="5" style="font-size: 13px;">Parts</th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="font-size: 13px;">
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("PARTSNAME") %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("QUANTITY")+" "+Eval("UNITCODE") %> 
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("AMOUNT") %>
                                        </td>

                                        <td style="font-size: 13px;">
                                            <%#Eval("REMARKS") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater runat="server" ID="rptCharge">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="5" style="font-size: 14px;">Service Charge</th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="font-size: 13px;">
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("CHARGENAME") %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("QUANTITY") %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("AMOUNT") %>
                                        </td>

                                        <td style="font-size: 13px;">
                                            <%#Eval("REMARKS") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="3" style="text-align: right; font-size: 13px;" >Sub Total =
                                </th>
                                <th style="text-align: right; font-size: 13px;">
                                    <asp:Label runat="server" ID="lblSubTotal"></asp:Label>.00/=
                                </th>
                                <td></td>
                            </tr>
                            <tr>
                                <th colspan="3" style="text-align: right;">Discount =
                                </th>
                                <th style="text-align: right; font-size: 13px;">
                                    <asp:Label runat="server" ID="lblDiscount"></asp:Label>.00/=
                                </th>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="1" style="border-right: none; font-size: 10px;">Included VAT 10%
                                </td>
                                <th colspan="2" style="text-align: right; border-left: none; font-size: 13px;">Total =
                                </th>
                                <td style="text-align: right; font-size: 13px;">
                                    <asp:Label runat="server" ID="lblTotal"></asp:Label>.00/=
                                </td>
                                <td></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="box">
                <div class="box-header">
                    Technicians
                </div>
                <div class="box-body">
                    <asp:Repeater runat="server" ID="rptTechs">
                        <HeaderTemplate>
                            <table class="table table-responsive table-bordered">
                                <thead>
                                    <tr>
                                        <td style="font-size: 13px;">SL#
                                        </td>
                                        <td style="font-size: 13px;">StaffID
                                        </td>
                                        <td style="font-size: 13px;">Name
                                        </td>
                                        <td style="font-size: 13px;">Remarks
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="font-size: 13px;">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td style="font-size: 13px;">
                                    <%#Eval("STAFFID") %>
                                </td>
                                <td style="font-size: 13px;">
                                    <%#Eval("TECHNAME") %>
                                </td>
                                <td style="font-size: 13px;">
                                    <%#Eval("REMARKS") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //PrintElem('#mydiv');
        });
        function PrintElem(elem) {
            Popup($(elem).html());
        }
        function Popup(data) {
            var mywindow = window.open('', 'my div', 'height=400,width=600');
            mywindow.document.write('<html><head><title>Print JobCard</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="../../RaffelDraw/Content/bootstrap.min.css" type="text/css" />');
            mywindow.document.write('<style>.text-left {text-align: left;} .text-right {text-align: right;} .text-center {text-align: center;}.text-justify {text-align: justify;}.text-nowrap {white-space: nowrap;}.text-lowercase {text-transform: lowercase;}.text-uppercase {text-transform: uppercase;}.text-capitalize {text-transform: capitalize;}.img-thumbnail {display: inline-block;max-width: 100%;height: auto;padding: 4px;line-height: 1.02857143; font-size:10px; background-color: #fff;border: 1px solid #ddd;border-radius: 4px;-webkit-transition: all .2s ease-in-out;-o-transition: all .2s ease-in-out;transition: all .2s ease-in-out;}.panel-title {margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;}.table {width: 100%;max-width: 100%;margin-bottom: 20px;} .table {border-collapse: collapse !important;}.table td, .table th {background-color: #fff !important;}.table-bordered th, .table-bordered td {border: 1px solid #ddd !important;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {padding: 4px;line-height: 1.02857143; font-size:10px; vertical-align: top;border-top: 1px solid #ddd; text-align:left }.table > thead > tr > th {vertical-align: bottom;border-bottom: 2px solid #ddd;}.table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {border-top: 0;}.table > tbody + tbody {border-top: 2px solid #ddd;}.table .table {background-color: #fff;}.table-bordered {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > thead > tr > td {border-bottom-width: 2px;}</style></head><body >');
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

        console.clear();
        var table = $("table");
        var rows = table.find($("tr"));
        var colsLength = $(rows[0]).find($("td")).length;
        var removeLater = new Array();
        for (var i = 0; i < colsLength; i++) {
            var startIndex = 0;
            var lastIndex = 0;
            var startText = $($(rows[0]).find("td")[i]).text();
            for (var j = 1; j < rows.length; j++) {
                var cRow = $(rows[j]);
                var cCol = $(cRow.find("td")[i]);
                var currentText = cCol.text();
                if (currentText == startText) {
                    cCol.css("background", "gray");
                    console.log(cCol);
                    removeLater.push(cCol);
                    lastIndex = j;
                } else {
                    var spanLength = lastIndex - startIndex;
                    if (spanLength >= 1) {
                        console.log(lastIndex + " - " + startIndex)
                        //console.log($($(rows[startIndex]).find("td")[i]))
                        $($(rows[startIndex]).find("td")[i]).attr("rowspan", spanLength + 1);
                    }
                    lastIndex = j;
                    startIndex = j;
                    startText = currentText;
                }

            }
            var spanLength = lastIndex - startIndex;
            if (spanLength >= 1) {
                console.log(lastIndex + " - " + startIndex)
                //console.log($($(rows[startIndex]).find("td")[i]))
                $($(rows[startIndex]).find("td")[i]).attr("rowspan", spanLength + 1);
            }
            console.log("---");
        }

        for (var i in removeLater) {
            $(removeLater[i]).remove();
        }
    </script>
</asp:Content>

