using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLWIncharge
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalLWIncharge()
	{
	}
    public int Insert(int lineid, int inchargeid, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_INCHARGE(ID,LINEID,INCHARGEID,REMARKS,CREATEDBY)
        VALUES(SQ_PR_INCHARGE.NEXTVAL,'" + lineid + "','" + inchargeid + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int lineid, int inchargeid, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_INCHARGE set LINEID='" + lineid + "',INCHARGEID='" + inchargeid + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PR_INCHARGE WHERE ID=" + id);
    }
    public DataTable GetLWItem()
    {
        return dm.ExecuteQuery(@"SELECT IR.*, MI.INCHARGENAME, L.LINENAME FROM PR_INCHARGE IR
        INNER JOIN PR_MINCHARGE MI ON MI.ID = IR.INCHARGEID
        INNER JOIN PR_MLINE L ON L.ID = IR.LINEID");
    }
    public bool CheckDuplicate(int lineid, int inchargeid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_INCHARGE WHERE LINEID='" + lineid + "' AND INCHARGEID='" + inchargeid + "'");
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