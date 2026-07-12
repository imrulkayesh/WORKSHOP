<%@ Page Title="Spare Parts Sale Store" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SpSaleStore.aspx.cs" Inherits="Pages_VISM_SpSaleStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            Pending for Delivery
        </div>
        <div class="panel-body">
            <asp:Repeater ID="rptSales" runat="server">
                <HeaderTemplate>
                    <table id="example1" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>SalesID</th>
                                <th>Customer</th>
                                <th>Total</th>
                                <th>Discount</th>
                                <th>Net Amount</th>
                                <th>Date</th>
                                <th>BillPaid</th>
                                <th>Delivered</th>
                                <th>Print</th>
                                <th>Pay</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ID") %></td>
                        <td><%#Eval("CUSTOMER")%></td>
                        <td><%#Eval("TOTALAMOUNT")+" "+Eval("CURRENCYCODE") %></td>
                        <td><%#Eval("DISCOUNT")+" "+Eval("CURRENCYCODE")%></td>
                        <td><%#Eval("NETAMOUNT")+" "+Eval("CURRENCYCODE")%></td>
                        <td><%#Eval("SELLDATE")%></td>
                        <td><%#Eval("BILLPAID").ToString().Replace("0","No").Replace("1","Yes") %></td>
                        <td><%#Eval("DELIVERED").ToString().Replace("0","No").Replace("1","Yes") %></td>
                        <td>
                            <asp:ImageButton ID="btnPrint" runat="server" OnCommand="btnPrint_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/printer_icon.png" ToolTip="Invoice Print" />

                        </td>
                        <td>
                            <asp:ImageButton ID="btnPay" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/oksmal.jpg" ToolTip="Pay" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                                </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

