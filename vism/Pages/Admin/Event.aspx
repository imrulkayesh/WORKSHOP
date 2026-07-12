<%@ Page Title="Academic Calendar" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Pages_Admin_Event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/cupertino/jquery-ui-1.10.3.min.css" rel="stylesheet" type="text/css" />
    <link href="../../fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../../css/jquery.qtip-2.2.0.css" rel="stylesheet" type="text/css" />

    <script src="../../jquery/moment-2.8.1.min.js" type="text/javascript"></script>
    <script src="../../jquery/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../../jquery/jquery-ui-1.11.1.js" type="text/javascript"></script>
    <script src="../../jquery/jquery.qtip-2.2.0.js" type="text/javascript"></script>
    <script src="../../fullcalendar/fullcalendar-2.0.3.js" type="text/javascript"></script>
    <script src="../../Scripts/calendarscript.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div id="calendar">
    </div>
    <div id="updatedialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none; overflow: hidden;"
        title="Update or Delete Event">
        <div class="form-horizontal">
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2 control-label">Name</label>
                <div class="col-sm-10">
                    <input id="eventName" type="text" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2 control-label">Description</label>
                <div class="col-sm-10">
                    <textarea id="eventDesc" class="form-control" rows="3"></textarea>
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">Start</label>
                <div class="col-sm-10">
                    <span id="eventStart"></span>
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">End</label>
                <div class="col-sm-10">
                    <span id="eventEnd"></span>
                </div>
            </div>
            <input type="hidden" id="eventId" />
        </div>
    </div>
    <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; overflow: hidden" title="Add Event">
        <div class="form-horizontal">
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2 control-label">Name</label>
                <div class="col-sm-10">
                    <input id="addEventName" type="text" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2 control-label">Description</label>
                <div class="col-sm-10">
                    <textarea id="addEventDesc" class="form-control" rows="3"></textarea>
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">Start</label>
                <div class="col-sm-10">
                    <span id="addEventStartDate"></span>
                </div>
            </div>
            <div class="form-group">
                <label for="inputEmail3" class="col-sm-2">End</label>
                <div class="col-sm-10">
                    <span id="addEventEndDate"></span>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
</asp:Content>

