<%@ Page Title="Task Manager" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="TaskManager.aspx.cs" Inherits="Pages_UserManagement_TaskManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Content/Styles/fontawesome-iconpicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-10 col-sm-10 col-xs-10">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label13" runat="server" Text="Module"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlModule" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" DataTextField="MODULENAME" DataValueField="ID" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        Root</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlParent" Width="100%" runat="server" DataTextField="TextEng" DataValueField="Id" CssClass="form-control select2">
                            <asp:ListItem Text="Root" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        TitleEnglidh</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxTitleEng" runat="server" placeholder="Enter Role Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Title(Englidh)" ControlToValidate="tbxTitleEng">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        TitleBangla</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxTitleBan" runat="server" placeholder="Enter Role Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Title(Bangla)" ControlToValidate="tbxTitleBan">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        URL</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxURL" runat="server" placeholder="Enter URL" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter URL" ControlToValidate="tbxURL">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label12" runat="server" Text="Column Order"></asp:Label></label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="tbxOrderColumn" runat="server" placeholder="Enter Column Order" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                            ErrorMessage="Enter Column Order" ControlToValidate="tbxOrderColumn">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb" runat="server" TargetControlID="tbxOrderColumn" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        ChooseIcon</label>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="tbxIconPicker" runat="server" ClientIDMode="Static" data-placement="bottomRight" CssClass="form-control icp icp-auto"></asp:TextBox>
                            <span class="input-group-addon"></span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary" ValidationGroup="save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" ClientIDMode="Static" Text="Update" CssClass="btn btn-primary" ValidationGroup="save" Visible="false"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="btnReset" runat="server" ClientIDMode="Static" Text="Refresh" CssClass="btn btn-primary" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptTask" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>TitleEnglidh</th>
                                                    <th>TitleBangla</th>
                                                    <th>URL</th>
                                                    <th>Root</th>
                                                    <th>CreatedBy</th>
                                                    <th>CreatedDate</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("TextEng") %></td>
                                            <td><%#Eval("TextBan") %></td>
                                            <td><%#Eval("URL") %></td>
                                            <td><%#Eval("PARENTID") %></td>
                                            <td><%#Eval("CreatedBy") %></td>
                                            <td><%# Convert.ToDateTime(Eval("CreateDate")).ToString("dd-MMM-yyyy")%></td>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Content/Images/Common/edit.png" ToolTip="Edit" />

                                               <%-- <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/Content/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')" />--%>

                                            </td>
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
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        function loadIcon() {
            $('.icp-auto').iconpicker();
            $('.action-create').on('click', function () {
                $('.icp-auto').iconpicker();

                $('.icp-opts').iconpicker({
                    title: 'With custom options',
                    icons: ['fa-github', 'fa-heart', 'fa-html5', 'fa-css3'],
                    selectedCustomClass: 'label label-success',
                    mustAccept: true,
                    placement: 'bottomRight',
                    showFooter: true,
                    hideOnSelect: true,
                    templates: {
                        footer: '<div class="popover-footer">' +
                                    '<div style="text-align:left; font-size:12px;">Placements: \n\
                    <a href="#" class=" action-placement">inline</a>\n\
                    <a href="#" class=" action-placement">topLeftCorner</a>\n\
                    <a href="#" class=" action-placement">topLeft</a>\n\
                    <a href="#" class=" action-placement">top</a>\n\
                    <a href="#" class=" action-placement">topRight</a>\n\
                    <a href="#" class=" action-placement">topRightCorner</a>\n\
                    <a href="#" class=" action-placement">rightTop</a>\n\
                    <a href="#" class=" action-placement">right</a>\n\
                    <a href="#" class=" action-placement">rightBottom</a>\n\
                    <a href="#" class=" action-placement">bottomRightCorner</a>\n\
                    <a href="#" class=" active action-placement">bottomRight</a>\n\
                    <a href="#" class=" action-placement">bottom</a>\n\
                    <a href="#" class=" action-placement">bottomLeft</a>\n\
                    <a href="#" class=" action-placement">bottomLeftCorner</a>\n\
                    <a href="#" class=" action-placement">leftBottom</a>\n\
                    <a href="#" class=" action-placement">left</a>\n\
                    <a href="#" class=" action-placement">leftTop</a>\n\
                    </div><hr></div>'
                    }
                }).data('iconpicker').show();
            }).trigger('click');

            $('.icp').on('iconpickerSelected', function (e) {
                $('.lead .picker-target').get(0).className = 'picker-target fa-3x ' +
                            e.iconpickerInstance.options.iconBaseClass + ' ' +
                            e.iconpickerInstance.options.fullClassFormatter(e.iconpickerValue);
            });
        }
        $(function () {
            loadIcon();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                loadIcon();
            });
        };
    </script>
</asp:Content>

