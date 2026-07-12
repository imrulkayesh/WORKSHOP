<%@ Page Title="Location" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Location.aspx.cs" Inherits="Pages_Admin_Location" %>

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
                        <asp:Label ID="Label2" runat="server" Text="Country"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddCountry" runat="server" CssClass="form-control select2" DataTextField="COUNTRYNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddCountry_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label9" runat="server" Text="Titme Zone"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlTimeZone" runat="server" CssClass="form-control select2" ></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Division<span class="required">*</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDivision" runat="server" DataTextField="DIVISIONNAME" DataValueField="ID" CssClass="form-control select2" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Division" ControlToValidate="ddlDivision">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                       District<span class="required">*</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDistrict" DataTextField="DISTRICTNAME" DataValueField="Id" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Select District" ControlToValidate="ddlDistrict">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                       Thana<span class="required">*</span></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlThana" DataTextField="THANANAME" DataValueField="ID" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlThana_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Thana" ControlToValidate="ddlThana">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label7" runat="server" Text="Location Name"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Location Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Location Name" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label8" runat="server" Text="Location Code"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxLocationCode" runat="server" placeholder="Enter Location Code" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Location Code" ControlToValidate="tbxLocationCode">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
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
                                                        <asp:Label ID="Label1" runat="server" Text="Location"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label4" runat="server" Text="Location Code"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label6" runat="server" Text="Time Zone"></asp:Label></th>
                                                    <th>
                                                        Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("NAME") %></td>
                                            <td><%#Eval("LOCATIONCODE") %></td>
                                            <td><%#Eval("TIMEZONE") %></td>
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

