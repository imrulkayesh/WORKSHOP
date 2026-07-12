using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalCaseDateFile
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCaseDateFile()
    {
    }
    public int Insert(string casesuitno, string doucType, string casedate, string filename, string filelocation, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_CASEDATEFILES(ID,CASESUITNO,DOCUMENTTYPE,CASEDATE,FILENAME,FILELOCATION,REMARKS,CREATEDBY)
        VALUES(SQ_LEG_CASEDATEFILES.NEXTVAL,'" + casesuitno + "','" + doucType + "',TO_DATE('" + casedate + "','DD/MM/YYYY'),'" + filename + "','" + filelocation + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string casesuitno, string doucType, string casedate, string filename, string filelocation, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_CASEDATEFILES set CASESUITNO='" + casesuitno + "',DOCUMENTTYPE='" + doucType + "',CASEDATE=TO_DATE('" + casedate + "','DD/MM/YYYY'), FILENAME='" + filename + "', FILELOCATION='" + filelocation + "', REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllDeteFileByCase(string casesuit)
    {
        return dm.ExecuteQuery("Select * from LEG_CASEDATEFILES WHERE CASESUITNO='" + casesuit + "'");
    }
    public DataTable GetViewAll(string casesuit)
    {
        return dm.ExecuteQuery(@"select C.*,CA.CASESUBJECT, CA.CASEBODY, CA.OPOSITION, CA.PETITIONER,  CT.CASETYPE,
 CO.COURTNAME,L.LAWYERNAME, S.STATUSNAME from LEG_CASEDATES C
       LEFT OUTER JOIN LEG_CASETYPE CT ON CT.ID = C.CASETYPESTEPID
       LEFT OUTER JOIN LEG_COURT CO ON CO.ID = C.COURTID
       LEFT OUTER JOIN LEG_LAWYER L ON L.ID = C.LAWYERID
       LEFT OUTER JOIN LEG_STATUS S ON S.ID = C.STATUSID
       LEFT OUTER JOIN LEG_CASE CA ON CA.CASESUITNO = C.CASESUITNO WHERE C.CASESUITNO='" + casesuit + "'");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_CASEDATEFILES WHERE ID=" + id);
    }
    public bool CheckDuplicate(string casesuitno)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_CASEDATEFILES WHERE CASESUITNO='" + casesuitno + "'");
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
