using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_SPSales : BasePage
{
    dalSPSales obj = new dalSPSales();
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
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            LoadDDL();
            SalesId = CheckSales();
            WorkShopId = string.IsNullOrEmpty(ddlWorkshop.SelectedValue) ? 0 : Convert.ToInt32(ddlWorkshop.SelectedValue);
            BindData();
            BindSalesData();

        }
    }
    int SalesId
    {
        set { ViewState["SalesId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["SalesId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    int WorkShopId
    {
        set { Cache["WorkShopId"] = value; }
        get
        {
            try
            {
                return (int)Cache["WorkShopId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    private int CheckSales()
    {
        int a = obj.CheckSalesId();

        return a;
    }
    private void LoadDDL()
    {
        ddlWorkshop.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlWorkshop.DataBind();
        ListItem li = new ListItem("Walk In Customer", "0");
        ddlCurrency.DataSource = new Common().GetAll("ASSET_CURRENCY");
        ddlCurrency.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int sid = SalesId;
            obj.Update(SalesId, Convert.ToInt32(ddlWorkshop.SelectedValue), tbxCustomer.Text.Trim(), Convert.ToDecimal(tbxtotalAmount.Text.Trim()), Convert.ToDecimal(tbxDiscount.Text.Trim()), Convert.ToDecimal(tbxNetAmount.Text.Trim()), Convert.ToInt32(ddlCurrency.SelectedValue));
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            SalesId = 0;
            CheckSales();
            BindData();

            string url = @"SpSalseInvoice.aspx?SalesID=" + sid.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        catch { }
    }
    protected void ClearAll()
    {
        tbxParts.Text = "";
        tbxUnitPrice.Text = "";
        tbxTotal.Text = "";
    }
    protected void BindData()
    {
        DataTable dt = obj.GetSPItems(SalesId);
        rpt.DataSource = dt;
        rpt.DataBind();
        decimal amt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            amt += Convert.ToDecimal(dr["TOTALPRICE"]);
        }
        tbxtotalAmount.Text = amt.ToString();
        tbxNetAmount.Text = (amt - (string.IsNullOrEmpty(tbxDiscount.Text) ? 0 : Convert.ToDecimal(tbxDiscount.Text))).ToString();
    }
    protected void BindSalesData()
    {
        if (!string.IsNullOrEmpty(WorkShopId.ToString()))
        {
            rptSales.DataSource = obj.GetSPSalesbyWorkshop(Convert.ToInt32(WorkShopId));
        }
        else
        {
            rptSales.DataSource = null;
        }
        rptSales.DataBind();
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_SPSALESITEMS", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

    protected void tbxParts_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] parts = tbxParts.Text.Trim().Split('#');
            hdnPartsId.Value = parts[0];
            tbxUnitPrice.Text = parts[3];
            tbxTotal.Text = parts[3];
        }
        catch
        {
            MessageController.Show("Please select Parts", MessageType.Error, Page);
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchParts(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT S.ID||'#'||S.PARTSCODE||'#'||S.PARTSNAME||'#'||S.SELLINGPRICE PARTS FROM RVS_SPAREPARTS S
                WHERE S.WORKSHOPID='" + HttpContext.Current.Cache["WorkShopId"] + "' AND LOWER(S.ID||'#'||S.PARTSCODE||'#'||S.PARTSNAME||'#'||S.SELLINGPRICE) LIKE LOWER('%" + prefixText + "%')  AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["PARTS"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        WorkShopId = Convert.ToInt32(ddlWorkshop.SelectedValue);
        BindSalesData();
    }

    protected void btnAddParts_Click(object sender, EventArgs e)
    {
        try
        {
            obj.InsertParts(SalesId, Convert.ToInt32(hdnPartsId.Value), Convert.ToDecimal(tbxQuanity.Text), Convert.ToDecimal(tbxUnitPrice.Text), Convert.ToDecimal(tbxTotal.Text));
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            BindData();
            BindSalesData();
            ClearAll();
            hdnPartsId.Value = "0";
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);
        }
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        SalesId = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(SalesId);
        BindData();
        if (dt.Rows.Count > 0)
        {
            tbxCustomer.Text = dt.Rows[0]["CUSTOMER"].ToString();
            tbxNetAmount.Text = dt.Rows[0]["NETAMOUNT"].ToString();
            tbxDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
            tbxtotalAmount.Text = dt.Rows[0]["TOTALAMOUNT"].ToString();
            ddlCurrency.SelectedValue = dt.Rows[0]["CURRENCYID"].ToString();
        }
    }

    protected void btnPrint_Command(object sender, CommandEventArgs e)
    {
        string url = @"SpSalseInvoice.aspx?SalesID=" + e.CommandArgument.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void tbxDiscount_TextChanged(object sender, EventArgs e)
    {
        tbxNetAmount.Text = (Convert.ToDecimal(tbxtotalAmount.Text.Trim()) - Convert.ToDecimal(tbxDiscount.Text.Trim())).ToString();
    }

    protected void tbxQuanity_TextChanged(object sender, EventArgs e)
    {
        Total();
    }
    protected void Total()
    {
        decimal qty = string.IsNullOrEmpty(tbxQuanity.Text) ? 0 : Convert.ToDecimal(tbxQuanity.Text);
        decimal unitPrice = string.IsNullOrEmpty(tbxUnitPrice.Text) ? 0 : Convert.ToDecimal(tbxUnitPrice.Text);
        tbxTotal.Text = (qty * unitPrice).ToString();
    }
    protected void tbxUnitPrice_TextChanged(object sender, EventArgs e)
    {
        Total();
    }
}