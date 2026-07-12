<%@ Page Title="Vehicle PI History" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VehiclePIHistory.aspx.cs" Inherits="Pages_VISM_VehiclePIHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                WorkShop
            </label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlWorkShop" Width="100%" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
                <asp:Button ID="btnExport" Visible="False" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_OnClick" />
            </div>
        </div>
    </div>
    <hr/>
    <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title text-center">PI Vehicle List</div>
                    </div>                     
                    <div class="panel-body">
                        <div style="overflow: auto; max-height: 500px;">
                        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_OnItemDataBound">
    <HeaderTemplate>
        <table id="dt" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                <thead>
                <tr>
                    <th>
                        SL#
                    </th>
            <asp:Repeater runat="server" ID="hdrRpt">
                <ItemTemplate>
                    <th>
                        <%# Container.DataItem %>
                    </th>
                    
                </ItemTemplate>
            </asp:Repeater>
                    
        </tr>
                </thead>
                <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%#Container.ItemIndex+1 %></td>
            <asp:Repeater runat="server" ID="bdyRpt">
                <ItemTemplate>
                    <td>
                        <%# Container.DataItem %>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
            
        </tr>

    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        
            </table>
        </FooterTemplate>
</asp:Repeater>
</div>
                        </div>
                   
                </div>
    <%--<div class="col-sm-12">
        <div style="overflow: auto; height: 450px">
            <div class="box">
                <div class="box-body">
                    <asp:Panel ID="pnlExport" runat="server">
                        <center>
                <b style="font-size:24px;">PI History</b><br />
                            <asp:Label ID="lblfromDate" Text="From Date" runat="server"></asp:Label> to
                        <asp:Label ID="lbltoDate" Text="To Date" runat="server"></asp:Label> <br />
              </center>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>
                                <table id="dtt" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#SL No</th>
                                            <th>PI No</th>
                                            <th>Reg.No</th>
                                            <th>Chassis No</th>
                                            <th>Model</th>
                                            <th>Millage</th>
                                            <th>Service Type</th>
                                            <th>Created Date</th>
                                            <th>Driver Name</th>
                                            <th>Driver Mobile</th>
                                            <th>Remarks</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex+1 %></td>
                                    <td><%#Eval("ID") %></td>
                                    <td><%#Eval("REGNO") %></td> 
                                    <td><%#Eval("CHASSISNO") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("MILLAGE") %></td>
                                    <td><%#Eval("SERVICETYPE") %></td>
                                    <td><%#Eval("CREATEDATE","{0:dd/MM/yyyy}") %></td>
                                    <td><%#Eval("DRIVERNAME") %></td>
                                    <td><%#Eval("DRIVERMOBILE") %></td>
                                    <td><%#Eval("REMARKS") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                        
                            </FooterTemplate>
                        </asp:Repeater>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
   <script src="../../Content/dt/dataTables.buttons.min.js"></script>
    <script src="../../Content/dt/buttons.flash.min.js"></script>
    <script src="../../Content/dt/jszip.min.js"></script>
    <script src="../../Content/dt/pdfmake.min.js"></script>
    <script src="../../Content/dt/vfs_fonts.js"></script>
    <script src="../../Content/dt/buttons.html5.min.js"></script>
    <script src="../../Content/dt/buttons.print.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#dt').DataTable({
                dom: 'Bfrtip',
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true,
                "order": [[0, "desc"]],
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>

