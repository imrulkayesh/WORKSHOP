<%@ Page Title="User Role" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="UserRole.aspx.cs" Inherits="Pages_UserManagement_UserRole" %>

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
                                        UserName</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="tbxUserName" runat="server" placeholder="Enter User Name" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxUserName_OnTextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                            ErrorMessage="Enter User Name" ControlToValidate="tbxUserName">*</asp:RequiredFieldValidator>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchStaffId"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxUserName"
                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="box">
                                            <div class="box-body">
                                                <asp:Repeater ID="rpt" runat="server">
                                                    <HeaderTemplate>
                                                        <div style="max-height: 550px; overflow: auto;">
                                                            <table id="example1" class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Role</th>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkHeader" Visible="False" runat="server" /></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Eval("ROLENAME") %><asp:HiddenField ID="hdRoleId" runat="server" Value='<%#Eval("ID") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkRecom" runat="server" /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                </table>
                                        </div>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-3 col-sm-9 text-right">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                                            OnClick="btnSave_Click" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Update" Visible="false" CssClass="btn btn-primary" ValidationGroup="save"
                                            OnClick="btnEdit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            load();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                load();
            }
        });
        function load() {
            var table = $('#example1').DataTable().destroy();

            table = $('#example1').DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true
            });

            $("#example1 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example1 [id*=chkRecom]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkRecom]").prop("checked", false);
                }
            });

            $("#example1 [id*=chkRecom]").click(function () {
                if ($("#example1 [id*=chkRecom]").length == $("#example1 [id*=chkRecom]:checked").length) {
                    $("#example1 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkHeader]").prop("checked", false);
                }
            });

            $(".select2").select2();
        }
    </script>
</asp:Content>

