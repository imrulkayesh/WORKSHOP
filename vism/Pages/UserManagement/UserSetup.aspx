<%@ Page Title="User Setup" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="UserSetup.aspx.cs" Inherits="Pages_UserManagement_UserSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="upUserSEtup" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 col-sm-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label10" runat="server" Text="Staff ID"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxStaffId" runat="server" placeholder="Enter StaffId" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter StaffId" ControlToValidate="tbxStaffId">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label17" runat="server" Text="Company"></asp:Label><span class="required"> *</span></label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" DataTextField="COMPANYNAME" DataValueField="ID"></asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator11" runat="server" ValidationGroup="save"
                                            ErrorMessage="Select Company" ControlToValidate="ddlCompany">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxName" runat="server" placeholder="Enter your name" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter your name" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label3" runat="server" Text="Mobile"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter your mobile number" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter your mobile number" ControlToValidate="tbxMobile">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label4" runat="server" Text="Role"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlRole" DataTextField="ROLENAME" DataValueField="ID" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                            ErrorMessage="Select Role Name" ControlToValidate="ddlRole">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter your email address" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label11" runat="server" Text="Location"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlBusinessCenter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessCenter_SelectedIndexChanged" CssClass="form-control select2" DataTextField="CENTERNAME" DataValueField="ID"></asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                                            ErrorMessage="Select Location" ControlToValidate="ddlBusinessCenter">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label12" runat="server" Text="Department"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control select2" DataTextField="DEPARTMENTNAME" DataValueField="ID"></asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                            ErrorMessage="Select Department" ControlToValidate="ddlDepartment">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label7" runat="server" Text="User Name"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxUserName" runat="server" placeholder="Enter user name" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter users name" ControlToValidate="tbxUserName">*</asp:RequiredFieldValidator>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label13" runat="server" Text="Designation"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlDesignation" DataTextField="DESIGNATIONNAME" DataValueField="ID" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                                            ErrorMessage="Select Designation" ControlToValidate="ddlDesignation">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label8" runat="server" Text="Password"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxpassword" runat="server" placeholder="Enter password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter password" ControlToValidate="tbxpassword">*</asp:RequiredFieldValidator>--%>
                                    </div>
                                    <label for="inputEmail3" class="col-sm-2">
                                        <asp:Label ID="Label5" runat="server" Text="Confirm Password"></asp:Label><span class="red">*</span></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxConfirmPass" runat="server" placeholder="Enter confirm password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter confirm password" ControlToValidate="tbxConfirmPass">*</asp:RequiredFieldValidator>--%>
                                        <asp:CompareValidator ID="comparePassword" Display="None" ControlToCompare="tbxpassword" ControlToValidate="tbxConfirmPass" runat="server" ErrorMessage="Password does not match" ValidationGroup="save"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-6 col-sm-6">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                                            OnClick="btnSave_Click" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Update" Visible="false" CssClass="btn btn-primary" ValidationGroup="save"
                                            OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                                            OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align:right;">
                <div class="col-sm-4 text-right">
                    
                    <div style="margin-bottom: 25px" class="input-group">
                        <b>To view/update user, search first</b>
                        <br/>
                        <asp:TextBox runat="server" ID="tbxSearch" CssClass="form-control" placeholder="Search StaffID"></asp:TextBox>
                        <span class="input-group-addon" style="padding: 0px">
                            <asp:Button ID="btnSearch" runat="server" ClientIDMode="Static" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_OnClick" /></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <div style="overflow: auto; height: 500px">
                                            <table id="example1" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>User Name</th>
                                                        <th>Mobile</th>
                                                        <th>Location</th>
                                                        <th>Department</th>
                                                        <th>Designation</th>
                                                        <th>Role</th>
                                                        <th>IsActive</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("EMPLOYEENAME") %></td>
                                            <td><%#Eval("USERNAME") %></td>
                                            <td><%#Eval("OFFICEMOBILE") %></td>
                                            <td><%#Eval("CENTERCODE") %></td>
                                            <td><%#Eval("DEPARTMENTCODE") %></td>
                                            <td><%#Eval("DESIGNATIONNAME") %></td>
                                            <td><%#Eval("ROLENAME") %></td>
                                            <td><%#Eval("ISACTIVE") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

