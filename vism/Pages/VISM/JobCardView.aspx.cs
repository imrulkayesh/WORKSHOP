using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_JobCardView : BasePage
{
    dalJobCard obj = new dalJobCard();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            if (SessionManager.SessionName == null)
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
                return;
            }
            else
            {
                JOBID = (int)0;
                if (Request.QueryString["JobID"] != null)
                {
                    JOBID = Convert.ToInt32(Request.QueryString["JobID"]);
                }
                else
                {
                    MessageController.Show("No Data Found", MessageType.Error, Page);
                }
            }
            DataTable dt = obj.GetJobCardPrint(JOBID);
            if (dt.Rows.Count > 0)
            {
                lblBranchAddress.Text = dt.Rows[0]["WADDRESS"].ToString();
                lblworkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
                lblJobNo.Text = dt.Rows[0]["JOBID"].ToString();
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["CREATEDATE"]).ToString("dd/MM/yyyy");
                lblCustName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                lblOfficeAddress.Text = dt.Rows[0]["BADDRESS"].ToString();
                lblDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
                lblDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
                lblLpg.Text = dt.Rows[0]["SERVICETYPE"].ToString();
                lblMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                lblRegNo.Text = dt.Rows[0]["REGNO"].ToString();
                lblChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                lblMeterReading.Text = dt.Rows[0]["MILLAGE"].ToString();
                lblEngineModuleNo.Text = dt.Rows[0]["MODELNAME"].ToString();
              
                lblcompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();
                lblSubTotal.Text = dt.Rows[0]["TOTALAMOUNT"].ToString();
                lblDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
                lblTotal.Text = dt.Rows[0]["NETAMOUNT"].ToString();
                int Total = string.IsNullOrEmpty(dt.Rows[0]["NETAMOUNT"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["NETAMOUNT"]);
                LoadCharges();
                LoadParts();
                LoadTechnician();
            }
            else
            {
                MessageController.Show("No Data Found", MessageType.Error, Page);
            }
        }
    }
    int JOBID
    {
        set { ViewState["JOBID"] = value; }
        get
        {
            try
            {
                return (int)ViewState["JOBID"];
            }
            catch
            {
                return 0;
            }
        }
    }
    private void LoadCharges()
    {

        rptCharge.DataSource = obj.GetChargesByJobID(JOBID);
        rptCharge.DataBind();
    }
    private void LoadParts()
    {
        rptParts.DataSource = obj.GetPartsByJobID(JOBID);
        rptParts.DataBind();
    }
    private void LoadTechnician()
    {
        rptTechs.DataSource = obj.GetTechBYJOBID(JOBID);
        rptTechs.DataBind();
    }
}