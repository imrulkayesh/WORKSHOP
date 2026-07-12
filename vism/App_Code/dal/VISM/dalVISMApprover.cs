using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalVISMApprover
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVISMApprover()
    {
    }
    public int Insert(int companyId, string staffId, int priority, int centerid, int departmentid)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_APPROVER(ID,COMPANYID,APPROVERID,PRIORITY,BUSINESSCENTERID,DEPARTMENTID,CREATEDBY)
        VALUES(SQ_RVS_APPROVER.NEXTVAL,'" + companyId + "','" + staffId + "','" + priority + "','" + centerid + "','" + departmentid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int companyId, string staffId, int priority, int centerid, int departmentid)
    {
        return dm.ExecuteNonQuery("update RVS_APPROVER set COMPANYID='" + companyId + "', APPROVERID='" + staffId + "',PRIORITY='" + priority + "',BUSINESSCENTERID='" + centerid + "',DEPARTMENTID=" + departmentid + ",UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE where ID=" + id);
    }
    public void BRTAApproval(int iD, int Status)
    {
        dm.ExecuteNonQuery(@"Update RVS_VEHICLE set ISAPPROVED='" + Status + "',APPROVERID='" + SessionManager.SessionName.StaffId + "',APPROVEDATE=SYSDATE where ID=" + iD);
    }

    public DataTable GetBRTAList(int Status)
    {
        return dm.ExecuteQuery(@"Select V.*,B.BRANDNAME,M.MODELNAME,MNF.MANUFACTURERNAME,S.SELLDATE from RVS_VEHICLE V INNER JOIN RVS_BRAND B ON B.ID=V.BRANDID INNER JOIN RVS_MODEL M ON 
M.ID=V.MODELID 
left JOIN RVS_SALES S ON S.VEHICLEID=V.ID left JOIN RVS_MANUFACTURER MNF ON MNF.ID=V.MANUFACTURERID left JOIN RVS_APPROVER A
ON A.BUSINESSCENTERID=V.BUSINESSCENTERID");
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DE.DEPARTMENTNAME,DS.DESIGNATIONNAME FROM RVS_APPROVER A  INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID INNER JOIN BS_DESIGNATION DS
ON DS.ID=E.DESIGNATIONID where A.ID=" + id);
    }
    public DataTable GetAll(int businessCenterId)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,CO.COMPANYNAME,DE.DEPARTMENTNAME,DE.DEPARTMENTCODE,DS.DESIGNATIONNAME FROM RVS_APPROVER A  
INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID 
INNER JOIN BS_COMPANY CO ON A.COMPANYID=CO.ID 
INNER JOIN EM_EMPLOYEE E ON E.USERID =A.APPROVERID 
INNER JOIN BS_DESIGNATION DS ON DS.ID=E.DESIGNATIONID 
where A.BUSINESSCENTERID=" + businessCenterId);
    }
}