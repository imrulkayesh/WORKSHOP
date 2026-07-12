using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalProductionDetails
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalProductionDetails()
    {
    }
    public int Insert(int lineId, int machineId, int productionLineid, string productionDate, int shiftId, int fHour, int tHour, decimal productionWeightIn, decimal productionWeightOut, int aQuantity, int bQuantity, int cQuantity, int problemId, int quantity, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_PRODETAILS(ID,LINEID,MACHINEID,PROLINEID,PRODATE,SHIFTID,FHOURID,TOHOURID,PROAVGWEIGHT,PROTOTALWEIGHT,AQUANTITY,BQUANTITY,CQUANTITY,PROBLEMID,QUANTITY,REMARKS,CREATEDBY)
        VALUES(SQ_PR_PRODETAILS.NEXTVAL,'" + lineId + "','" + machineId + "','" + productionLineid + "',TO_DATE('" + productionDate + "','DD/MM/YYYY'),'" + shiftId + "','" + fHour + "','" + tHour + "','" + productionWeightIn + "','" + productionWeightOut + "','" + aQuantity + "','" + bQuantity + "','" + cQuantity + "','" + problemId + "','" + quantity + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public int Update(int id, int lineId, int machineId, int productionLineid, string productionDate, int shiftId, int fHour, int tHour, decimal productionWeightIn, decimal productionWeightOut, int aQuantity, int bQuantity, int cQuantity, int problemId, int quantity, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_PRODETAILS set LINEID='" + lineId + "',MACHINEID='" + machineId + "', PROLINEID='" + productionLineid + "',PRODATE=TO_DATE('" + productionDate + "','DD/MM/YYYY'),SHIFTID='" + shiftId + "',FHOURID='" + fHour + "',TOHOURID='" + tHour + "',PROAVGWEIGHT='" + productionWeightIn + "',PROTOTALWEIGHT='" + productionWeightOut + "',AQUANTITY='" + aQuantity + "',BQUANTITY='" + bQuantity + "',CQUANTITY='" + cQuantity + "',PROBLEMID='" + problemId + "',QUANTITY='" + quantity + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_PRODETAILS WHERE ID=" + id);
    }
    public DataTable GetALL(int lineId, int machineId, string date)
    {
        //string fromDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
        //string toDate = DateTime.Now.ToString("dd/MM/yyyy");

        return dm.ExecuteQuery(@"SELECT P.*, L.LINENAME, H.HOURNAME FHOUR, TH.HOURNAME THOUR  FROM PR_PRODETAILS P 
        INNER JOIN PR_MLINE L ON L.ID = P.LINEID
        INNER JOIN PR_MHOUR H ON H.ID = P.FHOURID
        INNER JOIN PR_MHOUR TH ON TH.ID = P.TOHOURID
        WHERE P.LINEID='" + lineId + "' AND P.MACHINEID='" + machineId + "' AND TRUNC(P.PRODATE)= TO_DATE('" + date + "','DD/MM/YYYY')");
    }

    public DataTable GetUserWiseLine(string staffid)
    {
        return dm.ExecuteQuery(@"SELECT ML.* FROM PR_MLINE ML 
        INNER JOIN PR_USERWISELP UL ON UL.LINEID = ML.ID
        WHERE UL.STAFFID =" + SessionManager.SessionName.StaffId);
    }

    public DataTable GetMWItem(int lineId, int machineId, string date)
    {
        return dm.ExecuteQuery(@"SELECT P.ID PRODUCTIONID,P.PRODTIONDATE, IT.ID, MIT.ITEMNAME FROM PR_PRODUCTION P 
        INNER JOIN PR_MLINE L ON L.ID = P.LINEID
        INNER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
        INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
        INNER JOIN PR_ITEM IT ON IT.ID = P.ITEMID
        INNER JOIN PR_MITEM MIT ON MIT.ID = IT.ITEMID
        WHERE P.LINEID='" + lineId + "' AND P.MACHINEID='" + machineId + "' AND TRUNC(P.PRODTIONDATE)= TO_DATE('" + date + "','DD/MM/YYYY')");
    }
    public DataTable GetMWMould(int lineId, int machineId, string date)
    {
        return dm.ExecuteQuery(@"SELECT  P.ID PRODUCTION,P.PRODTIONDATE,MU.ID, MMU.MOULDNAME FROM PR_PRODUCTION P 
        INNER JOIN PR_MLINE L ON L.ID = P.LINEID
        INNER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
        INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
        INNER JOIN PR_MOULD MU ON MU.ID = P.MOULDID
        INNER JOIN PR_MMOULD MMU ON MMU.ID = MU.MOULDID
        WHERE P.LINEID='" + lineId + "' AND P.MACHINEID='" + machineId + "' AND TRUNC(P.PRODTIONDATE)= TO_DATE('" + date + "','DD/MM/YYYY')");
    }
    public DataTable GetMWOperator(int lineId, int machineId, string date)
    {
        return dm.ExecuteQuery(@"SELECT  P.ID PRODUCTION,P.PRODTIONDATE,OP.ID, OP.OPERATORNAME FROM PR_PRODUCTION P 
        INNER JOIN PR_MLINE L ON L.ID = P.LINEID
        INNER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
        INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
        LEFT OUTER JOIN PR_MOPERATOR OP ON OP.ID = P.OPERATORID
        WHERE P.LINEID='" + lineId + "' AND P.MACHINEID='" + machineId + "' AND TRUNC(P.PRODTIONDATE)= TO_DATE('" + date + "','DD/MM/YYYY')");
    }
    public DataTable GetMWIncharge(int lineId, int machineId, string date)
    {
        return dm.ExecuteQuery(@"SELECT  P.ID PRODUCTION,P.PRODTIONDATE,MI.ID, MI.INCHARGENAME FROM PR_PRODUCTION P 
        INNER JOIN PR_MLINE L ON L.ID = P.LINEID
        INNER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
        INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
        LEFT OUTER JOIN PR_MINCHARGE MI ON MI.ID = P.INCHARGEID
        WHERE P.LINEID='" + lineId + "' AND P.MACHINEID='" + machineId + "' AND TRUNC(P.PRODTIONDATE)= TO_DATE('" + date + "','DD/MM/YYYY')");
    }
    public DataTable GetViewAll(int lineId, int machineId, string date)
    {
        return dm.ExecuteQuery(@" SELECT P.ID,P.SHIFTID, P.CREATEDATE, P.PRODTIONDATE, L.LINENAME, MS.SHIFTNAME, MI.INCHARGENAME, OP.OPERATORNAME, 
        MM.MACHINENAME, MMU.MOULDNAME, MIT.ITEMNAME FROM PR_PRODUCTION P 
        INNER JOIN PR_MLINE L ON L.ID = P.LINEID
        INNER JOIN PR_SHIFT S ON S.ID = P.SHIFTID
        INNER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        LEFT OUTER JOIN PR_MINCHARGE MI ON MI.ID = P.INCHARGEID
        LEFT OUTER JOIN PR_MOPERATOR OP ON OP.ID = P.OPERATORID
        INNER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
        INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
        INNER JOIN PR_MOULD MU ON MU.ID = P.MOULDID
        INNER JOIN PR_MMOULD MMU ON MMU.ID = MU.MOULDID
        INNER JOIN PR_ITEM IT ON IT.ID = P.ITEMID
        INNER JOIN PR_MITEM MIT ON MIT.ID = IT.ITEMID 
        WHERE P.LINEID='" + lineId + "'AND P.MACHINEID='" + machineId + "' AND TRUNC(P.PRODTIONDATE)= TO_DATE('" + date + "','DD/MM/YYYY') ");
    }
    public int GetLineId(int lineId)
    {
        DataTable dt = dm.ExecuteQuery("Select ML.* from PR_MLINE  ML WHERE ML.ID=" + lineId);
        if (dt.Rows.Count > 0)
        {
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        else
        {
            return 0;
        }
    }
    public DataTable GetLineDetails(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT P.ID,P.LOCATIONID,P.LINEID, P.BQUANTITY,P.CQUANTITY, P.QUANTITY,P.CREATEDATE, BC.CENTERNAME, ML.LINENAME, MI.ITEMNAME, MS.SHIFTNAME, MM.MOULDNAME,P.PRODTIONDATE, P.STATUS, P.ACTIVE FROM PR_PRODUCTION P
        INNER JOIN PR_MLINE ML ON ML.ID = P.LINEID
        INNER JOIN BS_BUSINESSCENTER BC ON BC.ID = P.LOCATIONID
        INNER JOIN PR_SHIFT S ON S.ID = P.SHIFTID
        INNER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        INNER JOIN PR_ITEM I ON I.ID = P.ITEMID
        INNER JOIN PR_MITEM MI ON MI.ID= I.ITEMID
        INNER JOIN PR_MOULD M ON M.ID = P.MOULDID
        INNER JOIN PR_MMOULD MM ON MM.ID= M.MOULDID
        INNER JOIN PR_APPROVER A ON A.LINEID = P.LINEID
        INNER JOIN EM_EMPLOYEE EM ON EM.USERID = A.APPROVERID
        WHERE P.ID>0 " + critaria);
    }
    public bool CheckDuplicate(int lineId)
    {
        DataTable dt = dm.ExecuteQuery("@'" + lineId + "' ");
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