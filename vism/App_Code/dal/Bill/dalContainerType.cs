using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalContainerType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalContainerType()
	{
	}
    public int Insert(string contype, int capacity)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_CONTAINERTYPE(ID,CONTAINERTYPE,CAPACITY,CREATEDBY)
        VALUES(SQ_BIL_CONTAINERTYPE.NEXTVAL,'" + contype + "','" + capacity + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string contype, int capacity)
    {
        return dm.ExecuteNonQuery("update BIL_CONTAINERTYPE set CONTAINERTYPE='" + contype + "',CAPACITY='" + capacity + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_CONTAINERTYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string cmode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_CONTAINERTYPE WHERE CONTAINERTYPE='" + cmode + "'");
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