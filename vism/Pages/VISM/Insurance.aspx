<%@ Page Title="Insurance Company" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Insurance.aspx.cs" Inherits="Pages_VISM_Insurance" %>

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
                        <asp:Label ID="Label5" runat="server" Text="Insurance Company"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Insurance Company" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Insurance Company" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxAddress" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Address" ControlToValidate="tbxAddress">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label3" runat="server" Text="Mobile"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Mobile" ControlToValidate="tbxMobile">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label4" runat="server" Text="Phone"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label7" runat="server" Text="Fax"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxFax" runat="server" placeholder="Enter Fax" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
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
                                <asp:Repeater ID="rptYear" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label5" runat="server" Text="Insuranc Company"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label8" runat="server" Text="Mobile"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="Address"></asp:Label></th>
                                                    <th>
                                                        Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("COMPANYNAME") %></td>
                                            <td><%#Eval("MOBILE") %></td>
                                            <td><%#Eval("EMAIL") %></td>
                                            <td><%#Eval("ADDRESS") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%></td>
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

