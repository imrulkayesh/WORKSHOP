<%@ Page Title="Item-Ledger" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemLedger.aspx.cs" Inherits="Pages_VISM_ItemLedger" %>


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
                From Item<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlFromItem" Width="100%" runat="server" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT"  autocomplete="off"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlFromItem" InitialValue="" ID="RequiredFieldValidator2" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Item<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlToItem" Width="100%" runat="server" DataValueField="ID" DataTextField="TEXT" CssClass="form-control select2"  autocomplete="off"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlToItem" InitialValue="" ID="RequiredFieldValidator3" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

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
                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_OnClickClick" Text="Show" />
                <asp:Button ID="btnOpenPdf" Visible="True" runat="server" CssClass="btn btn-success" Text="View As Pdf" OnClick="btnOpenPdf_OnClick" ClientIDMode="Static" />
                <asp:Button ID="btnDownloadPdf" Visible="True" runat="server" CssClass="btn btn-success" Text="Download As Pdf" OnClick="btnDownloadPdf_OnClick" />
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-success" CausesValidation="False" Text="Reset" OnClick="btnReset_OnClick" />

            </div>
        </div>

    </div>
    <hr />



    <div style="overflow: auto; max-height: 600px;">
        <asp:Panel runat="server" ID="pnlReportButton" Visible="False">
            <div class="form-group">
                <div class="col-sm-12 text-center">
                    <%--<asp:Button ID="btndownloadExcel" runat="server" CssClass="btn btn-success" Text="Download As Excel" OnClick="btndownloadExcel_OnClick" />--%>
                </div>
            </div>
        </asp:Panel>
        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_ItemBound">
            <HeaderTemplate>
                <table id="dt" class="table table-striped table-bordered" width="100%" cellspacing="0">
                    <thead>
                        <tr>
                            <th>SL#
                            </th>
                            <asp:Repeater runat="server" ID="hdrRpt">
                                <ItemTemplate>
                                    <th>
                                        <%# Container.DataItem %>
                                    </th>

                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Container.ItemIndex+1 %></td>
                    <asp:Repeater runat="server" ID="bdyRpt">
                        <ItemTemplate>
                            <td>
                                <%# Container.DataItem %>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>

            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        <tr>
            <td>Total
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <%# totalAmt.ToString() %>
            </td>
            <td></td>
        </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
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

