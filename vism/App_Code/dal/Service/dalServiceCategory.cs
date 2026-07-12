using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalServiceCategory
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalServiceCategory()
    {
    }
    public int Insert(string name, int ServiceCenterId)
    {
        return dm.ExecuteNonQuery(@"Insert into SC_CATEGORY(ID,CATEGORYNAME,SERVICECENTERID)
        VALUES(SQ_SC_CATEGORY.NEXTVAL,'" + name + "','" + ServiceCenterId + "')");
    }
    public int Update(int id, string name, int ServiceCenterId)
    {
        return dm.ExecuteNonQuery("update SC_CATEGORY set CATEGORYNAME='" + name + "',SERVICECENTERID='" + ServiceCenterId + "' where ID=" + id);
    }
    public DataTable GetServiceCenterId(int ServiceCenterId)
    {
        return dm.ExecuteQuery("Select * from SC_CATEGORY where SERVICECENTERID=" + ServiceCenterId);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from SC_CATEGORY C INNER JOIN SC_SERVICECENTER SC ON SC.ID=C.SERVICECENTERID where C.ID=" + id);
    }
    public bool CheckDuplicate(string name, int ServiceCenterId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_CATEGORY WHERE CATEGORYNAME='" + name + "' AND SERVICECENTERID=" + ServiceCenterId);
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