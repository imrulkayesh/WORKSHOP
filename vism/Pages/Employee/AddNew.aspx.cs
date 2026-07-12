using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Employee_AddNew : BasePage
{
    dalEmployee obj = new dalEmployee();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/Employee/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            ViewState["ID"] = (int)0;
            LoadDdl();
            ddlBusinessCenter.SelectedValue = SessionManager.SessionName.BusinessCentetid.ToString();

        }

    }

    private void LoadDdl()
    {
        ddlRole.DataSource = new dalUser().GetRolesForUserSetup();
        ddlRole.DataBind();
        ddlCountry.DataSource = new Common().GetAll("BS_COUNTRY" + " ORDER BY ID ASC");
        ddlCountry.DataBind();
        ddlCompany.DataSource = new Common().GetAll("BS_COMPANY");
        ddlCompany.DataBind();
        ddlDesignation.DataSource = new Common().Get_AllDesc("BS_DESIGNATION");
        ddlDesignation.DataBind();
        ddlDepartment.DataSource = new Common().Get_AllDesc("BS_DEPARTMENT");
        ddlDepartment.DataBind();
        ddlBusinessCenter.DataSource = new Common().Get_AllDesc("BS_BUSINESSCENTER");
        ddlBusinessCenter.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            obj.InsertUser(tbxStaffID.Text.Trim(),Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlRole.SelectedValue));
            int a= obj.Update(tbxStaffID.Text.Trim(), Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text, tbxMobile.Text, tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
            if (a==0)
            obj.Insert(tbxStaffID.Text.Trim(), Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text,tbxMobile.Text,tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue),Convert.ToInt32(ddlCompany.SelectedValue),Convert.ToInt32(ddlDepartment.SelectedValue));
            if (Convert.ToInt32(ddlRole.SelectedValue) == 12|| Convert.ToInt32(ddlRole.SelectedValue) == 22)
            {
                new dalApprover().Insert(Convert.ToInt32(ddlCompany.SelectedValue), tbxStaffID.Text.Trim(),1, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue),1);
            }
            new dalUserRole().Insert(tbxStaffID.Text.Trim(), Convert.ToInt32(ddlRole.SelectedValue), 1);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
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
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text, tbxMobile.Text, tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
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
        tbxEmail.Text = "";
        tbxMobile.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["VENDORNAME"].ToString();
            tbxMobile.Text = dt.Rows[0]["MOBILE"].ToString();
            tbxEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            ddlCountry.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();
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
            new Common().Delete("EM_EMPLOYEE", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
}