using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalRptService
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalRptService()
    {
    }
    public DataTable ServiceRequest()
    {
        string query = "SELECT * FROM VW_SERVICEREQUEST WHERE ID>0";

        return dm.ExecuteQuery(query);
    }
}