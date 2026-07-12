<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobDurationTrackingDetailsReport.aspx.cs" Inherits="Pages_VISM_JobDurationTrackingDetailsReport" %>

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

            <label for="inputEmail3" class="col-sm-2">
                Vehicle 
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxVehicle" runat="server" placeholder="Enter Vehicle" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="tbxVehicle"
                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                </ajaxToolkit:AutoCompleteExtender>
            </div>
        </div>

        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="col-sm-6 text-right">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
            </div>
        <%--    <div class="col-sm-6" style="text-align: right; padding-right: 100px;">
                <asp:Button ID="btnExport" Visible="False" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-success" OnClick="btnExport_OnClick" Style="text-align: right" />
            </div>--%>
        </div>
    </div>
    <div class="col-sm-12">
        <div style="overflow: auto; max-height: 500px">
            <div class="box">
                <div class="box-body">
                    <asp:Panel ID="pnlExport" runat="server">
                        <center>
                <b style="font-size:24px;">Job Duration Tracking Report</b><br />
                            <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> 
                            
                             <br />
                        <hr/>
                         </center>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>

                                <table id="dtt" class="table table-bordered table-hover" border="1px">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Workshop</th>
                                            <th>Reg. No</th>

                                            <th>Work Order No</th>
                                            <th>Work Order Date</th>
                                            <th>WO. Creator</th>

                                            <th>Gate In</th>
                                            <th>Diff. WO To Gate In</th>

                                            <th>Preinspection Id</th>
                                            <th>Preinspection Date</th>
                                            <th>Diff. Gate In To PI.</th>

                                            <th>Job Id</th>
                                            <th>Job Date</th>
                                            <th>Diff. PI. To Job</th>

                                            <th>Job Close Date</th>
                                            <th>Diff. Job To Close</th>

                                            <th>Gate Out</th>
                                            <th>Diff. Close To Gate Out</th>

                                            <th>Working Hours</th>
                                            <th>Actual Working Hours</th>
                                            <th>Delay Time</th>

                                            <th>Service Type</th>
                                            <th>Technician Group</th>
                                            <th>Customer Name</th>
                                            <th>Company Name</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex+1 %></td>
                                    <td><%#Eval("WORKSHOP") %></td>
                                    <td><%#Eval("REGNO") %></td>

                                    <td><%#Eval("WORKORDERNO") %></td>
                                    <td><%# String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Eval("WORKORDERDATE")) %></td>
                                    <td><%#Eval("WORK_ORDER_CREATOR") %></td>

                                    <td><%# String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Eval("GATEIN")) %></td>
                                    <td><%#Eval("DIFF_WORKORDER_TO_GATEIN") %></td>

                                    
                                    <td><%#Eval("PREINSPECTION_ID") %></td>
                                    <td><%# String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Eval("PREINSPECTIONDATE")) %></td>
                                    <td><%#Eval("DIFF_GATEIN_TO_PREINSPECTION") %></td>

                                    <td><%#Eval("JOB_ID") %></td>
                                    <td><%# String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Eval("JOBDATE")) %></td>
                                    <td><%#Eval("DIFF_PREINSPECTION_TO_JOBDATE") %></td>

                                    <td><%# String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Eval("JOBCLOSEDATE")) %></td>
                                    <td><%#Eval("DIFF_JOB_TO_CLOSE") %></td>

                                    <td><%# String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Eval("GATEOUT")) %></td>
                                    <td><%#Eval("DIFF_CLOSE_TO_GATEOUT") %></td>

                                    <td><%#Eval("WORKINGHOURS") %></td>
                                    <td><%#Eval("ACTUALWORKINGHOURS") %></td>  
                                    <td><%#Eval("DELAYTIME") %></td>  

                                    <td><%#Eval("SERVICETYPE") %></td>
                                    <td><%#Eval("TECHNICIAN_GROUP") %></td>  
                                    <td><%#Eval("CUSTOMERNAME") %></td>  
                                    <td><%#Eval("COMPANYNAME") %></td>  

                                    <%--<td><%#Eval("JOBDURATION") %></td>--%>  
                                    
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
    </div>
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
            var table = $('#dtt').DataTable({
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


    <script type="text/javascript">
        $(document).ready(function () {
            //PrintElem('#mydiv');
        });
        var table = $("table");
        var rows = table.find($("tr"));
        var colsLength = $(rows[0]).find($("td")).length;
        var removeLater = new Array();
        for (var i = 0; i < colsLength; i++) {
            var startIndex = 0;
            var lastIndex = 0;
            var startText = $($(rows[0]).find("td")[i]).text();
            for (var j = 1; j < rows.length; j++) {
                var cRow = $(rows[j]);
                var cCol = $(cRow.find("td")[i]);
                var currentText = cCol.text();
                if (currentText == startText) {
                    cCol.css("background", "gray");
                    console.log(cCol);
                    removeLater.push(cCol);
                    lastIndex = j;
                } else {
                    var spanLength = lastIndex - startIndex;
                    if (spanLength >= 1) {
                        console.log(lastIndex + " - " + startIndex)
                        //console.log($($(rows[startIndex]).find("td")[i]))
                        $($(rows[startIndex]).find("td")[i]).attr("rowspan", spanLength + 1);
                    }
                    lastIndex = j;
                    startIndex = j;
                    startText = currentText;
                }

            }
            var spanLength = lastIndex - startIndex;
            if (spanLength >= 1) {
                console.log(lastIndex + " - " + startIndex)
                //console.log($($(rows[startIndex]).find("td")[i]))
                $($(rows[startIndex]).find("td")[i]).attr("rowspan", spanLength + 1);
            }
            console.log("---");
        }

        for (var i in removeLater) {
            $(removeLater[i]).remove();
        }
    </script>
</asp:Content>
