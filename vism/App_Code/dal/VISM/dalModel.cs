using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalModel
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalModel()
	{
	}
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_MODEL(ID,MODELNAME)
        VALUES(SQ_RVS_MODEL.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_MODEL set MODELNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_MODEL WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_MODEL WHERE MODELNAME='" + name + "'");
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