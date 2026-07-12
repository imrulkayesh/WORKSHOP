<%@ Page Title="Customer" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Pages_VISM_Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
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
                        <asp:Label ID="Label18" runat="server" Text="Customer Name"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxCustomerName" runat="server" placeholder="Enter Customer Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Customer Name" ControlToValidate="tbxCustomerName">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label5" runat="server" Text="Mobile"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Mobile" ControlToValidate="tbxMobile">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label6" runat="server" Text="Phone"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="Fax"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxFax" runat="server" placeholder="Enter Fax" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">

                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label8" runat="server" Text="Email"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label11" runat="server" Text="Gender"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlGender" Width="100%" runat="server" CssClass="form-control select2" DataTextField="GENDERNAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Gender" ControlToValidate="ddlGender">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label10" runat="server" Text="Present Address"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxAddress" runat="server" placeholder="Enter Present Address" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Present Address" ControlToValidate="tbxAddress">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label20" runat="server" Text="Country"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCountry" Width="100%" runat="server" CssClass="form-control select2" DataTextField="COUNTRYNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Country" ControlToValidate="ddlCountry">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label7" runat="server" Text="Division"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDivision" Width="100%" runat="server" CssClass="form-control select2" DataTextField="DIVISIONNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Division" ControlToValidate="ddlDivision">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label9" runat="server" Text="District"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDistrict" Width="100%" runat="server" CssClass="form-control select2" DataTextField="DISTRICTNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                            ErrorMessage="Select District" ControlToValidate="ddlDistrict">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label12" runat="server" Text="Thana"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlThana" Width="100%" runat="server" CssClass="form-control select2" DataTextField="THANANAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Thana" ControlToValidate="ddlDivision">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label13" runat="server" Text="PostCode"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxPostCode" runat="server" placeholder="Enter PostCode" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label19" runat="server" Text="NID"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxNid" runat="server" placeholder="Enter NID" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter NID" ControlToValidate="tbxNid">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label21" runat="server" Text="Passport"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxPassport" runat="server" placeholder="Enter Passport No." CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label24" runat="server" Text="Birth Certificate"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label25" runat="server" Text="TIN"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxTin" runat="server" placeholder="Enter TIN No." CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label22" runat="server" Text="Religion"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlReligion" Width="100%" runat="server" CssClass="form-control select2" DataTextField="RELIGIONNAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Religion" ControlToValidate="ddlReligion">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label23" runat="server" Text="Profession"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxProfession" runat="server" placeholder="Enter Profession" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label26" runat="server" Text="Father's Name"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxFather" runat="server" placeholder="Enter Father's Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Father's Name" ControlToValidate="tbxFather">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label27" runat="server" Text="Mother's Name"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxMother" runat="server" placeholder="Enter Mother's Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator12" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Mother's Name" ControlToValidate="tbxMother">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label28" runat="server" Text="Guardian's Name"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxGuardian" runat="server" placeholder="Enter Guardian's Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator13" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Guardian's Name" ControlToValidate="tbxGuardian">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label29" runat="server" Text="DOB"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxDob" runat="server" placeholder="Enter Date of Birth" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator14" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Date of Birth" ControlToValidate="tbxDob">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDob"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label30" runat="server" Text="Customer Photo"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:FileUpload ID="addPhoto" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-11 text-right">
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
                                        <div style="overflow: auto; height: 500px">
                                            <table id="example1" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Mobile</th>
                                                        <th>Email</th>
                                                        <th>Address</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("CUSTOMERNAME") %></td>
                                            <td><%#Eval("MOBILE") %></td>
                                            <td><%#Eval("EMAIL") %></td>
                                            <td><%#Eval("ADDRESS") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                </table> </div>
                                  
                                    </FooterTemplate>

                                </asp:Repeater>
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

