<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="NPTForm.aspx.cs" Inherits="Pages_VISM_NPTForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
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
                    <label class="col-sm-2">Vehicle</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxVehicleid" Enabled="true" runat="server" placeholder="Enter Vehicle ID" AutoPostBack="true"  CssClass="form-control" OnTextChanged="tbxVehicleid_TextChanged"></asp:TextBox>
                        <asp:HiddenField ID="hdnVehicleId" Value="0" runat="server" />
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Vehicle" ControlToValidate="tbxVehicleid">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="tbxVehicleid"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                   <label for="inputEmail3" class="col-sm-2">
                        Job Id</label>
                     <div class="col-sm-4">
                        <asp:DropDownList ID="ddlJobId" Width="100%" runat="server" CssClass="form-control select2" DataTextField="JOBID" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged"></asp:DropDownList>
                     </div>
                </div>
                <div class="form-group">
                      <label for="inputEmail3" class="col-sm-2">
                        Delay Causes</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDelayCauses" Width="100%" runat="server" CssClass="form-control select2" DataTextField="DELAYCAUSESNAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                         Responsible Department</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlResponsibleDepartment" Width="100%" runat="server" CssClass="form-control select2" DataTextField="RESPONSIBLEDEPARTMENTNAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2"> Registration Number</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxVehicleRegNumber" Enabled="true" runat="server" placeholder="Registration Number" CssClass="form-control"></asp:TextBox>

                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                       Delay Time</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxTime" runat="server" placeholder="Enter time (e.g., 3.25)" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2">Workshop </label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlWorkWorkshopId" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                    </div>

                    <label for="inputEmail3" class="col-sm-2">
                        Remarks</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxRemarks" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
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
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <%--       <th>SL#</th>--%>
                                                    <th>Job ID</th>
                                                    <th>Delay Causes</th>
                                                    <th>Responsible Department</th>
                                                    <th>Time</th>
                                                    <th>Remarks</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("JOBID") %></td>
                                            <td><%#Eval("DELAYCAUSESNAME") %></td>
                                            <td><%#Eval("RESPONSIBLEDEPARTMENTNAME") %></td>
                                            <td><%#Eval("TIME") %></td>
                                            <td><%#Eval("REMARKS") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("JOBID")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" /></td>
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
    </script>
</asp:Content>

