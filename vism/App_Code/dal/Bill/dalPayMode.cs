using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalPayMode
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalPayMode()
	{
	}
    public int Insert(string paymode)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_PAYMODE(ID,PAYMODE,CREATEDBY)
        VALUES(SQ_BIL_PAYMODE.NEXTVAL,'" + paymode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string paymode)
    {
        return dm.ExecuteNonQuery("update BIL_PAYMODE set PAYMODE='" + paymode + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_PAYMODE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string paymode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_PAYMODE WHERE PAYMODE='" + paymode + "'");
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