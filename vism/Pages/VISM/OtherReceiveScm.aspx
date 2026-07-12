<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="OtherReceiveScm.aspx.cs" Inherits="Pages_VISM_OtherReceiveScm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
   <asp:UpdatePanel runat="server">
       <ContentTemplate>
        <div class="form-horizontal">
            <asp:HiddenField runat="server" ID="hdnOiLocCode"/>
                        <asp:HiddenField runat="server" ID="hdnOiWhCode"/>
            <asp:HiddenField runat="server" ID="hdnOiComCode"/>
                        <asp:HiddenField runat="server" ID="hdnOiUser"/>
            <asp:HiddenField runat="server" ID="hdnOiRemark"/>
            <asp:HiddenField runat="server" ID="hdnOrCompnay"/>
            

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
                        <asp:DropDownList runat="server" ID="ddlToLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlToLoc_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlToLoc" InitialValue="" ID="RequiredFieldValidator4"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                            </div>
 <div class="form-group">
                <label class="col-sm-4">VISM Warehouse</label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" Enabled="False" ID="ddlToWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlToWh_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlToWh" InitialValue="" ID="RequiredFieldValidator5"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    </div>
                        <div class="form-group">
                <label class="col-sm-4">Company/Workshop </label>
                    <div class="col-sm-8">
                       <asp:TextBox runat="server" Enabled="False" ID="tbxToCompany" CssClass="form-control"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxToCompany" InitialValue="" ID="RequiredFieldValidator7"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                    </div>
            <%--<div class="form-group">
                    
                     <label class="col-sm-4">Receive Date</label>
                    <div class="col-sm-8">
                       <asp:TextBox ID="tbxRecDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator6"  ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxRecDate" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                </div>--%>
                        <div class="form-group">
                <label class="col-sm-4">Remark</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="tbxToRemark" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    </div>
                
        
            </div>
                </div>
                </div>
            
            <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading" style="height: 35px">
                    <div class="panel-title" style="text-align: center; margin-top: -7px">Sender Details (SCM)</div>
                    </div>                     
                    <div class="panel-body">
                        <div class="form-group">
                    
                     <label class="col-sm-4">OI Id</label>
                    <div class="col-sm-8">
                        <asp:DropDownList runat="server" Enabled="False" ID="ddlOiId" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlOiId_OnSelectedIndexChanged"/>
                            <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator3"  ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlOiId" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                            </div>
                        <div class="form-group">
                    
                     <label class="col-sm-4">Location</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" Enabled="False" ID="tbxFromLoc" CssClass="form-control"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxFromLoc" InitialValue="" ID="RequiredFieldValidator1"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        
                    </div>
                            </div>
 <div class="form-group">
                <label class="col-sm-4">SCM Warehouse</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" Enabled="False" ID="tbxFromWh" CssClass="form-control"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxFromWh" InitialValue="" ID="RequiredFieldValidator2"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        
                    </div>
                    </div>
                        <div class="form-group">
                <label class="col-sm-4">Company </label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" Enabled="False" ID="tbxFromCompany" CssClass="form-control"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxFromCompany" InitialValue="" ID="RequiredFieldValidator6"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        
                    </div>
                    </div>
            <%--<div class="form-group">
                    
                     <label class="col-sm-4">Receive Date</label>
                    <div class="col-sm-8">
                       <asp:TextBox ID="tbxRecDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator6"  ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxRecDate" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    </div>
                </div>--%>
                        <div class="form-group">
                <label class="col-sm-4">Sent Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="tbxFromDate" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    </div>
                        <%--<div class="form-group">
                <label class="col-sm-4">Remark</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    </div>--%>
                
        
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
                    <%--<div class="form-group">
                        <div class="col-sm-3">
                    <label>
                      Expense type
                    </label>
                    <asp:DropDownList runat="server" ID="ddlExpense" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlExpense_OnSelectedIndexChanged"/>
                   
                </div>
                        <div class="col-sm-3">
                    <label>
                      Description
                    </label>
                    <asp:TextBox ID="tbxDesc" runat="server"  CssClass="form-control"></asp:TextBox>
                   
                </div>
                    <div class="col-sm-1">
                    <label>
                      Unit
                    </label>
                    <asp:TextBox ID="tbxUnit" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                   
                </div>
                        <div class="col-sm-1">
                    <label>
                      Unit Price
                    </label>
                    <asp:TextBox ID="tbxUnitPrice" runat="server" onkeyup="Total()"  CssClass="form-control"></asp:TextBox>
                   
                </div>
                        <div class="col-sm-1">
                    <label>
                      Qty
                    </label>
                    <asp:TextBox ID="tbxQty" runat="server" onkeyup="Total()"  CssClass="form-control"></asp:TextBox>
                   
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
                    <asp:Button ID="tbnAdd" Text="Add" runat="server"  CssClass="btn btn-primary" CausesValidation="False" OnClick="tbnAdd_OnClick"></asp:Button>
                   
                </div>
                     
                </div>--%>
         
                    
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
                                                        <th>Unit</th>
                                                        <th>Rate</th>
                                                        <th>Qty</th>
                                                        <th>Total Amount</th>
                                                        <th>Remark</th>
                                                        <%--<th>Action</th>--%>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Container.ItemIndex+1 %></td>
                                                <td><%#Eval("Category Code")%></td>
                                                <td><%#Eval("Category Name")%></td>
                                                <td><%#Eval("Item Code")%></td>
                                                <td><%#Eval("Item Name")%></td>
                                                <td><%#Eval("Unit Code")%></td>
                                                <td><%#Eval("Unit")%></td>
                                                <td><%#Eval("Rate")%></td>
                                                <td><%#Eval("Qty")%></td>
                                                <td><%#Eval("Total Amount")%></td>
                                                <td><%#Eval("Remark")%></td>
                                                <%--<td style="width: 1px; text-align: center">
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("OID")%>' OnCommand="imgbtnDelete_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" CausesValidation="False" /></td>--%>
                                                
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
</asp:Content>

