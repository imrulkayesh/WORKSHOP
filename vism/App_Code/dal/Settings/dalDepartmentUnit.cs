using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalDepartmentUnit
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDepartmentUnit()
    {
    }
    public int Insert(string name, int DepartmentId,string DeptCode)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_DEPARTMENTUNIT(ID,DEPTUNITNAME,DEPARTMENTID,DEPTUNITCODE,CREATEDBY)
        VALUES(SQ_BS_DEPARTMENTUNIT.NEXTVAL,'" + name + "','" + DepartmentId + "','" + DeptCode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, int DepartmentId, string DeptCode)
    {
        return dm.ExecuteNonQuery("update BS_DEPARTMENTUNIT set DEPTUNITNAME='" + name + "',DEPARTMENTID='" + DepartmentId + "',DEPTUNITCODE='" + DeptCode + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_DEPARTMENTUNIT WHERE ID=" + id);
    }
    public DataTable GetAll(int DepartmentId)
    {
        return dm.ExecuteQuery("Select DU.*,D.DEPARTMENTNAME from BS_DEPARTMENTUNIT DU INNER JOIN BS_DEPARTMENT D ON D.ID=DU.DEPARTMENTID WHERE DU.DEPARTMENTID=" + DepartmentId);
    }
    public bool CheckDuplicate(string name, int DepartmentId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_DEPARTMENTUNIT WHERE DEPTUNITNAME='" + name + "' AND DEPARTMENTID=" + DepartmentId);
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