<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Login</title>
    <link rel="shortcut icon" href="/Images/Common/logo.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="College Education Responsive Template" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="author" content="Esmet" />
    <link href="Content/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/ionicons-2.0.1/css/ionicons.min.css" rel="stylesheet" />
    <link href="Content/Styles/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="../../Content/Styles/AdminLTE.min.css" rel="stylesheet" />--%>
    <link href="Content/Styles/AdminLTE.css" rel="stylesheet" />
    <style>
         html, body {
                height: 100%;
                margin: 0;
                padding: 0;
            }
        body {
            background-image: url('./Pages/img/login-bg.jpg'); 
            background-size: cover;          
            background-repeat: no-repeat;    
            background-position: center;     
            background-attachment: fixed;   
        }

          /*body {
            background-image: url('/Pages/img/login-bg.jpg'); 
            background-size: 100% 100%;       
            background-repeat: no-repeat;
            background-position: center;
           }*/

        .panel, .login-box {
            background-color: rgba(255, 255, 255, 0.95); /* Adds a white translucent background to panels for better readability */
            border-radius: 10px;
            padding: 15px;
        }
   </style>

</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 style="color:red;" class="panel-title">Use HRIS User ID and Password for login</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="login-box" style="margin-top: 25%">
                    <!-- /.login-logo -->
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title text-center"> Login Panel</h3>
                        </div>
                        <div class="panel-body">
                            <form id="formLogin" runat="server">
                                <asp:Panel runat="server" ID="pnlLogin" DefaultButton="LoginButton">
                                    <div class="form-login">
                                        <div class="col-md-12 pb-10px ">
                                            <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="Login" />
                                            <span class="LogIn" style="color: red;">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></span>
                                        </div>
                                        <div class="form-group">
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">User Name</label>
                                            <div style="margin-bottom: 25px" class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                                <asp:TextBox CssClass="form-control" ID="tbxUserName" runat="server" placeholder="username">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="UserNameRequired" runat="server" ControlToValidate="tbxUserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Password</label>
                                            <div style="margin-bottom: 25px" class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                                <asp:TextBox CssClass="form-control" ID="tbxPassword" runat="server" TextMode="Password"
                                                    placeholder="password"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" ID="PasswordRequired" runat="server" ControlToValidate="tbxPassword"
                                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div>
                                            <asp:Button ID="LoginButton" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="LoginButton_Click" ValidationGroup="Login" />
                                             <asp:Button ID="btnLoginAndChange" Visible="False" runat="server" Text="Login and Change Mobile" CssClass="btn btn-success btn-block" OnClick="btnLoginAndChange_OnClick" ValidationGroup="Login" />
                                            
                                        </div>
                                    </div>
                                </asp:Panel>
                            </form>
                        </div>
                        <div class="panel-footer">
                            <a href="#">Forget Password?</a>
                        </div>
                    </div>

                </div>
            </div>
            <%--<div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Upcomming Updates</h3>
                        <h3 class="panel-title"><i>Your Cooperation is Highly Expected</i></h3>
                    </div>
                    <div class="panel-body">
                        <div style="margin-bottom: 25px" class="input-group">
                            <label>#Legal(New)</label><br />
                            <label>#Service Reporting</label><br />
                            <label>#Export Pran</label><br />
                        </div>
                    </div>
                </div>
            </div>--%>
        </div>
    </div>

</body>
</html>
