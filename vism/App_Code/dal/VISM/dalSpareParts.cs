using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalSpareParts
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalSpareParts()
    {
    }
    public int Insert(int workshopid, int cateid, int unitid, string parts,string partscode, decimal stock , decimal cost, decimal sellprice,int currencyid)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_SPAREPARTS(ID,WORKSHOPID,CATEGORYID,UNITID,PARTSNAME,PARTSCODE,STOCK,COSTPRICE,SELLINGPRICE,CURRENCYID,CREATEDBY)
        VALUES(SQ_RVS_SPAREPARTS.NEXTVAL,'" + workshopid + "','" + cateid + "','" + unitid + "','" + parts + "','" + partscode + "','" + stock + "','" + cost + "','" + sellprice + "','" + currencyid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int workshopid, int cateid, int unitid, string parts, string partscode, decimal stock, decimal cost, decimal sellprice, int currencyid)
    {
        return dm.ExecuteNonQuery("update RVS_SPAREPARTS set WORKSHOPID='" + workshopid + "',CATEGORYID='" + cateid + "',UNITID='" + unitid + "',PARTSNAME='" + parts + "',PARTSCODE='" + partscode + "',STOCK='" + stock + "',COSTPRICE='" + cost + "',SELLINGPRICE='" + sellprice + "',CURRENCYID='"+currencyid+"',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_SPAREPARTS WHERE ID=" + id);
    }
    public DataTable GetAll(int workshopid, int cateid)
    {
        return dm.ExecuteQuery(@"SELECT SP.*, W.WORKSHOP,SC.CATNAME,U.UNITNAME,C.CURRENCYNAME,C.CURRENCYCODE FROM RVS_SPAREPARTS SP 
            INNER JOIN RVS_WORKSHOP W ON W.ID = SP.WORKSHOPID
            INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=SP.CATEGORYID
            INNER JOIN ASSET_UNIT U ON U.ID = SP.UNITID
            INNER JOIN ASSET_CURRENCY C ON C.ID=SP.CURRENCYID WHERE SP.WORKSHOPID='" + workshopid + "' AND SP.CATEGORYID='" + cateid + "'");
    }

    public bool CheckDuplicate(int workshopid, int cateid, string parts)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_SPAREPARTS WHERE WORKSHOPID='" + workshopid + "' AND CATEGORYID='" + cateid + "' AND PARTSNAME='" + parts + "'");
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