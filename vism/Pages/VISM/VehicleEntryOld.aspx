<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VehicleEntryOld.aspx.cs" Inherits="Pages_VISM_VehicleEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">

                <div class="panel panel-info">
                    <div class="panel-heading" style="height: 35px">
                        <div class="panel-title" style="text-align: center; margin-top: -7px">Vehicle Entry</div>
                    </div>
                    <div class="panel-body">

                        <div class="form-group">
                            <label class="col-sm-2">Vehicle Reg No.</label>
                            <asp:Panel runat="server" ID="pnltbxvehreg" Visible="True">
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="tbxVehReg" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator21" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxVehReg" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnlVehicleNew" Visible="False">
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="ddlVehRegHead" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />

                                    <%--<asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator2"  ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlVehRegHead" ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-sm-1" style="margin-left: -30px">
                                    <asp:DropDownList runat="server" ID="ddlVehRegSeries" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                    <%--<asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator9"  ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlVehRegSeries" ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-sm-1" style="margin-left: -30px">
                                    <div class='input-group'>
                                        <asp:TextBox runat="server" ID="tbxVehReg2Digit" onkeypress='return validateInt2(event);' CssClass="form-control" />
                                        <span class="input-group-addon" style="padding: 7px">-</span>

                                    </div>
                                    <%--<asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator8"  ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxVehReg2Digit" ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="col-sm-1" style="margin-left: -30px">
                                    <asp:TextBox runat="server" ID="tbxVehReg4Digit" onkeypress='return validateInt4(event);' CssClass="form-control" />
                                    <%--<asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator17"  ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxVehReg4Digit" ErrorMessage="Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                </div>
                            </asp:Panel>
                            <%--<div class="col-sm-1">
                        <asp:CheckBox runat="server" ID="chkNewVehicle" Text="New" AutoPostBack="True" OnCheckedChanged="chkNewVehicle_OnCheckedChanged"/>
                    </div>--%>
                            <label class="col-sm-2">Chassis#</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" ID="tbxChassis" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator18" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxChassis" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>


                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2">Engine#</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" ID="tbxEngine" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator3" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxEngine" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <label class="col-sm-2">Model</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlModel" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator6" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlModel" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2">Manufacturar</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlManufacturer" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator11" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlManufacturer" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                            <label class="col-sm-2">Reg. Date</label>
                            <div class="col-sm-4">
                                <%--                     <asp:TextBox runat="server" ID="tbxManufacYear" CssClass="form-control"/>
                            <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator12" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxManufacYear" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                <asp:TextBox ID="tbxManufacYear" runat="server" placeholder="Enter Reg. Date" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxManufacYear"></ajaxToolkit:CalendarExtender>

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2">Vehicle Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlVehicleType" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator13" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlVehicleType" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <label class="col-sm-2">Fuel Type</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlFuelType" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator14" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlFuelType" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2">Driver Name</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" ID="tbxDriverName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator4" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxDriverName" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <label class="col-sm-2">Driver Mobile</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" ID="tbxDriverMobile" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="" ID="RequiredFieldValidator10" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxDriverMobile" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2">Location</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlLocation" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator15" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlLocation" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <label class="col-sm-2">Company</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlCompany" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator16" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlCompany" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2">Origin</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlOrigin" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator1" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlOrigin" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <label class="col-sm-2">Country</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlCountry" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator5" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlCountry" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-2">CNG Cylinder Re-Test Date</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="tbxCNGReTestDate" runat="server" placeholder="Enter Reg. Date" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" TargetControlID="tbxCNGReTestDate"></ajaxToolkit:CalendarExtender>

                            </div>

                        </div>
                <%--        <div class="form-group">
                            <label class="col-sm-2">CNG Cylinder Re-Test Date</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="DropDownList1" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidator2" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlComsumtionUnit" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                        </div>--%>
                        <hr />
                        <div class="form-group">
                            <label class="col-sm-2">Customer Name</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlCustomer" Width="100%" CssClass="form-control select2" DataValueField="ID" DataTextField="TEXT" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_OnSelectedIndexChanged" />
                                <asp:TextBox runat="server" Visible="False" ID="tbxCustomer" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" InitialValue="0" ID="RequiredFieldValidatorDD" ClientIDMode="Static" Display="Dynamic" ControlToValidate="ddlCustomer" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" Enabled="False" InitialValue="" ID="RequiredFieldValidatorTX" ClientIDMode="Static" Display="Dynamic" ControlToValidate="tbxCustomer" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-sm-1">
                                <asp:CheckBox runat="server" ID="chkNewCustomer" Text="New" AutoPostBack="True" OnCheckedChanged="chkNewCustomer_OnCheckedChanged" />
                            </div>

                            <label class="col-sm-1">Mobile No.</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" ID="tbxMobile" CssClass="form-control" Enabled="False" />

                            </div>

                        </div>
                        <asp:Panel runat="server" ID="pnlCusInfo" Enabled="False">
                            <div class="form-group">
                                <label class="col-sm-2">Customer Address</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="tbxAddress" CssClass="form-control" />

                                </div>
                                <label class="col-sm-2">Customer Email</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="tbxEmail" CssClass="form-control" />

                                </div>

                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">NID</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="tbxNid" CssClass="form-control" />

                                </div>
                                <label class="col-sm-2"></label>
                                <div class="col-sm-4">
                                </div>

                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <d iv class="form-group">
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Button runat="server" Text="Save" ID="btnSave" CssClass="btn btn-success" OnClick="btnSave_OnClick" />
                        <asp:Button runat="server" Text="Update" ID="btnUpdate" Visible="False" CssClass="btn btn-success" OnClick="btnUpdate_OnClick" />

                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Button runat="server" Text="Reset" ID="btnReset" CssClass="btn btn-default" CausesValidation="False" OnClick="btnReset_OnClick" />
                    </div>
                    <div class="col-sm-2" style="text-align: right">
                        <asp:Button runat="server" Text="Search" ID="btnSearch" CssClass="btn btn-primary" CausesValidation="False" OnClick="btnSearch_OnClick" />
                    </div>
                </d>
                <hr />
                <div style="overflow: auto; max-height: 500px;">
                    <asp:Panel runat="server" ID="pnlReportButton" Visible="False">
                        <div class="form-group">
                            <div class="col-sm-12 text-center">
                                <asp:LinkButton ID="lnkDownloadExcel" runat="server" CssClass="btn btn-success" OnClick="lnkDownloadExcel_OnClick" CausesValidation="False">Download (Excel) <i class="fa fa-download" ></i></asp:LinkButton>
                                <asp:Button ID="btnDownloadPdf" Visible="False" runat="server" CssClass="btn btn-success" Text="Download As Pdf" OnClick="btnDownloadPdf_OnClick" />
                                <asp:Button ID="btnOpenPdf" Visible="False" runat="server" CssClass="btn btn-success" Text="View As Pdf" OnClick="btnOpenPdf_OnClick" ClientIDMode="Static" />

                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlExport">
                        <asp:Repeater runat="server" ID="rptMain" OnItemDataBound="rptMain_ItemBound">
                            <HeaderTemplate>
                                <table id="example1" class="table table-striped table-bordered" width="100%" cellspacing="0" border="1px">
                                    <thead>
                                        <tr>
                                            <th>Action</th>
                                            <th>SL#
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
                                    <td style="width: 500px">
                                        <%--<asp:LinkButton ID="btnEdit" Text="Edit"  Enabled="True" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnEdit_OnCommand" CausesValidation="False"/> &nbsp;&nbsp;&nbsp;&nbsp;

                <asp:LinkButton ID="btnDelete" Text="Delete" Visible="False" runat="server" CommandArgument='<%#Eval("Id")%>' OnCommand="btnDelete_OnCommand" CausesValidation="False" />--%>
                                        <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" CausesValidation="False" />
                                        <asp:ImageButton Visible="False" ID="btnDelete" ClientIDMode="Inherit" runat="server" OnCommand="btnDelete_OnCommand" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" CausesValidation="False" />

                                    </td>
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
                    </asp:Panel>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkDownloadExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">


    <script>
        function validateInt2(event) {
            var key = window.event ? event.keyCode : event.which;
            var val2Digit = document.getElementById('<%= tbxVehReg2Digit.ClientID %>').value;
            if (event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 46
                || event.keyCode == 37 || event.keyCode == 39) {
                return true;
            }
            else if (key < 48 || key > 57) {
                return false;
            }
            else if (val2Digit > 9) {
                return false;
            }
            else return true;
        }
        function validateInt4(event) {
            var key = window.event ? event.keyCode : event.which;
            var val4Digit = document.getElementById('<%= tbxVehReg4Digit.ClientID %>').value;
            if (event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 46
                || event.keyCode == 37 || event.keyCode == 39) {
                return true;
            }
            else if (key < 48 || key > 57) {
                return false;
            }
            else if (val4Digit > 999) {
                return false;
            }
            else return true;
        }







    </script>
</asp:Content>

