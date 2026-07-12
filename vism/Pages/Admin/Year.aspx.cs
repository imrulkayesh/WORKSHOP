using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Year : BasePage
{
    dalYear objYear = new dalYear();
    protected static int ID;
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
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
           

            BindData();
        }

    }
    protected bool GetDefault()
    {
        bool flag;
        DataTable dt = objYear.GetDefault();
        if (dt.Rows.Count > 0)
            flag = true;
        else
            flag = false;
        return flag;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkDefault.Checked && GetDefault() == true)
        {
            MessageController.Show("A year already set as default. Only one year can be set as default year. Please unchecked the box.", MessageType.Error, Page);
            return;
        }
        ID = objYear.Insert(tbxYear.Text, chkDefault.Checked);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This year already exists. Please try anothor.", MessageType.Error, Page);
        BindData();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        objYear.Update(ID, tbxYear.Text, chkDefault.Checked);
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
        tbxYear.Text = "";
        chkDefault.Checked = false;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("bs_Year");
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = objYear.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxYear.Text = dt.Rows[0]["Year"].ToString();
            chkDefault.Checked = dt.Rows[0]["IsDefault"].ToString() == "True" ? true : false;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("bs_Year", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}