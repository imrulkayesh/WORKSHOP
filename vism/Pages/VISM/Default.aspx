<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_VISM_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <%--<img class="img-responsive" style="width: 100%; height: 650px;" src="../../Content/Images/Common/dashbord.jpg" />--%>

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <%--<span>Today's Job Chart</span>--%>
                <asp:HiddenField runat="server" ID="hdnUnitName" />
                <div class="panel panel-success" style="display: none;">
                    <div class="panel-heading">
                        Today's Job Chart
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <%--                        <div class="col-md-2">
                            </div>--%>
                            <div class="col-md-12" style="height: 200px">
                                <%--                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Today's Job Chart
                                </div>--%>
                                <div class="panel-body">
                                    <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
                                    <div id="chart_div" style="height: 200px">
                                    </div>
                                    <%--</div>--%>
                                </div>
                            </div>
                            <%--                        <div class="col-md-2">
                            </div>--%>
                        </div>

                    </div>
                </div>

                <%--<hr />--%>
                <%--</div>--%>
                <section class="content" style="display: none;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-4">
                                    Last Day's Job Hisotry
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                    Last Update : <span>
                                        <asp:Label ID="UDATE1" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="container-fluid">
                            <div class="row">
                                <%--<div class="col-lg-3 col-6">
                            <!-- small box -->
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblJI" runat="server" Font-Bold="True" SkinID="message"></asp:Label></h3>

                                    <p>Daily Job Received</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-person-add"></i>
                                </div>
                            </div>
                        </div>--%>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <div class="info-box">
                                        <span class="info-box-icon" style="background-color: chocolate"><i class="fa fa-user-plus"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text"><b>Job Received</b></span>
                                            <span class="info-box-number">
                                                <asp:Label ID="lblJI" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                        </div>
                                        <!-- /.info-box-content -->
                                    </div>
                                    <!-- /.info-box -->
                                </div>
                                <%--<div class="col-lg-3 col-6">
                            <!-- small box -->
                            <div class="small-box bg-warning">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblJO" runat="server" Font-Bold="True" SkinID="message"></asp:Label></h3>

                                    <p>Daily Job Delivery</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-person-minus"></i>
                                </div>
                            </div>
                        </div>--%>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <div class="info-box">
                                        <span class="info-box-icon" style="background-color: darkseagreen"><i class="fa fa-car"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text"><b>Job Delivery</b></span>
                                            <span class="info-box-number">
                                                <asp:Label ID="lblJO" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                        </div>
                                        <!-- /.info-box-content -->
                                    </div>
                                    <!-- /.info-box -->
                                </div>
                                <!-- ./col -->
                                <%--<div class="col-lg-3 col-6">
                            <!-- small box -->
                            <div class="small-box" style="background-color: #fcf8e3">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblper" runat="server" Font-Bold="True" SkinID="message"></asp:Label><sup style="font-size: 20px">%</sup></h3>
                                    <p>Daily Assessment</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                            </div>
                        </div>--%>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <div class="info-box">
                                        <span class="info-box-icon" style="background-color: cornflowerblue"><i class="fa fa-line-chart"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text"><b>Job Assessment</b></span>
                                            <span class="info-box-number">
                                                <asp:Label ID="lblper" runat="server" Font-Bold="True" SkinID="message"></asp:Label>%</span>
                                        </div>
                                        <!-- /.info-box-content -->
                                    </div>
                                    <!-- /.info-box -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <%--            </section>
            <section class="content">--%>
                    <%--<div class="container-fluid">
                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <div class="col-lg-3 col-6">
                            <!-- small box -->
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <h3>150</h3>

                                    <p>New Orders</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-bag"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                        <div class="col-lg-3 col-6">
                            <!-- small box -->
                            <div class="small-box bg-danger">
                                <div class="inner">
                                    <h3>65</h3>

                                    <p>Unique Visitors</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                        <!-- ./col -->
                    </div>
                </div>--%>
                    <%--<hr />--%>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                    Last Update : <span>
                                        <asp:Label ID="UDATE" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Repeater ID="rpt_co" runat="server">
                                    <HeaderTemplate>
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Consumption (This Month):</h3>
                                            <%--<div class="box-tools pull-right">
                                                <a class="btn btn-box-tool"><i class="fa fa-refresh"></i></a>
                                            </div>--%>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body no-padding">
                                            <table id="dtt" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="background-color: chocolate;">
                                                        <th style="text-align: center; vertical-align: middle">SL</th>
                                                        <th style="text-align: center; vertical-align: middle">Name of Wksp</th>
                                                        <th style="text-align: center; vertical-align: middle">Consumption (<span><b>BDT</b></span>)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #f2dedf;">
                                            <td style="text-align: center; vertical-align: middle"><%# Container.ItemIndex+1 %></td>
                                            <td><%#Eval("RVS_C1VALUE") %></td>
                                            <td <%# Convert.ToDecimal(Eval("RVS_C2VALUE"))<0?"style=color:red;text-align:right; vertical-align:middle":"style=text-align:right; vertical-align:middle"  %>><%#Eval("RVS_C2VALUE") %> </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                <tfoot>
                                    <tr style="background-color: darkseagreen">
                                        <th></th>
                                        <th style="text-align: right; vertical-align: middle"><b>Total Consumption</b></th>
                                        <td style="text-align: right; vertical-align: middle">
                                            <asp:Label ID="lblCOTotal" runat="server" />
                                            <span><b>৳</b></span></td>
                                    </tr>
                                </tfoot>
                                        </table>
                                </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-md-6">
                                <asp:Repeater ID="rpt_stock" runat="server">
                                    <HeaderTemplate>
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Stock (This Month):</h3>
                                            <%--<div class="box-tools pull-right">
                                                <a class="btn btn-box-tool"><i class="fa fa-refresh"></i></a>
                                            </div>--%>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body no-padding">
                                            <table id="dt" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="background-color: cornflowerblue">
                                                        <th style="text-align: center; vertical-align: middle">SL</th>
                                                        <th style="text-align: center; vertical-align: middle">Name of Wksp</th>
                                                        <th style="text-align: center; vertical-align: middle">Stock (<span><b>BDT</b></span>)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #f2dedf">
                                            <td style="text-align: center; vertical-align: middle"><%# Container.ItemIndex+1 %></td>
                                            <td><%#Eval("RVS_C1VALUE") %></td>
                                            <td <%# Convert.ToDecimal(Eval("RVS_C2VALUE"))<0?"style=color:red;text-align:right; vertical-align:middle":"style=text-align:right; vertical-align:middle"  %>><%#Eval("RVS_C2VALUE") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                <tfoot>
                                    <tr style="background-color: darkseagreen">
                                        <th></th>
                                        <th style="text-align: right; vertical-align: middle"><b>Total Stock</b></th>
                                        <td style="text-align: right; vertical-align: middle">
                                            <asp:Label ID="lblSTTotal" runat="server" />
                                            <span><b>৳</b></span></td>
                                    </tr>
                                </tfoot>
                                        </table>
                       </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                    </div>
                    <%-- Monthly Job Count --%>
                    <hr />
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-4">
                                    This Month Job Hisotry
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                    Last Update : <span>
                                        <asp:Label ID="Mtime" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <div class="info-box">
                                        <span class="info-box-icon" style="background-color: chocolate"><i class="fa fa-user-plus"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text"><b>Job Received</b></span>
                                            <span class="info-box-number">
                                                <asp:Label ID="MJR" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <div class="info-box">
                                        <span class="info-box-icon" style="background-color: darkseagreen"><i class="fa fa-car"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text"><b>Job Delivery</b></span>
                                            <span class="info-box-number">
                                                <asp:Label ID="MJD" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                        </div>
                                        <!-- /.info-box-content -->
                                    </div>
                                    <!-- /.info-box -->
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <div class="info-box">
                                        <span class="info-box-icon" style="background-color: cornflowerblue"><i class="fa fa-line-chart"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text"><b>Job Assessment</b></span>
                                            <span class="info-box-number">
                                                <asp:Label ID="MJP" runat="server" Font-Bold="True" SkinID="message"></asp:Label>%</span>
                                        </div>
                                        <!-- /.info-box-content -->
                                    </div>
                                    <!-- /.info-box -->
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Repeater ID="rpt_JR" runat="server">
                                    <HeaderTemplate>
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Job Receive (This Month)</h3>
                                            <%--<div class="box-tools pull-right">
                                                <a class="btn btn-box-tool"><i class="fa fa-refresh"></i></a>
                                            </div>--%>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body no-padding">
                                            <table id="dtt" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="background-color: chocolate">
                                                        <th style="text-align: center; vertical-align: middle">SL</th>
                                                        <th style="text-align: center; vertical-align: middle">Service Type</th>
                                                        <th style="text-align: center; vertical-align: middle">Receive Qty</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #f2dedf">
                                            <td style="text-align: center; vertical-align: central"><%# Container.ItemIndex+1 %></td>
                                            <td><%#Eval("RVS_C1VALUE") %></td>
                                            <td style="text-align: right; vertical-align: middle"><%#Eval("RVS_C2VALUE") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                <tfoot>
                                    <tr style="background-color: darkseagreen">
                                        <th></th>
                                        <th style="text-align: right; vertical-align: middle"><b>Total Job Received</b></th>
                                        <td style="text-align: right; vertical-align: middle">
                                            <asp:Label ID="lblJRTotal" runat="server" /></td>
                                    </tr>
                                </tfoot>
                                        </table>
                                </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-md-4">
                                <asp:Repeater ID="rpt_JD" runat="server">
                                    <HeaderTemplate>
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Job Delivery (This Month)</h3>
                                            <%--<div class="box-tools pull-right">
                                                <a class="btn btn-box-tool"><i class="fa fa-refresh"></i></a>
                                            </div>--%>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body no-padding">
                                            <table id="dt" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="background-color: seagreen">
                                                        <th style="text-align: center; vertical-align: middle">SL</th>
                                                        <th style="text-align: center; vertical-align: middle">Service Type</th>
                                                        <th style="text-align: center; vertical-align: middle">Delivery Qty</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #f2dedf">
                                            <td style="text-align: center; vertical-align: middle"><%# Container.ItemIndex+1 %></td>
                                            <td><%#Eval("RVS_C1VALUE") %></td>
                                            <td style="text-align: right; vertical-align: middle"><%#Eval("RVS_C2VALUE") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                <tfoot>
                                    <tr style="background-color: darkseagreen">
                                        <th></th>
                                        <th style="text-align: right; vertical-align: middle"><b>Total Job Delivery</b></th>
                                        <td style="text-align: right; vertical-align: middle">
                                            <asp:Label ID="lblJDTotal" runat="server" /></td>
                                    </tr>
                                </tfoot>
                                        </table>
                                </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-md-4">
                                <asp:Repeater ID="rpt_JS" runat="server">
                                    <HeaderTemplate>
                                        <div class="box-header with-border">
                                            <h5 class="box-title">Job Staying</h5>
                                            <%--<div class="box-tools pull-right">
                                                <a class="btn btn-box-tool"><i class="fa fa-refresh"></i></a>
                                            </div>--%>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body no-padding">
                                            <table id="dt" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="background-color: cornflowerblue">
                                                        <th style="text-align: center; vertical-align: middle">SL</th>
                                                        <th style="text-align: center; vertical-align: middle">Service Type</th>
                                                        <th style="text-align: center; vertical-align: middle">Staying Qty</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #f2dedf">
                                            <td style="text-align: center; vertical-align: middle"><%# Container.ItemIndex+1 %></td>
                                            <td><%#Eval("RVS_C1VALUE") %></td>
                                            <td style="text-align: right; vertical-align: middle"><%#Eval("RVS_C2VALUE") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                <tfoot>
                                    <tr style="background-color: darkseagreen">
                                        <th></th>
                                        <th style="text-align: right; vertical-align: middle"><b>Total Job Staying</b></th>
                                        <td style="text-align: right; vertical-align: middle">
                                            <asp:Label ID="lblTotal" runat="server" /></td>
                                    </tr>
                                </tfoot>
                                        </table>
                       </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <%--            <section class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-3 col-6">
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblJI" runat="server" Font-Bold="True" SkinID="message"></asp:Label></h3>

                                    <p>Daily Job Received</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-bag"></i>
                                </div>
                                <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </section>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

