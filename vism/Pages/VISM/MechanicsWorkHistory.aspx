<%@ Page Title="Mechanics Work History" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="MechanicsWorkHistory.aspx.cs" Inherits="Pages_VISM_MechanicsWorkHistory" %>

<asp:content id="Content1" contentplaceholderid="head" runat="Server">
    <link href="../../Content/dt/buttons.dataTables.min.css" rel="stylesheet" />
</asp:content>
<asp:content id="Content2" contentplaceholderid="cphMain" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                StaffID
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxStaffID" runat="server" placeholder="Enter StaffID" CssClass="form-control"></asp:TextBox>
            </div>
              <label for="inputEmail3" class="col-sm-2">
                WorkShop
            </label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlWorkShop" Width="100%" CssClass="form-control select2" DataTextField="WORKSHOP" DataValueField="ID" 
                    AutoPostBack="True" OnSelectedIndexChanged="ddlWorkShop_OnSelectedIndexChanged"/>
            </div>
        </div>
        <div class="form-group">
             <label for="inputEmail3" class="col-sm-2">
                Service Type</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlServiceType" Width="100%" runat="server" CssClass="form-control select2" DataTextField="SERVICETYPE" DataValueField="ID"></asp:DropDownList>
            </div>

              <label for="inputEmail3" class="col-sm-2">
                Team</label>
            <div class="col-sm-4">
                 <asp:DropDownList runat="server" Enabled="True" ID="ddlteam" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
            </div>
        </div>
          <div class="form-group">
             <label for="inputEmail3" class="col-sm-2">
                Date Type</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddldatetype" Width="100%" runat="server" CssClass="form-control select2" DataTextField="TEXT" DataValueField="ID"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddldatetype" InitialValue="" ID="RequiredFieldValidator5"  ClientIDMode="Static" Display="Dynamic" ErrorMessage="This is required field." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>

              <label for="inputEmail3" class="col-sm-2">
                Group</label>
            <div class="col-sm-4">
                 <asp:DropDownList runat="server" Enabled="True" ID="ddlgroup" CssClass="form-control" Width="100%" DataValueField="ID" DataTextField="TEXT" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputEmail3" class="col-sm-2">
                From Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxfromdate" runat="server" placeholder="Enter From Date" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxfromdate"></ajaxToolkit:CalendarExtender>
            </div>
            <label for="inputEmail3" class="col-sm-2">
                To Date
            </label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxtodate" runat="server" placeholder="Enter To Date" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxtodate"></ajaxToolkit:CalendarExtender>
            </div>
        </div>
        <div class="col-sm-12 text-center">
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search" Text="Search with Problem" />
            <asp:Button ID="btnSearch2" runat="server" CssClass="btn btn-primary" OnClick="btnSearch2_Click" ValidationGroup="search" Text="Search" />
            <asp:Button ID="btnExport" runat="server" ClientIDMode="Static" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click" />
        </div>
    </div>
    <div class="col-sm-12">
        <div style="overflow: auto; height: 600px">
            <div class="box">
                <div class="box-body">
                    <asp:Panel ID="pnlExport" runat="server">
                        <center>
                <b style="font-size:24px;">Machanics Work History</b><br />
                            <b><asp:Label ID="lblCompanyandLocation" runat="server"></asp:Label> </b><br />
                              <asp:Label ID="lblfromDate" Text="From" runat="server"></asp:Label>  -
                        <asp:Label ID="lbltoDate" Text="To" runat="server"></asp:Label> <br /><hr/>
              </center>
                        <asp:Repeater ID="rpt" runat="server">
                            <HeaderTemplate>
                                <table id="dt" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Technician</th>
                                            <th>Team</th>
                                            <th>ServiceType</th>
                                            <th>Customer</th>
                                            <th>Job ID</th>
                                            <th>RegNo</th>
                                            <th>ChassisNo</th>
                                            <th>Model</th>
                                            <th>Receive Date</th>
                                            <th>Delivary Date</th>
                                            <th>Total Days</th>
                                            <th>Status</th>
                                            <th>Workshop</th>
                                            <th>Problem</th>
                                            <th>Unit</th>
                                            <th>Rate</th>
                                            <th>Labor Qty</th>
                                            <th>Working Hour</th>
                                            <th>Amount</th>
                                            <th>Remarks</th>
                                            
                                            <%--<th>JobNo</th>
                                            <th>PINo</th>
                                            <th>PDI</th>
                                            <th>BILL</th>
                                            <th>MoneyRecipt</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex+1 %></td>
                                    <td><%#Eval("STAFFID")+"-"+Eval("EMPLOYEENAME") %></td>
                                    <td><%#Eval("TEAMNAME") %></td>
                                    <td><%#Eval("SERVICETYPE") %></td>
                                    <td><%#Eval("CUSTOMERNAME") %></td>
                                    <td><%#Eval("ID") %></td>
                                    <td><%#Eval("REGNO") %></td>
                                    <td><%#Eval("CHASSISNO") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("CREATEDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                                    <td><%#Eval("UPDATEDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                                    <td><%# string.IsNullOrEmpty(Eval("UPDATEDATE").ToString())?"": (Convert.ToDateTime(Eval("UPDATEDATE")) - Convert.ToDateTime(Eval("CREATEDATE"))).Duration().TotalDays.ToString("0")%></td>
                                    <td><%#Eval("ISOPEN").ToString().Replace("0","Completed").Replace("1","InProgress") %></td>
                                    <td><%#Eval("WORKSHOP") %></td>
                                    <td><%#Eval("ITEMNAME") %></td>
                                    <td><%#Eval("QUANTITY") %></td>
                                    <td><%#Eval("LABORRATE") %></td>
                                    <td><%#Eval("LABORQTY") %></td>
                                    <td><%#Eval("WORKINGHOUR") %></td>
                                    <td><%#Eval("AMOUNT") %></td>
                                    <td><%#Eval("TREMARKS") %></td>
                                    
                                    <%--<td>
                                        <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' Text='<%#Eval("ID") %>' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnPI" runat="server" OnCommand="btnPI_Command" CommandArgument='<%# Eval("PIID")%>' Text='<%#Eval("PIID") %>' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnPDI" runat="server" OnCommand="btnPDI_Command" CommandArgument='<%# Eval("ID")%>' Text='View' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnBill" runat="server" OnCommand="btnBill_Command" CommandArgument='<%# Eval("ID")%>' Text='View' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnMoneyRecipt" runat="server" OnCommand="btnMoneyRecipt_Command" CommandArgument='<%# Eval("ID")%>' Text='View' ToolTip="Job Details" /></td>--%>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                       
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rpt2" runat="server">
                            <HeaderTemplate>
                                <table id="dt" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#SL</th>
                                            <th>Technician</th>
                                            <th>Team</th>
                                            <th>ServiceType</th>
                                            <th>Customer</th>
                                            <th>Job ID</th>
                                            <th>RegNo</th>
                                            <th>ChassisNo</th>
                                            <th>Model</th>
                                            <th>Receive Date</th>
                                            <th>Delivary Date</th>
                                            <th>Total Days</th>
                                            <th>Status</th>
                                            <th>Workshop</th>
                                            <th>Remarks</th>
                                            
                                            <%--<th>JobNo</th>
                                            <th>PINo</th>
                                            <th>PDI</th>
                                            <th>BILL</th>
                                            <th>MoneyRecipt</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex+1 %></td>
                                    <td><%#Eval("STAFFID")+"-"+Eval("EMPLOYEENAME") %></td>
                                    <td><%#Eval("TEAMNAME") %></td>
                                    <td><%#Eval("SERVICETYPE") %></td>
                                    <td><%#Eval("CUSTOMERNAME") %></td>
                                    <td><%#Eval("ID") %></td>
                                    <td><%#Eval("REGNO") %></td>
                                    <td><%#Eval("CHASSISNO") %></td>
                                    <td><%#Eval("MODELNAME") %></td>
                                    <td><%#Eval("CREATEDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                                    <td><%#Eval("UPDATEDATE","{0:dd/MM/yyyy HH:MM}") %></td>
                                    <td><%# string.IsNullOrEmpty(Eval("UPDATEDATE").ToString())?"": (Convert.ToDateTime(Eval("UPDATEDATE")) - Convert.ToDateTime(Eval("CREATEDATE"))).Duration().TotalDays.ToString("0")%></td>
                                    <td><%#Eval("ISOPEN").ToString().Replace("0","Completed").Replace("1","InProgress") %></td>
                                    <td><%#Eval("WORKSHOP") %></td>
                                    <td><%#Eval("REMARKS") %></td>
                                    
                                    <%--<td>
                                        <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("ID")%>' Text='<%#Eval("ID") %>' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnPI" runat="server" OnCommand="btnPI_Command" CommandArgument='<%# Eval("PIID")%>' Text='<%#Eval("PIID") %>' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnPDI" runat="server" OnCommand="btnPDI_Command" CommandArgument='<%# Eval("ID")%>' Text='View' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnBill" runat="server" OnCommand="btnBill_Command" CommandArgument='<%# Eval("ID")%>' Text='View' ToolTip="Job Details" /></td>
                                    <td>
                                        <asp:LinkButton ID="btnMoneyRecipt" runat="server" OnCommand="btnMoneyRecipt_Command" CommandArgument='<%# Eval("ID")%>' Text='View' ToolTip="Job Details" /></td>--%>
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
        </div>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="script" runat="Server">
    <script src="../../Content/dt/dataTables.buttons.min.js"></script>
    <script src="../../Content/dt/buttons.flash.min.js"></script>
    <script src="../../Content/dt/jszip.min.js"></script>
    <script src="../../Content/dt/pdfmake.min.js"></script>
    <script src="../../Content/dt/vfs_fonts.js"></script>
    <script src="../../Content/dt/buttons.html5.min.js"></script>
    <script src="../../Content/dt/buttons.print.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#dt').DataTable({
                dom: 'Bfrtip',
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true,
                "order": [[0, "desc"]],
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:content>

