<%@ Page Title="SpareParts" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SpareParts.aspx.cs" Inherits="Pages_VISM_SpareParts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Workshop</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlWorkshop" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkshop_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Category</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCategory" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CATNAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Unit</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlUnit" Width="100%" runat="server" CssClass="form-control select2" DataTextField="UNITNAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Parts
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxpartname" runat="server" placeholder="Enter Parts Name" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Code
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxCode" runat="server" placeholder="Enter Code" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Stock</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxStock" runat="server" placeholder="Enter Stock" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Cost</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxCost" runat="server" placeholder="Enter Cost" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Selling Price</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxSellingPrice" runat="server" placeholder="Enter Selling Price" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Currency</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCurrency" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CURRENCYCODE" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group text-center">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>WorkShop</th>
                                                    <th>Category</th>
                                                    <th>Unit</th>
                                                    <th>Parts</th>
                                                    <th>Code</th>
                                                    <th>Stock</th>
                                                    <th>Cost</th>
                                                    <th>Selling Price</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("WORKSHOP") %></td>
                                            <td><%#Eval("CATNAME") %></td>
                                            <td><%#Eval("UNITNAME") %></td>
                                            <td><%#Eval("PARTSNAME") %></td>
                                            <td><%#Eval("PARTSCODE") %></td>
                                            <td><%#Eval("STOCK") %></td>
                                            <td><%#Eval("COSTPRICE") %></td>
                                            <td><%#Eval("SELLINGPRICE")%> <%#Eval("CURRENCYCODE") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%>

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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

