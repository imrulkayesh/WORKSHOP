using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalGroupofCompany
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalGroupofCompany()
    {
    }
    public int Insert(string name,DateTime Establishdate)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_GROUPOFCOMPANY(ID,COMPANYTITLE,ESTABLISHDATE,CREATEDBY)
        VALUES(SQ_BS_GROUPOFCOMPANY.NEXTVAL,'" + name + "',TO_DATE('" + Establishdate + "','MM/dd/yyyy HH:MI:SSAM'),'"+SessionManager.SessionName.StaffId+"')");
    }
    public int Update(int id, string name, DateTime Establishdate)
    {
        return dm.ExecuteNonQuery("update BS_GROUPOFCOMPANY set COMPANYTITLE='" + name + "',ESTABLISHDATE=TO_DATE('" + Establishdate + "','MM/dd/yyyy HH:MI:SSAM'), UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_GROUPOFCOMPANY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_GROUPOFCOMPANY WHERE COMPANYTITLE='" + name + "'");
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