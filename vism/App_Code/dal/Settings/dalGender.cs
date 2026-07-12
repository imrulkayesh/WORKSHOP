
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalGender
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalGender()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_GENDER(ID,GENDERNAME)
        VALUES(SQ_BS_GENDER.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update BS_GENDER set GENDERNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_GENDER WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_GENDER WHERE GENDERNAME='" + name + "'");
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