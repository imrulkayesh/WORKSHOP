<%@ Page Title="Vehicle Receive History Details" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VehicleRHDetails.aspx.cs" Inherits="Pages_VISM_VehicleRHDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        <div class="col-sm-12 text-center">
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
            <asp:Button ID="btnExport" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click" />
        </div>
    </div>
    <div class="col-sm-12">
        <div class="box">
            <div class="box-body">
                <asp:Panel ID="pnlExport" runat="server">
                    <center>
                <b style="font-size:24px;">Vehicle Receive History Details</b><br />
                         <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> <br />
                            <b><asp:Label ID="lblTotal" runat="server"></asp:Label> </b><hr/>
              </center>
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <div style="overflow: auto">
                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Workshop</th>
                                            <th>ServiceType</th>
                                            <th>Customer</th>
                                            <th>RegNo</th>
                                            <th>ChassisNo</th>
                                            <th>Model</th>
                                            <th>ReceiveTime</th>
                                            <th>DeliveryTime</th>
                                            <th>Total Day</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex+1 %></td>
                                <td><%#Eval("WORKSHOP") %></td>
                                <td><%#Eval("SERVICETYPE") %></td>
                                <td><%#Eval("CUSTOMERNAME") %></td>
                                <td><%#Eval("REGNO") %></td>
                                <td><%#Eval("CHASSISNO") %></td>
                                <td><%#Eval("MODELNAME") %></td>
                                <td><%#Eval("CREATEDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                                <td><%#Eval("UPDATEDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                                <td><%# (Convert.ToDateTime(Eval("CREATEDATE")) - (string.IsNullOrEmpty(Eval("UPDATEDATE").ToString())?Convert.ToDateTime(Eval("CREATEDATE")): Convert.ToDateTime(Eval("UPDATEDATE")))).Duration().Days.ToString("0") %></td>
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
</asp:Content>

