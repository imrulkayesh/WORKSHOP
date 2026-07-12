using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalMTFoodItems
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMTFoodItems()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into MT_FOODS(ID,FOODTITLE)
        VALUES(SQ_MT_FOODS.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update MT_FOODS set FOODTITLE='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MT_FOODS WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MT_FOODS WHERE FOODTITLE='" + name + "'");
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