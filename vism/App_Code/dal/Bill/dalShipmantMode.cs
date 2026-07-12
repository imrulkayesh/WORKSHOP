using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalShipmantMode
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalShipmantMode()
	{
	}
    public int Insert(string smmode)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_SHIPMENTMODE(ID,SHIPMENTMODE,CREATEDBY)
        VALUES(SQ_BIL_SHIPMENTMODE.NEXTVAL,'" + smmode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string smmode)
    {
        return dm.ExecuteNonQuery("update BIL_SHIPMENTMODE set SHIPMENTMODE='" + smmode + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_SHIPMENTMODE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string smmode)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_SHIPMENTMODE WHERE SHIPMENTMODE='" + smmode + "'");
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