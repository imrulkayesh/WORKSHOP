<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="Pages_VISM_ItemMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
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
                   <%-- <label for="inputEmail3" class="col-sm-2">
                       Item Group</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlworkshop" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" AutoPostBack="true" OnSelectedIndexChanged="ddlworkshop_SelectedIndexChanged" DataValueField="ID" ></asp:DropDownList>
                    </div>--%>
                    <label for="inputEmail3" class="col-sm-2">
                       Item Group</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2" DataTextField="ITEMCATEGORYNAME"  DataValueField="ITEMCATEGORYCODE" ></asp:DropDownList>
                    </div>
                     <label for="inputEmail3" class="col-sm-2">
                        Item Code</label>
                    <div class="col-sm-4">
                          <asp:TextBox ID="tbxItemCode" runat="server" placeholder="Item Code" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Item Name</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxItemName" runat="server" placeholder="Item Name" CssClass="form-control"></asp:TextBox>
                    </div>
                  <%--  <label for="inputEmail3" class="col-sm-2">
                        Unit</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxCorder" runat="server" placeholder="Enter Corder" CssClass="form-control"></asp:TextBox>
                    </div>--%>
                     <label for="inputEmail3" class="col-sm-2">
                       Unit Name </label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2" DataTextField="UNITNAME" AutoPostBack="true" DataValueField="UNITCODE" ></asp:DropDownList>
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
                <br />
                <div class="form-group text-center">
                    <div class="col-sm-offset-2 col-sm-11">
                  <asp:Button ID="btnDownloadTemplate" runat="server" Text="Download Excel Template" 
                            CssClass="btn btn-success" OnClick="btnDownloadTemplate_Click" />
                        <div class="form-group">
                            <label class="col-sm-2">Upload Excel</label>
                            <div class="col-sm-4">
                                <asp:FileUpload ID="fuExcel" runat="server" CssClass="form-control" />
                            </div>

                            <div class="col-sm-2">
                                <asp:Button ID="btnUploadExcel" runat="server"
                                    Text="Upload Excel"
                                    CssClass="btn btn-success"
                                    OnClick="btnUploadExcel_Click" />
                            </div>
                        </div>
                     </div>
                 </div>
                <br />
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <%--<th>SL#</th>--%>
                                                    <th>Item Group</th>
                                                    <th>Item Code</th>
                                                    <th>Item Name</th>
                                                    <th>Unit Name</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <%--<td><%#Eval("CORDER") %></td>--%>
                                            <td><%#Eval("ITEMCATEGORYNAME") %></td>
                                            <td><%#Eval("ITEMCODE") %></td>                                           
                                            <td><%#Eval("ITEMNAME") %></td>
                                            <td><%#Eval("UNITNAME ") %></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ITEMCODE")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
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
                </div>

            </div>
        </ContentTemplate>

        <Triggers>
          <asp:PostBackTrigger ControlID="btnUploadExcel" />
          <asp:PostBackTrigger ControlID="btnDownloadTemplate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

