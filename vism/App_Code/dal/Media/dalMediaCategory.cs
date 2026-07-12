using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMediaCategory
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMediaCategory()
    {
    }
    public int Insert( string catename)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_MEDIACATEGORY(ID,CATNAME,CREATEDBY)
        VALUES(SQ_MD_MEDIACATEGORY.NEXTVAL,'" + catename + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string catename)
    {
        return dm.ExecuteNonQuery("update MD_MEDIACATEGORY set CATNAME='" + catename + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAll(int catid)
    {
        return dm.ExecuteQuery("Select * from MD_MEDIACATEGORY");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_MEDIACATEGORY WHERE ID=" + id);
    }
    public bool CheckDuplicate( string catename)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_MEDIACATEGORY WHERE CATNAME='" + catename +"'");
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
