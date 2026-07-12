<%@ Page Title="FA Detail" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ReportFADetail.aspx.cs" Inherits="Pages_VISM_ReportCoDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Sender Details (VISM)</div>
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
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Receiver Details (VISM)</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-sm-4">FA Unit </label>
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

        < 
        <div class="form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Download As Normal Excel" OnClick="btndownloadNormalExcel_OnClick" />
                <%--              <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Download As Excel" OnClick="btndownloadExcel_OnClick" />
                <asp:Button ID="Button3" runat="server" CssClass="btn btn-success" Text="Download As Pdf" OnClick="btnDownloadPdf_OnClick" />
                <asp:Button ID="Button4" runat="server" CssClass="btn btn-success" Text="View As Pdf" OnClick="btnOpenPdf_OnClick" ClientIDMode="Static" />--%>

                <asp:Button ID="Button5" runat="server" CssClass="btn btn-default" CausesValidation="False" Text="Reset" OnClick="btnReset_OnClick" />
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
                            <asp:BoundField DataField="VISMWHNAME" HeaderText="WH" />
                            <asp:BoundField DataField="REGNO" HeaderText="REGNO" />
                            <asp:BoundField DataField="modelname" HeaderText="MODEL NAME" />
                            <asp:BoundField DataField="companyname" HeaderText="COMPANY" />
                            <asp:BoundField DataField="CATEGORYNAME" HeaderText="CATEGORY" />
                            <asp:BoundField DataField="itemcode" HeaderText="ITEM CODE" />
                            <asp:BoundField DataField="itemname" HeaderText="ITEM NAME" />
                            <asp:BoundField DataField="trdate" HeaderText="FA DATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                            <asp:BoundField DataField="reftrid" HeaderText="FA ID" />
                            <asp:BoundField DataField="unitname" HeaderText="UNIT" />
                            <asp:BoundField DataField="customername" HeaderText="CUSTOMER" />
                            <asp:BoundField DataField="CUSCATEGORY" HeaderText="CO CATEGORY" />
                            <asp:BoundField DataField="rate" HeaderText="RATE" />
                            <asp:BoundField DataField="qty" HeaderText="QUANTITY" />
                            <asp:BoundField DataField="PP" HeaderText="CR QUANTITY" />
                            <asp:BoundField DataField="CRAMOUNT" HeaderText="CR AMOUNT" />
                            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
  <%--                          <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="reftrid" HeaderText="reftrid" />
                            <asp:BoundField DataField="trdate" HeaderText="trdate" />
                            <asp:BoundField DataField="vismwhname" HeaderText="vismwhname" />
                            <asp:BoundField DataField="recvwh" HeaderText="recvwh" />
                            <asp:BoundField DataField="unitid" HeaderText="unitid" />
                            <asp:BoundField DataField="idate" HeaderText="idate" />
                            <asp:BoundField DataField="rtitle" HeaderText="rtitle" />
                            <asp:BoundField DataField="remarks" HeaderText="remarks" />--%>



                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

