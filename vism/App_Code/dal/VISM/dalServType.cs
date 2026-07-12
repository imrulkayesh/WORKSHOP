using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalServType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalServType()
    {
    }
    public int Insert(string servtype, int custobill, int coder)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_SERVICETYPE(ID,SERVICETYPE,CUSTOMERBILL,CORDER,CREATEDBY)
        VALUES(SQ_RVS_SERVICETYPE.NEXTVAL,'" + servtype + "','" + custobill + "','" + coder + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string servtype, int custobill, int coder)
    {
        return dm.ExecuteNonQuery("update RVS_SERVICETYPE set SERVICETYPE='" + servtype + "',CUSTOMERBILL='" + custobill + "',CORDER='" + coder + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_SERVICETYPE WHERE ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery("Select * from RVS_SERVICETYPE");
    }

    public bool CheckDuplicate( string servtype)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_SERVICETYPE WHERE SERVICETYPE='" + servtype + "'");
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