<%@ Page Title="Gate Exit" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="GateOut.aspx.cs" Inherits="Pages_VISM_GateOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            Search Criteria
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Workshop</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlworkshopid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshopid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Service Type</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlSerCatid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlSerCatid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Staying Vehicle list
        </div>
        <div class="panel-body">
            <asp:Repeater ID="rpt" runat="server">
                <HeaderTemplate>
                    <table id="example1" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Type</th>
                                <th>Chassis No</th>
                                <th>Reg No</th>
                                <th>Owner</th>
                                <th>Driver</th>
                                <th>Owner Mob</th>
                                <th>Driver Mob</th>
                                <th>InDate</th>
                                <th>OutDate</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("SERVICETYPE") %></td>
                        <td><%#Eval("CHASSISNO") %></td>
                        <td><%#Eval("REGNO") %></td>
                        <td><%#Eval("OWNERNAME") %></td>
                        <td><%#Eval("DRIVERNAME") %></td>
                        <td><%#Eval("OWNERMOBILE") %></td>
                        <td><%#Eval("DRIVERMOBILE") %></td>
                        <td><%#Eval("INDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                        <td><%#Eval("OUTDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                        <td>
                            <asp:ImageButton ID="btnExit" runat="server" OnCommand="btnGateOut_Command" CommandArgument='<%# Eval("Id").ToString()+"-"+Eval("CHASSISNO").ToString() %>' ImageUrl="~/Content/Images/Common/forward.png" ToolTip="Vehicle Exit" />
                            <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                                </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

    <asp:Panel ID="pnPrint" Visible="false" runat="server">
        <div class="row">
            <div class="col-md-6" style="width: 100%;">
                <table class="table table-bordered">
                    <tr>
                        <td colspan="2" class="text-center">
                            <center>
                                <b>Gate Pass</b><br />
                                <b><asp:Label ID="lblWorkshop" runat="server" EnableViewState="false"></asp:Label></b>
                                <br />
                                <asp:Label ID="lblAddress" runat="server" EnableViewState="false"></asp:Label>
                                <br />
                                <asp:Label ID="lblPhone" runat="server" EnableViewState="false"></asp:Label><br />
                                <asp:Label ID="lblInOut" runat="server" EnableViewState="false"></asp:Label>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td>ChassisNo</td>
                        <td>
                            <asp:Label ID="LblChassis" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>RegNo</td>
                        <td>
                            <asp:Label ID="lblRegNo" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Purpose</td>
                        <td>
                            <asp:Label ID="lblPurpose" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Owner</td>
                        <td>
                            <asp:Label ID="lblOwner" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Mobile</td>
                        <td>
                            <asp:Label ID="lblOwnerMobile" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Driver</td>
                        <td>
                            <asp:Label ID="lblDriver" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Mobile</td>
                        <td>
                            <asp:Label ID="lblDriverMobile" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Printed By</td>
                        <td>
                            <asp:Label ID="lblPrintedBy" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>

                </table>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        //$(document).ready(function () {
        //    PrintPanel();
        //});
        function PrintPanel() {
            var panel = document.getElementById("<%=pnPrint.ClientID %>");
            var windowHeight = window.innerHeight;
            var windowWidth = window.innerWidth;
            var printWindow = window.open('', '', 'letf=0,top=0,height=' + windowHeight + ',width=' + windowWidth + ',toolbar=0,scrollbars=1,status=0');
            printWindow.document.write("<html><head><title>Contents</title>");
            printWindow.document.write('<style>.text-left {text-align: left;} .text-right {text-align: right;} .text-center {text-align: center;}.text-justify {text-align: justify;}.text-nowrap {white-space: nowrap;}.text-lowercase {text-transform: lowercase;}.text-uppercase {text-transform: uppercase;}.text-capitalize {text-transform: capitalize;}.img-thumbnail {display: inline-block;max-width: 100%;height: auto;padding: 4px;line-height: 1.42857143;background-color: #fff;border: 1px solid #ddd;border-radius: 4px;-webkit-transition: all .2s ease-in-out;-o-transition: all .2s ease-in-out;transition: all .2s ease-in-out;}.panel-title {margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;}.table {width: 100%;max-width: 100%;margin-bottom: 20px;} .table {border-collapse: collapse !important;}.table td, .table th {background-color: #fff !important;}.table-bordered th, .table-bordered td {border: 1px solid #ddd !important;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {padding: 8px;line-height: 1.42857143; vertical-align: top;border-top: 1px solid #ddd; text-align:left }.table > thead > tr > th {vertical-align: bottom;border-bottom: 2px solid #ddd;}.table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {border-top: 0;}.table > tbody + tbody {border-top: 2px solid #ddd;}.table .table {background-color: #fff;}.table-bordered {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > thead > tr > td {border-bottom-width: 2px;}</style></head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.focus();
            printWindow.print();
            printWindow.close();
            panel.style.display = 'none';
        }
    </script>
</asp:Content>

