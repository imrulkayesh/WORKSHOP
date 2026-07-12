using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalCaseDetails
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCaseDetails()
    {
    }
    public int Insert(string casesuitno, int casetypestepid, DateTime cdate, string ctime, int courtid, int lawyer, int statusid, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_CASEDATES(ID,CASESUITNO,CASETYPESTEPID,CDATE,CTIME,COURTID,LAWYERID,STATUSID,REMARKS,CREATEDBY)
        VALUES(SQ_LEG_CASEDATES.NEXTVAL,'" + casesuitno + "','" + casetypestepid + "',TO_DATE('" + cdate + "','MM/dd/yyyy HH:MI:SSAM'),'" + ctime + "','" + courtid + "','" + lawyer + "','" + statusid + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string casesuitno, int casetypestepid, DateTime cdate, string ctime, int courtid, int lawyer, int statusid, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_CASEDATES set CASESUITNO='" + casesuitno + "', CASETYPESTEPID='" + casetypestepid + "', CDATE=TO_DATE('" + cdate + "','MM/dd/yyyy HH:MI:SSAM'), CTIME='" + ctime + "', COURTID='" + courtid + "',LAWYERID='" + lawyer + "',STATUSID='" + statusid + "', REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_CASEDATES WHERE ID=" + id);
    }
    public DataTable GetCaseDetails(string caseno)
    {
        return dm.ExecuteQuery(@"select C.*, CT.CASETYPE, CO.COURTNAME,L.LAWYERNAME, S.STATUSNAME from LEG_CASEDATES C
       LEFT OUTER JOIN LEG_CASETYPE CT ON CT.ID = C.CASETYPESTEPID
       LEFT OUTER JOIN LEG_COURT CO ON CO.ID = C.COURTID
       LEFT OUTER JOIN LEG_LAWYER L ON L.ID = C.LAWYERID
       LEFT OUTER JOIN LEG_STATUS S ON S.ID = C.STATUSID WHERE C.CASESUITNO ='"+caseno+"'");
    }
    public DataTable GetCaseDetailsView(string suitno)
    {
        return dm.ExecuteQuery(@"select C.*, L.LAWYERNAME, CO.COURTNAME, CA.CASETYPE, P.PLACENAME,
        S.STATUSNAME, GC.COMPANYTITLE, CM.COMPANYNAME from LEG_CASE C
        LEFT OUTER JOIN LEG_LAWYER L ON L.ID = C.LAWYERID  
        LEFT OUTER JOIN LEG_COURT CO ON CO.ID = C.COURTID
        LEFT OUTER JOIN LEG_CASETYPE CA ON CA.ID = C.CASETYPEID
        LEFT OUTER JOIN LEG_PLACE P ON P.ID = C.PLACEID 
        LEFT OUTER JOIN LEG_STATUS S ON S.ID = C.STATUSID 
        LEFT OUTER JOIN BS_GROUPOFCOMPANY GC ON GC.ID = C.GCID
        LEFT OUTER JOIN BS_COMPANY CM ON CM.ID = C.COMPANYID WHERE C.CASESUITNO ='" + suitno+"'");
    }
    public bool CheckDuplicate(string casesuitno)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_CASEDATES WHERE CASESUITNO='" + casesuitno + "'");
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