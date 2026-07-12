using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PreInspectionPrint : BasePage
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
                PreInspectionID = (int)0;
                if (Request.QueryString["PreInspectionID"] != null)
                {
                    PreInspectionID = Convert.ToInt32(Request.QueryString["PreInspectionID"]);
                }
                else
                {
                    // MessageController.Show("No Data Found", MessageType.Error, Page);
                }
            }
            DataTable dt = obj.GetPreInspectionCardPrint(PreInspectionID);
            if (dt.Rows.Count > 0)
            {
                lblBranchAddress.Text = dt.Rows[0]["WADDRESS"].ToString();
                lblworkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
                lblPreInsID.Text = dt.Rows[0]["PIID"].ToString();
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["CREATEDATE"]).ToString("dd/MM/yyyy") + "- Time :" + Convert.ToDateTime(dt.Rows[0]["CREATEDATE"]).ToString("hh:mm tt");
                //lblUpdateDate.Text = Convert.ToDateTime(dt.Rows[0]["UPDATEDATE"]).ToString("dd/MM/yyyy") + "- Time :" + Convert.ToDateTime(dt.Rows[0]["UPDATEDATE"]).ToString("HH:MM");
                lblCustName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                // lblOfficeAddress.Text = dt.Rows[0]["BADDRESS"].ToString();
                lblStaffId.Text = dt.Rows[0]["DRIVERID"].ToString();

                lblDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
                lblDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
                lblLpg.Text = dt.Rows[0]["SERVICETYPE"].ToString();
                lblMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                lblRegNo.Text = dt.Rows[0]["REGNO"].ToString();
                lblChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                lblMeterReading.Text = dt.Rows[0]["MILLAGE"].ToString();
                lblEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                lblEngineModuleNo.Text = dt.Rows[0]["MODELNAME"].ToString();
                lblcompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();
                lblPreparedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
                lblgroup.Text = dt.Rows[0]["GROUPNAME"].ToString();
                lblRemark.Text = dt.Rows[0]["REMARKS"].ToString();

                rptCharge.DataSource = new dalPreInspection().GetProblems(Convert.ToInt32(dt.Rows[0]["PIID"]));
                rptCharge.DataBind();
            }
            else
            {
                //MessageController.Show("No Data Found", MessageType.Error, Page);
            }
        }
    }
    int PreInspectionID
    {
        set { ViewState["PreInspectionID"] = value; }
        get
        {
            try
            {
                return (int)ViewState["PreInspectionID"];
            }
            catch
            {
                return 0;
            }
        }
    }
}