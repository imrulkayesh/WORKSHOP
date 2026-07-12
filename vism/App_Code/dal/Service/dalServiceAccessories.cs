using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalServiceAccessories
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalServiceAccessories()
    {
    }
    public int Insert(int categoryid, string name, int stock, int businesscenterid, int servicecenterid)
    {
        return dm.ExecuteNonQuery(@"Insert into SC_ACCESSORIES(ID,CATEGORYID,ACCESSORIESNAME,STOCK,BUSINESSCENTERID,SERVICECENTERID,CREATEDBY)
        VALUES(SQ_SC_ACCESSORIES.NEXTVAL,'" + categoryid + "','" + name + "','" + stock + "','" + businesscenterid + "','" + servicecenterid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int categoryid, string name, int stock, int businesscenterid, int servicecenterid)
    {
        return dm.ExecuteNonQuery("update SC_ACCESSORIES set CATEGORYID='" + categoryid + "',ACCESSORIESNAME='" + name + "',STOCK='" + stock + "',BUSINESSCENTERID='" + businesscenterid + "',SERVICECENTERID='" + servicecenterid + "',UPDATEDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from SC_ACCESSORIES where ID=" + id);
    }
    public DataTable GetByServiceCenterId(int ServiceCenterId)
    {
        return dm.ExecuteQuery(@"Select AC.*,B.CENTERCODE,S.SERVICECENTERNAME,C.ACCESSORIESCATEGORY from SC_ACCESSORIES AC INNER JOIN BS_BUSINESSCENTER B ON B.ID=AC.BUSINESSCENTERID INNER JOIN SC_SERVICECENTER S
ON S.ID=AC.SERVICECENTERID INNER JOIN SC_ACCESSORIESCATEGORY C ON C.ID=AC.CATEGORYID where AC.SERVICECENTERID=" + ServiceCenterId);
    }
    public bool CheckDuplicate(string name, int categoryid, int ServiceCenterId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_ACCESSORIES WHERE ACCESSORIESNAME='" + name + "'AND SERVICECENTERID=" + ServiceCenterId + " AND CATEGORYID=" + categoryid);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public DataTable GetByServiceCenterAndCategory(int CategoryId)
    {
        return dm.ExecuteQuery(@"Select AC.* from SC_ACCESSORIES AC INNER JOIN 
        SC_SERVICETEAM ST ON ST.SERVICECENTERID=AC.SERVICECENTERID WHERE AC.CATEGORYID=" + CategoryId + " AND ST.SUPERVISOR='" + SessionManager.SessionName.StaffId + "'");
    }
}