
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalCaseTypeStep
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCaseTypeStep()
    {
    }
    public int Insert(int casetype, string stepname, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_CASETYPESTEPS(ID,CASETYPEID,STEPNAME,REMARKS,CREATEDBY)
        VALUES(SQ_LEG_CASETYPESTEPS.NEXTVAL,'" + casetype + "','" + stepname + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int casetype, string stepname, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_CASETYPESTEPS set CASETYPEID='" + casetype + "',STEPNAME='" + stepname + "', REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_CASETYPESTEPS WHERE ID=" + id);
    }
    public DataTable GetAllCaseType(int casetype)
    {
        return dm.ExecuteQuery(@"Select C.*, CA.CASETYPE from LEG_CASETYPESTEPS C
        INNER JOIN LEG_CASETYPE CA ON CA.ID = C.CASETYPEID WHERE  C.CASETYPEID='" + casetype + "'");
    }

    public DataTable GetAllCaseType()
    {
        return dm.ExecuteQuery(@"Select C.*, CA.CASETYPE from LEG_CASETYPESTEPS C
        INNER JOIN LEG_CASETYPE CA ON CA.ID = C.CASETYPEID ");
    }
    public bool CheckDuplicate(int casetype, string stepname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_CASETYPESTEPS WHERE CASETYPEID='" + casetype + "' AND STEPNAME='" + stepname + "'");
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