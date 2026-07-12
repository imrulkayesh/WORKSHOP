<%@ Control Language="C#" AutoEventWireup="true" CodeFile="News.ascx.cs" Inherits="UserControl_News" %>
<div class="widget-main">
    <div class="widget-main-title">
        <h4 class="widget-title">Latest News</h4>
    </div>
    <div class="widget-inner">
        <div class="holder">
            <ul id="ticker01">
                <asp:Repeater runat="server" ID="rptNews">
                    <ItemTemplate>
                        <li>
                            <div class="blog-list-post clearfix">
                                <div class="blog-list-details col-md-11">
                                    <h5 class="blog-list-title">
                                        <asp:HyperLink ID="HyperLink2" runat="server" Text='<%#Eval("Title") %>' NavigateUrl='<%# String.Concat("~/Pages/user/NewsDetails.aspx?ID=", Eval("Id")) %>'></asp:HyperLink>
                                    </h5>
                                    <p class="blog-list-meta small-text"><span><%# Convert.ToDateTime(Eval("Date").ToString()).ToString("dd-MMM-yyyy")%></span></p>
                                    <p class="small-text"><%# Eval("ShortDescription") %>... ...</p>
                                </div>
                            </div>

                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>
