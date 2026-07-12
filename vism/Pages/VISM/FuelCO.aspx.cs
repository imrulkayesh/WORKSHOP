using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class Pages_VISM_FuelCO : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    dalFuelType obj = new dalFuelType();
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
            ViewState["ID"] = (int)0;
            BindData();
        }

    }

    private void BindData()
    {
        DataTable dt = new Common().GetAllAF(SessionManager.SessionName.UserId);
        rpt.DataSource = dt;
        rpt.DataBind();
    }


    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string ID = e.CommandArgument.ToString();

            ImageButton btn = (ImageButton)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox tbxfgqty = (TextBox)item.FindControl("tbxfgqty");
            obj.UpdateFG(ID, tbxfgqty.Text.Trim());
            MessageController.Show("Updated", MessageType.Confirmation, Page);
            BindData();

        }

        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }

    protected void imgbtnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_FUELDMD", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
}