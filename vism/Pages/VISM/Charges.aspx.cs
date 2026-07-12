using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Charges : BasePage
{
    dalCharges obj = new dalCharges();
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
            ViewState["ID"] = (int)0;
            LoadDDL();
            LoadWorkshop();
            BindData();
        }

    }

    private void LoadDDL()
    {
        ddllocationid.DataSource = new Common().GetAll("BS_BUSINESSCENTER");
        ddllocationid.DataBind();
        ddlCategoryid.DataSource = new Common().GetAll("RVS_SERVICECATEGORY");
        ddlCategoryid.DataBind();
        ddlCharges.DataSource = new Common().GetAll("RVS_CHARGENAME");
        ddlCharges.DataBind();
        ddlCurrencyid.DataSource = new Common().GetAll("ASSET_CURRENCY");
        ddlCurrencyid.DataBind();
    }
    private void LoadWorkshop()
    {
        if (!string.IsNullOrEmpty(ddllocationid.SelectedValue))
        {

            ddlworkshopid.DataSource = new dalWorkShop().GetAll(Convert.ToInt32(ddllocationid.SelectedValue));
            ddlworkshopid.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Insert(Convert.ToInt32(ddlCategoryid.SelectedValue), Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlCharges.SelectedValue), Convert.ToDecimal(tbxamount.Text), tbxremark.Text, Convert.ToInt32(ddllocationid.SelectedValue), Convert.ToInt32(ddlCurrencyid.SelectedValue));

            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            BindData();
            ClearAll();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(ddlCategoryid.SelectedValue), Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlCharges.SelectedValue), Convert.ToDecimal(tbxamount.Text), tbxremark.Text, Convert.ToInt32(ddllocationid.SelectedValue), Convert.ToInt32(ddlCurrencyid.SelectedValue));
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            BindData();
            ClearAll();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxamount.Text = "";
        tbxremark.Text = "";

        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue) && !string.IsNullOrEmpty(ddllocationid.SelectedValue) && !string.IsNullOrEmpty(ddlCategoryid.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddllocationid.SelectedValue), Convert.ToInt32(ddlCategoryid.SelectedValue));
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
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            ddllocationid.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
            LoadWorkshop();
            ddlworkshopid.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
            ddlCharges.SelectedValue = dt.Rows[0]["CHARGEID"].ToString();
            ddlCurrencyid.SelectedValue = dt.Rows[0]["CURRENCYID"].ToString();
            tbxamount.Text = dt.Rows[0]["AMOUNT"].ToString();
            tbxremark.Text = dt.Rows[0]["REMARKS"].ToString();

            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_CHARGES", ID);
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
        BindData();
    }

    protected void ddlworkshopid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWorkshop();
        BindData();
    }

    protected void ddlCategoryid_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddllocationid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWorkshop();
        BindData();
    }
}