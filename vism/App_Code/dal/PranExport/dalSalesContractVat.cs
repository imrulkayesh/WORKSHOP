using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalSalesContractVat
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalSalesContractVat()
    {
    }
    public int Insert(int shipmentId, int salesContract, string ItemCode, string itemName, decimal sizeGM, string HSCode, int totalPCS, decimal cbm, decimal totalcbn, decimal grossWeight, decimal totalGrossWeight, int pcsInCTN, int totalCarton, decimal ratePerCTN, decimal totalAmountCFR)
    {
        return dm.ExecuteNonQuery(@"Insert into PE_SALESCONTRACTVAT(ID,SHIPMENTID,SALESCONTRACT,ITEMCODE,ITEMNAME,SIZEGM,HSCODE,TOTALPCS,CBM,TOTALCBN,GROSSWEIGHT,TOTALGWEIGHT,PCSINCTN,TOTALCARTON,RATEPCTN,TOTALAMOUNTCFR,CREATEDBY)
        VALUES(SQ_PE_SALESCONTRACTVAT.NEXTVAL,'" + shipmentId + "','" + salesContract + "','" + ItemCode + "','" + itemName + "','" + sizeGM + "','" + HSCode + "','" + totalPCS + "','" + cbm + "','" + totalcbn + "','" + grossWeight + "','" + totalGrossWeight + "','" + pcsInCTN + "','" + totalCarton + "','" + ratePerCTN + "','" + totalAmountCFR + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int shipmentId, int salesContract, string ItemCode, string itemName, decimal sizeGM, string HSCode, int totalPCS, decimal cbm, decimal totalcbn, decimal grossWeight, decimal totalGrossWeight, int pcsInCTN, int totalCarton, decimal ratePerCTN, decimal totalAmountCFR)
    {
        return dm.ExecuteNonQuery("update PE_SALESCONTRACTVAT set SHIPMENTID='" + shipmentId + "',SALESCONTRACT='" + salesContract + "',ITEMCODE='" + ItemCode + "',ITEMNAME='" + itemName + "',SIZEGM='" + sizeGM + "',HSCODE='" + HSCode + "',TOTALPCS='" + totalPCS + "',CBM='" + cbm + "',TOTALCBN='" + totalcbn + "',GROSSWEIGHT='" + grossWeight + "',TOTALGWEIGHT='" + totalGrossWeight + "',PCSINCTN='" + pcsInCTN + "',TOTALCARTON='" + totalCarton + "',RATEPCTN='" + ratePerCTN + "',TOTALAMOUNTCFR='" + totalAmountCFR + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PE_SALESCONTRACTVAT WHERE ID=" + id);
    }

    public DataTable DeleteSalesContractVat(int shipmentId, string ItemCode)
    {
        return dm.ExecuteQuery("DELETE from PE_SALESCONTRACTVAT S WHERE S.SHIPMENTID='" + shipmentId + "' AND S.ITEMCODE='" + ItemCode + "' ");
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
        return dm.ExecuteQuery(@"SELECT SC.ID, SC.SHIPMENTID, SC.SALESCONTRACT, SC.ITEMCODE, SC.ITEMNAME, SC.SIZEGM,SC.HSCODE,SC.TOTALCARTON,
        SC.PCSINCTN,SC.RATEPCTN, SC.TOTALAMOUNTCFR, SC.TOTALPCS, SC.CBM, SC.TOTALCBN, SC.GROSSWEIGHT, SC.TOTALGWEIGHT,
        (SELECT SUM(SC.TOTALCARTON) FROM PE_SALESCONTRACTVAT SC WHERE SC.SHIPMENTID= S.ID ) TTOTALCARTON,
        (SELECT SUM(SC.TOTALPCS) FROM PE_SALESCONTRACTVAT SC WHERE SC.SHIPMENTID= S.ID ) TTOTALPCS,
        (SELECT SUM( SC.TOTALAMOUNTCFR) FROM PE_SALESCONTRACTVAT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALAMOUNTCFR,
        (SELECT SUM( SC.TOTALCBN) FROM PE_SALESCONTRACTVAT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALCBN,
        (SELECT SUM( SC.TOTALGWEIGHT) FROM PE_SALESCONTRACTVAT SC WHERE SC.SHIPMENTID =S.ID ) TTOTALGWEIGHT
        FROM PE_SALESCONTRACTVAT SC
        INNER JOIN PE_SHIPMENT S ON S.ID = '" + shipmentId + "' WHERE SC.SHIPMENTID='" + shipmentId + "' GROUP BY SC.ID, S.ID, SC.SHIPMENTID, SC.SALESCONTRACT, SC.ITEMCODE, SC.ITEMNAME,SC.SIZEGM, SC.HSCODE,SC.TOTALCARTON,SC.RATEPCTN,SC.PCSINCTN, SC.TOTALAMOUNTCFR, SC.TOTALPCS, SC.CBM, SC.TOTALCBN, SC.GROSSWEIGHT, SC.TOTALGWEIGHT");
    }
    public bool CheckDuplicate(int shipmentId, int salesContract, string itemCode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PE_SALESCONTRACTVAT WHERE SHIPMENTID='" + shipmentId + "' AND SALESCONTRACT='" + salesContract + "' AND ITEMCODE='" + itemCode + "'");
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