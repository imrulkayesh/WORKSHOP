using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMMachine
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalMMachine()
	{
	}

    public int Insert(string machine)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MMACHINE(ID,MACHINENAME,CREATEDBY)
        VALUES(SQ_PR_MMACHINE.NEXTVAL,'" + machine + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string machine)
    {
        return dm.ExecuteNonQuery("update PR_MMACHINE set MACHINENAME='" + machine + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MMACHINE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string machine)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MMACHINE WHERE MACHINENAME='" + machine + "'");
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