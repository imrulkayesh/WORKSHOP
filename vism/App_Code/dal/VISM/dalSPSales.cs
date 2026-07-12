using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalSPSales
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalSPSales()
    {
    }
    public int InsertParts(int SalesId, int partsId, decimal quantity, decimal unitPrice, decimal TotalPrice)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_SPSALESITEMS(ID,SPSALESID,PARTSID,QUANTITY,UNITPRICE,TOTALPRICE)
        VALUES(SQ_RVS_SPSALESITEMS.NEXTVAL,'" + SalesId + "','" + partsId + "','" + quantity + "','" + unitPrice + "','" + TotalPrice + "')");
    }
    public int Update(int SalesId, int workshop, string cust, decimal totalamount, decimal discount, decimal netamount, int currency)
    {
        return dm.ExecuteNonQuery("update RVS_SPSALES set WORKSHOPID='" + workshop + "',CUSTOMER='" + cust + "',TOTALAMOUNT='" + totalamount + "',DISCOUNT='" + discount + "',NETAMOUNT='" + netamount + "',CURRENCYID='" + currency + "',ISOPEN=0,SELLDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + SalesId);
    }

    public void BillPay(int iD)
    {
        dm.ExecuteNonQuery("update RVS_SPSALES set BILLPAID=1,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }
    public void Delivered(int iD)
    {
        dm.ExecuteNonQuery("update RVS_SPSALES set DELIVERED=1,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }

    public int CheckSalesId()
    {
        int a = 0;
        DataTable dt = dm.ExecuteQuery(@"SELECT ID FROM RVS_SPSALES WHERE ISOPEN=1 AND CREATEDBY='" + SessionManager.SessionName.StaffId + "'");
        if (dt.Rows.Count > 0)
        {
            a = Convert.ToInt32(dt.Rows[0][0]);
        }
        else
        {
            dm.ExecuteNonQuery(@"Insert into RVS_SPSALES(ID,CREATEDBY) VALUES(SQ_RVS_SPSALES.NEXTVAL,'" + SessionManager.SessionName.StaffId + "')");
            dt = dm.ExecuteQuery(@"SELECT ID FROM RVS_SPSALES WHERE ISOPEN=1 AND CREATEDBY='" + SessionManager.SessionName.StaffId + "'");
            if (dt.Rows.Count > 0)
            {
                a = Convert.ToInt32(dt.Rows[0][0]);
            }
        }
        return a;
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select S.*,W.WORKSHOP,W.ADDRESS,C.CURRENCYCODE from RVS_SPSALES S
        INNER JOIN RVS_WORKSHOP W ON W.ID = S.WORKSHOPID
        INNER JOIN ASSET_CURRENCY C ON C.ID = S.CURRENCYID WHERE S.ID=" + id);
    }
    public DataTable GetSPSalesbyWorkshop(int WorkshopId)
    {
        return dm.ExecuteQuery(@"Select S.*,W.WORKSHOP,W.ADDRESS,C.CURRENCYCODE from RVS_SPSALES S
        INNER JOIN RVS_WORKSHOP W ON W.ID = S.WORKSHOPID
        INNER JOIN ASSET_CURRENCY C ON C.ID = S.CURRENCYID WHERE S.BILLPAID=0 AND S.DELIVERED=0 AND S.WORKSHOPID=" + WorkshopId);
    }
    public DataTable GetSPSalesAccounts()
    {
        return dm.ExecuteQuery(@"Select S.*,W.WORKSHOP,W.ADDRESS,C.CURRENCYCODE from RVS_SPSALES S
        INNER JOIN RVS_WORKSHOP W ON W.ID = S.WORKSHOPID
        INNER JOIN ASSET_CURRENCY C ON C.ID = S.CURRENCYID WHERE S.BILLPAID=0 AND S.DELIVERED=0 AND W.BUSINESSCENTERID=" + SessionManager.SessionName.BusinessCentetid);
    }
    public DataTable GetSPSalesStore()
    {
        return dm.ExecuteQuery(@"Select S.*,W.WORKSHOP,W.ADDRESS,C.CURRENCYCODE from RVS_SPSALES S
        INNER JOIN RVS_WORKSHOP W ON W.ID = S.WORKSHOPID
        INNER JOIN ASSET_CURRENCY C ON C.ID = S.CURRENCYID WHERE S.BILLPAID=1 AND S.DELIVERED=0 AND W.BUSINESSCENTERID=" + SessionManager.SessionName.BusinessCentetid);
    }
    public DataTable GetByCriteria(string Criteria)
    {
        return dm.ExecuteQuery(@"Select S.*,W.WORKSHOP,W.ADDRESS,C.CURRENCYCODE from RVS_SPSALES S
        INNER JOIN RVS_WORKSHOP W ON W.ID = S.WORKSHOPID
        INNER JOIN ASSET_CURRENCY C ON C.ID = S.CURRENCYID WHERE S.WORKSHOPID>0 " + Criteria);
    }
    public DataTable GetSPItems(int salesId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM RVS_SPSALESITEMS S
        INNER JOIN RVS_SPAREPARTS SP ON SP.ID = S.PARTSID
        INNER JOIN ASSET_UNIT U ON U.ID = SP.UNITID
        WHERE S.SPSALESID = " + salesId);
    }

    public DataTable GetAll()
    {
        return dm.ExecuteQuery("Select * from RVS_SPSALES");
    }


}