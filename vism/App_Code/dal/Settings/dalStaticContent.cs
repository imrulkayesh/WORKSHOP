
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalStaticContent
/// </summary>
public class dalStaticContent
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalStaticContent()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int update(string pageName, string content)
    {
        dm.AddParameteres("@PageName",pageName);
        dm.AddParameteres("@Content", content);
        return dm.ExecuteNonQuery("USP_StaticContent_Update");
    }

    public DataTable GetByPageName(string pageName)
    {
        dm.AddParameteres("@PageName", pageName);
        return dm.ExecuteQuery("USP_StaticContent_GetByPageName");
    }
}