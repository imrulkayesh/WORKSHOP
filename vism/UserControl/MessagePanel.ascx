<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessagePanel.ascx.cs"
    Inherits="UserControls_MessagePanel" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
<asp:Panel runat="server" ID="pnlMessage" Visible="false">
    <div  runat="server" id="dvmessage">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>
            <asp:Label ID="lblMessageTitle" runat="server" />!</strong>
        <asp:Label ID="lblMessageDetails" runat="server"></asp:Label>
    </div>
</asp:Panel>
 </contenttemplate>
</asp:UpdatePanel>
