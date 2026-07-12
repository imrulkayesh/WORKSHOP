using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalPDI
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalPDI()
    {
    }
    public int Insert(int jobid, string staffid, int teamid, string remark)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_PDI(ID,JOBID,TECSTAFFID,REMARKS,CREATEDBY)
        VALUES(SQ_RVS_PDI.NEXTVAL,'" + jobid + "','" + staffid + "','" + remark + "','" + SessionManager.SessionName.StaffId + "')");
        int PDiId = 0;
        DataTable dt = dm.ExecuteQuery("SELECT ID FROM RVS_PDI WHERE JOBID='" + jobid + "'  AND CREATEDBY='" + SessionManager.SessionName.StaffId + "' ORDER BY ID DESC");
        if (dt.Rows.Count > 0)
        {
            PDiId = Convert.ToInt32(dt.Rows[0][0]);
        }
        return PDiId;
    }
    public int Update(int id, int jobid, string staffid, int teamid, string remark)
    {
        return dm.ExecuteNonQuery("update RVS_PDI set JOBID='" + jobid + "',TECSTAFFID='" + staffid + "',REMARKS='" + remark + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_PDI WHERE ID=" + id);
    }
    public DataTable GetAll(int teamid)
    {
        return dm.ExecuteQuery("Select P.*,T.TEAMNAME from RVS_PDI P INNER JOIN RVS_TEAM T ON T.ID= P.TEAMID WHERE P.TEAMID='" + teamid + "'");
    }
    public DataTable GetInspectionItems(int PDid)
    {
        string query = @"Select I.ID,SC.CATNAME, I.ITEMNAME,'' PDIID,'0' STATUSID, '' REMARKS from RVS_INSPECTIONITEMS I
                        INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID = I.SERVICECATID
        WHERE I.INSPECTIONTYPE=1 ORDER BY I.SERVICECATID ASC";
        if (PDid > 0)
        {
            query = @"Select II.ID,SC.CATNAME,II.ITEMNAME,PD.PDIID,PD.STATUSID, PD.REMARKS from RVS_INSPECTIONITEMS II
            INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID
            INNER JOIN RVS_PDIDETAILS PD ON PD.INSPECTIONITEMID = II.ID WHERE II.INSPECTIONTYPE=1 AND PD.PDIID='" + PDid + "'  ORDER BY II.SERVICECATID ASC";
        }
        return dm.ExecuteQuery(query);
    }
    public DataTable GetPDIDetails(int PDIID)
    {
        return dm.ExecuteQuery(@"SELECT PD.*, ST.SERVICETYPE,V.CHASSISNO, V.REGNO, V.ENGINENO ,V.MILLAGE,V.MANUFACTURINGYEAR,
        C.CUSTOMERNAME,W.WORKSHOP,W.ADDRESS WADDRESS,C.MOBILE,C.PHONE ,CO.COMPANYNAME, C.ADDRESS ,O.OWNPERCENT,
        PI.DRIVERNAME, PI.DRIVERMOBILE FROM RVS_PDI PD
        LEFT OUTER JOIN RVS_JOBCARD J ON J.ID=PD.JOBID
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID = V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        INNER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN V_COMPANY CO ON CO.COMPANYID =V.COMPANYID
        WHERE PD.ID=" + PDIID);
    }
    public DataTable GetPDIItems(int PDIID)
    {
        string query = @"Select P.ID,SC.CATNAME, I.ITEMNAME,'' PDIID,'0' STATUSID, '' REMARKS from RVS_PDI P
                        LEFT OUTER JOIN RVS_INSPECTIONITEMS I ON I.ID = P.JOBID 
                        INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID = I.SERVICECATID
        WHERE II.INSPECTIONTYPE=1 ORDER BY II.SERVICECATID ASC";
        if (PDIID > 0)
        {
            query = @"Select II.ID,SC.CATNAME,II.ITEMNAME,PD.PDIID,PD.STATUSID, PD.REMARKS from RVS_INSPECTIONITEMS II
            INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID
            INNER JOIN RVS_PDIDETAILS PD ON PD.INSPECTIONITEMID = II.ID WHERE II.INSPECTIONTYPE=1 AND PD.PDIID='" + PDIID + "'  ORDER BY II.SERVICECATID ASC";
        }
        return dm.ExecuteQuery(query);
    }
    public bool CheckDuplicate(int jobid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_PDI WHERE TEAMID='" + jobid + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ClosePDI(int iD)
    {
        throw new NotImplementedException();
    }
    public DataTable GetAllPDI(int workshopid)
    {
        return dm.ExecuteQuery(@"SELECT P.*, ST.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS FROM RVS_PDI P
        INNER JOIN RVS_JOBCARD J ON J.ID=P.JOBID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        WHERE P.ISOPEN=1 and J.WORKSHOPID='" + workshopid + "'");
    }

    public void DeletePDIDetails(int pdid)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_PDIDETAILS Where PDIID='" + pdid + "'");
    }
    public void InsertPDIDetails(int pdid, int ItemId, int Status, string remark)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_PDIDETAILS(PDIID,INSPECTIONITEMID,STATUSID,REMARKS)
        VALUES('" + pdid + "','" + ItemId + "','" + Status + "','" + remark + "')");
    }
}