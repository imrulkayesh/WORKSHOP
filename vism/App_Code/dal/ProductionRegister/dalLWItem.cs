using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalLWItem
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLWItem()
    {
    }
    public int Insert(int businesscenter, int departmentId, int itemid,string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_ITEM(ID,BUSINESSUNITID,DEPARTMENTID,ITEMID,REMARKS,CREATEDBY)
        VALUES(SQ_PR_ITEM.NEXTVAL,'" + businesscenter + "','" + departmentId + "','" + itemid + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int businesscenter, int departmentId, int itemid, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_ITEM set BUSINESSUNITID='" + businesscenter + "',DEPARTMENTID='" + departmentId + "',ITEMID='" + itemid + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PR_ITEM WHERE ID=" + id);
    }
    public DataTable GetLWItem()
    {
        return dm.ExecuteQuery(@"SELECT I.*, C.COMPANYNAME , D.DEPARTMENTNAME, MI.ITEMNAME FROM PR_ITEM I 
        INNER JOIN BS_COMPANY C ON C.ID = I.BUSINESSUNITID
        INNER JOIN BS_DEPARTMENT D ON D.ID = I.DEPARTMENTID
        INNER JOIN PR_MITEM MI ON MI.ID = I.ITEMID");
    }
    public bool CheckDuplicate(int businesscenter, int itemid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_ITEM WHERE BUSINESSUNITID='" + businesscenter + "' AND ITEMID='" + itemid + "'");
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