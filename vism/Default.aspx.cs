using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(SessionManager.SessionName.StaffId))
        {
            Response.Redirect("~/Pages/Admin/Default.aspx");
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }
}