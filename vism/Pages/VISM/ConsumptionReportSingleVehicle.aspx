<%@ Page Title="Consumption Report Single Vehilce" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ConsumptionReportSingleVehicle.aspx.cs" Inherits="Pages_VISM_ConsumptionReportSingleVehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Vehicle 
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxVehicle" runat="server" placeholder="Enter Vehicle" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxVehicle_OnTextChanged"></asp:TextBox>
                  <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="tbxVehicle"
                    ID="AutoCompleteExtender1" runat="server"  FirstRowSelected="true">
                </ajaxToolkit:AutoCompleteExtender>
            </div>

            <label for="inputEmail3" class="col-sm-2">
                WorkShop
            </label>
            <div class="col-sm-4">
             <asp:DropDownList runat="server" ID="ddlWorkShop" Width="100%" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Reg. No 
            </label>
            <div class="col-sm-4">
                <asp:Label runat="server" ID="lblRegNo" CssClass="form-control"></asp:Label>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                Chiss. No
            </label>
            <div class="col-sm-4">
                <asp:Label runat="server" ID="lblChissNo" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
                <asp:Button ID="btnExport" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_OnClick" />
            </div>
        </div>
    </div>
    <div class="col-sm-12">
        <div class="box">
            <div class="box-body">
                <asp:Panel ID="pnlExport" runat="server">
                    <center>
                <b style="font-size:24px;">Consumption Report Single Vehicle</b><br />
                            <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> <br />
                        <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-3" id="lbl1">Company Name :</label>
                            <asp:Label runat="server" ID="lblCompany" CssClass="col-sm-3"></asp:Label> 
                            <label class="col-sm-3" id="lbl2">Registration Name :</label>
                            <asp:Label runat="server" ID="lblRegistration" CssClass="col-sm-3"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3" id="lbl3">Chassis No :</label>
                            <asp:Label runat="server" ID="lblChassisNo" CssClass="col-sm-3"></asp:Label>
                            <label class="col-sm-3" id="lbl4">Workshop :</label>
                            <asp:Label runat="server" ID="lblWorkshop" CssClass="col-sm-3"></asp:Label>
                        </div>
                    </div>
                        <hr/>
                         </center>
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <div style="overflow: auto">
                                <table id="dtt" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Job ID</th>
                                            <th>Effective Date</th>
                                            <th>Item Name</th>
                                            <th>Quantity</th>
                                            <th>Unit</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex+1 %></td>
                                <td><%#Eval("JOBID") %></td>
                                <td><%#Eval("EFFECTIVEDATE","{0:dd/MM/yyyy}") %></td>
                                <td><%#Eval("PARTSNAME") %></td>
                                <td><%#Eval("QUANTITY") %></td>
                                <td><%#Eval("UNITNAME") %></td>
                                <td><%#Eval("AMOUNT") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table>
                        </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script src="../../Content/dt/dataTables.buttons.min.js"></script>
    <script src="../../Content/dt/buttons.flash.min.js"></script>
    <script src="../../Content/dt/jszip.min.js"></script>
    <script src="../../Content/dt/pdfmake.min.js"></script>
    <script src="../../Content/dt/vfs_fonts.js"></script>
    <script src="../../Content/dt/buttons.html5.min.js"></script>
    <script src="../../Content/dt/buttons.print.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#dt').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>


