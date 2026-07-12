using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

/// <summary>
/// Summary description for dalSMS
/// </summary>
public class dalSMS
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalSMS()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(string name, string variable, string message)
    {
        dm.AddParameteres("@Name",name);
        dm.AddParameteres("@Variable", variable);
        dm.AddParameteres("@Message", message);
        DataTable dt = dm.ExecuteQuery("USP_SMSTemplete_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(int id, string name, string variable, string message)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@Variable", variable);
        dm.AddParameteres("@Message", message);
        return dm.ExecuteNonQuery("USP_SMSTemplete_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id",id);
        return dm.ExecuteQuery("USP_SMSTemplete_GetById");
    }
}