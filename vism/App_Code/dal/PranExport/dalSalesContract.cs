using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalSalesContract
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalSalesContract()
    {
    }
    public int Insert(int shipmentId, int salesContract, string ItemCode, string itemName, decimal sizegm, string HSCode, int totalPCS, decimal cbm, decimal totalcbn, decimal grossWeight, decimal totalGrossWeight, int pcsInCTN, int totalCarton, decimal ratePerCTN, decimal totalAmountCFR)
    {
        return dm.ExecuteNonQuery(@"Insert into PE_SALESCONTRACT(ID,SHIPMENTID,SALESCONTRACT,ITEMCODE,ITEMNAME,SIZEGM,HSCODE,TOTALPCS,CBM,TOTALCBN,GROSSWEIGHT,TOTALGWEIGHT,PCSINCTN,TOTALCARTON,RATEPCTN,TOTALAMOUNTCFR,CREATEDBY)
        VALUES(SQ_PE_SALESCONTRACT.NEXTVAL,'" + shipmentId + "','" + salesContract + "','" + ItemCode + "','" + itemName + "','" + sizegm + "','" + HSCode + "','" + totalPCS + "','" + cbm + "','" + totalcbn + "','" + grossWeight + "','" + totalGrossWeight + "','" + pcsInCTN + "','" + totalCarton + "','" + ratePerCTN + "','" + totalAmountCFR + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int shipmentId, int salesContract, string ItemCode, string itemName, decimal sizegm, string HSCode, int totalPCS, decimal cbm, decimal totalcbn, decimal grossWeight, decimal totalGrossWeight, int pcsInCTN, int totalCarton, decimal ratePerCTN, decimal totalAmountCFR)
    {
        return dm.ExecuteNonQuery("update PE_SALESCONTRACT set SHIPMENTID='" + shipmentId + "',SALESCONTRACT='" + salesContract + "',ITEMCODE='" + ItemCode + "',ITEMNAME='" + itemName + "',SIZEGM='" + sizegm + "',HSCODE='" + HSCode + "',TOTALPCS='" + totalPCS + "',CBM='" + cbm + "',TOTALCBN='" + totalcbn + "',GROSSWEIGHT='" + grossWeight + "',TOTALGWEIGHT='" + totalGrossWeight + "',PCSINCTN='" + pcsInCTN + "',TOTALCARTON='" + totalCarton + "',RATEPCTN='" + ratePerCTN + "',TOTALAMOUNTCFR='" + totalAmountCFR + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PE_SALESCONTRACT WHERE ID=" + id);
    }

    public string GetByItemCode(int shipmentId, int itemId)
    {
        DataTable dt = dm.ExecuteQuery(@"SELECT S.ITEMCODE FROM PE_SALESCONTRACT S WHERE S.SHIPMENTID='" + shipmentId + "' AND S.ID='" + itemId + "'");
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0][0].ToString();
        }
        else
        {
            return "";
        }
    }

    public DataTable GetViewAll(int shipmentId)
    {
        return dm.ExecuteQuery(@" SELECT * FROM PE_SHIPMENT S WHERE S.ID='" + shipmentId + "'");
    }
    public DataTable GetAllItem(string itemCode)
    {
        return dm.ExecuteQuery(@" SELECT * FROM PE_ITEMMASTER IM WHERE IM.ITEM_ID='" + itemCode + "'");
    }
    public DataTable GetViewAllItem(string casesuit)
    {
        return dm.ExecuteQuery(@" SELECT * FROM PE_SHIPMENT S WHERE S.ID='" + casesuit + "'");
    }
    public DataTable GetAllItemList(int shipmentId)
    {
        return dm.ExecuteQuery(@"SELECT SC.ID, SC.SHIPMENTID, SC.SALESCONTRACT, SC.ITEMCODE, SC.ITEMNAME, SC.SIZEGM, SC.HSCODE,SC.TOTALCARTON,
        SC.PCSINCTN,SC.RATEPCTN, SC.TOTALAMOUNTCFR, SC.TOTALPCS, SC.CBM, SC.TOTALCBN, SC.GROSSWEIGHT, SC.TOTALGWEIGHT,
        (SELECT SUM(SC.TOTALCARTON) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID= S.ID ) TTOTALCARTON,
        (SELECT SUM(SC.TOTALPCS) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID= S.ID ) TTOTALPCS,
        (SELECT SUM( SC.TOTALAMOUNTCFR) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALAMOUNTCFR,
        (SELECT SUM( SC.TOTALCBN) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALCBN,
        (SELECT SUM( SC.TOTALGWEIGHT) FROM PE_SALESCONTRACT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALGWEIGHT
        FROM PE_SALESCONTRACT SC
        INNER JOIN PE_SHIPMENT S ON S.ID = '" + shipmentId + "' WHERE SC.SHIPMENTID='" + shipmentId + "' GROUP BY SC.ID, S.ID, SC.SHIPMENTID, SC.SALESCONTRACT, SC.ITEMCODE, SC.ITEMNAME,SC.SIZEGM, SC.HSCODE,SC.TOTALCARTON,SC.RATEPCTN,SC.PCSINCTN, SC.TOTALAMOUNTCFR, SC.TOTALPCS, SC.CBM, SC.TOTALCBN, SC.GROSSWEIGHT, SC.TOTALGWEIGHT");
    }
    public bool CheckDuplicate(int shipmentId, int salesContract, string itemCode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PE_SALESCONTRACT WHERE SHIPMENTID='" + shipmentId + "' AND SALESCONTRACT='" + salesContract + "' AND ITEMCODE='" + itemCode + "'");
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
