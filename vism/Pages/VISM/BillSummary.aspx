<%@ Page Title="Bill Summary" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="BillSummary.aspx.cs" Inherits="Pages_VISM_BillSummary" %>

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
                Vehicle No
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxRecVeh" runat="server" placeholder="Start typing vehicle reg no" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxRecVeh_OnTextChanged"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="tbxRecVeh"
                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                </ajaxToolkit:AutoCompleteExtender>
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
        <div style="overflow: auto; height: 500px">
            <div class="box">
                <div class="box-body">
                    <asp:Panel ID="pnlExport" runat="server">
                        <center>
                <b style="font-size:24px;">Bill Summary </b><br />
                          <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> <br /><hr/>
                           
              </center>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>

                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Vehicle No</th>
                                            <th>From Date</th>
                                            <th>To Date</th>
                                            <th>Reg. No</th>
                                            <th>Chiss No</th>
                                            <th>Model</th>
                                            <th>Servie Type</th>
                                            <th>Bill Amount(Tk)</th>
                                            <th>Job No</th>
                                            <th>Bill No</th>
                                            <th>Print Bill</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex+1 %></td>
                                    <td><%#Eval("VEHICLE") %></td>
                                    <td><%#Eval("CREATEDATE","{0:dd/MM/yyyy}") %></td>
                                    <td><%#Eval("UPDATEDATE","{0:dd/MM/yyyy}") %></td>
                                    <td><%#Eval("REGNO") %></td>
                                    <td><%#Eval("CHASSISNO") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("SERVICETYPE") %></td>
                                    <td><%#Eval("TOTALBILL") %></td>
                                    <td><%#Eval("JOBID") %></td>
                                    <td><%#Eval("BILLNO") %></td>
                                    <td>
                                        <asp:ImageButton ID="btnBillPrint" runat="server" OnCommand="btnBillPrint_Command" CommandArgument='<%# Eval("VEHICLE")+","+Eval("JOBID")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print" CausesValidation="False" />

                                     <%--  <div class="yildiz"><asp:ImageButton ID="ImageButton4" runat="server" Height="19px" ImageUrl="~   commandArgument='<%#Eval("sdasdas") + ","+Eval("fafasfa") %>' /></div>--%>
                                    </td>

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

