<%@ Page Title="WorkShop" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="WorkShop.aspx.cs" Inherits="Pages_VISM_WorkShop" %>

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
                        WorkShop</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxWName" runat="server" placeholder="Enter WorkShop" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter WorkShop" ControlToValidate="tbxWName">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Location</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlBusCenter" Width="100%"  runat="server" CssClass="form-control select2" DataTextField="CENTERNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlBusCenter_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Incharges ID</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxIncStaffid" runat="server" placeholder="Enter Incharges StaffID" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Incharges StaffID" ControlToValidate="tbxIncStaffid">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Address</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxAddress" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Address" ControlToValidate="tbxAddress">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Web Site</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxWebSite" runat="server" placeholder="Enter Web Site" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Web Site" ControlToValidate="tbxWebSite">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Email</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator5" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Email" ControlToValidate="tbxEmail">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Mobile</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Mobile" ControlToValidate="tbxMobile">*</asp:RequiredFieldValidator>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Phone</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Phone" ControlToValidate="tbxPhone">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Fax</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxFax" runat="server" placeholder="Enter Fax" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Fax" ControlToValidate="tbxFax">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group text-center">
                    <div class="col-sm-offset-2 col-sm-11">
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
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate><div style="overflow:auto">
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>WorkShop</th>
                                                    <th>Business Center</th>
                                                    <th>Incharges ID</th>
                                                    <th>Address</th>
                                                    <th>Website</th>
                                                    <th>Email</th>
                                                    <th>Mobile</th>
                                                    <th>Phone</th>
                                                    <th>Fax</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("WORKSHOP") %></td>
                                            <td><%#Eval("CENTERNAME") %></td>
                                            <td><%#Eval("INCHARGESTAFFID") %></td>
                                            <td><%#Eval("ADDRESS") %></td>
                                            <td><%#Eval("WEBSITE") %></td>
                                            <td><%#Eval("EMAIL") %></td>
                                            <td><%#Eval("MOBILE") %></td>
                                            <td><%#Eval("PHONE") %></td>
                                            <td><%#Eval("FAX") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%>

                                            </td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

