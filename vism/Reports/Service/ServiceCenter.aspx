<%@ Page Title="Service Center Report" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ServiceCenter.aspx.cs" Inherits="Reports_Service_ServiceCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="col-md-12">
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title"></h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <asp:Repeater ID="rpt" runat="server">
                    <HeaderTemplate>
                        <div style="overflow: auto">
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Service Center</th>
                                        <th>Admin</th>
                                        <th>Mobile</th>
                                        <th>Email</th>
                                        <th>Request</th>
                                        <th>Completed</th>
                                        <th>Forwared</th>
                                        <th>Processing</th>
                                        <th>Pending</th>
                                        <th>Instant</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("CENTERCODE")+"-"+Eval("SERVICECENTERNAME") %></td>
                            <td><%# Eval("EMPLOYEENAME") %></td>
                            <td><%#Eval("OFFICEMOBILE") %></td>
                            <td><%#Eval("OFFICEMAIL") %></td>
                            <td><%#Eval("SERVICECOUNT") %></td>
                            <td><%#Eval("COMPLETED") %></td>
                            <td><%#Eval("FORWARDED") %></td>
                            <td><%#Eval("PROCESSING") %></td>
                            <td><%#Eval("PENDING") %></td>
                            <td><%#Eval("INSTANTSERVICE") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        <tfoot>
                            <td></td>
                            <td></td>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tfoot>
                        </table>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

