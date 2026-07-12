using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalItemMaster
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalItemMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(string ItemCode, string ItemName, string ItemCategory, string Unit)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_ITEMMASTER(ITEMCODE,ITEMNAME,ITEMCATEGORYCODE,SCMUNITCODE,IUSER)
        VALUES('" + ItemCode + "','" + ItemName + "','" + ItemCategory + "','" + Unit + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(string itemCategory, string itemCode, string itemName, string unitCode)
    {
        return dm.ExecuteNonQuery("update RVS_ITEMMASTER set ITEMCATEGORYCODE='" + itemCategory + "',ITEMNAME='" + itemName + "',SCMUNITCODE='" + unitCode + "',EUSER='" + SessionManager.SessionName.StaffId + "',EDATE=SYSDATE WHERE ITEMCODE= '" + itemCode + "'");
    }
    public DataTable GetById(string itemcode)
    {
        return dm.ExecuteQuery("SELECT * FROM RVS_ITEMMASTER WHERE ITEMCODE = '" + itemcode + "'");
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery("SELECT IC.ITEMCATEGORYNAME, IM.ITEMCODE, IM.ITEMNAME, U.UNITNAME   FROM RVS_ITEMMASTER IM LEFT JOIN RVS_ITEMCATEGORY IC ON IC.ITEMCATEGORYCODE = IM.ITEMCATEGORYCODE LEFT JOIN RVS_SCMUNIT U ON U.UNITCODE = IM.SCMUNITCODE WHERE TRUNC(IM.IDATE) = TRUNC(SYSDATE)");
    }
    public bool CheckDuplicate(string ItemCode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_ITEMMASTER WHERE  ITEMCODE='" + ItemCode + "'");
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