using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalBillingApproval
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalBillingApproval()
    {
    }
    public int Insert(int partyid, int exporterid, int salestermid, string shipaddress, string pino, DateTime pidate, decimal pivalue, string cino, DateTime cidate, decimal civalue, string exportno, DateTime exportdate, decimal exportvalue, string goodsdescription, DateTime shipmentdate, decimal trgfreight, int shipmentmoodid, int deschargereportid, string finaldestination, int currencyid, int discountryid, decimal totaquentity, int unitid)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_SHIPMENT(ID,PARTYID,EXPORTERID,SALESTERMID,SHIPMENTADDS,PINO,PIDATE,PIVALUE,CINO,CIDATE,CIVALUE,EXPNO,EXPDATE,EXPVALUE,GOODSDESCRIPTION,SHIPMENTDATE,TRGFREIGHT,SHIPMENTMODEID,DISCHARGEPORTID,FINALDESTINATION,CURRENCYID,DESTCOUNTRYID,TOTALQUANTITY,UNITID,CREATEDBY)
        VALUES(SQ_BIL_SHIPMENT.NEXTVAL,'" + partyid + "','" + exporterid + "','" + salestermid + "','" + shipaddress + "','" + pino + "',TO_DATE('" + pidate + "','MM/dd/yyyy HH:MI:SSAM'),'" + pivalue + "','" + cino + "',TO_DATE('" + cidate + "','MM/dd/yyyy HH:MI:SSAM'),'" + civalue + "','" + exportno + "',TO_DATE('" + exportdate + "','MM/dd/yyyy HH:MI:SSAM'),'" + exportvalue + "','" + goodsdescription + "',TO_DATE('" + shipmentdate + "','MM/dd/yyyy HH:MI:SSAM'),'" + trgfreight + "','" + shipmentmoodid + "','" + deschargereportid + "','" + finaldestination + "','" + currencyid + "','" + discountryid + "','" + totaquentity + "','" + unitid + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public int Update(int id, int partyid, int exporterid, int salestermid, string shipaddress, string pino, DateTime pidate, decimal pivalue, string cino, DateTime cidate, decimal civalue, string exportno, DateTime exportdate, decimal exportvalue, string goodsdescription, DateTime shipmentdate, decimal trgfreight, int shipmentmoodid, int deschargereportid, string finaldestination, int currencyid, int discountryid, decimal totaquentity, int unitid)
    {
        return dm.ExecuteNonQuery("update BIL_SHIPMENT set PARTYID='" + partyid + "',EXPORTERID='" + exporterid + "',SALESTERMID='" + salestermid + "',SHIPMENTADDS='" + shipaddress + "',PINO='" + pino + "',PIDATE=TO_DATE('" + pidate + "','MM/dd/yyyy HH:MI:SSAM'),PIVALUE='" + pivalue + "',CINO='" + cino + "',CIDATE=TO_DATE('" + cidate + "','MM/dd/yyyy HH:MI:SSAM'),CIVALUE='" + civalue + "',EXPNO='" + exportno + "',EXPDATE=TO_DATE('" + exportdate + "','MM/dd/yyyy HH:MI:SSAM'),EXPVALUE='" + exportvalue + "',GOODSDESCRIPTION='" + goodsdescription + "',SHIPMENTDATE=TO_DATE('" + shipmentdate + "','MM/dd/yyyy HH:MI:SSAM'),TRGFREIGHT='" + trgfreight + "',SHIPMENTMODEID='" + shipmentmoodid + "',DISCHARGEPORTID='" + deschargereportid + "',FINALDESTINATION='" + finaldestination + "',CURRENCYID='" + currencyid + "',DESTCOUNTRYID='" + discountryid + "',TOTALQUANTITY='" + totaquentity + "',UNITID='" + unitid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }

    public void FirstApv(int Id, int firstAppSelect, int ststus)
    {
        dm.ExecuteNonQuery("update BIL_FREIGHT set FIRSTAPVRSELECT='" + firstAppSelect + "',STATUS='" + ststus + "',FIRSTAPVR='" + SessionManager.SessionName.StaffId + "',FIRSTAPVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + Id);
    }
    public void FinalApv(int Id, int finalAppSelect, int ststus)
    {
        dm.ExecuteNonQuery("update BIL_FREIGHT set FINALAPVASELECT='" + finalAppSelect + "',STATUS='" + ststus + "',FINALAPVA='" + SessionManager.SessionName.StaffId + "',FINALAPVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + Id);
    }

    public void RjectFreightFirstApp(int id, int upStatus)
    {
        dm.ExecuteNonQuery(@"update BIL_FREIGHT set ISUPDATE='" + upStatus + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }

    public void RjectFreightFinalApp(int id, int upStatus, int status)
    {
        dm.ExecuteNonQuery(@"update BIL_FREIGHT set ISUPDATE='" + upStatus + "', STATUS='" + status + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public void FirstApvRate(int Id, int firstApprecom)
    {
        dm.ExecuteNonQuery("update BIL_FREIGHTRATE set FIRSTAPPRECOM='" + firstApprecom + "',FIRSTAPVR='" + SessionManager.SessionName.StaffId + "',FIRSTAPVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE FREIGHTID=" + Id);
    }
    public void FinalApvRate(int Id, int finalApprecom)
    {
        dm.ExecuteNonQuery("update BIL_FREIGHTRATE set FINALAPPRECOM='" + finalApprecom + "',FINALAPVA='" + SessionManager.SessionName.StaffId + "',FINALAPVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE FREIGHTID=" + Id);
    }
    public void ShipMentStatus(int ShipmentId, int Status)
    {
        dm.ExecuteNonQuery("update BIL_SHIPMENT set STATUS='" + Status + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + ShipmentId);
    }
    public DataTable GetByBillId(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_BILL WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_SHIPMENT WHERE ID=" + id);
    }
    public DataTable GetAll(int Status)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT F.ID FREIGHTID, S.*, P.PARTYNAME, CO.COMPANYNAME, SA.TERM,SM.SHIPMENTMODE,PO.PORTNAME, C.COUNTRYNAME, 
          CU.CURRENCYCODE,CU.CURRENCYNAME, U.UNITCODE, U.UNITNAME  FROM BIL_FREIGHT F 
            INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
            INNER JOIN BIL_PARTY P ON P.ID = S.PARTYID
            INNER JOIN BS_COMPANY CO ON CO.ID = S.EXPORTERID
            INNER JOIN BIL_SALESTERM SA ON SA.ID = S.SALESTERMID
            INNER JOIN BIL_SHIPMENTMODE SM ON SM.ID = S.SHIPMENTMODEID
            INNER JOIN BIL_PORT PO ON PO.ID = S.DISCHARGEPORTID
            INNER JOIN BS_COUNTRY C ON C.ID=S.DESTCOUNTRYID
            INNER JOIN ASSET_CURRENCY CU ON CU.ID = S.CURRENCYID
            INNER JOIN ASSET_UNIT U ON U.ID = S.UNITID
            WHERE F.ISUPDATE=1 AND F.STATUS=" + Status);
    }
    public DataTable GetShipmentDetails(string piid)
    {
        return dm.ExecuteQuery(@"Select S.*,P.PARTYNAME, CO.COMPANYNAME, SA.TERM,SM.SHIPMENTMODE,PO.PORTNAME, C.COUNTRYNAME, 
            CU.CURRENCYCODE,CU.CURRENCYNAME, U.UNITCODE, U.UNITNAME  from BIL_SHIPMENT S
            INNER JOIN BIL_PARTY P ON P.ID = S.PARTYID
            INNER JOIN BS_COMPANY CO ON CO.ID = S.EXPORTERID
            INNER JOIN BIL_SALESTERM SA ON SA.ID = S.SALESTERMID
            INNER JOIN BIL_SHIPMENTMODE SM ON SM.ID = S.SHIPMENTMODEID
            INNER JOIN BIL_PORT PO ON PO.ID = S.DISCHARGEPORTID
            INNER JOIN BS_COUNTRY C ON C.ID=S.DESTCOUNTRYID
            INNER JOIN ASSET_CURRENCY CU ON CU.ID = S.CURRENCYID
            INNER JOIN ASSET_UNIT U ON U.ID = S.UNITID WHERE PINO='" + piid + "'");
    }
    public DataTable GetAllBill(int status, int IsApproved, int IsPaid)
    {
        return dm.ExecuteQuery(@"SELECT B.*, BT.BILLTYPE, P.PARTYNAME, C.CURRENCYNAME, C.CURRENCYCODE, PM.PAYMODE FROM BIL_BILL B 
        INNER JOIN BIL_BILLTYPE BT ON BT.ID = B.BILLTYPEID
        LEFT OUTER JOIN BIL_PARTY P ON P.ID =B.PARTYID 
        INNER JOIN ASSET_CURRENCY C ON C.ID = B.CURRENCYID
        LEFT OUTER JOIN BIL_PAYMODE PM ON PM.ID = B.PAYMODEID
        WHERE B.STATUS='" + status + "' AND B.ISAPPROVED='" + IsApproved + "' AND B.ISPAID='" + IsPaid + "' ");
    }
    public void BillFirstApproval(int iD, int status)
    {
        dm.ExecuteNonQuery("update BIL_BILL set ISAPPROVED=1,STATUS='" + status + "',FIRSTAPV='" + SessionManager.SessionName.StaffId + "',FIRSTAPVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }
    public void BillFinalApproval(int iD, int status)
    {
        dm.ExecuteNonQuery("update BIL_BILL set ISAPPROVED=2,STATUS='" + status + "',FINALAPV='" + SessionManager.SessionName.StaffId + "',FINALAPVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }
    public void BillReject(int iD, int IsApproved)
    {
        dm.ExecuteNonQuery("update BIL_BILL set ISAPPROVED='" + IsApproved + "',REJECTEDBY='" + SessionManager.SessionName.StaffId + "',REJECTAPVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }
    public void BillAccounts(int iD, int status)
    {
        dm.ExecuteNonQuery("update BIL_BILL set ISPAID=1,STATUS='" + status + "',ACCOUNTANT='" + SessionManager.SessionName.StaffId + "',PAYDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }
    public void RjectBillAccounts(int iD, int status)
    {
        dm.ExecuteNonQuery("update BIL_BILL set ISPAID=0,ISAPPROVED=0,STATUS='" + status + "',ACCOUNTANT='" + SessionManager.SessionName.StaffId + "',PAYDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }
    public DataTable GetAllAttetchment(int billId)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT B.ID, S.ID SHIPMENT, S.ATTECHMENT SHIPMENTFILE, B.ATTACHMENT BILLCOPY FROM BIL_BILL B 
        INNER JOIN BIL_SHIPMENT S ON S.ID = B.SHIPMENTID
        WHERE B.ID=" + billId);
    }
    public int getShipmentId(int billId)
    {
        DataTable dt = dm.ExecuteQuery(@"SELECT B.SHIPMENTID FROM BIL_SHIPMENT S 
        INNER JOIN BIL_BILL B ON B.SHIPMENTID = S.ID
        WHERE B.ID =" + billId);
        if (dt.Rows.Count > 0)
        {
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        else
        {
            return 0;
        }
    }
}