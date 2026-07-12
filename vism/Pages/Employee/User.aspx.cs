using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Employee_User : BasePage
{
    dalLocationWiseReport obj = new dalLocationWiseReport();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);

        if (!IsPostBack)
        {
            string url = "/Pages/Employee/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (!string.IsNullOrEmpty(tbxStaffID.Text))
            {
                DataTable dt = obj.GetUserInfomationt(tbxStaffID.Text);
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                MessageController.Show("Staff ID not found !",MessageType.Error,Page);
            }
        }
        catch (Exception ex)
        {
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
