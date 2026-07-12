using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalTransport
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalTransport()
	{
    }
    public int Insert(string name, int isExternal)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_TRANSPORT(ID,TRANSPORTNAME,ISEXTERNAL)
        VALUES(SQ_DP_TRANSPORT.NEXTVAL,'" + name + "','" + isExternal + "')");
    }
    public int Update(int id, string name, int isExternal)
    {
        return dm.ExecuteNonQuery("update DP_TRANSPORT set TRANSPORTNAME='" + name + "',ISEXTERNAL=" + isExternal + " WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from DP_TRANSPORT WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_TRANSPORT WHERE TRANSPORTNAME='" + name + "'");
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