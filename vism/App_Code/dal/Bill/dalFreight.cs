using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalFreight
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalFreight()
    {
    }

    public int Insert(int shipmentid, int loadingport, int dischargeport, int conttypeid, int contquantity, string goodsdes, string shipmentdate, int currency, string tansittime, string hscode, string remark, int ststus, int isUpdate)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_FREIGHT(ID,SHIPMENTID,LOADINGPORTID,DISCHARGEPORTID,CONTTYPEID,CONTQUANTITY,GOODSDESCRIPTIN,SHIPMENTDATE,CURRENCYID,TRANSITTIME,HSCODE,REMARKS,STATUS,ISUPDATE,CREATEDBY)
        VALUES(SQ_BIL_FREIGHT.NEXTVAL,'" + shipmentid + "','" + loadingport + "','" + dischargeport + "','" + conttypeid + "','" + contquantity + "','" + goodsdes + "',TO_DATE('" + shipmentdate + "','DD/MM/YYYY'),'" + currency + "','" + tansittime + "','" + hscode + "','" + remark + "','" + ststus + "','" + isUpdate + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int shipmentid, int loadingport, int dischargeport, int conttypeid, int contquantity, string goodsdes, string shipmentdate, int currency, string tansittime, string hscode, string remark, int isUpdate)
    {
        return dm.ExecuteNonQuery(@"update BIL_FREIGHT set SHIPMENTID='" + shipmentid + "',LOADINGPORTID='" + loadingport + "',DISCHARGEPORTID='" + dischargeport + "',CONTTYPEID='" + conttypeid + "',CONTQUANTITY='" + contquantity + "',GOODSDESCRIPTIN='" + goodsdes + "',SHIPMENTDATE=TO_DATE('" + shipmentdate + "','DD/MM/YYYY'),CURRENCYID='" + currency + "',TRANSITTIME='" + tansittime + "',HSCODE='" + hscode + "',REMARKS='" + remark + "',ISUPDATE='" + isUpdate + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE=SYSDATE WHERE STATUS=1 AND  ID ='" + id + "' AND FIRSTAPVRSELECT=0 ");
    }
    public int UpdateFreight(int id, int shipmentid, int loadingport, int dischargeport, int conttypeid, int contquantity, string goodsdes, string shipmentdate, int currency, string tansittime, string hscode, string remark, int isUpdate)
    {
        return dm.ExecuteNonQuery(@"update BIL_FREIGHT set SHIPMENTID='" + shipmentid + "',LOADINGPORTID='" + loadingport + "',DISCHARGEPORTID='" + dischargeport + "',CONTTYPEID='" + conttypeid + "',CONTQUANTITY='" + contquantity + "',GOODSDESCRIPTIN='" + goodsdes + "',SHIPMENTDATE=TO_DATE('" + shipmentdate + "','DD/MM/YYYY'),CURRENCYID='" + currency + "',TRANSITTIME='" + tansittime + "',HSCODE='" + hscode + "',REMARKS='" + remark + "',ISUPDATE='" + isUpdate + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE=SYSDATE WHERE  ID ='" + id + "' AND FIRSTAPVRSELECT=0 AND STATUS=1 ");
    }
    public int InsertFreightRate(int freightid, int forwarderid, decimal offerprice, int isrecom, string remarks, string attachment)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_FREIGHTRATE(FREIGHTID,FORWARDERID,OFFERPRICE,ISRECOMMENDED,REMARKS,ATTACHMENT)
             VALUES('" + freightid + "','" + forwarderid + "','" + offerprice + "','" + isrecom + "','" + remarks + "','" + attachment + "')");
    }
    public int InsertFreightRateFA(int freightid, int forwarderid, decimal offerprice, int isrecom, string remarks, string attachment, int firstApp, int finalApp)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_FREIGHTRATE(FREIGHTID,FORWARDERID,OFFERPRICE,ISRECOMMENDED,REMARKS,ATTACHMENT,FIRSTAPPRECOM,FINALAPPRECOM)
             VALUES('" + freightid + "','" + forwarderid + "','" + offerprice + "','" + isrecom + "','" + remarks + "','" + attachment + "','" + firstApp + "','" + finalApp + "')");
    }
    public int InsertFreightRateDoc(int freightid, int forwarderid, string attachment)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_FREIGHTRATEDOC(FREIGHTID,FORWARDERID,ATTACHMENT)
             VALUES('" + freightid + "','" + forwarderid + "','" + attachment + "')");
    }
    public int UpdateFreightRate(int freightid, int forwarderid, decimal offerprice, int isrecom, string remarks, string attachment)
    {
        return dm.ExecuteNonQuery(@"update BIL_FREIGHTRATE set FORWARDERID='" + forwarderid + "', OFFERPRICE='" + offerprice + "', ISRECOMMENDED='" + isrecom + "', REMARKS='" + remarks + "',ATTACHMENT='" + attachment + "' WHERE FREIGHTID='" + freightid + "' ");
    }
    public int DeleteFreightRate(int freihtid)
    {
        return dm.ExecuteNonQuery(@"DELETE FROM BIL_FREIGHTRATE WHERE FREIGHTID=" + freihtid);
    }
    public int DeleteFreightRateDoc(int freihtid)
    {
        return dm.ExecuteNonQuery(@"DELETE FROM BIL_FREIGHTRATEDOC WHERE FREIGHTID=" + freihtid);
    }
    public int Delete(int id)
    {
        return dm.ExecuteNonQuery(@"DELETE FROM BIL_FREIGHT WHERE ID=" + id);
    }
    public DataTable GetShipment()
    {
        return dm.ExecuteQuery("Select S.*,S.ID ||'-' || S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE STATUS=0 AND ISOPEN=1 ORDER BY S.ID DESC");
    }
    public DataTable GetShipmentComparision()
    {
        return dm.ExecuteQuery("Select S.*,S.ID ||'-' || S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE STATUS=1 AND ISOPEN=1");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_FREIGHT WHERE ID=" + id);
    }

    public DataTable GetFreightDetails(int fid)
    {
        return dm.ExecuteQuery(@"SELECT F.*,S.*,P.PORTNAME LOADINGPORT ,D.PORTNAME DISCHARGEPORT, C.CONTAINERTYPE, CU.CURRENCYCODE  FROM BIL_FREIGHT F 
        INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
        INNER JOIN BIL_PORT P ON P.ID = F.LOADINGPORTID 
        INNER JOIN BIL_PORT D ON D.ID = F.DISCHARGEPORTID
        INNER JOIN BIL_CONTAINERTYPE C ON C.ID = F.CONTTYPEID
        INNER JOIN ASSET_CURRENCY CU ON CU.ID= F.CURRENCYID 
        WHERE F.ID=" + fid);
    }
    public DataTable GetByFreightId(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_FREIGHT WHERE ID=" + id);
    }
    public DataTable FreightDetails(int shipmentid)
    {
        return dm.ExecuteQuery(@"SELECT F.*,S.*,FR.*,FD.FORWARDER,P.PORTNAME LOADINGPORT ,D.PORTNAME DISCHARGEPORT, C.CONTAINERTYPE, CU.CURRENCYCODE  FROM BIL_FREIGHT F 
        INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
        INNER JOIN BIL_PORT P ON P.ID = F.LOADINGPORTID 
        INNER JOIN BIL_PORT D ON D.ID = F.DISCHARGEPORTID
        INNER JOIN BIL_CONTAINERTYPE C ON C.ID = F.CONTTYPEID
        INNER JOIN ASSET_CURRENCY CU ON CU.ID= F.CURRENCYID 
        LEFT OUTER JOIN BIL_FREIGHTRATE FR ON FR.FREIGHTID = F.ID
        INNER JOIN BIL_FORWARDER FD ON FD.ID = FR.FORWARDERID
        WHERE FR.FINALAPPRECOM=1 AND F.SHIPMENTID =" + shipmentid);
    }
    public DataTable FreightMail(int shipmentid)
    {
        return dm.ExecuteQuery(@"SELECT F.*,S.PINO ,P.PORTNAME LOADINGPORT ,D.PORTNAME DISCHARGEPORT, C.CONTAINERTYPE, CU.CURRENCYCODE  FROM BIL_FREIGHT F 
        INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
        INNER JOIN BIL_PORT P ON P.ID = F.LOADINGPORTID 
        INNER JOIN BIL_PORT D ON D.ID = F.DISCHARGEPORTID
        INNER JOIN BIL_CONTAINERTYPE C ON C.ID = F.CONTTYPEID
        INNER JOIN ASSET_CURRENCY CU ON CU.ID= F.CURRENCYID 
        WHERE F.ID =" + shipmentid);
    }
    public DataTable FreightMailTO(int freightid)
    {
        return dm.ExecuteQuery(@"SELECT FR.*, BF.SHIPMENTID, F.EMAIL FROM BIL_FREIGHTRATE FR 
        INNER JOIN BIL_FREIGHT BF ON BF.ID = FR.FREIGHTID
        INNER JOIN BIL_FORWARDER F ON F.ID = FR.FORWARDERID
        WHERE FR.FREIGHTID =" + freightid);
    }
    public List<string> GetByForwarder(int freightid)
    {
        List<string> list = new List<string>();
        DataTable dt = dm.ExecuteQuery(@"SELECT FR.*, BF.SHIPMENTID, F.EMAIL FROM BIL_FREIGHTRATE FR 
        INNER JOIN BIL_FREIGHT BF ON BF.ID = FR.FREIGHTID
        INNER JOIN BIL_FORWARDER F ON F.ID = FR.FORWARDERID
        WHERE FR.FREIGHTID ='" + freightid + "'");
        foreach (DataRow dr in dt.Rows)
        {
            list.Add(dr["EMAIL"].ToString());
        }
        return list;
    }

    public DataTable GetForwarder(int fid)
    {
        return dm.ExecuteQuery(@"SELECT * FROM BIL_FREIGHTRATE FR
                        WHERE FR.ISRECOMMENDED=1 AND FR.FREIGHTID=" + fid);
    }

    public DataTable GetForwarderR(int shipmentid)
    {
        return dm.ExecuteQuery(@"SELECT FR.*, FO.FORWARDER,F.* FROM BIL_FREIGHTRATE FR 
        INNER JOIN BIL_FORWARDER FO ON FO.ID= FR.FORWARDERID
        INNER JOIN BIL_FREIGHT F ON F.ID = FR.FREIGHTID
        INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
        WHERE FR.ISRECOMMENDED=1 AND F.SHIPMENTID =" + shipmentid);
    }
    public DataTable GetForwarderRate(int freightid)
    {
        return dm.ExecuteQuery(@"SELECT FR.*, F.* FROM BIL_FREIGHTRATE FR 
         INNER JOIN BIL_FORWARDER F ON F.ID = FR.FORWARDERID
         WHERE FR.FREIGHTID" + freightid);
    }

    public int GetFreightId(int shipmentId)
    {
        DataTable dt = dm.ExecuteQuery(@"SELECT * FROM BIL_FREIGHT F INNER JOIN BIL_FREIGHTRATE FR ON FR.FREIGHTID = F.ID WHERE F.SHIPMENTID =" + shipmentId);

        if (dt.Rows.Count > 0)
        {
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        else
        {
            return 0;
        }
    }
    public DataTable GetFreightDetail(int status)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT F.*,S.PINO , F.SHIPMENTID, P.PORTNAME LOADINGPORT ,D.PORTNAME DISCHARGEPORT, C.CONTAINERTYPE, CU.CURRENCYCODE  FROM BIL_FREIGHT F 
            INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
            INNER JOIN BIL_PORT P ON P.ID = F.LOADINGPORTID 
            INNER JOIN BIL_PORT D ON D.ID = F.DISCHARGEPORTID
            INNER JOIN BIL_CONTAINERTYPE C ON C.ID = F.CONTTYPEID
            INNER JOIN ASSET_CURRENCY CU ON CU.ID= F.CURRENCYID 
            WHERE F.STATUS='" + status + "'");
    }
    public DataTable GetFreightDetailComp(int status, int isRecom)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT F.*,S.PINO ,P.PORTNAME LOADINGPORT ,D.PORTNAME DISCHARGEPORT, C.CONTAINERTYPE, CU.CURRENCYCODE  FROM BIL_FREIGHT F 
            INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
            INNER JOIN BIL_FREIGHTRATE FR ON FR.FREIGHTID= F.ID
            INNER JOIN BIL_PORT P ON P.ID = F.LOADINGPORTID 
            INNER JOIN BIL_PORT D ON D.ID = F.DISCHARGEPORTID
            INNER JOIN BIL_CONTAINERTYPE C ON C.ID = F.CONTTYPEID
            INNER JOIN ASSET_CURRENCY CU ON CU.ID= F.CURRENCYID 
            WHERE F.STATUS='" + status + "' AND FR.ISRECOMMENDED='" + isRecom + "'");
    }
    public DataTable GetFreightComparisonPrint(int freightId)
    {
        return dm.ExecuteQuery(@"SELECT S.CINO, F.CONTQUANTITY, F.GOODSDESCRIPTIN, F.CREATEDBY, C.CONTAINERTYPE, F.SHIPMENTID, D.PORTNAME, S.TRGFREIGHT, F.REMARKS  FROM BIL_FREIGHT F 
        INNER JOIN BIL_SHIPMENT S ON S.ID = F.SHIPMENTID
        INNER JOIN BIL_PORT P ON P.ID = F.LOADINGPORTID 
        INNER JOIN BIL_PORT D ON D.ID = F.DISCHARGEPORTID
        INNER JOIN BIL_CONTAINERTYPE C ON C.ID = F.CONTTYPEID
        INNER JOIN ASSET_CURRENCY CU ON CU.ID= F.CURRENCYID
        WHERE F.ID>0 AND F.ID=" + freightId);
    }

    public DataTable ForwarderList(int freightid)
    {
        string query = @"SELECT FR.*, F.FORWARDER FROM BIL_FREIGHTRATE FR
                        INNER JOIN BIL_FORWARDER F ON F.ID = FR.FORWARDERID 
                        WHERE ID>0 AND FR.FREIGHTID ='" + freightid + "' ";
        return dm.ExecuteQuery(query);
    }
    public DataTable LoadForwarderList(int freightid)
    {
        string query = @"SELECT FR.*, F.FORWARDER FROM BIL_FREIGHTRATE FR
                        INNER JOIN BIL_FORWARDER F ON F.ID = FR.FORWARDERID 
                        WHERE FR.ISRECOMMENDED=1 AND FR.FREIGHTID ='" + freightid + "' ";
        return dm.ExecuteQuery(query);
    }
    public bool CheckDuplicate(int shipmentId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_FREIGHT WHERE SHIPMENTID='" + shipmentId + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetLastId()
    {
        DataTable dt = dm.ExecuteQuery("Select ID from BIL_FREIGHT ORDER BY ID DESC ");
        if (dt.Rows.Count > 0)
        {
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        else
        {
            return 0;
        }
    }

    public DataTable MailFrom(string staffid)
    {
        return dm.ExecuteQuery(@"SELECT * FROM EM_EMPLOYEE E WHERE E.STAFFID='" + staffid + "'");
    }
    public DataTable GetShipmentDetails(int shipmentId)
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
            INNER JOIN ASSET_UNIT U ON U.ID = S.UNITID WHERE S.ID=" + shipmentId);
    }

    public DataTable GetAllDocument(int freightId)
    {
        return dm.ExecuteQuery(@"SELECT FD.*, FO.FORWARDER FROM BIL_FREIGHTRATEDOC FD 
        LEFT OUTER JOIN BIL_FORWARDER FO ON FO.ID = FD.FORWARDERID
        LEFT OUTER JOIN BIL_FREIGHT F ON F.ID = FD.FREIGHTID
        WHERE FD.FREIGHTID = " + freightId);
    }
}