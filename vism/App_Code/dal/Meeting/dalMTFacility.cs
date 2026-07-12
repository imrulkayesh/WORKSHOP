using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalMTFacility
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMTFacility()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into MT_FACILITY(ID,FACILITYNAME,CREATEDBY)
        VALUES(SQ_MT_FACILITY.NEXTVAL,'" + name + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update MT_FACILITY set FACILITYNAME='" + name + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MT_FACILITY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MT_FACILITY WHERE FACILITYNAME='" + name + "'");
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