using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalServiceCenter
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalServiceCenter()
    {
    }
    public int Insert(string name, int businessCenterid, string centerAdmin)
    {
        return dm.ExecuteNonQuery(@"Insert into SC_SERVICECENTER(ID,SERVICECENTERNAME,BUSINESSCENTERID,CENTERADMIN,CREATEDBY)
        VALUES(SQ_SC_SERVICECENTER.NEXTVAL,'" + name + "','" + businessCenterid + "','" + centerAdmin + "','" + SessionManager.SessionName.UserId + "')");
    }
    public int Update(int id, string name, int businessCenterid, string centerAdmin)
    {
        return dm.ExecuteNonQuery("update SC_SERVICECENTER set SERVICECENTERNAME='" + name + "',BUSINESSCENTERID='" + businessCenterid + "',CENTERADMIN='" + centerAdmin + "',UPDATEDBY='" + SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select SC.*,B.CENTERNAME,D.DESIGNATIONNAME,DE.DEPARTMENTCODE,E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL from SC_SERVICECENTER SC INNER JOIN EM_EMPLOYEE E ON E.STAFFID=SC.CENTERADMIN INNER JOIN BS_DESIGNATION D ON D.ID=E.DESIGNATIONID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=E.DEPARTMENTID INNER JOIN BS_BUSINESSCENTER B ON B.ID=SC.BUSINESSCENTERID WHERE SC.ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery(@"Select SC.*,B.CENTERNAME,D.DESIGNATIONNAME,DE.DEPARTMENTCODE,E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL from SC_SERVICECENTER SC INNER JOIN EM_EMPLOYEE E ON E.STAFFID=SC.CENTERADMIN INNER JOIN BS_DESIGNATION D ON D.ID=E.DESIGNATIONID
        INNER JOIN BS_DEPARTMENT DE ON DE.ID=E.DEPARTMENTID INNER JOIN BS_BUSINESSCENTER B ON B.ID=SC.BUSINESSCENTERID");
    }
    public bool CheckDuplicate(string name, int BusinessCenter)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_SERVICECENTER WHERE SERVICECENTERNAME='" + name + "' and BUSINESSCENTERID=" + BusinessCenter);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public DataTable GetServiceCenterAdminDetails(int ServiceCenterId)
    {
        return dm.ExecuteQuery(@"SELECT E.*,B.CENTERCODE,B.CENTERNAME,B.ADDRESS,D.DEPARTMENTCODE FROM SC_SERVICECENTER S INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.CENTERADMIN INNER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID
INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID WHERE S.ID=" + ServiceCenterId);
    }
    public DataTable GetByBusinessCenerId(int BusinessCenter)
    {
        return dm.ExecuteQuery(@"Select * from SC_SERVICECENTER where BUSINESSCENTERID=" + BusinessCenter);
    }
}