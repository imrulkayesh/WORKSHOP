using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PrintBill : BasePage
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
                lblJobNo.Text = dt.Rows[0]["ID"].ToString();
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["EFFECTIVEDATE"]).ToString("dd/MM/yyyy");
                lblCustName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                lblCustAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                lblOfficeAddress.Text = dt.Rows[0]["BADDRESS"].ToString();
                lblContactPerson.Text = dt.Rows[0]["BCONTACTPERSON"].ToString();
                lblContactPersonPhone.Text = dt.Rows[0]["BPHONE"].ToString();
                lblContactPersonMobile.Text = dt.Rows[0]["BMOBILE"].ToString();
                lblLpg.Text = dt.Rows[0]["CATNAME"].ToString();
                lblPhone.Text = dt.Rows[0]["PHONE"].ToString();
                lblMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                lblRegNo.Text = dt.Rows[0]["REGNO"].ToString();
                lblChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                lblYearofManuf.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
                lblMeterReading.Text = dt.Rows[0]["MILLAGE"].ToString();
                lblEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                lblBillNo.Text = dt.Rows[0]["ID"].ToString();
                lblSubTotal.Text = dt.Rows[0]["TOTALAMOUNT"].ToString();
                lblDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
                lblTotal.Text = dt.Rows[0]["NETAMOUNT"].ToString();
                int Total = string.IsNullOrEmpty(dt.Rows[0]["NETAMOUNT"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["NETAMOUNT"]);
                lblTakaInword.Text = dalCommon.NumberToText(Total);
                LoadCharges();
                LoadParts();
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
}