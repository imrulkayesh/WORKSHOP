using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Outlook;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //private void ThisAddIn_Startup(object sender, System.EventArgs e)
    //{
    //    AppointmentItem agendaMeeting = (AppointmentItem) this.Application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olAppointmentItem);

    //    if (agendaMeeting != null)
    //    {
    //        agendaMeeting.MeetingStatus =
    //            Microsoft.Office.Interop.Outlook.OlMeetingStatus.olMeeting;
    //        agendaMeeting.Location = "Conference Room";
    //        agendaMeeting.Subject = "Discussing the Agenda";
    //        agendaMeeting.Body = "Let's discuss the agenda.";
    //        agendaMeeting.Start = new DateTime(2016, 5, 5, 5, 0, 0);
    //        agendaMeeting.Duration = 60;
    //        Recipient recipient =
    //            agendaMeeting.Recipients.Add("Nate Sun");
    //        recipient.Type =
    //            (int)OlMeetingRecipientType.olRequired;
    //        ((_AppointmentItem)agendaMeeting).Send();
    //    }
    //}
}