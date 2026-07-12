using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalPEItems
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalPEItems()
	{
	}
    public int Insert(string item, string itemCode, int unitId)
    {
        return dm.ExecuteNonQuery(@"Insert into PE_ITEM(ID,ITEMNAME,ITEMCODE,UNITID,CREATEDBY)
        VALUES(SQ_PE_ITEMS.NEXTVAL,'" + item + "','" + itemCode + "','" + unitId + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string item, string itemCode, int unitId)
    {
        return dm.ExecuteNonQuery("update PE_ITEM set ITEMNAME='" + item + "',ITEMCODE='" + itemCode + "',UNITID='" + unitId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PE_ITEM WHERE ID=" + id);
    }

    public DataTable GetAllItemList()
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT I.*, U.UNITNAME, U.UNITCODE FROM PE_ITEM I 
        LEFT OUTER JOIN ASSET_UNIT U ON U.ID = I.UNITID");
    }
    public bool CheckDuplicate(string item, string itemCode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PE_ITEM WHERE ITEMNAME='" + item + "' AND ITEMCODE='" + itemCode + "' ");
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
