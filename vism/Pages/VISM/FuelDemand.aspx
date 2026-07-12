<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="FuelDemand.aspx.cs" Inherits="Pages_VISM_FuelDemand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <%--<div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>--%>
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Fuel Demand</div>
                    </div>
                    <div class="panel-body">

                        <div class="form-group">

                            <label class="col-sm-2">Vehicle Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" required="" ID="ddlVehicleType" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlVehicleType_OnSelectedIndexChanged" />
                            </div>
                            <label class="col-sm-2">Fuel Station</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" required="" ID="ddlPump" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2">Vehicle Reg No.</label>
                            <asp:Panel runat="server" ID="pnltbxvehreg" Visible="True">
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbxRecVeh" required="" runat="server" placeholder="Start typing vehicle reg no" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxRecVeh_OnTextChanged"></asp:TextBox>
                                    <div id="Div1" runat="server" style="max-height: 300px; overflow-y: auto; display: none"></div>
                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                        MinimumPrefixLength="1"
                                        CompletionListElementID="Div1"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="tbxRecVeh"
                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                                    </ajaxToolkit:AutoCompleteExtender>

                                </div>
                            </asp:Panel>

                            <label class="col-sm-2">Fuel Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" required="" ID="ddlFuelType" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlFuelType_OnSelectedIndexChanged" />
                                <%-- <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator14" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlFuelType" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>


                        <div class="form-group">

                            <label class="col-sm-2">Qty(Ltr.)</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxQty" required="" runat="server" ClientIDMode="Static" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>

                            <label class="col-sm-2">Rate</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxrate" runat="server" ClientIDMode="Static" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-sm-2">Remarks</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxRemarks" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-2" style="text-align: right">
                                <%--     <asp:Button runat="server" Text="Save" ID="btnSave" CssClass="btn btn-success" OnClick="btnSave_OnClick" />--%>
                                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                                    OnClick="btnSave_OnClick" />&emsp;

                                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false" formnovalidate=""
                                    OnClick="btnReset_Click" />
                            </div>
                        </div>

                    </div>
                </div>
                <hr />

                <div class="col-sm-12">
                    <div class="box">
                        <h4 class="panel-heading">Pending List</h4>
                        <div class="box-body">

                            <asp:Repeater ID="rpt" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>SL#</th>
                                                <th>Vehicle ID</th>
                                                <th>Fuel Name</th>
                                                <th>Pump</th>
                                                <th>Qty(Ltr.)</th>
                                                <th>Demand ID</th>
                                                <th>Remarks</th>
                                                <th>Delete</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Container.ItemIndex+1 %></td>
                                        <td><%#Eval("REGNO")%></td>
                                        <td><%#Eval("ITEMNAME")%></td>
                                        <td><%#Eval("PUMPNAME")%></td>
                                        <td><%#Eval("QTY")%></td>
                                        <td><%#Eval("TRANSACTIONID")%></td>
                                        <td><%#Eval("REMARKS")%></td>
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

                <div class="col-sm-12">
                    <div class="box">
                        <h4 class="panel-heading">Approved List</h4>
                        <div class="box-body">
                            <asp:Repeater ID="rptApprove" runat="server">
                                <HeaderTemplate>
                                    <div style="overflow: auto">
                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>SL#</th>
                                                    <th>Vehicle ID</th>
                                                    <th>Fuel Name</th>
                                                    <th>Pump</th>
                                                    <th>Demand Qty(Ltr.)</th>
                                                    <th>Approve Qty(Ltr.)</th>
                                                    <th>Approve Date</th>
                                                    <th>Demand ID</th>
                                                    <th>Remarks</th>
                                                    <th>Print</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Container.ItemIndex+1 %></td>
                                        <td><%#Eval("REGNO")%></td>
                                        <td><%#Eval("ITEMNAME")%></td>
                                        <td><%#Eval("PUMPNAME")%></td>
                                        <td><%#Eval("QTY")%></td>
                                        <td><%#Eval("AQTY")%></td>
                                        <td><%#Eval("A_DATE")%></td>
                                        <td><%#Eval("TRANSACTIONID")%></td>
                                        <td><%#Eval("REMARKS")%></td>
                                        <td>
                                            <asp:ImageButton ID="btnBillPrint" runat="server" OnCommand="btnBillPrint_Command" CommandArgument='<%# Eval("TRANSACTIONID")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print" formnovalidate="" />
                                            <%--                                            <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("ITEM_ID") %>' ImageUrl="img/edit.png" ToolTip="Edit" formnovalidate="" />--%>

                                            <%--<asp:ImageButton ID="btnSelect" runat="server" OnCommand="btnSelect_Command" CommandArgument='<%# Eval("MBDE_WSR")%>' ImageUrl="../Content/Images/Common/preview.png" ToolTip="Select" /></td>--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                        <tfoot>
                            <td></td>
                            <td></td>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tfoot>
                                    </table>
                        </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>


            </div>
        </ContentTemplate>
        <Triggers>
            <%--            <asp:PostBackTrigger ControlID="lnkDownloadExcel" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt) {

            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode == 46)
                return true
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>

