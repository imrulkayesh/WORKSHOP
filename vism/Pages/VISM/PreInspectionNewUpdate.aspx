<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PreInspectionNewUpdate.aspx.cs" Inherits="Pages_VISM_PreInspectionNewUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tbxVehicleid").on("keypress", function (e) {
                if (e.keyCode == 13) {
                    alert("asdasd");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-9">

            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Vehicle Information</div>

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
                                <asp:TextBox ID="tbxVehicleid" Enabled="False" runat="server" placeholder="Enter Vehicle ID" AutoPostBack="true" OnTextChanged="tbxVehicleid_TextChanged" CssClass="form-control"></asp:TextBox>
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
                                Driver Staff Id</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxStaffId" runat="server" placeholder="Enter Driver Staff ID" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-group">

                            <label for="inputEmail3" class="col-sm-2">
                                Driver Mobile</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxDriverMobile" runat="server" placeholder="Enter Driver Mobile No" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="tbxDriverMobile" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                            </div>

                            <label for="inputEmail3" class="col-sm-2">
                                Millage
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxMillage" runat="server" placeholder="Enter Millage" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb2" runat="server" TargetControlID="tbxMillage" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
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
                                    ValidationExpression="^[0][1][3,4,5,6,7,8,9]([0-9]{8})$" Display="Dynamic" ForeColor="Red" />
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
                                Effective Date
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxEftDate" runat="server" placeholder="Enter Effective Date" CssClass="form-control"></asp:TextBox>
                                <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxEftDate"></ajaxToolkit:CalendarExtender>--%>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxEftDate"></ajaxToolkit:CalendarExtender>
                            </div>

                            <label for="inputEmail3" class="col-sm-2">
                                Remarks</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxremark" TextMode="MultiLine" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Deadline
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxDateLine" runat="server" placeholder="Enter Deadline" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxDateLine"></ajaxToolkit:CalendarExtender>
                            </div>
                            <label class="col-sm-2">CNG Cylinder Re-Test Date</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxCCRD" ReadOnly ="true" runat="server" placeholder="Enter Re-Test Date" CssClass="form-control"></asp:TextBox>
                                <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender2" Enabled="false" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxCCRD"></ajaxToolkit:CalendarExtender>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Casualties</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtCAS" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <%--       <div class="form-group">
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
                </div>

                <div class="col-lg-3"> 
                  <div class="panel panel-info">
                    <div class="panel-heading">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Work Order Information</div>
                    </div>
                    <div class="panel-body">
                       <div class="form-horizontal">
                        <%--<div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                Vehicle</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxVehicledata" runat="server" placeholder="Enter Vehicle ID" AutoPostBack="true" OnTextChanged="tbxVehicleid_TextChanged" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Vehicle" ControlToValidate="tbxVehicledata">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicleworkOrderdata"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="tbxVehicledata"
                                    ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                        </div>--%>
                         <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                Workshop</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlWorkWorkshopId" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                Work Order No</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlWorkOrderNo" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKORDERNO" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkOrderNo_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                Vehicle </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxVehiid" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4"> 
                                    Work Order Date</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbxWorkOrderDate" Enabled="False" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                                </div>
                        </div>
                        <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4"> Driver Staff Id</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbxStafId" runat="server" Enabled="False" placeholder="Driver Staff ID" CssClass="form-control"></asp:TextBox>
                                </div>
                        </div>
                        <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4"> Running KM </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbxRunningMillage" runat="server" Enabled="False" placeholder="Running KM" CssClass="form-control"></asp:TextBox>
                                </div>
                        </div>
                       </div>

                     </div>
                   </div>
                   <div class="panel panel-info">
                       <div class="panel-heading">
                            <div class="panel-title" style="text-align: center; margin-top: -7px">Details Information</div>
                       </div>
                       <div class="panel-body">
                            <asp:Repeater ID="rptWorkOrder" runat="server">
                                <HeaderTemplate>
                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>SL#</th>
                                                <th>Item Name</th>
                                                 <th>Reason</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Container.ItemIndex+1 %></td>
                                        <td><%#Eval("ITEMNAME")%></td>
                                        <td><%#Eval("REASON")%></td>
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

            </div>
            <hr />
            <div class="panel panel-info">
                <div class="panel-heading" style="height: 35px">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Pre-Inspection Problem List</div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-3">
                            <label>
                                Category
                            </label>
                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCategory" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator6" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-sm-3">
                            <label>
                                Problem
                            </label>
                            <asp:DropDownList runat="server" Enabled="False" ID="ddlProblemName" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlProblemName_OnSelectedIndexChanged" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProblemName" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator8" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                        </div>

                        <div class="col-sm-1">
                            <label>
                                Quantity
                            </label>
                            <asp:TextBox ID="tbxQty" runat="server" Enabled="True" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="tbxLaborRate" Value="0" runat="server" />
                            <asp:HiddenField ID="tbxLaborQty" Value="0" runat="server" />
                            <asp:HiddenField ID="tbxWorkingHour" Value="0" runat="server" />
                        </div>

                        <div class="col-sm-1">
                            <label>
                                Amount
                            </label>
                            <asp:TextBox ID="tbxAmount" runat="server" Enabled="True" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-sm-3">
                            <label>
                                Remarks
                            </label>
                            <asp:TextBox ID="tbxProbRemarks" runat="server" Enabled="True" CssClass="form-control"></asp:TextBox>
                            <%--                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxAmt" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator12"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator runat="server" ControlToValidate="tbxAmt" ValidationGroup="add" ValidationExpression="^(\d*\.)?\d+$" ID="RegularExpressionValidator1"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="Invalid." ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            --%>
                        </div>
                        <div class="col-sm-1">
                            <label>
                                Action
                            </label>
                            <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="btn btn-primary" ValidationGroup="add" OnClick="btnAdd_OnClick"></asp:Button>
                            <asp:Button ID="btnUpdate" Visible="False" Text="Update" runat="server" CssClass="btn btn-warning" ValidationGroup="add" OnClick="btnUpdate_OnClick"></asp:Button>

                        </div>

                    </div>
                </div>
                <div style="max-height: 500px; overflow: auto;">
                    <asp:Repeater ID="rptDetails" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>SL#</th>
                                        <th style="display: none">Category Code</th>
                                        <th>Category Name</th>
                                        <th style="display: none">Problem Code</th>
                                        <th>Problem Name</th>
                                        <th>Quantity</th>
                                        <th>Labor Rate</th>
                                        <th>Labor Qty</th>
                                        <th>Working Hour</th>
                                        <th>Amount</th>
                                        <th>Remarks</th>
                                        <th>Edit</th>
                                        <th>Delete</th>

                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Container.ItemIndex+1 %></td>
                                <td style="display: none"><%#Eval("CategoryCode")%></td>
                                <td><%#Eval("CategoryName")%></td>
                                <td style="display: none"><%#Eval("ProblemCode")%></td>
                                <td><%#Eval("ProblemName")%></td>
                                <td><%#Eval("Qty")%></td>
                                <td><%#Eval("LaborRate")%></td>
                                <td><%#Eval("LABORQTY")%></td>
                                <td><%#Eval("WorkingHour")%></td>
                                <td><%#Eval("TotAmt")%></td>
                                <td><%#Eval("ProblemRemarks")%></td>
                                <td>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandArgument='<%# Eval("ProblemCode")%>' OnCommand="imgbtnEdit_OnCommand" ImageUrl="../../Content/Images/edit1.png" ToolTip="Delete" CausesValidation="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("ProblemCode")%>' OnCommand="imgbtnDelete_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure?')" />

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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Pre-Inspection List</div>

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
                                        <th>PrintJob</th>
                                        <th>JobCard</th>
                                        <th style="display:none;">Close</th>
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
                                     <asp:ImageButton ID="btnPrintPJob" runat="server" OnCommand="btnPrintPJob_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/printer_icon.png" ToolTip="Print" CausesValidation="False" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnJobCard" runat="server" OnCommand="btnJobCard_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/assign.png" ToolTip="Job Card" />
                                </td>
                                <td style="display:none;">
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
    <script>
        $(document).ready(function () {
            load();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                load();
            }
        });
        function load() {
            var table = $('#example2,#example2').DataTable().destroy();

            table = $('#example2,#example2').DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true
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
            $(".select2").select2();
        }
    </script>
</asp:Content>

