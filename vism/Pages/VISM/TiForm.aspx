<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="TiForm.aspx.cs" Inherits="Pages_VISM_TiForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
   <asp:UpdatePanel runat="server">
       <ContentTemplate>
        <div class="form-horizontal">
            <asp:HiddenField runat="server" ID="hdnUnitName"/>

            <div class="form-group">
            
            
            
                <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading" style="height: 35px">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Receiver Details (VISM)</div>
                    </div>                     
                    <div class="panel-body">
                        
                      <div class="form-group">
                     <label class="col-sm-4">Location</label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" Enabled="True" ID="ddlRecvLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlRecvLoc_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvLoc" InitialValue="" ID="RequiredFieldValidator4"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                            </div>
 <div class="form-group">
                <label class="col-sm-4">VISM Warehouse</label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlRecvWh_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvWh" InitialValue="" ID="RequiredFieldValidator5"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    </div>
                        <div class="form-group">
                <label class="col-sm-4">Company/Workshop </label>
                    <div class="col-sm-8">
                       <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvCom" InitialValue="" ID="RequiredFieldValidator7"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    </div>
                        <div class="form-group">
                <label class="col-sm-4">Remarks</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="tbxRecvRemarks" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
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
                <label class="col-sm-4">TO Id </label>
                    <div class="col-sm-8">
                       <asp:DropDownList runat="server" Enabled="False" ID="ddlToId" CssClass="form-control" DataValueField="ID" DataTextField="TEXT" Width="100%" OnSelectedIndexChanged="ddlToId_OnSelectedIndexChanged" AutoPostBack="True" ClientIDMode="Static"/>
                    </div>
                    </div>
                      <div class="form-group">
                     <label class="col-sm-4">Location</label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendLoc" InitialValue="" ID="RequiredFieldValidator1"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                            </div>
 <div class="form-group">
                <label class="col-sm-4">VISM Warehouse</label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendWh" InitialValue="" ID="RequiredFieldValidator2"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    </div>
                        <div class="form-group">
                <label class="col-sm-4">Company/Workshop </label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" Enabled="False" ID="ddlSendCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendCom" InitialValue="" ID="RequiredFieldValidator3"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    </div>
                        
                        
            <div class="form-group">
                <label class="col-sm-4">TI No: </label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server" ReadOnly="True" ID="tbxTrId" placeholder="Auto Generated Field" CssClass="form-control" BackColor="DarkCyan" ForeColor="White"/>

                    </div>
                    </div>
                
        
            </div>
                </div>
                </div>
            </div>
            
            <div class="panel-footer" style="height: 55px;">
                        <div class="col-sm-12 text-center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-success" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-default" OnClientClick="this.form.reset();return false;" OnClick="btnReset_OnClick"/>
                        </div>
                    
                </div>
            <hr/>
            <div class="panel panel-info">
                <div class="panel-heading" style="height: 35px">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Item List</div>
                </div>
                <div class="panel-body">
                    <div class="form-group" style="display: none">
                        <div class="col-sm-2">
                    <label>
                      Item Category
                    </label>
                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCategory" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator6"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   
                </div>
                        <div class="col-sm-4">
                    <label>
                      Item Name
                    </label>
                    <asp:DropDownList runat="server" Enabled="False" ID="ddlItemName" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemName" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator8"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   
                </div>
                    <div class="col-sm-1">
                    <label>
                      Unit
                    </label>
                    <asp:TextBox ID="tbxUnitCode" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxUnitCode" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator9"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   
                </div>
                        <div class="col-sm-1">
                    <label>
                      Rate
                    </label>
                    <asp:TextBox ID="tbxRate" runat="server" ClientIDMode="Static" Enabled="False"  CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxRate" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator10"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   
                </div>
                        <div class="col-sm-1">
                    <label>
                      Qty
                    </label>
                    <asp:TextBox ID="tbxQty" Enabled="False" runat="server" ClientIDMode="Static" onkeyup="Total();"  CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxQty" ValidationGroup="add" InitialValue="" ID="RequiredFieldValidator11"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator runat="server" ControlToValidate="tbxQty" ValidationGroup="add" ValidationExpression="^(\d*\.)?\d+$" ID="RequiredFieldValidator111"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="Invalid." ForeColor="Red" SetFocusOnError="True"></asp:RegularExpressionValidator>
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
                    <asp:Button ID="btnAdd" Text="Add" runat="server"  CssClass="btn btn-primary" ValidationGroup="add" OnClick="btnAdd_OnClick"></asp:Button>
                    <asp:Button ID="btnUpdate" Visible="False" Text="Update" runat="server"  CssClass="btn btn-warning" ValidationGroup="add" OnClick="btnUpdate_OnClick"></asp:Button>
                   
                </div>
                     
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
                                                        <th>Qty</th>
                                                        <th>Total Amount</th>
                                                        <%--<th>Action</th>--%>

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
                                                <td><%#Eval("Qty")%></td>
                                                <td><%#Eval("TotalAmount")%></td>
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
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
     <script>
        function Total() {
            var unitPrice = document.getElementById('<%= tbxRate.ClientID %>').value;
            var qty = document.getElementById('<%= tbxQty.ClientID %>').value;
            if (unitPrice == "")
                unitPrice = 0;
            if (qty == "")
                qty = 0;
            var result = (parseFloat(unitPrice) * parseFloat(qty)).toFixed(2);
            if (!isNaN(result)) {
                document.getElementById('<%= tbxAmt.ClientID %>').value = result;
                
            }
            else
            {
                document.getElementById('<%= tbxAmt.ClientID %>').value = "Invalid";
            }

        }
    </script>
</asp:Content>

