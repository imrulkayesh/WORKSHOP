using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Service_ServiceRequest : BasePage
{
    dalRptService obj = new dalRptService();
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
        }

    }
    private void BindData()
    {
        rpt.DataSource = obj.ServiceRequest();
        rpt.DataBind();
    }
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        long ID = Convert.ToInt32(e.CommandArgument);
        SessionManager.SessionName.RequestId = ID;
        Response.Redirect("~/Pages/Service/View.aspx");
    }

    protected void ddlBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlServiceCenter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

    }
}