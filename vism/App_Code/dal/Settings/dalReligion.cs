
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalReligion
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalReligion()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_RELIGION(ID,RELIGIONNAME)
        VALUES(SQ_BS_RELIGION.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update BS_RELIGION set RELIGIONNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_RELIGION WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_RELIGION WHERE RELIGIONNAME='" + name + "'");
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