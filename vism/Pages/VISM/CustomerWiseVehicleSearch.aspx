<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CustomerWiseVehicleSearch.aspx.cs" Inherits="Pages_VISM_CustomerWiseVehicleSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2">Customer</label>
                    <div class="col-sm-4">
                        <asp:DropDownList runat="server" ID="ddlCustomer" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" Width="100%"/>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_OnClick"/>
                    </div>
                </div>
                
                 <hr/>
            <div style="overflow: auto; height: 500px">
                <asp:Panel runat="server" ID="pnlReportButton" Visible="False">
               <div class="form-group">
                <div class="col-sm-12 text-center">
                    <asp:Button ID="btndownloadExcel" runat="server" CssClass="btn btn-success" Text="Download As Excel" OnClick="btndownloadExcel_OnClick" />
                    <asp:Button ID="btnDownloadPdf" runat="server" CssClass="btn btn-success" Text="Download As Pdf" OnClick="btnDownloadPdf_OnClick"/>
                    <asp:Button ID="btnOpenPdf" runat="server" CssClass="btn btn-success" Text="View As Pdf" OnClick="btnOpenPdf_OnClick" ClientIDMode="Static"/>
                    
                </div>
            </div>
                   </asp:Panel>
        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_ItemBound">
    <HeaderTemplate>
        <table id="example1" class="table table-striped table-bordered" width="100%" cellspacing="0">
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
                <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnEdit_OnCommand" CausesValidation="False"/>
                <asp:LinkButton ID="btnDelete" Text="Delete" Enabled="True" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure to delete?');" CausesValidation="False" />

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

