using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PrintApplication : BasePage
{
    dalVISMApprover obj = new dalVISMApprover();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            BindData();
        }
    }

    private void BindData()
    {
        rptApproval.DataSource = obj.GetBRTAList(1);
        rptApproval.DataBind();
        rptRejected.DataSource = obj.GetBRTAList(2);
        rptRejected.DataBind();
    }

    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            obj.BRTAApproval(ID, 1);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);
        }
    }

    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        SessionManager.SessionName.VehicleId = ID;
        Response.Redirect("~/Pages/VISM/PrintBRTAFrom.aspx");
    }

    protected void btnRejectedView_Command(object sender, CommandEventArgs e)
    {

    }
}