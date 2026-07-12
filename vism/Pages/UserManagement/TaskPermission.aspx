<%@ Page Title="TaskToRole" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="TaskPermission.aspx.cs" Inherits="Pages_RoleManagement_TaskPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(load);
            </script>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">Role</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlRole" Width="100%" DataTextField="ROLENAME" DataValueField="ID" runat="server" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">TaskList</label>
                    <div class="col-sm-10">
                        <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes="All">
                        </asp:TreeView>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlRole"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        function load() {
            $("[id*=TreeView1] input[type=checkbox]").on("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).prop("checked", true);
                        } else {
                            $(this).prop("checked", false);
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).prop("checked", true);
                    } else {
                        $("input[type=checkbox]", parentDIV.prev()).prop("checked", false);
                    }
                }
            });
        }

        $(function () {
            load();
        });
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                load();
            });
        };
    </script>
</asp:Content>

