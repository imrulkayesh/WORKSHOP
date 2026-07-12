<%@ Page Title="Job Information" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobInformation.aspx.cs" Inherits="Pages_VISM_JobInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
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

            <label for="inputEmail3" class="col-sm-2">
                WorkShop
            </label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlWorkShop" Width="100%" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Service Category
            </label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" DataValueField="ID" DataTextField="TEXT" />
            </div>

            <label for="inputEmail3" class="col-sm-2">
                Company
            </label>
            <div class="col-sm-4">
                 <asp:TextBox ID="tbxCom" runat="server" placeholder="Enter Company Name" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCom"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="tbxCom"
                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                </ajaxToolkit:AutoCompleteExtender>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputEmail3" class="col-sm-2">
                Reg. No 
            </label>
            <div class="col-sm-4">
                <asp:Label runat="server" ID="lblRegNo" CssClass="form-control"></asp:Label>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                Chiss. No
            </label>
            <div class="col-sm-4">
                <asp:Label runat="server" ID="lblChissNo" CssClass="form-control"></asp:Label>
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
                <b style="font-size:24px;">Job Information</b><br />
                            <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> 
                            
                             <br />
                            <%--<div class="form-horizontal">
                            <div class="form-group">
                            <label class="col-sm-3"  id="lbl1">  Reg. No  :</label>
                            <asp:Label runat="server" ID="lblRegNor" CssClass="col-sm-3"></asp:Label> 
                            <label class="col-sm-3" id="lbl2">Chiss. No :</label>
                            <asp:Label runat="server" ID="lblChissNor" CssClass="col-sm-3"></asp:Label>
                            </div>
                                 <div class="form-group">
                                    <label class="col-sm-3" id="lbl2">Model:</label>
                            <asp:Label runat="server" ID="lblModel" CssClass="col-sm-3"></asp:Label> 
                                 </div>
                            </div>--%>
                        <hr/>
                         </center>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>

                                <table id="dtt" class="table table-bordered table-hover" border="1px">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Job ID</th>
                                            <th>Receive Date</th>
                                            <th>Delivery Date</th>
                                            <th>Reg. No</th>
                                            <th>Model</th>
                                            <th>Millage</th>
                                            <th>Service Type</th>
                                            <th>Service Catagory</th>
                                            <th>Problem</th>

                                            <th>Unit</th>
                                            <th>Rate</th>
                                            <th>Labor Qty</th>
                                            <th>Working Hour</th>
                                            <th>Amount</th>

                                            <th>Remarks</th>
                                            <th>Customer</th>
                                            <th>Company</th>
                                            <th>CreatedBy</th>
                                            <th>CloseBy</th>
                                            <th>Workshop</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex+1 %></td>
                                    <td><%#Eval("JOBID") %></td>
                                  <%--  <td><%#Eval("CREATEDATE") %></td>
                                    <td><%#Eval("UPDATEDATE") %></td>--%>
                                    <td><%#Eval("CREATEDATE","{0:dd/MM/yyyy}") %></td>
                                    <td><%#Eval("UPDATEDATE","{0:dd/MM/yyyy}") %></td>
                                    <td><%#Eval("REGNO") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("MILLAGE") %></td>
                                    <td><%#Eval("SERVICETYPE") %></td>
                                    <td><%#Eval("CATNAME") %></td>
                                    <td><%#Eval("ITEMNAME") %></td>

                                    <td><%#Eval("QUANTITY") %></td>
                                    <td><%#Eval("LABORRATE") %></td>
                                    <td><%#Eval("LABORQTY") %></td>
                                    <td><%#Eval("WORKINGHOUR") %></td>
                                    <td><%#Eval("AMOUNT") %></td>

                                    <td><%#Eval("REMARKS") %></td>
                                    <td><%#Eval("CUSTOMERNAME") %></td>
                                    <td><%#Eval("COMPANYNAME") %></td>
                                    <td><%#Eval("CREATEDBY") %></td>
                                    <td><%#Eval("UPDATEDBY") %></td>
                                    <td><%#Eval("WORKSHOP") %></td>
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

