using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_NewsUpdate : System.Web.UI.UserControl
{
    dalNews obj = new dalNews();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
            LoadData();
    }
    protected void LoadData()
    {
        DataTable dt = obj.GetLatest();
        rptNews.DataSource = dt;
        rptNews.DataBind();
    }
}