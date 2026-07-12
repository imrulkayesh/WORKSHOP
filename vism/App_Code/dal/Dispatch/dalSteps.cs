using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalSteps
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalSteps()
	{
    }
    public int Insert(string name,int priority,int IssueTypeId, int isFinalStep)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_STEPS(ID,STEPNAME,PRIORITY,ISSUETYPEID,ISFINALSTEP)
        VALUES(SQ_DP_STEPS.NEXTVAL,'" + name + "'," + priority + "," + IssueTypeId + ",'" + isFinalStep + "')");
    }
    public int Update(int id, string name, int priority, int IssueTypeId, int isFinalStep)
    {
        return dm.ExecuteNonQuery("update DP_STEPS set STEPNAME='" + name + "',PRIORITY=" + priority + ",ISSUETYPEID=" + IssueTypeId + ",ISFINALSTEP=" + isFinalStep + " WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from DP_STEPS WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name, int IssueType)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_STEPS WHERE STEPNAME='" + name + "' and ISSUETYPEID=" + IssueType);
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