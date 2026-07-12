<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsUpdate.ascx.cs" Inherits="UserControl_NewsUpdate" %>
<section class="container" style="padding-bottom:0; margin-bottom:0;">
    <div class="col-md-12" style="padding:0; margin:0; margin-bottom:-30px;">
        <ul id="js-news" class="js-hidden">
            <asp:Repeater runat="server" ID="rptNews">
                <ItemTemplate>
                    <li class="news-item"><asp:HyperLink ID="HyperLink2" runat="server" Text='<%#Eval("ShortDescription") %>' NavigateUrl='<%# String.Concat("~/Pages/user/NewsDetails.aspx?ID=", Eval("Id")) %>'></asp:HyperLink></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</section>
