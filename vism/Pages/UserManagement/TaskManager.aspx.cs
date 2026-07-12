using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_UserManagement_TaskManager : BasePage
{
    dalTaskManager objTask = new dalTaskManager();
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
            LoadModule();
            GetParent();
            BindData();
        }

    }
    private void LoadModule()
    {
        ddlModule.DataSource = new Common().GetAll("UC_MODULE");
        ddlModule.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string icon = Request.Form[tbxIconPicker.UniqueID];
        int ID = objTask.Insert(tbxTitleBan.Text, tbxTitleEng.Text, tbxURL.Text, Convert.ToInt32(ddlParent.SelectedValue), Convert.ToInt32(tbxOrderColumn.Text), icon, Convert.ToInt32(ddlModule.SelectedValue));

        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

        BindData();
        GetParent();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string icon = Request.Form[tbxIconPicker.UniqueID];
        objTask.Update(Convert.ToInt32(ViewState["ID"]), tbxTitleBan.Text, tbxTitleEng.Text, tbxURL.Text, Convert.ToInt32(ddlParent.SelectedValue), Convert.ToInt32(tbxOrderColumn.Text), icon, Convert.ToInt32(ddlModule.SelectedValue));
        MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        BindData();
        GetParent();
        ClearAll();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void GetParent()
    {
        DataTable dt = objTask.GetParanet(SessionManager.SessionName.RoleId, Convert.ToInt32(ddlModule.SelectedValue));
        ddlParent.Items.Clear();
        ListItem li = new ListItem("Root", "0");
        ddlParent.Items.Add(li);
        foreach (DataRow row in dt.Rows)
        {
            ListItem item = new ListItem();
            item.Text = row["TextEng"].ToString();
            item.Value = (row["Id"].ToString());
            ddlParent.Items.Add(item);

        }
    }
    protected void ClearAll()
    {
        tbxTitleEng.Text = "";
        tbxTitleBan.Text = "";
        tbxURL.Text = "";
        btnEdit.Visible = false;
        btnSave.Visible = true;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlModule.SelectedValue))
        {
            DataTable dt = objTask.GetAllTask(Convert.ToInt32(ddlModule.SelectedValue));
            rptTask.DataSource = dt;
        }
        else
        {
            rptTask.DataSource = null;
        }
        rptTask.DataBind();

    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = objTask.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxTitleEng.Text = dt.Rows[0]["TextEng"].ToString();
            tbxTitleBan.Text = dt.Rows[0]["TextBan"].ToString();
            tbxURL.Text = dt.Rows[0]["URL"].ToString();
            ddlParent.SelectedValue = dt.Rows[0]["ParentId"].ToString();
            tbxIconPicker.Text = dt.Rows[0]["Icon"].ToString();
            tbxOrderColumn.Text = dt.Rows[0]["ORDERCOLUMN"].ToString();
            ddlModule.SelectedValue = dt.Rows[0]["MODULEID"].ToString();
            ViewState["ID"] = ID;
        }
        btnEdit.Visible = true;
        btnSave.Visible = false;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("UC_TaskManager", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetParent();
        BindData();
    }
}