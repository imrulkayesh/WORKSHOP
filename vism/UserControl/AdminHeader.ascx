<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminHeader.ascx.cs" Inherits="UserControl_AdminHeader" %>
<header class="main-header">
    <a href="../../Pages/Admin/Default.aspx" class="logo">
        <span class="logo-mini"><b>WORKSHOP</b></span>
        <span class="logo-lg"><b>WORKSHOP</b></span>
    </a>
    <nav class="navbar navbar-static-top" role="navigation">
        <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
            <span class="sr-only">Toggle navigation</span>
        </a>
        <div class="navbar-custom-menu">
            <ul class="nav navbar-nav">
                <li><a href="../../Pages/Admin/Default.aspx"><i class="fa fa-mail-reply-all"></i> Modules</a></li>
                <li class="dropdown notifications-menu">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <i class="fa fa-bell-o"></i>
                        <span id="lblNotice" class="label label-warning" runat="server"></span>
                    </a>
                    <ul class="dropdown-menu">
                        <li class="header">You have
                            <asp:Label ID="lblcount" runat="server"></asp:Label>
                            notifications</li>
                        <li>
                            <ul class="menu">
                                <li>
                                    <asp:Repeater ID="rptNotice" runat="server">
                                        <ItemTemplate>
                                            <table id="example12" class="table table-bordered table-hover">
                                                <tr>
                                                    <td>
                                                        <a href="<%#String.Concat("../../Pages/Notification/View.aspx?Id=",Eval("ID")) %>">
                                                            <i class="fa fa-envelope"></i><%#Eval("Title") %>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </li>

                            </ul>
                        </li>
                        <li class="footer"><a href="#">View all</a></li>
                    </ul>
                </li>
                <li class="dropdown user user-menu">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <img src="../../Content/Images/Common/avatar5.png" class="img-circle" height="15" width="15" />
                        <span class="hidden-xs">
                            <asp:Label ID="lblUser" runat="server"></asp:Label></span>
                    </a>
                    <ul class="dropdown-menu">
                        <li class="user-header">
                            <img src="../../Content/Images/Common/avatar5.png" class="img-circle" />
                            <div>
                                <h3><asp:Label ID="lblUserName" runat="server"></asp:Label></h3>

                            </div>
                        </li>
                        <li class="user-body">
                            <div class="row">
                                <div class="col-sm-12 text-center" style="color: white;">
                                    <a href="../../Pages/UserManagement/ChangePassword.aspx" class="btn btn-block btn-default btn-flat">Change Password</a>

                                </div>
                            </div>
                        </li>
                        <li class="user-footer">
                            <div class="pull-left">
                                <a href="../../Pages/User/Profile.aspx" class="btn btn-info btn-flat">Profile</a>
                            </div>
                            <div class="pull-right">
                                <asp:Button ID="btnLogout" UseSubmitBehavior="false" runat="server" CausesValidation="False" Text="Logout" OnClick="btnLogout_Click" CssClass="btn btn-default btn-flat" />
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>

    </nav>
</header>
