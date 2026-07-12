<%@ Page Title="Tools Transaction Report" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="RptToolsTrn.aspx.cs" Inherits="Pages_VISM_RptToolsTrn" %>

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
                <asp:DropDownList ID="ddlwksp" Width="100%" runat="server" CssClass="form-control select2" DataValueField="id" DataTextField="text" AutoPostBack="true" OnSelectedIndexChanged="ddlwksp_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                Staff Id</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlTech" CssClass="form-control select2" DataValueField="id" DataTextField="text" />
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
            <div class="panel-title text-center">Tools Transaction Report</div>
        </div>
        <br />
        <div class="form-group">
            <div class="col-sm-12">
                <div class="box">
                    <div class="box-body">
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>
                                <table id="dt" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL#</th>
                                            <th>EMPLOYEE NAME</th>
                                            <th>ITEMNAME</th>
                                            <th>UNIT</th>
                                            <th>QTY</th>
                                            <th>CATEGORY</th>
                                            <th>WORKSHOP</th>
                                            <th>Date</th>
                                            <th>REMARKS</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#((IDataItemContainer) Container).DataItemIndex + 1%></td>
                                    <td><%#Eval("EMPLOYEENAME") %></td>
                                    <td><%#Eval("ITEMNAME") %></td>
                                    <td><%#Eval("SCMUNITCODE") %></td>
                                    <td><%#Eval("QTY") %></td>
                                    <td><%#Eval("CATNAME") %></td>
                                    <td><%#Eval("WORKSHOP") %></td>
                                    <td><%#Eval("DT") %></td>
                                    <td><%#Eval("REMARKS")%></td>
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
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>

