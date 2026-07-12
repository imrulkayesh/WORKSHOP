<%@ Page Title="Bill" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Bill.aspx.cs" Inherits="Pages_VISM_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">
            Bill Details
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        JobID</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxJobID" runat="server" placeholder="Enter JobID" AutoPostBack="true" OnTextChanged="tbxJobID_TextChanged" CssClass="form-control"></asp:TextBox>
                        <asp:HiddenField ID="hdnJobID" Value="0" runat="server" />
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator25" runat="server" ValidationGroup="save"
                            ErrorMessage="Select Job Card" ControlToValidate="tbxJobID">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchJob"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="tbxJobID"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        PayType</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlPayType" Width="100%" runat="server" CssClass="form-control select2" DataTextField="PAYTYPE" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Total Amount</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxTotal" Enabled="false" runat="server" placeholder="Enter Total Amount" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Discount</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxDiscount" runat="server" AutoPostBack="true" OnTextChanged="tbxDiscount_TextChanged" placeholder="Enter Discount" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Net Amount</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxNetAmount" runat="server" placeholder="Enter Net Amount" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Customer Paid
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxPaid" runat="server" placeholder="Enter Paid Amount" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Company Paid</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxCompanyPaid" runat="server" placeholder="Enter Company Paid Amount" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Paid Company</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCompany" Width="100%" runat="server" CssClass="form-control select2" DataTextField="COMPANYNAME" DataValueField="ID"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Employee Paid</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxEmpPaid" runat="server" placeholder="Enter Employee Paid Amount" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Employee StaffID</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxEmpStaffID" runat="server" placeholder="Enter Employee StaffID" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Currency</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCurrency" Width="100%" runat="server" CssClass="form-control select2" DataTextField="CURRENCYCODE" DataValueField="ID"></asp:DropDownList>
                    </div>
                    <label for="inputEmail3" class="col-sm-2">
                        Remarks</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tbxremark" runat="server" placeholder="Enter Remark" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group text-center">
                    <div class="col-sm-offset-2 col-sm-11">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Bill List
        </div>
        <div class="panel-body">
            <div class="box">
                <div class="box-body">
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <table id="example1" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>BILLNo</th>
                                        <th>Vehicle</th>
                                        <th>Customer</th>
                                        <th>Mobile</th>
                                        <th>Total</th>
                                        <th>Discount</th>
                                        <th>Paid</th>
                                        <th>Due</th>
                                        <th>Remark</th>
                                        <th>Edit</th>
                                        <th>Print</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("JOBID")%>' Text='<%#Eval("JOBID") %>' ToolTip="View Details" /></td>
                                <td><%#Eval("CHASSISNO") %> / <%#Eval("REGNO") %></td>
                                <td><%#Eval("CUSTOMERNAME") %></td>
                                <td><%#Eval("CUSTMOBILE") %></td>
                                <td><%#Eval("TOTALAMOUNT") %></td>
                                <td><%#Eval("DISCOUNT") %></td>
                                <td><%#Eval("PAIDAMOUNT") %></td>
                                <td><%#Convert.ToDecimal(Eval("NETAMOUNT"))-Convert.ToDecimal(Eval("PAIDAMOUNT")) %></td>
                                <td><%#Eval("REMARKS") %></td>
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("JOBID")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnPrint" runat="server" OnCommand="btnPrint_Command" CommandArgument='<%# Eval("JOBID")%>' ImageUrl="~/Content/Images/Common/printBill.png" ToolTip="Bill Print" />
                                    <%--<asp:ImageButton ID="btnJobCard" runat="server" OnCommand="btnJobCard_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/assign.png" ToolTip="Job Card" />--%></td>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

