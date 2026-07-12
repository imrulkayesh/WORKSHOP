using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalShipment
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalShipment()
    {
    }
    public int Insert(int partyid, int exporterid, int salestermid, int loadingportid, string pino, string pidate, decimal pivalue, string cino, string cidate, decimal civalue, string exportno, string exportdate, decimal exportvalue, string goodsdescription, string shipmentdate, decimal trgfreight, int shipmentmoodid, int deschargereportid, string finaldestination, int currencyid, int discountryid, decimal totaquentity, int unitid, string attechment)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_SHIPMENT(ID,PARTYID,EXPORTERID,SALESTERMID,LOADINGPORTID,PINO,PIDATE,PIVALUE,CINO,CIDATE,CIVALUE,EXPNO,EXPDATE,EXPVALUE,GOODSDESCRIPTION,SHIPMENTDATE,TRGFREIGHT,SHIPMENTMODEID,DISCHARGEPORTID,FINALDESTINATION,CURRENCYID,DESTCOUNTRYID,TOTALQUANTITY,UNITID,ATTECHMENT,CREATEDBY)
        VALUES(SQ_BIL_SHIPMENT.NEXTVAL,'" + partyid + "','" + exporterid + "','" + salestermid + "','" + loadingportid + "','" + pino + "',TO_DATE('" + pidate + "','DD/MM/YYYY'),'" + pivalue + "','" + cino + "',TO_DATE('" + cidate + "','DD/MM/YYYY'),'" + civalue + "','" + exportno + "',TO_DATE('" + exportdate + "','DD/MM/YYYY'),'" + exportvalue + "','" + goodsdescription + "',TO_DATE('" + shipmentdate + "','DD/MM/YYYY'),'" + trgfreight + "','" + shipmentmoodid + "','" + deschargereportid + "','" + finaldestination + "','" + currencyid + "','" + discountryid + "','" + totaquentity + "','" + unitid + "','" + attechment + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int partyid, int exporterid, int salestermid, int loadingportid, string pino, string pidate, decimal pivalue, string cino, string cidate, decimal civalue, string exportno, string exportdate, decimal exportvalue, string goodsdescription, string shipmentdate, decimal trgfreight, int shipmentmoodid, int deschargereportid, string finaldestination, int currencyid, int discountryid, decimal totaquentity, int unitid, string attechment)
    {
        return dm.ExecuteNonQuery("update BIL_SHIPMENT set PARTYID='" + partyid + "',EXPORTERID='" + exporterid + "',SALESTERMID='" + salestermid + "',LOADINGPORTID='" + loadingportid + "',PINO='" + pino + "',PIDATE=TO_DATE('" + pidate + "','DD/MM/YYYY'),PIVALUE='" + pivalue + "',CINO='" + cino + "',CIDATE=TO_DATE('" + cidate + "','DD/MM/YYYY'),CIVALUE='" + civalue + "',EXPNO='" + exportno + "',EXPDATE=TO_DATE('" + exportdate + "','DD/MM/YYYY'),EXPVALUE='" + exportvalue + "',GOODSDESCRIPTION='" + goodsdescription + "',SHIPMENTDATE=TO_DATE('" + shipmentdate + "','DD/MM/YYYY'),TRGFREIGHT='" + trgfreight + "',SHIPMENTMODEID='" + shipmentmoodid + "',DISCHARGEPORTID='" + deschargereportid + "',FINALDESTINATION='" + finaldestination + "',CURRENCYID='" + currencyid + "',DESTCOUNTRYID='" + discountryid + "',TOTALQUANTITY='" + totaquentity + "',UNITID='" + unitid + "',ATTECHMENT='" + attechment + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public int UpdateStatus(int status, int id)
    {
        return dm.ExecuteNonQuery("update BIL_SHIPMENT set STATUS='" + status + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_SHIPMENT WHERE ID=" + id);
    }
    public DataTable GetAllShipment(int id)
    {
        return dm.ExecuteQuery("Select S.ID from BIL_SHIPMENT S WHERE S.ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery(@"Select S.*,P.PARTYNAME, CO.COMPANYNAME, SA.TERM,SM.SHIPMENTMODE,PO.PORTNAME, C.COUNTRYNAME, 
            CU.CURRENCYCODE,CU.CURRENCYNAME, U.UNITCODE, U.UNITNAME  from BIL_SHIPMENT S
            LEFT OUTER JOIN BIL_PARTY P ON P.ID = S.PARTYID
            INNER JOIN BS_COMPANY CO ON CO.ID = S.EXPORTERID
            INNER JOIN BIL_SALESTERM SA ON SA.ID = S.SALESTERMID
            INNER JOIN BIL_SHIPMENTMODE SM ON SM.ID = S.SHIPMENTMODEID
            INNER JOIN BIL_PORT PO ON PO.ID = S.DISCHARGEPORTID
            INNER JOIN BS_COUNTRY C ON C.ID=S.DESTCOUNTRYID
            INNER JOIN ASSET_CURRENCY CU ON CU.ID = S.CURRENCYID
            INNER JOIN ASSET_UNIT U ON U.ID = S.UNITID ");
    }
    public DataTable GetShipmentDetails(int tbxShipmentID)
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
            INNER JOIN ASSET_UNIT U ON U.ID = S.UNITID WHERE S.ID=" + tbxShipmentID);
    }

    public DataTable GetAllDocDowonload(int shipmentId)
    {
        return dm.ExecuteQuery(@"SELECT S.ID,S.ATTECHMENT, DF.ATTACHMENTONE, DF.ATTACHMENTTWO, DF.ATTACHMENTTHREE, B.ATTACHMENT BILATTACHMENT FROM BIL_SHIPMENT S
        LEFT OUTER JOIN BIL_FREIGHT F ON F.SHIPMENTID = S.ID
        LEFT OUTER JOIN BIL_DOFROM DF ON DF.SHIPMENTID = S.ID
        LEFT OUTER JOIN BIL_BILL B ON B.SHIPMENTID = S.ID
        WHERE S.ID=" + shipmentId);
    }

    public DataTable GetAllFowarder(int shipmentId)
    {
        return dm.ExecuteQuery(@"SELECT S.ID, FW.FORWARDER, FD.ATTACHMENT FROM BIL_FREIGHTRATEDOC FD
        INNER JOIN BIL_FREIGHT F ON F.ID = FD.FREIGHTID
        INNER JOIN BIL_FORWARDER FW ON FW.ID = FD.FORWARDERID
        INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
        WHERE S.ID='"+shipmentId+ "' ORDER BY FW.FORWARDER ASC");
    }
    public DataTable GetAllFowarderRate(int shipmentId)
    {
        return dm.ExecuteQuery(@"SELECT S.ID, FW.FORWARDER, FR.OFFERPRICE FROM BIL_FREIGHTRATE FR  
        INNER JOIN BIL_FREIGHT F ON F.ID = FR.FREIGHTID 
        INNER JOIN BIL_FORWARDER FW ON FW.ID = FR.FORWARDERID
        INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
        WHERE S.ID='" + shipmentId + "' ORDER BY FW.FORWARDER ASC");
    }
    public DataTable GetCashIncentive(int shipmentId)
    {
        return dm.ExecuteQuery(@"SELECT D.SHIPMENTID, DD.ATTACHMENT, DD.REALDATE FROM BIL_DEDO D
        LEFT OUTER JOIN BIL_DEDODETAILS DD ON DD.SHIPMENTID = D.SHIPMENTID
        WHERE D.SHIPMENTID =" + shipmentId);
    }
    public DataTable ShipmentDetails()
    {
        return dm.ExecuteQuery(@"Select S.*,P.PARTYNAME,P.PARTYADDRESS ,BL.BILLNO, BL.BILLDATE
             FROM BIL_SHIPMENT S
             LEFT OUTER JOIN BIL_PARTY P ON P.ID = S.PARTYID
             LEFT OUTER JOIN BIL_BILL BL ON BL.SHIPMENTID =S.ID ");
    }
    public DataTable ShipmentDetailsCriteria(string criteria)
    {
        return dm.ExecuteQuery(@"Select S.*,P.PARTYNAME,P.PARTYADDRESS ,BL.BILLNO, BL.BILLDATE
             FROM BIL_SHIPMENT S
             LEFT OUTER JOIN BIL_PARTY P ON P.ID = S.PARTYID
             LEFT OUTER JOIN BIL_BILL BL ON BL.SHIPMENTID =S.ID where " + criteria);
    }

    public DataTable GetExportDetails(string criteria)
    {
        return dm.ExecuteQuery(@"SELECT S.ID, S.EXPNO, S.EXPDATE, S.EXPVALUE, S.CREATEDATE, BB.BILLOFLADING, BB.LADINGDATE, PO.PORTNAME, 
        BB.BILLNO, BB.BILLDATE, BB.NETWEIGHT, DD.REALVALUE,DD.REALDATE FROM BIL_SHIPMENT S 
        INNER JOIN BIL_BILL BB ON BB.SHIPMENTID = S.ID
        INNER JOIN BIL_PORT PO ON PO.ID = S.DISCHARGEPORTID
        INNER JOIN BIL_DEDODETAILS DD ON DD.SHIPMENTID = S.ID
        WHERE S.ID > 0 " + criteria);
    }
    public DataTable GetByShipmentId(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_SHIPMENT WHERE ID=" + id);
    }
    public DataTable SingleShipmentDetails(int shipmentId)
    {
        return dm.ExecuteQuery(@"SELECT S.ID, S.EXPNO, S.EXPDATE, S.EXPVALUE, S.CREATEDATE, BB.BILLOFLADING, BB.LADINGDATE, PO.PORTNAME, 
        BB.BILLNO, BB.BILLDATE, BB.NETWEIGHT, DD.REALVALUE,DD.REALDATE FROM BIL_SHIPMENT S 
        INNER JOIN BIL_BILL BB ON BB.SHIPMENTID = S.ID
        INNER JOIN BIL_PORT PO ON PO.ID = S.DISCHARGEPORTID
        INNER JOIN BIL_DEDODETAILS DD ON DD.SHIPMENTID = S.ID
        WHERE S.ID =" + shipmentId);
    }
    public bool CheckDuplicate(string pino, string cino, string expo)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_SHIPMENT WHERE PINO='" + pino + "' AND CINO='" + cino + "' AND EXPNO='" + expo + "' ");
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