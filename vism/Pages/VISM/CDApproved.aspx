<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CDApproved.aspx.cs" Inherits="Pages_VISM_CDApproved" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .Grid th {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid th {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
    </style>
    <script src="../js/jsView.js"></script>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../img/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">

                <br />
                <div class="form-group">
                    <div class="col-sm-12">

                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                            DataKeyNames="ID" OnRowDataBound="OnRowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" src="../img/plus.png" />
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="LINE" HeaderText="SL#" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ITEMCODE" HeaderText="Code" />
                                                    <asp:BoundField ItemStyle-Width="300px" DataField="INAME" HeaderText="Name" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="UNITNAME" HeaderText="Unit" />
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="QTY" HeaderText="Qty" />
                                                    <asp:BoundField ItemStyle-Width="100px" DataField="RATE" HeaderText="Rate" />
                                                    <%--   <asp:BoundField ItemStyle-Width="100px" DataField="TOTALAMT" HeaderText="Total Price" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("ID") %>' ImageUrl="~/Content/Images/Common/oksmal.jpg" ToolTip="Approve" formnovalidate="" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%# Eval("ID")%>' OnCommand="imgbtnDelete_OnCommand" ImageUrl="../../Content/Images/delete.png" ToolTip="Delete" formnovalidate="" OnClientClick="return confirm('Are you sure?')" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField ItemStyle-Width="150px" DataField="ID" HeaderText="CD" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="REGNO" HeaderText="REGNO" />
                                <%--<asp:BoundField ItemStyle-Width="150px" DataField="SERVICETYPE" HeaderText="SERVICETYPE" />--%>
                                <asp:BoundField ItemStyle-Width="150px" DataField="EMPLOYEENAME" HeaderText="RECEIVER" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="COMPANYNAME" HeaderText="COMPANYNAME" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="IDATE" HeaderText="CD_DATE" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="RECVREMARKS" HeaderText="REMARKS" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvMaster" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

