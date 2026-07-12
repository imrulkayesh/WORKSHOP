<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="GateInn.aspx.cs" Inherits="Pages_VISM_GateInn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--   <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="ddlcategoryid" ValidationGroup="save" ErrorMessage="select service type" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>--%>

    <script type="text/javascript">
        function doPostBack(o) {
            __doPostBack(o.id, '');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Vehicle Gate In</div>

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
                                Vehicle Id</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxVehicleid" runat="server" placeholder="Barcode Read" AutoPostBack="true" OnTextChanged="tbxVehicleid_TextChanged" CssClass="form-control" autocomplete="off" TabIndex="1" onkeyup="doPostBack(this);"></asp:TextBox>
                                <asp:HiddenField ID="hdnVehicleId" Value="0" runat="server" />
                                <%--    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Vehicle" ControlToValidate="tbxVehicleid">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="tbxVehicleid"
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>--%>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Customer</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxCustomer" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Chassis no.</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxChassisNo" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Engine no</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxEngineNo" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Model</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxModel" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Reg. Date</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxManufactureYear" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Vehicle Type</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxVehicleType" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Fuel Type</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxFuelType" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Location</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxLocation" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Company</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxCompany" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Country</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxCountry" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>

                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Workshop</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlworkshopid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshopid_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Service Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlcategoryid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID"></asp:DropDownList>
                                <%--   <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="ddlcategoryid" ValidationGroup="save" ErrorMessage="select service type" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Technician</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlTechnician" Width="100%" runat="server" CssClass="form-control select2" DataTextField="TECHNICIAN" DataValueField="STAFFID"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Driver Name</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxDriverName" runat="server" placeholder="Enter Driver Name" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Driver Mobile</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxDriverMobile" runat="server" placeholder="Enter Driver Mobile No" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="tbxDriverMobile" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Millage
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxMillage" runat="server" placeholder="Enter Millage" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb2" runat="server" TargetControlID="tbxMillage" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Group</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" Enabled="True" ID="ddlgroup" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlgroup" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Customer Mobile
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxCusMobile" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCusMobile" ValidationGroup="save" ErrorMessage="Please enter customer mobile number" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="save"
                                    ControlToValidate="tbxCusMobile"
                                    ErrorMessage="Invalid mobile number" SetFocusOnError="True"
                                    ValidationExpression="^[0][1][5,6,7,8,9]([0-9]{8})$" Display="Dynamic" ForeColor="Red" />
                            </div>

                            <label for="inputEmail3" class="col-sm-2">
                                Remarks</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxremark" TextMode="MultiLine" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Effective Date
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxEftDate" runat="server" placeholder="Enter From Date" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxEftDate"></ajaxToolkit:CalendarExtender>
                            </div>

                            <label for="inputEmail3" class="col-sm-2">
                                Deadline
                                </label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbxDateLine1" runat="server" placeholder="Enter Deadline Date" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="tbxDateLine1_CalendarExtender" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxDateLine1"></ajaxToolkit:CalendarExtender>
                                </div>
                        </div>

                        <%--    <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Barcode Read
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxBarcode" runat="server" placeholder="Enter From Date" CssClass="form-control" AutoPostBack="true"
                                    OnTextChanged="tbxBarcode_TextChanged"></asp:TextBox>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="tbxVehicleid" />
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
            var table = $('#example1,#example2').DataTable().destroy();
            table = $('#example1,#example2').DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true
            });
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
            $(".select2").select2();
        }
    </script>
</asp:Content>

