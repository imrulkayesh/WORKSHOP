
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalAccessoriesCategory
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalAccessoriesCategory()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into SC_ACCESSORIESCATEGORY(ID,ACCESSORIESCATEGORY)
        VALUES(SQ_SC_ACCESSORIESCATEGORY.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update SC_ACCESSORIESCATEGORY set ACCESSORIESCATEGORY='" + name + "' where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from SC_ACCESSORIESCATEGORY where ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_ACCESSORIESCATEGORY WHERE ACCESSORIESCATEGORY='" + name + "'");
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