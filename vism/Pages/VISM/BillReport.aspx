<%@ Page Title="Bill Report" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="BillReport.aspx.cs" Inherits="Pages_VISM_BillReport" %>

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
                <b>Money Receipt</b><br />
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
                            <th style="font-size: 13px;">Bill Date</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblBillDate"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Print Date</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblPrintDate"></asp:Label></td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Job No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblJobNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">PayType</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblPayType"></asp:Label></td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Total Amount</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblTotalAmount"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Discount</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDiscount"></asp:Label></td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Net Amount</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblNetAmount"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Coustomer Paid</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblCoustomerPaid"></asp:Label></td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">CompanyPaid</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblCompanyPaid"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Paid Company</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblPaidCompany"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Employee Paid</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblEmployeePaid"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Currency</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblCurrency"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Remarks</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="box">
                <div class="box-body">
                    <table class="table table-responsive table-bordered">
                        <tr>
                            <td colspan="3" style="border-top: 0px; border-right: 0px; padding-top: 20px;">Prepared By<br />
                                <asp:Label ID="lblPreparedBy" runat="server"></asp:Label>
                            </td>
                            <td colspan="2" style="border-top: 0px; border-left: 0px; padding-top: 20px; text-align: right;">Accounts Signature................................
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="border-top: 0px; border-right: 0px; padding-top: 20px;">Signature With Date...........................
                            </td>
                            <td colspan="2" style="border-top: 0px; border-left: 0px; padding-top: 20px; text-align: right;"></td>
                        </tr>
                    </table>
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
            mywindow.document.write('<style>.text-left {text-align: left;} .text-right {text-align: right;} .text-center {text-align: center;}.text-justify {text-align: justify;}.text-nowrap {white-space: nowrap;}.text-lowercase {text-transform: lowercase;}.text-uppercase {text-transform: uppercase;}.text-capitalize {text-transform: capitalize;}.img-thumbnail {display: inline-block;max-width: 100%;height: auto;padding: 4px;line-height: 1.02857143; font-size:12px; background-color: #fff;border: 1px solid #ddd;border-radius: 4px;-webkit-transition: all .2s ease-in-out;-o-transition: all .2s ease-in-out;transition: all .2s ease-in-out;}.panel-title {margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;}.table {width: 100%;max-width: 100%;margin-bottom: 20px;} .table {border-collapse: collapse !important;}.table td, .table th {background-color: #fff !important;}.table-bordered th, .table-bordered td {border: 1px solid #ddd !important;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {padding: 4px;line-height: 1.02857143; font-size:12px; vertical-align: top;border-top: 1px solid #ddd; text-align:left }.table > thead > tr > th {vertical-align: bottom;border-bottom: 2px solid #ddd;}.table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {border-top: 0;}.table > tbody + tbody {border-top: 2px solid #ddd;}.table .table {background-color: #fff;}.table-bordered {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > thead > tr > td {border-bottom-width: 2px;}</style></head><body >');
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
</asp:Content>

