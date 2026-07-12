
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalDashbord
/// </summary>
public class dalDashbord
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalDashbord()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int GetStudent()
    {
        DataTable dt= dm.ExecuteQuery("USP_Dashboard_GetStudent");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int GetTeacher()
    {
        DataTable dt = dm.ExecuteQuery("USP_Dashboard_GetTeacher");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int GetUniqueVisitor()
    {
        DataTable dt = dm.ExecuteQuery("USP_Dashboard_GetUniqueVisit");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public DataTable UpdateUniqueVisitor()
    {
        return dm.ExecuteQuery("USP_Dashboard_UpdateUniqueVisit");
    }
}