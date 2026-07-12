<%@ Page Title="Group of Company" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="GroupofCompany.aspx.cs" Inherits="Pages_Admin_GroupofCompany" %>

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
                        <asp:Label ID="Label5" runat="server" Text="Group of Company"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxName" runat="server" placeholder="Enter Company Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Company Name" ControlToValidate="tbxName">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="Establish Date"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxEstablishDate" runat="server" placeholder="Enter Establish Date" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Establish Date" ControlToValidate="tbxEstablishDate">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calext" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxEstablishDate"></ajaxToolkit:CalendarExtender>
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
                                                    <asp:Label ID="Label5" runat="server" Text="Group of Company"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label3" runat="server" Text="Establish Date"></asp:Label></th>
                                                <th>
                                                   Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("COMPANYTITLE") %></td>
                                        <td><%#Eval("ESTABLISHDATE", "{0:dd/MM/yyyy}") %></td>
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

