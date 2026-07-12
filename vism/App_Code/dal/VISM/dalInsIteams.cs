using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalInsIteams
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalInsIteams()
    {
    }
    public int Insert(string item, int coder, int InspectionType, int ServiceCatId, string LaborQuantity, string LaborRate, string WorkingHour, int Laborcharge)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_INSPECTIONITEMS(ID,ITEMNAME,CORDER,INSPECTIONTYPE,SERVICECATID,CREATEDBY,LABORQTY,LABORRATE,WORKINGHOUR,LABORCHARGE)
        VALUES(SQ_RVS_INSPECTIONITEMS.NEXTVAL,'" + item + "','" + coder + "','" + InspectionType + "','" + ServiceCatId + "','" + SessionManager.SessionName.StaffId + "', '"+ LaborQuantity + "','"+ LaborRate + "','"+ WorkingHour + "','"+ Laborcharge + "')");
    }
    public int Update(int id, string item, int coder, int InspectionType, int ServiceCatId, string LaborQuantity, string LaborRate, string WorkingHour, int Laborcharge)
    {
        return dm.ExecuteNonQuery("update RVS_INSPECTIONITEMS set SERVICECATID='" + ServiceCatId + "', ITEMNAME='" + item + "',CORDER='" + coder + "',INSPECTIONTYPE='" + InspectionType + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "', LABORQTY='"+ LaborQuantity + "', LABORRATE='"+ LaborRate + "', WORKINGHOUR='"+ WorkingHour + "', LABORCHARGE='" + Laborcharge + "',   UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_INSPECTIONITEMS WHERE ID=" + id);
    }
    public DataTable GetAll(int InspectionType, int servicecatid)
    {
        return dm.ExecuteQuery("Select * from RVS_INSPECTIONITEMS Where SERVICECATID='" + servicecatid + "' AND INSPECTIONTYPE='" + InspectionType + "'");
    }
    public bool CheckDuplicate(string item, int inspectiontype)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_INSPECTIONITEMS WHERE ITEMNAME='" + item + "' and INSPECTIONTYPE='" + inspectiontype + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CheckToolsDuplicate(string item)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_TOOLSITEMS WHERE TOOLITEMID ='" + item + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CheckNPTsDuplicate(string jobid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_NPT WHERE JOBID ='" + jobid + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int InsertTools(string itemcode, string itemname, int scrcat, string scmunit)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_TOOLSITEMS(TOOLITEMID,ITEMNAME,SERVICECATEGORYID,SCMUNITCODE,WKSPID,CREATEDBY)
        VALUES('" + itemcode + "','" + itemname + "','" + scrcat + "','" + scmunit + "','1','" + SessionManager.SessionName.StaffId + "')");
    }

    public int InsertNPT(string tbxVehicleid, string ddlJobId, string ddlDelayCauses, string ddlResponsibleDepartment, string tbxTime, string tbxRemarks, string createdBy, string isActive, string ddlWorkWorkshopId)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_NPT(VEHICLEID,JOBID,DELAYCAUSESID,RESPONSIBLEDEPARTMENTID,TIME,REMARKS,CREATEDBY,ISACTIVE,WORKSHOPID)
        VALUES('" + tbxVehicleid + "','" + ddlJobId + "','" + ddlDelayCauses + "','" + ddlResponsibleDepartment +"','" + tbxTime + "','" + tbxRemarks + "','" + createdBy + "','" + isActive + "','" + ddlWorkWorkshopId + "')");
    }

    public DataTable GetByNPTJobId(int jobId)
    {
        return dm.ExecuteQuery("select NPT.*, V.REGNO from RVS_NPT NPT LEFT JOIN RVS_VEHICLE V ON NPT.VEHICLEID = V.ID where JOBID=" + jobId);
    }

    public DataTable GetByToolId(int id)
    {
        return dm.ExecuteQuery("select * from RVS_TOOLSITEMS where TOOLITEMID="+ id);
    }

    public int UpdateToolItem(int id, string item, int scrcat, string scmunit)
    {
        return dm.ExecuteNonQuery("update RVS_TOOLSITEMS set ITEMNAME='" + item + "', SERVICECATEGORYID='" + scrcat + "',SCMUNITCODE='" + scmunit + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE TOOLITEMID=" + id);
    }
    public int UpdateNPT(int id, string time, int ResponsibleDepartmentId, string ddlDelayCausesId, string remarks, string ddlWorkWorkshopId)
    {
        return dm.ExecuteNonQuery("update RVS_NPT set TIME='" + time + "', RESPONSIBLEDEPARTMENTID='" + ResponsibleDepartmentId + "',DELAYCAUSESID='" + ddlDelayCausesId + "',REMARKS='" + remarks + "',WORKSHOPID='" + ddlWorkWorkshopId + "',UPDATEBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE JOBID=" + id);
    }

    public int InsertToolsTrn(int wkid, string tecid, int itemcode, decimal qty, string remks)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_TOOLSDTL(ID,WKSPID,STAFFID,TOOLITEMID,QTY,REMARKS,CREATEDBY)
        VALUES(SQ_RVS_TOOLSTRN.NEXTVAL,'" + wkid + "','" + tecid + "','" + itemcode + "','" + qty + "','"+ remks + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public DataTable RVS_TOOLSDTL(int id)
    {
        return dm.ExecuteQuery(@"select TD.*,SC.ID SID 
from RVS_TOOLSDTL TD
INNER JOIN RVS_TOOLSITEMS TI ON TI.TOOLITEMID = TD.TOOLITEMID
INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID = TI.SERVICECATEGORYID
where TD.ID = '"+id+ @"'");
    }

    public int UpdateToolTrn(int id, int wkid, string tecid, int itemcode, decimal qty, string remks)
    {
        return dm.ExecuteNonQuery("update RVS_TOOLSDTL set WKSPID='" + wkid + "', STAFFID='" + tecid + "',TOOLITEMID='" + itemcode + "',QTY='" + qty + "',REMARKS='"+ remks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
}