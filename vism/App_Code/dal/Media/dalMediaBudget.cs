using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
public class dalMediaBudget
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMediaBudget()
    {
    }
    public int Insert(int gcid, int compid, int brandid, decimal amount, int monthid, int currencyid)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_BUDGET(ID,GCID,COMPANYID,BRANDID,AMOUNT,MONTHS,CURRENCYID,CREATEDBY)
        VALUES(SQ_MD_BUDGET.NEXTVAL,'" + gcid + "','" + compid + "','" + brandid + "','" + amount + "','" + monthid + "','" + currencyid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int gcid, int compid, int brandid, decimal amount, int monthid, int currencyid)
    {
        return dm.ExecuteNonQuery("update MD_BUDGET set GCID='" + gcid + "',COMPANYID='" + compid + "',BRANDID='" + brandid + "',AMOUNT='" + amount + "',MONTHS='" + monthid + "',CURRENCYID='" + currencyid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllBudget(int gcid,int companyid)
    {
        return dm.ExecuteQuery(@"Select B.*, GC.COMPANYTITLE, C.COMPANYNAME, BR.BRANDNAME, CU.CURRENCYNAME from MD_BUDGET B
        INNER JOIN BS_GROUPOFCOMPANY GC ON GC.ID = B.GCID
        INNER JOIN BS_COMPANY C ON C.ID = B.COMPANYID
        INNER JOIN MD_BRAND BR ON BR.ID = B.BRANDID
        INNER JOIN ASSET_CURRENCY CU ON CU.ID = B.CURRENCYID WHERE B.GCID='" + gcid + "' AND B.COMPANYID='" + companyid + "'");
    }
    public DataTable GetAll(int gcid)
    {
        return dm.ExecuteQuery(@"Select B.*, GC.COMPANYTITLE, C.COMPANYNAME from MD_BUDGET B
        INNER JOIN BS_GROUPOFCOMPANY GC ON GC.ID = B.GCID
        INNER JOIN BS_COMPANY C ON C.ID = B.COMPANYID WHERE B.GCID='" + gcid + "'");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_BUDGET B INNER JOIN ASSET_CURRENCY C ON C.ID=B.CURRENCYID WHERE B.ID=" + id);
    }
    public bool CheckDuplicate(int gcid, int compid, int brandid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_BUDGET WHERE GCID='" + gcid + "' AND COMPANYID='"+compid+"' AND BRANDID='"+brandid+"' ");
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
