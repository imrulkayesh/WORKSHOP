using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalVehicleCondition
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVehicleCondition()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_VEHICLECONDITION(ID,CONDITIONNAME)
        VALUES(SQ_RVS_VEHICLECONDITION.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_VEHICLECONDITION set CONDITIONNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_VEHICLECONDITION WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_VEHICLECONDITION WHERE CONDITIONNAME='" + name + "'");
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