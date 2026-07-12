<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobCardWithCo.aspx.cs" Inherits="Pages_VISM_JobCardWithCo" %>

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
                                <div class="form-group">

                                    <label class="col-sm-4">Vehicle</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxRecVeh" EnableViewState="True" Enabled="True" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxRecVeh_OnTextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxRecVeh"
                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <label class="col-sm-4">Model</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxModel" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <label class="col-sm-4">Customer</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxCustomer" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Location</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxLoc" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Company </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxCom" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Service Type </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxSerType" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Effective Date </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxEffectiveDate" Enabled="False" runat="server"  CssClass="form-control"></asp:TextBox>

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
                                        <asp:TextBox ID="tbxSendLoc" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">VISM Warehouse</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxSendWh" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Workshop </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxSendCom" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">CD No </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxCd" TextMode="MultiLine" Height="150" ReadOnly="True" runat="server" CssClass="form-control"></asp:TextBox>
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

                            <div class="col-sm-2">
                                <label>
                                    Total Bill
                                </label>
                                <asp:TextBox ID="tbxBillTotal" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>


                            </div>

                            <div class="col-sm-1">
                                <label>
                                    Discount(%)
                                </label>
                                <asp:TextBox ID="tbxBillDisc" runat="server" onkeyup="CalDisc();" Enabled="True" CssClass="form-control"></asp:TextBox>


                            </div>
                            <div class="col-sm-2">
                                <label>
                                    Discount Amount
                                </label>
                                <asp:TextBox ID="tbxBillDiscAmt" runat="server" ClientIDMode="Static" Enabled="False" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillDiscAmt" InitialValue="" ID="RequiredFieldValidator23" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-2">
                                <label>
                                    Net Bill
                                </label>
                                <asp:TextBox ID="tbxBillNetAmt" runat="server" ClientIDMode="Static" Enabled="False" onkeyup="TotalPart();" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxBillNetAmt" InitialValue="" ID="RequiredFieldValidator24" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-5">
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
                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-success" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-default" OnClientClick="this.form.reset();return false;" OnClick="btnReset_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button runat="server" ID="btnPrint" Text="Print Bill" CssClass="btn btn-success" OnClick="btnPrint_OnClick" CausesValidation="False" />
                    </div>

                </div>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Spare Parts List (CD)</div>
                    </div>
                    <div class="panel-body">

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
                                            <th>CD Qty</th>
                                            <th>CO Qty</th>
                                            <th>CD Total Amount</th>
                                            <th>CO Total Amount</th>
                                            <th>CD Remarks</th>
                                            <th>CO Remarks</th>

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
                                    <td><%#Eval("CdQty")%></td>
                                    <td><%#Eval("CoQty")%></td>
                                    <td><%#Eval("CdTotalAmount")%></td>
                                    <td><%#Eval("CoTotalAmount")%></td>
                                    <td><%#Eval("CdRemarks")%></td>
                                    <td><%#Eval("CoRemarks")%></td>

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
                                                <td></td>
                                                <td></td>
                                                <td>Total</td>
                                                <td><%#partCdTot.ToString("F2")%></td>
                                                <td><%#partCoTot.ToString("F2")%></td>
                                                <td></td>
                                                <td></td>



                                            </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Service Charge List</div>
                    </div>
                    <div class="panel-body">

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
                                            <th>Total Amount</th>
                                            <th>Remarks</th>

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
                                    <td><%#Eval("ServiceAmount")%></td>
                                    <td><%#Eval("ServiceRemarks")%></td>

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
                                                <td>Total: </td>
                                                <td><%#serAmtTot.ToString("F2")%></td>
                                                <td></td>


                                            </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>


            </div>

        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="rptMain" />--%>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">

    <script>
        function CalDisc() {
            var totAmt = document.getElementById('<%= tbxBillTotal.ClientID %>').value;
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
</asp:Content>

