<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="FuelDemandPrint.aspx.cs" Inherits="Pages_VISM_FuelDemand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <button class="btn btn-primary" onclick="javascript:PrintElem('#mydiv');">Print</button>
    <div id="mydiv">
        <div style="width: 100%">
            <div>
                <center>
                <h1 style="font-size:large"><b>Fuel Demand List</b></h1><br />
                </center>
            </div>
        </div>
        <div>
            <br />
        </div>
        <div class="col-md-12">
            <div class="box">
                <div class="box-body">
                    <table class="table table-responsive table-bordered">
                        <tr>
                            <th style="font-size: 13px;">Vehicle No :</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblregNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Model :</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblModel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Company :</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblCompanyName"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Pump :</th>
                            <td style="font-size: 13px;">
                                <asp:Label ID="lblPump" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Date:</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </td>

                            <th style="font-size: 13px;">Demand ID:</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDid"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Remarks:</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                            </td>

                            <th style="font-size: 13px;"></th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="printStatus" Text=""></asp:Label>
                             <%--  <asp:Label runat="server" ID="lblnop"></asp:Label>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="box">
                <%--       <div class="box-header">
                    Charges
                </div>--%>
                <div class="box-body">
                    <table class="table table-responsive table-bordered">
                        <thead>
                            <%--   <tr>
                                <th colspan="2" style="text-align: center;">SR Type :
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSRtype"></asp:Label>
                                </th>
                                <th colspan="1" style="text-align: center;">Date :
                                </th>
                                <th colspan="4" style="text-align: center;">
                                    <asp:Label runat="server" ID="lblDate"></asp:Label>
                                </th>
                            </tr>--%>
                            <tr>
                                <th>SL. No
                                </th>
                                <th>Item ID
                                </th>
                                <th>Item Name
                                </th>
                                <th>Qty.
                                </th>
                                <th>Remarks
                                </th>
                                <%--    <th>Date
                                </th>
                                <th>SR Type
                                </th>--%>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptItems">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td>
                                            <%#Eval("itemid") %>
                                        </td>
                                        <td>
                                            <%#Eval("ITEMNAME") %>
                                        </td>
                                        <td>
                                            <%#Eval("AQTY")%> 
                                        </td>
                                        <td>
                                            <%#Eval("REMARKS")%> 
                                        </td>

                                        <%--       <td>
                                            <%#Eval("UDT")%> 
                                        </td>
                                        <td>
                                            <%#Eval("damage_name")%> 
                                        </td>--%>

                                        <%--     <td>
                                            <%#Eval("REMARKS") %>
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tfoot>
                            <%--    <tr>
                                <th colspan="2" style="text-align: center;">Total Qty :
                                </th>
                                <th colspan="2" style="text-align: right;">
                                    <asp:Label runat="server" Text="0" ID="lblTotalQ"></asp:Label>
                                </th>
                            </tr>--%>
                            <tr>
                                <td colspan="3" style="border-top: 0px; border-right: 0px;" class="auto-style1">Received By:</td>
                                <td colspan="2" style="border-top: 0px; border-right: 0px;" class="auto-style1">Creator By:</td>
                                <td style="border-top: 0px; border-right: 0px;" class="auto-style1">Approved By:</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="border-top: 0px; border-right: 0px;"></td>
                                <td colspan="2" style="border-top: 0px; border-right: 0px;">
                                    <asp:Label runat="server" ID="lblCreator"></asp:Label></td>
                                <td style="border-top: 0px; border-left: 0px;">
                                    <asp:Label runat="server" ID="lblApproveBy"></asp:Label></td>
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
            PrintElem('#mydiv');
        });
        function PrintElem(elem) {
           // UpdatePrintNo();
            Popup($(elem).html());

        }
        function Popup(data) {
            var mywindow = window.open('', 'my div', 'height=400,width=600');
            //mywindow.document.write('<html><head><title>Print Bill</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="../../RaffelDraw/Content/bootstrap.min.css" type="text/css" />');
            mywindow.document.write('<style>.text-left {text-align: left;} .text-right {text-align: right;} .text-center {text-align: center;}.text-justify {text-align: justify;}.text-nowrap {white-space: nowrap;}.text-lowercase {text-transform: lowercase;}.text-uppercase {text-transform: uppercase;}.text-capitalize {text-transform: capitalize;}.img-thumbnail {display: inline-block;max-width: 100%;height: auto;padding: 4px;line-height: 1.02857143; font-size:13px; background-color: #fff;border: 1px solid #ddd;border-radius: 4px;-webkit-transition: all .2s ease-in-out;-o-transition: all .2s ease-in-out;transition: all .2s ease-in-out;}.panel-title {margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;}.table {width: 100%;max-width: 100%;margin-bottom: 20px;} .table {border-collapse: collapse !important;}.table td, .table th {background-color: #fff !important;}.table-bordered th, .table-bordered td {border: 1px solid #ddd !important;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {padding: 4px;line-height: 1.02857143; font-size:13px; vertical-align: top;border-top: 1px solid #ddd; text-align:left }.table > thead > tr > th {vertical-align: bottom;border-bottom: 2px solid #ddd;}.table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {border-top: 0;}.table > tbody + tbody {border-top: 2px solid #ddd;}.table .table {background-color: #fff;}.table-bordered {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > thead > tr > td {border-bottom-width: 2px;}</style></head><body >');
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


        function UpdatePrintNo() {
            var dmdid = $("#lblDid").text();
            var nop = $("#lblnop").text();          
            $.ajax({
                type: "POST",
                url: "FuelDemandPrint.aspx/UpdatePrint",
                data: "{dmdid:'" + dmdid + "',nop:'" + nop + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false
            });
        }
    </script>
</asp:Content>

