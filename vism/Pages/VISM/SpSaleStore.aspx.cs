using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_SpSaleStore : BasePage
{
    dalSPSales obj = new dalSPSales();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
           
            MessageController.Clear(Page);
            string url = "/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            BindSalesData();
        }
    }
    protected void BindSalesData()
    {

        rptSales.DataSource = obj.GetSPSalesStore();
        rptSales.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            obj.Delivered(ID);
            BindSalesData();
            MessageController.Show("Delivered", MessageType.Confirmation, Page);
        }
        catch (Exception ex)
        {
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }

    protected void btnPrint_Command(object sender, CommandEventArgs e)
    {
        string url = @"SpSalseInvoice.aspx?SalesID=" + e.CommandArgument.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }
}