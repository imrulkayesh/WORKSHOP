
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalProfession
/// </summary>
public class dalProfession
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalProfession()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string name)
    {
        dm.AddParameteres("@Profession", name);
        DataTable dt = dm.ExecuteQuery("USP_Profession_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Profession", name);
        return dm.ExecuteNonQuery("USP_Profession_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Profession_GetById");
    }
}