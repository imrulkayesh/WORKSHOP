using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;

public partial class Pages_RoleManagement_RoleSetup : BasePage
{
    dalRole objRole = new dalRole();
    public static string RoleId;
    public static string RoleName;
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
       
        if (!IsPostBack)
        {
            string url = "/Pages/UserManagement/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            Common.LoadDropdown(ddlPriority, "UC_ROLESETUP", 1, 0);
            BindData();
        }
    }
    int ID
    {
        set { ViewState["ID"] = value; }
        get
        {
            try
            {
                return Convert.ToInt32(ViewState["ID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (objRole.CheckDuplicate(tbxRole.Text))
        {
            try
            {
                int priority = new dalCommon().GetRolePriority(Convert.ToInt32(ddlPriority.SelectedValue)) + 1;
                ID = objRole.Insert(tbxRole.Text, tbxDescription.Text, Convert.ToInt32(ddlPriority.SelectedValue), priority);
                BindData();
                Common.LoadDropdown(ddlPriority, "UC_ROLESETUP", 1, 0);
                ClearAll();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

            }
        }
        else
        {
            MessageController.Show("This Role already exists. Please try anothor.", MessageType.Error, Page);
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (objRole.CheckDuplicate(tbxRole.Text))
        {

            try
            {
                int priority = new dalCommon().GetRolePriority(Convert.ToInt32(ddlPriority.SelectedValue)) + 1;
                int value = objRole.Update(ID, tbxRole.Text, tbxDescription.Text, Convert.ToInt32(ddlPriority.SelectedValue), priority);
                BindData();
                Common.LoadDropdown(ddlPriority, "UC_ROLESETUP", 1, 0);
                ClearAll();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
            }
        }
        else
        {
            MessageController.Show("This role name already exists. Please try another name.", MessageType.Information, Page);
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("UC_ROLESETUP", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
        BindData();
        Common.LoadDropdown(ddlPriority, "RoleSetup", 1, 0);
    }
    protected void BindData()
    {
        DataTable dt = objRole.GetAllRole();
        if (dt.Rows.Count > 0)
        {
            rptRole.DataSource = dt;
            rptRole.DataBind();
        }
        else
        {
            rptRole.DataSource = null;
            rptRole.DataBind();
        }
    }

    protected void ClearAll()
    {
        tbxRole.Text = "";
        tbxDescription.Text = "";
        ddlPriority.SelectedValue = "";
        btnEdit.Visible = false;
        btnSave.Visible = true;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument.ToString());
        DataTable dt = objRole.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxRole.Text = dt.Rows[0]["ROLENAME"].ToString();
            tbxDescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
            ddlPriority.SelectedValue = dt.Rows[0]["PARRENTID"].ToString();
        }
        btnEdit.Visible = true;
        btnSave.Visible = false;
    }
}