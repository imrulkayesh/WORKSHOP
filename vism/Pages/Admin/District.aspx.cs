using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_District : BasePage
{
    dalDistrict obj = new dalDistrict();
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
            BindData();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (obj.CheckDuplicate(tbxName.Text, Convert.ToInt32(ddlDivision.SelectedValue)))
            {
                obj.Insert(tbxName.Text, Convert.ToInt32(ddlDivision.SelectedValue));

                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
                ClearAll();
            }
            else
            {
                MessageController.Show("This District name already exists. Please try anothor.", MessageType.Error, Page);
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
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxName.Text, Convert.ToInt32(ddlDivision.SelectedValue));
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
        if (ddlDivision.SelectedValue != "")
        {
            DataTable dt = obj.GetByDivisionId(Convert.ToInt32(ddlDivision.SelectedValue));
            rptYear.DataSource = dt;
            rptYear.DataBind();
        }
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["DISTRICTNAME"].ToString();
            ddCountry.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();
            LoadDivisions();
            ddlDivision.SelectedValue = dt.Rows[0]["DIVISIONID"].ToString();
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
            ddlDivision.DataBind();
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDivisions();
    }
}