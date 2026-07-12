<%@ Page Title="Consumption Report Detail With JOB Card" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ConsumptionWithJOBCard.aspx.cs" Inherits="Pages_VISM_ConsumptionWithJOBCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        <div style="overflow: auto; height: 500px">
            <div class="box">
                <div class="box-body">
                    <asp:Panel ID="pnlExport" runat="server">
                        <center>
                <b style="font-size:24px;">Consumption Report</b><br />
                            <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> <br /><hr/>
              </center>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>

                                <table id="dtt" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Job ID</th>
                                            <th>Company Name</th>
                                            <th>Reg No</th>
                                            <th>Chassis No</th>
                                            <th>Model</th>
                                            <th>Item Name</th>
                                            <th>Quantity</th>
                                            <th>Unit</th>
                                            <th>Amount</th>
                                            <th>Remarks</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex+1 %></td>
                                    <td><%#Eval("JOBID") %></td>
                                     <td><%#Eval("COMPANYNAME") %></td>
                                    <td><%#Eval("REGNO") %></td>
                                    <td><%#Eval("CHASSISNO") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("PARTSNAME") %></td>
                                    <td><%#Eval("QUANTITY") %></td>
                                    <td><%#Eval("UNITNAME") %></td>
                                    <td><%#Eval("AMOUNT") %></td>
                                    <td><%#Eval("REMARKS") %></td>
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

