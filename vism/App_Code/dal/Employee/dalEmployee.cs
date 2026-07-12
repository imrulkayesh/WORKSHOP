using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalEmployee
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalEmployee()
    {
    }
    public int Insert(string staffId, int designationid, string name, string mobile, string email, int centerid, int companyid, int departmentid)
    {
        return dm.ExecuteNonQuery(@"Insert into EM_EMPLOYEE(ID,STAFFID,USERID,DESIGNATIONID,EMPLOYEENAME,OFFICEMOBILE,OFFICEMAIL,BUSINESSCENTERID,COMPANYID,DEPARTMENTID,CREATEDBY)
        VALUES(SQ_EM_EMPLOYEE.NEXTVAL,'" + staffId + "','" + staffId + "','" + designationid + "','" + name + "','" + mobile + "','" + email + "','" + centerid + "','" + companyid + "'," + departmentid + ",'" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(string staffId, int designationid, string name, string mobile, string email, int centerid, int companyid, int departmentid)
    {
        return
            dm.ExecuteNonQuery(@"update EM_EMPLOYEE set ID=SQ_EM_EMPLOYEE.NEXTVAL,STAFFID='" + staffId + "',USERID='" +
                               staffId + "',DESIGNATIONID='" + designationid + "',EMPLOYEENAME='" + name +
                               "',OFFICEMOBILE='" + mobile + "',OFFICEMAIL='" + email + "',BUSINESSCENTERID='" +
                               centerid + "',COMPANYID='" + companyid + "',DEPARTMENTID='" + departmentid +
                               "',CREATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDBY='"+ SessionManager.SessionName.StaffId + "',UPDATEDATE='SYSDATE' where STAFFID='" + staffId +
                               "'");


    }
    public int InsertUser(string staffId, int departmentid, int RoleId)
    {
        return dm.ExecuteNonQuery(@"Insert into UC_USERS(ID,USERNAME,PASSWORD,ROLEID,DEPARTMENTID,CREATEDBY)
        VALUES('" + staffId + "','" + staffId + "','" + Encryption.EncryptionWithSh(staffId) + "','" + RoleId + "','" + departmentid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int designationid, string name, string mobile, string email, int centerid, int companyid, int departmentid)
    {
        return dm.ExecuteNonQuery("update EM_EMPLOYEE set DESIGNATIONID='" + designationid + "',EMPLOYEENAME='" + name + "',OFFICEMOBILE='" + mobile + "',OFFICEMAIL='" + email + "',BUSINESSCENTERID='" + centerid + "',COMPANYID='" + companyid + "',DEPARTMENTID=" + departmentid + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= TO_DATE('" + DateTime.Now + "','MM/dd/yyyy HH:MI:SSAM') where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select E.*,B.CENTERNAME,L.NAME,C.COUNTRYNAME,L.COUNTRYID from EM_EMPLOYEE E inner join BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID INNER JOIN BS_LOCATIONS L ON L.ID=B.LOCATIONID
INNER JOIN BS_COUNTRY C ON  C.ID=L.COUNTRYID where E.ID=" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery("Select I.*,C.PARENTID from EM_EMPLOYEE I inner join ASSET_CATEGORY C ON C.ID=I.CATEGORYID where I.ID=" + id);
    }

    public DataTable GetEmployeeByDepartment(int BusinessCenterId, int DepartmentId)
    {
        return dm.ExecuteQuery("Select * from EM_EMPLOYEE where BUSINESSCENTERID=" + BusinessCenterId + " and DEPARTMENTID=" + DepartmentId);
    }
}