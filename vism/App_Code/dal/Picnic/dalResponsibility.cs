using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalResponsibility
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalResponsibility()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into PC_RESPONSIBILITY(ID,RESPONSIBILITY)
        VALUES(SQ_PC_RESPONSIBILITY.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update PC_RESPONSIBILITY set RESPONSIBILITY='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PC_RESPONSIBILITY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PC_RESPONSIBILITY WHERE RESPONSIBILITY='" + name + "'");
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