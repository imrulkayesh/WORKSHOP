<%@ Page Title="Booking" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="Pages_VISM_Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                        Vehicle</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxVehicle" runat="server" placeholder="Enter Vehicle" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Workshop</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlworkshopid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshopid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Service Category</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCategory" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CATNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlcategoryid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Booking Date</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxBookingDate" runat="server" placeholder="Enter Booking Date" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="caleff" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxBookingDate"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Total</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxTotal" runat="server" placeholder="Enter Total" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Expiredate</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxExpireDate" runat="server" placeholder="Enter Expire Date" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxExpireDate"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Discount</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxDiscount" runat="server" placeholder="Enter Discount" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Booking Money</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxBookingMoney" runat="server" placeholder="Enter Booking Money" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Is Confirmed</label>
                    <div class="col-sm-4">
                        <asp:CheckBox ID="chkConfirm" runat="server" Text="Yes" />
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Currency</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCurrency" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CURRENCYCODE" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Remarks</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="tbxremark" TextMode="MultiLine" runat="server" placeholder="Enter Remark" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-header">
                                Service Charges
                            </div>
                            <div class="box-body">
                                <asp:Repeater ID="rptCharges" runat="server">
                                    <HeaderTemplate>
                                        <div style="max-height: 200px; overflow-y: auto">
                                            <table id="example1" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Service</th>
                                                        <th>Quantity</th>
                                                        <th>Amount</th>
                                                        <th>Remark</th>
                                                        <th>
                                                            <asp:CheckBox ID="chkHeader" runat="server" Text="All" /></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("CHARGENAME") %>
                                                <asp:HiddenField ID="hdnChargeId" runat="server" Value='<%#Eval("ID") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxChargeQty" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("BOOKINGID").ToString())? "1" : Eval("QUANTITY") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxChargeAmount" CssClass="form-control" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxChargeRemarks" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("BOOKINGID").ToString())? "" : Eval("REMARKS") %>'></asp:TextBox>
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
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-header">
                                Parts List
                            </div>
                            <div class="box-body">
                                <asp:Repeater ID="rptParts" runat="server">
                                    <HeaderTemplate>
                                        <div style="max-height: 200px; overflow-y: auto">
                                            <table id="example2" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Item</th>
                                                        <th>Quantity</th>
                                                        <th>Amount</th>
                                                        <th>Remark</th>
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
                                                <asp:TextBox ID="tbxPartsQty" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("BOOKINGID").ToString())? "1" : Eval("QUANTITY") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPartsAmount" CssClass="form-control" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxPartsRemarks" CssClass="form-control" runat="server" Text='<%# string.IsNullOrEmpty(Eval("BOOKINGID").ToString())? "" : Eval("REMARKS") %>'></asp:TextBox>
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
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group text-center">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnCalculate" runat="server" ClientIDMode="Static" Text="Calculate" CssClass="btn btn-primary" OnClick="btnCalculate_Click" />
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
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
                                        <table id="example3" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>BookingID</th>
                                                    <th>Customer</th>
                                                    <th>Mobile</th>
                                                    <th>Vehicle</th>
                                                    <th>Date</th>
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
                                            <td><%#Eval("BOOKINGDATE","{0:dd/MM/yyyy}") %></td>
                                            <td><%#Eval("REMARKS") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%></td>
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
            $("#example2 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example2 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example2 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example2 [id*=chkrow]").click(function () {
                if ($("#example2 [id*=chkrow]").length == $("#example2 [id*=chkrow]:checked").length) {
                    $("#example2 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example2 [id*=chkHeader]").prop("checked", false);
                }
            });
            $("#example4 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example4 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example4 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example4 [id*=chkrow]").click(function () {
                if ($("#example4 [id*=chkrow]").length == $("#example4 [id*=chkrow]:checked").length) {
                    $("#example4 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example4 [id*=chkHeader]").prop("checked", false);
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
