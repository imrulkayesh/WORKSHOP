using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



public class dalPranShipment
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalPranShipment()
    {
    }
    public int Insert(string salesContract, int origin, string expDate, int salestermid, int exporter, int emproter, int notifyparty, int beneficiaryBank, int accountNo, int shipmentmode, int placeOfLoading, int dischargePort, string container, decimal freight, string finaldestination, int currencyid, string attechment)
    {
        return dm.ExecuteNonQuery(@"Insert into PE_SHIPMENT(ID,SALESCONTRACT,ORIGIN,EXPDATE,SALESTERMID,EXPOTER,IMPORTER,NOTIFYPARTY,BENEFICIARYBANK,ACCOUNTNO,SHIPMODE,PLACEOFLOADING,DISCHARGEPORT,CONTAINERNAME,FREIGHT,FINALDESTINATION,CURRENCYID,ATTECHMENT,CREATEDBY)
        VALUES(SQ_PE_SHIPMENT.NEXTVAL,'" + salesContract + "','" + origin + "',TO_DATE('" + expDate + "','DD/MM/YYYY'),'" + salestermid + "','" + exporter + "','" + emproter + "','" + notifyparty + "','" + beneficiaryBank + "','" + accountNo + "','" + shipmentmode + "','" + placeOfLoading + "','" + dischargePort + "','" + container + "','" + freight + "','" + finaldestination + "','" + currencyid + "','" + attechment + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string salesContract, int origin, string expDate, int salestermid, int exporter, int emproter, int notifyparty, int beneficiaryBank, int accountNo, int shipmentmode, int placeOfLoading, int dischargePort, string container, decimal freight, string finaldestination, int currencyid, string attechment)
    {
        return dm.ExecuteNonQuery("update PE_SHIPMENT set SALESCONTRACT='" + salesContract + "',ORIGIN='" + origin + "',EXPDATE=TO_DATE('" + expDate + "','DD/MM/YYYY'),SALESTERMID='" + salestermid + "',EXPOTER='" + exporter + "',IMPORTER='" + emproter + "',NOTIFYPARTY='" + notifyparty + "',BENEFICIARYBANK='" + beneficiaryBank + "',ACCOUNTNO='" + accountNo + "',SHIPMODE='" + shipmentmode + "',PLACEOFLOADING='" + placeOfLoading + "',DISCHARGEPORT='" + dischargePort + "',CONTAINERNAME='" + container + "',FREIGHT='" + freight + "',FINALDESTINATION='" + finaldestination + "',CURRENCYID='" + currencyid + "',ATTECHMENT='" + attechment + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PE_SHIPMENT WHERE ID=" + id);
    }
    public DataTable ShipmentALL()
    {
        return dm.ExecuteQuery("Select * from PE_SHIPMENT");
    }
    public DataTable GetALL()
    {
        return dm.ExecuteQuery(@"SELECT S.ID, S.SALESCONTRACT, S.EXPDATE, C.COUNTRYNAME, ST.SALESTREM, EX.EXPORTERNAME, IM.IMPORTERNAME,NP.NOTIFYPARTYNAME,S.FREIGHT, S.CONTAINERNAME,
        B.BANKNAME, SM.SHIPMENTMODENAME, LP.PORTNAME LODINGPORT,DP.PORTNAME DISCHARGEPORT,S.ACCOUNTNO,CU.CURRENCYCODE, S.ATTECHMENT FROM PE_SHIPMENT S
        LEFT OUTER JOIN BS_COUNTRY C ON C.ID = S.ORIGIN
        LEFT OUTER JOIN PE_SALESTREM ST ON ST.ID = S.SALESTERMID
        LEFT OUTER JOIN PE_EXPORTER EX ON EX.ID = S.EXPOTER
        LEFT OUTER JOIN PE_IMPORTER IM ON IM.ID = S.IMPORTER
        LEFT OUTER JOIN PE_NOTIFYPARTY NP ON NP.ID = S.NOTIFYPARTY
        LEFT OUTER JOIN PE_BENEFICIARYBANK B ON B.ID = S.BENEFICIARYBANK
        LEFT OUTER JOIN PE_SHIPMENTMODE SM ON SM.ID = S.SHIPMODE
        LEFT OUTER JOIN PE_PORT LP ON LP.ID = S.PLACEOFLOADING
        LEFT OUTER JOIN PE_PORT DP ON DP.ID = S.DISCHARGEPORT
        LEFT OUTER JOIN ASSET_CURRENCY CU ON CU.ID= S.CURRENCYID");
    }

    public DataTable SalesContractDetalis(int ShipmentId)
    {
        return
            dm.ExecuteQuery(
                @"SELECT DISTINCT S.ID, S.SALESCONTRACT, TO_CHAR (EXPDATE, 'DD/MM/YYYY') EXPDATE, C.COUNTRYNAME, S.FINALDESTINATION, ST.SALESTREM, E.EXPORTERNAME, E.ADDRESS EXPOTERADDRESS, 
        M.IMPORTERNAME, M.IMPORTERADDRESS, NF.NOTIFYPARTYNAME, BB.BANKNAME, BB.BANKADDRESS, BC.ACCOUNTNUMBER,SC.SIZEGM,
        SM.SHIPMENTMODENAME, P.PORTNAME LODINGPORT, DP.PORTNAME DISCHARGPORT, CU.CURRENCYCODE,S.FREIGHT,S.CONTAINERNAME,
        ((SC.PCSINCTN) * (SC.SIZEGM)/1000) * (SC.TOTALCARTON) NETWEIGHTKGS,
        SC.ITEMNAME, SC.HSCODE, SC.PCSINCTN, SC.TOTALCARTON, SC.TOTALPCS, SC.RATEPCTN , SC.TOTALAMOUNTCFR,BB.SWIFTCODE, NF.NPADDRESS,
        (SELECT SUM( SC.TOTALCARTON) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALCARTON,
        (SELECT SUM( SC.TOTALPCS) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALPCS,
        (SELECT SUM( SC.TOTALAMOUNTCFR) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALAMOUNTCFR,
        (SELECT SUM( SC.TOTALAMOUNTCFR)- (S.FREIGHT) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) INTOTALAMOUNTCFR
        FROM PE_SALESCONTRACT SC
        INNER JOIN PE_SHIPMENT S ON S.ID = SC.SHIPMENTID
        INNER JOIN BS_COUNTRY C ON C.ID= S.ORIGIN
        INNER JOIN PE_SALESTREM ST ON ST.ID = S.SALESTERMID
        INNER JOIN PE_EXPORTER E ON E.ID = S.EXPOTER
        INNER JOIN PE_IMPORTER M ON M.ID = S.IMPORTER
        INNER JOIN PE_NOTIFYPARTY NF ON NF.ID = S.NOTIFYPARTY
        INNER JOIN PE_BENEFICIARYBANK BB ON BB.ID = S.BENEFICIARYBANK
        INNER JOIN PE_BANKACCOUNT BC ON BC.ID = S.ACCOUNTNO
        INNER JOIN PE_SHIPMENTMODE SM ON SM.ID = S.SHIPMODE
        INNER JOIN PE_PORT P ON P.ID = S.PLACEOFLOADING
        INNER JOIN PE_PORT DP ON DP.ID = S.DISCHARGEPORT
        INNER JOIN ASSET_CURRENCY CU ON CU.ID = S.CURRENCYID
        INNER JOIN PE_ITEMMASTER IM ON IM.ITEM_ID = SC.ITEMCODE
        WHERE S.ID > 0 AND SC.SHIPMENTID= '" + ShipmentId + "' " +
                "GROUP BY S.ID,SC.SHIPMENTID, S.SALESCONTRACT, S.EXPDATE , C.COUNTRYNAME, S.FINALDESTINATION, ST.SALESTREM, E.EXPORTERNAME," +
                " E.ADDRESS, M.IMPORTERNAME, M.IMPORTERADDRESS, NF.NOTIFYPARTYNAME, BB.BANKNAME, BB.BANKADDRESS, BC.ACCOUNTNUMBER," +
                "SM.SHIPMENTMODENAME, P.PORTNAME, DP.PORTNAME, CU.CURRENCYCODE,SC.ITEMNAME, SC.HSCODE, SC.PCSINCTN," +
                " SC.TOTALCARTON, SC.TOTALPCS, SC.RATEPCTN , SC.TOTALAMOUNTCFR,FREIGHT, S.CONTAINERNAME, SC.SIZEGM,BB.SWIFTCODE, NF.NPADDRESS");
    }

    public DataTable SalesContractVatDetalis(int ShipmentId)
    {
        return
            dm.ExecuteQuery(
                @"SELECT DISTINCT S.ID, S.SALESCONTRACT, TO_CHAR (EXPDATE, 'DD/MM/YYYY') EXPDATE, C.COUNTRYNAME, S.FINALDESTINATION, ST.SALESTREM, E.EXPORTERNAME, E.ADDRESS EXPOTERADDRESS, 
        M.IMPORTERNAME, M.IMPORTERADDRESS, NF.NOTIFYPARTYNAME, BB.BANKNAME, BB.BANKADDRESS, BC.ACCOUNTNUMBER,SC.SIZEGM,
        SM.SHIPMENTMODENAME, P.PORTNAME LODINGPORT, DP.PORTNAME DISCHARGPORT, CU.CURRENCYCODE,S.FREIGHT,S.CONTAINERNAME,
        SC.ITEMNAME, SC.HSCODE, SC.PCSINCTN, SC.TOTALCARTON, SC.TOTALPCS, SC.RATEPCTN , SC.TOTALAMOUNTCFR,BB.SWIFTCODE, NF.NPADDRESS,
        ((SC.PCSINCTN) * (SC.SIZEGM)/1000) * (SC.TOTALCARTON) NETWEIGHTKGS,
        (SELECT SUM( SC.TOTALCARTON) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALCARTON,
        (SELECT SUM( SC.TOTALPCS) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALPCS,
        (SELECT SUM( SC.TOTALAMOUNTCFR) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALAMOUNTCFR,
        (SELECT SUM( SC.TOTALAMOUNTCFR)- (S.FREIGHT) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) INTOTALAMOUNTCFR
        FROM PE_SALESCONTRACTVAT SC
        INNER JOIN PE_SHIPMENT S ON S.ID = SC.SHIPMENTID
        INNER JOIN BS_COUNTRY C ON C.ID= S.ORIGIN
        INNER JOIN PE_SALESTREM ST ON ST.ID = S.SALESTERMID
        INNER JOIN PE_EXPORTER E ON E.ID = S.EXPOTER
        INNER JOIN PE_IMPORTER M ON M.ID = S.IMPORTER
        INNER JOIN PE_NOTIFYPARTY NF ON NF.ID = S.NOTIFYPARTY
        INNER JOIN PE_BENEFICIARYBANK BB ON BB.ID = S.BENEFICIARYBANK
        INNER JOIN PE_BANKACCOUNT BC ON BC.ID = S.ACCOUNTNO
        INNER JOIN PE_SHIPMENTMODE SM ON SM.ID = S.SHIPMODE
        INNER JOIN PE_PORT P ON P.ID = S.PLACEOFLOADING
        INNER JOIN PE_PORT DP ON DP.ID = S.DISCHARGEPORT
        INNER JOIN ASSET_CURRENCY CU ON CU.ID = S.CURRENCYID
        INNER JOIN PE_ITEMMASTER IM ON IM.ITEM_ID = SC.ITEMCODE
        WHERE S.ID > 0 AND SC.SHIPMENTID= '" + ShipmentId + "' " +
                "GROUP BY S.ID,SC.SHIPMENTID, S.SALESCONTRACT, S.EXPDATE , C.COUNTRYNAME, S.FINALDESTINATION, ST.SALESTREM, E.EXPORTERNAME," +
                "E.ADDRESS, M.IMPORTERNAME, M.IMPORTERADDRESS, NF.NOTIFYPARTYNAME, BB.BANKNAME, BB.BANKADDRESS, BC.ACCOUNTNUMBER, " +
                "SM.SHIPMENTMODENAME, P.PORTNAME, DP.PORTNAME, CU.CURRENCYCODE,SC.ITEMNAME, SC.HSCODE, SC.PCSINCTN, " +
                "SC.TOTALCARTON, SC.TOTALPCS, SC.RATEPCTN , SC.TOTALAMOUNTCFR,FREIGHT, S.CONTAINERNAME, SC.SIZEGM,BB.SWIFTCODE, NF.NPADDRESS");
    }
    public DataTable DiscriptionOfGoods()
    {
        return dm.ExecuteQuery(@"SELECT SC.ID, SC.SHIPMENTID,IM.ITEM_NAME, SC.HSCODE, SC.PCSINCTN, SC.TOTALCARTON, SC.TOTALPCS, SC.RATEPCTN, SC.TOTALAMOUNTCFR FROM PE_SALESCONTRACT SC
        INNER JOIN PE_SHIPMENT S ON S.ID = SC.SHIPMENTID
        INNER JOIN PE_ITEMMASTER IM ON IM.ITEM_ID = SC.ITEMCODE");
    }
    public DataTable GetByBank(int bankId)
    {
        return dm.ExecuteQuery(@"SELECT BB.ID, BC.ACCOUNTNUMBER FROM PE_BANKACCOUNT BC 
        INNER JOIN PE_BENEFICIARYBANK BB ON BB.ID = BC.BANKID
        WHERE BB.ID=" + bankId);
    }
    public bool CheckDuplicate(string salesContract)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PE_SHIPMENT WHERE SALESCONTRACT='" + salesContract + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    //public bool CheckDuplicate(string salesContract, string expDate)
    //{
    //    DataTable dt = dm.ExecuteQuery("Select * from PE_SHIPMENT WHERE SALESCONTRACT='" + salesContract + "',EXPDATE=TO_DATE('" + expDate + "','DD/MM/YYYY') ");
    //    if (dt.Rows.Count > 0)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
}