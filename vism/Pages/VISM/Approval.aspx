<%@ Page Title="Approval" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="Pages_VISM_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="box">
                    <h4 class="panel-heading">Waiting for Approval</h4>
                    <div class="box-body">
                        <asp:Repeater ID="rptApproval" runat="server">
                            <HeaderTemplate>
                                <div style="overflow: auto">
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    Action</th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="View"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="Chassis#"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label5" runat="server" Text="Brand"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="Model"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label7" runat="server" Text="Manufacturer"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label6" runat="server" Text="Selling Date"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnApprove" runat="server" OnCommand="btnApprove_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/oksmal.jpg" ToolTip="Approve" />
                                        <asp:ImageButton ID="btnReject" runat="server" OnCommand="btnReject_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/dltgrd.png" ToolTip="Reject" /></td>
                                    <td>
                                        <asp:ImageButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/preview.png" ToolTip="View Details" /></td>
                                    <td><%#Eval("CHASSISNO") %></td>
                                    <td><%#Eval("BRANDNAME") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("MANUFACTURERNAME") %></td>
                                    <td><%#Eval("SELLDATE", "{0:dd/M/yyyy}") %></td>

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
            <div class="col-sm-12">
                <div class="box">
                    <h4 class="panel-heading">Rejected</h4>
                    <div class="box-body">
                        <asp:Repeater ID="rptRejected" runat="server">
                            <HeaderTemplate>
                                <div style="overflow: auto">
                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    Action</th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="View"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="Chassis#"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label5" runat="server" Text="Brand"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="Model"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label7" runat="server" Text="Manufacturer"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label6" runat="server" Text="Selling Date"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnApprove" runat="server" OnCommand="btnApprove_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/oksmal.jpg" ToolTip="Approve" /></td>
                                    <td>
                                        <asp:ImageButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/preview.png" ToolTip="View Details" /></td>
                                    <td><%#Eval("CHASSISNO") %></td>
                                    <td><%#Eval("BRANDNAME") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("MANUFACTURERNAME") %></td>
                                    <td><%#Eval("SELLDATE", "{0:dd/M/yyyy}") %></td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

