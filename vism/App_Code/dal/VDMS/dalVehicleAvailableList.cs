using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
public class dalVehicleAvailableList
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVehicleAvailableList()
    {
    }
    public int Insert(int buscenter, int routeid, string busno, int vehicleTypeid, string dstaffid, string dname, string dmobile, string fromTo, DateTime departureDate, string departure, string arrive, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_VEHICLEALIST(ID,BUSINESSCENTERID,ROUTEID,BUSNO,VEHICLETYPEID,DRIVERSTAFFID,DRIVERNAME,DRIVERMOBILE,FROMTO,DEPARTUREDATE,DEPARTURETIME,ARRIVETIME,REMARKS,CREATEDBY)
        VALUES(SQ_VD_VEHICLEALIST.NEXTVAL,'" + buscenter + "','" + routeid + "','" + busno + "','" + vehicleTypeid + "','" + dstaffid + "','" + dname + "','" + dmobile + "','" + fromTo + "',TO_DATE('" + departureDate + "','dd/MM/yyyy HH:MI:SSAM'),'" + departure + "','" + arrive + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int buscenter, int routeid, string busno, int vehicleTypeid, string dstaffid, string dname, string dmobile, string fromTo, DateTime departureDate, string departure, string arrive, string remarks)
    {
        return dm.ExecuteNonQuery("update VD_VEHICLEALIST set BUSINESSCENTERID='" + buscenter + "',ROUTEID='" + routeid + "', BUSNO='" + busno + "',VEHICLETYPEID='" + vehicleTypeid + "',DRIVERSTAFFID='" + dstaffid + "',DRIVERNAME='" + dname + "',DRIVERMOBILE='" + dmobile + "',FROMTO='" + fromTo + "',DEPARTUREDATE=TO_DATE('" + departureDate + "','dd/MM/yyyy HH:MI:SSAM'),DEPARTURETIME='" + departure + "',ARRIVETIME='" + arrive + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_VEHICLEALIST WHERE ID=" + id);
    }

    public DataTable GetVehicleAllocationR(string critaria)
    {
        return dm.ExecuteQuery(@"Select VL.*,B.CENTERNAME,R.ROUTENAME,VT.VEHICLETYPE FROM
         VD_VEHICLEALIST VL,
         BS_BUSINESSCENTER B,
         VD_ROUTE R,
         VD_VEHICLETYPE VT
         WHERE
         B.ID = VL.BUSINESSCENTERID AND
         R.ID = VL.ROUTEID AND
         VT.ID = VL.VEHICLETYPEID AND VL.ID>0" + critaria);
    }
    public DataTable GetALL()
    {
        return dm.ExecuteQuery(@"Select VL.*,B.CENTERNAME,R.ROUTENAME,VT.VEHICLETYPE FROM
         VD_VEHICLEALIST VL,
         BS_BUSINESSCENTER B,
         VD_ROUTE R,
         VD_VEHICLETYPE VT
         WHERE
         B.ID = VL.BUSINESSCENTERID AND
         R.ID = VL.ROUTEID AND
         VT.ID = VL.VEHICLETYPEID");
    }
    public bool CheckDuplicate(string busno, int vehicleTypeid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_VEHICLEALIST WHERE BUSNO='" + busno + "' AND VEHICLETYPEID='" + vehicleTypeid + "'");
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