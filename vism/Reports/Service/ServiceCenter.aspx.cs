using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Service_ServiceCenter : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Reports/Service/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            BindData();
            Page.Title = "Service Summary- " + DateTime.Now.ToString("MMMM yyyy");
        }

    }

    private void BindData()
    {
        rpt.DataSource = new Common().GetAll("VW_SERVICECENTER");
        rpt.DataBind();
    }
}