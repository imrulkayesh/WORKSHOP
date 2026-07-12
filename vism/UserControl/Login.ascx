<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="UserControl_Login" %>
<asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Default.aspx" TitleText=""
    OnLoggedIn="Login1_LoggedIn" FailureAction="RedirectToLoginPage">
    <LayoutTemplate>
        <asp:Panel runat="server" ID="pnlLogin" DefaultButton="LoginButton">
            <div class="form-login">
                <div class="col-md-12 pb-10px ">
                    <span class="LogIn">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></span>
                </div>
                <div class="form-group clearfix">
                    <asp:TextBox CssClass="form-control" ID="UserName" runat="server" placeholder="username"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="None" ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="Password" runat="server" TextMode="Password"
                        placeholder="password"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="None" ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                </div>
            <div>
                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" CssClass="btn btn-primary"/>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../Pages/Security/ForgotPassword.aspx">Forgot Password?</asp:HyperLink>
            </div>
            </div>
        </asp:Panel>
    </LayoutTemplate>
</asp:Login>
