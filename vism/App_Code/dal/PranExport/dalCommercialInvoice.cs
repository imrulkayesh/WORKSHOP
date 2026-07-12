using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalCommercialInvoice
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCommercialInvoice()
    {
    }
    public int Insert(int shipmentId, int salesContract, string ItemCode, string itemName, string HSCode, int totalPCS, decimal cbn, decimal totalcbn, decimal grossWeight, decimal totalGrossWeight, int pcsInCTN, int totalCarton, decimal ratePerCTN, decimal totalAmountCFR)
    {
        return dm.ExecuteNonQuery(@"Insert into PE_COMINVOICE(ID,SHIPMENTID,SALESCONTRACT,ITEMCODE,ITEMNAME,HSCODE,TOTALPCS,CBN,TOTALCBN,GROSSWEIGHT,TOTALGWEIGHT,PCSINCTN,TOTALCARTON,RATEPCTN,TOTALAMOUNTCFR,CREATEDBY)
        VALUES(SQ_PE_COMINVOICE.NEXTVAL,'" + shipmentId + "','" + salesContract + "','" + ItemCode + "','" + itemName + "','" + HSCode + "','" + totalPCS + "','" + cbn + "','" + totalcbn + "','" + grossWeight + "','" + totalGrossWeight + "','" + pcsInCTN + "','" + totalCarton + "','" + ratePerCTN + "','" + totalAmountCFR + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int shipmentId, int salesContract, string ItemCode, string itemName, string HSCode, int totalPCS, decimal cbn, decimal totalcbn, decimal grossWeight, decimal totalGrossWeight, int pcsInCTN, int totalCarton, decimal ratePerCTN, decimal totalAmountCFR)
    {
        return dm.ExecuteNonQuery("update PE_COMINVOICE set SHIPMENTID='" + shipmentId + "',SALESCONTRACT='" + salesContract + "',ITEMCODE='" + ItemCode + "',ITEMNAME='" + itemName + "',HSCODE='" + HSCode + "',TOTALPCS='" + totalPCS + "',CBN='" + cbn + "',TOTALCBN='" + totalcbn + "',GROSSWEIGHT='" + grossWeight + "',TOTALGWEIGHT='" + totalGrossWeight + "',PCSINCTN='" + pcsInCTN + "',TOTALCARTON='" + totalCarton + "',RATEPCTN='" + ratePerCTN + "',TOTALAMOUNTCFR='" + totalAmountCFR + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PE_COMINVOICE WHERE ID=" + id);
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
        return dm.ExecuteQuery(@"SELECT SC.ID, SC.SHIPMENTID, SC.SALESCONTRACT, SC.ITEMCODE, SC.ITEMNAME, SC.HSCODE,SC.TOTALCARTON,
        SC.PCSINCTN,SC.RATEPCTN, SC.TOTALAMOUNTCFR, SC.TOTALPCS, SC.CBN, SC.TOTALCBN, SC.GROSSWEIGHT, SC.TOTALGWEIGHT,
        (SELECT SUM(SC.TOTALCARTON) FROM PE_COMINVOICE SC WHERE SC.SHIPMENTID= S.ID ) TTOTALCARTON,
        (SELECT SUM(SC.TOTALPCS) FROM PE_COMINVOICE SC WHERE SC.SHIPMENTID= S.ID ) TTOTALPCS,
        (SELECT SUM( SC.TOTALAMOUNTCFR) FROM PE_COMINVOICE SC WHERE SC.SHIPMENTID =S.ID ) TTOTALAMOUNTCFR,
        (SELECT SUM( SC.TOTALCBN) FROM PE_COMINVOICE SC WHERE SC.SHIPMENTID =S.ID ) TTOTALCBN,
        (SELECT SUM( SC.TOTALGWEIGHT) FROM PE_COMINVOICE SC WHERE SC.SHIPMENTID =S.ID ) TTOTALGWEIGHT
        FROM PE_COMINVOICE SC
        INNER JOIN PE_SHIPMENT S ON S.ID = '" + shipmentId + "' WHERE SC.SHIPMENTID=2 GROUP BY SC.ID, S.ID, SC.SHIPMENTID, SC.SALESCONTRACT, SC.ITEMCODE, SC.ITEMNAME, SC.HSCODE,SC.TOTALCARTON,SC.RATEPCTN,SC.PCSINCTN, SC.TOTALAMOUNTCFR, SC.TOTALPCS, SC.CBN, SC.TOTALCBN, SC.GROSSWEIGHT, SC.TOTALGWEIGHT");
    }
    public bool CheckDuplicate(int shipmentId, int salesContract, string itemCode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PE_COMINVOICE WHERE SHIPMENTID='" + shipmentId + "' AND SALESCONTRACT='" + salesContract + "' AND ITEMCODE='" + itemCode + "'");
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