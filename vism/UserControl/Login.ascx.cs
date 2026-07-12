using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Admin/Dashboard.aspx");
    }
}