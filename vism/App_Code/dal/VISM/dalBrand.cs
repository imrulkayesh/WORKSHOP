using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalBrand
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalBrand()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_BRAND(ID,BRANDNAME)
        VALUES(SQ_RVS_BRAND.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_BRAND set BRANDNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_BRAND WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_BRAND WHERE BRANDNAME='" + name + "'");
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