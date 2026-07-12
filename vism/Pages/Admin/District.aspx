<%@ Page Title="District" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="District.aspx.cs" Inherits="Pages_Admin_District" %>

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
                    <label for="inputEmail3" class="col-sm-1">
                        <asp:Label ID="Label2" runat="server" Text="Country"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddCountry" runat="server" CssClass="form-control select2" DataTextField="COUNTRYNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddCountry_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-1">
                       Division<span class="required">*</span></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDivision" runat="server" DataTextField="DIVISIONNAME" DataValueField="ID" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Division" ControlToValidate="ddlDivision">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-1">
                        <asp:Label ID="Label1" runat="server" Text="District"></asp:Label><span class="required">*</span></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxName" runat="server" placeholder="Enter District" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter District" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-1 col-sm-11">
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
                                                       District</th>
                                                    <th>
                                                       Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("DISTRICTNAME") %></td>
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
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>

