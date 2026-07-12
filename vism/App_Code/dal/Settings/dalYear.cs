using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

/// <summary>
/// Summary description for dalYear
/// </summary>
public class dalYear
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalYear()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string year,bool isDefault)
    {
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@IsDefault", isDefault);
        DataTable dt= dm.ExecuteQuery("USP_Year_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id,string year, bool isDefault)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@IsDefault", isDefault);
        return dm.ExecuteNonQuery("USP_Year_Update");
    }

    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Year_GetById");
    }

    public DataTable GetDefault()
    {
        return dm.ExecuteQuery("USP_Year_GetDefault");
    }
}