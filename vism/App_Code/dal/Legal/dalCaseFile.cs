using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalCaseFile
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCaseFile()
    {
    }
    public int Insert(string casesuitno, string ducType, string filename, DateTime uploadDate, string filelocation, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_CASEFILES(ID,CASESUITNO,DOCUMENTTYPE,FILENAME,DOCUMENTDATE,FILELOCATION,REMARKS,CREATEDBY)
        VALUES(SQ_LEG_CASEFILES.NEXTVAL,'" + casesuitno + "','" + ducType + "','" + filename + "',TO_DATE('" + uploadDate + "','MM/dd/yyyy HH:MI:SSAM'),'" + filelocation + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string casesuitno, string ducType, string filename, DateTime uploadDate, string filelocation, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_CASEFILES set CASESUITNO='" + casesuitno + "',DOCUMENTTYPE='" + ducType + "', FILENAME='" + filename + "', DOCUMENTDATE=TO_DATE('" + uploadDate + "','MM/dd/yyyy HH:MI:SSAM'),FILELOCATION='" + filelocation + "', REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllCaseFile(string casesuit)
    {
        return dm.ExecuteQuery("Select * from LEG_CASEFILES WHERE CASESUITNO='" + casesuit + "'");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_CASEFILES WHERE ID=" + id);
    }
    public bool CheckDuplicate(string casesuitno)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_CASEFILES WHERE CASESUITNO='" + casesuitno + "'");
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
