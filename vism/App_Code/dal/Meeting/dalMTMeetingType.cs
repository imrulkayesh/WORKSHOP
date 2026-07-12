using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalMTMeetingType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMTMeetingType()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into MT_MEETINGTYPE(ID,MTYPENAME)
        VALUES(SQ_MT_MEETINGTYPE.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update MT_MEETINGTYPE set MTYPENAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MT_MEETINGTYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MT_MEETINGTYPE WHERE MTYPENAME='" + name + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}