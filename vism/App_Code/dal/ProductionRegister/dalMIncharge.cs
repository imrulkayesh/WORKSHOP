using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMIncharge
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMIncharge()
    {
    }
    public int Insert(int locationId, int businesscenter, int departmentId, int lineId, string staffid, string name, string mobile, string email)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MINCHARGE(ID,LOCATIONID,BUSINESSUNITID,DEPARTMENTID,LINEID,STAFFID,INCHARGENAME,MOBILE,EMAIL,CREATEDBY)
        VALUES(SQ_PR_MINCHARGE.NEXTVAL,'" + locationId + "','" + businesscenter + "','" + departmentId + "','" + lineId + "','" + staffid + "','" + name + "','" + mobile + "','" + email + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int locationId, int businesscenter, int departmentId, int lineId, string staffid, string name, string mobile, string email)
    {
        return dm.ExecuteNonQuery("update PR_MINCHARGE set LOCATIONID='" + locationId + "',BUSINESSUNITID='" + businesscenter + "',DEPARTMENTID='" + departmentId + "',LINEID='" + lineId + "', STAFFID='" + staffid + "',INCHARGENAME='" + name + "',MOBILE='" + mobile + "',EMAIL='" + email + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MINCHARGE WHERE ID=" + id);
    }

    public DataTable GetAllIncharge(int id)
    {
        return dm.ExecuteQuery(@"SELECT I.*, BS.CENTERNAME, L.LINENAME FROM PR_MINCHARGE I
        INNER JOIN BS_BUSINESSCENTER BS ON BS.ID = I.LOCATIONID
        INNER JOIN PR_MLINE L ON L.ID = I.LINEID WHERE I.LOCATIONID=" + id);
    }
    public bool CheckDuplicate(int locationId, int lineId, string staffid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MINCHARGE WHERE LOCATIONID='" + locationId + "' AND LINEID='" + lineId + "' AND STAFFID='" + staffid + "'");
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