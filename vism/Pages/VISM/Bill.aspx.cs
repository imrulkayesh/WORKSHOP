using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Bill : BasePage
{
    dalVISMBill obj = new dalVISMBill();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + System.IO.Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            JOBID = (int)0;
            PIID = (int)0;
            LoadDDL();
            BindData();
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
    private void LoadDDL()
    {
        ListItem li = new ListItem("Select", "0");
        ddlCompany.DataSource = new Common().GetAll("BS_COMPANY");
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, li);
        ddlPayType.DataSource = new Common().GetAll("RVS_PAYTYPE");
        ddlPayType.DataBind();
        ddlCurrency.DataSource = new Common().GetAll("ASSET_CURRENCY");
        ddlCurrency.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (JOBID > 0)
        {

            try
            {
                int isOpen = 1;
                decimal empPaid = string.IsNullOrEmpty(tbxEmpPaid.Text) ? 0 : Convert.ToDecimal(tbxEmpPaid.Text);
                decimal companyPaid = string.IsNullOrEmpty(tbxCompanyPaid.Text) ? 0 : Convert.ToDecimal(tbxCompanyPaid.Text);
                decimal custPaid = string.IsNullOrEmpty(tbxPaid.Text) ? 0 : Convert.ToDecimal(tbxPaid.Text);
                decimal totalPaid = empPaid + companyPaid + custPaid;
                decimal total = string.IsNullOrEmpty(tbxTotal.Text) ? 0 : Convert.ToDecimal(tbxTotal.Text);
                decimal discount = 0;
                string dis = tbxDiscount.Text;
                if (dis.Contains("%"))
                {
                    dis = dis.Replace("%", "");
                    if (Convert.ToDecimal(dis) >= 100)
                    {
                        MessageController.Show("Discount percentage should be less than 100", MessageType.Error, Page);
                        return;
                    }
                    else
                    {
                        discount = (total * Convert.ToDecimal(dis)) / 100;
                    }
                }
                else if (!string.IsNullOrEmpty(tbxDiscount.Text))
                {
                    discount = Convert.ToDecimal(tbxDiscount.Text);
                }
                decimal netAmount = total - discount;
                if (netAmount == totalPaid)
                {
                    isOpen = 0;
                }
                obj.Update(JOBID, isOpen, totalPaid, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(ddlPayType.SelectedValue), discount,
                    netAmount, custPaid, companyPaid, Convert.ToInt32(ddlCompany.SelectedValue),
                    empPaid, tbxEmpStaffID.Text, tbxremark.Text);
                if (isOpen == 0)
                {
                    new dalPreInspection().ClosePI(PIID);
                }
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Confirmation, Page);
                BindData();
                ClearAll();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

            }
        }
        else
        {
            MessageController.Show("No Open Bill Found", MessageType.Warning, Page);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxJobID.Text = "";
        tbxremark.Text = "";
        PIID = (int)0;
        JOBID = (int)0;
    }
    protected void BindData()
    {
        DataTable dt = obj.GetAllBILL();
        rpt.DataSource = dt;
        rpt.DataBind();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        JOBID = Convert.ToInt32(e.CommandArgument);
        tbxJobID.Text = JOBID.ToString();
        LoadBill();
    }
    private void LoadBill()
    {
        DataTable dt = obj.GetById(JOBID);
        if (dt.Rows.Count > 0)
        {
            tbxTotal.Text = dt.Rows[0]["TOTALAMOUNT"].ToString();
            tbxDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
            tbxNetAmount.Text = dt.Rows[0]["NETAMOUNT"].ToString();
            tbxPaid.Text = dt.Rows[0]["CUSTOMERPAID"].ToString();
            tbxCompanyPaid.Text = dt.Rows[0]["COMPANYPAID"].ToString();
            tbxEmpPaid.Text = dt.Rows[0]["EMPPAID"].ToString();
            tbxEmpStaffID.Text = dt.Rows[0]["PAIDBYEMP"].ToString();
            tbxremark.Text = dt.Rows[0]["REMARKS"].ToString();
            PIID = string.IsNullOrEmpty(dt.Rows[0]["PIID"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["PIID"]);
            try
            {
                ddlCurrency.SelectedValue = dt.Rows[0]["CURRENCYID"].ToString();
            }
            catch
            {
                ddlCurrency.SelectedIndex = 0;
            }
            try
            {
                ddlPayType.SelectedValue = dt.Rows[0]["PAYTYPEID"].ToString();
            }
            catch
            {
                ddlPayType.SelectedIndex = 0;
            }
        }
        else
        {
            MessageController.Show("No Open Bill Found", MessageType.Warning, Page);
            return;
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_BILL", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

    protected void tbxJobID_TextChanged(object sender, EventArgs e)
    {
        string b = tbxJobID.Text;
        string[] a = tbxJobID.Text.Trim().Split('-');
        try
        {
            JOBID = string.IsNullOrEmpty(a[0]) ? 0 : Convert.ToInt32(a[0]);
        }
        catch
        {
            JOBID = (int)0;
        }
        LoadBill();
        tbxJobID.Text = b;
    }

    protected void tbxDiscount_TextChanged(object sender, EventArgs e)
    {
        decimal discount = 0;
        decimal total = string.IsNullOrEmpty(tbxTotal.Text) ? 0 : Convert.ToDecimal(tbxTotal.Text);
        string dis = tbxDiscount.Text;
        if (dis.Contains("%"))
        {
            dis = dis.Replace("%", "");
            if (Convert.ToDecimal(dis) >= 100)
            {
                MessageController.Show("Discount percentage should be less than 100", MessageType.Error, Page);
                return;
            }
            else
            {
                discount = (total * Convert.ToDecimal(dis)) / 100;
            }
        }
        else if (!string.IsNullOrEmpty(tbxDiscount.Text))
        {
            discount = Convert.ToDecimal(tbxDiscount.Text);
        }
        tbxDiscount.Text = discount.ToString();
        decimal netAmount = total - discount;
        tbxNetAmount.Text = netAmount.ToString();
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchJob(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT J.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME JOBNO FROM RVS_JOBCARD J
                INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
                INNER JOIN RVS_BILL B ON B.JOBID=J.ID
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(J.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%')) AND B.ISOPEN=1 AND W.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "'  AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["JOBNO"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }


    protected void btnView_Command(object sender, CommandEventArgs e)
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

    protected void btnPrint_Command(object sender, CommandEventArgs e)
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