using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalUnit
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalUnit()
	{
    }
    public int Insert(string name, string code)
    {
        return dm.ExecuteNonQuery(@"Insert into ASSET_UNIT(ID,UNITNAME,UNITCODE)
        VALUES(SQ_ASSET_UNIT.NEXTVAL,'" + name + "','" + code + "')");
    }
    public int Update(int id, string name, string code)
    {
        return dm.ExecuteNonQuery("update ASSET_UNIT set UNITNAME='" + name + "',UNITCODE='" + code + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from ASSET_UNIT WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from ASSET_UNIT WHERE UNITNAME='" + name + "'");
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