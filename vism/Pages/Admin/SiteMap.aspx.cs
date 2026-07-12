using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SiteMap : BasePage
{
    dalTaskManager objTask = new dalTaskManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/Admin/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            LoadParent();
        }
    }
    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        Session["RedirectFrom"] = null;
        Response.Redirect("~/Default.aspx", true);
    }
    protected void LoadParent()
    {
        DataTable dt = objTask.GetParanet(SessionManager.SessionName.RoleId,SessionManager.SessionName.ModuleId);
        if (dt.Rows.Count > 0)
        {
            rptParent.DataSource = dt;
            rptParent.DataBind();
        }
    }
    protected void rptCategory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rptChild = (Repeater)e.Item.FindControl("rptChild");
        HiddenField hdnValue = (HiddenField)e.Item.FindControl("hdnValue");
        DataTable dt = objTask.GetChild(Convert.ToInt32(hdnValue.Value), SessionManager.SessionName.RoleId);
        if (dt.Rows.Count > 0)
        {
            rptChild.DataSource = dt;
            rptChild.DataBind();
        }
    }
}