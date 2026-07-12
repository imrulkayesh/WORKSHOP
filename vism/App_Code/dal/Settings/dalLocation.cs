using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLocation
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalLocation()
	{
    }
    public int Insert(string name, int countryid,int thanaid,string timeZone,string locationcode)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_LOCATIONS(ID,NAME,COUNTRYID,THANAID,TIMEZONE,LOCATIONCODE)
        VALUES(SQ_BS_LOCATIONS.NEXTVAL,'" + name + "'," + countryid + "," + thanaid + ",'" + timeZone + "','" + locationcode + "')");
    }
    public int Update(int id, string name, int countryid, int thanaid, string timeZone, string locationcode)
    {
        return dm.ExecuteNonQuery("update BS_LOCATIONS set NAME='" + name + "',COUNTRYID=" + countryid + ",THANAID=" + thanaid + ",TIMEZONE='" + timeZone + "',LOCATIONCODE='" + locationcode + "' where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select l.*,t.DISTRICTID,d.DIVISIONID from BS_LOCATIONS l inner join BS_THANA t on t.ID=l.THANAID inner join BS_DISTRICT d on d.ID=t.DISTRICTID where l.ID=" + id);
    }
    public DataTable GetByDivisionId(int divisionid)
    {
        return dm.ExecuteQuery("Select * from BS_DISTRICT where DIVISIONID=" + divisionid);
    }
    public DataTable GetByCountryId(int countryid)
    {
        return dm.ExecuteQuery("Select * from BS_DIVISION where COUNTRYID=" + countryid);
    }
    public DataTable GetByDistrictId(int id)
    {
        return dm.ExecuteQuery("Select * from BS_THANA where DISTRICTID=" + id);
    }
    public DataTable GetByThanaId(int id)
    {
        return dm.ExecuteQuery("Select * from BS_LOCATIONS where THANAID=" + id);
    }
    public bool CheckDuplicate(string name, int ID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_LOCATIONS WHERE NAME='" + name + "' and THANAID=" + ID);
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