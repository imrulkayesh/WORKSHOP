<%@ Page Title="Service Request" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ServiceRequest.aspx.cs" Inherits="Reports_Service_ServiceRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="col-sm-12">
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">Search Criteria</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2">Service ID</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="tbxServiceId" runat="server" CssClass="form-control" placeholder="Enter Service ID"></asp:TextBox>
                        </div>
                        <label class="col-md-2">Service Person</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="tbxServicManStaffID" runat="server" CssClass="form-control" placeholder="Enter Service Person StaffID"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2">Location</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlBusinessCenter" runat="server" CssClass="form-control select2" DataTextField="CENTERNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessCenter_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <label class="col-md-2">Service Center</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlServiceCenter" runat="server" CssClass="form-control select2" DataTextField="SERVICECENTERNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceCenter_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2">Service Type</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control select2" DataTextField="CATEGORYNAME" DataValueField="ID"></asp:DropDownList>
                        </div>
                        <label class="col-md-2">Instant Service</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlInstantService" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2">From StaffID</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="tbxFromStaffID" runat="server" CssClass="form-control" placeholder="Enter From Employee StaffID"></asp:TextBox>
                        </div>
                        <label class="col-md-2">Service Status</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="0">Pending</asp:ListItem>
                                <asp:ListItem Value="1">Assigned</asp:ListItem>
                                <asp:ListItem Value="2">User Rating</asp:ListItem>
                                <asp:ListItem Value="3">Serviceperson Rating</asp:ListItem>
                                <asp:ListItem Value="4">Revision Required</asp:ListItem>
                                <asp:ListItem Value="5">Completed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6 col-md-offset-6">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12">
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">Search Result</h3>
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
                            <table id="example2" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        <th>Type</th>
                                        <th>Request From</th>
                                        <th>Mobile</th>
                                        <th>ServiceCenter</th>
                                        <th>ServicePerson</th>
                                        <th>Mobile</th>
                                        <th>ServiceID</th>

                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("CREATEDATE") %></td>
                            <td>
                                <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' Text='<%#Eval("CATEGORYNAME") %>' ToolTip="View Details" /></td>
                            <td><%#Eval("EMPLOYEENAME") %></td>
                            <td><%#Eval("OFFICEMOBILE") %></td>
                            <td><%#Eval("SERVICECENTERLOCATION") %>-<%#Eval("SERVICECENTERNAME") %></td>
                            <td><%#Eval("TEAMNAME")+"-"+Eval("SERVICEMAN") %></td>
                            <td><%#Eval("SERVICEMOBILE") %></td>
                            <td><%#Eval("SERVICEID") %></td>
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
</asp:Content>

