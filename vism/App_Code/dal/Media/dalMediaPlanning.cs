using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMediaPlanning
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMediaPlanning()
    {
    }
    public int Insert(int budgetid, int mediacatid, int media, string caption, int month, DateTime fdate, DateTime tdate, int colorid, int adpositionid, decimal sizecol, decimal sizeinch, decimal totalsize, decimal duration, decimal mediarate, decimal commission, decimal totalamount, decimal totalcommission, decimal totalpayment, decimal aitrate, decimal aitamount, decimal netpayment, string designsource, string attachment)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_PLANNING(ID,BUDGETID,MEDIACATID,MEDIAID,CAPTION,MONTHS,FDATE,TDATE,COLORID,ADPOSITIONID,SIZECOL,SIZEINCH,TOTALSIZE,DURATION,MEDIARATE,COMMISSION,TOTALAMOUNT,TOTALCOMMISION,TOTALPAYMENT,AITRATE,AITAMOUNT,NETPAYMENT,DESIGNSOURCE,ATTACHMENT,CREATEDBY)
        VALUES(SQ_MD_PLANNING.NEXTVAL,'" + budgetid + "','" + mediacatid + "','" + media + "','" + caption + "','" + month + "',TO_DATE('" + fdate + "','MM/dd/yyyy HH:MI:SSAM'),TO_DATE('" + tdate + "','MM/dd/yyyy HH:MI:SSAM'),'" + colorid + "','" + adpositionid + "','" + sizecol + "','" + sizeinch + "','" + totalsize + "','" + duration + "','" + mediarate + "','" + commission + "','" + totalamount + "','" + totalcommission + "','" + totalpayment + "','" + aitrate + "','" + aitamount + "','" + netpayment + "','" + designsource + "','" + attachment + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int budgetid, int mediacatid, int media, string caption, int month, DateTime fdate, DateTime tdate, int colorid, int adpositionid, decimal sizecol, decimal sizeinch, decimal totalsize, decimal duration, decimal mediarate, decimal commission, decimal totalamount, decimal totalcommission, decimal totalpayment, decimal aitrate, decimal aitamount, decimal netpayment, string designsource, string attachment)
    {
        return dm.ExecuteNonQuery("update MD_PLANNING set BUDGETID='" + budgetid + "',MEDIACATID='" + mediacatid + "',MEDIAID='" + media + "',CAPTION='" + caption + "',MONTHS='" + month + "',FDATE=TO_DATE('" + fdate + "','MM/dd/yyyy HH:MI:SSAM'),TDATE=TO_DATE('" + tdate + "','MM/dd/yyyy HH:MI:SSAM'),COLORID='" + colorid + "',ADPOSITIONID='" + adpositionid + "',SIZECOL='" + sizecol + "',SIZEINCH='" + sizeinch + "',TOTALSIZE='" + totalsize + "',DURATION='" + duration + "',MEDIARATE='" + mediarate + "',COMMISSION='" + commission + "',TOTALAMOUNT='" + totalamount + "',TOTALCOMMISION='" + totalcommission + "',TOTALPAYMENT='" + totalpayment + "',AITRATE='" + aitrate + "',AITAMOUNT='" + aitamount + "',NETPAYMENT='" + netpayment + "',DESIGNSOURCE='" + designsource + "',ATTACHMENT='" + attachment + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllBudget(string budgetId)
    {
        return dm.ExecuteQuery(@"Select P.*, MC.CATNAME, M.MEDIANAME,AP.ADPOSITION,C.COLORNAME from MD_PLANNING P 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = P.MEDIACATID
        INNER JOIN MD_MEDIA M ON M.ID = P.MEDIAID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = P.ADPOSITIONID
        INNER JOIN MD_COLOR C ON C.ID = P.COLORID WHERE P.STATUS= 0 AND P.BUDGETID=" + budgetId);
    }
    public DataTable GetBudget(int id)
    {
        return dm.ExecuteQuery(@"Select P.*, MC.CATNAME, M.MEDIANAME,AP.ADPOSITION,C.COLORNAME from MD_PLANNING P 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = P.MEDIACATID
        INNER JOIN MD_MEDIA M ON M.ID = P.MEDIAID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = P.ADPOSITIONID
        INNER JOIN MD_COLOR C ON C.ID = P.COLORID WHERE P.BUDGETID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_PLANNING WHERE ID=" + id);
    }
    public bool CheckDuplicate(int budgetid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_PLANNING WHERE BUDGETID='" + budgetid + "' ");
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
