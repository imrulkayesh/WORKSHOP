<%@ Page Title="Charges" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Charges.aspx.cs" Inherits="Pages_VISM_Charges" %>

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
                        Business Center</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddllocationid" runat="server" CssClass="form-control select2" DataTextField="CENTERNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddllocationid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        WorkShop</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlworkshopid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshopid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Category</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCategoryid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CATNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Charges</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCharges" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CHARGENAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Amount</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxamount" runat="server" placeholder="Enter Amount" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Currency</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCurrencyid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CURRENCYCODE" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Remarks</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxremark" runat="server" placeholder="Enter Remark" CssClass="form-control"></asp:TextBox>
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
                                                    <th>Category</th>
                                                    <th>WorkShop</th>
                                                    <th>Charge</th>
                                                    <th>Amount</th>
                                                    <th>Remark</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("CATNAME") %></td>
                                            <td><%#Eval("WORKSHOP") %></td>
                                            <td><%#Eval("CHARGENAME") %></td>
                                            <td><%#Eval("AMOUNT") %> <%#Eval("CURRENCYCODE") %></td>
                                            <td><%#Eval("REMARKS") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
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

