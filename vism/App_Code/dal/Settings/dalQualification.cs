
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalQualification
/// </summary>
public class dalQualification
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalQualification()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string name)
    {
        dm.AddParameteres("@Qualification", name);
        DataTable dt = dm.ExecuteQuery("USP_Qualification_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Qualification", name);
        return dm.ExecuteNonQuery("USP_Qualification_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Qualification_GetById");
    }
}