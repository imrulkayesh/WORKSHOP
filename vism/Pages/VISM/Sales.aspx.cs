using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Sales : BasePage
{
    dalSales obj = new dalSales();
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
            ViewState["SalesId"] = (int)0;
            LoadDdl();
            CheckSales();
        }

    }
    private void LoadDdl()
    {
        ddlCurrency.DataSource = new Common().GetAll("ASSET_CURRENCY");
        ddlCurrency.DataBind();
    }
    protected void chkOwnerType_CheckedChanged(object sender, EventArgs e)
    {
        pnlJointOwner.Visible = false;
        if (chkOwnerType.Checked)
        {
            pnlJointOwner.Visible = true;
        }
    }
    private void CheckSales()
    {
        ViewState["SalesId"] = obj.CheckSales();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int JointOwner = 0;
            if (chkOwnerType.Checked)
            {
                JointOwner = 1;
            }
            decimal Paid = string.IsNullOrEmpty(tbxPaidAmount.Text) ? 0 : Convert.ToDecimal(tbxPaidAmount.Text);
            decimal price = string.IsNullOrEmpty(tbxPrice.Text) ? 0 : Convert.ToDecimal(tbxPrice.Text);
            obj.UpdateSales(Convert.ToInt32(ViewState["SalesId"]), Convert.ToInt32(hdnVehicleId.Value), price, Convert.ToInt32(ddlCurrency.SelectedValue), Paid, dalCommon.DateFormatYYYYMMDD(tbxNextPayDate.Text), dalCommon.DateFormatYYYYMMDD(tbxSellingDate.Text), JointOwner);
            obj.InsertOwner(Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(hdnOwnerId1.Value), tbxBillingAddress1.Text, tbxContactPerson1.Text, tbxBillingMobile1.Text, tbxContactPhone1.Text, tbxContactFax1.Text, tbxContactEmail.Text, Convert.ToInt32(ViewState["SalesId"]));
            if (JointOwner == 1)
            {
                obj.InsertOwner(Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(hdnOwnerId2.Value), tbxBillingAddress2.Text, tbxContactPerson2.Text, tbxBillingMobile2.Text, tbxContactPhone2.Text, tbxContactFax2.Text, tbxContacEmail2.Text, Convert.ToInt32(ViewState["SalesId"]));
            }
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            ClearAll();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

        }

    }
    private void ClearAll()
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchVehicle(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT CHASSISNO FROM RVS_VEHICLE where CHASSISNO like '" + prefixText + "%' AND ID NOT IN(SELECT VEHICLEID FROM RVS_SALES) AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["CHASSISNO"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomer(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT ID||'-'||CUSTOMERNAME CUSTOMERNAME FROM RVS_CUSTOMER WHERE (CUSTOMERNAME LIKE '" + prefixText + "%' OR ID LIKE '" + prefixText + "%' OR MOBILE LIKE '" + prefixText + "%')  AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["CUSTOMERNAME"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void tbxOwnerName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] a = tbxOwnerName.Text.Trim().Split('-');
            DataTable dt = new dalVISMCustomer().GetById(Convert.ToInt32(a[0]));
            if (dt.Rows.Count > 0)
            {
                tbxOwnerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                tbxOwnerMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                hdnOwnerId1.Value = a[0];
            }
            else
            {
                tbxOwnerName.Text = "";
                hdnOwnerId1.Value = "0";
            }
        }
        catch
        {
            tbxOwnerName.Text = "";
            hdnOwnerId1.Value = "0";
        }
    }

    protected void tbxOwner2Name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] a = tbxOwner2Name.Text.Trim().Split('-');
            DataTable dt = new dalVISMCustomer().GetById(Convert.ToInt32(a[0]));
            if (dt.Rows.Count > 0)
            {
                tbxOwner2Name.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                tbxOwnerMobile2.Text = dt.Rows[0]["MOBILE"].ToString();
                hdnOwnerId2.Value = a[0];
            }
            else
            {
                tbxOwner2Name.Text = "";
                hdnOwnerId2.Value = "0";
            }
        }
        catch
        {
            tbxOwner2Name.Text = "";
            hdnOwnerId2.Value = "0";
        }
    }

    protected void tbxChassisNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new dalVehicle().GetByChassisNo(tbxChassisNo.Text);
            if (dt.Rows.Count > 0)
            {
                hdnVehicleId.Value = dt.Rows[0]["ID"].ToString();
                tbxBrand.Text = dt.Rows[0]["BRANDNAME"].ToString();
                tbxModel.Text = dt.Rows[0]["MODELNAME"].ToString();
                tbxManufacturer.Text = dt.Rows[0]["MANUFACTURERNAME"].ToString();
                tbxPrice.Text = dt.Rows[0]["SELLINGPRICE"].ToString();
                ddlCurrency.SelectedValue = dt.Rows[0]["SELLINGCURRENCYID"].ToString();
            }
            else
            {
                tbxChassisNo.Text = "";
                hdnVehicleId.Value = "0";
            }
        }
        catch
        {
            tbxChassisNo.Text = "";
            hdnVehicleId.Value = "0";
        }

    }
}