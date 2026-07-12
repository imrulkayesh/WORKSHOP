using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVDDriver
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVDDriver()
    {
    }
    public int Insert(int buscenter, string staffid, string driverName, string driverMobile)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_DRIVER(ID,BUSINESSCENTERID,STAFFID,DRIVERNAME,DRIVERMOBILE,CREATEDBY)
        VALUES(SQ_VD_DRIVER.NEXTVAL,'" + buscenter + "','" + staffid + "','" + driverName + "','" + driverMobile + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int buscenter, string staffid, string driverName, string driverMobile)
    {
        return dm.ExecuteNonQuery("update VD_DRIVER set BUSINESSCENTERID='" + buscenter + "',STAFFID='" + staffid + "',DRIVERNAME='" + driverName + "',DRIVERMOBILE='" + driverMobile + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_DRIVER WHERE ID=" + id);
    }
    public DataTable GetALL()
    {
        return dm.ExecuteQuery(@"Select D.*, BC.CENTERNAME  from VD_DRIVER D
            INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=D.BUSINESSCENTERID ");
    }
    public bool CheckDuplicate(string staffid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_DRIVER WHERE STAFFID='" + staffid + "'");
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