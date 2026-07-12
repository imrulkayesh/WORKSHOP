using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalCategory
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCategory()
    {
    }
    public int Insert(string name, int parentid)
    {
        return dm.ExecuteNonQuery(@"Insert into ASSET_CATEGORY(ID,CATEGORYNAME,PARENTID)
        VALUES(SQ_ASSET_CATEGORY.NEXTVAL,'" + name + "','" + parentid + "')");
    }
    public int Update(int id, string name, int parentid)
    {
        return dm.ExecuteNonQuery("update ASSET_CATEGORY set CATEGORYNAME='" + name + "',PARENTID=" + parentid + " where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from ASSET_CATEGORY where ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery("Select C.*,D.CATEGORYNAME PARENT from ASSET_CATEGORY C LEFT OUTER JOIN ASSET_CATEGORY D ON D.ID=C.PARENTID");
    }
    public DataTable GetByParent()
    {
        return dm.ExecuteQuery("Select * from ASSET_CATEGORY where PARENTID=0");
    }
    public DataTable GetByParentId(int ParentId)
    {
        return dm.ExecuteQuery("Select * from ASSET_CATEGORY where PARENTID=" + ParentId);
    }
    public bool CheckDuplicate(string name, int ID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from ASSET_CATEGORY WHERE CATEGORYNAME='" + name + "' and PARENTID=" + ID);
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