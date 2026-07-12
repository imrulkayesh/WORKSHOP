
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalThana
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalThana()
    {
    }
    public int Insert(string name, int districtId)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_THANA(ID,THANANAME,DISTRICTID)
        VALUES(SQ_BS_THANA.NEXTVAL,'" + name + "','" + districtId + "')");
    }
    public int Update(int id, string name, int districtId)
    {
        return dm.ExecuteNonQuery("update BS_THANA set THANANAME='" + name + "',DISTRICTID=" + districtId + " where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select t.*,d.DIVISIONID,dv.COUNTRYID from BS_THANA t inner join BS_DISTRICT d on d.ID=t.DISTRICTID inner join BS_DIVISION dv on dv.ID=d.DIVISIONID where t.ID=" + id);
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
        return dm.ExecuteQuery("Select * from BS_THANA where DISTRICTID='" + id + "' ORDER BY THANANAME ASC");
    }
    public bool CheckDuplicate(string name, int ID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_THANA WHERE THANANAME='" + name + "' and DISTRICTID=" + ID);
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