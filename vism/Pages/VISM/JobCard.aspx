<%@ Page Title="Job Card" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobCard.aspx.cs" Inherits="Pages_VISM_JobCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Job Card Details
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-10 col-sm-10 col-xs-10">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Pre-Inspection Id</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxPIID" Enabled="false" runat="server" placeholder="Enter Pre Inspection ID" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Workshop</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlworkshopid" Enabled="false" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshopid_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Vehicle</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxVehicle" runat="server" Enabled="false" placeholder="Enter Vehicle" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Service Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlServiceType" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Effective Date</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxEffectiveDate" runat="server" placeholder="Enter Effective Date" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caleff" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxEffectiveDate"></ajaxToolkit:CalendarExtender>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Group</label>
                            <div class="col-sm-4">
                                <%--    <asp:TextBox ID="tbxremark" TextMode="MultiLine" runat="server" placeholder="Enter Group" CssClass="form-control"></asp:TextBox>--%>
                                <%--    <asp:DropDownList ID="ddlgroup" Width="100%" runat="server" CssClass="form-control select2" DataValueField="ID"></asp:DropDownList>
                                  <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlgroup" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                                <asp:DropDownList runat="server" Enabled="True" ID="ddlgroup" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlgroup" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Remarks</label>
                            <div class="col-sm-4">
                                   <asp:TextBox ID="tbxremark" TextMode="MultiLine" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Service Charges
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rptCharges" runat="server">
                        <HeaderTemplate>
                            <div style="max-height: 200px; overflow-y: auto">
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Service</th>
                                            <th>Quantity</th>
                                            <th>Amount</th>
                                            <th>Remark</th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" Text="All" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CHARGENAME") %>
                                    <asp:HiddenField ID="hdnChargeId" runat="server" Value='<%#Eval("ID") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxChargeQty" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("JOBID").ToString())? "1" : Eval("QUANTITY") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxChargeAmount" CssClass="form-control" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxChargeRemarks" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("JOBID").ToString())? "" : Eval("REMARKS") %>'></asp:TextBox>
                                </td>

                                <td>
                                    <asp:CheckBox ID="chkrow" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table></div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Parts List
                </div>
                <div class="panle-body">
                    <asp:Repeater ID="rptParts" runat="server">
                        <HeaderTemplate>
                            <div style="max-height: 200px; overflow-y: auto">
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Item</th>
                                            <th>Quantity</th>
                                            <th>Amount</th>
                                            <th>Remark</th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" Text="All" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("PARTSNAME") %>
                                    <asp:HiddenField ID="hdnPartsId" runat="server" Value='<%#Eval("ID") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxPartsQty" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("JOBID").ToString())? "1" : Eval("QUANTITY") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxPartsAmount" CssClass="form-control" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxPartsRemarks" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("JOBID").ToString())? "" : Eval("REMARKS") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkrow" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table></div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Technician
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rptTechs" runat="server">
                        <HeaderTemplate>
                            <div style="max-height: 200px; overflow-y: auto">
                                <table id="example4" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>StaffID</th>
                                            <th>Name</th>
                                            <th>Remark</th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" Text="All" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("STAFFID") %>
                                    <asp:HiddenField ID="hdnStaffID" runat="server" Value='<%#Eval("STAFFID") %>' />
                                </td>
                                <td>
                                    <%#Eval("TECHNAME") %>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxTecRemarks" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("JOBID").ToString())? "" : Eval("REMARKS") %>'></asp:TextBox>
                                </td>

                                <td>
                                    <asp:CheckBox ID="chkrow" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table></div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                    OnClick="btnReset_Click" />
            </div>
            <br />
            <hr />
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">
                    Jobcard Search</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="tbxVehileForJobIns" runat="server" placeholder="Enter Vehicle ID" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                        TargetControlID="tbxVehileForJobIns"
                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                    </ajaxToolkit:AutoCompleteExtender>
                </div>
                <asp:Button ID="btnPreInsSearch" runat="server" ClientIDMode="Static" Text="Search" CssClass="btn btn-success"
                    OnClick="btnPreInsSearch_Click" />

            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Jobcard List
                </div>
                <div class="panel-body" style="overflow: auto">
                    <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_OnItemDataBound">
                        <HeaderTemplate>
                            <table id="example3" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                                <thead>
                                    <tr>

                                        <th>SL#
                                        </th>
                                        <asp:Repeater runat="server" ID="hdrRpt">
                                            <ItemTemplate>
                                                <th>
                                                    <%# Container.DataItem %>
                                                </th>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <th>Edit</th>
                                        <th>PrintJob</th>
                                        <th>PrintBill</th>
                                        <th>Job Close</th>
                                        <th>No Work</th>
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
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnPrint" runat="server" OnCommand="btnPrint_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/printer_icon.png" ToolTip="Print" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnBillPrint" runat="server" OnCommand="btnBillPrint_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnClose" runat="server" OnCommand="btnClose_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/dltgrd.png" ToolTip="Close" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnNoWork" Visible="False" runat="server" OnCommand="btnNoWork_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/dustbin.png" ToolTip="No Work" />
                                </td>
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
        
            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <%--<div class="panel panel-primary">
                <div class="panel-heading">
                    Jobcard List
                </div>
                <div class="panel-body">
                    <div style="overflow: auto;">
                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_OnItemDataBound">
                            <HeaderTemplate>
                                <div style="max-height: 350px; overflow-y: auto">
                                <table id="example3" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>JobNo</th>
                                            <th>Customer</th>
                                            <th>Mobile</th>
                                            <th>Vehicle</th>
                                            <th>Service Type</th>
                                            <th>Eff. Date</th>
                                            <th>Remark</th>
                                            <th>Update</th>
                                            <th>PrintJob</th>
                                            <th>PrintBill</th>
                                            <th>Job Close</th>
                                            <th>No Work</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("ID") %></td>
                                    <td>
                                        <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' Text='<%#Eval("CUSTOMERNAME") %>' ToolTip="View Details" /></td>
                                    <td><%#Eval("MOBILE") %></td>
                                    <td><%#Eval("CHASSISNO") %>/<%#Eval("REGNO") %></td>
                                    <td><%#Eval("SERVICETYPE") %></td>
                                    <td><%#Eval("EFFECTIVEDATE","{0:dd/MM/yyyy}") %></td>
                                    <td><%#Eval("REMARKS") %></td>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPrint" runat="server" OnCommand="btnPrint_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/printer_icon.png" ToolTip="Print" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnBillPrint" runat="server" OnCommand="btnBillPrint_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnClose" runat="server" OnCommand="btnClose_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/dltgrd.png" ToolTip="Close" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnNoWork" Visible="False" runat="server" OnCommand="btnNoWork_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/dustbin.png" ToolTip="No Work" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table></div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rptMain" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script>
        $(document).ready(function () {
            load();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                load();
            }
        });
        function load() {
            $("#example1 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example1 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example1 [id*=chkrow]").click(function () {
                if ($("#example1 [id*=chkrow]").length == $("#example1 [id*=chkrow]:checked").length) {
                    $("#example1 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkHeader]").prop("checked", false);
                }
            });
            $("#example2 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example2 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example2 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example2 [id*=chkrow]").click(function () {
                if ($("#example2 [id*=chkrow]").length == $("#example2 [id*=chkrow]:checked").length) {
                    $("#example2 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example2 [id*=chkHeader]").prop("checked", false);
                }
            });
            $("#example4 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example4 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example4 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example4 [id*=chkrow]").click(function () {
                if ($("#example4 [id*=chkrow]").length == $("#example4 [id*=chkrow]:checked").length) {
                    $("#example4 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example4 [id*=chkHeader]").prop("checked", false);
                }
            });
            var table = $('#example1,#example2,#example3,#example4').DataTable().destroy();

            table = $('#example1,#example2,#example3,#example4').DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true
            });
            $(".select2").select2();
        }
    </script>
</asp:Content>

