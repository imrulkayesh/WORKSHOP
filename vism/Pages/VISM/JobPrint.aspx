<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobPrint.aspx.cs" Inherits="Pages_VISM_JobPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <button class="btn btn-primary" onclick="javascript:PrintElem('#mydiv');">Print</button>
    <div id="mydiv">
        <div style="width: 100%">
            <div style="float: left; width: 23%;">
                <img src="../../Content/Images/Common/white.jpg" height="100" width="120" />
            </div>
            <div style="float: left; width: 50%;">
                <center>
                <asp:Label ID="lblworkshop" Font-Size="Medium" Font-Bold="true" EnableViewState="false" runat="server"></asp:Label><br />
                    <b>RFL AUTOS (Service Division)</b><br />
            <asp:Label runat="server" Font-Size="Small" ID="lblBranchAddress"></asp:Label><br /><br />
                <b>JOBCARD</b><br />
        </center>
            </div>
            <div style="float: right; width: 23%; text-align: right">
                <img src="../../Content/Images/Common/white.jpg" height="100" width="120" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="box">
                <div class="box-body">
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
                            <th style="font-size: 13px;">Driver Staff ID</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblStaffId"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Company</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblcompany"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Driver Mobile</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblDriverMobile"></asp:Label>
                            </td>
                            <th style="font-size: 13px;">Group</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblgroup"></asp:Label></td>
                        </tr>
                        <tr>
                            <th style="font-size: 13px;">Service Type</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblLpg"></asp:Label></td>
                            <th style="font-size: 13px;">Remarks</th>
                            <td style="font-size: 13px;">
                                <asp:Label runat="server" ID="lblRemark"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="box">
                <div class="box-header" style="font-size: 14px;">
                    Services
                </div>
                <div class="box-body">
                    <table class="table table-responsive table-bordered">
                        <thead>
                            <tr>
                                <th style="font-size: 13px;">SL
                                </th>
                                <th style="font-size: 13px;">Category
                                </th>
                                <th style="font-size: 13px;">Problem
                                </th>

                                <th style="font-size: 13px;">Quantity
                                </th>
                                <th style="font-size: 13px;">Remarks
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptCharge">
                                <ItemTemplate>
                                    <tr>
                                        <td style="font-size: 13px;">
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("CATNAME") %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("ITEMNAME") %>
                                        </td>
                                        <td style="font-size: 13px;">
                                            <%#Eval("QUANTITY") %> 
                                        </td>

                                        <td style="font-size: 13px;">
                                            <%#Eval("REMARKS") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="5" style="border-top: none; font-size: 13px; border-bottom: none; padding-top: 30px; padding-bottom: 15px">I/We hereby authorize the above repair to be done along with necessary materials
                        in a manner as you see fit. You will not be held responsible for any loss caused
                        beyond your control. You are also allowed to drive the vehicle to road testing.
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="border-top: 0px; font-size: 13px; border-right: 0px; padding-top: 20px;">Prepared By<br />
                                    <asp:Label ID="lblPreparedBy" runat="server"></asp:Label>
                                </td>
                                <td colspan="2" style="border-top: 0px; border-left: 0px; font-size: 13px; padding-top: 20px; text-align: right;">Floor Incharge................................
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="border-top: 0px; font-size: 13px; border-right: 0px; padding-top: 20px;">Signature With Date...........................
                                </td>
                                <td colspan="2" style="border-top: 0px; border-left: 0px; font-size: 13px; padding-top: 20px; text-align: right;"></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
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

