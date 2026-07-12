<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="WorkOrder.aspx.cs" Inherits="Pages_VISM_WorkOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .btn-success {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:HiddenField runat="server" ID="hdnUnitName" />
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="panel panel-info">
                            <div class="panel-heading" style="height: 35px">
                                <div class="panel-title" style="text-align: center; margin-top: -7px">Work Order</div>
                            </div>
                            <div class="panel-body">
                              <div class="form-group">
                                <label class="col-sm-2">Work Order No: </label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" Enabled="False" ID="tbxWorkOrderNo" placeholder="Auto Generated Field" CssClass="form-control" />
                                </div>
                               
                              </div>
                               <div class="form-group">
                                   <label class="col-sm-2">Company/Workshop </label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList runat="server" Enabled="true" ID="ddlSendCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendCom" InitialValue="" ID="RequiredFieldValidator3" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                     <label for="inputEmail3" class="col-sm-2">Work Order Date </label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxWorkOrderDate" runat="server" placeholder="Enter Work Order Date" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxWorkOrderDate"></ajaxToolkit:CalendarExtender>
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
                                    <label for="inputEmail3" class="col-sm-2"> Registration Number</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxVehicleRegNumber" Enabled="false" runat="server" placeholder="Registration Number" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                 <div class="form-group">                                  
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2"> Model</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxModel" Enabled="true" runat="server" placeholder="Model Name" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <%-- <label for="inputEmail3" class="col-sm-2">User</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxCustomer" Enabled="true" runat="server" placeholder="User" CssClass="form-control"></asp:TextBox>
                                    </div>--%>
                                    <label for="inputEmail3" class="col-sm-2">
                                        Running KM
                                    </label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxMillage" runat="server" placeholder="Running KM" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb2" runat="server" TargetControlID="tbxMillage" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>                            
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        Driver Name</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxDriverName" runat="server" placeholder="Driver Name" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <label for="inputEmail3" class="col-sm-2">
                                        Driver Staff Id</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxStaffId" runat="server" placeholder="Driver Staff ID" CssClass="form-control"></asp:TextBox>
                                    </div>
                               </div>
                               <div class="form-group">

                                    <label for="inputEmail3" class="col-sm-2">
                                        Driver Mobile</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxDriverMobile" runat="server" placeholder="Driver Mobile No" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="tbxDriverMobile" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>

                                    
                              </div>

                                <hr />

                                <%--Customer Info--%>
                                 <div class="form-group">
                                    <label class="col-sm-2">Customer Name</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList runat="server" ID="ddlCustomer" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_OnSelectedIndexChanged" />
                                        <%--<asp:DropDownList runat="server" ID="ddlCustomer" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" />--%>
                                        <asp:TextBox runat="server" Visible="False" ID="tbxCustomer" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidatorDD" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlCustomer" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator runat="server" Enabled="False" InitialValue="" ID="RequiredFieldValidatorTX" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxCustomer" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-sm-1">
                                        <asp:CheckBox runat="server" ID="chkEditCustomer" Text="Edit" AutoPostBack="True" OnCheckedChanged="chkEditCustomer_OnCheckedChanged" />
                                    </div>

                                    <label class="col-sm-1">Mobile No.</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="tbxMobile" CssClass="form-control" Enabled="False" />

                                    </div>

                                 </div>
                                 <asp:Panel runat="server" ID="pnlCusInfo" Enabled="False">
                                    <div class="form-group">
                                        <label class="col-sm-2">Customer Address</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbxAddress" CssClass="form-control" />

                                        </div>
                                        <label class="col-sm-2">Customer Email</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbxEmail" CssClass="form-control" />

                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">CC1</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbxcc1" CssClass="form-control" />

                                        </div>
                                        <label class="col-sm-2">CC2</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbxcc2" CssClass="form-control" />
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">NID</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbxNid" CssClass="form-control" />

                                        </div>
                                    </div>
                                  </asp:Panel>
                                 <%--Customer Info End--%>
                              
                            </div>
                        </div>
                    </div>
     
                </div> 

                <div class="panel-footer" style="height: 55px;">
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="btnSave" UseSubmitBehavior="false" OnClientClick="this.disabled=true;" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <%--<asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                            <asp:Button runat="server" ID="btnReset" Text="New " CssClass="btn btn-primary" OnClick="btnReset_OnClick" CausesValidation="False" />
                    </div>
                </div>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Item List</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-5">
                                <label>
                                    Item Name
                                </label>
                                <asp:DropDownList runat="server" Enabled="False" ID="ddlItemName" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemName" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator8" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-5">
                                <label>
                                    Reason
                                </label>
                                <asp:TextBox ID="tbxReason" runat="server" Enabled="True" CssClass="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="tbxReason" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator9" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="col-sm-1">
                                <label>
                                    Action
                                </label>
                                <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="btn btn-primary" ValidationGroup="add" OnClick="btnAdd_OnClick" Visible="True"></asp:Button>
                                <asp:Button ID="btnUpdate" Visible="False" Text="Update" runat="server" CssClass="btn btn-warning" ValidationGroup="add" OnClick="btnUpdate_OnClick"></asp:Button>
                            </div>
                        </div>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>SL#</th>
                                            <th>Item Id</th>
                                            <th>Item Name</th>
                                            <th>Reason</th>
                                            <%--<th>Edit</th>--%>
                                            <th>Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td><%#Eval("ItemId")%></td>
                                    <td><%#Eval("ItemName")%></td>
                                    <td><%#Eval("REASON")%></td>
                                   <%-- <td>
                                        <asp:ImageButton ID="imgbtnEdit" runat="server" CommandArgument='<%# Eval("ItemId")%>' OnCommand="imgbtnEdit_OnCommand" ImageUrl="../../Content/Images/edit1.png" ToolTip="Edit" CausesValidation="False" />
                                    </td>--%>
                                    <td>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("ItemId")%>' OnCommand="imgbtnDelete_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure?')" />
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
                    <%--<label for="inputEmail3" class="col-sm-2">
                        Work Order Search</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="tbxTrNo" runat="server" placeholder="Start typing CD or Vehicle No or Name" CssClass="form-control"></asp:TextBox>
                        <div id="listPlc" runat="server" style="height: 100px; overflow-y: scroll;"></div>
                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchTr"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="tbxTrNo" CompletionListElementID="listPlc"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>--%>
                    <%--<asp:Button ID="btnSearchTr" runat="server" ClientIDMode="Static" Text="Search" CssClass="btn btn-success"
                    OnClick="btnSearchTr_OnClick" CausesValidation="False" />--%>
                </div>
            </div>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
   
</asp:Content>
