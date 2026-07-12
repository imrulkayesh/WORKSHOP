<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VISMdashboard.aspx.cs" Inherits="Pages_VISM_ItemAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="refresh" content="600">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script src="assets/js/loader.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Monthly Sales Comparison till today in Value
                                </div>
                                <div class="panel-body">
                                    <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
                                    <div id="chart_div" style="height: 400px">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

