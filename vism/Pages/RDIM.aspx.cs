using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RDIM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Rdim"] != null && Request.QueryString["CenterId"] != null && Request.QueryString["GateNo"] != null)
        {
            Response.Write("0");
        }
        else
        {
            Response.Write("-1");
        }
    }
}