using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVDVehicle
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVDVehicle()
    {
    }
    public int Insert(int buscenter, string chassisno, string regno, int seatcapacity, string vehicleMobile, string tagno)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_VEHICLE(ID,BUSINESSCENTERID,CHASSISNO,REGNO,SEATCAPACITY,VEHICLEMOBILE,TAGNO,CREATEDBY)
        VALUES(SQ_VD_VEHICLE.NEXTVAL,'" + buscenter + "','" + chassisno + "','" + regno + "','" + seatcapacity + "','" + vehicleMobile + "','" + tagno + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int buscenter, string chassisno, string regno, int seatcapacity, string vehicleMobile, string tagno)
    {
        return dm.ExecuteNonQuery("update VD_VEHICLE set BUSINESSCENTERID='" + buscenter + "',CHASSISNO='" + chassisno + "',REGNO='" + regno + "',SEATCAPACITY='" + seatcapacity + "',VEHICLEMOBILE='" + vehicleMobile + "',TAGNO='" + tagno + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_VEHICLE WHERE ID=" + id);
    }
    public DataTable GetALL()
    {
        return dm.ExecuteQuery(@"Select V.*, BC.CENTERNAME  from VD_VEHICLE V
            INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=V.BUSINESSCENTERID");
    }
    public bool CheckDuplicate(string chassisno)
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_VEHICLE WHERE CHASSISNO='" + chassisno + "'");
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