<%@ Page Title="Single Job Details" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SingleJobDetails.aspx.cs" Inherits="Pages_VISM_SingleJobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Jobcard No 
            </label>
            <div class="col-sm-12">
                <asp:TextBox ID="tbxJobId" runat="server" placeholder="Enter JobID/RegNO/ChassisNO/Customer Name" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxVehicle_OnTextChanged"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="tbxJobId"
                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                </ajaxToolkit:AutoCompleteExtender>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
                <asp:Button ID="btnExport" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_OnClick" />
            </div>
        </div>
    </div>
    <div class="col-sm-12">
        <div class="box">
            <div class="box-body">
                <asp:Panel ID="pnlExport" runat="server">
                    <center>
                <b style="font-size:24px;">Single Job Details</b><br />
                            <asp:Label ID="lblworkshop" Font-Size="Medium" Font-Bold="true" EnableViewState="false" runat="server"></asp:Label><br />
            <asp:Label runat="server" Font-Size="Small" ID="lblBranchAddress"></asp:Label><br /><br />
                        <div class="form-horizontal">
                       <table class="table table-responsive table-bordered">
                        <tr>
                            <th style="font-size: 13px;">Open Date</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Millage</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblMeterReading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Job Card No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblJobNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Chassis No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblChassisNo"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Reg No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblRegNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Engine No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblEngineNo"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Model No</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblEngineModuleNo"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Customer</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblCustName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Driver Name</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDriverName"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Mobile</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblMobile"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Driver Mobile</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDriverMobile"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Address</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblOfficeAddress"></asp:Label></td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Service Type</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblLpg"></asp:Label></td>

                            <th style="font-size: 13px;">Company</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblcompany"></asp:Label>
                            </td>
                        </tr>
                           <tr>
                                 <th style="font-size: 13px;">Close Date</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblUpdateDate"></asp:Label>
                            </td>

                           </tr>
                    </table>
                    </div>
                        <hr/>
                         </center>
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <div style="overflow: auto">
                                <table id="dtt" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL</th>
                                            <th>Category</th>
                                            <th>Problem</th>
                                            <th>Quantity</th>
                                            <th>Remarks</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex+1 %></td>
                                <td><%#Eval("CATNAME") %></td>
                                <td><%#Eval("ITEMNAME") %></td>
                                <td>1</td>
                                <td><%#Eval("REMARKS") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table>
                        </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //PrintElem('#mydiv');
        });
        function PrintElem(elem) {
            Popup($(elem).html());
        }
        function Popup(data) {
            var mywindow = window.open('', 'my div', 'height=400,width=600');
            mywindow.document.write('<html><head><title>Print JobCard</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="../../RaffelDraw/Content/bootstrap.min.css" type="text/css" />');
            mywindow.document.write('<style>.text-left {text-align: left;} .text-right {text-align: right;} .text-center {text-align: center;}.text-justify {text-align: justify;}.text-nowrap {white-space: nowrap;}.text-lowercase {text-transform: lowercase;}.text-uppercase {text-transform: uppercase;}.text-capitalize {text-transform: capitalize;}.img-thumbnail {display: inline-block;max-width: 100%;height: auto;padding: 4px;line-height: 1.02857143; font-size:13px; background-color: #fff;border: 1px solid #ddd;border-radius: 4px;-webkit-transition: all .2s ease-in-out;-o-transition: all .2s ease-in-out;transition: all .2s ease-in-out;}.panel-title {margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;}.table {width: 100%;max-width: 100%;margin-bottom: 20px;} .table {border-collapse: collapse !important;}.table td, .table th {background-color: #fff !important;}.table-bordered th, .table-bordered td {border: 1px solid #ddd !important;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {padding: 4px;line-height: 1.02857143; font-size:13px; vertical-align: top;border-top: 1px solid #ddd; text-align:left }.table > thead > tr > th {vertical-align: bottom;border-bottom: 2px solid #ddd;}.table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {border-top: 0;}.table > tbody + tbody {border-top: 2px solid #ddd;}.table .table {background-color: #fff;}.table-bordered {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > thead > tr > td {border-bottom-width: 2px;}</style></head><body >');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10

            mywindow.print();
            mywindow.close();
            window.onfocus = function () { window.close(); };
            return true;
        }

        console.clear();
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

