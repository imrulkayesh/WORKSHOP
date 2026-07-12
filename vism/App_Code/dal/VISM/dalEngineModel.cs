using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
public class dalEngineModel
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalEngineModel()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_ENGINEMODEL(ID,ENGINEMODELNAME)
        VALUES(SQ_RVS_ENGINEMODEL.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_ENGINEMODEL set ENGINEMODELNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_ENGINEMODEL WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_ENGINEMODEL WHERE ENGINEMODELNAME='" + name + "'");
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