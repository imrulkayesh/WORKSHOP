<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="RptCRDetails.aspx.cs" Inherits="Pages_VISM_RptCRDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
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
                                <asp:DropDownList runat="server" Enabled="True" ID="ddlRecLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlRecLoc_OnSelectedIndexChanged" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">VISM Warehouse</label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ClientIDMode="Static" Enabled="False" ID="ddlRecWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlRecWh_OnSelectedIndexChanged" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">Company/Workshop </label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" Enabled="False" ID="ddlRecCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Receive Date (VISM)</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-sm-4">From Date</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">To Date</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control"  autocomplete="off"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-sm-4"></label>
                            <div class="col-sm-8">
                                <asp:Button ID="btnSearchCR" runat="server" CssClass="btn btn-success" Text="Search CR" OnClick="btnSearchCR_OnClick" />
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>

          <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Panel ID="Panel2" runat="server" Width="100%">
                    <asp:GridView ID="GridView2" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" Width="100%" AlternatingRowStyle-ForeColor="#000"
                        runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="TRID" HeaderText="CR_ID" />
                            <asp:BoundField DataField="TRDATE" HeaderText="CR_DATE" />
                            <asp:BoundField DataField="WHCODE" HeaderText="RECEIVE_WHCODE" />
                            <asp:BoundField DataField="WHNAME" HeaderText="RECEIVE_WHNAME" />
                            <asp:BoundField DataField="ITEMCODE" HeaderText="ITEMCODE" />
                            <asp:BoundField DataField="ITEMNAME" HeaderText="ITEMNAME" />
                            <asp:BoundField DataField="UNITNAME" HeaderText="CO UNITNAME" />
                            <asp:BoundField DataField="RERATE" HeaderText="RATE" />
                            <asp:BoundField DataField="REQTY" HeaderText="QTY" />
                            <asp:BoundField DataField="REAMT" HeaderText="AMOUNT" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

