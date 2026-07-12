using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalBusinessCenter
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalBusinessCenter()
    {
    }
    public int Insert(string name, string code, string address, int locationid)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_BUSINESSCENTER(ID,CENTERNAME,CENTERCODE,ADDRESS,LOCATIONID,CREATEDBY)
        VALUES(SQ_BS_BUSINESSCENTER.NEXTVAL,'" + name + "','" + code + "','" + address + "'," + locationid + ",'" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, string code, string address, int locationid)
    {
        return dm.ExecuteNonQuery("update BS_BUSINESSCENTER set CENTERNAME='" + name + "',CENTERCODE='" + code + "',ADDRESS='" + address + "',LOCATIONID=" + locationid + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= SYSDATE where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select b.*,l.COUNTRYID from BS_BUSINESSCENTER b inner join BS_LOCATIONS l on l.ID=b.LOCATIONID where b.ID=" + id);
    }
    public DataTable GetByCountryId(int countryid)
    {
        return dm.ExecuteQuery("Select * from BS_LOCATIONS where COUNTRYID=" + countryid);
    }
    public DataTable GetByLocationId(int locationId)
    {
        return dm.ExecuteQuery("Select * from BS_BUSINESSCENTER where LOCATIONID=" + locationId);
    }
    public bool CheckDuplicate(string name, int ID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_BUSINESSCENTER WHERE CENTERNAME='" + name + "' and LOCATIONID=" + ID);
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