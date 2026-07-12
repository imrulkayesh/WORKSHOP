<%@ Page Title="Tools Item" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ToolsTrn.aspx.cs" Inherits="Pages_VISM_ToolsIteams" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />



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
                        Workshop Name</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlwksp" Width="100%" runat="server" CssClass="form-control select2" DataValueField="id" DataTextField="text" AutoPostBack="true" OnSelectedIndexChanged="ddlwksp_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <label for="inputEmail3" class="col-sm-2">
                        Staff ID</label>
                    <div class="col-sm-4">
                        <asp:DropDownList runat="server" ID="ddlTech" CssClass="form-control select2" DataValueField="id" DataTextField="text" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTech" ValidationGroup="addTech" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Section Category</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlSectionCategory" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CATNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlSectionCategory_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <label for="inputEmail3" class="col-sm-2">
                        Item Name</label>
                    <div class="col-sm-4">
                        <asp:DropDownList runat="server" ID="ddlItemName" CssClass="form-control select2" DataValueField="id" DataTextField="text" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Qty</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxQty" runat="server" onkeypress="return isNumberKey(event)" placeholder="Enter Qty" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Remarks</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxRemarks" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        From Date
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        To Date
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>

                <div class="form-group">
                </div>
                <div class="form-group text-center">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />

                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search" />
                    </div>
                </div>

                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title text-center">Tools Transaction Report</div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="box">
                                <div class="box-body">
                                    <asp:Repeater ID="rpt" runat="server">
                                        <HeaderTemplate>
                                            <table id="dt" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>SL#</th>
                                                        <th>EMPLOYEE NAME</th>
                                                        <th>ITEMNAME</th>
                                                        <th>UNIT</th>
                                                        <th>QTY</th>
                                                        <th>CATEGORY</th>
                                                        <th>WORKSHOP</th>
                                                        <th>Date</th>
                                                        <th>REMARKS</th>
                                                        <th>Edit</th>
                                                        <th>Delete</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#((IDataItemContainer) Container).DataItemIndex + 1%></td>
                                                <td><%#Eval("EMPLOYEENAME") %></td>
                                                <td><%#Eval("ITEMNAME") %></td>
                                                <td><%#Eval("SCMUNITCODE") %></td>
                                                <td><%#Eval("QTY") %></td>
                                                <td><%#Eval("CATNAME") %></td>
                                                <td><%#Eval("WORKSHOP") %></td>
                                                <td><%#Eval("DT") %></td>
                                                <td><%#Eval("REMARKS")%></td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ID")%>' OnCommand="btnEdit_Command" ImageUrl="../../Content/Images/edit1.png" ToolTip="Edit" CausesValidation="False" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%#Eval("ID") %>' ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" formnovalidate="" />
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
    <script src="../../Content/dt/dataTables.buttons.min.js"></script>
    <script src="../../Content/dt/buttons.flash.min.js"></script>
    <script src="../../Content/dt/jszip.min.js"></script>
    <script src="../../Content/dt/pdfmake.min.js"></script>
    <script src="../../Content/dt/vfs_fonts.js"></script>
    <script src="../../Content/dt/buttons.html5.min.js"></script>
    <script src="../../Content/dt/buttons.print.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            load();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                load();
            }
        });
        function load() {
            var table = $('#dt').DataTable().destroy();
            table = $('#dt').DataTable({
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
    <%--    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#dt').DataTable({
                dom: 'Bfrtip',
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true,
                "order": [[0, "desc"]],
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>--%>
</asp:Content>

