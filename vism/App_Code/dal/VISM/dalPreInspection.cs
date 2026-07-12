using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalPreInspection
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalPreInspection()
    {
    }
    public int Insert(int vehicleid, int workshopId, string techStaffId, int sercategoryid, string Driver, string driverMobile, int millage, string remark, int groupId, string efectdate, string dldate, string CASUALTIES)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_PREINSPECTION(ID,VEHICLEID,TECSTAFFID,SERVICETYPEID,BUSINESSCENTERID,WORKSHOPID,DRIVERNAME,DRIVERMOBILE,MILLAGE,REMARKS,CREATEDBY,GROUPID,DLDATE,CASUALTIES)
        VALUES(SQ_RVS_PREINSPECTION.NEXTVAL,'" + vehicleid + "','" + techStaffId + "','" + sercategoryid + "','" + SessionManager.SessionName.BusinessCentetid + "','" + workshopId + "','" + Driver + "','" + driverMobile + "','" + millage + "','" + remark + "','" + SessionManager.SessionName.StaffId + "','" + groupId + "',TO_DATE('" + dldate + "','DD/MM/YYYY'),'"+ CASUALTIES +"')");
        int PiId = 0;
        DataTable dt = dm.ExecuteQuery("SELECT ID FROM RVS_PREINSPECTION WHERE VEHICLEID='" + vehicleid + "' AND  SERVICETYPEID='" + sercategoryid + "' AND  WORKSHOPID='" + workshopId + "' AND CREATEDBY='" + SessionManager.SessionName.StaffId + "' ORDER BY ID DESC");
        if (dt.Rows.Count > 0)
        {
            PiId = Convert.ToInt32(dt.Rows[0][0]);
        }
        return PiId;
    }
    public void InsertPIDetails(int Pid, int ItemId, int Status, string remark)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_PIDETAILS(PIID,INSPECTIONITEMID,STATUSID,REMARKS)
        VALUES('" + Pid + "','" + ItemId + "','" + Status + "','" + remark + "')");
    }
    public void InsertPIDetailsUpdate(int Pid, int ItemId, int Status, string remark, int Qty, int LaborRate, int LaborQty, int WorkingHour)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_PIDETAILS(PIID,INSPECTIONITEMID,STATUSID,REMARKS,QUANTITY,LABORRATE,LABORQTY,WORKINGHOUR)
        VALUES('" + Pid + "','" + ItemId + "','" + Status + "','" + remark + "','" + Qty + "', '" + LaborRate + "', '" + LaborQty + "', '" + WorkingHour + "')");
    }
    public void InsertPIDetailsUpdateNew(int Pid, int ItemId, int Status, string remark, int Qty, int LaborRate, int LaborQty, float WorkingHour)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_PIDETAILS(PIID,INSPECTIONITEMID,STATUSID,REMARKS,QUANTITY,LABORRATE,LABORQTY,WORKINGHOUR)
        VALUES('" + Pid + "','" + ItemId + "','" + Status + "','" + remark + "','" + Qty + "', '" + LaborRate + "', '" + LaborQty + "', '" + WorkingHour + "')");
    }
    public void DeletePIDetails(int Pid)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_PIDETAILS Where PIID='" + Pid + "'");
    }
    public int Update(int id, int vehicleid, int workshopId, string techStaffId, int sercategoryid, string Driver, string driverMobile, int millage, string remark,int groupId, string efectdate, string dldate)
    {
        //return dm.ExecuteNonQuery("update RVS_PREINSPECTION set VEHICLEID='" + vehicleid + "',WORKSHOPID='" + workshopId + "',TECSTAFFID='" + techStaffId + "',SERVICETYPEID='" + sercategoryid + "',DRIVERNAME='" + Driver + "',DRIVERMOBILE='" + driverMobile + "',MILLAGE='" + millage + "',REMARKS='" + remark + "',GROUPID='"+groupId+ "',EFCTDATE=TO_DATE('" + efectdate + "','DD/MM/YYYY'), UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE,DLDATE=TO_DATE('" + dldate + @"','DD/MM/YYYY') WHERE ID='" + id+"'");
        return dm.ExecuteNonQuery("update RVS_PREINSPECTION set VEHICLEID='" + vehicleid + "',WORKSHOPID='" + workshopId + "',TECSTAFFID='" + techStaffId + "',SERVICETYPEID='" + sercategoryid + "',DRIVERNAME='" + Driver + "',DRIVERMOBILE='" + driverMobile + "',MILLAGE='" + millage + "',REMARKS='" + remark + "',GROUPID='" + groupId + "',EFCTDATE=TO_DATE('" + efectdate + "','DD/MM/YYYY'), UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE,DLDATE=TO_DATE('" + dldate +"','DD/MM/YYYY') WHERE ID='" + id + "'");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select PI.ID,PI.VEHICLEID,PI.EFFECTIVEDATE,PI.TECSTAFFID,PI.REMARKS,PI.SERVICETYPEID,PI.ISOPEN,PI.CREATEDBY,PI.CREATEDATE,PI.UPDATEDBY,PI.UPDATEDATE,PI.WORKSHOPID,
PI.BUSINESSCENTERID, PI.MILLAGE, PI.ISACTIVE, PI.GROUPID, PI.EFCTDATE, VH.DRIVERMOBILE, VH.DRIVERNAME, VH.DRIVERID,NVL(VH.CNGCYLINDERREDDATE,NULL) CNGCYLINDERREDDATE,
to_date(PI.EFCTDATE, 'dd/mm/yy') etdate,to_date(PI.DLdate, 'dd/mm/yy') DLdate from RVS_PREINSPECTION PI
 INNER JOIN RVS_VEHICLE VH ON VH.ID = PI.VEHICLEID WHERE PI.ISOPEN = 1 AND PI.ID=" + id);
    }
    public DataTable GetAll(int workshopid, int sercategoryid)
    {
        return dm.ExecuteQuery(@"SELECT PI.*, SC.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS FROM RVS_PREINSPECTION PI
        INNER JOIN RVS_SERVICETYPE SC ON SC.ID= PI.SERVICETYPEID
        INNER JOIN RVS_VEHICLE V ON V.ID=PI.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID WHERE PI.ISOPEN=1 and PI.SERVICETYPEID='" + sercategoryid + "' and PI.WORKSHOPID='" + workshopid + "'");
    }

    public DataTable GetInspectionItems(int Pid, int status)
    {
        string query = @"Select II.ID,SC.CATNAME, II.ITEMNAME," + Pid + " PIID, " +
        @" (SELECT PD.STATUSID FROM RVS_PIDETAILS PD WHERE PD.INSPECTIONITEMID=II.ID AND PD.PIID=" + Pid + " ) STATUSID, " +
        @" (SELECT PD.REMARKS FROM RVS_PIDETAILS PD WHERE PD.INSPECTIONITEMID=II.ID AND PD.PIID= " + Pid + ") REMARKS from RVS_INSPECTIONITEMS II  " +
        @"  INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID INNER JOIN RVS_PIDETAILS PD ON PD.INSPECTIONITEMID = II.ID
        WHERE II.INSPECTIONTYPE=0 AND PD.STATUSID=" + status + " AND PD.PIID=" + Pid + " ORDER BY II.SERVICECATID ASC";
       
        return dm.ExecuteQuery(query);
    }

    public DataTable LoadInspectionItems(int Pid)
    {
        string query = @"Select II.ID,SC.CATNAME, II.ITEMNAME," + Pid + " PIID, " +
       @" (SELECT PD.STATUSID FROM RVS_PIDETAILS PD WHERE PD.INSPECTIONITEMID=II.ID AND PD.PIID=" + Pid + " ) STATUSID, " +
       @" (SELECT PD.REMARKS FROM RVS_PIDETAILS PD WHERE PD.INSPECTIONITEMID=II.ID AND PD.PIID= " + Pid + ") REMARKS from RVS_INSPECTIONITEMS II  " +
       @"  INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID 
        WHERE II.INSPECTIONTYPE=0 ORDER BY II.SERVICECATID ASC";
        //if (Pid > 0)
        //{
        //    query = @"Select II.ID,SC.CATNAME,II.ITEMNAME,PD.PIID,PD.STATUSID, PD.REMARKS from RVS_INSPECTIONITEMS II
        //    INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID
        //    INNER JOIN RVS_PIDETAILS PD ON PD.INSPECTIONITEMID = II.ID WHERE II.INSPECTIONTYPE=0 AND PD.PIID='" + Pid + "'  ORDER BY II.SERVICECATID ASC";
        //}
        return dm.ExecuteQuery(query);
    }
    public DataTable GetProblems(int Pid)
    {
        string query = @"Select II.ID,SC.CATNAME,II.ITEMNAME,PD.PIID,PD.STATUSID,PD.QUANTITY, PD.REMARKS from RVS_INSPECTIONITEMS II
            INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID
            INNER JOIN RVS_PIDETAILS PD ON PD.INSPECTIONITEMID = II.ID WHERE II.INSPECTIONTYPE=0 AND PD.STATUSID=2 AND PD.PIID='" + Pid + "'  ORDER BY II.SERVICECATID ASC";

        return dm.ExecuteQuery(query);
    }
    public DataTable GetPIDetails(int PIID)
    {
        return dm.ExecuteQuery(@"SELECT J.*, ST.SERVICETYPE,V.CHASSISNO,V.REGNO,V.ENGINENO,V.MILLAGE,V.MANUFACTURINGYEAR , M.MODELNAME
        ,C.CUSTOMERNAME,C.MOBILE,C.PHONE ,C.ADDRESS ,O.*,W.WORKSHOP,CO.COMPANYNAME, W.ADDRESS WADDRESS,B.* FROM RVS_PREINSPECTION J
        LEFT OUTER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        LEFT OUTER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        LEFT OUTER JOIN RVS_BILL B ON B.JOBID=J.ID
        LEFT OUTER JOIN RVS_MODEL M ON M.ID = V.MODELID
        INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
        WHERE J.ID=" + PIID);
    }
    public DataTable VehiclePIHistory(String critaria)
    {
        return dm.ExecuteQuery(@"SELECT J.*, ST.SERVICETYPE,V.CHASSISNO,V.REGNO,V.ENGINENO,V.MILLAGE,V.MANUFACTURINGYEAR , M.MODELNAME
        ,C.CUSTOMERNAME,C.MOBILE,C.PHONE ,C.ADDRESS ,O.*,W.WORKSHOP,CO.COMPANYNAME, W.ADDRESS WADDRESS,B.* FROM RVS_PREINSPECTION J
        INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        LEFT OUTER JOIN RVS_BILL B ON B.JOBID=J.ID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
         WHERE J.ID>0 AND J.ISACTIVE=1 " + critaria+" ORDER BY J.CREATEDATE DESC ");
    }
    public void ClosePI(int PiID)
    {
        dm.ExecuteNonQuery("update RVS_PREINSPECTION set ISOPEN=0,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + PiID);
    }
    public string CheckDuplicate(int VehicleId)
    {
        DataTable dt = dm.ExecuteQuery(@"Select W.WORKSHOP from RVS_PREINSPECTION PI
                        INNER JOIN RVS_WORKSHOP W ON W.ID=PI.WORKSHOPID
                        WHERE PI.ISOPEN=1 AND PI.VEHICLEID='" + VehicleId + "'");
        if (dt.Rows.Count > 0)
        {
            return "PI already open in " + dt.Rows[0]["WORKSHOP"].ToString();
        }
        else
        {
            return "0";
        }
    }
}