<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminMenu.ascx.cs" Inherits="UserControls_AdminMenu" %>
<aside class="main-sidebar">
    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar">
        <!-- Sidebar user panel -->
        <div class="user-panel">
            <div class="pull-left image">
                <img src="../../Content/Images/Common/avatar5.png" class="img-circle" />
            </div>
            <div class="pull-left info">
                <p>
                    Welcome!
                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                </p>
                <a href="#"><i class="fa fa-circle text-success"></i>Online</a>
            </div>
        </div>
        <div class="input-group sidebar-form">
            <input type="text" name="q" class="form-control" placeholder="Search...">
            <span class="input-group-btn">
                <button type="submit" name="search" id="search-btn" class="btn btn-flat">
                    <i class="fa fa-search"></i>
                </button>
            </span>
        </div>
        <ul class="sidebar-menu">
            <li class="header">MAIN NAVIGATION</li>
            <asp:Repeater runat="server" ID="rptParent" OnItemDataBound="rptCategory_OnItemDataBound">
                <ItemTemplate>
                    <li class="treeview">
                        <%--<a href='<%# "/vismf"+Eval("URL") %>'>--%>
                         <a href='<%#Eval("URL") %>'>
                          <i class='fa <%#Eval("Icon") %>'></i><span><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString() %></span> <i class="fa fa-angle-left pull-right"></i>
                        </a
                        <asp:HiddenField runat="server" ID="hdnValue" Value='<%#Eval("Id")%>' />
                            <asp:Repeater runat="server" ID="rptChild" >
                                <HeaderTemplate> <ul class="treeview-menu"></HeaderTemplate>
                                <ItemTemplate>
                                     <li> 
                                         <%--<a href='<%# "/vismf"+Eval("URL") %>'>--%>
                                         <a href='<%#Eval("URL") %>'>
                                         <i class='fa <%#Eval("Icon") %>'></i><span><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString()  %></span>
                                        </a>
                                     </li>
                                </ItemTemplate>
                                <FooterTemplate></ul></FooterTemplate>
                            </asp:Repeater>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </section>
    <!-- /.sidebar -->
</aside>
