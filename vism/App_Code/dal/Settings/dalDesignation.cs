
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalDesignation
/// </summary>
public class dalDesignation
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDesignation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(string name, int priority)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_DESIGNATION(ID,DESIGNATIONNAME,PRIORITY,CREATEDBY)
        VALUES(SQ_BS_DESIGNATION.NEXTVAL,'" + name + "'," + priority + ",'" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, int priority)
    {
        return dm.ExecuteNonQuery("update BS_DESIGNATION set DESIGNATIONNAME='" + name + "',PRIORITY=" + priority + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE= TO_DATE('" + DateTime.Now + "','MM/dd/yyyy HH:MI:SSAM') WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_DESIGNATION WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_DESIGNATION WHERE DESIGNATIONNAME='" + name + "'");
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