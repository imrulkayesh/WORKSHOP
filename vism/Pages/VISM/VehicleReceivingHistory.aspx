<%@ Page Title="Vehicle Receiving History" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VehicleReceivingHistory.aspx.cs" Inherits="Pages_VISM_VehicleReceivingHistory" %>

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
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Status
            </label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlStatus" Width="100%" CssClass="form-control">
                    <asp:ListItem Text="All" Value="10"></asp:ListItem>
                    <asp:ListItem Text="Pending for JobCard" Value="2"></asp:ListItem>
                    <asp:ListItem Text="InProgress" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Delivered" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-12 text-center">
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
            <asp:Button ID="btnExport" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click" />
        </div>
    </div>
    <div class="col-sm-12">
        <div style="overflow: auto; height: 550px">
            <div class="box">
                <div class="box-body">
                    <asp:Panel ID="pnlExport" runat="server">
                        <center>
                <b style="font-size:24px;">Job Status</b><br />
                            <b><asp:Label ID="lblTotal" runat="server"></asp:Label> </b><br />
                            <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> <br /><hr/>
              </center>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>
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
                                            <th>Receive Date</th>
                                            <th>Delivery Date</th>
                                            <th>Total Days</th>
                                            <th>Status</th>
                                            <th>Remarks</th>
                                            <th>JobNo</th>
                                            <th>PINo</th>
                                            <th>PDI</th>
                                            <th>BILL</th>
                                            <th>MoneyRecipt</th>
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
                                    <td><%#Eval("STATUS").ToString()=="0"? Eval("UPDATEDATE","{0:dd/MM/yyyy HH:MM}"):"" %></td>
                                    <td><%#string.IsNullOrEmpty(Eval("UPDATEDATE").ToString())?"": (Convert.ToDateTime(Eval("UPDATEDATE")) - Convert.ToDateTime(Eval("CREATEDATE"))).Duration().TotalDays.ToString("0")%></td>
                                    <td><%#Eval("STATUS").ToString().Replace("0","Delivered").Replace("1","InProgress").Replace("2","Pending for JobCard").Replace("3","Cancelled") %></td>
                                    <td><%#Eval("REMARKS") %></td>
                                    <td>
                                        <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("JOBID")%>' Text='<%#Eval("JOBID") %>' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnPI" runat="server" OnCommand="btnPI_Command" CommandArgument='<%# Eval("PIID")%>' Text='<%#Eval("PIID") %>' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnPDI" runat="server" OnCommand="btnPDI_Command" CommandArgument='<%# Eval("JOBID")%>' Text='View' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnBill" runat="server" OnCommand="btnBill_Command" CommandArgument='<%# Eval("JOBID")%>' Text='View' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnMoneyRecipt" runat="server" OnCommand="btnMoneyRecipt_Command" CommandArgument='<%# Eval("JOBID")%>' Text='View' ToolTip="Job Details" /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
