using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_AdminHeader : System.Web.UI.UserControl
{
    dalNotice objNotice = new dalNotice();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            //LoadNotice();
            lblUser.Text = SessionManager.SessionName.UserName;
            lblUserName.Text = SessionManager.SessionName.UserName;
        }

    }

    protected void LoadNotice()
    {
        if (Page.User.IsInRole("SystemAdmin") || Page.User.IsInRole("Admin"))
        {
            lblNotice.Visible = false;
            lblcount.Text = "no";
        }
        else if (Page.User.IsInRole("Student"))
        {
            //DataTable dt = objNotice.GetNoticeForStudent(Common.SessionInfo.StudentToClassId);
            //if (dt.Rows.Count > 0)
            //{
            //    lblcount.Text = dt.Rows.Count.ToString();
            //    lblNotice.InnerHtml = dt.Rows.Count.ToString();
            //    rptNotice.DataSource = dt;
            //    rptNotice.DataBind();
            //}
            //else
            //{
            //    lblcount.Text = "no";
            //    lblNotice.Visible = false;
            //}
        }
        else
        {
            //DataTable dt = objNotice.UnseenNoticeForTeacher(16);
            //if (dt.Rows.Count > 0)
            //{
            //    lblcount.Text = dt.Rows.Count.ToString();
            //    lblNotice.InnerHtml = dt.Rows.Count.ToString();
            //    rptNotice.DataSource = dt;
            //    rptNotice.DataBind();
            //}
            //else
            //{
            //    lblcount.Text = "no";
            //    lblNotice.Visible = false;
            //}

        }
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        SessionManager.SessionName = null;
        Response.Redirect("~/Login.aspx");
    }
}