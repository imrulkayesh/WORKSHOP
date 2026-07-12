using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalMMApproval
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMMApproval()
    {
    }

    public DataTable GetPlaningApprovalList(string staffId, int Status)
    {
        return dm.ExecuteQuery(@"Select P.*, MC.CATNAME, M.MEDIANAME,AP.ADPOSITION,C.COLORNAME from MD_PLANNING P 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = P.MEDIACATID
        INNER JOIN MD_MEDIA M ON M.ID = P.MEDIAID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = P.ADPOSITIONID
        INNER JOIN MD_BUDGET B ON B.ID=P.BUDGETID
        INNER JOIN MD_BRAND BN ON BN.ID=B.BRANDID
        INNER JOIN MD_COLOR C ON C.ID = P.COLORID WHERE P.STATUS=0 AND P.ISAPPROVED = '" + Status + "' AND BN.RESPERSON='" + staffId + "'");
    }
    public DataTable GetBillingList()
    {
        return dm.ExecuteQuery(@"Select P.*, MC.CATNAME, M.MEDIANAME,AP.ADPOSITION,C.COLORNAME from MD_PLANNING P 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = P.MEDIACATID
        INNER JOIN MD_MEDIA M ON M.ID = P.MEDIAID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = P.ADPOSITIONID
        INNER JOIN MD_BUDGET B ON B.ID=P.BUDGETID
        INNER JOIN MD_BRAND BN ON BN.ID=B.BRANDID
        INNER JOIN MD_COLOR C ON C.ID = P.COLORID WHERE P.STATUS=1 AND P.ISAPPROVED=1 AND P.ID NOT IN(SELECT PLANNINGID FROM MD_BILLING)");
    }

    public void BillApproval(int PlanId, string MrrNo)
    {
        dm.ExecuteNonQuery("update MD_PLANNING set MRRNO='" + MrrNo + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + PlanId);
        dm.ExecuteNonQuery("update MD_BILLING set ISAPPROVED=1,APVDATE=SYSDATE,APVBY='" + SessionManager.SessionName.StaffId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE PLANNINGID=" + PlanId);
    }

    public void BillReject(int PlanId)
    {
        dm.ExecuteNonQuery("update MD_BILLING set ISAPPROVED=3,APVDATE=SYSDATE,APVBY='" + SessionManager.SessionName.StaffId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE PLANNINGID=" + PlanId);
    }
    public void WorkOrder(int PlanId)
    {
        dm.ExecuteNonQuery("update MD_PLANNING set STATUS=1,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + PlanId);
    }

    public DataTable GetForWorkOrderList()
    {
        return dm.ExecuteQuery(@"Select P.*, MC.CATNAME, M.MEDIANAME,AP.ADPOSITION,C.COLORNAME from MD_PLANNING P 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = P.MEDIACATID
        INNER JOIN MD_MEDIA M ON M.ID = P.MEDIAID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = P.ADPOSITIONID
        INNER JOIN MD_BUDGET B ON B.ID=P.BUDGETID
        INNER JOIN MD_BRAND BN ON BN.ID=B.BRANDID
        INNER JOIN MD_COLOR C ON C.ID = P.COLORID WHERE P.ISAPPROVED = 1 AND P.STATUS=0");
    }
    public void BillPay(int PlanId)
    {
        dm.ExecuteNonQuery("update MD_PLANNING set STATUS=2,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + PlanId);
        dm.ExecuteNonQuery("update MD_BILLING set STATUS=1,PAYDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE PLANNINGID=" + PlanId);
    }

    public DataTable GetApprovedBillingList(int Status)
    {
        return dm.ExecuteQuery(@"Select P.*, MC.CATNAME, M.MEDIANAME,AP.ADPOSITION,C.COLORNAME from MD_PLANNING P 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = P.MEDIACATID
        INNER JOIN MD_MEDIA M ON M.ID = P.MEDIAID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = P.ADPOSITIONID
        INNER JOIN MD_BUDGET B ON B.ID=P.BUDGETID
        INNER JOIN MD_BRAND BN ON BN.ID=B.BRANDID
        INNER JOIN MD_BILLING BL ON BL.PLANNINGID=P.ID
        INNER JOIN MD_COLOR C ON C.ID = P.COLORID WHERE P.STATUS=1 AND BL.STATUS=0 AND BL.ISAPPROVED=" + Status);
    }
    public DataTable GetBillingForApproval(string StaffId, int Status)
    {
        return dm.ExecuteQuery(@"Select P.*, MC.CATNAME, M.MEDIANAME,AP.ADPOSITION,C.COLORNAME from MD_PLANNING P 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = P.MEDIACATID
        INNER JOIN MD_MEDIA M ON M.ID = P.MEDIAID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = P.ADPOSITIONID
        INNER JOIN MD_BUDGET B ON B.ID=P.BUDGETID
        INNER JOIN MD_BRAND BN ON BN.ID=B.BRANDID
        INNER JOIN MD_BILLING BL ON BL.PLANNINGID=P.ID
        INNER JOIN MD_COLOR C ON C.ID = P.COLORID WHERE P.STATUS=1 AND BL.STATUS=0 AND BL.ISAPPROVED='" + Status + "' AND BN.RESPERSON='" + StaffId + "'");
    }
    public void PlanApproval(int PlanId, string PRNo)
    {
        dm.ExecuteNonQuery("update MD_PLANNING set ISAPPROVED=1,PRNO='" + PRNo + "',APVBY='" + SessionManager.SessionName.StaffId + "',APVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + PlanId);
    }
    public void PlanReject(int PlanId)
    {
        dm.ExecuteNonQuery("update MD_PLANNING set ISAPPROVED=3,APVBY='" + SessionManager.SessionName.StaffId + "',APVDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + PlanId);
    }
    public void GenerateBill(int PlanId, string PONo)
    {
        try
        {
            DataTable dt = new dalMediaPlanning().GetById(PlanId);
            if (dt.Rows.Count > 0)
            {
                if (CheckDuplicate(Convert.ToInt32(dt.Rows[0]["BUDGETID"]), PlanId))
                {
                    dm.ExecuteNonQuery(@"Insert into MD_BILLING(ID,BUDGETID,PLANNINGID,PAIDAMOUNT,CURRENCYID,CREATEDBY)
        VALUES(SQ_MD_BILLING.NEXTVAL,'" + dt.Rows[0]["BUDGETID"].ToString() + "','" + PlanId + "','" + dt.Rows[0]["NETPAYMENT"].ToString() + "','" + dt.Rows[0]["CURRENCYID"].ToString() + "','" + SessionManager.SessionName.StaffId + "')");
                    dm.ExecuteNonQuery("update MD_PLANNING set PONO='" + PONo + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + PlanId);
                }
            }
        }
        catch { }
    }
    public bool CheckDuplicate(int budgetid, int PlanId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_BILLING WHERE BUDGETID='" + budgetid + "' AND PLANNINGID='" + PlanId + "'");
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