<%@ Page Title="Gate IN" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="GateIn.aspx.cs" Inherits="Pages_VISM_GateIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Vehicle details
                </div>
                <div class="panel-body">
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
                                Workshop</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlworkshopid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshopid_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Service Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlSerCatid" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlSerCatid_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Chassis No</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxchassisno" runat="server" placeholder="Enter Chassis No" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Reg No</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxregno" runat="server" placeholder="Enter Reg No" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Owner</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxowner" runat="server" placeholder="Enter Owner" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Owner Mobile</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxownermob" runat="server" placeholder="Enter Owner Mobile" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                Driver Name</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxDriverName" runat="server" placeholder="Enter Driver Name" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                Driver Mobile</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxDriverMobile" runat="server" placeholder="Enter Driver Mobile No" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="tbxDriverMobile" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                            </div>
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
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Staying vehicle list
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Type</th>
                                        <th>Chassis No</th>
                                        <th>Reg No</th>
                                        <th>Owner</th>
                                        <th>Owner Mob</th>
                                        <th>Driver Name</th>
                                        <th>Driver Mobile</th>
                                        <th>InDate</th>
                                        <th>OutDate</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("SERVICETYPE") %></td>
                                <td><%#Eval("CHASSISNO") %></td>
                                <td><%#Eval("REGNO") %></td>
                                <td><%#Eval("OWNERNAME") %></td>
                                <td><%#Eval("OWNERMOBILE") %></td>
                                <td><%#Eval("DRIVERNAME") %></td>
                                <td><%#Eval("DRIVERMOBILE") %></td>
                                <td><%#Eval("INDATE") %></td>
                                <td><%#Eval("OUTDATE") %></td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

