<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VehicleGateOut.aspx.cs" Inherits="Pages_VISM_VehicleGateOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .btn-success {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <asp:HiddenField runat="server" ID="hdnUnitName" />
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="panel panel-info">
                            <div class="panel-heading" style="height: 35px">
                                <div class="panel-title" style="text-align: center; margin-top: -7px"> Gate Out</div>
                            </div>
                            <div class="panel-body">
                               <div class="form-group">
                                    <label class="col-sm-2">Vehicle</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxVehicleid" Enabled="true" runat="server" placeholder="Enter Vehicle ID" AutoPostBack="true"  CssClass="form-control" OnTextChanged="tbxVehicleid_TextChanged"></asp:TextBox>
                                        <asp:HiddenField ID="hdnVehicleId" Value="0" runat="server" />
                                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                                            ErrorMessage="Select Vehicle" ControlToValidate="tbxVehicleid">*</asp:RequiredFieldValidator>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchVehicle"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxVehicleid"
                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                  
                                    <label for="inputEmail3" class="col-sm-2">
                                        Job ID</label>
                                    <div class="col-sm-4">
                                         <asp:DropDownList ID="tbxJobID" Width="100%" runat="server" CssClass="form-control select2" DataTextField="JOBID" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                     <label class="col-sm-2">Workshop </label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlWorkWorkshopId" Width="100%" runat="server" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <label for="inputEmail3" class="col-sm-2"> Registration Number</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxVehicleRegNumber" Enabled="true" runat="server" placeholder="Registration Number" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                 <div class="form-group">                                  
                                </div>                  
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2">
                                        Driver Name</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxDriverName" runat="server" placeholder="Driver Name" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <label for="inputEmail3" class="col-sm-2">
                                        Driver Staff Id</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxStaffId" runat="server" placeholder="Driver Staff ID" CssClass="form-control"></asp:TextBox>
                                    </div>
                               </div>
                               <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2"> Gate Out Date & Time </label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="tbxGateOutDate" runat="server" Enabled="False"  placeholder="Enter Work Order Date" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy HH:mm" TargetControlID="tbxGateOutDate"></ajaxToolkit:CalendarExtender>
                                    </div>
                              </div>
                              
                            </div>
                        </div>
                    </div>
     
                </div> 

                <div class="panel-footer" style="height: 55px;">
                    <div class="col-sm-12 text-center">
                       <asp:Button runat="server" ID="btnSave" UseSubmitBehavior="false" OnClientClick="this.disabled=true;" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnReset" Text="New" CssClass="btn btn-info" OnClick="btnReset_OnClick" CausesValidation="False" />     
                    </div>
                </div>

                <hr />

                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rpt" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <%--       <th>SL#</th>--%>
                                                    <th>Work Order No</th>
                                                    <th>Vehicle ID</th>
                                                    <th>Vehicle Reg. No</th>
                                                    <th>Job ID</th>
                                                    <th>Gate In Date & Time</th>
                                                    <th>Driver Staff ID</th>
                                                    <%--<th>Action</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("WORKORDERNO") %></td>
                                            <td><%#Eval("VEHICLEID") %></td>
                                            <td><%#Eval("REGNO") %></td>
                                            <td><%#Eval("JOBID") %></td>
                                            <td><%#Eval("INDATETIME") %></td>
                                            <td><%#Eval("DRIVERSTAFFID") %></td>
                                            <%--<td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" /></td>--%>
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

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
   
</asp:Content>


