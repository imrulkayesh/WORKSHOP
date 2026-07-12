<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemMerge.aspx.cs" Inherits="Pages_VISM_ItemMerge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <div class="form-horizontal">
        <div id="background">&nbsp</div>
        <div class="form-group">
            <div id="dddd" style="margin-left: 200px;" class="col-sm-6">
                <asp:Button runat="server" ID="ItemMerge" ClientIDMode="Static" Text="Just click me to open new item" Font-Size="30" Width="700" Height="200" CssClass="btn btn-success derw" OnClick="ItemMerge_OnClick"/>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
    <script>
        var i = 0;
        function change() {
            var doc = document.getElementById("ItemMerge");
            var color = ["black", "blue", "brown", "green"];
            doc.style.backgroundColor = color[i];
            i = (i + 1) % color.length;
        }
        setInterval(change, 500);
    </script>
</asp:Content>

