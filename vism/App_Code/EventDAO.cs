using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;


public class EventDAO
{
    private static string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
        List<CalendarEvent> events = new List<CalendarEvent>();
        OracleConnection con = new OracleConnection(connectionString);
        //OracleCommand cmd = new OracleCommand("SELECT ID,MEETINGTITLE,AGENDA,STARTTIME,ENDTIME,ALLDAY FROM MT_MEETING where ISAPPROVED<2 AND MROOMID='" + SessionManager.SessionName.MRoomId + "' AND STARTTIME>=TO_DATE('" + start + "','MM/dd/yyyy hh:mi:ss am') AND ENDTIME<=TO_DATE('" + end + "','MM/dd/yyyy hh:mi:ss am')", con);
        OracleCommand cmd = new OracleCommand("SELECT ID,MEETINGTITLE,AGENDA,STARTTIME,ENDTIME,ALLDAY FROM MT_MEETING where ISAPPROVED<2 AND MROOMID='" + SessionManager.SessionName.MRoomId + "' AND STARTTIME>=TO_DATE('" + start + "','dd/MM/yyyy hh:mi:ss am') AND ENDTIME<=TO_DATE('" + end + "','dd/MM/yyyy hh:mi:ss am')", con);


        using (con)
        {
            con.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = Convert.ToInt32(reader["ID"]);
                cevent.title = reader["MEETINGTITLE"].ToString();
                cevent.description = reader["AGENDA"].ToString();
                cevent.start = Convert.ToDateTime(reader["STARTTIME"]);
                cevent.end = Convert.ToDateTime(reader["ENDTIME"]);
                cevent.allDay = Convert.ToBoolean(reader["ALLDAY"]);
                events.Add(cevent);
            }
        }
        return events;
    }
    public static void updateEvent(int id, String title, String description)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("UPDATE MT_MEETING SET MEETINGTITLE='" + title + "', AGENDA='" + description + "' WHERE ID='" + id + "'", con);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
            SessionManager.SessionName.MeetingId = id;
        }
    }
    public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("UPDATE MT_MEETING SET STARTTIME=TO_DATE('" + start + "','MM/dd/yyyy hh:mi:ss am'), ENDTIME=TO_DATE('" + end + "','MM/dd/yyyy hh:mi:ss am'), ALLDAY='" + Convert.ToInt32(allDay) + "' WHERE ID='" + id + "'", con);

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        
    }

    public static void deleteEvent(int id)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("UPDATE MT_MEETING SET ISAPPROVED=2 WHERE ID='" + id + "'", con);
       
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static int addEvent(CalendarEvent cevent)
    {
        int key = 0;
        if (SessionManager.SessionName.MRoomId > 0)
        {
            OracleConnection con = new OracleConnection(connectionString);
            //OracleCommand cmd = new OracleCommand(@"INSERT INTO MT_MEETING(ID,MROOMID,CREATEDBY,MEETINGTITLE, AGENDA, STARTTIME, ENDTIME, ALLDAY) 
            //VALUES(SQ_MT_MEETING.NEXTVAL,'" + SessionManager.SessionName.MRoomId + "','" + SessionManager.SessionName.StaffId + "','" + cevent.title + "', '" + cevent.description + "', TO_DATE('" + cevent.start + "','MM/dd/yyyy hh:mi:ss am'), TO_DATE('" + cevent.end + "','MM/dd/yyyy hh:mi:ss am'), '" + Convert.ToInt32(cevent.allDay) + "')", con);
            OracleCommand cmd = new OracleCommand(@"INSERT INTO MT_MEETING(ID,MROOMID,CREATEDBY,MEETINGTITLE, AGENDA, STARTTIME, ENDTIME, ALLDAY) 
            VALUES(SQ_MT_MEETING.NEXTVAL,'" + SessionManager.SessionName.MRoomId + "','" + SessionManager.SessionName.StaffId + "','" + cevent.title + "', '" + cevent.description + "', TO_DATE('" + cevent.start + "','dd/MM/yyyy hh:mi:ss am'), TO_DATE('" + cevent.end + "','dd/MM/yyyy hh:mi:ss am'), '" + Convert.ToInt32(cevent.allDay) + "')", con);

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                cmd = new OracleCommand("SELECT MAX(ID) FROM MT_MEETING where CREATEDBY='" + SessionManager.SessionName.StaffId + "' AND MROOMID='" + SessionManager.SessionName.MRoomId + "'", con);
                key = string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()) ? 0 : Convert.ToInt32(cmd.ExecuteScalar());
                SessionManager.SessionName.MeetingId = key;
            }

        }
        else
        {
            key = -1;
        }
        return key;
    }
}
