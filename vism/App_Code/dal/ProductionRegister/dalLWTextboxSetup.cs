using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLWTextboxSetup
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLWTextboxSetup()
    {
    }
    public int Insert(int locationId, int lineId, int labelId, int textboxId, int active)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_LINEWISETBP(LOCATIONID,LINEID,LABELID,TEXTBOXID,ACTIVE,CREATEDBY)
        VALUES('" + locationId + "','" + lineId + "','" + labelId + "','" + textboxId + "','" + active + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int lineId, int labelId, int textboxId)
    {
        return dm.ExecuteNonQuery("update PR_LINEWISETBP set LINEID='" + lineId + "',LABELID='" + labelId + "',TEXTBOXID='" + textboxId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public int Delete(int Lineid)
    {
        return dm.ExecuteNonQuery(@"DELETE FROM PR_LINEWISETBP WHERE LINEID=" + Lineid);
    }

    public DataTable GetAll(int lineId)
    {
        return dm.ExecuteQuery(@"SELECT LT.*, TB.LABELNAME, TB.TEXTBOXNAME FROM PR_LINEWISETBP LT 
        INNER JOIN PR_TEXTBOX TB ON TB.ID = LT.TEXTBOXID 
        WHERE LT.LINEID=" + lineId);
    }
    public DataTable loadTextboxlist(int lineId)
    {
        return dm.ExecuteQuery(@"SELECT LT.*, TB.LABELNAME, TB.TEXTBOXNAME FROM PR_LINEWISETBP LT 
        INNER JOIN PR_TEXTBOX TB ON TB.ID = LT.TEXTBOXID 
        WHERE LT.LINEID = " + lineId);
    }
    public DataTable loadLWTextbox(int lineId)
    {
        return dm.ExecuteQuery(@"SELECT LT.*, TB.LABELNAME, TB.TEXTBOXNAME, BS.CENTERNAME FROM PR_LINEWISETBP LT 
        INNER JOIN PR_TEXTBOX TB ON TB.ID = LT.TEXTBOXID 
        INNER JOIN BS_BUSINESSCENTER BS ON BS.ID = LT.LOCATIONID
        WHERE LT.LINEID = " + lineId);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_LINEWISETBP WHERE ID=" + id);
    }
    public DataTable GetLoadTextbox()
    {
        return dm.ExecuteQuery(@"");
    }
    public bool CheckDuplicate(int lineId, int machineId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_LINEWISETBP WHERE LINEID='" + lineId + "' AND MACHINEID='" + machineId + "'");
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