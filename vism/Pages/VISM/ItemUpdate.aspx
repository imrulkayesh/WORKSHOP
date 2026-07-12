<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemUpdate.aspx.cs" Inherits="Pages_VISM_ItemUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:HiddenField runat="server" ID="hdnUnitName" />

                <div class="form-group">
                    <div class="col-md-6">
                        <div class="panel panel-info">
                            <%-- <a href="../../App_WebReferences/">../../App_WebReferences/</a>--%>
                            <div class="panel-heading" style="height: 35px">
                                <div class="panel-title" style="text-align: center; margin-top: -7px">Warehouse Item Update</div>
                            </div>
                            <div class="panel-body">
                                <%-- <a href="../../Content/">../../Content/</a>--%>

                                <div class="form-group">
                                    <label class="col-sm-4">Location</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="True" ID="ddlRecvLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlRecvLoc_OnSelectedIndexChanged" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvLoc" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">VISM Warehouse</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlRecvWh_OnSelectedIndexChanged" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvWh" InitialValue="" ID="RequiredFieldValidator5" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Item Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxItem" runat="server" ClientIDMode="Static" placeholder="Start typing to search item" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxItem_OnTextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchItem"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxItem"
                                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>

                                </div>
                                <%--    <div class="form-group">

                                    <div class="col-sm-8">
                                        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_OnClick" />
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                    </div>
                </div>

                <%--     <div class="panel-footer" style="height: 55px;">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-success" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-default" OnClientClick="this.form.reset();return false;" OnClick="btnReset_OnClick" />
                    </div>

                </div>--%>
                <hr />

                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Item List</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Panel runat="server" ID="pnlAddItem">
                                <div class="col-sm-2">
                                    <label>
                                        Category Name
                                    </label>
                                    <asp:TextBox ID="tbxCategoryName" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <label>
                                        Item Code
                                    </label>
                                    <asp:TextBox ID="tbxItemCode" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <label>
                                        Item Name
                                    </label>
                                    <asp:TextBox ID="tbxItemName" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Unit
                                    </label>
                                    <asp:TextBox ID="tbxUnitName" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxUnitName" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator9" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Rate
                                    </label>
                                    <asp:TextBox ID="tbxRate" runat="server" ClientIDMode="Static" onkeypress="return isNumberKey(event)"  CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxRate" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator10" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>

                                <div class="col-sm-1">
                                    <label>
                                        Action
                                    </label>
                                    <%-- <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="btn btn-primary" ValidationGroup="add" OnClick="btnAdd_OnClick"></asp:Button>--%>
                                    <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btn btn-warning" ValidationGroup="add" OnClick="btnUpdate_OnClick"></asp:Button>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
     <script type="text/javascript">
      <!--
    function isNumberKey(evt) {

        var e = event || evt; // for trans-browser compatibility
        var charCode = e.which || e.keyCode;
        if (charCode == 46)
            return true
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;



    }
    //-->
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
</asp:Content>

