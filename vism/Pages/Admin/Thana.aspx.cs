using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Thana : BasePage
{

    dalThana obj = new dalThana();
    protected void Page_Load(object sender, EventArgs e)
    {
       
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
            BindData();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (obj.CheckDuplicate(tbxName.Text, Convert.ToInt32(ddlDistrict.SelectedValue)))
            {
                obj.Insert(tbxName.Text, Convert.ToInt32(ddlDistrict.SelectedValue));

                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
                ClearAll();
            }
            else
            {
                MessageController.Show("This Thana name already exists. Please try anothor.", MessageType.Error, Page);
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
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxName.Text, Convert.ToInt32(ddlDistrict.SelectedValue));
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
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (ddlDistrict.SelectedValue != "")
        {
            DataTable dt = obj.GetByDistrictId(Convert.ToInt32(ddlDistrict.SelectedValue));
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
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["THANANAME"].ToString();
            ddCountry.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();
            LoadDivisions();
            ddlDivision.SelectedValue = dt.Rows[0]["DIVISIONID"].ToString();
            loadDistrict();
            ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
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
            new Common().Delete("BS_DISTRICT", ID);
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
        DataTable dt = obj.GetByDivisionId(string.IsNullOrEmpty(ddlDivision.SelectedValue) ?0:Convert.ToInt32(ddlDivision.SelectedValue));
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
        BindData();
        ClearAll();
    }
}