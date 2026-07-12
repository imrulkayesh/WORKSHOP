using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLWColor
{
   DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalLWColor()
	{
	}
    public int Insert(int lineId, int colorId, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_COLOR(ID,LINEID,COLORID,REMARKS,CREATEDBY)
        VALUES(SQ_PR_COLOR.NEXTVAL,'" + lineId + "','" + colorId + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int lineId, int colorId, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_COLOR set LINEID='" + colorId + "',COLORID='" + colorId + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_COLOR WHERE ID=" + id);
    }
    public DataTable GetLWColor()
    {
        return dm.ExecuteQuery(@"SELECT CO.*, ML.LINENAME, MO.COLORNAME FROM PR_COLOR CO
        INNER JOIN PR_MLINE ML ON ML.ID = CO.LINEID
        INNER JOIN PR_MCOLOR MO ON MO.ID = CO.COLORID");
    }
    public bool CheckDuplicate(int lineId, int colorId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_COLOR WHERE LINEID='" + lineId + "' AND COLORID='" + colorId + "'");
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