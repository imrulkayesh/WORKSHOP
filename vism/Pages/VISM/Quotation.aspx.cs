using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Quotation : BasePage
{
    dalVISMQuotation obj = new dalVISMQuotation();
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
            try
            {
                BookingID = (int)0;

                LoadDDL();
                tbxBookingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                LoadCharges();
                LoadParts();
                BindData();
            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
        }

    }
    int BookingID
    {
        set { ViewState["BookingID"] = value; }
        get
        {
            try
            {
                return (int)ViewState["BookingID"];
            }
            catch
            {
                return 0;
            }
        }
    }
    private void LoadDDL()
    {
        ddlCategory.DataSource = new Common().GetAll("RVS_SERVICECATEGORY");
        ddlCategory.DataBind();
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();
        ddlCurrency.DataSource = new Common().GetAll("ASSET_CURRENCY");
        ddlCurrency.DataBind();
    }
    private void LoadCharges()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue) && !string.IsNullOrEmpty(ddlCategory.SelectedValue))
        {
            rptCharges.DataSource = obj.GetCharges(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), 0);
            rptCharges.DataBind();
        }
        if (BookingID > 0)
        {
            DataTable dt = obj.GetCharges(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), BookingID);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem r in rptCharges.Items)
                {
                    HiddenField ChargeId = (HiddenField)r.FindControl("hdnChargeId");
                    if (ChargeId.Value == dr["ID"].ToString())
                    {
                        CheckBox chk = (CheckBox)r.FindControl("chkrow");
                        TextBox qty = (TextBox)r.FindControl("tbxChargeQty");
                        TextBox tbxChargeAmount = (TextBox)r.FindControl("tbxChargeAmount");
                        TextBox tbxChargeRemarks = (TextBox)r.FindControl("tbxChargeRemarks");
                        qty.Text = dr["QUANTITY"].ToString();
                        tbxChargeAmount.Text = dr["AMOUNT"].ToString();
                        tbxChargeRemarks.Text = dr["REMARKS"].ToString();
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    private void LoadParts()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue) && !string.IsNullOrEmpty(ddlCategory.SelectedValue))
        {
            rptParts.DataSource = obj.GetParts(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), 0);
            rptParts.DataBind();
        }
        if (BookingID > 0)
        {
            DataTable dt = obj.GetParts(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), BookingID);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem r in rptParts.Items)
                {
                    HiddenField hdnPartsId = (HiddenField)r.FindControl("hdnPartsId");
                    if (hdnPartsId.Value == dr["ID"].ToString())
                    {
                        CheckBox chk = (CheckBox)r.FindControl("chkrow");
                        TextBox tbxPartsQty = (TextBox)r.FindControl("tbxPartsQty");
                        TextBox tbxPartsAmount = (TextBox)r.FindControl("tbxPartsAmount");
                        TextBox tbxPartsRemarks = (TextBox)r.FindControl("tbxPartsRemarks");
                        tbxPartsQty.Text = dr["QUANTITY"].ToString();
                        tbxPartsAmount.Text = dr["AMOUNT"].ToString();
                        tbxPartsRemarks.Text = dr["REMARKS"].ToString();
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            decimal totalAmount = 0;
            decimal total = string.IsNullOrEmpty(tbxTotal.Text) ? 0 : Convert.ToDecimal(tbxTotal.Text);
            decimal discount = string.IsNullOrEmpty(tbxDiscount.Text) ? 0 : Convert.ToDecimal(tbxDiscount.Text);
            decimal bookingMoney = string.IsNullOrEmpty(tbxBookingMoney.Text) ? 0 : Convert.ToDecimal(tbxBookingMoney.Text);
            decimal netAmount = total - discount;
            if (btnSave.Text == "Save")
            {
                BookingID = obj.Insert(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(tbxVehicle.Text), Convert.ToInt32(ddlCategory.SelectedValue),
                    dalCommon.DateFormatYYYYMMDD(tbxExpireDate.Text), bookingMoney, total, discount,
                    netAmount, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(chkConfirm.Checked), tbxremark.Text);
                if (BookingID > 0)
                {
                    totalAmount = InsertCharge(BookingID);
                    totalAmount += InsertParts(BookingID);
                }
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
                ClearAll();
            }
            else
            {
                if (BookingID > 0)
                {
                    obj.Update(BookingID, Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(tbxVehicle.Text), Convert.ToInt32(ddlCategory.SelectedValue),
                    dalCommon.DateFormatYYYYMMDD(tbxExpireDate.Text), bookingMoney, total, discount,
                    netAmount, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(chkConfirm.Checked), tbxremark.Text);
                    totalAmount = InsertCharge(BookingID);
                    totalAmount += InsertParts(BookingID);
                    MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
                    BindData();
                    ClearAll();
                }
                else
                {
                    MessageController.Show("Please select a Quotation from the list", MessageType.Error, Page);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show("Something is wrong.", MessageType.Error, Page);
        }
    }
    private decimal InsertCharge(int BookingID)
    {
        obj.DeleteCharges(BookingID);
        decimal amt = 0;
        foreach (RepeaterItem r in rptCharges.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField ChargeId = (HiddenField)r.FindControl("hdnChargeId");
                TextBox qty = (TextBox)r.FindControl("tbxChargeQty");
                TextBox tbxChargeAmount = (TextBox)r.FindControl("tbxChargeAmount");
                TextBox tbxChargeRemarks = (TextBox)r.FindControl("tbxChargeRemarks");
                decimal qtys = Convert.ToDecimal(qty.Text);
                decimal amts = Convert.ToDecimal(tbxChargeAmount.Text);
                obj.InsertCharges(BookingID, Convert.ToInt32(ChargeId.Value), qtys, qtys * amts, tbxChargeRemarks.Text);
                amt += amts * qtys;
            }
        }
        return amt;
    }
    private decimal InsertParts(int BookingID)
    {
        obj.DeleteParts(BookingID);
        decimal amt = 0;
        foreach (RepeaterItem r in rptParts.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField hdnPartsId = (HiddenField)r.FindControl("hdnPartsId");
                TextBox tbxPartsQty = (TextBox)r.FindControl("tbxPartsQty");
                TextBox tbxPartsAmount = (TextBox)r.FindControl("tbxPartsAmount");
                TextBox tbxPartsRemarks = (TextBox)r.FindControl("tbxPartsRemarks");
                decimal qtys = Convert.ToDecimal(tbxPartsQty.Text);
                decimal amts = Convert.ToDecimal(tbxPartsAmount.Text);
                obj.InsertParts(BookingID, Convert.ToInt32(hdnPartsId.Value), qtys, qtys * amts, tbxPartsRemarks.Text);
                amt += amts * qtys;
            }
        }
        return amt;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxremark.Text = "";
        BookingID = (int)0;
        tbxTotal.Text = "";
        LoadCharges();
        LoadParts();
        btnSave.Text = "Save";
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue) && !string.IsNullOrEmpty(ddlCategory.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
            rpt.DataSource = dt;
        }
        else
        {
            rpt.DataSource = null;
        }
        rpt.DataBind();
    }


    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        BookingID = ID;
        LoadData();
    }
    private void LoadData()
    {
        DataTable dt = obj.GetById(BookingID);
        if (dt.Rows.Count > 0)
        {
            tbxVehicle.Text = dt.Rows[0]["VEHICLEID"].ToString();
            ddlCategory.SelectedValue = dt.Rows[0]["SERVICECATID"].ToString();
            ddlworkshopid.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
            tbxTotal.Text = dt.Rows[0]["TOTALAMOUNT"].ToString();
            tbxExpireDate.Text = Convert.ToDateTime(dt.Rows[0]["EXPIREDATE"]).ToString("dd/MM/yyyy");
            tbxDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
            tbxBookingMoney.Text = dt.Rows[0]["BOOKINGMONEY"].ToString();
            chkConfirm.Checked = Convert.ToBoolean(dt.Rows[0]["ISCONFIRM"]);
            tbxremark.Text = dt.Rows[0]["REMARKS"].ToString();
            tbxBookingDate.Text = Convert.ToDateTime(dt.Rows[0]["BOOKINGDATE"]).ToString("dd/MM/yyyy");
            ddlCurrency.SelectedValue = dt.Rows[0]["CURRENCYID"].ToString();
            LoadCharges();
            LoadParts();
            btnSave.Text = "Update";
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_QUOTATION", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

    protected void ddlcategoryid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCharges();
        LoadParts();
        BindData();
    }

    protected void ddlworkshopid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCharges();
        LoadParts();
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        decimal amt = 0;
        foreach (RepeaterItem r in rptParts.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField hdnPartsId = (HiddenField)r.FindControl("hdnPartsId");
                TextBox tbxPartsQty = (TextBox)r.FindControl("tbxPartsQty");
                TextBox tbxPartsAmount = (TextBox)r.FindControl("tbxPartsAmount");
                TextBox tbxPartsRemarks = (TextBox)r.FindControl("tbxPartsRemarks");
                decimal qtys = Convert.ToDecimal(tbxPartsQty.Text);
                decimal amts = Convert.ToDecimal(tbxPartsAmount.Text) * qtys;
                amt += amts;
            }
        }

        foreach (RepeaterItem r in rptCharges.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField ChargeId = (HiddenField)r.FindControl("hdnChargeId");
                TextBox qty = (TextBox)r.FindControl("tbxChargeQty");
                TextBox tbxChargeAmount = (TextBox)r.FindControl("tbxChargeAmount");
                TextBox tbxChargeRemarks = (TextBox)r.FindControl("tbxChargeRemarks");
                decimal qtys = Convert.ToDecimal(qty.Text);
                decimal amts = Convert.ToDecimal(tbxChargeAmount.Text) * qtys;
                amt += amts;
            }
        }
        tbxTotal.Text = amt.ToString();

    }
}