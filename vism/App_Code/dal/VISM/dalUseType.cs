using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalUseType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalUseType()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_USETYPE(ID,USETYPENAME)
        VALUES(SQ_RVS_USETYPE.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_USETYPE set USETYPENAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_USETYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_USETYPE WHERE USETYPENAME='" + name + "'");
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