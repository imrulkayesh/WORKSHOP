using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalTaskType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalTaskType()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into MYT_TASKTYPE(ID,TASKTYPE)
        VALUES(SQ_MYT_TASKTYPE.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update MYT_TASKTYPE set TASKTYPE='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MYT_TASKTYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MYT_TASKTYPE WHERE TASKTYPE='" + name + "'");
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