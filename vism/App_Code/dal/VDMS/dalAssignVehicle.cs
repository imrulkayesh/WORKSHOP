using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalAssignVehicle
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalAssignVehicle()
    {
    }
    public int Insert(int schedule, int demandid, string driver, string approveby, DateTime apvdate, int vehicleid, decimal startmillage, decimal endmillage, string drivercode, DateTime releaseTime)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_ASSIGNVEHICLE(ID,SCHEDULEID,DEMANDID,DRIVER,APPROVEDBY,APVDATE,VEHICLEID,STARTMILLAGE,ENDMILLAGE,DRIVERCODE,RELEASETIME,CREATEDBY)
        VALUES(SQ_VD_ASSIGNVEHICLE.NEXTVAL,'" + schedule + "','" + demandid + "','" + driver + "','" + approveby + "',TO_DATE('" + apvdate + "','MM/dd/yyyy HH:MI:SSAM'),'" + vehicleid + "','" + startmillage + "','" + endmillage + "','" + drivercode + "',TO_DATE('" + releaseTime + "','MM/dd/yyyy HH:MI:SSAM'),'" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int schedule, int demandid, string driver, string approveby, DateTime apvdate, int vehicleid, decimal startmillage, decimal endmillage, string drivercode, DateTime releaseTime)
    {
        return dm.ExecuteNonQuery("update VD_ASSIGNVEHICLE set SCHEDULEID='" + schedule + "',DEMANDID='" + demandid + "',DRIVER='" + driver + "',APPROVEDBY='" + approveby + "', APVDATE=TO_DATE('" + apvdate + "','MM/dd/yyyy HH:MI:SSAM'), VEHICLEID='" + vehicleid + "', STARTMILLAGE='" + startmillage + "', ENDMILLAGE='" + endmillage + "', DRIVERCODE='" + drivercode + "', RELEASETIME=TO_DATE('" + releaseTime + "','MM/dd/yyyy HH:MI:SSAM'),UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_ASSIGNVEHICLE WHERE ID=" + id);
    }
    public DataTable GetALLAssignVehicle()
    {
        return dm.ExecuteQuery(@"SELECT AV.*, D.DEMANDSEAT, P.RETURNTIME,P.DAYS, P.STARTTIME, V.CHASSISNO, V.REGNO, V.TAGNO
        FROM VD_ASSIGNVEHICLE AV
        INNER JOIN VD_DEMAND D ON D.ID= AV.DEMANDID
        INNER JOIN VD_POOLSCHEDULE P ON P.ID = AV.SCHEDULEID
        INNER JOIN VD_VEHICLE V ON V.ID = AV.VEHICLEID ");
    }
    public bool CheckDuplicate()
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_ASSIGNVEHICLE ");
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