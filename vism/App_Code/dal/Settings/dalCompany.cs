using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalCompany
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCompany()
    {
    }
    public int Insert(string name,int GroupofCompanyId,DateTime EstablishDate)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_COMPANY(ID,COMPANYNAME,GROUPOFCOMPANYID,COMESTABLISHDATE,CREATEDBY)
        VALUES(SQ_BS_COMPANY.NEXTVAL,'" + name + "','" + GroupofCompanyId + "',TO_DATE('" + EstablishDate + "','MM/dd/yyyy HH:MI:SSAM'),'"+SessionManager.SessionName.StaffId+"')");
    }
    public int Update(int id, string name, int GroupofCompanyId, DateTime EstablishDate)
    {
        return dm.ExecuteNonQuery("update BS_COMPANY set COMPANYNAME='" + name + "',GROUPOFCOMPANYID='" + GroupofCompanyId + "',COMESTABLISHDATE=TO_DATE('" + EstablishDate + "','MM/dd/yyyy HH:MI:SSAM'),UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_COMPANY WHERE ID=" + id);
    }
    public DataTable GetAll(int GroupofCompnayid)
    {
        return dm.ExecuteQuery("Select C.*,G.COMPANYTITLE from BS_COMPANY C INNER JOIN BS_GROUPOFCOMPANY G ON G.ID=C.GROUPOFCOMPANYID WHERE C.GROUPOFCOMPANYID=" + GroupofCompnayid);
    }
    public bool CheckDuplicate(string name, int GroupOfCompanyId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_COMPANY WHERE COMPANYNAME='" + name + "' AND GROUPOFCOMPANYID=" + GroupOfCompanyId);
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