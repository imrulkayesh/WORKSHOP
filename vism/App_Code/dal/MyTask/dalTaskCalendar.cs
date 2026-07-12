using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

public class dalTaskCalendar
{
    private static string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
        List<CalendarEvent> events = new List<CalendarEvent>();
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("SELECT * FROM MYT_TASK where ASSIGNEDTO='" + SessionManager.SessionName.StaffId + "' AND TASKSTART>=TO_DATE('" + start + "','MM/dd/yyyy hh:mi:ss am') AND TASKEND<=TO_DATE('" + end + "','MM/dd/yyyy hh:mi:ss am')", con);

        using (con)
        {
            con.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = Convert.ToInt32(reader["ID"]);
                cevent.title = reader["TASKTITLE"].ToString();
                cevent.description = reader["DETAILS"].ToString();
                cevent.start = Convert.ToDateTime(reader["TASKSTART"]);
                cevent.end = Convert.ToDateTime(reader["TASKEND"]);
                cevent.allDay = Convert.ToBoolean(reader["ISFULLDAY"]);
                events.Add(cevent);
            }
        }
        return events;
    }
    public static void updateEvent(int id, String title, String description)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("UPDATE MYT_TASK SET TASKTITLE='" + title + "', DETAILS='" + description + "' WHERE ID='" + id + "'", con);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
            SessionManager.SessionName.MyTaskId = id;
        }
    }
    public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("UPDATE MYT_TASK SET TASKSTART=TO_DATE('" + start + "','MM/dd/yyyy hh:mi:ss am'), TASKEND=TO_DATE('" + end + "','MM/dd/yyyy hh:mi:ss am'), ISFULLDAY='" + Convert.ToInt32(allDay) + "' WHERE ID='" + id + "'", con);

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }

    }

    public static void deleteEvent(int id)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("DELETE FROM MYT_TASK WHERE ID='" + id + "'", con);

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static int addEvent(CalendarEvent cevent)
    {
        int key = 0;
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(@"INSERT INTO MYT_TASK(ID,TASKTITLE,ASSIGNEDTO,CREATEDBY, DETAILS, TASKSTART, TASKEND, ISFULLDAY) 
            VALUES(SQ_MYT_TASK.NEXTVAL,'" + cevent.title + "','" + SessionManager.SessionName.StaffId + "','" + SessionManager.SessionName.StaffId + "', '" + cevent.description + "', TO_DATE('" + cevent.start + "','MM/dd/yyyy hh:mi:ss am'), TO_DATE('" + cevent.end + "','MM/dd/yyyy hh:mi:ss am'), '" + Convert.ToInt32(cevent.allDay) + "')", con);


        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
            cmd = new OracleCommand("SELECT MAX(ID) FROM MYT_TASK where CREATEDBY='" + SessionManager.SessionName.StaffId + "'", con);
            key = string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()) ? 0 : Convert.ToInt32(cmd.ExecuteScalar());
            SessionManager.SessionName.MyTaskId = key;
        }
        return key;
    }
}