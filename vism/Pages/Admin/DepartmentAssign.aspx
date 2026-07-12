<%@ Page Title="Department Assign" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="DepartmentAssign.aspx.cs" Inherits="Pages_Admin_DepartmentAssign" %>

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
                        <asp:Label ID="Label5" runat="server" Text="Company"></asp:Label><span class="required"> *</span></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" DataTextField="COMPANYNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Company" ControlToValidate="ddlCompany">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label6" runat="server" Text="Country"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control select2" DataTextField="COUNTRYNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="Location"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2" DataTextField="NAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Location" ControlToValidate="ddlLocation">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label1" runat="server" Text="Business Center"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlBusinessCenter" runat="server" CssClass="form-control select2" DataTextField="CENTERNAME" DataValueField="ID"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Business Center" ControlToValidate="ddlBusinessCenter">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button ID="btnSearch" runat="server" ClientIDMode="Static" Text="Search" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />

                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:GridView ID="gvDepartment" ClientIDMode="Static" DataKeyNames="ID" runat="server" CssClass="table table-bordered table-responsive table-hover" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="Department ID" />
                                        <asp:BoundField DataField="DEPARTMENTNAME" HeaderText="Department" />
                                        <asp:BoundField DataField="DEPARTMENTCODE" HeaderText="Department Code" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkrow" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
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
            $("#gvDepartment [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#gvDepartment [id*=chkrow]").prop("checked", true);
                } else {
                    $("#gvDepartment [id*=chkrow]").prop("checked", false);
                }
            });

            $("#gvDepartment [id*=chkrow]").click(function () {
                if ($("#gvDepartment [id*=chkrow]").length == $("#gvDepartment [id*=chkrow]:checked").length) {
                    $("#gvDepartment [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#gvDepartment [id*=chkHeader]").prop("checked", false);
                }
            });

            $(".select2").select2();
        }
    </script>
</asp:Content>

