using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_News : BasePage
{
    dalNews obj = new dalNews();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ID = obj.Insert(tbxName.Text, tbxShortDescription.Text, tbxDetails.Content, Convert.ToDateTime(tbxDate.Text), Page.User.Identity.Name);
        if (ID != -1)
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        else
            MessageController.Show("This news already exists in selected date. Please try anothor title.", MessageType.Error, Page);

        BindData();
        ClearAll();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        obj.Update(ID, tbxName.Text, tbxShortDescription.Text, tbxDetails.Content, Convert.ToDateTime(tbxDate.Text), Page.User.Identity.Name);
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
        tbxDate.Text = "";
        tbxShortDescription.Text = "";
        tbxDetails.Content = string.Empty;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("bs_News");
        rptYear.DataSource = dt;
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["Title"].ToString();
            tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString("MM/dd/yyyy");
            tbxShortDescription.Text = dt.Rows[0]["ShortDescription"].ToString();
            tbxDetails.Content = dt.Rows[0]["Details"].ToString();

        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        ID = Convert.ToInt32(e.CommandArgument);
        new Common().Delete("bs_News", ID);
        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        BindData();
    }
}