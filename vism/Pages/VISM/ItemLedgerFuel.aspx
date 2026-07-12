<%@ Page Title="Item-Ledger for Fuel" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemLedgerFuel.aspx.cs" Inherits="Pages_VISM_ItemLedgerFuel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Location<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlLocation" Width="100%" runat="server" DataValueField="ID" DataTextField="TEXT" CssClass="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_OnSelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlLocation" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                Warehouse<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlWh" Width="100%" runat="server" DataValueField="ID" DataTextField="TEXT" CssClass="form-control select2"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlWh" InitialValue="" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </div>
        </div>

        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxfromdate" InitialValue="" ID="RequiredFieldValidator5" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxtodate" InitialValue="" ID="RequiredFieldValidator6" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </div>
        </div>


        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-danger" Text="Download Fuel CO" OnClick="btndownloadNormalExcelonFUEL_OnClick" />
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-success" CausesValidation="False" Text="Reset" OnClick="btnReset_OnClick" />

            </div>
        </div>

    </div>
    <hr />

    <div class="form-group">
        <div class="col-sm-12 text-center">
            <asp:Panel ID="Panel2" runat="server" Width="100%">
                <asp:GridView ID="GridView2" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" Width="100%" AlternatingRowStyle-ForeColor="#000"
                    runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="CATEGORYNAME" HeaderText="CATEGORY" />
                        <asp:BoundField DataField="ITEMCODE" HeaderText="ITEMCODE" />
                        <asp:BoundField DataField="ITEMNAME" HeaderText="ITEMNAME" />
                        <asp:BoundField DataField="UNITNAME" HeaderText="UNIT" />
                        <asp:BoundField DataField="RATE" HeaderText="RATE" />
                        <asp:BoundField DataField="OPQTY" HeaderText="OPQTY" />
                        <asp:BoundField DataField="OPAMT" HeaderText="OPAMT" />
                        <asp:BoundField DataField="REQTY" HeaderText="REQTY" />
                        <asp:BoundField DataField="REAMT" HeaderText="REAMT" />
                        <asp:BoundField DataField="ISQTY" HeaderText="ISQTY" />
                        <asp:BoundField DataField="ISAMT" HeaderText="ISAMT" />
                        <asp:BoundField DataField="CLQTY" HeaderText="CLQTY" />
                        <asp:BoundField DataField="CLAMT" HeaderText="CLAMT" />
                    </Columns>
                </asp:GridView>

            </asp:Panel>
        </div>
    </div>



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
            var table = $('#dt').DataTable({
                dom: 'Bfrtip',
                "paging": false,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                //"info": true,
                "autoWidth": true,
                "order": [[0, "asc"]],
                buttons: [
                    'copy', 'excel', 'print'
                ]
            });
        });
    </script>
</asp:Content>

