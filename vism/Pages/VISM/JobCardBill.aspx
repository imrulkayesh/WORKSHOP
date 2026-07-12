<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="JobCardBill.aspx.cs" Inherits="Pages_VISM_JobCardBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:HiddenField runat="server" ID="hdnUnitName" />

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Jobcard Search</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxJobId" runat="server" placeholder="Enter Job ID" CssClass="form-control"></asp:TextBox>
                      <%--  <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="tbxVehileForJobIns"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>--%>
                    </div>
                    <asp:Button ID="btnPreInsSearch" runat="server" ClientIDMode="Static" Text="Search" CssClass="btn btn-success"
                        OnClick="btnPreInsSearch_Click" CausesValidation="False" />
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Job Card Bill Print</div>
                    </div>
                    <div class="panel-body" style="overflow: auto">
                        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_OnItemDataBound">
                            <HeaderTemplate>
                                <table id="example4" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                                    <thead>
                                        <tr>
                                            <th>SL#
                                            </th>
                                            <asp:Repeater runat="server" ID="hdrRpt">
                                                <ItemTemplate>
                                                    <th>
                                                        <%# Container.DataItem %>
                                                    </th>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <th>Bill Print</th>
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
                                        <asp:ImageButton ID="btnBillPrintUpdate" runat="server" OnCommand="btnBillPrintUpdate_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print" CausesValidation="False" />
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rptMain" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
  
</asp:Content>



