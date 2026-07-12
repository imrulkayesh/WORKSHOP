<%@ Page Title="PreInspection" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PreInspection.aspx.cs" Inherits="Pages_VISM_PreInspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Vehicle Details
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
                                Vehicle</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxVehicleid" runat="server" placeholder="Enter Vehicle ID" AutoPostBack="true" OnTextChanged="tbxVehicleid_TextChanged" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdnVehicleId" Value="0" runat="server" />
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Vehicle" ControlToValidate="tbxVehicleid">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="tbxVehicleid"
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>
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
                                Manufacture Year</label>
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
                                <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="ddlcategoryid" ValidationGroup="save" ErrorMessage="select service type" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxDriverName" ValidationGroup="save" ErrorMessage="Please enter driver name" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Driver Mobile</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxDriverMobile" runat="server" placeholder="Enter Driver Mobile No" CssClass="form-control"></asp:TextBox>
                                <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="tbxDriverMobile" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>--%>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxDriverMobile" ValidationGroup="save" ErrorMessage="Please enter driver mobile number" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="YourRegularExpressionValidator" runat="server" ValidationGroup="save"
                                    ControlToValidate="tbxDriverMobile"
                                    ErrorMessage="Invalid mobile number" SetFocusOnError="True"
                                    ValidationExpression="^[0][1][5,6,7,8,9]([0-9]{8})$" Display="Dynamic" ForeColor="Red" />
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
                                <%--<asp:TextBox ID="tbxremark" TextMode="MultiLine" runat="server" placeholder="Enter Group" CssClass="form-control"></asp:TextBox>--%>
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
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxCusMobile" ValidationGroup="save" ErrorMessage="Please enter customer mobile number" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
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
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Inspection Status
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rptInspection" runat="server">
                        <HeaderTemplate>
                            <div style="max-height: 300px; overflow: auto;">
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Category</th>
                                            <th>Problem</th>
                                            <th>Status</th>
                                            <th>Remark</th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" Text="All" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CATNAME") %></td>
                                <td><%#Eval("ITEMNAME") %>
                                    <asp:HiddenField ID="hdnInsItemId" runat="server" Value='<%#Eval("ID") %>' />
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("STATUSID") %>' />
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbStatus" SelectedValue='<%#Convert.ToInt32(Eval("PIID"))>0?Eval("STATUSID"):"1" %>' runat="server" Width="100%" RepeatColumns="3">
                                        <asp:ListItem Value="1" Selected="True"> OK</asp:ListItem>
                                        <asp:ListItem Value="2"> Problem</asp:ListItem>
                                        <asp:ListItem Value="3"> N/A</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxInsRemarks" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("PIID").ToString())? "" : Eval("REMARKS") %>'></asp:TextBox></td>
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
                <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                    OnClick="btnEdit_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                    OnClick="btnReset_Click" />
            </div>

            <br />
            <hr />
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">
                    Pre inspection Search</label>
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
                    Pre-Inspection List
                </div>
                <div class="panel-body" style="overflow: auto">
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <table id="example2" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Entry Date</th>
                                        <th>Vehicle</th>
                                        <th>Customer</th>
                                        <th>Mobile</th>
                                        <th>Technician</th>
                                        <th>Remark</th>
                                        <th>Edit</th>
                                        <th>JobCard</th>
                                        <th>Close</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ID") %></td>
                                <td><%#Eval("Entry Date") %></td>
                                <td>
                                    <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' Text='<%#Eval("CHASSISNO")+"/"+Eval("REGNO") %>' ToolTip="View Details" /></td>
                                <td><%#Eval("CUSTOMERNAME") %></td>
                                <td><%#Eval("MOBILE") %></td>
                                <td><%#Eval("TECSTAFFID") %></td>
                                <td><%#Eval("REMARKS") %></td>
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnJobCard" runat="server" OnCommand="btnJobCard_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/assign.png" ToolTip="Job Card" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnClosePI" runat="server" OnCommand="btnClosePI_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/dltgrd.png" ToolTip="Close PI" />
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
        </ContentTemplate>
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

