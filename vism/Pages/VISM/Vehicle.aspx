<%@ Page Title="Vehicle Information" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Vehicle.aspx.cs" Inherits="Pages_VISM_Vehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                <asp:HiddenField ID="hdnID" runat="server" />
            </div>
            <div class="page-header">
                Customer Details
            </div>
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            Customer</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxOwnerName" runat="server" AutoPostBack="true" OnTextChanged="tbxOwnerName_TextChanged" placeholder="Enter Owner Name/Mobile/ID" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdnOwnerId1" Value="0" runat="server" />
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Owner Name" ControlToValidate="tbxOwnerName">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCustomer"
                                MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="tbxOwnerName"
                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                            </ajaxToolkit:AutoCompleteExtender>
                        </div>
                        <label for="inputEmail3" class="col-sm-2">
                            Mobile</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxOwnerMobile" Enabled="false" runat="server" placeholder="Enter Owner Mobile" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            Billing Address</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxBillingAddress1" runat="server" placeholder="Enter Billing Address" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator28" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Billing Address" ControlToValidate="tbxBillingAddress1">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-sm-2">
                            Contact Person</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxContactPerson1" runat="server" placeholder="Enter Contact Person" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator29" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Contact Person" ControlToValidate="tbxContactPerson1">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            Contact Mobile</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxBillingMobile1" runat="server" placeholder="Enter Contact Mobile" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator30" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Contact Mobile" ControlToValidate="tbxBillingMobile1">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-sm-2">
                            Contact Phone</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxContactPhone1" runat="server" placeholder="Enter Contact Phone" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2">
                            Contact Email</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxContactEmail" runat="server" placeholder="Enter Contact Email" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label for="inputEmail3" class="col-sm-2">
                            Contact Fax</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbxContactFax1" runat="server" placeholder="Enter Contact Fax" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="page-header">
                Vehicle Details
            </div>
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label5" runat="server" Text="Chassis#"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxChassisNo" runat="server" placeholder="Enter Chassis No" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Chassis No" ControlToValidate="tbxChassisNo">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label2" runat="server" Text="Reg#"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxRegistrationNo" runat="server" placeholder="Enter Registration No" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label3" runat="server" Text="Engine#"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxEngineNo" runat="server" placeholder="Enter Engine No" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Engine No" ControlToValidate="tbxEngineNo">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label4" runat="server" Text="V.Type"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlVehicleType" Width="100%" DataTextField="VEHICLETYPENAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Vechile Type" ControlToValidate="ddlVehicleType">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label6" runat="server" Text="VIN#"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxVIN" runat="server" placeholder="Enter VIN No" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label7" runat="server" Text="EngineModel"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlEngineModel" Width="100%" DataTextField="ENGINEMODELNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Engine Model" ControlToValidate="ddlEngineModel">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label8" runat="server" Text="Manufacturer"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlManufacturer" Width="100%" DataTextField="MANUFACTURERNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Manufacturer" ControlToValidate="ddlManufacturer">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label9" runat="server" Text="Manu.Year"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxManufacturingYear" runat="server" placeholder="Enter Manufacturing Year" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label10" runat="server" Text="Eng.Capacity"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxEngineCapacity" runat="server" placeholder="Enter Engine Capacity" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Engine Model" ControlToValidate="tbxEngineCapacity">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                     <%--   <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label11" runat="server" Text="Millage"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxMillage" runat="server" placeholder="Enter Millage" CssClass="form-control"></asp:TextBox>

                        </div>--%>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label12" runat="server" Text="Brand"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlBrand" Width="100%" DataTextField="BRANDNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Brand" ControlToValidate="ddlBrand">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label13" runat="server" Text="Model"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlModel" Width="100%" DataTextField="MODELNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Model" ControlToValidate="ddlModel">*</asp:RequiredFieldValidator>
                        </div>
                         <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label14" runat="server" Text="Body"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlBodyType" Width="100%" DataTextField="BODYTYPENAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Body Type" ControlToValidate="ddlBodyType">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label15" runat="server" Text="Fuel"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlFuleType" Width="100%" DataTextField="FUELTYPENAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Fuel Type" ControlToValidate="ddlFuleType">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label16" runat="server" Text="Origin"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlOrigin" Width="100%" DataTextField="COUNTRYNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator12" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Origin" ControlToValidate="ddlOrigin">*</asp:RequiredFieldValidator>
                        </div>
                         <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label17" runat="server" Text="#Cylinder"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxNoofCylinder" runat="server" placeholder="Enter Number of Cylinder" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                       
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label18" runat="server" Text="C.Capacity"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxCylCapacity" runat="server" placeholder="Enter Cylinder Capacity" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label19" runat="server" Text="WheelBase"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxWheelBase" runat="server" placeholder="Enter Wheel Base" CssClass="form-control"></asp:TextBox>
                        </div>
                         <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label20" runat="server" Text="Seats"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxSeats" runat="server" placeholder="Enter Number of Seats" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label21" runat="server" Text="L.Weight"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxLadenWeight" runat="server" placeholder="Enter Laden Weight" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label22" runat="server" Text="U.Weight"></asp:Label></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxUnladenWeight" runat="server" placeholder="Enter Unladen Weight" CssClass="form-control"></asp:TextBox>
                        </div>
                         <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label23" runat="server" Text="#Tyre"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxNoofTyre" runat="server" placeholder="Enter Number of Tyres" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator13" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Number of Tyres" ControlToValidate="tbxNoofTyre">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label24" runat="server" Text="TyreSize"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxTyreSize" runat="server" placeholder="Enter Laden Weight" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator14" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Laden Weight" ControlToValidate="tbxTyreSize">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label25" runat="server" Text="Uses"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlTypeofUse" Width="100%" DataTextField="USETYPENAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator15" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Uses" ControlToValidate="ddlTypeofUse">*</asp:RequiredFieldValidator>
                        </div>
                         <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label26" runat="server" Text="#Axles"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxNoofAxles" runat="server" placeholder="Enter Number of Axles" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator16" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Number of Axles" ControlToValidate="tbxNoofAxles">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label27" runat="server" Text="A.Weight"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxAxleWeights" runat="server" placeholder="Enter Axles Weight" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator17" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Axles Weight" ControlToValidate="tbxAxleWeights">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label28" runat="server" Text="Cost"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-2">
                            <asp:TextBox ID="tbxCost" runat="server" placeholder="Enter CostPrice" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator18" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Cost Price" ControlToValidate="tbxCost">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1">
                            <asp:DropDownList ID="ddlCostCurrency" Width="100%" DataTextField="CURRENCYCODE" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label29" runat="server" Text="HP"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxHorsePower" runat="server" placeholder="Enter Horse Power" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator19" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Horse Power" ControlToValidate="tbxHorsePower">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label30" runat="server" Text="RPM"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxRpm" runat="server" placeholder="Enter RPM" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator20" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter RPM" ControlToValidate="tbxRpm">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label31" runat="server" Text="Price"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-2">
                            <asp:TextBox ID="tbxSellingPrice" runat="server" placeholder="Enter Selling Price" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator21" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Selling Price" ControlToValidate="tbxSellingPrice">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1">
                            <asp:DropDownList ID="ddlSellingCurrency" Width="100%" DataTextField="CURRENCYCODE" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label32" runat="server" Text="Company"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCompany" Width="100%" DataTextField="COMPANYNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator22" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Select Company" ControlToValidate="ddlCompany">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label33" runat="server" Text="Location"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlBusinessCenter" Width="100%" DataTextField="CENTERNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator23" runat="server" ValidationGroup="save"
                                ErrorMessage="Select Location" ControlToValidate="ddlBusinessCenter">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label35" runat="server" Text="ImpDate"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxImportDate" runat="server" placeholder="Enter Import Date" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator24" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Import Date" ControlToValidate="tbxImportDate">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="cal" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxImportDate"></ajaxToolkit:CalendarExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label36" runat="server" Text="Drawing"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:FileUpload ID="flDrawing" runat="server" />
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label37" runat="server" Text="Apv.By"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxApprovedBy" runat="server" placeholder="Enter Drawing Approver Name" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator26" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Drawing Approver Name" ControlToValidate="tbxApprovedBy">*</asp:RequiredFieldValidator>
                        </div>
                        <label for="inputEmail3" class="col-md-1">
                            <asp:Label ID="Label38" runat="server" Text="ApvDate"></asp:Label><span class="required">*</span></label>
                        <div class="col-md-3">
                            <asp:TextBox ID="tbxApproveDate" runat="server" placeholder="Enter Approve Date" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator27" runat="server" ValidationGroup="save"
                                ErrorMessage="Enter Approve Date" ControlToValidate="tbxImportDate">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxApproveDate"></ajaxToolkit:CalendarExtender>
                        </div>
                          <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label1" runat="server" Text="Country"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlCountryId" DataTextField="COUNTRYNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Location" ControlToValidate="ddlCountryId">*</asp:RequiredFieldValidator>
                            </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10 text-right">
                            <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" Visible="false"
                                OnClick="btnEdit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                                OnClick="btnReset_Click" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="box">
                                <div class="box-body">
                                    <asp:Repeater ID="rpt" runat="server">
                                        <HeaderTemplate>
                                            <div style="overflow: auto; height: 400px">
                                            <table id="example1" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Chassis#</th>
                                                        <th>RegNo#</th>
                                                        <th>Model</th>
                                                        <th>Action</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td><%#Eval("ID") %></td>
                                                <td><%#Eval("CHASSISNO") %></td>
                                                <td><%#Eval("REGNO") %></td>
                                                <td><%#Eval("REGNO") %></td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                                    <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                </table></div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnEdit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

