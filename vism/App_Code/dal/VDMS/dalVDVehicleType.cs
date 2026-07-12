using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalVDVehicleType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalVDVehicleType()
	{
	}
    public int Insert(string vehicletype, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_VEHICLETYPE(ID,VEHICLETYPE,REMARKS,CREATEDBY)
        VALUES(SQ_VD_VEHICLETYPE.NEXTVAL,'" + vehicletype + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string vehicletype, string remarks)
    {
        return dm.ExecuteNonQuery("update VD_VEHICLETYPE set VEHICLETYPE='" + vehicletype + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_VEHICLETYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string vehicletype)
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_VEHICLETYPE WHERE VEHICLETYPE='" + vehicletype + "'");
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