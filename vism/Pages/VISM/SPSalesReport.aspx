<%@ Page Title="Spare Parts Sales Report" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SPSalesReport.aspx.cs" Inherits="Pages_VISM_SPSalesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Workshop
            </label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlWorkshop"  Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID"></asp:DropDownList>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                SellByStaffId
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxStaffID" runat="server" placeholder="Enter To Sales person staffid" CssClass="form-control"></asp:TextBox>
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
            <div class="col-sm-12 text-right">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Show" />
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
                                        <th>SalesID</th>
                                        <th>Customer</th>
                                        <th>Date</th>
                                        <th>Total</th>
                                        <th>Discount</th>
                                        <th>Net Amount</th>
                                        <th>BillPaid</th>
                                        <th>Delivered</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("ID") %></td>
                            <td><a href='SpSalseInvoice.aspx?SalesID=<%#Eval("ID") %>' target="_blank"><%#Eval("CUSTOMER")%></a></td>
                            <td><%#Eval("SELLDATE")%></td>
                            <td><%#Eval("TOTALAMOUNT")+" "+Eval("CURRENCYCODE") %></td>
                            <td><%#Eval("DISCOUNT")+" "+Eval("CURRENCYCODE")%></td>
                            <td><%#Eval("NETAMOUNT")+" "+Eval("CURRENCYCODE")%></td>
                            <td><%#Eval("BILLPAID").ToString().Replace("0","No").Replace("1","Yes") %></td>
                            <td><%#Eval("DELIVERED").ToString().Replace("0","No").Replace("1","Yes") %></td>
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
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>

