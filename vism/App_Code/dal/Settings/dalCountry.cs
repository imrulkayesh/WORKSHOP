using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalCountry
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCountry()
    {
    }
    public int Insert(string name, string countrycode)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_COUNTRY(ID,COUNTRYNAME,COUNTRYCODE)
        VALUES(SQ_BS_COUNTRY.NEXTVAL,'" + name + "','" + countrycode + "')");
    }
    public int Update(int id, string name, string countrycode)
    {
        return dm.ExecuteNonQuery("update BS_COUNTRY set COUNTRYNAME='" + name + "',COUNTRYCODE='" + countrycode + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_COUNTRY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_COUNTRY WHERE COUNTRYNAME='" + name + "'");
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