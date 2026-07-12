using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_UserManagement_ChangePassword : BasePage
{
    dalUser obj = new dalUser();
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
            if (Request.QueryString["UserId"] != null)
            {
                UserId = Request.QueryString["UserId"].ToString();
            }
            else
            {
                if (Common.SessionInfo != null)
                {
                    UserId = Common.SessionInfo.UserName;
                }

            }
            tbxResetUser.Text = UserId;
        }

    }

    string UserId
    {
        set { ViewState["UserId"] = value; }
        get
        {
            try
            {
                return ViewState["UserId"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (tbxPass.Text == tbxConPass.Text)
        {
            if (UserId != "")
            {
                obj.ChangePassword(UserId, Encryption.EncryptionWithSh(tbxPass.Text));
                MessageController.Show("Password change successfully", MessageType.Information, Page);
               // Response.Redirect("~/Pages/Admin/Default.aspx");
            }
        }
        else
        {
            MessageController.Show("Password and confirm password does not mathch", MessageType.Error, Page);
        }
    }
}