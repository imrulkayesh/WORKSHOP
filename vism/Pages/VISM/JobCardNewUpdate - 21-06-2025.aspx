<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobCardNewUpdate.aspx.cs" Inherits="Pages_VISM_JobCardNewUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:HiddenField runat="server" ID="hdnUnitName" />

                <div class="form-group">
                    <div class="col-md-6">
                        <div class="panel panel-info">
                            <div class="panel-heading" style="height: 35px">
                                <div class="panel-title" style="text-align: center; margin-top: -7px">Receiver Details (VISM)</div>
                            </div>
                            <div class="panel-body">
                                <div class="form-group" style="display: none;">

                                    <label class="col-sm-4">Pre-Inspection Id</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxPIID" Enabled="false" runat="server" placeholder="Enter Pre Inspection ID" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <label class="col-sm-4">Vehicle</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxRecVeh" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <label class="col-sm-4">Model</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxModel" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Location</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvLoc" InitialValue="" ID="RequiredFieldValidator5" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Company </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvCom" InitialValue="" ID="RequiredFieldValidator7" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Service Type </label>
                                    <div class="col-sm-8">
                                         <%-- <asp:DropDownList ID="ddlServiceType" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlServiceType" Width="100%" runat="server" CssClass="form-control" DataTextField="TEXT" DataValueField="ID"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlServiceType" InitialValue="" ID="RequiredFieldValidator20" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Effective Date </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxEffectiveDate" runat="server" placeholder="Enter Effective Date" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="caleff" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxEffectiveDate"></ajaxToolkit:CalendarExtender>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="panel panel-info">
                            <div class="panel-heading" style="height: 35px">
                                <div class="panel-title" style="text-align: center; margin-top: -7px">Sender Details (VISM)</div>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">

                                    <label class="col-sm-4">Location</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendLoc" InitialValue="" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">VISM Warehouse</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendWh" InitialValue="" ID="RequiredFieldValidator2" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Workshop </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendCom" InitialValue="" ID="RequiredFieldValidator3" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">CD Id </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" Enabled="False" ID="tbxCdId" placeholder="Auto Generated Field" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Group</label>
                                    <div class="col-sm-8">
                                        
                                        <asp:DropDownList runat="server" Enabled="True" ID="ddlgroup" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlgroup" InitialValue="" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label class="col-sm-4">Remarks </label>
                                    <div class="col-sm-8">
                                       <asp:TextBox ID="tbxRecRemarks" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Bill Information</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">

                            <div class="col-sm-1">
                                <label>
                                    Labor Bill
                                </label>
                                <asp:TextBox ID="tbxBillActual" Width="80" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillActual" InitialValue="" ID="RequiredFieldValidator21" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Parts Bill
                                </label>
                                <asp:TextBox ID="tbxBillMarginAmtTot" Width="80" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillMarginAmtTot" InitialValue="" ID="RequiredFieldValidator27" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Margin(%)
                                </label>
                                <asp:TextBox ID="tbxBillMargin" runat="server" onkeyup="CalMargin();" Enabled="True" CssClass="form-control"></asp:TextBox>
                                <%--   <asp:TextBox ID="TextBox1" runat="server" onkeyup="CalDisc();" Enabled="True" CssClass="form-control"></asp:TextBox>--%>
                                <%--   <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillMargin" InitialValue="" ID="RequiredFieldValidator25"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="col-sm-1">
                                <label>
                                    Mar. Amt
                                </label>
                                <asp:TextBox ID="tbxBillMarginAmt" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <%--       <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillMarginAmt" InitialValue="" ID="RequiredFieldValidator26" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="col-sm-1">
                                <label>
                                    Total Bill
                                </label>
                                <asp:TextBox ID="tbxTotalBill" Width="80" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxTotalBill" InitialValue="" ID="RequiredFieldValidator14" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                            <div class="col-sm-1">
                                <label>
                                    Discount(%)
                                </label>
                                <asp:TextBox ID="tbxBillDisc" runat="server" onkeyup="CalDisc();" Enabled="True" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillDisc" InitialValue="" ID="RequiredFieldValidator22" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Dis. Amt
                                </label>
                                <asp:TextBox ID="tbxBillDiscAmt" runat="server" ClientIDMode="Static" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillDiscAmt" InitialValue="" ID="RequiredFieldValidator23" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-2">
                                <label>
                                    Net Amount
                                </label>
                                <asp:TextBox ID="tbxBillNetAmt" runat="server" ClientIDMode="Static" Enabled="False" onkeyup="TotalPart();" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillNetAmt" InitialValue="" ID="RequiredFieldValidator24" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-2">
                                <label>
                                    Bill Remarks
                                </label>
                                <asp:TextBox ID="tbxBillRemarks" runat="server" ClientIDMode="Static" Enabled="True" onkeyup="TotalPart();" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </div>




                <div class="panel-footer" style="height: 55px;">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="btnSave" UseSubmitBehavior="false" OnClientClick="this.disabled=true;" Text="Save" CssClass="btn btn-success" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-default" OnClientClick="this.form.reset();return false;" OnClick="btnReset_OnClick" />
                        <asp:Button runat="server" ID="btnCdPrint" Text="CD Print" CssClass="btn btn-primary" OnClick="btnCdPrint_OnClick" Visible="False" CausesValidation="False" />

                    </div>

                </div>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Spare Parts List</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-2">
                                <label>
                                    Item Category
                                </label>
                                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCategory" ValidationGroup="addPart" InitialValue="" ID="RequiredFieldValidator6" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-3">
                                <label>
                                    Item Name
                                </label>
                                <asp:DropDownList runat="server" ID="ddlItemName" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_OnSelectedIndexChanged" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemName" ValidationGroup="addPart" InitialValue="" ID="RequiredFieldValidator8" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Unit
                                </label>
                                <asp:TextBox ID="tbxUnitCode" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxUnitCode" ValidationGroup="addPart" InitialValue="" ID="RequiredFieldValidator9" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Rate
                                </label>
                                <asp:TextBox ID="tbxRate" Width="80" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxRate" ValidationGroup="addPart" InitialValue="" ID="RequiredFieldValidator10" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Margin
                                </label>
                                <asp:TextBox ID="tbxMarginPart" runat="server" Enabled="True" onkeyup="TotalPart();" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxRate" ValidationGroup="addPart" InitialValue="" ID="RequiredFieldValidator16" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Mar.Rate
                                </label>
                                <asp:TextBox ID="tbxMarginRatePart" Width="80" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxMarginRatePart" ValidationGroup="addPart" InitialValue="" ID="RequiredFieldValidator17" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Qty
                                </label>
                                <asp:TextBox ID="tbxQtyPart" runat="server" onkeyup="TotalPart();" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxQtyPart" ValidationGroup="addPart" InitialValue="" ID="RequiredFieldValidator11" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="tbxQtyPart" ValidationGroup="addPart" ValidationExpression="^(\d*\.)?\d+$" ID="RequiredFieldValidator111" ClientIDMode="Static" Display="Dynamic" ErrorMessage="Invalid." ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Amount
                                </label>
                                <asp:TextBox ID="tbxAmtPart" Width="80" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-sm-1">
                              <%--  <label>
                                    Action
                                </label>--%>
                                <asp:Button ID="btnAdd" Text="Add" Visible="False" runat="server" CssClass="btn btn-primary" ValidationGroup="addPart" OnClick="btnAdd_OnClick"></asp:Button>
                                <asp:Button ID="btnUpdate" Visible="False" Text="Update" runat="server" CssClass="btn btn-warning" ValidationGroup="addPart" OnClick="btnUpdate_OnClick"></asp:Button>

                            </div>

                        </div>


                        <asp:Repeater ID="rptDetailPart" runat="server" OnItemDataBound="rptDetailPart_OnItemDataBound">
                            <HeaderTemplate>
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL#</th>
                                            <th style="display: none">Category Code</th>
                                            <th>Category Name</th>
                                            <th>Item Code</th>
                                            <th>Item Name</th>
                                            <th style="display: none">Unit Code</th>
                                            <th>Unit Name</th>
                                            <th>Rate</th>
                                            <th>Margin(%)</th>
                                            <th>Margin Rate</th>
                                            <th>Qty</th>
                                            <th>Actual Amount</th>
                                            <th>Margin Amount</th>
                                            <th>Total Amount</th>
                                            <th>Remarks</th>
                                <%--            <th>Edit</th>
                                            <th>Delete</th>--%>

                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td style="display: none"><%#Eval("CategoryCode")%></td>
                                    <td><%#Eval("CategoryName")%></td>
                                    <td><%#Eval("ItemCode")%></td>
                                    <td><%#Eval("ItemName")%></td>
                                    <td style="display: none"><%#Eval("UnitCode")%></td>
                                    <td><%#Eval("UnitName")%></td>
                                    <td><%#Eval("Rate")%></td>
                                    <td><%#Eval("Margin")%></td>
                                    <td><%#Eval("MarginRate")%></td>
                                    <td><%#Eval("Qty")%></td>
                                    <td><%#Eval("ActualAmount")%></td>
                                    <td><%#Eval("MarginAmount")%></td>
                                    <td><%#Eval("TotalAmount")%></td>
                                    <td><%#Eval("Remarks")%></td>
                                 <%--   <td>
                                        <asp:ImageButton ID="imgbtnEdit"  runat="server" CommandArgument='<%# Eval("ItemCode")%>' OnCommand="imgbtnEdit_OnCommand" ImageUrl="../../Content/Images/edit1.png" ToolTip="Delete" CausesValidation="False" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("ItemCode")%>' OnCommand="imgbtnDelete_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure?')" />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                            <tr style="font-weight: bold">

                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>Total</td>
                                                <td><%#partQtyTot.ToString("F0")%></td>
                                                <td><%#partActTot.ToString("F2")%></td>
                                                <td><%#partMarginTot.ToString("F2")%></td>
                                                <td><%#partAmtTot.ToString("F2")%></td>
                                                <td></td>
                                                <td></td>
                                           <%--     <td></td>--%>



                                            </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
                <hr />
                <div class="panel panel-info" style="display:none;">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Service Charge List</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">

                            <div class="col-sm-3">
                                <label>
                                    Service Name
                                </label>
                                <asp:DropDownList runat="server" ID="ddlService" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlService_OnSelectedIndexChanged" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlService" ValidationGroup="addSer" InitialValue="" ID="RequiredFieldValidator12" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Charge
                                </label>
                                <asp:TextBox ID="tbxSerChar" runat="server"  CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxSerChar" ValidationGroup="addSer" InitialValue="" ID="RequiredFieldValidator13" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Margin
                                </label>
                                <asp:TextBox ID="tbxMarginSer" runat="server" Enabled="True" onkeyup="TotalSer();" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxSerChar" ValidationGroup="addSer" InitialValue="" ID="RequiredFieldValidator18" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Mar. Rate
                                </label>
                                <asp:TextBox ID="tbxMarginRateSer" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxSerChar" ValidationGroup="addSer" InitialValue="" ID="RequiredFieldValidator19" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Qty
                                </label>
                                <asp:TextBox ID="tbxSerQty" runat="server" onkeyup="TotalSer();" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxSerQty" ValidationGroup="addSer" InitialValue="" ID="RequiredFieldValidator15" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="tbxSerQty" ValidationGroup="addSer" ValidationExpression="^(\d*\.)?\d+$" ID="RegularExpressionValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="Invalid." ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-2">
                                <label>
                                    Amount
                                </label>
                                <asp:TextBox ID="tbxSerAmt" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-sm-2">
                                <label>
                                    Remarks
                                </label>
                                <asp:TextBox ID="tbxSerRemarks" runat="server" Enabled="True" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Action
                                </label>
                                <asp:Button ID="btnSerAdd" Text="Add" runat="server" CssClass="btn btn-primary" ValidationGroup="addSer" OnClick="btnSerAdd_OnClick"></asp:Button>
                                <asp:Button ID="btnSerUpdate" Visible="False" Text="Update" runat="server" CssClass="btn btn-warning" ValidationGroup="addSer" OnClick="btnSerUpdate_OnClick"></asp:Button>

                            </div>

                        </div>


                        <asp:Repeater ID="rptDetailSer" runat="server" OnItemDataBound="rptDetailSer_OnItemDataBound">
                            <HeaderTemplate>
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL#</th>
                                            <th style="display: none;">Service Code</th>
                                            <th>Service Name</th>
                                            <th>Charge</th>
                                            <th>Margin</th>
                                            <th>Mar. Rate</th>
                                            <th>Qty</th>
                                            <th>Actual Amount</th>
                                            <th>Margin Amount</th>
                                            <th>Total Amount</th>
                                            <th>Remarks</th>
                                            <th>Edit</th>
                                            <th>Delete</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td style="display: none;"><%#Eval("ServiceCode")%></td>
                                    <td><%#Eval("ServiceName")%></td>
                                    <td><%#Eval("ServiceCharge")%></td>
                                    <td><%#Eval("ServiceChargeMargin")%></td>
                                    <td><%#Eval("ServiceChargeMarginRate")%></td>
                                    <td><%#Eval("SerQty")%></td>
                                    <td><%#Eval("SerActualAmount")%></td>
                                    <td><%#Eval("SerMarginAmount")%></td>
                                    <td><%#Eval("ServiceAmount")%></td>
                                    <td><%#Eval("ServiceRemarks")%></td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnEditSer" runat="server" CommandArgument='<%# Eval("ServiceCode")%>' OnCommand="imgbtnEditSer_OnCommand" ImageUrl="../../Content/Images/edit1.png" ToolTip="Delete" CausesValidation="False" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnDeleteSer" runat="server" CommandArgument='<%# Eval("ServiceCode")%>' OnCommand="imgbtnDeleteSer_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure?')" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                            <tr style="font-weight: bold">


                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>Total </td>
                                                <td><%#serQtyTot.ToString("F0")%></td>
                                                <td><%#serActTot.ToString("F2")%></td>
                                                <td><%#serMarginTot.ToString("F2")%></td>
                                                <td><%#serAmtTot.ToString("F2")%></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
                <%--Labor Service Charge--%>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Labor Service Charge List</div>
                    </div>
                    <div class="panel-body">
                       
                        <asp:Repeater ID="rptDetailProblemServiceCharge" runat="server" OnItemDataBound="rptDetailProblemServiceCharge_OnItemDataBound">
                            <HeaderTemplate>
                                <table id="example12" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL#</th>
                                            <th style="display: none;">Service ID</th>
                                            <th>Service Name</th>
                                            <th>Qty</th>
                                            <th>Rate</th>
                                            <th>Labor Quantity</th>
                                            <th>Working Hour</th>
                                            <th>Total Amount</th>
                                            <th>Remarks</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td style="display: none;"><%#Eval("ITEMID")%></td>
                                    <td><%#Eval("ITEMNAME")%></td>
                                    <td><%#Eval("QUANTITY")%></td>
                                    <td><%#Eval("LABORRATE")%></td>
                                    <td><%#Eval("LABORQTY")%></td>
                                    <td><%#Eval("WORKINGHOUR")%></td>
                                    <td><%#Eval("LABORCHARGE")%></td>
                                    <td><%#Eval("REMARKS")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                            <tr style="font-weight: bold">
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>Total</td>
                                                <td><%#ProblemserAmtTot.ToString("F2")%></td>
                                                <td></td>
                                            </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
                <%--Labor Service Charge End--%>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Technician List</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">

                            <div class="col-sm-5">
                                <label>
                                    Technician
                                </label>
                                <asp:DropDownList runat="server" ID="ddlTech" CssClass="form-control select2" DataValueField="id" DataTextField="text" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTech" ValidationGroup="addTech" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-6">
                                <label>
                                    Remarks
                                </label>
                                <asp:TextBox ID="tbxRemarkTech" runat="server" Enabled="True" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Action
                                </label>
                                <asp:Button ID="btnAddTech" Text="Add" runat="server" CssClass="btn btn-primary" ValidationGroup="addTech" OnClick="btnAddTech_OnClick"></asp:Button>
                                <asp:Button ID="btnUpdateTech" Visible="False" Text="Update" runat="server" CssClass="btn btn-warning" ValidationGroup="addTech" OnClick="btnUpdateTech_OnClick"></asp:Button>

                            </div>

                        </div>


                        <asp:Repeater ID="rptDetailTech" runat="server">
                            <HeaderTemplate>
                                <table id="example3" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL#</th>
                                            <th>Staff Id</th>
                                            <th>Technician Name</th>
                                            <th>Group</th>
                                            <th>Remarks</th>
                                            <th>Edit</th>
                                            <th>Delete</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td><%#Eval("StaffId")%></td>
                                    <td><%#Eval("TechnicianName")%></td>
                                    <td><%#Eval("Group")%></td>
                                    <td><%#Eval("TechRemark")%></td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnEditTech" runat="server" CommandArgument='<%# Eval("StaffId")%>' OnCommand="imgbtnEditTech_OnCommand" ImageUrl="../../Content/Images/edit1.png" ToolTip="Delete" CausesValidation="False" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnDeleteTech" runat="server" CommandArgument='<%# Eval("StaffId")%>' OnCommand="imgbtnDeleteTech_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure?')" />
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
                <hr />
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Jobcard Search</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxVehileForJobIns" runat="server" placeholder="Enter Vehicle ID" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="tbxVehileForJobIns"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                    <asp:Button ID="btnPreInsSearch" runat="server" ClientIDMode="Static" Text="Search" CssClass="btn btn-success"
                        OnClick="btnPreInsSearch_Click" CausesValidation="False" />

                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Job Card List</div>

                    </div>
                    <div class="panel-body" style="overflow: auto">
                        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_OnItemDataBound">
                            <HeaderTemplate>
                                <table id="example4" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
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
                                            <th>Edit</th>
                                            <th>PrintJob</th>
                                            <th style="display:none;">PrintBill</th>
                                            <th>PrintBill Update</th>
                                            <th>Job Close</th>
                                            <th style="display:none;">No Work</th>
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
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" CausesValidation="False" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPrint" runat="server" OnCommand="btnPrint_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/printer_icon.png" ToolTip="Print" CausesValidation="False" />
                                    </td>
                                    <td style="display:none;">
                                        <asp:ImageButton ID="btnBillPrint" runat="server" OnCommand="btnBillPrint_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print" CausesValidation="False" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnBillPrintUpdate" runat="server" OnCommand="btnBillPrintUpdate_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print Update" CausesValidation="False" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnClose" runat="server" OnCommand="btnClose_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/dltgrd.png" ToolTip="Close" CausesValidation="False" />
                                    </td>
                                    <td style="display:none;">
                                        <asp:ImageButton ID="btnNoWork" Visible="False" runat="server" OnCommand="btnNoWork_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/dustbin.png" ToolTip="No Work" CausesValidation="False" />
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

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rptMain" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script>
        function TotalPart() {
            var rate = document.getElementById('<%= tbxRate.ClientID %>').value;
            var margin = document.getElementById('<%= tbxMarginPart.ClientID %>').value;
            var qty = document.getElementById('<%= tbxQtyPart.ClientID %>').value;
            if (rate == "")
                rate = 0;
            if (margin == "")
                margin = 0;
            if (qty == "")
                qty = 0;
            var marRate = (parseFloat(rate) + parseFloat(rate) * parseFloat(margin) / 100).toFixed(2);
            var totAmt = (parseFloat(marRate) * parseFloat(qty)).toFixed(2);
            if (!isNaN(marRate) && !isNaN(totAmt)) {
                document.getElementById('<%= tbxMarginRatePart.ClientID %>').value = marRate;
                document.getElementById('<%= tbxAmtPart.ClientID %>').value = totAmt;
            }
            else {
                document.getElementById('<%= tbxMarginRatePart.ClientID %>').value = "Invalid";
                document.getElementById('<%= tbxAmtPart.ClientID %>').value = "Invalid";
            }

        }
    </script>
    <script>
        function TotalSer() {
            var rate = document.getElementById('<%= tbxSerChar.ClientID %>').value;
            var margin = document.getElementById('<%= tbxMarginSer.ClientID %>').value;
            var qty = document.getElementById('<%= tbxSerQty.ClientID %>').value;
            if (rate == "")
                rate = 0;
            if (margin == "")
                margin = 0;
            if (qty == "")
                qty = 0;
            var marRate = (parseFloat(rate) + parseFloat(rate) * parseFloat(margin) / 100).toFixed(2);
            var totAmt = (parseFloat(marRate) * parseFloat(qty)).toFixed(2);
            if (!isNaN(marRate) && !isNaN(totAmt)) {
                document.getElementById('<%= tbxMarginRateSer.ClientID %>').value = marRate;
                document.getElementById('<%= tbxSerAmt.ClientID %>').value = totAmt;
            }
            else {
                document.getElementById('<%= tbxMarginRateSer.ClientID %>').value = "Invalid";
                document.getElementById('<%= tbxSerAmt.ClientID %>').value = "Invalid";
            }

        }
    </script>
    <script>
        function CalDisc() {
            var totAmt = document.getElementById('<%= tbxTotalBill.ClientID %>').value;
            var dis = document.getElementById('<%= tbxBillDisc.ClientID %>').value;
            if (totAmt == "")
                totAmt = 0;
            if (dis == "")
                dis = 0;
            var disAmt = (parseFloat(totAmt) * parseFloat(dis) / 100).toFixed(2);
            var netAmt = (parseFloat(totAmt) - parseFloat(disAmt)).toFixed(2);
            if (!isNaN(disAmt) && !isNaN(netAmt)) {
                document.getElementById('<%= tbxBillDiscAmt.ClientID %>').value = disAmt;
                document.getElementById('<%= tbxBillNetAmt.ClientID %>').value = netAmt;

            }
            else {
                document.getElementById('<%= tbxBillDiscAmt.ClientID %>').value = "Invalid";
                document.getElementById('<%= tbxBillNetAmt.ClientID %>').value = "Invalid";
            }

        }
    </script>
    <script>
        function CalMargin() {
            var totPartsAmt = document.getElementById('<%= tbxBillActual.ClientID %>').value;
            var totAmt = document.getElementById('<%= tbxBillMarginAmtTot.ClientID %>').value;
            var dis = document.getElementById('<%= tbxBillMargin.ClientID %>').value;
            if (totAmt == "")
                totAmt = 0;
            if (dis == "")
                dis = 0;
            var disAmt = (parseFloat(totAmt) * parseFloat(dis) / 100).toFixed(2);
            var netAmt = (parseFloat(totPartsAmt) + parseFloat(totAmt) + parseFloat(disAmt)).toFixed(2);
            if (!isNaN(disAmt) && !isNaN(netAmt)) {
                document.getElementById('<%= tbxBillMarginAmt.ClientID %>').value = disAmt;
                    document.getElementById('<%= tbxTotalBill.ClientID %>').value = netAmt;
                    document.getElementById('<%= tbxBillNetAmt.ClientID %>').value = netAmt;

                }
                else {
                    document.getElementById('<%= tbxBillMarginAmt.ClientID %>').value = "Invalid";
                    document.getElementById('<%= tbxTotalBill.ClientID %>').value = "Invalid";
                    document.getElementById('<%= tbxBillNetAmt.ClientID %>').value = "Invalid";
                }

            }
    </script>
</asp:Content>

