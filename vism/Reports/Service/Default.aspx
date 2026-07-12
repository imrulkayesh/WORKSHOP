<%@ Page Title="Overview" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reports_Service_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <div class="row">
        <asp:Repeater ID="rpt" runat="server">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="box box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title"><%#Eval("ChartTitle") %> </h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <asp:Literal ID="ltScripts" Text='<%#Eval("Chart") %>' runat="server"></asp:Literal>
                            <div id='Chart<%# Container.ItemIndex+1 %>' style="width: 400px; height: 300px;">
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

