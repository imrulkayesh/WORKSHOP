<%@ Page Title="Spare Parts Sales Invoice" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SpSalseInvoice.aspx.cs" Inherits="Pages_VISM_SpSalseInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <button class="btn btn-primary" onclick="javascript:PrintElem('#mydiv');">Print</button>
    <div id="mydiv">
        <center>
                <asp:Label ID="lblworkshop" Font-Size="Medium" Font-Bold="true" EnableViewState="false" runat="server"></asp:Label><br />
             <b>RFL AUTOS (Service Division)</b><br />
            <asp:Label runat="server" Font-Size="Small" ID="lblBranchAddress"></asp:Label><br />
            <asp:Label runat="server" Font-Size="Small" ID="lblCustomer"></asp:Label><br />
            <asp:Label runat="server" Font-Size="Small" ID="lblDate"></asp:Label><br /><br />
                <b>Sales Invoice</b><br />
        </center>
        <div class="col-md-12">
            <div class="box">
                <div class="box-body">

                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th style="font-size: 13px; text-align: center;">Parts</th>
                                <th style="font-size: 13px; text-align: center;">UnitPrice</th>
                                <th style="font-size: 13px; text-align: center;">Quantity</th>
                                <th style="font-size: 13px; text-align: center;">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rpt" runat="server">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="font-size: 13px;"><%#Eval("PARTSCODE")+"-"+Eval("PARTSNAME") %></td>
                                        <td style="font-size: 13px; text-align: right;"><%#Eval("UNITPRICE")%></td>
                                        <td style="font-size: 13px; text-align: center;"><%#Eval("QUANTITY")+" "+Eval("UNITCODE") %></td>
                                        <td style="font-size: 13px; text-align: right;"><%#Eval("TOTALPRICE")%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="3" style="text-align: right;">Total</th>
                                <th style="font-size: 13px; text-align: right;">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <th colspan="3" style="text-align: right;">Discount</th>
                                <th style="font-size: 13px; text-align: right;">
                                    <asp:Label ID="lblDiscount" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <th colspan="3" style="text-align: right;">Net Amount</th>
                                <th style="font-size: 13px; text-align: right;">
                                    <asp:Label ID="lblNetAmount" runat="server"></asp:Label></th>
                            </tr>
                        </tfoot>
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
            mywindow.document.write('<html><head><title>Print Sales Invoice</title>');
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

