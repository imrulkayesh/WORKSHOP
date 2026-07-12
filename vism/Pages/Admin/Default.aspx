<%@ Page Title="Modules" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .inner {
            min-height: 90px;
        }

            .inner a {
                color: #fff;
            }

            .inner:hover, .inner:hover a {
                background-color: #ccffdd;
                color: #ff9900;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <asp:Repeater ID="rptModule" runat="server">
            <ItemTemplate>                            
                <div class="col-md-2">
                <asp:LinkButton ID="btnModule" runat="server" Width="100%" OnCommand="btnModule_Command" CommandArgument='<%# Eval("ID")%>' CssClass='fa fa-2x <%#Eval("Icon") %>'>
                    <div class="small-box bg-green">
                        <div class="inner text-center"><i class='fa fa-2x <%#Eval("Icon") %>'></i>
                            <br />
                            <span><b style="font-size:14px; font-weight:bold; font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif"><%#Eval("MODULENAME") %></b></span>
                        </div>
                    </div>
                </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

