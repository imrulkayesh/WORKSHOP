using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_MonthlyReportWorkShopWise : BasePage
{
    dalItem obj = new dalItem();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            ddlLoad();
        }
    }
    private void ddlLoad()
    {
        ListItem li = new ListItem("Select", "0");
        ddlWorkshop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        ddlWorkshop.DataBind();
        ddlWorkshop.Items.Insert(0, li);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(tbxfromdate.Text) && !string.IsNullOrEmpty(tbxtodate.Text) && !string.IsNullOrEmpty(ddlWorkshop.SelectedValue))
        {
            DataTable dt = obj.MonthlyReportWwise(Criteria());
            rpt.DataSource = dt;
            rpt.DataBind();
            lblfromDate.Text = tbxfromdate.Text;
            lbltoDate.Text = tbxtodate.Text;
        }
        else
        {
            MessageController.Show("Data Not Found !", MessageType.Warning, Page);
        }
    }
    private string Criteria()
    {
        string val = "";
        if (!string.IsNullOrEmpty(ddlWorkshop.SelectedValue) && ddlWorkshop.SelectedValue != "0")
        {
            val += " AND J.WORKSHOPID =" + ddlWorkshop.SelectedValue;
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(J.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " trunc(J.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text+ "','DD/MM/YYYY'))";
        }

        return val;
    }

    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }
}
