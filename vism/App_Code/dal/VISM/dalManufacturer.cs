using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalManufacturer
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalManufacturer()
	{
	}
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_MANUFACTURER(ID,MANUFACTURERNAME)
        VALUES(SQ_RVS_MANUFACTURER.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_MANUFACTURER set MANUFACTURERNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_MANUFACTURER WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_MANUFACTURER WHERE MANUFACTURERNAME='" + name + "'");
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