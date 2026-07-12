<%@ Page Title="RoleSetup" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="RoleSetup.aspx.cs" Inherits="Pages_RoleManagement_RoleSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 col-sm-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3">
                                        RoleName</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="tbxRole" runat="server" placeholder="Enter Role Name" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter Role Name" ControlToValidate="tbxRole">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3">
                                        <asp:Label ID="Label4" runat="server" Text="Role Priority"></asp:Label></label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="savewww"
                                            ErrorMessage="Enter Role Priority" ControlToValidate="ddlPriority">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3">
                                        <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label></label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="tbxDescription" runat="server" placeholder="Enter Role Description" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-3 col-sm-9">
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
                <div class="col-md-6">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <asp:Repeater ID="rptRole" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>SL</th>
                                                <th>RoleName</th>
                                                <th>CreatedBy</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Container.ItemIndex+1 %></td>
                                        <td><%#Eval("ROLENAME") %></td>
                                        <td><%#Eval("CREATEDBY") %></td>
                                        <%--<td><%# Convert.ToDateTime(Eval("CreateDate")).ToString("dd-MMM-yyyy") %></td>--%>
                                        <td>
                                            <asp:LinkButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ID")%>' CssClass="fa fa-2x fa-edit" />
                                            <asp:LinkButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" Visible="false" CssClass="fa fa-2x fa-trash-o" OnClientClick="return confirm('Are you sure?')" /></td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

