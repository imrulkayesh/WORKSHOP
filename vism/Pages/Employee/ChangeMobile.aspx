<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangeMobile.aspx.cs" Inherits="Pages_Employee_ChangeMobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <hr/>
            <h2>User Mobile Number Change</h2>
            <hr/>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2">Username</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="tbxusername" Enabled="True" placeholder="Start typing to search staff id" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchStaffId"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="tbxusername"
                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                        </div>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxusername" ErrorMessage="Please enter staff id" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    

                </div>
                <%--<div class="form-group">
                    <label class="col-sm-2">Existing mobile</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="tbxexistMobile" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>--%>
                <div class="form-group">
                    <label class="col-sm-2">New Mobile</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" ID="tbsNewMobile" Enabled="True" CssClass="form-control integer"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbsNewMobile" ErrorMessage="Please enter mobile number" SetFocusOnError="True" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="YourRegularExpressionValidator" runat="server"
                                                    ControlToValidate="tbsNewMobile"
                                                    ErrorMessage="Invalid mobile number.Ex: 01992123456" SetFocusOnError="True"
                                                    ValidationExpression="^[0][1][5,6,7,8,9][0-9]{8}$"/>
                </div>
                <div class="form-group">
                    <label class="col-sm-2"></label>
                    <div class="col-sm-4">
                        <asp:Button runat="server" ID="tbnChange" Text="Change" CssClass="btn btn-success" OnClick="tbnChange_OnClick"/>
                    </div>
                    
                </div>
                
                <%--<div class="form-group">
                    <label class="col-sm-2"></label>
                    <div class="col-sm-4" style="margin-top: 50px">
                        <a href="http://hrms.prangroup.com:8283/ya/Pages/Admin/Default.aspx"><h1><u>Goto Home Page</u></h1></a>
                    </div>
                    
                </div>--%>
            </div>
    
   

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
    <script>
        $(document).ready(function () {
            $('.integer').inputmask({
                regex: "^[0][1][5,6,7,8,9][0-9]{8}$",
                placeholder: "",
                onKeyValidation: function(key, result){
                if (!result) {
                    alert('Your input is not valid');
                }
            }
            });
        });
    </script>
</asp:Content>

