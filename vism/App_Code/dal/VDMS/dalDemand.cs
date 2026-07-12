using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalDemand
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDemand()
    {
    }
    public int Insert(int buscenter, int routeid, DateTime fdate, int demandset, string staffid, string name, int departmentid, int designationid, string mobile, string email, int triptype, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_DEMAND(ID,BUSINESSCENTERID,ROUTEID,FDATE,DEMANDSEAT,
        STAFFID,NAME,DEPARTMENTID,DESIGNATIONID,MOBILE,EMAIL,TRIPTYPE,REMARKS,CREATEDBY)
        VALUES(SQ_VD_DEMAND.NEXTVAL,'" + buscenter + "','" + routeid + "',TO_DATE('" + fdate + "','dd/MM/yyyy HH:MI:SSAM'),'" + demandset + "','" + staffid + "','" + name + "','" + departmentid + "','" + designationid + "','" + mobile + "','" + email + "','" + triptype + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int buscenter, int routeid, DateTime fdate, int demandset, string staffid, string name, int departmentid, int designationid, string mobile, string email, int triptype, string remarks)
    {
        return dm.ExecuteNonQuery("update VD_DEMAND set BUSINESSCENTERID='" + buscenter + "',ROUTEID='" + routeid + "',FDATE=TO_DATE('" + fdate + "','dd/MM/yyyy HH:MI:SSAM'),DEMANDSEAT='" + demandset + "',STAFFID='" + staffid + "',NAME='" + name + "',DEPARTMENTID='" + departmentid + "',DESIGNATIONID='" + designationid + "',MOBILE='" + mobile + "',EMAIL='" + email + "',TRIPTYPE='" + triptype + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_DEMAND WHERE ID=" + id);
    }
    public DataTable GetALL()
    {
        return dm.ExecuteQuery(@"Select D.*, R.ROUTENAME,DP.DEPARTMENTNAME,DE.DESIGNATIONNAME, BC.CENTERNAME  from VD_DEMAND D
            INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=D.BUSINESSCENTERID
            INNER JOIN VD_ROUTE R ON R.ID = D.ROUTEID
            INNER JOIN BS_DEPARTMENT DP ON DP.ID= D.DEPARTMENTID
            INNER JOIN BS_DESIGNATION DE ON DE.ID = D.DESIGNATIONID ");
    }

    public DataTable GetDemandList(string craiteria)
    {
        return dm.ExecuteQuery(@"Select D.*, R.ROUTENAME,DP.DEPARTMENTNAME,DE.DESIGNATIONNAME, BC.CENTERNAME  from VD_DEMAND D
            INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=D.BUSINESSCENTERID
            INNER JOIN VD_ROUTE R ON R.ID = D.ROUTEID
            INNER JOIN BS_DEPARTMENT DP ON DP.ID= D.DEPARTMENTID
            INNER JOIN BS_DESIGNATION DE ON DE.ID = D.DESIGNATIONID WHERE D.ID>0" + craiteria);
    }

    public DataTable GetTotalSeat(string craiteria)
    {
        return dm.ExecuteQuery(@"SELECT COUNT(D.DEMANDSEAT) TOTALSEAT FROM VD_DEMAND D WHERE D.ID>0" + craiteria);
    }

    public DataTable GetTotalSeatRouteWise(string craiteria)
    {
        return dm.ExecuteQuery(@"SELECT COUNT(D.DEMANDSEAT) TOTALSEAT, BC.CENTERNAME, R.ROUTENAME FROM VD_DEMAND D
    INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=D.BUSINESSCENTERID
    INNER JOIN VD_ROUTE R ON R.ID = D.ROUTEID
     WHERE D.ID>0 " + craiteria + " GROUP BY R.ROUTENAME,BC.CENTERNAME  ORDER BY COUNT(D.DEMANDSEAT) DESC");
    }
    public DataTable GetEmployeeDetails(string staffId)
    {
        return dm.ExecuteQuery(@"Select E.*,B.CENTERNAME,B.CENTERCODE,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,L.NAME,C.COUNTRYNAME,L.COUNTRYID from EM_EMPLOYEE E inner join BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID INNER JOIN BS_LOCATIONS L ON L.ID=B.LOCATIONID
        INNER JOIN BS_COUNTRY C ON  C.ID=L.COUNTRYID INNER JOIN BS_DEPARTMENT D on D.ID=E.DEPARTMENTID Inner join BS_DESIGNATION DE on DE.ID=E.DESIGNATIONID where E.STAFFID='" + staffId + "'");
    }
    public bool CheckDuplicate(string chassisno)
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_DEMAND WHERE CHASSISNO='" + chassisno + "'");
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