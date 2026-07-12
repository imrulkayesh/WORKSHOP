using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalChargesName
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalChargesName()
    {
    }
    public int Insert(string cname)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_CHARGENAME(ID,CHARGENAME,CREATEDBY)
        VALUES(SQ_RVS_CHARGENAME.NEXTVAL,'" + cname + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string cname )
    {
        return dm.ExecuteNonQuery("update RVS_CHARGENAME set CHARGENAME='" + cname + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_CHARGENAME WHERE ID=" + id);
    }
    public bool CheckDuplicate(string cname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_CHARGENAME WHERE CHARGENAME='" + cname + "'");
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