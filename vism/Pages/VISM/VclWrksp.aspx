<%@ Page Title="Vehicle" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VclWrksp.aspx.cs" Inherits="Pages_VISM_VclWrksp" %>

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
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Customer Details
                </div>
                <div class="panel-body">
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
            </div>
            
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Vehicle Details
                </div>
                <div class="panel-body">
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
                                <asp:DropDownList ID="ddlVehicleType" DataTextField="VEHICLETYPENAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
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
                                <asp:DropDownList ID="ddlEngineModel" DataTextField="ENGINEMODELNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Engine Model" ControlToValidate="ddlEngineModel">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label8" runat="server" Text="Manufacturer"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlManufacturer" DataTextField="MANUFACTURERNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Manufacturer" ControlToValidate="ddlManufacturer">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label9" runat="server" Text="Manu.Year"></asp:Label></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="tbxManufacturingYear" runat="server" placeholder="Enter Only Year (like-2017)" CssClass="form-control"></asp:TextBox>
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
                            <%--<label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label11" runat="server" Text="Millage"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="tbxMillage" runat="server" placeholder="Enter Millage" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Millage" ControlToValidate="tbxMillage">*</asp:RequiredFieldValidator>
                            </div>--%>
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label12" runat="server" Text="Brand"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlBrand" DataTextField="BRANDNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Brand" ControlToValidate="ddlBrand">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label13" runat="server" Text="Model"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlModel" DataTextField="MODELNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Model" ControlToValidate="ddlModel">*</asp:RequiredFieldValidator>
                            </div>
                             <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label14" runat="server" Text="Body"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlBodyType" DataTextField="BODYTYPENAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Body Type" ControlToValidate="ddlBodyType">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label15" runat="server" Text="Fuel"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlFuleType" DataTextField="FUELTYPENAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Fuel Type" ControlToValidate="ddlFuleType">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label16" runat="server" Text="Origin"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlOrigin" DataTextField="COUNTRYNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator12" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Origin" ControlToValidate="ddlOrigin">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label32" runat="server" Text="Company"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlCompany" DataTextField="COMPANYNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator22" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Select Company" ControlToValidate="ddlCompany">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-md-1">
                                <asp:Label ID="Label33" runat="server" Text="Location"></asp:Label><span class="required">*</span></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlBusinessCenter" DataTextField="CENTERNAME" DataValueField="ID" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator23" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Location" ControlToValidate="ddlBusinessCenter">*</asp:RequiredFieldValidator>
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
                            <div class="col-sm-12 text-center">
                                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnUpdate" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" Visible="false"
                                    OnClick="btnUpdate_OnClick" />
                                <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                                    OnClick="btnReset_Click" />
                            <asp:Button ID="btnVehicleSearch" runat="server" Text="Search" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnVehicleSearch_OnClick"/>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <hr/>
            <div style="overflow: auto; max-height: 500px;">
                <asp:Panel runat="server" ID="pnlReportButton" Visible="False">
               <div class="form-group">
                <div class="col-sm-12 text-center">
                   <asp:LinkButton ID="lnkDownloadExcel" runat="server" CssClass="btn btn-success" OnClick="lnkDownloadExcel_OnClick">Download (Excel) <i class="fa fa-download"></i></asp:LinkButton>
                    <asp:Button ID="btnDownloadPdf" Visible="False" runat="server" CssClass="btn btn-success" Text="Download As Pdf" OnClick="btnDownloadPdf_OnClick"/>
                    <asp:Button ID="btnOpenPdf" Visible="False" runat="server" CssClass="btn btn-success" Text="View As Pdf" OnClick="btnOpenPdf_OnClick" ClientIDMode="Static"/>
                    
                </div>
            </div>
                   </asp:Panel>
                
        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_ItemBound">
    <HeaderTemplate>
        <table id="example1" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                <thead>
                <tr>
                    <th>
                        SL#
                    </th>
            <asp:Repeater runat="server" ID="hdrRpt">
                <ItemTemplate>
                    <th>
                        <%# Container.DataItem %>
                    </th>
                    
                </ItemTemplate>
            </asp:Repeater>
                    <th>Action</th>
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
                <asp:LinkButton ID="btnEdit" Text="Edit"  Enabled="True" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnEdit_OnCommand" CausesValidation="False"/>
                <asp:LinkButton ID="btnDelete" Text="Delete" Visible="False" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure to delete?');" CausesValidation="False" />

            </td>
        </tr>

    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        
            </table>
        </FooterTemplate>
</asp:Repeater>
                    
            </div>
           <!-- <div class="panel panel-primary">
                <div class="panel-heading">
                    Vehicle List
                </div>
                <div class="panel-body">
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
                                                <th>Reg.No#</th>
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
                                        <td><%#Eval("MODELNAME") %></td>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                            <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" /></td>
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
            </div>-->
            
        </ContentTemplate>
        <Triggers>
     <asp:PostBackTrigger ControlID="lnkDownloadExcel"/> 
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

