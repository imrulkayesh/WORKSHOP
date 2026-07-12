using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalCostItem
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCostItem()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into PC_COSTITEM(ID,COSTITEM)
        VALUES(SQ_PC_COSTITEM.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update PC_COSTITEM set COSTITEM='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PC_COSTITEM WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PC_COSTITEM WHERE COSTITEM='" + name + "'");
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