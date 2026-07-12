<%@ Page Title="Fixed Asset Entry" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="FAForm.aspx.cs" Inherits="Pages_VISM_FAForm" %>

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
                                        <asp:TextBox ID="tbxRecVeh" runat="server" placeholder="Start typing vehicle reg no" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxRecVeh_OnTextChanged"></asp:TextBox>
                                        <div id="Div1" runat="server" style="max-height: 300px; overflow-y: auto; display: none"></div>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                            MinimumPrefixLength="1"
                                            CompletionListElementID="Div1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxRecVeh"
                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>

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
                                    <label class="col-sm-4">Company/Workshop </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvCom" InitialValue="" ID="RequiredFieldValidator7" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Customer </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvCustomer" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvCustomer" InitialValue="" ID="RequiredFieldValidator6" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Model </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvModel" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvModel" InitialValue="" ID="RequiredFieldValidator8" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">CO Catagory: </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlCOcatagory" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Remarks</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbxRecRemarks" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
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
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlSendLoc_OnSelectedIndexChanged" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendLoc" InitialValue="" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">VISM Warehouse</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ClientIDMode="Static" Enabled="False" ID="ddlSendWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlSendWh_OnSelectedIndexChanged" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendWh" InitialValue="" ID="RequiredFieldValidator2" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">Company/Workshop </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendCom" InitialValue="" ID="RequiredFieldValidator3" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">FA No. </label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" Enabled="False" ID="tbxDrId" placeholder="Auto Generated Field" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4">Receiver </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlTech" CssClass="form-control select2" DataValueField="id" DataTextField="text" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTech" ValidationGroup="addTech" InitialValue="" ID="RequiredFieldValidator13" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel-footer" style="height: 55px;">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="btnSave" UseSubmitBehavior="false" OnClientClick="this.disabled=true;" Text="Save" CssClass="btn btn-success" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnReset" Text="New FA" CssClass="btn btn-danger" OnClientClick="this.form.reset();return true;" OnClick="btnReset_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnCdPrint" Text="FA Print" CssClass="btn btn-primary" OnClick="btnCdPrint_OnClick" CausesValidation="False" />

                    </div>

                </div>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Item List</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Panel runat="server" ID="pnlAddItem">
                                <asp:TextBox runat="server" Enabled="False" ID="tbxCategory" Visible="False" placeholder="Auto Generated Field" CssClass="form-control" />
                                <asp:TextBox runat="server" Enabled="False" ID="tbxCategoryName" Visible="False" placeholder="Auto Generated Field" CssClass="form-control" />
                                <%--<asp:DropDownList Visible="False" runat="server" ID="ddlCategory" CssClass="form-control" DataValueField="ID" DataTextField="TEXT"/>--%>
                                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCategory" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator6"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>


                                <div class="col-sm-4">
                                    <label>
                                        Item Name
                                    </label>
                                    <%--<asp:DropDownList runat="server" ID="ddlItemName" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemName" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator8"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                                    <asp:TextBox ID="tbxItem" runat="server" ClientIDMode="Static" placeholder="Start typing to search item" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxItem_OnTextChanged"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchItem"
                                        MinimumPrefixLength="1"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="tbxItem"
                                        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="true">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Unit
                                    </label>
                                    <asp:TextBox ID="tbxUnitCode" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxUnitCode" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator9" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Rate
                                    </label>
                                    <asp:TextBox ID="tbxRate" runat="server" ClientIDMode="Static" Enabled="False" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxRate" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator10" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Margin
                                    </label>
                                    <asp:TextBox ID="tbxMargin" runat="server" ClientIDMode="Static" Enabled="True" CssClass="form-control" onkeyup="TotalMar();"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxMargin" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Mar. Rate
                                    </label>
                                    <asp:TextBox ID="tbxMarginRate" runat="server" ClientIDMode="Static" Enabled="False" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxMarginRate" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator12" ClientIDMode="Static" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Qty
                                    </label>
                                    <asp:TextBox ID="tbxQty" runat="server" ClientIDMode="Static" onkeyup="Total();" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxQty" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator11" ClientIDMode="Static" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="tbxQty" ValidationGroup="add" ValidationExpression="^(\d*\.)?\d+$" ID="RequiredFieldValidator111" ClientIDMode="Static" Display="Dynamic" ErrorMessage="Invalid." ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-2">
                                    <label>
                                        Amount
                                    </label>
                                    <asp:TextBox ID="tbxAmt" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-sm-1">
                                    <label>
                                        Action
                                    </label>
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="btn btn-primary" ValidationGroup="add" OnClick="btnAdd_OnClick"></asp:Button>
                                    <asp:Button ID="btnUpdate" Visible="False" Text="Update" runat="server" CssClass="btn btn-warning" ValidationGroup="add" OnClick="btnUpdate_OnClick"></asp:Button>

                                </div>
                            </asp:Panel>
                        </div>


                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL#</th>
                                            <th>Category Code</th>
                                            <th>Category Name</th>
                                            <th>Item Code</th>
                                            <th>Item Name</th>
                                            <th>Unit Code</th>
                                            <th>Unit Name</th>
                                            <th>Rate</th>
                                            <th>Margin</th>
                                            <th>Margin Rate</th>
                                            <th>Qty</th>
                                            <th>Total Amount</th>
                                            <th>Edit</th>
                                            <th>Delete</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td><%#Eval("CategoryCode")%></td>
                                    <td><%#Eval("CategoryName")%></td>
                                    <td><%#Eval("ItemCode")%></td>
                                    <td><%#Eval("ItemName")%></td>
                                    <td><%#Eval("UnitCode")%></td>
                                    <td><%#Eval("UnitName")%></td>
                                    <td><%#Eval("Rate")%></td>
                                    <td><%#Eval("Margin")%></td>
                                    <td><%#Eval("MarginRate")%></td>
                                    <td><%#Eval("Qty")%></td>
                                    <td><%#Eval("TotalAmount")%></td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnEdit" runat="server" CommandArgument='<%# Eval("ItemCode")%>' OnCommand="imgbtnEdit_OnCommand" ImageUrl="../../Content/Images/edit1.png" ToolTip="Edit" CausesValidation="False" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("ItemCode")%>' OnCommand="imgbtnDelete_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure?')" />
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
                <div class="form-group" style="max-height: 500px;">
                    <label for="inputEmail3" class="col-sm-2">
                        CD Search</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="tbxTrNo" runat="server" placeholder="Start typing FD or Vehicle No or Name" CssClass="form-control"></asp:TextBox>
                        <div id="listPlc" runat="server" style="height: 100px; overflow-y: scroll;"></div>
                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchTr"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="tbxTrNo" CompletionListElementID="listPlc"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                    <asp:Button ID="btnSearchTr" runat="server" ClientIDMode="Static" Text="Search" CssClass="btn btn-success"
                        OnClick="btnSearchTr_OnClick" CausesValidation="False" />

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tbxItem" />
            <asp:AsyncPostBackTrigger ControlID="ddlSendWh" />
            <%--<asp:PostBackTrigger ControlID="ddlSendWh" />--%>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script>
        function Total() {
            var unitPrice = document.getElementById('<%= tbxMarginRate.ClientID %>').value;
            var qty = document.getElementById('<%= tbxQty.ClientID %>').value;
            if (unitPrice == "")
                unitPrice = 0;
            if (qty == "")
                qty = 0;
            var result = (parseFloat(unitPrice) * parseFloat(qty)).toFixed(2);
            if (!isNaN(result)) {
                document.getElementById('<%= tbxAmt.ClientID %>').value = result;

            }
            else {
                document.getElementById('<%= tbxAmt.ClientID %>').value = "Invalid";
            }

        }
    </script>
    <script>
        function TotalMar() {
            var unitPrice = document.getElementById('<%= tbxRate.ClientID %>').value;
            var mar = document.getElementById('<%= tbxMargin.ClientID %>').value;
            if (unitPrice == "")
                unitPrice = 0;
            if (mar == "")
                mar = 0;
            var result = (parseFloat(unitPrice) + parseFloat(unitPrice) * parseFloat(mar) / 100).toFixed(2);
            if (!isNaN(result)) {
                document.getElementById('<%= tbxMarginRate.ClientID %>').value = result;
                document.getElementById('<%= tbxAmt.ClientID %>').value = result;

            }
            else {
                document.getElementById('<%= tbxMarginRate.ClientID %>').value = "Invalid";
                document.getElementById('<%= tbxAmt.ClientID %>').value = "Invalid";
            }

        }
    </script>
</asp:Content>
