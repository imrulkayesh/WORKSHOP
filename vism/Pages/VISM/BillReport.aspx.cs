using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_BillReport : BasePage
{
    dalVISMBill obj = new dalVISMBill();
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
            DataTable dt = obj.GetByJobID(JOBID);
            if (dt.Rows.Count > 0)
            {
                lblBillDate.Text = Convert.ToDateTime(dt.Rows[0]["CREATEDATE"]).ToString("dd/MM/yyyy");
                lblPrintDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblBranchAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                lblworkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
                lblJobNo.Text = dt.Rows[0]["JOBID"].ToString();
                lblPayType.Text = dt.Rows[0]["PAYTYPE"].ToString();
                lblTotalAmount.Text = dt.Rows[0]["TOTALAMOUNT"].ToString();
                lblDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
                lblNetAmount.Text = dt.Rows[0]["NETAMOUNT"].ToString();
                lblCoustomerPaid.Text = dt.Rows[0]["CUSTOMERPAID"].ToString();
                lblCompanyPaid.Text = dt.Rows[0]["COMPANYPAID"].ToString();
                lblPaidCompany.Text = dt.Rows[0]["EMPPAID"].ToString();
                lblEmployeePaid.Text = dt.Rows[0]["PAIDBYEMP"].ToString();
                lblCurrency.Text = dt.Rows[0]["CURRENCYNAME"].ToString();
                lblRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                lblPreparedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
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
}