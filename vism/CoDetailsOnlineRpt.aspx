<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CoDetailsOnlineRpt.aspx.cs" Inherits="CoDetailsOnlineRpt" %>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>Details Report</title>

        <link href="Content/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="Content/ionicons-2.0.1/css/ionicons.min.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/_all-skins.min.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/select2.min.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/jquery.dataTables_themeroller.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/Table.css" rel="stylesheet" type="text/css" />
        <link href="Content/Styles/Custom.css" rel="stylesheet" type="text/css" />
        <link href="Content/DateTimePeaker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
        <style>
            body{

                width:1250px;
                margin: 10 auto;
            }
        </style>

    </head>

    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            <div class="form-horizontal">
                <div class="row">

                    <div class="form-group">
                    
                          <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading" style="height: 35px">
                                    <div class="panel-title" style="text-align: center; margin-top: -7px">Consumption Report (VISM)</div>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">

                                       <label for="inputEmail3" class="col-sm-1">
                                            Vehicle 
                                        </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="tbxVehicle" runat="server" placeholder="Enter Vehicle Id / Registration No / Chassis No" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                                MinimumPrefixLength="1"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                TargetControlID="tbxVehicle"
                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </div>

                                        <label class="col-sm-1">From Date</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
                                        </div>
                                   
                                        <label class="col-sm-1">To Date</label>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btndownloadNormalExcel_OnClick" />
                            
                        </div>
                    </div>
                </div>
            </div>

            <asp:Repeater ID="dtr" runat="server">
                    <HeaderTemplate>

                        <table class="table table-bordered table-hover" style="font-size: 12px;">
                            <thead>
                                <tr>
                                    <th style="text-align:left;">Warehouse</th>
                                    <th style="text-align:left;">Job ID</th>
                                    <th style="text-align:left;">Registration No</th>
                                    <th style="text-align:left;">Service Type</th>
                                    <th style="text-align:left;">TR Date</th>
                                    <th style="text-align:left;">CO ID</th>

                                    <th style="text-align:left;">Item Code</th>
                                    <th style="text-align:left;">Item Name</th>
                                    <th style="text-align:left;">Unit Name</th>
                                    <th style="text-align:left;">Rate</th>
                                    <th style="text-align:left;">Qty</th>
                                    <th style="text-align:left;">Amount</th>
   
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:left;"><%#Eval("WH") %></td>
                            <td style="text-align:left;"><%#Eval("JOBID") %></td>
                            <td style="text-align:left;"><%#Eval("REGNO") %></td>
                            <td style="text-align:left;"><%#Eval("SERVICETYPE") %></td>
                             <td style="text-align:left;"><%#Eval("TRDATE") %></td>
                            <td style="text-align:left;"><%#Eval("ID") %></td>

                            <td style="text-align:left;"><%#Eval("ITEMCODE") %></td>
                            <td style="text-align:left;"><%#Eval("ITEMNAME") %></td>
                             <td style="text-align:left;"><%#Eval("UNITNAME") %></td>
                            <td style="text-align:left;"><%#Eval("RATE") %></td>
                            <td style="text-align:left;"><%#Eval("QTY") %></td>
                            <td style="text-align:left;"><%#Eval("AMOUNT") %></td>
                                  
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                       
                    </FooterTemplate>
             </asp:Repeater>

          

            <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Panel ID="Panel2" runat="server" Width="100%">
                    <asp:GridView ID="GridView2" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" Width="100%" AlternatingRowStyle-ForeColor="#000"
                        runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="WH" HeaderText="WH" />
                            <asp:BoundField DataField="JOBID" HeaderText="JOB ID" />
                            <asp:BoundField DataField="COUNIT" HeaderText="REGNO" />
                            <asp:BoundField DataField="MODELNAME" HeaderText="MODELNAME" />
                            <asp:BoundField DataField="servicetype" HeaderText="SERVICE" />
                            <asp:BoundField DataField="COMPANY" HeaderText="COMPANY" /> 
                            <asp:BoundField DataField="CUSTOMERNAME" HeaderText="CUSTOMER" />
                            <asp:BoundField DataField="CUSCATEGORY" HeaderText="CO CATEGORY" />
                            <asp:BoundField DataField="TRDATE" HeaderText="CO_DATE"/>
                            <asp:BoundField DataField="ID" HeaderText="CO_ID" />
                            <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" />
                            <asp:BoundField DataField="ITEMCODE" HeaderText="ITEMCODE" />
                            <asp:BoundField DataField="ITEMNAME" HeaderText="ITEMNAME" />      
                            <asp:BoundField DataField="UNITNAME" HeaderText="UNIT" />                           
                            <asp:BoundField DataField="RATE" HeaderText="RATE" />
                            <asp:BoundField DataField="QTY" HeaderText="QTY" />
                            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>

             <script type="text/javascript">
                 Sys.Application.add_load(function () {
                    $('.dp1').datetimepicker({
                        format: 'DD-MM-YYYY',
                        defaultDate: 'moment', //auto select current date
                        //showClear: true,
                        showClose: true,
                        showTodayButton: true,
                        //toolbarPlacement: 'bottom',
                        allowInputToggle: true
                

                    });

                    $('.tp1').datetimepicker({
                        format: 'hh A',
                        defaultDate: 'moment', //auto select current date
                        //showClear: true,
                        showClose: true,
                        showTodayButton: true,
                        //toolbarPlacement: 'bottom',
                        allowInputToggle: true
                    });
                });
            </script>

      </form>
    </body>

    <%--Master JS--%>

        <script type="text/jscript" src="../../Content/Scripts/jquery-2.1.3.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/bootstrap.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/fastclick.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/app.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/jquery.sparkline.min.js"></script>

        <script type="text/jscript" src="../../Content/Scripts/jquery-jvectormap-1.2.2.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/jquery.slimscroll.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/Chart.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/demo.js"></script>

        <script type="text/jscript" src="../../Content/Scripts/dataTables.bootstrap.min.js"></script>
        <script type="text/jscript" src="../../Content/Scripts/jquery.dataTables.min.js"></script>
        <script src="../../Content/Scripts/fontawesome-iconpicker.js"></script>
        <script src="../../Content/Scripts/select2.full.min.js"></script>
        <script src="../../Content/Scripts/html2canvas.min.js"></script>
        <script src="../../Content/JS/Common.js"></script>
        
        <%--bellows are added  by enamul--%>
       <script src="../../Content/DateTimePeaker/js/moment.min.js"></script>
       <script src="../../Content/DateTimePeaker/js/bootstrap-datetimepicker.min.js"></script>
       <script src="../../Content/Inputmask4/dist/jquery.inputmask.bundle.js"></script>

     <%-- Master JS End--%>

     <script src="Content/dt/dataTables.buttons.min.js"></script>
    <script src="Content/dt/buttons.flash.min.js"></script>
    <script src="Content/dt/jszip.min.js"></script>
    <script src="Content/dt/pdfmake.min.js"></script>
    <script src="Content/dt/vfs_fonts.js"></script>
    <script src="Content/dt/buttons.html5.min.js"></script>
    <script src="Content/dt/buttons.print.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#dtr').DataTable({
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

</html>

