using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalBillApprover
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalBillApprover()
    {
    }
    public int Insert(string fristapv, string finalapv, int buscenter, int company, int department)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_APPROVER(ID,FIRSTAPVSTAFFID,FINALAPVSTAFFID,BUSINESSCENTERID,COMPANYID,DEPARTMENTID,CREATEDBY)
        VALUES(SQ_BIL_APPROVER.NEXTVAL,'" + fristapv + "','" + finalapv + "','" + buscenter + "','" + company + "','" + department + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string fristapv, string finalapv, int buscenter, int company, int department)
    {
        return dm.ExecuteNonQuery("update BIL_APPROVER set FIRSTAPVSTAFFID='" + fristapv + "',FINALAPVSTAFFID='" + finalapv + "',BUSINESSCENTERID='" + buscenter + "',COMPANYID='" + company + "',DEPARTMENTID='" + department + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_APPROVER WHERE ID=" + id);
    }
    public DataTable GetAll(int buscenter, int company)
    {
        return dm.ExecuteQuery(@"Select A.*, BC.CENTERNAME, C.COMPANYNAME, D.DEPARTMENTCODE, D.DEPARTMENTNAME from BIL_APPROVER A
            INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=A.BUSINESSCENTERID
            INNER JOIN BS_COMPANY C ON C.ID = A.COMPANYID
            INNER JOIN BS_DEPARTMENT D ON D.ID = A.DEPARTMENTID WHERE BUSINESSCENTERID='" + buscenter + "' AND COMPANYID='" + company + "'");
    }
    public bool CheckDuplicate(string fristapv, string finalapv, int businesscenterid, int companyid, int departmentid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_APPROVER WHERE ISACTIVE=1 AND FIRSTAPVSTAFFID='" + fristapv + "' AND FINALAPVSTAFFID='" + finalapv + "' AND BUSINESSCENTERID='" + businesscenterid + "' AND COMPANYID='" + companyid + "' AND DEPARTMENTID='" + departmentid + "'");
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