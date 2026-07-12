
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Section
/// </summary>
public class dalSection
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalSection()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string name)
    {
        dm.AddParameteres("@Section", name);
        return dm.ExecuteNonQuery("USP_Section_Insert");
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Section", name);
        return dm.ExecuteNonQuery("USP_Section_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Section_GetById");
    }
}