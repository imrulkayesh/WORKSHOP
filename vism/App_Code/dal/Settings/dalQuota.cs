using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalQuota
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalQuota()
    {
    }


    public int Insert(string name, decimal Percentage)
    {
        dm.AddParameteres("@Quota", name);
        dm.AddParameteres("@Percentage", Percentage);
        return dm.ExecuteNonQuery("USP_Quota_Insert");
    }
    public int Update(int id, string name, decimal Percentage)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Quota", name);
        dm.AddParameteres("@Percentage", Percentage);
        return dm.ExecuteNonQuery("USP_Quota_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Quota_GetById");
    }
}