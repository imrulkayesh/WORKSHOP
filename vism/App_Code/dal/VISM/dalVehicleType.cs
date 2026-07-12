using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVehicleType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVehicleType()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_VEHICLETYPE(ID,VEHICLETYPENAME)
        VALUES(SQ_RVS_VEHICLETYPE.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_VEHICLETYPE set VEHICLETYPENAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_VEHICLETYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_VEHICLETYPE WHERE VEHICLETYPENAME='" + name + "'");
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