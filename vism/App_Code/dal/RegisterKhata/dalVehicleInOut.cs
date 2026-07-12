using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVehicleInOut
{
    DatabaseManagerORCL dm = DatabaseManagerORCL.Instance;
	public dalVehicleInOut()
	{
		
	}
    public int Insert(int locationId, string vehicleNo, string gateNo, string driverName, string driverMobile, string inDate, string inTime, string outDate, string outTime, string Remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into RK_VEHICLEINOUT(ID,LOCATIONID,VEHICLENO,GATENO,DRIVERNAME,DRIVERMOBILE,INDATE,INTIME,OUTDATE,OUTTIME,REMARKS,CREATEDBY)
        VALUES(SQ_RK_VEHICLEINOUT.NEXTVAL,'" + locationId + "','" + vehicleNo + "','" + gateNo + "','" + driverName + "','" + driverMobile + "',TO_DATE('" + inDate + "','DD/MM/YYYY'),'" + inTime + "',TO_DATE('" + outDate + "','DD/MM/YYYY'),'" + outTime + "','" + Remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int locationId, string vehicleNo, string gateNo, string driverName, string driverMobile, string inDate, string inTime, string outDate, string outTime, string Remarks)
    {
        return dm.ExecuteNonQuery("update RK_VEHICLEINOUT set LOCATIONID='" + locationId + "',VEHICLENO='" + vehicleNo + "',GATENO='" + gateNo + "',DRIVERNAME='" + driverName + "',DRIVERMOBILE='" + driverMobile + "',INDATE=TO_DATE('" + inDate + "','DD/MM/YYYY'),INTIME='" + inTime + "',OUTDATE=TO_DATE('" + outDate + "','DD/MM/YYYY'),OUTTIME='" + outTime + "',REMARKS='" + Remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RK_VEHICLEINOUT WHERE ID=" + id);
    }

    public DataTable GetALL(int id)
    {
        return dm.ExecuteQuery(@"SELECT G.*, B.CENTERNAME FROM RK_VEHICLEINOUT G
        INNER JOIN BS_BUSINESSCENTER B ON B.ID = G.LOCATIONID
        WHERE G.ID>0 AND B.ID='" + id + "' ORDER BY G.CREATEDATE DESC");
    }
    public bool CheckDuplicate(string guestname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RK_VEHICLEINOUT WHERE GUESTNAME='" + guestname + "' ");
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