<%@ Page Title="Sales" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="Pages_VISM_Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-10 col-sm-10 col-xs-10">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                    <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                </div>
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Vehicle Details</div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label5" runat="server" Text="Chassis#"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxChassisNo" runat="server" AutoPostBack="true" OnTextChanged="tbxChassisNo_TextChanged" placeholder="Enter Chassis Number" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField ID="hdnVehicleId" runat="server" />
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Chassis Number" ControlToValidate="tbxChassisNo">*</asp:RequiredFieldValidator>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxChassisNo"
                                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label9" runat="server" Text="Model"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxModel" Enabled="false" runat="server" placeholder="Enter Model" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label10" runat="server" Text="Brand"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxBrand" Enabled="false" runat="server" placeholder="Enter Brand" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label11" runat="server" Text="Manufacturer"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxManufacturer" Enabled="false" runat="server" placeholder="Enter Manufacturer" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Owner Details</div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label1" runat="server" Text="Owner Name"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxOwnerName" runat="server" AutoPostBack="true" OnTextChanged="tbxOwnerName_TextChanged" placeholder="Enter Owner Name/Mobile/ID" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField ID="hdnOwnerId1" runat="server" />
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Owner Name" ControlToValidate="tbxOwnerName">*</asp:RequiredFieldValidator>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCustomer"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxOwnerName"
                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label2" runat="server" Text="Mobile"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxOwnerMobile" Enabled="false" runat="server" placeholder="Enter Owner Mobile" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label14" runat="server" Text="Billing Address"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxBillingAddress1" runat="server" placeholder="Enter Billing Address" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Billing Address" ControlToValidate="tbxBillingAddress1">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label15" runat="server" Text="Contact Person"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxContactPerson1" runat="server" placeholder="Enter Contact Person" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Contact Person" ControlToValidate="tbxContactPerson1">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label16" runat="server" Text="Contact Mobile"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxBillingMobile1" runat="server" placeholder="Enter Contact Mobile" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Contact Mobile" ControlToValidate="tbxBillingMobile1">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label17" runat="server" Text="Contact Phone"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxContactPhone1" runat="server" placeholder="Enter Contact Phone" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label18" runat="server" Text="Contact Email"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxContactEmail" runat="server" placeholder="Enter Contact Email" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label19" runat="server" Text="Contact Fax"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxContactFax1" runat="server" placeholder="Enter Contact Fax" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label4" runat="server" Text="Joint Owner"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkOwnerType" AutoPostBack="true" OnCheckedChanged="chkOwnerType_CheckedChanged" CssClass="checkbox checkbox-inline" runat="server" Text="Yes" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlJointOwner" Visible="false" runat="server">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label6" runat="server" Text="Owner Name"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxOwner2Name" runat="server" AutoPostBack="true" OnTextChanged="tbxOwner2Name_TextChanged" placeholder="Enter Owner Name/Mobile/ID" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnOwnerId2" runat="server" />
                                            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCustomer"
                                                MinimumPrefixLength="1"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                TargetControlID="tbxOwner2Name"
                                                ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label7" runat="server" Text="Mobile"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxOwnerMobile2" Enabled="false" runat="server" placeholder="Enter Owner Mobile" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label20" runat="server" Text="Billing Address"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxBillingAddress2" runat="server" placeholder="Enter Billing Address" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label21" runat="server" Text="Contact Person"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxContactPerson2" runat="server" placeholder="Enter Contact Person" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label22" runat="server" Text="Contact Mobile"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxBillingMobile2" runat="server" placeholder="Enter Contact Mobile" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label23" runat="server" Text="Contact Phone"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxContactPhone2" runat="server" placeholder="Enter Contact Phone" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label24" runat="server" Text="Contact Email"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxContacEmail2" runat="server" placeholder="Enter Contact Email" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label for="inputEmail3" class="col-sm-2">
                                            <asp:Label ID="Label25" runat="server" Text="Contact Fax"></asp:Label></label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="tbxContactFax2" runat="server" placeholder="Enter Contact Fax" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">Sales Details</div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label3" runat="server" Text="Price"></asp:Label></label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="tbxPrice" runat="server" placeholder="Enter Price" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Chassis Number" ControlToValidate="tbxPrice">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:DropDownList ID="ddlCurrency" runat="server" DataTextField="CURRENCYCODE" DataValueField="ID" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label8" runat="server" Text="Paid Amount"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxPaidAmount" runat="server" placeholder="Enter Paid Amount" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label12" runat="server" Text="Selling Date"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxSellingDate" runat="server" placeholder="Enter Selling Date" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxSellingDate"></ajaxToolkit:CalendarExtender>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label13" runat="server" Text="Next Pay Date"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxNextPayDate" runat="server" placeholder="Enter Next Pay Date" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calNextPayDate" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxNextPayDate"></ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-right">
                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                        OnClick="btnEdit_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                        OnClick="btnReset_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

