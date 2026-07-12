using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLWShift
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLWShift()
    {
    }
    public int Insert(int lineId, int shiftId, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MSHIFT(ID,LINEID,SHIFTID,REMARKS,CREATEDBY)
        VALUES(SQ_PR_SHIFT.NEXTVAL,'" + lineId + "','" + shiftId + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int lineId, int shiftId, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_SHIFT set LINEID='" + lineId + "',SHIFTID='" + shiftId + "',REMARKS='"+remarks+"',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_SHIFT WHERE ID=" + id);
    }
    public DataTable GetLWShift()
    {
        return dm.ExecuteQuery(@"SELECT S.*, ML.LINENAME, MS.SHIFTNAME FROM PR_SHIFT S
        INNER JOIN PR_MLINE ML ON ML.ID = S.LINEID
        INNER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID ");
    }
    public bool CheckDuplicate(int lineId, int shiftId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_SHIFT WHERE LINEID='" + lineId + "' AND SHIFTID='" + shiftId + "'");
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