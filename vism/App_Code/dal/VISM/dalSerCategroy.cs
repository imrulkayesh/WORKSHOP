using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalSerCategroy
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalSerCategroy()
    {
    }
    public int Insert(string servcateg, int coder)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_SERVICECATEGORY(ID,CATNAME,CORDER,CREATEDBY)
        VALUES(SQ_RVS_SERVICECATEGORY.NEXTVAL,'" + servcateg + "','" + coder + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string servcateg, int coder)
    {
        return dm.ExecuteNonQuery("update RVS_SERVICECATEGORY set CATNAME='" + servcateg + "',CORDER='" + coder + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_SERVICECATEGORY WHERE ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery("Select * from RVS_SERVICECATEGORY");
    }

    public bool CheckDuplicate(string servcateg)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_SERVICECATEGORY WHERE CATNAME='" + servcateg + "'");
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