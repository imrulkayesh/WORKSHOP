using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalCollectionMode
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalCollectionMode()
	{
	}
    public int Insert(string cmode)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_COLLECTIONMODE(ID,COLLECTIONMODE,CREATEDBY)
        VALUES(SQ_BIL_COLLECTIONMODE.NEXTVAL,'" + cmode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string cmode)
    {
        return dm.ExecuteNonQuery("update BIL_COLLECTIONMODE set COLLECTIONMODE='" + cmode + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_COLLECTIONMODE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string cmode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_COLLECTIONMODE WHERE COLLECTIONMODE='" + cmode + "'");
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