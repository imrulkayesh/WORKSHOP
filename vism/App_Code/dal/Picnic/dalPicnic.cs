using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

public class dalPicnic
{
    private static string connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
        List<CalendarEvent> events = new List<CalendarEvent>();
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("SELECT * FROM PC_SCHEDULE where STARTDATE>=TO_DATE('" + start + "','MM/dd/yyyy hh:mi:ss am') AND ENDDATE<=TO_DATE('" + end + "','MM/dd/yyyy hh:mi:ss am')", con);

        using (con)
        {
            con.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = Convert.ToInt32(reader["ID"]);
                cevent.title = reader["PICNICTITLE"].ToString();
                cevent.description = reader["DESCRIPTION"].ToString();
                cevent.start = Convert.ToDateTime(reader["STARTDATE"]);
                cevent.end = Convert.ToDateTime(reader["ENDDATE"]);
                cevent.allDay = Convert.ToBoolean(reader["ALLDAY"]);
                events.Add(cevent);
            }
        }
        return events;
    }
    public static void updateEvent(int id, String title, String description)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("UPDATE PC_SCHEDULE SET PICNICTITLE='" + title + "', DESCRIPTION='" + description + "' WHERE ID='" + id + "'", con);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
            SessionManager.SessionName.PicnicId = id;
        }
    }
    public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("UPDATE PC_SCHEDULE SET STARTDATE=TO_DATE('" + start + "','MM/dd/yyyy hh:mi:ss am'), ENDDATE=TO_DATE('" + end + "','MM/dd/yyyy hh:mi:ss am'), ALLDAY='" + Convert.ToInt32(allDay) + "' WHERE ID='" + id + "'", con);

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }

    }

    public static void deleteEvent(int id)
    {
        OracleConnection con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand("DELETE FROM PC_SCHEDULE WHERE ID='" + id + "'", con);

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
        OracleCommand cmd = new OracleCommand(@"INSERT INTO PC_SCHEDULE(ID,PICNICTITLE,CREATEDBY, DESCRIPTION, STARTDATE, ENDDATE, ALLDAY) 
            VALUES(SQ_PC_SCHEDULE.NEXTVAL,'" + cevent.title + "','" + SessionManager.SessionName.StaffId + "', '" + cevent.description + "', TO_DATE('" + cevent.start + "','MM/dd/yyyy hh:mi:ss am'), TO_DATE('" + cevent.end + "','MM/dd/yyyy hh:mi:ss am'), '" + Convert.ToInt32(cevent.allDay) + "')", con);


        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
            cmd = new OracleCommand("SELECT MAX(ID) FROM PC_SCHEDULE where CREATEDBY='" + SessionManager.SessionName.StaffId + "'", con);
            key = string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()) ? 0 : Convert.ToInt32(cmd.ExecuteScalar());
            SessionManager.SessionName.PicnicId = key;
        }
        return key;
    }
}