using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    public static string nm()
    {
        string clientMachineName;
        clientMachineName =
            (Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName);
        return clientMachineName;
    }
    [WebMethod]
    public static string DetermineCompName()
    {
        string clientMachineName;
        clientMachineName =
            (Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName);
        DataSet dt = new DataSet();
        dt.Tables.Add("Table");
        dt.Tables[0].Columns.Add("name");
        dt.Tables[0].Rows.Add(clientMachineName);
        return dt.GetXml();
    }
}