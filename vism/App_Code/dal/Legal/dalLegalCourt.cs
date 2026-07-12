using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLegalCourt
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLegalCourt()
    {
    }
    public int Insert(string court, int place)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_COURT(ID,COURTNAME,PLACEID,CREATEDBY)
        VALUES(SQ_LEG_COURT.NEXTVAL,'" + court + "','" + place + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string court, int place)
    {
        return dm.ExecuteNonQuery("update LEG_COURT set COURTNAME='" + court + "',PLACEID='" + place + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_COURT WHERE ID=" + id);
    }
    public DataTable GetALL(int place)
    {
        return dm.ExecuteQuery(@"Select L.*, P.PLACENAME from LEG_COURT L
        INNER JOIN LEG_PLACE P ON P.ID = L.PLACEID WHERE L.PLACEID=" + place);
    }
    public DataTable GetByPlace()
    {
        return dm.ExecuteQuery(@"Select L.*, P.PLACENAME from LEG_COURT L
        LEFT OUTER JOIN LEG_PLACE P ON P.ID = L.PLACEID");
    }
    public bool CheckDuplicate(string court, int place)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_COURT WHERE COURTNAME='" + court + "' AND PLACEID='" + place + "'");
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
