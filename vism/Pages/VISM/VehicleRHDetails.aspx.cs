using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_VehicleRHDetails : BasePage
{
    dalGatein obj = new dalGatein();

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
        ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        ddlWorkShop.DataBind();
        ddlWorkShop.Items.Insert(0, li);
        ddlServiceType.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlServiceType.DataBind();
        ddlServiceType.Items.Insert(0, li);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = obj.VReceivingHistoryDetails(Criteria());
        rpt.DataSource = dt;
        rpt.DataBind();

        if (dt != null)
        {
            lblTotal.Text = "Total: " + dt.Rows.Count.ToString();
        }
        else
        {
            lblTotal.Text = "Total: 0";
        }
        lblfromDate.Text= tbxfromdate.Text;
        lbltoDate.Text= tbxtodate.Text;
    }
    private string Criteria()
    {
        string val = "";
        if (!string.IsNullOrEmpty(ddlWorkShop.SelectedValue) && ddlWorkShop.SelectedValue != "0")
        {
            val += " AND J.WORKSHOPID=" + ddlWorkShop.SelectedValue;
        }
       
        if (!string.IsNullOrEmpty(ddlServiceType.SelectedValue) && ddlServiceType.SelectedValue != "0")
        {
            val += " AND J.SERVICETYPEID='" + ddlServiceType.SelectedValue + "'";
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(PI.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        else if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND trunc(PI.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
        }

        return val;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }
}
