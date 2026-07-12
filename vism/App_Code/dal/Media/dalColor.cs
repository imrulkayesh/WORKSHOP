using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalColor
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalColor()
    {
    }
    public int Insert(string color)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_COLOR(ID,COLORNAME,CREATEDBY)
        VALUES(SQ_MD_COLOR.NEXTVAL,'" + color + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string color)
    {
        return dm.ExecuteNonQuery("update MD_COLOR set COLORNAME='" + color + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAll(int catid)
    {
        return dm.ExecuteQuery("Select * from MD_COLOR");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_COLOR WHERE ID=" + id);
    }
    public bool CheckDuplicate(string color)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_COLOR WHERE COLORNAME='" + color + "'");
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
