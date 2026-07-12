<%@ Page Title="Report-CompanyWise" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ReportCompanyWise.aspx.cs" Inherits="Pages_VISM_ReportCompanyWise" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                Select Company<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxCom" EnableViewState="True" Enabled="True" runat="server" placeholder="Enter Company Name" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="tbxCom_OnTextChanged"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCom"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                    TargetControlID="tbxCom"
                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                </ajaxToolkit:AutoCompleteExtender>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxCom" InitialValue="" ID="RequiredFieldValidator2" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>

        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxfromdate" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date<span class="required"> *</span></label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxtodate" InitialValue="" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </div>


        </div>




        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_OnClick" Text="Show" />

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

