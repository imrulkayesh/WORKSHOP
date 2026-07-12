using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalRegLcoation
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalRegLcoation()
    {
    }
    public int Insert(string name, string Address)
    {
        return dm.ExecuteNonQuery(@"Insert into PC_REGLOCATION(ID,LOCATIONNAME,ADDRESS)
        VALUES(SQ_PC_REGLOCATION.NEXTVAL,'" + name + "','" + Address + "')");
    }
    public int Update(int id, string name,string Address)
    {
        return dm.ExecuteNonQuery("update PC_REGLOCATION set LOCATIONNAME='" + name + "',ADDRESS='" + Address + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PC_REGLOCATION WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PC_REGLOCATION WHERE LOCATIONNAME='" + name + "'");
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