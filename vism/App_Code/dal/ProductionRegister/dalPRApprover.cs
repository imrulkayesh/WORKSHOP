using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalPRApprover
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalPRApprover()
    {
    }

    public int Insert(int companyId, string staffId, int priority, int centerid, int departmentid,int lineid, int IsFinalApprover)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_APPROVER(ID,COMPANYID,APPROVERID,PRIORITY,BUSINESSCENTERID,DEPARTMENTID,LINEID,ISFINALAPPROVER,CREATEDBY)
        VALUES(SQ_PR_APPROVER.NEXTVAL,'" + companyId + "','" + staffId + "','" + priority + "','" + centerid + "'," + departmentid + ",'"+lineid+"'," + IsFinalApprover + ",'" + SessionManager.SessionName.StaffId + "')");
    }

    public int Update(int id, int companyId, string staffId, int priority, int centerid, int departmentid,int lineid,int IsFinalApprover)
    {
        return dm.ExecuteNonQuery("update PR_APPROVER set COMPANYID='" + companyId + "', APPROVERID='" + staffId + "',PRIORITY='" + priority + "',BUSINESSCENTERID='" + centerid + "',DEPARTMENTID='" + departmentid + "',LINEID='"+lineid+"' ,ISFINALAPPROVER=" + IsFinalApprover + ",UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DE.DEPARTMENTNAME,DS.DESIGNATIONNAME FROM PR_APPROVER A  INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID INNER JOIN BS_DESIGNATION DS
        ON DS.ID=E.DESIGNATIONID where A.ID=" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DE.DEPARTMENTNAME,DE.DEPARTMENTCODE,DS.DESIGNATIONNAME,ML.LINENAME FROM PR_APPROVER A  
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID 
        INNER JOIN PR_MLINE ML ON ML.ID = A.LINEID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID 
        INNER JOIN BS_DESIGNATION DS ON DS.ID=E.DESIGNATIONID where A.BUSINESSCENTERID=" + id);
    }

    public DataTable SearchStaffID(string prefixText)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DE.DEPARTMENTNAME,DE.DEPARTMENTCODE,DS.DESIGNATIONNAME,ML.LINENAME FROM PR_APPROVER A  
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID 
        INNER JOIN PR_MLINE ML ON ML.ID = A.LINEID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID 
        INNER JOIN BS_DESIGNATION DS ON DS.ID=E.DESIGNATIONID where A.APPROVERID LIKE '"+prefixText+ "%' AND ROWNUM <= 50 ");
    }
}