using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalServiceTeam
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalServiceTeam()
    {
    }
    public int Insert(string name, int ServiceCenterid, int businessCenterid, string supervisorid)
    {
        return dm.ExecuteNonQuery(@"Insert into SC_SERVICETEAM(ID,TEAMNAME,SERVICECENTERID,BUSINESSCENTERID,SUPERVISOR,CREATEDBY)
        VALUES(SQ_SC_SERVICETEAM.NEXTVAL,'" + name + "','" + ServiceCenterid + "','" + businessCenterid + "','" + supervisorid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, int ServiceCenterid, int businessCenterid, string supervisorid)
    {
        return dm.ExecuteNonQuery("update SC_SERVICETEAM set TEAMNAME='" + name + "',SERVICECENTERID='" + ServiceCenterid + "',BUSINESSCENTERID='" + businessCenterid + "',SUPERVISOR='" + supervisorid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select T.*,SC.SERVICECENTERNAME,B.CENTERNAME,D.DESIGNATIONNAME,DE.DEPARTMENTCODE,E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL from SC_SERVICETEAM T INNER JOIN
SC_SERVICECENTER SC ON SC.ID=T.SERVICECENTERID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=T.SUPERVISOR INNER JOIN BS_DESIGNATION D ON D.ID=E.DESIGNATIONID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=E.DEPARTMENTID INNER JOIN BS_BUSINESSCENTER B ON B.ID=T.BUSINESSCENTERID WHERE T.ID=" + id);
    }
    public DataTable GetAll(int ServiceCenterId)
    {
        return dm.ExecuteQuery(@"Select T.*,SC.SERVICECENTERNAME,B.CENTERNAME,D.DESIGNATIONNAME,DE.DEPARTMENTCODE,E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL from SC_SERVICETEAM T INNER JOIN
SC_SERVICECENTER SC ON SC.ID=T.SERVICECENTERID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=T.SUPERVISOR INNER JOIN BS_DESIGNATION D ON D.ID=E.DESIGNATIONID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=E.DEPARTMENTID INNER JOIN BS_BUSINESSCENTER B ON B.ID=T.BUSINESSCENTERID WHERE T.SERVICECENTERID=" + ServiceCenterId);
    }
    public bool CheckDuplicate(string name, int ServiceCenterId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_SERVICETEAM WHERE TEAMNAME='" + name + "' and SERVICECENTERID=" + ServiceCenterId);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public DataTable GetByServiceCenterId(int ServiceCenterId)
    {
        return dm.ExecuteQuery("Select * from SC_SERVICETEAM WHERE SERVICECENTERID=" + ServiceCenterId);
    }

    public DataTable GetAllTeamMember(int TeamId)
    {
        return dm.ExecuteQuery(@"Select TM.*,T.TEAMNAME,SC.SERVICECENTERNAME,B.CENTERNAME,D.DESIGNATIONNAME,DE.DEPARTMENTCODE,E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL from SC_TEAMMEMBER TM INNER JOIN
SC_SERVICETEAM T ON T.ID=TM.TEAMID INNER JOIN
SC_SERVICECENTER SC ON SC.ID=T.SERVICECENTERID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID INNER JOIN BS_DESIGNATION D ON D.ID=E.DESIGNATIONID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=E.DEPARTMENTID INNER JOIN BS_BUSINESSCENTER B ON B.ID=TM.BUSINESSCENTERID WHERE TM.TEAMID=" + TeamId);
    }
    public DataTable GetByTeamMemberId(int TeamMemberId)
    {
        return dm.ExecuteQuery(@"Select TM.*,T.TEAMNAME,SC.SERVICECENTERNAME,B.CENTERNAME,D.DESIGNATIONNAME,DE.DEPARTMENTCODE,E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL from SC_TEAMMEMBER TM INNER JOIN
SC_SERVICETEAM T ON T.ID=TM.TEAMID INNER JOIN
SC_SERVICECENTER SC ON SC.ID=T.SERVICECENTERID INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID INNER JOIN BS_DESIGNATION D ON D.ID=E.DESIGNATIONID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=E.DEPARTMENTID INNER JOIN BS_BUSINESSCENTER B ON B.ID=TM.BUSINESSCENTERID WHERE TM.ID=" + TeamMemberId);
    }
    public int InsertTeamMember(int TeamId, int ServiceCenterid, int businessCenterid, long MemberId)
    {
        return dm.ExecuteNonQuery(@"Insert into SC_TEAMMEMBER(ID,TEAMID,SERVICECENTERID,BUSINESSCENTERID,STAFFID,CREATEDBY)
        VALUES(SQ_SC_TEAMMEMBER.NEXTVAL,'" + TeamId + "','" + ServiceCenterid + "','" + businessCenterid + "','" + MemberId + "','" + SessionManager.SessionName.UserId + "')");
    }
    public int UpdateTeamMember(int id, int TeamId, int ServiceCenterid, int businessCenterid, long MemberId)
    {
        return dm.ExecuteNonQuery("update SC_TEAMMEMBER set TEAMID='" + TeamId + "',SERVICECENTERID='" + ServiceCenterid + "',BUSINESSCENTERID='" + businessCenterid + "',STAFFID='" + MemberId + "',UPDATEDBY='" + SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID=" + id);
    }
}