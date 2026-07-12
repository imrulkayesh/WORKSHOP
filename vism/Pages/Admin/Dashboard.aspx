<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Admin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <asp:UpdatePanel ID="UpdatePie" runat="server">
        <ContentTemplate>
            <div class="row">
<%--                <div class="col-sm-6">
                    <div class="box box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title">Dispatch Overview</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
                            <div id="DispatchChart" style="height: 500px; width:450px;">
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="box box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title">Service Overview</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <asp:Literal ID="ltService" runat="server"></asp:Literal>
                            <div id="ServiceChart" style="height: 500px; width:450px;">
                            </div>

                        </div>
                    </div>
                </div>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
