using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Default : BasePage
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

            //DataTable dtModule = objTask.GetModule(SessionManager.SessionName.RoleId);
            //DataTable dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, Convert.ToInt32(dtModule.Rows[0]["ID"]));
            //SessionManager.SessionName.TaskList = dtTaskList;
            //SessionManager.SessionName.ModuleList = dtModule;

            rptModule.DataSource = objTask.GetModule(SessionManager.SessionName.RoleId);
            rptModule.DataBind();
        }
    }
    protected void btnModule_Command(object sender, CommandEventArgs e)
    {
        try
        {
            
            int ID = Convert.ToInt32(e.CommandArgument);
            SessionManager.SessionName.ModuleId = ID;
            DataTable dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, ID);
            var pathUrl = dtTaskList.Rows[0]["MURL"].ToString();
            //SessionManager.SessionName.TaskList = dtTaskList;
            Response.Redirect("~" + pathUrl);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
        }
    }
}