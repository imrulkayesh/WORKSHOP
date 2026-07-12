using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalBillingBill
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalBillingBill()
    {
    }
    public int Insert(int shipmentid, int billType, int paymodeid, string invoiceno, string billno, string billoflading, string ladingDate, string billDate, int partyid, decimal netWeight, decimal amount, decimal damurragevalue, int currency, decimal exchangerate, string attachment, int status)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_BILL(ID,SHIPMENTID,BILLTYPEID,BILLOFLADING,LADINGDATE,PAYMODEID,INVOICENO,BILLNO,BILLDATE,PARTYID,NETWEIGHT,AMOUNT,DAMAGEVALUE,CURRENCYID,EXCHANGERATE,ATTACHMENT,STATUS,CREATEDBY)
        VALUES(SQ_BIL_BILL.NEXTVAL,'" + shipmentid + "','" + billType + "','" + billoflading + "',TO_DATE('" + ladingDate + "','DD/MM/YYYY'),'" + paymodeid + "','" + invoiceno + "','" + billno + "',TO_DATE('" + billDate + "','DD/MM/YYYY'),'" + partyid + "','" + netWeight + "','" + amount + "','" + damurragevalue + "','" + currency + "','" + exchangerate + "','" + attachment + "','" + status + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int shipmentid, int billType, string billoflading, string ladingDate, int paymodeid, string invoiceno, string billno, string billDate, int partyid, decimal netWeight, decimal amount, decimal damurragevalue, int currency, decimal exchangerate, string attachment)
    {

        string atcht = "";
        atcht = string.IsNullOrEmpty(attachment) ? "" : ",ATTACHMENT='" + attachment + "'";
        return dm.ExecuteNonQuery("update BIL_BILL set SHIPMENTID='" + shipmentid + "',BILLTYPEID='" + billType + "',BILLOFLADING='" + billoflading + "',LADINGDATE=TO_DATE('" + ladingDate + "','DD/MM/YYYY'),PAYMODEID='" + paymodeid + "',INVOICENO='" + invoiceno + "',BILLNO='" + billno + "',BILLDATE=TO_DATE('" + billDate + "','DD/MM/YYYY'),PARTYID='" + partyid + "',NETWEIGHT='" + netWeight + "',AMOUNT='" + amount + "',DAMAGEVALUE='" + damurragevalue + "',CURRENCYID='" + currency + "',EXCHANGERATE='" + exchangerate + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE" + atcht + " WHERE STATUS=4 AND ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery(@"SELECT B.*, BT.BILLTYPE, P.PARTYNAME, C.CURRENCYNAME, C.CURRENCYCODE FROM BIL_BILL B
        INNER JOIN BIL_BILLTYPE BT ON BT.ID = B.BILLTYPEID 
        LEFT OUTER JOIN BIL_PARTY P ON P.ID =B.PARTYID
        INNER JOIN ASSET_CURRENCY C ON C.ID = B.CURRENCYID  
        WHERE B.ISAPPROVED=0 AND B.STATUS <=6");
    }

    public DataTable GetAllBillingDetails(int billId)
    {
        return dm.ExecuteQuery(@"SELECT B.*, S.CINO, S.PINO, S.EXPNO, BT.BILLTYPE, S.GOODSDESCRIPTION, F.CONTQUANTITY, P.PARTYNAME, C.CURRENCYNAME, C.CURRENCYCODE, PM.PAYMODE FROM BIL_BILL B 
        INNER JOIN BIL_BILLTYPE BT ON BT.ID = B.BILLTYPEID
        LEFT OUTER JOIN BIL_PARTY P ON P.ID =B.PARTYID 
        INNER JOIN ASSET_CURRENCY C ON C.ID = B.CURRENCYID
        LEFT OUTER JOIN BIL_PAYMODE PM ON PM.ID = B.PAYMODEID
        INNER JOIN BIL_SHIPMENT S ON S.ID = B.SHIPMENTID
        LEFT OUTER JOIN BIL_FREIGHT F ON F.SHIPMENTID = S.ID
                WHERE B.ID=" + billId);
        //return dm.ExecuteQuery(@"SELECT DISTINCT B.*, S.*, F.*, C.CURRENCYNAME, C.CURRENCYCODE,FR.OFFERPRICE, FD.FORWARDER,
        //        BT.BILLTYPE, P.PORTNAME DISCHARGEPORT, LP.PORTNAME LOADINGPORT, CT.CONTAINERTYPE  FROM BIL_BILL B
        //        INNER JOIN BIL_SHIPMENT S ON S.ID = B.SHIPMENTID
        //        INNER JOIN BIL_FREIGHT F ON F.SHIPMENTID = B.SHIPMENTID
        //        INNER JOIN BIL_FREIGHTRATE FR ON FR.FREIGHTID= F.ID
        //        INNER JOIN BIL_FORWARDER FD ON FD.ID = FR.FORWARDERID
        //        INNER JOIN BIL_BILLTYPE BT ON BT.ID = B.BILLTYPEID
        //        INNER JOIN BIL_PORT P ON P.ID = F.DISCHARGEPORTID
        //        INNER JOIN BIL_PORT LP ON LP.ID = F.LOADINGPORTID
        //        INNER JOIN BIL_CONTAINERTYPE CT ON CT.ID = F.CONTTYPEID
        //        INNER JOIN ASSET_CURRENCY C ON C.ID=B.CURRENCYID
        //        WHERE B.ID=" + billId);
    }
    public DataTable GetShipment()
    {
        return dm.ExecuteQuery("Select S.*,S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE ISOPEN=1");
    }
    public DataTable GetShipmentFreightApp()
    {
        return dm.ExecuteQuery("Select S.*,S.ID ||'-'||S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE S.STATUS=2 AND ISOPEN=1");
    }
    public DataTable GetShipmentBill()
    {
        return dm.ExecuteQuery("Select S.*,S.ID ||'-'||S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE ISOPEN=1 AND S.STATUS <= 8 ORDER BY S.ID DESC");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_BILL WHERE ID=" + id);
    }

    public DataTable GetAllForwarderRegister(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT B.SHIPMENTID, B.CREATEDATE, B.FINALAPVDATE, S.PINO, B.INVOICENO, B.BILLOFLADING,
         B.LADINGDATE , B.NETWEIGHT, B.AMOUNT, B.DAMAGEVALUE FROM BIL_BILL B
         INNER JOIN BIL_SHIPMENT S ON S.ID = B.SHIPMENTID WHERE B.ID >0 AND B.BILLTYPEID=2 " + critaria + " ORDER BY B.SHIPMENTID DESC ");
    }

    public DataTable GetAllCnFRegister(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT B.SHIPMENTID, B.CREATEDATE, B.FINALAPVDATE, S.PINO, B.INVOICENO, C.PARTYNAME, B.BILLNO,
         B.BILLDATE, B.NETWEIGHT, B.AMOUNT, B.DAMAGEVALUE FROM BIL_BILL B
         INNER JOIN BIL_SHIPMENT S ON S.ID = B.SHIPMENTID
         INNER JOIN BIL_CNFPARTY C ON C.ID = B.PARTYID WHERE B.ID > 0 " + critaria + " ORDER BY B.SHIPMENTID DESC ");
    }

    public bool CheckDuplicate(int shipmentid, int BillTypeId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_BILL WHERE SHIPMENTID='" + shipmentid + "' and BILLTYPEID=" + BillTypeId);
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