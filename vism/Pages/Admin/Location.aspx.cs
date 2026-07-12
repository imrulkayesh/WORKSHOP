using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Location : BasePage
{
    dalLocation obj = new dalLocation();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/Admin/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            ViewState["ID"] = (int)0;
            LoadDivision();
            loadDistrict();
            loadThana();
            LoadTimeZone();
            BindData();
        }

    }
    private void LoadTimeZone()
    {
        ddlTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones();
        ddlTimeZone.DataTextField = "DisplayName";
        ddlTimeZone.DataValueField = "DisplayName";
        ddlTimeZone.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (obj.CheckDuplicate(tbxName.Text, Convert.ToInt32(ddlThana.SelectedValue)))
            {
                obj.Insert(tbxName.Text, Convert.ToInt32(ddCountry.SelectedValue), Convert.ToInt32(ddlThana.SelectedValue), ddlTimeZone.SelectedValue, tbxLocationCode.Text);

                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
                ClearAll();
            }
            else
            {
                MessageController.Show("This Location already exists. Please try anothor.", MessageType.Error, Page);
            }
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
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxName.Text, Convert.ToInt32(ddCountry.SelectedValue), Convert.ToInt32(ddlThana.SelectedValue), ddlTimeZone.SelectedValue, tbxLocationCode.Text);
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
        tbxName.Text = "";
        tbxLocationCode.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (ddlThana.SelectedValue != "")
        {
            DataTable dt = obj.GetByThanaId(Convert.ToInt32(ddlThana.SelectedValue));
            rptYear.DataSource = dt;
            rptYear.DataBind();
        }
        else
        {
            rptYear.DataSource = null;
            rptYear.DataBind();
        }
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            DataTable dt = obj.GetById(ID);
            if (dt.Rows.Count > 0)
            {
                tbxName.Text = dt.Rows[0]["NAME"].ToString();
                tbxLocationCode.Text = dt.Rows[0]["LOCATIONCODE"].ToString();
                ddCountry.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();
                LoadDivisions();
                ddlDivision.SelectedValue = dt.Rows[0]["DIVISIONID"].ToString();
                loadDistrict();
                ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
                loadThana();
                ddlThana.SelectedValue = dt.Rows[0]["THANAID"].ToString();
                ddlTimeZone.SelectedValue = dt.Rows[0]["TIMEZO"].ToString();
                ViewState["ID"] = ID;
            }
            btnSave.Visible = false;
            btnEdit.Visible = true;
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("BS_LOCATIONS", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
    protected void LoadDivision()
    {
        ddCountry.DataSource = new Common().GetAll("BS_COUNTRY");
        ddCountry.DataBind();
        LoadDivisions();
    }

    private void LoadDivisions()
    {
        DataTable dt = obj.GetByCountryId(Convert.ToInt32(ddCountry.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            ddlDivision.DataSource = dt;

        }
        else
        {
            ddlDivision.DataSource = null;
            ddlDivision.Items.Clear();
        }
        ddlDivision.DataBind();
    }
    private void loadDistrict()
    {
        DataTable dt = obj.GetByDivisionId(Convert.ToInt32(ddlDivision.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            ddlDistrict.DataSource = dt;

        }
        else
        {
            ddlDistrict.DataSource = null;
            ddlDistrict.Items.Clear();
        }
        ddlDistrict.DataBind();
    }
    private void loadThana()
    {
        DataTable dt = obj.GetByDistrictId(Convert.ToInt32(ddlDistrict.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            ddlThana.DataSource = dt;

        }
        else
        {
            ddlThana.DataSource = null;
            ddlThana.Items.Clear();
        }
        ddlThana.DataBind();
        BindData();
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict();
        ClearAll();
    }
    protected void ddCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDivisions();
        loadDistrict();
        ClearAll();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadThana();
        ClearAll();
    }
    protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
        ClearAll();
    }
}