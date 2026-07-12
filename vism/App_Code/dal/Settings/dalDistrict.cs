
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalDistrict
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDistrict()
    {
    }
    public int Insert(string name, int divisionid)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_DISTRICT(ID,DISTRICTNAME,DIVISIONID)
        VALUES(SQ_BS_DISTRICT.NEXTVAL,'" + name + "','" + divisionid + "')");
    }
    public int Update(int id, string name, int divisionid)
    {
        return dm.ExecuteNonQuery("update BS_DISTRICT set DISTRICTNAME='" + name + "',DIVISIONID=" + divisionid + " where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select d.*,dv.COUNTRYID from BS_DISTRICT d inner join BS_DIVISION dv on dv.ID=d.DIVISIONID where d.ID=" + id);
    }
    public DataTable GetByDivisionId(int divisionid)
    {
        return dm.ExecuteQuery("Select * from BS_DISTRICT where DIVISIONID='" + divisionid+"'ORDER BY DISTRICTNAME ASC");
    }
    public DataTable GetByCountryId(int countryid)
    {
        return dm.ExecuteQuery("Select * from BS_DIVISION where COUNTRYID=" + countryid);
    }
    public bool CheckDuplicate(string name, int ID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_DISTRICT WHERE DISTRICTNAME='" + name + "' and DIVISIONID=" + ID);
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