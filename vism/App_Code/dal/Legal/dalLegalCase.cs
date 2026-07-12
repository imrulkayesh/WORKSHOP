using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLegalCase
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLegalCase()
    {
    }
    public int Insert(string casesuitno, string casesubject, string casebody, string oposition, string petitioner, string concernstaffid, int lowyerid, int courtid, int casetypeid, int Placeid, int gcid, int companyid, int statusid, string prevcasesuitno, DateTime nextdate, int ispoen, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_CASE(CASESUITNO,CASESUBJECT,CASEBODY,OPOSITION,PETITIONER,CONCERNSTAFFID,LAWYERID,COURTID,CASETYPEID,PLACEID,GCID,COMPANYID,STATUSID,PREVCASESUITNO,NEXTDATE,ISOPEN,REMARKS,CREATEDBY)
        VALUES('" + casesuitno + "','" + casesubject + "','" + casebody + "','" + oposition + "','" + petitioner + "','" + concernstaffid + "','" + lowyerid + "','" + courtid + "','" + casetypeid + "','" + Placeid + "','" + gcid + "','" + companyid + "','" + statusid + "','" + prevcasesuitno + "',TO_DATE('" + nextdate + "','MM/dd/yyyy HH:MI:SSAM'),'" + ispoen + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(string casesuitno, string casesubject, string casebody, string oposition, string petitioner, string concernstaffid, int lowyerid, int courtid, int casetypeid, int Placeid, int gcid, int companyid, int statusid, string prevcasesuitno, DateTime nextdate, int ispoen, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_CASE set CASESUITNO='" + casesuitno + "',CASESUBJECT='" + casesubject + "', CASEBODY='" + casebody + "',OPOSITION='" + oposition + "', PETITIONER='" + petitioner + "', CONCERNSTAFFID='" + concernstaffid + "', LAWYERID='" + lowyerid + "', COURTID='" + courtid + "', CASETYPEID='" + casetypeid + "', PLACEID='" + Placeid + "', GCID='" + gcid + "', COMPANYID='" + companyid + "', STATUSID='" + statusid + "', PREVCASESUITNO='" + prevcasesuitno + "',NEXTDATE=TO_DATE('" + nextdate + "','MM/dd/yyyy HH:MI:SSAM'), ISOPEN='" + ispoen + "', REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE CASESUITNO='" + casesuitno + "'");
    }
    public DataTable GetById(string id)
    {
        return dm.ExecuteQuery("Select * from LEG_CASE WHERE CASESUITNO='" + id + "'");
    }
    public DataTable GetCaseDetailsInfo(int gcid)
    {
        return dm.ExecuteQuery(@"select C.*, L.LAWYERNAME, CO.COURTNAME, CA.CASETYPE, P.PLACENAME,
        S.STATUSNAME, GC.COMPANYTITLE, CM.COMPANYNAME from LEG_CASE C
        INNER JOIN LEG_LAWYER L ON L.ID = C.LAWYERID  
        INNER JOIN LEG_COURT CO ON CO.ID = C.COURTID
        INNER JOIN LEG_CASETYPE CA ON CA.ID = C.CASETYPEID
        INNER JOIN LEG_PLACE P ON P.ID = C.PLACEID 
        INNER JOIN LEG_STATUS S ON S.ID = C.STATUSID 
        INNER JOIN BS_GROUPOFCOMPANY GC ON GC.ID = C.GCID
        INNER JOIN BS_COMPANY CM ON CM.ID = C.COMPANYID Where C.GCID='" + gcid + "'");
    }
    public bool CheckDuplicate(string casesuitno)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_CASE WHERE CASESUITNO='" + casesuitno + "'");
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