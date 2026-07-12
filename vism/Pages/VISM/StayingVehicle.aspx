<%@ Page Title="Staying Vehicle" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="StayingVehicle.aspx.cs" Inherits="Pages_VISM_StayingVehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                WorkShop
            </label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlWorkShop" Width="100%" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" />
            </div>
            <label for="inputEmail3" class="col-sm-2">
                Service Type</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlServiceType" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Group</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" Enabled="True" ID="ddlgroup" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
            </div>
            <label for="inputEmail3" class="col-sm-2">
                Company</label>
            <div class="col-sm-4">
                <%--      <asp:TextBox ID="tbxCom" runat="server" placeholder="Enter Company Name" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCom"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="tbxCom"
                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                </ajaxToolkit:AutoCompleteExtender>--%>
                <asp:DropDownList ID="ddlCompany" Width="100%" runat="server" CssClass="form-control select2" DataTextField="COMPANYNAME" DataValueField="ID"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
                <asp:Button ID="btnExport" Visible="False" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_OnClick" />
            </div>
        </div>
    </div>
    <hr />
    <div class="panel panel-info">
        <div class="panel-heading">
            <div class="panel-title text-center">Staying Vehicle List</div>
        </div>
        <div class="panel-body">
            <div style="overflow: auto; max-height: 500px;">
                <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_OnItemDataBound">
                    <HeaderTemplate>
                        <table id="dt" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                            <thead>
                                <tr>
                                    <th>SL#
                                    </th>
                                    <asp:Repeater runat="server" ID="hdrRpt">
                                        <ItemTemplate>
                                            <th>
                                                <%# Container.DataItem %>
                                            </th>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <asp:Repeater runat="server" ID="bdyRpt">
                                <ItemTemplate>
                                    <td>
                                        <%# Container.DataItem %>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>

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
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true,
                "order": [[0, "desc"]],
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>

