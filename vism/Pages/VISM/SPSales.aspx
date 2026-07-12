<%@ Page Title="SP Sales" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SPSales.aspx.cs" Inherits="Pages_VISM_SPSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-8">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-10 col-sm-10 col-xs-10">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:TextBox ID="tbxParts" runat="server" placeholder="Parts Name/Id" AutoPostBack="true" OnTextChanged="tbxParts_TextChanged" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField ID="hdnPartsId" runat="server" />
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                            ErrorMessage="Select Parts" ControlToValidate="tbxParts">*</asp:RequiredFieldValidator>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchParts"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxParts"
                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3">
                                        <asp:TextBox ID="tbxQuanity" AutoPostBack="true" OnTextChanged="tbxQuanity_TextChanged" Text="1" runat="server" placeholder="Quanity" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="tbxUnitPrice" runat="server" AutoPostBack="true" OnTextChanged="tbxUnitPrice_TextChanged" placeholder="Unit Price" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="tbxTotal" Enabled="false" runat="server" placeholder="TotalPrice" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="btnAddParts" runat="server" ValidationGroup="save" OnClick="btnAddParts_Click" Text="Add" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="box">
                                            <div class="box-body">
                                                <asp:Repeater ID="rpt" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>Parts</th>
                                                                    <th>UnitPrice</th>
                                                                    <th>Quantity</th>
                                                                    <th>Total</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Eval("PARTSCODE")+"-"+Eval("PARTSNAME") %></td>
                                                            <td><%#Eval("UNITPRICE")%></td>
                                                            <td><%#Eval("QUANTITY")+" "+Eval("UNITCODE") %></td>
                                                            <td><%#Eval("TOTALPRICE")%></td>
                                                            <td>
                                                                <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        WorkShop</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlWorkshop" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkshop_SelectedIndexChanged" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        Coustomer</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxCustomer" runat="server" Text="Walk IN Customer" placeholder="Enter Coustomer Name" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        Total</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxtotalAmount" Enabled="false" runat="server" placeholder="Enter Total Amount" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        Discount</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxDiscount" runat="server" Text="0" AutoPostBack="true" OnTextChanged="tbxDiscount_TextChanged" placeholder="Enter Discount" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        Net Amount</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxNetAmount" Enabled="false" runat="server" placeholder="Enter Net Amount" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4">
                                        Currency</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlCurrency" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CURRENCYCODE" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group text-right">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary"
                                            OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="box">
                    <div class="box-body">
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
                                            <th>Action</th>
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
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" runat="server" OnCommand="btnEdit_Command" Visible="false" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                        <asp:ImageButton ID="btnPrint" runat="server" OnCommand="btnPrint_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/printer_icon.png" ToolTip="Invoice Print" />
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rptSales" />
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

