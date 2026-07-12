using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalStatus
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalStatus()
	{
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_STATUS(ID,STATUSNAME)
        VALUES(SQ_DP_STATUS.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update DP_STATUS set STATUSNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from DP_STATUS WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_STATUS WHERE STATUSNAME='" + name + "'");
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