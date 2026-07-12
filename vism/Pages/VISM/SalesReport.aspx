<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SalesReport.aspx.cs" Inherits="Pages_VISM_SalesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-md-9">
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Sales Report</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">

                            <label class="col-sm-4">Location</label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" Enabled="True" ID="ddlSendLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlSendLoc_OnSelectedIndexChanged" />
                              <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendLoc" InitialValue="" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">VISM Warehouse</label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ClientIDMode="Static" Enabled="False" ID="ddlSendWh" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlSendWh_OnSelectedIndexChanged" />
                          <%--      <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendWh" InitialValue="" ID="RequiredFieldValidator2" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">Company/Workshop </label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" Enabled="False" ID="ddlSendCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                               <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSendCom" InitialValue="" ID="RequiredFieldValidator3" ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4">From Date</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">To Date</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <div class="col-md-3" style="display:none;">
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Receiver Details (VISM)</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-sm-4">CO Unit </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="tbxRecVeh" runat="server" placeholder="Start typing vehicle reg no" CssClass="form-control" AutoPostBack="True" OnTextChanged="tbxRecVeh_OnTextChanged"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                    MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="tbxRecVeh"
                                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">Location</label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvLoc" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRecvLoc" InitialValue="" ID="RequiredFieldValidator4"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4">Company/Workshop </label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" Enabled="False" ID="ddlRecvCom" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-4">Vehicle REGNO </label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" Enabled="False" ID="ddlVehicleRegNo" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>

         
        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Details (Excel)" OnClick="btndownloadNormalExcel_OnClick" />
                <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Summary (Excel)" OnClick="btndownloadSummaryExcel_OnClick" />
<%--                <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Download As Excel" OnClick="btndownloadExcel_OnClick" />
                <asp:Button ID="Button3" runat="server" CssClass="btn btn-success" Text="Download As Pdf" OnClick="btnDownloadPdf_OnClick" />
                <asp:Button ID="Button4" runat="server" CssClass="btn btn-success" Text="View As Pdf" OnClick="btnOpenPdf_OnClick" ClientIDMode="Static" />--%>

                <%--<asp:Button ID="Button5" runat="server" CssClass="btn btn-warning" Text="Download Excel with Mergin" OnClick="btnMergine_OnClick" />--%>
                <asp:Button ID="Button6" runat="server" CssClass="btn btn-default" CausesValidation="False" Text="Reset" OnClick="btnReset_OnClick" />
                <%--<asp:Button ID="btnExport" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_OnClickport_OnClick" />--%>
            </div>
        </div>

        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Panel ID="Panel2" runat="server" Width="100%">
                    <asp:GridView ID="GridView2" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" Width="100%" AlternatingRowStyle-ForeColor="#000"
                        runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="WORKSHOP" HeaderText="Name of Wksp" />
                            <asp:BoundField DataField="JOBID" HeaderText="Job ID" />
                            <asp:BoundField DataField="REGNO" HeaderText="Reg No." />

                            <asp:BoundField DataField="MODELNAME" HeaderText="Model" />
                            <asp:BoundField DataField="SERVICETYPE" HeaderText="Service Type" />
                            <asp:BoundField DataField="CUSTOMERNAME" HeaderText="Customer Name" />

                            <asp:BoundField DataField="COMPANYNAME" HeaderText="Company" />
                            <asp:BoundField DataField="Spare_Parts_Cost" HeaderText="Spare Parts cost" />
                            <asp:BoundField DataField="SP_Avg_Cost" HeaderText="SP Avg Cost (3 Months)" />
                            <asp:BoundField DataField="Labour_cost" HeaderText="Labour cost" />
                            <asp:BoundField DataField="POWERCOST" HeaderText="Power cost" />
                            <asp:BoundField DataField="Total_Variable_Cost" HeaderText="Total Variable Cost" />
                            <asp:BoundField DataField="CM" HeaderText="CM" />
                            <asp:BoundField DataField="Gross_Sales" HeaderText="Gross_Sales"/>
                            <asp:BoundField DataField="DISCOUNTPERCENT" HeaderText="Discount Percent" />
                            <asp:BoundField DataField="DISCOUNTAMOUNT" HeaderText="Discount Amount" />
                            <asp:BoundField DataField="Net_Sales" HeaderText="Net Sales" />
                           
                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" Width="100%" AlternatingRowStyle-ForeColor="#000"
                        runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Workshop" HeaderText="Workshop" />
                            <asp:BoundField DataField="Total_Jobs" HeaderText="Total Jobs" />
                            <asp:BoundField DataField="Total_Spare_Parts_Cost" HeaderText="Spare Parts Cost" />
                            <asp:BoundField DataField="Total_Labour_Cost" HeaderText="Labour Cost" />
                            <asp:BoundField DataField="Total_Power_Cost" HeaderText="Power_Cost" />
                            <asp:BoundField DataField="Total_Variable_Cost" HeaderText="Total Variable Cost" />
                            <asp:BoundField DataField="Total_CM" HeaderText="CM" />
                            <asp:BoundField DataField="Total_Gross_Sales" HeaderText="Gross Sales" />
                            <asp:BoundField DataField="Total_Discount_Percent" HeaderText="Discount (%)"/>
                            <asp:BoundField DataField="Total_Discount_Amount" HeaderText="Discount Amount" />
                            <asp:BoundField DataField="Total_Net_Sales" HeaderText="Net Sales" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
