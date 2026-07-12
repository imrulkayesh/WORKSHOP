<%@ Page Title="Inspection Item" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="InsIteams.aspx.cs" Inherits="Pages_VISM_InsIteams" %>

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
                        Service Category</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCategory" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CATNAME" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Inspection Item</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxitem" runat="server" placeholder="Enter Item Name" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Inspection Type</label>
                    <div class="col-sm-4">
                        <asp:RadioButtonList ID="chkInspectinType" OnSelectedIndexChanged="chkInspectinType_SelectedIndexChanged" RepeatColumns="2" AutoPostBack="true" Width="100%" runat="server">
                            <asp:ListItem Value="0" Selected="True">PI</asp:ListItem>
                            <asp:ListItem Value="1">PDI</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Order</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxcorder" runat="server" placeholder="Enter Corder" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Labor Rate</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxLaborRate" runat="server" placeholder="Enter Labor Rate" CssClass="form-control"></asp:TextBox>                        
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Labor Quantity</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxLaborQuantity" runat="server" placeholder="Enter Labor Quantity" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Working Hour</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxWorkingHour" runat="server" placeholder="Enter Labor Working Hour" CssClass="form-control"></asp:TextBox>                        
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
                                                    <th>SL#</th>
                                                    <th>Inspection Item</th>
                                                    <th>Labor Quantity</th>
                                                    <th>Labor Rate</th>
                                                    <th>Labor Hour</th>
                                                    <th>Inspection Type</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("CORDER") %></td>
                                            <td><%#Eval("ITEMNAME") %></td>
                                            <td><%#Eval("LABORQTY") %></td>
                                            <td><%#Eval("LABORRATE") %></td>
                                            <td><%#Eval("WORKINGHOUR") %></td>
                                            <td><%#Eval("INSPECTIONTYPE").ToString().Replace("0","PI").Replace("1","PDI") %></td>
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
</asp:Content>

