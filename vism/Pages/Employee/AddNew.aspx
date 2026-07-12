<%@ Page Title="Add New Employee" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="AddNew.aspx.cs" Inherits="Pages_Employee_AddNew" %>

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
                        <asp:Label ID="Label18" runat="server" Text="Staff ID"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxStaffID" runat="server" placeholder="Enter Staff ID" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Staff ID" ControlToValidate="tbxStaffID">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label5" runat="server" Text="Name"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Name" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Business Unit<span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCompany" Width="100%" runat="server" CssClass="form-control select2" DataTextField="COMPANYNAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Company" ControlToValidate="ddlCompany">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="Country"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCountry" Width="100%" runat="server" CssClass="form-control select2" DataTextField="COUNTRYNAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Location<span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlBusinessCenter" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CENTERNAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Business Center" ControlToValidate="ddlBusinessCenter">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label8" runat="server" Text="Department"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDepartment" Width="100%" runat="server" CssClass="form-control select2" DataTextField="DEPARTMENTNAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Department" ControlToValidate="ddlDepartment">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">

                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label9" runat="server" Text="Designation"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDesignation" Width="100%" runat="server" CssClass="form-control select2" DataTextField="DESIGNATIONNAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Designation" ControlToValidate="ddlDesignation">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label20" runat="server" Text="Email"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Email" ControlToValidate="tbxEmail">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label10" runat="server" Text="Mobile"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile Number" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Mobile Number" ControlToValidate="tbxMobile">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        User Type<span class="required"> *</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlRole" Width="100%" DataTextField="ROLENAME" DataValueField="ID" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>

                </div>
                <div class="form-group">
                    <div class="col-sm-11 text-right">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptYear" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label5" runat="server" Text="Employee"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label13" runat="server" Text="Company"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label14" runat="server" Text="Center"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label15" runat="server" Text="Department"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label4" runat="server" Text="Designation"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label16" runat="server" Text="Mobile"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label17" runat="server" Text="Email"></asp:Label></th>
                                                    <th>
                                                        Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("EMPLOYEENAME") %></td>
                                            <td><%#Eval("COMPANYNAME") %></td>
                                            <td><%#Eval("CENTERNAME") %></td>
                                            <td><%#Eval("DEPARTMENTNAME") %></td>
                                            <td><%#Eval("DESIGNATIONNAME") %></td>
                                            <td><%#Eval("OFFICEMOBILE") %></td>
                                            <td><%#Eval("OFFICEMAIL") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%></td>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

