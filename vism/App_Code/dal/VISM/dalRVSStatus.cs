using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalRVSStatus
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalRVSStatus()
    {
    }
    public int Insert(string sname)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_STATUS(ID,STATUSNAME,CREATEDBY)
        VALUES(SQ_RVS_STATUS.NEXTVAL,'" + sname + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string sname)
    {
        return dm.ExecuteNonQuery("update RVS_STATUS set STATUSNAME='" + sname + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_STATUS WHERE ID=" + id);
    }
    public bool CheckDuplicate(string sname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_STATUS WHERE STATUSNAME='" + sname + "'");
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