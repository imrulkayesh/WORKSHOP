<%@ Page Title="Master SpareParts" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="MasterSpareParts.aspx.cs" Inherits="Pages_VISM_MasterSpareParts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                        Country</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCountry" Width="100%" runat="server" CssClass="form-control select2" DataTextField="COUNTRYNAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Workshop</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlWorkshop" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOPNAME" AutoPostBack="True" OnTextChanged="ddlLine_OnTextChanged" DataValueField="ID"></asp:DropDownList>
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
                                                        <th>SpareParts</th>
                                                        <th>
                                                            <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("LABELNAME") %><asp:HiddenField ID="hdSparePartsId" runat="server" Value='<%#Eval("ID") %>' />
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

                <div class="form-group text-right">
                    <div class="col-sm-offset-1 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_OnClick" />
                        <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                            OnClick="btnEdit_OnClick" />
                        <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_OnClick" />
                    </div>
                </div>
                <hr />

                <div class="panel panel-primary">
                    <div class="panel-heading">
                        SpareParts List
                    </div>
                    <div class="panel-body">
                        <asp:Repeater ID="rpttbList" runat="server">
                            <HeaderTemplate>
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Country</th>
                                            <th>Workshop</th>
                                            <th>SpareParts</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("CENTERNAME") %></td>
                                    <td><%#Eval("LINENAME") %></td>
                                    <td><%#Eval("LINENAME") %></td>
                                    <td> <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" /></td>
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

