using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


public partial class Pages_VISM_MechanicsWorkHistory : BasePage
{
    dalGatein obj = new dalGatein();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
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
            LoadDDL();
            LoadDT();

            SelectWorkshop();
        }
    }

    private void SelectWorkshop()
    {
        try
        {
            string BI = SessionManager.SessionName.BusinessCentetid.ToString();
            ListItem li = new ListItem("Select", "");
            string qry = "";
            if (BI == "705")
            {
                qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
       SessionManager.SessionName.UserId + "')";
                ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
                ddlWorkShop.DataBind();

            }
            else
            {
                qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws  ORDER BY WORKSHOP ASC";
                ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
                ddlWorkShop.DataBind();
                ddlWorkShop.Items.Insert(0, li);
            }


        }
        catch
        {

            //
        }

    }

    private void LoadDDL()
    {
        ListItem li = new ListItem("All", "0");
        ddlServiceType.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlServiceType.DataBind();
        ddlServiceType.Items.Insert(0, li);
        //ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        //ddlWorkShop.DataBind();
        //ddlWorkShop.Items.Insert(0, li);

        string qry = @"Select ID,GROUPNAME text FROM RVS_GROUP";
        ddlgroup.DataSource = dm.ExecuteQuery(qry);
        ddlgroup.DataBind();
        ddlgroup.Items.Insert(0, li);


    }

    private void LoadDT()
    {

        string qry1 = @"select ID,TYPENAME TEXT from YA.RVS_DATETYPE";
        ddldatetype.DataSource = dm.ExecuteQuery(qry1);
        ddldatetype.DataBind();

    }

    protected void ddlWorkShop_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select ID,TEAMNAME TEXT from ya.RVS_TEAM where WORKSHOPID='" + ddlWorkShop.SelectedValue + "'";
        ddlteam.DataSource = dm.ExecuteQuery(qry);
        ddlteam.DataBind();
        ddlteam.Items.Insert(0, li);
        ddlteam.Enabled = true;
        // SessionManager.SessionName.RecvLoc = Convert.ToInt32(ddlRecvLoc.SelectedValue);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        rpt2.Visible = false;
        rpt.Visible = true;
        DataTable dt = null;
        if (ddldatetype.SelectedValue == "1")
        {
            dt = obj.GetWorkHistory(Criteria());
            //lblCompanyandLocation.Text = dt.Rows[0]["WORKSHOP"].ToString();
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else if (ddldatetype.SelectedValue == "2")
        {
            dt = obj.GetWorkHistory(CriteriaUpdate());
            //lblCompanyandLocation.Text = dt.Rows[0]["WORKSHOP"].ToString();
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {

        }

        if (ddlWorkShop.SelectedValue != "")
        {
            lblCompanyandLocation.Text = dt.Rows[0]["WORKSHOP"].ToString();
            lblCompanyandLocation.Visible = true;
        }
        else
        {
            lblCompanyandLocation.Visible = false;
        }
        lblfromDate.Text = tbxfromdate.Text;
        lbltoDate.Text = tbxtodate.Text;
    }
    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        rpt.Visible = false;
        rpt2.Visible = true;
        DataTable dt = null;
        if (ddldatetype.SelectedValue == "1")
        {
            dt = obj.GetWorkHistorywithoutproblem(Criteria());
            //lblCompanyandLocation.Text = dt.Rows[0]["WORKSHOP"].ToString();
            rpt2.DataSource = dt;
            rpt2.DataBind();
        }
        else if (ddldatetype.SelectedValue == "2")
        {
            dt = obj.GetWorkHistorywithoutproblem(CriteriaUpdate());
            //lblCompanyandLocation.Text = dt.Rows[0]["WORKSHOP"].ToString();
            rpt2.DataSource = dt;
            rpt2.DataBind();
        }
        else
        {

        }

        if (ddlWorkShop.SelectedValue != "")
        {
            lblCompanyandLocation.Text = dt.Rows[0]["WORKSHOP"].ToString();
            lblCompanyandLocation.Visible = true;
        }
        else
        {
            lblCompanyandLocation.Visible = false;
        }
        lblfromDate.Text = tbxfromdate.Text;
        lbltoDate.Text = tbxtodate.Text;
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
        if (!string.IsNullOrEmpty(ddlgroup.SelectedValue) && ddlgroup.SelectedValue != "0")
        {
            val += " AND J.GROUPID='" + ddlgroup.SelectedValue + "'";
        }
        if (tbxStaffID.Text.Length > 3)
        {
            val += " AND T.STAFFID='" + tbxStaffID.Text + "'";
        }
        if (!string.IsNullOrEmpty(ddlteam.SelectedValue) && ddlteam.SelectedValue != "0")
        {
            val += " AND TM.TEAMID='" + ddlteam.SelectedValue + "'";
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(J.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        else if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND trunc(J.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
        }

        return val;
    }

    private string CriteriaUpdate()
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
        if (!string.IsNullOrEmpty(ddlgroup.SelectedValue) && ddlgroup.SelectedValue != "0")
        {
            val += " AND J.GROUPID='" + ddlgroup.SelectedValue + "'";
        }
        if (tbxStaffID.Text.Length > 3)
        {
            val += " AND T.STAFFID='" + tbxStaffID.Text + "'";
        }
        if (!string.IsNullOrEmpty(ddlteam.SelectedValue) && ddlteam.SelectedValue != "0")
        {
            val += " AND TM.TEAMID='" + ddlteam.SelectedValue + "'";
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(J.UPDATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        else if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND trunc(J.UPDATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
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
    //protected void btnView_Command(object sender, CommandEventArgs e)
    //{
    //    string ID = e.CommandArgument.ToString();
    //    string url = @"JobCardView.aspx?JobID=" + ID.Trim();
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<script type = 'text/javascript'>");
    //    sb.Append("window.open('");
    //    sb.Append(url);
    //    sb.Append("');");
    //    sb.Append("</script>");
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    //}

    //protected void btnPI_Command(object sender, CommandEventArgs e)
    //{
    //    string ID = e.CommandArgument.ToString();
    //    string url = @"PreInspectionView.aspx?PIID=" + ID.Trim();
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<script type = 'text/javascript'>");
    //    sb.Append("window.open('");
    //    sb.Append(url);
    //    sb.Append("');");
    //    sb.Append("</script>");
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    //}

    //protected void btnPDI_Command(object sender, CommandEventArgs e)
    //{
    //    string ID = e.CommandArgument.ToString();
    //    string pdiNo = "";
    //    DataTable dt = obj.GetPDINo(ID);
    //    if (dt.Rows.Count > 0)
    //    {
    //        pdiNo = dt.Rows[0]["ID"].ToString();
    //        string url = @"PDIView.aspx?PDIID=" + pdiNo.Trim();
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("<script type = 'text/javascript'>");
    //        sb.Append("window.open('");
    //        sb.Append(url);
    //        sb.Append("');");
    //        sb.Append("</script>");
    //        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    //    }
    //    else
    //    {
    //        MessageController.Show("PDI Not Found", MessageType.Warning, Page);
    //    }
    //}

    //protected void btnBill_Command(object sender, CommandEventArgs e)
    //{
    //    string ID = e.CommandArgument.ToString();
    //    string url = @"PrintBill.aspx?JobID=" + ID.Trim();
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<script type = 'text/javascript'>");
    //    sb.Append("window.open('");
    //    sb.Append(url);
    //    sb.Append("');");
    //    sb.Append("</script>");
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    //}

    //protected void btnMoneyRecipt_Command(object sender, CommandEventArgs e)
    //{
    //    string ID = e.CommandArgument.ToString();
    //    string url = @"BillReport.aspx?JobID=" + ID.Trim();
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<script type = 'text/javascript'>");
    //    sb.Append("window.open('");
    //    sb.Append(url);
    //    sb.Append("');");
    //    sb.Append("</script>");
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    //}
}
