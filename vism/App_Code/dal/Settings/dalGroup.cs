
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalGroup
/// </summary>
public class dalGroup
{
	public dalGroup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name)
    {
        dm.AddParameteres("@Group", name);
        return dm.ExecuteNonQuery("USP_Group_Insert");
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Group", name);
        return dm.ExecuteNonQuery("USP_Group_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Group_GetById");
    }
}