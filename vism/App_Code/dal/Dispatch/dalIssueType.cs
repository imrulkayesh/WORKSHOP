using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalIssueType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalIssueType()
	{
    }
    public int Insert(string name, int isReturnable, int IsExternal, int IsPersonal)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_ISSUETYPE(ID,ISSUETYPENAME,RETURNABLE,ISEXTERNAL,ISPERSONAL)
        VALUES(SQ_DP_ISSUETYPE.NEXTVAL,'" + name + "'," + isReturnable + "," + IsExternal + ",'" + IsPersonal + "')");
    }
    public int Update(int id, string name, int isReturnable, int IsExternal, int IsPersonal)
    {
        return dm.ExecuteNonQuery("update DP_ISSUETYPE set ISSUETYPENAME='" + name + "',RETURNABLE=" + isReturnable + ",ISEXTERNAL=" + IsExternal + ",ISPERSONAL=" + IsPersonal + " WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from DP_ISSUETYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_ISSUETYPE WHERE ISSUETYPENAME='" + name + "'");
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