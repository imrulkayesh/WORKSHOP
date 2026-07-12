using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMColor
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMColor()
    {
    }
    public int Insert(string colorName, string colorCode)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MCOLOR(ID,COLORNAME,COLORCODE,CREATEDBY)
        VALUES(SQ_PR_MCOLOR.NEXTVAL,'" + colorName + "','" + colorCode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string colorName, string colorCode)
    {
        return dm.ExecuteNonQuery("update PR_MCOLOR set COLORNAME='" + colorName + "',COLORCODE='" + colorCode + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MCOLOR WHERE ID=" + id);
    }
    public bool CheckDuplicate(string colorName)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MCOLOR WHERE COLORNAME='" + colorName + "'");
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