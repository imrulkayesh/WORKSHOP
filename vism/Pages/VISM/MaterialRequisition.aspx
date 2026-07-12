<%@ Page Title="Material Requisition" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="MaterialRequisition.aspx.cs" Inherits="Pages_VISM_MaterialRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>

            <div class="col-md-10 col-sm-10 col-xs-10">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                <asp:HiddenField ID="hdnID" runat="server" />
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    Parts List
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rptParts" runat="server">
                        <HeaderTemplate>
                            <div style="max-height: 200px; overflow-y: auto">
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Item</th>
                                            <th>Req. Quantity</th>
                                            <th>Amount</th>
                                            <th>Store Qty.</th>
                                            <th>
                                                <asp:CheckBox ID="chkHeader" runat="server" Text="All" /></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("PARTSNAME") %>
                                    <asp:HiddenField ID="hdnPartsId" runat="server" Value='<%#Eval("ID") %>' />
                                </td>
                                <td>
                                    <%#Eval("QUANTITY") %>
                                </td>
                                <td><%#Eval("AMOUNT") %>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxStoreQty" CssClass="form-control" runat="server" Text='<%# Eval("STOREQTY") %>'></asp:TextBox>
                                </td>

                                <td>
                                    <asp:CheckBox ID="chkrow" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table></div>
                        </FooterTemplate>
                    </asp:Repeater>

                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Requisition List
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <table id="example3" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>JobNo</th>
                                        <th>Customer</th>
                                        <th>Mobile</th>
                                        <th>Vehicle</th>
                                        <th>Service Type</th>
                                        <th>Eff. Date</th>
                                        <th>Remark</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ID") %></td>
                                <td><%#Eval("CUSTOMERNAME") %></td>
                                <td><%#Eval("MOBILE") %></td>
                                <td><%#Eval("CHASSISNO") %>/<%#Eval("REGNO") %></td>
                                <td><%#Eval("SERVICETYPE") %></td>
                                <td><%#Eval("EFFECTIVEDATE","{0:dd/MM/yyyy}") %></td>
                                <td><%#Eval("REMARKS") %></td>
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
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
            var table = $('#example1,#example2,#example3,#example4').DataTable().destroy();

            table = $('#example1,#example2,#example3,#example4').DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true
            });
            $(".select2").select2();
        }
    </script>
</asp:Content>

