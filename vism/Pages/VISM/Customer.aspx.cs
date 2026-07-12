using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Customer : BasePage
{
    dalVISMCustomer obj = new dalVISMCustomer();
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
            ViewState["photo"] = "";
            LoadDdl();
            LoadDivisions();
            loadDistrict();
            LoadThana();
            BindData();

        }

    }
    private void BindData()
    {
        rpt.DataSource = new Common().GetAll("RVS_CUSTOMER");
        rpt.DataBind();
    }
    private void LoadDdl()
    {
        ddlCountry.DataSource = new Common().GetAll("BS_COUNTRY");
        ddlCountry.DataBind();
        ddlGender.DataSource = new Common().GetAll("BS_GENDER");
        ddlGender.DataBind();
        ddlReligion.DataSource = new Common().GetAll("BS_RELIGION");
        ddlReligion.DataBind();
    }
    private void LoadDivisions()
    {
        int country = 0;
        if (!string.IsNullOrEmpty(ddlCountry.SelectedValue))
        {
            country = Convert.ToInt32(ddlCountry.SelectedValue);
        }
        DataTable dt = new dalThana().GetByCountryId(country);
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
        int divi = 0;
        if (!string.IsNullOrEmpty(ddlDivision.SelectedValue))
        {
            divi = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        DataTable dt = new dalThana().GetByDivisionId(divi);
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
    protected void LoadThana()
    {
        if (!string.IsNullOrEmpty(ddlDistrict.SelectedValue))
        {
            DataTable dt = new dalThana().GetByDistrictId(Convert.ToInt32(ddlDistrict.SelectedValue));
            ddlThana.DataSource = dt;
        }
        else
        {
            ddlThana.DataSource = null;
        }
        ddlThana.DataBind();
    }
    private string FileUpload()
    {
        string AttachFile = ViewState["photo"].ToString();
        if (addPhoto.HasFile)
        {
            AttachFile = tbxMobile.Text + "_" + addPhoto.FileName;
            string MediumImagePath = Server.MapPath("~/Content/Attachment/VISMCustomer/" + AttachFile);
            addPhoto.SaveAs(string.Concat(MediumImagePath));

        }
        return AttachFile;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Insert(tbxCustomerName.Text, tbxMobile.Text.Trim(), tbxPhone.Text, tbxFax.Text, tbxEmail.Text, Convert.ToInt32(ddlGender.SelectedValue), tbxAddress.Text, Convert.ToInt32(ddlCountry.SelectedValue),
                Convert.ToInt32(ddlDivision.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlThana.SelectedValue), tbxPostCode.Text, tbxNid.Text, tbxPassport.Text, tbxBirthCertificate.Text,
                tbxTin.Text, Convert.ToInt32(ddlReligion.SelectedValue), tbxProfession.Text, tbxFather.Text, tbxMother.Text, tbxGuardian.Text, tbxDob.Text, FileUpload());
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
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxCustomerName.Text, tbxMobile.Text.Trim(), tbxPhone.Text, tbxFax.Text, tbxEmail.Text, Convert.ToInt32(ddlGender.SelectedValue), tbxAddress.Text, Convert.ToInt32(ddlCountry.SelectedValue),
                Convert.ToInt32(ddlDivision.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlThana.SelectedValue), tbxPostCode.Text, tbxNid.Text, tbxPassport.Text, tbxBirthCertificate.Text,
                tbxTin.Text, Convert.ToInt32(ddlReligion.SelectedValue), tbxProfession.Text, tbxFather.Text, tbxMother.Text, tbxGuardian.Text, tbxDob.Text, FileUpload());
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
        tbxAddress.Text = "";
        tbxBirthCertificate.Text = "";
        tbxCustomerName.Text = "";
        tbxMobile.Text = "";
        tbxDob.Text = "";
        tbxFather.Text = "";
        tbxFax.Text = "";
        tbxGuardian.Text = "";
        tbxMother.Text = "";
        tbxNid.Text = "";
        tbxPassport.Text = "";
        tbxPhone.Text = "";
        tbxPostCode.Text = "";
        tbxProfession.Text = "";
        tbxTin.Text = "";
        tbxEmail.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            tbxBirthCertificate.Text = dt.Rows[0]["BIRTHCERTIFICATE"].ToString();
            tbxCustomerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
            tbxMobile.Text = dt.Rows[0]["MOBILE"].ToString();
            tbxDob.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
            tbxFather.Text = dt.Rows[0]["FATHERNAME"].ToString();
            tbxFax.Text = dt.Rows[0]["FAX"].ToString();
            tbxGuardian.Text = dt.Rows[0]["GUARDIANNAME"].ToString();
            tbxMother.Text = dt.Rows[0]["MOTHERNAME"].ToString();
            tbxNid.Text = dt.Rows[0]["NID"].ToString();
            tbxPassport.Text = dt.Rows[0]["PASSPORT"].ToString();
            tbxPhone.Text = dt.Rows[0]["PHONE"].ToString();
            tbxPostCode.Text = dt.Rows[0]["POSTCODE"].ToString();
            tbxProfession.Text = dt.Rows[0]["PROFESSION"].ToString();
            tbxTin.Text = dt.Rows[0]["TIN"].ToString();
            tbxEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            ddlReligion.SelectedValue = dt.Rows[0]["RELIGIONID"].ToString();
            ddlGender.SelectedValue = dt.Rows[0]["GENDERID"].ToString();
            ddlCountry.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();
            LoadDivisions();
            ddlDivision.SelectedValue = dt.Rows[0]["DIVISIONID"].ToString();
            loadDistrict();
            ddlDistrict.SelectedValue = dt.Rows[0]["DISTRICTID"].ToString();
            LoadThana();
            ddlThana.SelectedValue = dt.Rows[0]["THANAID"].ToString();
            ViewState["ID"] = ID;
            ViewState["photo"] = dt.Rows[0]["PHOTO"].ToString();
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_CUSTOMER", ID);
            BindData();
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict();
        LoadThana();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadThana();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDivisions();
        loadDistrict();
        LoadThana();
    }
}