using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_VehicleReceivingHistory : BasePage
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
        DataTable dt = obj.ReceivingDetails(Criteria());
        rpt.DataSource = dt;
        rpt.DataBind();
        lblfromDate.Text = tbxfromdate.Text;
        lbltoDate.Text = tbxtodate.Text;
        if (dt != null)
        {
            lblTotal.Text = "Total: " + dt.Rows.Count.ToString();
        }
        else
        {
            lblTotal.Text = "Total: 0";
        }

    }
    private string Criteria()
    {
        string val = "";
        if (!string.IsNullOrEmpty(ddlWorkShop.SelectedValue) && ddlWorkShop.SelectedValue != "0")
        {
            val += " AND P.WORKSHOPID=" + ddlWorkShop.SelectedValue;
        }
        if (!string.IsNullOrEmpty(ddlStatus.SelectedValue) && ddlStatus.SelectedValue != "10")
        {
            if (ddlStatus.SelectedValue == "0")
            {
                val += " AND P.ISOPEN=0 AND J.ISOPEN=0";
            }
            else if (ddlStatus.SelectedValue == "1")
            {
                val += " AND P.ISOPEN=1 AND J.ISOPEN=1";
            }
            else if (ddlStatus.SelectedValue == "2")
            {
                val += " AND P.ISOPEN=1 AND J.ID is null";
            }
            else if (ddlStatus.SelectedValue == "3")
            {
                val += " AND P.ISOPEN=0 AND J.ID is null";
            }
            else
            {
                val += " AND P.ISOPEN=" + ddlStatus.SelectedValue;
            }
        }
        if (!string.IsNullOrEmpty(ddlServiceType.SelectedValue) && ddlServiceType.SelectedValue != "0")
        {
            val += " AND P.SERVICETYPEID='" + ddlServiceType.SelectedValue + "'";
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(P.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        else if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND trunc(P.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
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
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"JobCardView.aspx?JobID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnPI_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"PreInspectionView.aspx?PIID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnPDI_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string pdiNo = "";
        DataTable dt = obj.GetPDINo(ID);
        if (dt.Rows.Count > 0)
        {
            pdiNo = dt.Rows[0]["ID"].ToString();
            string url = @"PDIView.aspx?PDIID=" + pdiNo.Trim();
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        else
        {
            MessageController.Show("PDI Not Found", MessageType.Warning, Page);
        }
    }

    protected void btnBill_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"PrintBill.aspx?JobID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnMoneyRecipt_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"BillReport.aspx?JobID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }
}
