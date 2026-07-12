
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalDivision
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDivision()
    {
    }
    public int Insert(string name, int countryid)
    {
        return dm.ExecuteNonQuery(@"Insert into BS_DIVISION(ID,DIVISIONNAME,COUNTRYID)
        VALUES(SQ_BS_DIVSION.NEXTVAL,'" + name + "','" + countryid + "')");
    }
    public int Update(int id, string name, int countryid)
    {
        return dm.ExecuteNonQuery("update BS_DIVISION set DIVISIONNAME='" + name + "',COUNTRYID=" + countryid + " where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BS_DIVISION where ID=" + id);
    }
    public DataTable GetByCountryId(int countryid)
    {
        return dm.ExecuteQuery("Select * from BS_DIVISION where COUNTRYID='" + countryid+"' ORDER BY DIVISIONNAME ASC" );
    }
    public bool CheckDuplicate(string name, int ID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BS_DIVISION WHERE DIVISIONNAME='" + name + "' and COUNTRYID=" + ID);
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