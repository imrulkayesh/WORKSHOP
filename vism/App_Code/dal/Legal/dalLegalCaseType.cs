
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLegalCaseType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLegalCaseType()
    {
    }
    public int Insert(string casetype, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_CASETYPE(ID,CASETYPE,REMARKS,CREATEDBY)
        VALUES(SQ_LEG_CASETYPE.NEXTVAL,'" + casetype + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string casetype, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_CASETYPE set CASETYPE='" + casetype + "', REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_CASETYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string casetype)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_CASETYPE WHERE CASETYPE='" + casetype + "'");
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

