using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalBodyType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalBodyType()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_BODYTYPE(ID,BODYTYPENAME)
        VALUES(SQ_RVS_BODYTYPE.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_BODYTYPE set BODYTYPENAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_BODYTYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_BODYTYPE WHERE BODYTYPENAME='" + name + "'");
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