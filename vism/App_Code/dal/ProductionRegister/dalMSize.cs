using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMSize
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalMSize()
	{
	}
    public int Insert(string size)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MSIZE(ID,SIZENAME,CREATEDBY)
        VALUES(SQ_PR_MSIZE.NEXTVAL,'" + size + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string size)
    {
        return dm.ExecuteNonQuery("update PR_MSIZE set SIZENAME='" + size + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MSIZE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string size)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MSIZE WHERE SIZENAME='" + size + "'");
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