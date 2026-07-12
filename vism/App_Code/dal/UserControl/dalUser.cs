using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalUser
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalUser()
    {
    }

    public int CreateUsers(string userName, string staffId, int roleId, string password, int Departmentid)
    {
        return dm.ExecuteNonQuery(@"Insert into UC_USERS(ID,USERNAME,PASSWORD,ROLEID,DEPARTMENTID,CREATEDBY)
        VALUES('" + staffId + "','" + userName + "','" + Encryption.EncryptionWithSh(password) + "'," + roleId + ",'" + Departmentid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int UpdateUsers(string staffId, int roleId, string password, int Departmentid)
    {
        return dm.ExecuteNonQuery("update UC_USERS set PASSWORD='"+ password + "', ROLEID='" + roleId + "',DEPARTMENTID='" + Departmentid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= SYSDATE WHERE ID='" + staffId+"'");
    }
    public int Insert(string staffId, int designationid, string name, string mobile, string email, int centerid, int companyid, int departmentid)
    {
        return dm.ExecuteNonQuery(@"Insert into EM_EMPLOYEE(ID,STAFFID,USERID,DESIGNATIONID,EMPLOYEENAME,OFFICEMOBILE,OFFICEMAIL,BUSINESSCENTERID,COMPANYID,DEPARTMENTID,CREATEDBY)
        VALUES(SQ_EM_EMPLOYEE.NEXTVAL,'" + staffId + "','" + staffId + "','" + designationid + "','" + name + "','" + mobile + "','" + email + "','" + centerid + "','" + companyid + "'," + departmentid + ",'" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(string StaffId, int designationid, string name, string mobile, string email, int centerid, int companyid, int departmentid)
    {
        return dm.ExecuteNonQuery("update EM_EMPLOYEE set DESIGNATIONID='" + designationid + "',EMPLOYEENAME='" + name + "',OFFICEMOBILE='" + mobile + "',OFFICEMAIL='" + email + "',BUSINESSCENTERID='" + centerid + "',COMPANYID='" + companyid + "',DEPARTMENTID=" + departmentid + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= SYSDATE where STAFFID='" + StaffId+"'");
    }
    public DataTable GetRolesForUserSetup()
    {
        DataTable dt = new DataTable();
        if (SessionManager.SessionName.RoleId == 1)
        {
            dt = dm.ExecuteQuery("SELECT * FROM UC_ROLESETUP");
        }
        else
        {
            dt = dm.ExecuteQuery("SELECT * FROM UC_ROLESETUP WHERE ID>2");
        }
        return dt;
    }
    public DataTable GetUsers(int BusinessCenterId)
    {
        return dm.ExecuteQuery(@"SELECT U.*,E.*,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,D.DEPARTMENTNAME,C.COMPANYNAME,DE.DESIGNATIONNAME, R.ROLENAME FROM UC_USERS U 
INNER JOIN EM_EMPLOYEE E ON E.USERID=U.ID INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID
INNER JOIN BS_COMPANY C ON C.ID=E.COMPANYID INNER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN UC_ROLESETUP R ON R.ID=U.ROLEID WHERE E.BUSINESSCENTERID=" + BusinessCenterId + "");
    }
    public DataTable GetUsersSearch(string prefixText)
    {
        return dm.ExecuteQuery(@"SELECT U.*,E.*,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,D.DEPARTMENTNAME,C.COMPANYNAME,DE.DESIGNATIONNAME, R.ROLENAME FROM UC_USERS U 
        INNER JOIN EM_EMPLOYEE E ON E.USERID=U.ID 
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID
        INNER JOIN BS_COMPANY C ON C.ID=E.COMPANYID 
        INNER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID
        INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID 
        INNER JOIN UC_ROLESETUP R ON R.ID=U.ROLEID WHERE E.USERID = '" + prefixText + "'");
    }
    public DataTable GetUserById(string id)
    {
        return dm.ExecuteQuery(@"SELECT U.*,E.*,E.DEPARTMENTID DEPTID,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,D.DEPARTMENTNAME,C.COMPANYNAME,DE.DESIGNATIONNAME, R.ROLENAME FROM UC_USERS U 
INNER JOIN EM_EMPLOYEE E ON E.USERID=U.ID INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID
INNER JOIN BS_COMPANY C ON C.ID=E.COMPANYID INNER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN UC_ROLESETUP R ON R.ID=U.ROLEID WHERE U.ID='" + id+"'");
    }

    public DataTable GetLoginInfo(string userName, string password)
    {
        return dm.ExecuteQuery("Select * from UC_USERS where USERNAME='" + userName + "' AND PASSWORD='" + password + "'");
    }
    public DataTable GetLoginInfoByUserID(string userName)
    {
        return dm.ExecuteQuery("Select * from UC_USERS where ISACTIVE=1 AND USERNAME='" + userName + "'");
    }
    public DataTable GetEmployeeInfo(string userName)
    {
        return dm.ExecuteQuery("Select * from EM_EMPLOYEE where USERID='" + userName + "'");
    }
    public int ChangePassword(string userName, string password)
    {
        return dm.ExecuteNonQuery("UPDATE UC_USERS set PASSWORD='" + password + "' where USERNAME='" + userName + "'");
    }

    public bool CheckDuplicate(string staffId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from UC_USERS WHERE ID='" + staffId + "'");
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