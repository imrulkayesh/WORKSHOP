using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMQuality
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalMQuality()
	{
	}
    public int Insert(string quality)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MQUALITY(ID,QUALITYNAME,CREATEDBY)
        VALUES(SQ_PR_MQUALITY.NEXTVAL,'" + quality + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string quality)
    {
        return dm.ExecuteNonQuery("update PR_MQUALITY set QUALITYNAME='" + quality + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MQUALITY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string quality)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MQUALITY WHERE QUALITYNAME='" + quality + "'");
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