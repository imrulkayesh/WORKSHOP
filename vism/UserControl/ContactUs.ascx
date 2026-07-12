<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactUs.ascx.cs" Inherits="UserControl_ContactUs" %>
<div class="widget-main">
    <div class="widget-main-title">
        <h4 class="widget-title">Contact Us</h4>
    </div>
    <div class="widget-inner">
        <div class="form-group">
            <asp:TextBox CssClass="form-control" ID="UserName" runat="server" placeholder="Name">
                                            </asp:TextBox>
        </div>
        <div class="form-group">
           <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Email">
                                            </asp:TextBox>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Mobile">
                                            </asp:TextBox>
        </div>
        <div class="form-group">
            <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Subject">
                                            </asp:TextBox>
        </div>
        <div class="form-group">
           <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="3" ID="TextBox4" runat="server" placeholder="Message">
                                            </asp:TextBox>
        </div>
        <asp:Button ID="btnSubmit" CssClass="btn btn-success" Text="Submit" runat="server" />
    </div>
</div>
