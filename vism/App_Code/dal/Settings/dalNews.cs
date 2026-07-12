
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNews
/// </summary>
public class dalNews
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalNews()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(string title, string shorDescription, string details, DateTime date, string createdBy)
    {
        dm.AddParameteres("@Title", title);
        dm.AddParameteres("@ShortDescription", shorDescription);
        dm.AddParameteres("@Details", details);
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@CreatedBy", createdBy);
        return dm.ExecuteNonQuery("USP_News_Insert");
    }
    public int Update(int id, string title, string shorDescription, string details, DateTime date, string updatedBy)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Title", title);
        dm.AddParameteres("@ShortDescription", shorDescription);
        dm.AddParameteres("@Details", details);
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        return dm.ExecuteNonQuery("USP_News_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_News_GetById");
    }

    public DataTable GetLatest()
    {
        return dm.ExecuteQuery("USP_News_GetLatest");
    }
}