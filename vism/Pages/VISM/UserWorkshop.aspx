<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="UserWorkshop.aspx.cs" Inherits="Pages_VISM_UserWorkshop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-1">User</label>
                    <div class="col-sm-4">
                       <asp:TextBox runat="server" ID="tbxUser" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxUser_OnTextChanged"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxUser" InitialValue="" ID="RequiredFieldValidator1"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchStaffId"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxUser"
                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                <label class="col-sm-1">Workshop</label>
                    <div class="col-sm-4">
                       <asp:DropDownList runat="server" Enabled="True" ID="ddlWorkshop" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlWorkshop" InitialValue="" ID="RequiredFieldValidator7"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-sm-2">
                       <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-success" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-danger" OnClientClick="this.form.reset();return true;" OnClick="btnReset_OnClick"/>
                    </div>
                    </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

