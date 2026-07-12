using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalVISMPayType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVISMPayType()
    {
    }
    public int Insert(string sname)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_PAYTYPE(ID,PAYTYPE,CREATEDBY)
        VALUES(SQ_RVS_PAYTYPE.NEXTVAL,'" + sname + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string sname)
    {
        return dm.ExecuteNonQuery("update RVS_PAYTYPE set PAYTYPE='" + sname + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_PAYTYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string sname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_PAYTYPE WHERE PAYTYPE='" + sname + "'");
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