<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemAllBarcodePrint.aspx.cs" Inherits="Pages_VISM_ItemAllBarcodePrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
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
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvWh" InitialValue="" ID="RequiredFieldValidator5" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Item Category</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlItemCategory" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemCategory" InitialValue="" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-sm-8">
                                        <asp:Button ID="btnView" runat="server" ClientIDMode="Static" Text="View" CssClass="btn btn-primary" ValidationGroup="view"
                                            OnClick="btnView_OnClick" formnovalidate="" />

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                    </div>
                </div>
                <hr />

                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Sl. No.</th>
                                                    <th>Item Code</th>
                                                    <th>Item Name</th>
                                                    <th>Category</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#((IDataItemContainer) Container).DataItemIndex + 1%></td>
                                            <td><%#Eval("ITEMCODE") %></td>
                                            <td><%#Eval("ITEMNAME") %></td>
                                            <td><%#Eval("ITEMCATEGORYNAME") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("ITEMCODE") %>' ImageUrl="~/Content/Images/Common/printer_icon.png" ToolTip="Print" formnovalidate="" />&emsp;&emsp;
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
                dom: 'Bfrtip',
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true,
                "order": [[0, "asc"]],
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });

            $("#example1 [id*=tbxfdqty]").on("input", function () {
                var qty = $(this).closest('tr').find('[id*=tbxfdqty]').val();

            });

            $(".select2").select2();
        }
    </script>
</asp:Content>

