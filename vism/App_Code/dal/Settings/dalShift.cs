
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalShift
/// </summary>
public class dalShift
{
	public dalShift()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name)
    {
        dm.AddParameteres("@Shift", name);
        DataTable dt = dm.ExecuteQuery("USP_Shift_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Shift", name);
        return dm.ExecuteNonQuery("USP_Shift_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Shift_GetById");
    }
}