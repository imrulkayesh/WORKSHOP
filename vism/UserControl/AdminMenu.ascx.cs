using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_AdminMenu : System.Web.UI.UserControl
{
    dalTaskManager objTask = new dalTaskManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.SessionName.RoleId>0)
        {
            //if (SessionManager.SessionName.ModuleId != 1000000)
            //{
                lblUser.Text = SessionManager.SessionName.UserName;
                LoadParent();
                //SessionManager.SessionName.ModuleId = 1000000;
           // }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }
    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        Session["RedirectFrom"] = null;
        Response.Redirect("~/Login.aspx", true);
    }
    protected void LoadParent()
    {
        DataTable dt = objTask.GetParanet(SessionManager.SessionName.RoleId, SessionManager.SessionName.ModuleId);
        rptParent.DataSource = dt;
        rptParent.DataBind();


    }
    protected void rptCategory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rptChild = (Repeater)e.Item.FindControl("rptChild");
        HiddenField hdnValue = (HiddenField)e.Item.FindControl("hdnValue");
        DataTable dt = objTask.GetChild(Convert.ToInt32(hdnValue.Value), SessionManager.SessionName.RoleId);
        rptChild.DataSource = dt;
        rptChild.DataBind();
    }

}