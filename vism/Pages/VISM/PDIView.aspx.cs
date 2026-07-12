using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PDIView : BasePage
{
    dalPDI obj = new dalPDI();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            if (SessionManager.SessionName == null)
            {
                Response.Redirect("~/login.aspx", false);
                return;
            }
            else
            {
                PDIID = (int)0;
                if (Request.QueryString["PDIID"] != null)
                {
                    PDIID = Convert.ToInt32(Request.QueryString["PDIID"]);
                }
                else
                {
                    // MessageController.Show("No Data Found", MessageType.Error, Page);
                }
            }
            DataTable dt = obj.GetPDIDetails(PDIID);
            if (dt.Rows.Count > 0)
            {
                lblBranchAddress.Text = dt.Rows[0]["WADDRESS"].ToString();
                lblworkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
                lblJobNo.Text = dt.Rows[0]["ID"].ToString();
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

                rptCharge.DataSource = obj.GetPDIItems(PDIID);
                rptCharge.DataBind();
            }
            else
            {
                //MessageController.Show("No Data Found", MessageType.Error, Page);
            }
        }
    }
    int PDIID
    {
        set { ViewState["PDIID"] = value; }
        get
        {
            try
            {
                return (int)ViewState["PDIID"];
            }
            catch
            {
                return 0;
            }
        }
    }
}