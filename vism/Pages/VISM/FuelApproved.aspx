<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="FuelApproved.aspx.cs" Inherits="Pages_VISM_FuelApproved" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">

                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Fuel Approved</div>
                    </div>
                    <div class="panel-body">
                    </div>
                </div>

                <br />
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>SL#</th>
                                                    <th>Vehicle ID</th>
                                                    <th>Company</th>
                                                    <th>Fuel Type</th>
                                                    <th>Pump</th>
                                                    <th>Demand ID</th>
                                                    <th>Remarks</th>
                                                    <th>Qty(Ltr.)</th>
                                                    <th>Approve</th>
                                                    <th>Reject</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#((IDataItemContainer) Container).DataItemIndex + 1%></td>
                                            <td><%#Eval("REGNO") %></td>
                                            <td><%#Eval("COMPANYNAME") %></td>
                                            <td><%#Eval("ITEMNAME") %></td>
                                            <td><%#Eval("PUMPNAME") %></td>
                                            <td><%#Eval("TRANSACTIONID") %></td>
                                            <td><%#Eval("REMARKS")%></td>
                                            <td>
                                                <asp:TextBox ID="tbxfdqty" runat="server" Text='<%#Eval("QTY") %>' CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("TRANSACTIONID") %>' ImageUrl="~/Content/Images/Common/oksmal.jpg" ToolTip="Approve" formnovalidate="" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("TRANSACTIONID")%>' OnCommand="imgbtnDelete_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" formnovalidate="" OnClientClick="return confirm('Are you sure?')" />
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

