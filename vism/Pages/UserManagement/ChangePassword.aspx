<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_UserManagement_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-md-9">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="save" />
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                     UserName</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="tbxResetUser" ClientIDMode="Static" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label19" runat="server" Text="New Password"></asp:Label></label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="tbxPass" TextMode="Password" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Enter new password"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter new password" ControlToValidate="tbxPass">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3">
                        <asp:Label ID="Label20" runat="server" Text="Confirm Password"></asp:Label></label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="tbxConPass" TextMode="Password" ClientIDMode="Static" runat="server" placeholder="Enter confirm password" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter confirm password" ControlToValidate="tbxConPass">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="compair" runat="server" ControlToValidate="tbxConPass" ControlToCompare="tbxPass" ErrorMessage="Password does not match." ValidationGroup="save"></asp:CompareValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <asp:Button ID="btnResetPassword" runat="server" CssClass="btn btn-danger" Text="Reset Password" ValidationGroup="save" OnClick="btnResetPassword_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

