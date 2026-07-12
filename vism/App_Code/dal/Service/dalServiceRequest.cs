using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalServiceRequest
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalServiceRequest()
    {
    }
    public int Update(long id, int ServiceCenterId, string Staffid, int Categoryid, int MachineId)
    {
        string office = "";
        if (SessionManager.SessionName.BusinessCentetid < 100)
        {
            office = "00" + SessionManager.SessionName.BusinessCentetid.ToString();
        }
        else if (SessionManager.SessionName.BusinessCentetid < 10)
        {
            office = "0" + SessionManager.SessionName.BusinessCentetid.ToString();
        }
        else
        {
            office = SessionManager.SessionName.BusinessCentetid.ToString();
        }
       
        string TransID = "SC" + office + DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("d2");
        string TransIdQuery = "(Select '" + TransID + "'||TRIM(TO_CHAR((NVL(MAX(TO_NUMBER(SUBSTR(SERVICEID,10,6))),0)+1),'000000')) ID from SC_SERVICEREQUEST where TRIM(TO_CHAR(NVL(SUBSTR(SERVICEID,3,3),0),'000'))='" + office + "' and TRIM(TO_CHAR(NVL(SUBSTR(SERVICEID,6,2),0),'00'))='" + DateTime.Now.ToString("yy") + "'  and TRIM(TO_CHAR(NVL(SUBSTR(SERVICEID,8,2),0),'00'))='" + DateTime.Now.Month.ToString("d2") + "')";
        return dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set SERVICECENTERID=" + ServiceCenterId + ",CATEGORYID=" + Categoryid + ",FROMSTAFFID='" + Staffid + "',FROMCENTERID='" + SessionManager.SessionName.BusinessCentetid + "',FROMDEPARTMENTID='" + SessionManager.SessionName.DepartmentId + "',STATUS=0,ISOPEN=0,CREATEDATE=SYSDATE,MACHINEID='" + MachineId + "', SERVICEID=" + TransIdQuery + " where ID=" + id);

    }
    public int UpdateInstantService(long id, int ServiceCenterId, string FromStaffid, int Categoryid, int FromDepartmentId, int fromCenterId, int MachineId)
    {
        string office = "";
        if (SessionManager.SessionName.BusinessCentetid < 100)
        {
            office = "00" + SessionManager.SessionName.BusinessCentetid.ToString();
        }
        else if (SessionManager.SessionName.BusinessCentetid < 10)
        {
            office = "0" + SessionManager.SessionName.BusinessCentetid.ToString();
        }
        else
        {
            office = SessionManager.SessionName.BusinessCentetid.ToString();
        }

        string TransID = "SC" + office + DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("d2");
        string TransIdQuery = "(Select '" + TransID + "'||TRIM(TO_CHAR((NVL(MAX(TO_NUMBER(SUBSTR(SERVICEID,10,6))),0)+1),'000000')) ID from SC_SERVICEREQUEST where TRIM(TO_CHAR(NVL(SUBSTR(SERVICEID,3,3),0),'000'))='" + office + "' and TRIM(TO_CHAR(NVL(SUBSTR(SERVICEID,6,2),0),'00'))='" + DateTime.Now.ToString("yy") + "'  and TRIM(TO_CHAR(NVL(SUBSTR(SERVICEID,8,2),0),'00'))='" + DateTime.Now.Month.ToString("d2") + "')";
        return dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set SERVICECENTERID=" + ServiceCenterId + ",CATEGORYID=" + Categoryid + ",FROMSTAFFID='" + FromStaffid + "',FROMCENTERID='" + SessionManager.SessionName.BusinessCentetid + "',FROMDEPARTMENTID='" + SessionManager.SessionName.DepartmentId + "',STATUS=100,MACHINEID='" + MachineId + "',INSTANTSERVICE=1,ISOPEN=0,CREATEDATE=SYSDATE, SERVICEID=" + TransIdQuery + " where ID=" + id);

    }

    public DataTable GetMachineList()
    {
        return dm.ExecuteQuery(@"SELECT M.*,M.MACHINECODE||'-'||M.MACHINENAME MACHINE FROM SC_MACHINE M 
        WHERE M.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "' AND M.COMPANYID='" + SessionManager.SessionName.CompanyId + "'");
    }

    public long CheckRequest(string UserId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_SERVICEREQUEST where ISOPEN=1 AND CREATEDBY='" + UserId + "'");
        if (dt.Rows.Count == 0)
        {
            dm.ExecuteNonQuery(@"Insert into SC_SERVICEREQUEST(ID,ISOPEN,CREATEDBY) VALUES(SQ_SC_SERVICEREQUEST.NEXTVAL,1,'" + SessionManager.SessionName.StaffId + "')");
            dt = dm.ExecuteQuery("Select * from SC_SERVICEREQUEST where ISOPEN=1 AND CREATEDBY='" + UserId + "'");
        }
        return Convert.ToInt64(dt.Rows[0]["ID"]);
    }

    public DataTable GetServiceList(long RequestId)
    {
        return dm.ExecuteQuery("Select * from SC_SERVICEDETAILS where REQUESTID=" + RequestId);
    }

    public void UpdateService(long Id, decimal quantity, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update SC_SERVICEDETAILS SET QUANTITY='" + quantity + "',REMARKS='" + Remarks + "' Where ID=" + Id);
    }

    public void InsertServiceDetails(long RequestId, string Subject, string AttachFile, decimal quantity, string Remarks)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_SERVICEDETAILS where REQUESTID=" + RequestId + " and SUBJECT='" + Subject + "'");
        if (dt.Rows.Count > 0)
        {
            dm.ExecuteNonQuery("UPDATE SC_SERVICEDETAILS SET QUANTITY=QUANTITY+" + quantity + " WHERE ID=" + dt.Rows[0]["ID"]);
        }
        else
        {
            dm.ExecuteNonQuery("INSERT INTO SC_SERVICEDETAILS(ID,QUANTITY,SUBJECT,REQUESTID,ATTACHMENT,REMARKS) Values(SQ_SC_SERVICEDETAILS.NEXTVAL," + quantity + ",'" + Subject + "','" + RequestId + "','" + AttachFile + "','" + Remarks + "')");
        }
    }
    public DataTable GetApprovalList(int Status)
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERCODE SERVICECENTERLOCATION,E.OFFICEMAIL,E.OFFICEMOBILE FROM SC_SERVICEREQUEST S 
        INNER JOIN BS_BUSINESSCENTER B  ON B.ID=S.FROMCENTERID 
        INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID 
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
        INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID 
        INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID 
        INNER JOIN BS_BUSINESSCENTER BSC ON BSC.ID=SC.BUSINESSCENTERID 
        INNER JOIN DP_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID where A.DEPARTMENTID=E.DEPARTMENTID AND S.STATUS=0 AND S.ISOPEN=0 AND S.ISAPPROVED=" + Status + " AND A.APPROVERID=" + SessionManager.SessionName.StaffId);
    }
    public DataTable GetByAdminRequest(int Status)
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERNAME SERVICECENTERLOCATION,
    E.OFFICEMAIL,E.OFFICEMOBILE,C.CATEGORYNAME FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
    ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
    INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID INNER JOIN BS_BUSINESSCENTER BSC
    ON BSC.ID=SC.BUSINESSCENTERID INNER JOIN SC_CATEGORY C ON C.ID=S.CATEGORYID
    where S.STATUS=0 AND S.ISOPEN=0 AND S.ISAPPROVED=1 AND S.FRDSERVICECENTERID=0 AND SC.CENTERADMIN='" + SessionManager.SessionName.StaffId + "'" +
          @" UNION
    SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERNAME SERVICECENTERLOCATION,
    E.OFFICEMAIL,E.OFFICEMOBILE,C.CATEGORYNAME FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
    ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
    INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.FRDSERVICECENTERID INNER JOIN BS_BUSINESSCENTER BSC
    ON BSC.ID=SC.BUSINESSCENTERID INNER JOIN SC_CATEGORY C ON C.ID=S.CATEGORYID
    where S.STATUS=0 AND S.ISOPEN=0 AND S.ISAPPROVED=1 AND S.FRDSERVICECENTERID>0 AND SC.CENTERADMIN='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable GetByAdminAssignedRequest(int Status)
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERCODE SERVICECENTERLOCATION,
E.OFFICEMAIL,E.OFFICEMOBILE,SAE.EMPLOYEENAME SERVICEMAN,SAE.OFFICEMOBILE SERVICEMOBILE,SAE.OFFICEMAIL SERVICEEMAIL,ST.TEAMNAME
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID  INNER JOIN SC_SERVICEASSIGN SA ON SA.REQUESTID=S.ID INNER JOIN EM_EMPLOYEE SAE ON SAE.STAFFID=SA.TEAMMEMBERID
INNER JOIN SC_SERVICETEAM ST ON ST.ID=SA.TEAMID INNER JOIN BS_BUSINESSCENTER BSC
ON BSC.ID=ST.BUSINESSCENTERID where S.STATUS IN(1,2,3,4) AND SA.ISACTIVE=1  AND S.FRDSERVICECENTERID=0 AND S.ISAPPROVED=1 AND S.ISOPEN=0 AND SC.CENTERADMIN='" + SessionManager.SessionName.StaffId + "'" +
     @" UNION 
SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERCODE SERVICECENTERLOCATION,
E.OFFICEMAIL,E.OFFICEMOBILE,SAE.EMPLOYEENAME SERVICEMAN,SAE.OFFICEMOBILE SERVICEMOBILE,SAE.OFFICEMAIL SERVICEEMAIL,ST.TEAMNAME
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.FRDSERVICECENTERID  INNER JOIN SC_SERVICEASSIGN SA ON SA.REQUESTID=S.ID INNER JOIN EM_EMPLOYEE SAE ON SAE.STAFFID=SA.TEAMMEMBERID
INNER JOIN SC_SERVICETEAM ST ON ST.ID=SA.TEAMID INNER JOIN BS_BUSINESSCENTER BSC
ON BSC.ID=ST.BUSINESSCENTERID where S.STATUS IN(1,2,3,4) AND SA.ISACTIVE=1 AND S.ISAPPROVED=1 AND S.FRDSERVICECENTERID>0 AND S.ISOPEN=0 AND SC.CENTERADMIN='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable GetInstantServicestoApprove()
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERCODE SERVICECENTERLOCATION,
E.OFFICEMAIL,E.OFFICEMOBILE,SAE.EMPLOYEENAME SERVICEMAN,SAE.OFFICEMOBILE SERVICEMOBILE,SAE.OFFICEMAIL SERVICEEMAIL,ST.TEAMNAME
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID  INNER JOIN SC_SERVICEASSIGN SA ON SA.REQUESTID=S.ID INNER JOIN EM_EMPLOYEE SAE ON SAE.STAFFID=SA.TEAMMEMBERID
INNER JOIN SC_SERVICETEAM ST ON ST.ID=SA.TEAMID INNER JOIN BS_BUSINESSCENTER BSC
ON BSC.ID=ST.BUSINESSCENTERID where S.STATUS=100 AND SA.ISACTIVE=1  AND S.FRDSERVICECENTERID=0 AND S.ISAPPROVED=0 AND S.ISOPEN=0 AND SC.CENTERADMIN='" + SessionManager.SessionName.StaffId + "'");
    }
    public void AssignService(int TeamId, string TeamMemberId, long RequestId, string StartDate, string StartTime, string EndDate, string EndTime)
    {
        UpdateServiceAssignStatus(RequestId);
        dm.ExecuteNonQuery(@"INSERT INTO SC_SERVICEASSIGN(ID,TEAMID,TEAMMEMBERID,REQUESTID,ASSIGNDATE,STARTDATE,STARTTIME,ENDDATE,ENDTIME) 
        Values(SQ_SC_SERVICEASSIGN.NEXTVAL," + TeamId + ",'" + TeamMemberId + "','" + RequestId + "',SYSDATE,TO_DATE('" + StartDate + "','DD/MM/YYYY'),'" + StartTime + "',TO_DATE('" + EndDate + "','DD/MM/YYYY'),'" + EndTime + "')");
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set STATUS=1 where ID=" + RequestId);
    }
    public void AssignInstantService(int TeamId, string TeamMemberId, long RequestId, DateTime StartDate, string StartTime, DateTime EndDate, string EndTime)
    {
        UpdateServiceAssignStatus(RequestId);
        dm.ExecuteNonQuery(@"INSERT INTO SC_SERVICEASSIGN(ID,TEAMID,TEAMMEMBERID,REQUESTID,ASSIGNDATE,STARTDATE,STARTTIME,ENDDATE,ENDTIME) 
        Values(SQ_SC_SERVICEASSIGN.NEXTVAL," + TeamId + ",'" + TeamMemberId + "','" + RequestId + "',SYSDATE,TO_DATE('" + StartDate + "','MM/dd/yyyy HH:MI:SSAM'),'" + StartTime + "',TO_DATE('" + EndDate + "','MM/dd/yyyy HH:MI:SSAM'),'" + EndTime + "')");
    }
    public void ApproveInstantService(long ID)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set STATUS=2, APPROVERID='" + SessionManager.SessionName.StaffId + "',ISAPPROVED=1,APPROVEDATE=SYSDATE where ID=" + ID);
    }
    public void RejectInstantService(long ID)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set APPROVERID='" + SessionManager.SessionName.StaffId + "',ISAPPROVED=2,APPROVEDATE=SYSDATE where ID=" + ID);
    }
    public void UpdateServiceAssignStatus(long RequestId)
    {
        dm.ExecuteQuery("UPDATE SC_SERVICEASSIGN SET ISACTIVE=0 WHERE REQUESTID=" + RequestId);
    }
    public DataTable GetServiceCenter()
    {
        return dm.ExecuteQuery("SELECT * FROM SC_SERVICECENTER WHERE CENTERADMIN='" + SessionManager.SessionName.StaffId + "'");
    }
    public void ApproveRequest(long RequestId)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set ISAPPROVED=1,APPROVERID='" + SessionManager.SessionName.StaffId + "',APPROVEDATE=SYSDATE where ID=" + RequestId);
    }
    public void RejectRequest(long RequestId)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set ISAPPROVED=2,APPROVERID='" + SessionManager.SessionName.StaffId + "',APPROVEDATE=SYSDATE where ID=" + RequestId);
    }
    public DataTable GetRequestDetails(long RequestId)
    {
        return dm.ExecuteQuery(@"SELECT SR.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,D.DEPARTMENTCODE,D.DEPARTMENTNAME,DE.DESIGNATIONNAME,EA.EMPLOYEENAME CENTERADMIN, EA.OFFICEMOBILE ADMINMOBILE,
BA.CENTERCODE SERVICECENTERLOCATIONCODE,BA.CENTERNAME SERVICECENTERLOCATION,C.CATEGORYNAME,SC.SERVICECENTERNAME FROM SC_SERVICEREQUEST SR INNER JOIN EM_EMPLOYEE E ON E.STAFFID=SR.FROMSTAFFID INNER JOIN BS_BUSINESSCENTER B ON B.ID=SR.FROMCENTERID
INNER JOIN BS_DEPARTMENT D ON D.ID=SR.FROMDEPARTMENTID INNER JOIN SC_SERVICECENTER SC ON SC.ID=SR.SERVICECENTERID INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID
INNER JOIN EM_EMPLOYEE EA ON EA.STAFFID=SC.CENTERADMIN INNER JOIN BS_BUSINESSCENTER BA ON BA.ID=SC.BUSINESSCENTERID
INNER JOIN SC_CATEGORY C ON C.ID=SR.CATEGORYID WHERE SR.ID=" + RequestId);
    }
    public string RequestStatus(int StatusId)
    {
        string Status = "";
        switch (StatusId)
        {
            case 0:
                Status = "Waiting for Assign";
                break;
            case 1:
                Status = "On Process";
                break;
            case 2:
                Status = "Waiting for User Feedback";
                break;
            case 3:
                Status = "Waiting for Service Person Feedback";
                break;
            case 4:
                Status = "Waiting For Revise";
                break;
            case 5:
                Status = "Completed";
                break;
            default:
                break;
        }
        return Status;
    }
    public DataTable GetApproverDetails(long RequestId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM SC_SERVICEREQUEST SR INNER JOIN EM_EMPLOYEE E ON E.STAFFID=SR.APPROVERID INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID WHERE SR.ID=" + RequestId);
    }
    public DataTable MyServicesRequest(int ApproveStatus)
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,
E.OFFICEMAIL,E.OFFICEMOBILE,SB.CENTERCODE CENTERLOCATION
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID INNER JOIN BS_BUSINESSCENTER SB ON SB.ID=SC.BUSINESSCENTERID
 where S.STATUS=0 AND S.ISAPPROVED=" + ApproveStatus + " AND S.ISOPEN=0 AND S.FROMSTAFFID='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable MyAllServicesRequest()
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,
E.OFFICEMAIL,E.OFFICEMOBILE,SB.CENTERCODE CENTERLOCATION
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID INNER JOIN BS_BUSINESSCENTER SB ON SB.ID=SC.BUSINESSCENTERID
 where S.STATUS<5  AND S.ISOPEN=0 AND S.FROMSTAFFID='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable MyServicesReq(int Status)
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERCODE SERVICECENTERLOCATION,
E.OFFICEMAIL,E.OFFICEMOBILE,SAE.EMPLOYEENAME SERVICEMAN,SAE.OFFICEMOBILE SERVICEMOBILE,SAE.OFFICEMAIL SERVICEEMAIL,ST.TEAMNAME
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID  INNER JOIN SC_SERVICEASSIGN SA ON SA.REQUESTID=S.ID INNER JOIN EM_EMPLOYEE SAE ON SAE.STAFFID=SA.TEAMMEMBERID
INNER JOIN SC_SERVICETEAM ST ON ST.ID=SA.TEAMID INNER JOIN BS_BUSINESSCENTER BSC
ON BSC.ID=ST.BUSINESSCENTERID where S.STATUS=" + Status + " AND SA.ISACTIVE=1 AND S.ISAPPROVED=1 AND S.ISOPEN=0 AND S.FROMSTAFFID='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable MyServices(int Status)
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERCODE SERVICECENTERLOCATION,
E.OFFICEMAIL,E.OFFICEMOBILE,SAE.EMPLOYEENAME SERVICEMAN,SAE.OFFICEMOBILE SERVICEMOBILE,SAE.OFFICEMAIL SERVICEEMAIL,ST.TEAMNAME
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID  INNER JOIN SC_SERVICEASSIGN SA ON SA.REQUESTID=S.ID INNER JOIN EM_EMPLOYEE SAE ON SAE.STAFFID=SA.TEAMMEMBERID
INNER JOIN SC_SERVICETEAM ST ON ST.ID=SA.TEAMID INNER JOIN BS_BUSINESSCENTER BSC
ON BSC.ID=ST.BUSINESSCENTERID where S.STATUS=" + Status + " AND SA.ISACTIVE=1 AND S.ISAPPROVED=1 AND S.ISOPEN=0 AND SA.TEAMMEMBERID='" + SessionManager.SessionName.StaffId + "'");
    }
    public void UpdateServiceStatus(long RequestId, int Status)
    {
        string Releasedate = "";
        if (Status == 3 || Status == 5)
        {
            Releasedate = ",RELEASEDATE=SYSDATE";
        }
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set STATUS=" + Status + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE" + Releasedate + " where ID=" + RequestId);
    }
    public void UpdateSEDone(long RequestId, DateTime BFdate, DateTime BTDate, string Remarks)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set STATUS=2,BTIMEFROM=TO_DATE('" + BFdate + "','MM/dd/yyyy hh:mi:ss am'),BTIMETO=TO_DATE('" + BTDate + "','MM/dd/yyyy hh:mi:ss am'),UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE ,RELEASEDATE=SYSDATE where ID=" + RequestId);
    }
    public void UpdateServiceRevision(long RequestId, int Status)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEREQUEST set NOOFREVISION=NOOFREVISION+1, STATUS=" + Status + " where ID=" + RequestId);
    }
    public DataTable SuperVisorServices()
    {
        return dm.ExecuteQuery(@"SELECT S.*,E.EMPLOYEENAME,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,SC.SERVICECENTERNAME,BSC.CENTERCODE SERVICECENTERLOCATION,
E.OFFICEMAIL,E.OFFICEMOBILE,SAE.EMPLOYEENAME SERVICEMAN,SAE.OFFICEMOBILE SERVICEMOBILE,SAE.OFFICEMAIL SERVICEEMAIL,ST.TEAMNAME
FROM SC_SERVICEREQUEST S INNER JOIN BS_BUSINESSCENTER B 
ON B.ID=S.FROMCENTERID INNER JOIN BS_DEPARTMENT D ON D.ID=S.FROMDEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=S.FROMSTAFFID 
INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID INNER JOIN SC_SERVICECENTER SC ON SC.ID=S.SERVICECENTERID  INNER JOIN SC_SERVICEASSIGN SA ON SA.REQUESTID=S.ID INNER JOIN EM_EMPLOYEE SAE ON SAE.STAFFID=SA.TEAMMEMBERID
INNER JOIN SC_SERVICETEAM ST ON ST.ID=SA.TEAMID INNER JOIN BS_BUSINESSCENTER BSC
ON BSC.ID=ST.BUSINESSCENTERID where S.STATUS IN(1,4) AND SA.ISACTIVE=1 AND S.ISAPPROVED=1 AND S.ISOPEN=0 AND ST.SUPERVISOR='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable GetAccessories(long RequestId)
    {
        return dm.ExecuteQuery(@"Select AC.*,SA.ACCESSORIESNAME,C.CURRENCYCODE,CASE WHEN AC.ISNEW=1 THEN 'YES' ELSE 'NO' END NEWITEM
FROM SC_REQUIREDACCESSORIES AC INNER JOIN SC_ACCESSORIES SA ON SA.ID=AC.ACCESSORIESID LEFT OUTER JOIN ASSET_CURRENCY C ON C.ID=AC.CURRENCYID WHERE AC.REQUESTID=" + RequestId);
    }
    public DataTable GetAccessoriesByID(long ID, long RequestId)
    {
        return dm.ExecuteQuery(@"Select RA.*,AC.CATEGORYID FROM SC_REQUIREDACCESSORIES RA 
        INNER JOIN SC_ACCESSORIES AC ON AC.ID=RA.ACCESSORIESID WHERE RA.REQUESTID=" + RequestId + " AND RA.ID=" + ID);
    }
    public void InsertAccessories(int itemId, decimal quantity, decimal price, int currencyid, int isnew, long RequestId)
    {
        dm.ExecuteNonQuery(@"INSERT INTO SC_REQUIREDACCESSORIES(ID,ACCESSORIESID,QUANTITY,PRICE,CURRENCYID,ISNEW,CREATEDBY,REQUESTID) 
        Values(SQ_SC_REQUIREDACCESSORIES.NEXTVAL,'" + itemId + "','" + quantity + "','" + price + "'," + currencyid + ",'" + isnew + "','" + SessionManager.SessionName.StaffId + "','" + RequestId + "')");
    }
    public void UpdateAccessories(long Id, int itemId, decimal quantity, decimal price, int currencyid, int isnew, long RequestId)
    {
        dm.ExecuteNonQuery(@"Update SC_REQUIREDACCESSORIES SET ACCESSORIESID='" + itemId + "',QUANTITY='" + quantity + "',PRICE='" + price + "',CURRENCYID=" + currencyid + ",ISNEW='" + isnew + "',REQUESTID='" + RequestId + "' where ID=" + Id);
    }
    public bool CheckDuplicate(int ItemId, long RequesId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_REQUIREDACCESSORIES WHERE ACCESSORIESID='" + ItemId + "' AND REQUESTID=" + RequesId);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RatingFromUser(int Values, long RequestId)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEASSIGN set RATINGFROMUSER=" + Values + " where ISACTIVE=1 AND REQUESTID=" + RequestId);
    }

    public void RatingFromServiceMan(int Values, long RequestId)
    {
        dm.ExecuteNonQuery(@"update SC_SERVICEASSIGN set RATINGFROMSERVICEMAN=" + Values + " where ISACTIVE=1 AND REQUESTID=" + RequestId);
    }

    public DataTable GetRatings(long RequestId)
    {
        return dm.ExecuteQuery("SELECT * from SC_SERVICEASSIGN where ISACTIVE=1 AND REQUESTID=" + RequestId);
    }

    public void ForwardService(long DispatchId, int ServiceCenterId, long RequestId)
    {
        dm.ExecuteNonQuery(@"Update SC_SERVICEREQUEST set STATUS=0, DISPATCHID='" + DispatchId + "',FRDSERVICECENTERID='" + ServiceCenterId + "',FRDATE=SYSDATE where ID=" + RequestId);
    }

    public void UpdateRemarks(long ID, string Remarks)
    {
        dm.ExecuteNonQuery(@"INSERT INTO SC_REQUESTREMARKS(ID,REQUESTID,STAFFID,REMARKS) VALUES(SQ_SC_REQUESTREMARKS.NEXTVAL," + ID + ",'" + SessionManager.SessionName.StaffId + "','" + Remarks + "')");
    }

    public DataTable GetRemarks(long RequestId)
    {
        return dm.ExecuteQuery(@"SELECT R.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,B.CENTERNAME,D.DEPARTMENTCODE,D.DEPARTMENTNAME FROM SC_REQUESTREMARKS R INNER JOIN EM_EMPLOYEE E ON E.STAFFID=R.STAFFID INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID
INNER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID WHERE R.REQUESTID=" + RequestId);
    }

    public DataTable GetServiceCenterDetails(string StaffId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM SC_TEAMMEMBER WHERE STAFFID='" + StaffId + "' AND ISACTIVE=1");
    }


}