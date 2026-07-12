using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_SpSalseInvoice : BasePage
{
    dalSPSales obj = new dalSPSales();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.SessionName == null)
        {
            Response.Redirect("~/login.aspx", false);
            return;
        }
        else
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["SalesID"]))
                {
                    BindData(Convert.ToInt32(Request.QueryString["SalesID"]));
                }
            }
            catch
            {

            }
        }
    }
    private void BindData(int SalesId)
    {
        DataTable dt = obj.GetById(SalesId);
        if (dt.Rows.Count > 0)
        {
            lblworkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
            lblBranchAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            lblCustomer.Text = "Customer: " + dt.Rows[0]["CUSTOMER"].ToString();
            lblDate.Text = "Date: " + dt.Rows[0]["SELLDATE"].ToString();
            lblTotal.Text = dt.Rows[0]["TOTALAMOUNT"].ToString() + " " + dt.Rows[0]["CURRENCYCODE"].ToString();
            lblDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString() + " " + dt.Rows[0]["CURRENCYCODE"].ToString();
            lblNetAmount.Text = dt.Rows[0]["NETAMOUNT"].ToString() + " " + dt.Rows[0]["CURRENCYCODE"].ToString();

            rpt.DataSource = obj.GetSPItems(SalesId);
            rpt.DataBind();
        }
    }
}