using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_UserManagement_Module : BasePage
{
    dalModule obj = new dalModule();
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
            ViewState["ID"] = (int)0;
            BindData();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string icon = Request.Form[tbxIconPicker.UniqueID];
        int ID = obj.Insert(tbxName.Text, Convert.ToInt32(tbxOrderColumn.Text), tbxURL.Text, icon);

        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

        BindData();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string icon = Request.Form[tbxIconPicker.UniqueID];
        obj.Update(Convert.ToInt32(ViewState["ID"]), tbxName.Text, Convert.ToInt32(tbxOrderColumn.Text), tbxURL.Text, icon);
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxName.Text = "";
        tbxURL.Text = "";
        btnEdit.Visible = false;
        btnSave.Visible = true;
    }
    protected void BindData()
    {
        rptTask.DataSource = new Common().GetAll("UC_MODULE");
        rptTask.DataBind();

    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["MODULENAME"].ToString();
            tbxURL.Text = dt.Rows[0]["URL"].ToString();
            tbxIconPicker.Text = dt.Rows[0]["ICON"].ToString();
            tbxOrderColumn.Text = dt.Rows[0]["MODULEORDER"].ToString();
            ViewState["ID"] = ID;
        }
        btnEdit.Visible = true;
        btnSave.Visible = false;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("UC_MODULE", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}