<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ReportJobCard.aspx.cs" Inherits="Pages_VISM_ReportJobCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                WorkShop
            </label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlWorkShop" Width="100%" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" />
            </div>
            <label for="inputEmail3" class="col-sm-2">
                                Vehicle</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxVehicleid" runat="server" placeholder="Enter Vehicle ID" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdnVehicleId" Value="0" runat="server" />
                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                    ErrorMessage="Select Vehicle" ControlToValidate="tbxVehicleid">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="tbxVehicleid"
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
            </div>
        </div>
        <div class="form-group" style="margin-top: 25px">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnShowPdf" runat="server" CssClass="btn btn-success" OnClick="btnShowPdf_OnClick" ValidationGroup="search" Text="Show as PDF" />
                <asp:Button ID="btnDownloadPdf" runat="server" CssClass="btn btn-success" OnClick="btnDownloadPdf_OnClick" ValidationGroup="search" Text="Download as PDF" />
                <asp:Button ID="btnDownloadExcel" Visible="True" runat="server" OnClick="btnDownloadExcel_OnClick" Text="Download as Excel" CssClass="btn btn-success" />
            </div>
        </div>
    </div>
    <hr/>
<div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title text-center">Job Card List</div>
                    </div>                     
                    <div class="panel-body" style="overflow: auto; max-height: 500px;">
                        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_OnItemDataBound">
    <HeaderTemplate>
        <table id="example1" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                <thead>
                <tr>
                    <%--<th>Action</th>--%>
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
                    
        </tr>
                </thead>
                <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <%--<td>
               <asp:ImageButton Visible="True" ID="imgEdit" runat="server" OnCommand="imgEdit_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="../../../img/edit.png" ToolTip="Edit" CausesValidation="False" />
                <asp:ImageButton Visible="True" ID="imgDelete" runat="server" OnCommand="imgDelete_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="../../../img/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you Sure')" CausesValidation="False" />
                <asp:ImageButton Visible="True" ID="imgPrint" runat="server" OnCommand="imgPrint_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="../../../img/print.png" ToolTip="Print" CausesValidation="False" />
            </td>--%>
            <td><%#Container.ItemIndex+1 %></td>
            <asp:Repeater runat="server" ID="bdyRpt">
                <ItemTemplate>
                    <td>
                        <%# Container.DataItem %>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
            
        </tr>

    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        
            </table>
        </FooterTemplate>
</asp:Repeater>

                        </div>
                   
                </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

