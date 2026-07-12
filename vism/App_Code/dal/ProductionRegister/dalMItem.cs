using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMItem
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMItem()
    {
    }
    public int Insert(string item, string itemCode, int unitId, decimal price, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MITEM(ID,ITEMNAME,ITEMCODE,UNITID,PRICE,REMARKS,CREATEDBY)
        VALUES(SQ_PR_MITEM.NEXTVAL,'" + item + "','" + itemCode + "','" + unitId + "','" + price + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string item, string itemCode, int unitId, decimal price, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_MITEM set ITEMNAME='" + item + "',ITEMCODE='" + itemCode + "',UNITID='" + unitId + "',PRICE='" + price + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MITEM WHERE ID=" + id);
    }

    public DataTable GetAllItemList()
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT I.*, U.UNITNAME, U.UNITCODE FROM PR_MITEM I 
        LEFT OUTER JOIN ASSET_UNIT U ON U.ID = I.UNITID order by i.ITEMNAME");
    }
    public bool CheckDuplicate(string item, string itemCode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MITEM WHERE ITEMNAME='" + item + "' AND ITEMCODE='" + itemCode + "' ");
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