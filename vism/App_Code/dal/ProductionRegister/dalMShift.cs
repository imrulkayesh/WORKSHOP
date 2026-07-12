using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMShift
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalMShift()
	{
	}
    public int Insert(string shift)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MSHIFT(ID,SHIFTNAME,CREATEDBY)
        VALUES(SQ_PR_MSHIFT.NEXTVAL,'" + shift + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string shift)
    {
        return dm.ExecuteNonQuery("update PR_MSHIFT set SHIFTNAME='" + shift + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MSHIFT WHERE ID=" + id);
    }
    public bool CheckDuplicate(string shift)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MSHIFT WHERE SHIFTNAME='" + shift + "'");
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