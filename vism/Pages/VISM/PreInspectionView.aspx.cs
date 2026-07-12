using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PreInspectionView : BasePage
{
    dalPreInspection obj = new dalPreInspection();
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
                PIID = (int)0;
                if (Request.QueryString["PIID"] != null)
                {
                    PIID = Convert.ToInt32(Request.QueryString["PIID"]);
                }
                else
                {
                    // MessageController.Show("No Data Found", MessageType.Error, Page);
                }
            }
            DataTable dt = obj.GetPIDetails(PIID);
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
                lblPreparedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
                lblcompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();
                rptCharge.DataSource = obj.GetInspectionItems(PIID,2);
                rptCharge.DataBind();
            }
            else
            {
                //MessageController.Show("No Data Found", MessageType.Error, Page);
            }
        }
    }
    int PIID
    {
        set { ViewState["PIID"] = value; }
        get
        {
            try
            {
                return (int)ViewState["PIID"];
            }
            catch
            {
                return 0;
            }
        }
    }
}