<%@ Page Title="PDI" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PDI.aspx.cs" Inherits="Pages_VISM_PDI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Vehichle Details
                </div>
                <div class="panel-body">
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
                                JobNo</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxJobID" runat="server" AutoPostBack="true" OnTextChanged="tbxJobID_TextChanged" placeholder="Enter Job No" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdnJobID" Value="0" runat="server" />
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Job Card" ControlToValidate="tbxJobID">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchJob"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="tbxJobID"
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Workshop</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlworkshopid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshopid_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Service Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlcategoryid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID"></asp:DropDownList>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Technician</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlTechnician" Width="100%" runat="server" CssClass="form-control select2" DataTextField="TECHNICIAN" DataValueField="STAFFID"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Remarks</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="tbxremark" TextMode="MultiLine" runat="server" placeholder="Enter Remark" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Inspection Status
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rptInspection" runat="server">
                        <HeaderTemplate>
                            <div style="max-height: 300px; overflow: auto;">
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Category</th>
                                            <th>Problem</th>
                                            <th>Status</th>
                                            <th>Remark</th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" Text="All" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CATNAME") %></td>
                                <td><%#Eval("ITEMNAME") %>
                                    <asp:HiddenField ID="hdnInsItemId" runat="server" Value='<%#Eval("ID") %>' />
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("STATUSID") %>' />
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbStatus" runat="server" Width="100%" RepeatColumns="3">
                                        <asp:ListItem Value="1" Selected="True"> OK</asp:ListItem>
                                        <asp:ListItem Value="2"> Problem</asp:ListItem>
                                        <asp:ListItem Value="3"> N/A</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxInsRemarks" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("PDIID").ToString())? "" : Eval("REMARKS") %>'></asp:TextBox></td>
                                <td>
                                    <asp:CheckBox ID="chkrow" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table></div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="col-sm-12 text-center">
                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                    OnClick="btnEdit_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                    OnClick="btnReset_Click" />
            </div>
            <br />
            <hr />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Bill Details
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Vehicle</th>
                                        <th>Customer</th>
                                        <th>Mobile</th>
                                        <th>Technician</th>
                                        <th>Remark</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ID") %></td>
                                <td>
                                    <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' Text='<%#Eval("CHASSISNO")+"/"+Eval("REGNO") %>' ToolTip="View Details" /></td>
                                <td><%#Eval("CUSTOMERNAME") %></td>
                                <td><%#Eval("MOBILE") %></td>
                                <td><%#Eval("TECSTAFFID") %></td>
                                <td><%#Eval("REMARKS") %></td>
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table>
                        </FooterTemplate>
                    </asp:Repeater>
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
                    $("#example1 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example1 [id*=chkrow]").click(function () {
                if ($("#example1 [id*=chkrow]").length == $("#example1 [id*=chkrow]:checked").length) {
                    $("#example1 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkHeader]").prop("checked", false);
                }
            });
            $(".select2").select2();
        }
    </script>
</asp:Content>

