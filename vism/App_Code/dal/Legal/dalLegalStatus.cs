using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLegalStatus
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLegalStatus()
    {
    }
    public int Insert(string status, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_STATUS(ID,STATUSNAME,REMARKS,CREATEDBY)
        VALUES(SQ_LEG_STATUS.NEXTVAL,'" + status + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string status, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_STATUS set STATUSNAME='" + status + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_STATUS WHERE ID=" + id);
    }
    public bool CheckDuplicate(string status)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_STATUS WHERE STATUSNAME='" + status + "'");
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