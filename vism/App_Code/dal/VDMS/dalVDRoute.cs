using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVDRoute
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVDRoute()
    {
    }
    public int Insert(string route, decimal distance, int buid)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_ROUTE(ID,ROUTENAME,DISTANCE,BUSINESSCENTERID,CREATEDBY)
        VALUES(SQ_VD_ROUTE.NEXTVAL,'" + route + "','" + distance + "','" + buid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string route, decimal distance, int buid)
    {
        return dm.ExecuteNonQuery("update VD_ROUTE set ROUTENAME='" + route + "',BUSINESSCENTERID='" + buid + "',DISTANCE='" + distance + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_ROUTE WHERE ID=" + id);
    }
  
    public DataTable GetByBusinessCenter()
    {
        return dm.ExecuteQuery("Select * from VD_ROUTE ");
    }
    public bool CheckDuplicate(string route, int Buid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_ROUTE WHERE ROUTENAME='" + route + "' AND BUSINESSCENTERID=" + Buid);
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