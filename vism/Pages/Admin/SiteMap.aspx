<%@ Page Title="Site Map" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="Pages_Admin_SiteMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .inner {
            min-height: 90px;
        }

            .inner a {
                color: #fff;
            }

            .inner:hover, .inner:hover a {
                background-color: #ccffdd;
                color: #ff9900;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <asp:Repeater ID="rptParent" OnItemDataBound="rptCategory_OnItemDataBound" runat="server">
            <ItemTemplate>
                <div class="col-md-2">
                    <div class="small-box bg-green">
                        <div class="inner text-center">
                            <a style="text-decoration: none;" href='<%#Eval("URL") %>' title='<%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString() %>'>
                                <i class='fa fa-2x <%#Eval("Icon") %>'></i>
                                <br />
                                <span><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString() %></span>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnValue" Value='<%#Eval("Id")%>' />
                        <asp:Repeater runat="server" ID="rptChild">
                            <ItemTemplate>
                                <%--<a class="small-box-footer text-left" href='<%#Eval("URL") %>'><i class='fa <%#Eval("Icon") %>'></i><%#Request.Cookies["CurrentLanguage"].Value=="en-US"? Eval("TextEng").ToString():Eval("TextBan").ToString()  %> <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
