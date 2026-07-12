<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ItemAdd.aspx.cs" Inherits="Pages_VISM_ItemAdd" %>

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
                           <%-- <a href="../../App_WebReferences/">../../App_WebReferences/</a>--%>
                            <div class="panel-heading" style="height: 35px">
                                <div class="panel-title" style="text-align: center; margin-top: -7px">Warehouse Item Add</div>
                            </div>
                            <div class="panel-body">
                               <%-- <a href="../../Content/">../../Content/</a>--%>

                                <div class="form-group">
                                    <label class="col-sm-4">Item Code</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtItemCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4">Location</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="True" ID="ddlRecvLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlRecvLoc_OnSelectedIndexChanged" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvLoc" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4">VISM Warehouse</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT"/>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvWh" InitialValue="" ID="RequiredFieldValidator5" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-sm-8">
                                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-success" OnClick="btnSave_OnClick" />
                                    </div>
                                </div>




                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                    </div>
                </div>

                <hr />

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

