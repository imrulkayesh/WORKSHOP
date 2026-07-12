<%@ Page Title="Customer" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CustWrksp.aspx.cs" Inherits="Pages_VISM_CustWrksp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Customer Details
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
                                <asp:Label ID="Label18" runat="server" Text="Customer Name"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxCustomerName" runat="server" placeholder="Enter Customer Name" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Customer Name" ControlToValidate="tbxCustomerName">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label5" runat="server" Text="Mobile"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxMobile" runat="server" placeholder="Enter Mobile" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Mobile" ControlToValidate="tbxMobile">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label6" runat="server" Text="Phone"></asp:Label></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxPhone" runat="server" placeholder="Enter Phone" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label8" runat="server" Text="Email"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label10" runat="server" Text="Present Address"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxAddress" runat="server" placeholder="Enter Present Address" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter Present Address" ControlToValidate="tbxAddress">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label11" runat="server" Text="Gender"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlGender" Width="100%" runat="server" CssClass="form-control select2" DataTextField="GENDERNAME" DataValueField="ID"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Gender" ControlToValidate="ddlGender">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label19" runat="server" Text="NID"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxNid" runat="server" placeholder="Enter NID" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                    ErrorMessage="Enter NID" ControlToValidate="tbxNid">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label21" runat="server" Text="Passport"></asp:Label></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxPassport" runat="server" placeholder="Enter Passport No." CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label24" runat="server" Text="Birth Certificate"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxBirthCertificate" runat="server" placeholder="Enter Birth Certificate" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label25" runat="server" Text="TIN"></asp:Label></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxTin" runat="server" placeholder="Enter TIN No." CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label22" runat="server" Text="Religion"></asp:Label><span class="required"> *</span></label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlReligion" Width="100%" runat="server" CssClass="form-control select2" DataTextField="RELIGIONNAME" DataValueField="ID"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator9" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Religion" ControlToValidate="ddlReligion">*</asp:RequiredFieldValidator>
                            </div>
                            <label for="inputEmail3" class="col-sm-2">
                                <asp:Label ID="Label23" runat="server" Text="Profession"></asp:Label></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxProfession" runat="server" placeholder="Enter Profession" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12 text-center">
                                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary"
                                    OnClick="btnSave_OnClick" />
                                <asp:Button ID="btnUpdate" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" Visible="false"
                                    OnClick="btnUpdate_OnClick" />
                                <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                                    OnClick="btnReset_Click" />
                                <asp:Button ID="btnCustomerSearch" runat="server" Text="Search" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnCustomerSearch_OnClick"/>
                            
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr/>
            <div style="overflow: auto; max-height: 800px">
                <asp:Panel runat="server" ID="pnlReportButton" Visible="False">
               <div class="form-group">
                <div class="col-sm-12 text-center">
                    <asp:LinkButton ID="lnkDownloadExcel" runat="server" CssClass="btn btn-success" OnClick="lnkDownloadExcel_OnClick">Download (Excel) <i class="fa fa-download"></i></asp:LinkButton>
                    <asp:Button ID="btnDownloadPdf" Visible="False" runat="server" CssClass="btn btn-success" Text="Download As Pdf" OnClick="btnDownloadPdf_OnClick"/>
                    <asp:Button ID="btnOpenPdf" Visible="False" runat="server" CssClass="btn btn-success" Text="View As Pdf" OnClick="btnOpenPdf_OnClick" ClientIDMode="Static"/>
                    
                </div>
            </div>
                   </asp:Panel>
        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_ItemBound">
    <HeaderTemplate>
        <table id="example1" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                <thead>
                <tr>
                    <th>
                        SL#
                    </th>
            <asp:Repeater runat="server" ID="hdrRpt">
                <ItemTemplate>
                    <th>
                        <%# Container.DataItem %>
                    </th>
                    
                </ItemTemplate>
            </asp:Repeater>
                    <th>Action</th>
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
                <asp:LinkButton ID="btnEdit" Text="Edit"  Enabled="True" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnEdit_OnCommand" CausesValidation="False"/>
                <asp:LinkButton ID="btnDelete" Text="Delete" Visible="False" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure to delete?');" CausesValidation="False" />

            </td>
        </tr>

    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        
            </table>
        </FooterTemplate>
</asp:Repeater>
            </div>
            <!--<div class="panel panel-primary">
                <div class="panel-heading">
                    Customer List
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <div style="overflow: auto; height: 500px">
                                <table id="example1" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Mobile</th>
                                            <th>Email</th>
                                            <th>Address</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CUSTOMERNAME") %></td>
                                <td><%#Eval("MOBILE") %></td>
                                <td><%#Eval("EMAIL") %></td>
                                <td><%#Eval("ADDRESS") %></td>
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                    <%--<asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table></div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>-->

        </ContentTemplate>
        <Triggers>
     <asp:PostBackTrigger ControlID="lnkDownloadExcel"/>
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

