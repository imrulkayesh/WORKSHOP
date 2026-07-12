<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_VISM_Dashboard" %>

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
                <div class="panel panel-success" style="display:none;">
                    <div class="panel-heading">
                        Today's Job Chart
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12" style="height: 200px">
                                <div class="panel-body">
                                    <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
                                    <div id="chart_div" style="height: 200px">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <section class="content">
                    <%--  Work Order Count --%>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-4">
                                    Work Order Hisotry
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                     <span>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" SkinID="message"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-lg-4 col-6">
                                    <div class="small-box bg-info">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="lblJI" runat="server" Font-Bold="True" SkinID="message"></asp:Label></h3>

                                            <p style="font-weight: bold">TODAY WORK ORDER</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                    </div>
                               </div>
                                <div class="col-lg-4 col-6">
                                    <div class="small-box bg-warning">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="lblJO" runat="server" Font-Bold="True" SkinID="message"></asp:Label></h3>

                                            <p style="font-weight: bold">THIS MONTH WORK ORDER</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-6">
                                    <div class="small-box bg-info">
                                        <div class="inner">
                                            <h3>
                                                <asp:Label ID="lblper" runat="server" Font-Bold="True" SkinID="message"></asp:Label><sup style="font-size: 20px"></sup></h3>
                                            <p style="font-weight: bold">LAST MONTH WORK ORDER</p>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <%--<a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--  Job Count --%>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-md-6">
                                    Job Summary
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Repeater ID="rpt_JR" runat="server">
                                    <HeaderTemplate>
                                       <%-- <div class="box-header with-border">
                                            <h3 class="box-title">Job Summary</h3>
                                        </div>--%>
                                        <%--<br />--%>
                                        <div class="box-body no-padding">
                                            <table id="dtt" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="background-color: bisque">
                                                        <th style="text-align: center; vertical-align: middle">SL</th>
                                                        <th style="text-align: center; vertical-align: middle">Period</th>
                                                        <th style="text-align: center; vertical-align: middle">Received</th>
                                                        <th style="text-align: center; vertical-align: middle">Done</th>
                                                        <th style="text-align: center; vertical-align: middle">Pending</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr style="background-color: #f2dedf">
                                            <td style="text-align: center; vertical-align: middle"><%# Container.ItemIndex + 1 %></td>
                                            <td style="text-align: center; vertical-align: middle"><%# Eval("PERIOD") %></td>
                                            <td style="text-align: right; vertical-align: middle"><%# Eval("JOB_RECEIVED") %></td>
                                            <td style="text-align: right; vertical-align: middle"><%# Eval("JOB_DONE") %></td>
                                            <td style="text-align: right; vertical-align: middle"><%# Eval("JOB_PENDING") %></td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </tbody>
                                    </table>
                                    </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
