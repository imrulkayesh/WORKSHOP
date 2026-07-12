using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalVISMQuotation
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVISMQuotation()
    {
    }
    public int Insert(int workshopId, int vehicleid, int sercategoryid, DateTime BookingDate, decimal bookingmoney, decimal totalamt, decimal discount, decimal netamount, int currencyid, int isconfirm, string remarks)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_QUOTATION(ID,WORKSHOPID,VEHICLEID,SERVICECATID,BOOKINGDATE,BOOKINGMONEY,TOTALAMOUNT,DISCOUNT,NETAMOUNT,CURRENCYID,ISCONFIRM,EXPIREDATE,REMARKS,CREATEDBY)
        VALUES(SQ_RVS_QUOTATION.NEXTVAL,'" + workshopId + "','" + vehicleid + "','" + sercategoryid + "',SYSDATE,'" + bookingmoney + "','" + totalamt + "','" + discount + "','" + netamount + "','" + currencyid + "','" + isconfirm + "',TO_DATE('" + BookingDate + "','MM/dd/yyyy HH:MI:SSAM'),'" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
        int BookingID = 0;
        DataTable dt = dm.ExecuteQuery("SELECT ID FROM RVS_QUOTATION WHERE VEHICLEID='" + vehicleid + "' AND WORKSHOPID='" + workshopId + "' and  SERVICECATID='" + sercategoryid + "' AND CREATEDBY='" + SessionManager.SessionName.StaffId + "' ORDER BY ID DESC");
        if (dt.Rows.Count > 0)
        {
            BookingID = Convert.ToInt32(dt.Rows[0][0]);
        }
        return BookingID;
    }
    public int Update(int id, int workshopId, int vehicleid, int sercategoryid, DateTime BookingDate, decimal bookingmoney, decimal totalamt, decimal discount, decimal netamount, int currencyid, int isconfirm, string remarks)
    {
        return dm.ExecuteNonQuery("update RVS_QUOTATION set  WORKSHOPID='" + workshopId + "',VEHICLEID='" + vehicleid + "',SERVICECATID='" + sercategoryid + "', BOOKINGMONEY='" + bookingmoney + "',TOTALAMOUNT='" + totalamt + "',DISCOUNT='" + discount + "',NETAMOUNT='" + netamount + "',CURRENCYID='" + currencyid + "',ISCONFIRM='" + isconfirm + "',EXPIREDATE=TO_DATE('" + BookingDate + "','MM/dd/yyyy HH:MI:SSAM'),REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_QUOTATION WHERE ID=" + id);
    }
    public DataTable GetAll(int workshopid, int sercategoryid)
    {
        return dm.ExecuteQuery(@"SELECT J.*, V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE FROM RVS_QUOTATION J
        INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        WHERE J.SERVICETAKEN=0 AND J.SERVICECATID='" + sercategoryid + "' AND J.WORKSHOPID='" + workshopid + "'");
    }

    public DataTable GetCharges(int WorkshopId, int catid, int bookingID)
    {
        string query = @"SELECT CN.CHARGENAME,C.AMOUNT,1 QUANTITY, '' REMARKS,0 BOOKINGID,CN.ID FROM RVS_CHARGENAME CN
        INNER JOIN RVS_CHARGES C ON C.CHARGEID=CN.ID
        WHERE C.CATEGORYID='" + catid + "' AND C.WORKSHOPID=" + WorkshopId;
        if (bookingID > 0)
        {
            query = @"SELECT CN.CHARGENAME,JC.AMOUNT,JC.QUANTITY,JC.REMARKS,JC.BOOKINGID,CN.ID FROM RVS_CHARGENAME CN
            INNER JOIN RVS_CHARGES C ON C.CHARGEID=CN.ID
            INNER JOIN RVS_QUOTATIONCHARGE JC ON JC.CHARGEID=C.CHARGEID
            WHERE C.CATEGORYID='" + catid + "' AND JC.QUOTID='" + bookingID + "' AND C.WORKSHOPID=" + WorkshopId;
        }
        return dm.ExecuteQuery(query);
    }

    public DataTable GetParts(int WorkshopId, int catid, int bookingID)
    {
        string query = @"SELECT  P.ID, P.PARTSNAME,P.SELLINGPRICE AMOUNT, 1 QUANTITY, 0 BOOKINGID,'' REMARKS  FROM RVS_SPAREPARTS P 
            WHERE P.CATEGORYID='" + catid + "' AND P.WORKSHOPID=" + WorkshopId;
        if (bookingID > 0)
        {
            query = @" SELECT P.ID, P.PARTSNAME,IT.AMOUNT, IT.QUANTITY, IT.BOOKINGID ,IT.REMARKS
            FROM RVS_SPAREPARTS P 
            INNER JOIN RVS_QUOTATIONITEMS IT ON IT.PARTSID=P.ID 
            WHERE P.CATEGORYID='" + catid + "' AND IT.QUOTID='" + bookingID + "' AND P.WORKSHOPID=" + WorkshopId;
        }
        return dm.ExecuteQuery(query);
    }

    public void DeleteCharges(int bookingID)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_QUOTATIONCHARGE Where QUOTID='" + bookingID + "'");
    }

    public void InsertCharges(int bookingID, int ChargeId, decimal qtys, decimal amount, string remarks)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_QUOTATIONCHARGE(QUOTID,CHARGEID,AMOUNT,QUANTITY,REMARKS)
        VALUES('" + bookingID + "','" + ChargeId + "','" + amount + "','" + qtys + "','" + remarks + "')");
    }
    public void DeleteParts(int bookingID)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_QUOTATIONITEMS Where QUOTID='" + bookingID + "'");
    }

    public void InsertParts(int bookingID, int Parts, decimal qtys, decimal amount, string remarks)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_QUOTATIONITEMS(QUOTID,PARTSID,QUANTITY,AMOUNT,REMARKS)
        VALUES('" + bookingID + "','" + Parts + "','" + qtys + "','" + amount + "','" + remarks + "')");
    }
}