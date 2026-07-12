using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalSPSalesItems
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalSPSalesItems()
    {
    }
    public int Insert(int spsalesid, int partsid, int quentity, int unitid, decimal unitprice, decimal totalprice, int currencyid, decimal discount, decimal netamount)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_SPSALESITEMS(ID,SPSALESID,PARTSID,QUANTITY,UNITID,UNITPRICE,TOTALPRICE,CURRENCYID,DISCOUNT,NETAMOUNT,CREATEDBY)
        VALUES(SQ_RVS_SPSALESITEMS.NEXTVAL,'" + spsalesid + "','" + partsid + "','" + quentity + "','" + unitid + "','" + unitprice + "','" + totalprice + "','" + currencyid + "','" + discount + "','" + netamount + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int spsalesid, int partsid, int quentity, int unitid, decimal unitprice, decimal totalprice, int currencyid, decimal discount, decimal netamount)
    {
        return dm.ExecuteNonQuery("update RVS_SPSALESITEMS set SPSALESID='" + spsalesid + "',PARTSID='" + partsid + "',QUANTITY='" + quentity + "',UNITID='" + unitid + "',UNITPRICE='" + unitprice + "',TOTALPRICE='" + totalprice + "',CURRENCYID='" + currencyid + "',DISCOUNT='" + discount + "',NETAMOUNT='" + netamount + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_SPSALESITEMS WHERE ID=" + id);
    }
    public DataTable GetAll(int spsalesid, int partsid, int unitid)
    {
        return dm.ExecuteQuery(@"");
    }

    public bool CheckDuplicate(int spsalesid, int partsid, int unitid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_SPSALESITEMS WHERE SPSALESID='" + spsalesid + "' AND PARTSID='" + partsid + "' AND UNITID='" + unitid + "'");
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