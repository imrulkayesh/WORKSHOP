using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalPoolSchedule
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalPoolSchedule()
    {
    }
    public int Insert(int buId, int RouteId, string day, string starttime, string returntime)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_POOLSCHEDULE(ID,BUSINESSCENTERID,ROUTEID,DAYS,STARTTIME,RETURNTIME,CREATEDBY)
        VALUES(SQ_VD_POOLSCHEDULE.NEXTVAL,'" + buId + "','" + RouteId + "','" + day + "','" + starttime + "','" + returntime + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int buId, int RouteId, string day, string starttime, string returntime)
    {
        return dm.ExecuteNonQuery("update VD_POOLSCHEDULE set BUSINESSCENTERID='" + buId + "',ROUTEID='" + RouteId + "',DAYS='" + day + "',STARTTIME='" + starttime + "',RETURNTIME='" + returntime + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_POOLSCHEDULE WHERE ID=" + id);
    }
    public DataTable GetALL(int BusinessCenterId)
    {
        return dm.ExecuteQuery(@"Select D.*, BC.CENTERNAME,R.ROUTENAME  from VD_POOLSCHEDULE D
            INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=D.BUSINESSCENTERID
            INNER JOIN VD_ROUTE R ON R.ID=D.ROUTEID
            WHERE D.BUSINESSCENTERID=" + BusinessCenterId);
    }
}