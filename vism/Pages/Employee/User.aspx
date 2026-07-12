<%@ Page Title="User" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Pages_Employee_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-4 text-center">
                    <div style="margin-bottom: 25px" class="input-group">
                        <asp:TextBox ID="tbxStaffID" placeholder="Staff ID" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-addon" style="padding: 0px;">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" /></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12">
        <div class="box">
            <div class="box-body">
                <asp:Repeater ID="rpt" runat="server">
                    <HeaderTemplate>
                        <div style="overflow: auto">
                            <table id="dt" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Location</th>
                                        <th>Company</th>
                                        <th>Department</th>
                                        <th>StaffID</th>
                                        <th>Name</th>
                                        <th>Role</th>
                                        <th>Designation</th>
                                        <th>Mobile</th>
                                        <th>Email</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("CENTERNAME")%></td>
                            <td><%#Eval("COMPANYNAME") %></td>
                            <td><%#Eval("DEPARTMENTNAME") %></td>
                            <td><%#Eval("STAFFID") %></td>
                            <td><%#Eval("EMPLOYEENAME") %></td>
                            <td><%#Eval("ROLENAME") %></td>
                            <td><%#Eval("DESIGNATIONNAME") %></td>
                            <td><%#Eval("OFFICEMOBILE") %></td>
                            <td><%#Eval("OFFICEMAIL") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                                </table>
                        </div>
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

