using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalDepartment
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDepartment()
    {
    }
    public int Insert(string name, string departmentcode)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_DEPARTMENT(ID,DEPARTMENTNAME,DEPARTMENTCODE,CREATEDBY)
        VALUES(SQ_BS_DEPARTMENT.NEXTVAL,'" + name + "','" + departmentcode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, string departmentcode)
    {
        return dm.ExecuteNonQuery("update BS_DEPARTMENT set DEPARTMENTNAME='" + name + "',DEPARTMENTCODE='" + departmentcode + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE= TO_DATE('" + DateTime.Now + "','MM/dd/yyyy HH:MI:SSAM') WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_DEPARTMENT WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_DEPARTMENT WHERE DEPARTMENTNAME='" + name + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public DataTable GetByCountryId(int countryid)
    {
        return dm.ExecuteQuery("Select * from BS_LOCATIONS where COUNTRYID=" + countryid);
    }
    public DataTable GetAssignedDepartment(int companyid, int businessCenterid)
    {
        return dm.ExecuteQuery("select * from BS_DEPARTMENTASSIGN where COMPANYID=" + companyid + " AND BUSINESSCENTERID=" + businessCenterid);
    }
    public DataTable GetByLocationId(int locationId)
    {
        return dm.ExecuteQuery("Select * from BS_BUSINESSCENTER where LOCATIONID=" + locationId);
    }
    public void DeleteAssignedDepartment(int companyid, int businessCenterid)
    {
        dm.ExecuteQuery("DELETE from BS_DEPARTMENTASSIGN where COMPANYID=" + companyid + " AND BUSINESSCENTERID=" + businessCenterid);
    }
    public int AssignDepartment(int departmentid, int businessCenterid,int companyid)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_DEPARTMENTASSIGN(ID,DEPARTMENTID,BUSINESSCENTERID,COMPANYID,CREATEDBY)
        VALUES(SQ_BS_DEPARTMENTASSIGN.NEXTVAL," + departmentid + "," + businessCenterid + "," + companyid + "," + SessionManager.SessionName.UserId + ")");
    }
    public DataTable GetByBusinessCenterId(int businessCenterid)
    {
        return dm.ExecuteQuery("SELECT DISTINCT D.ID,D.DEPARTMENTCODE FROM BS_DEPARTMENT D INNER JOIN BS_DEPARTMENTASSIGN A ON A.DEPARTMENTID=D.ID where A.BUSINESSCENTERID=" + businessCenterid);
    }
}