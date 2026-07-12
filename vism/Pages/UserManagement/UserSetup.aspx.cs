using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_UserManagement_UserSetup : BasePage
{
    dalUser objUser = new dalUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = "/Pages/UserManagement/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
        if (SessionManager.SessionName.UserName == null)
        {
            dalCommon.LoginReturnUrl(url);
        }
        else if (!dalCommon.IsPermitted(url))
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            LoadDdl();
            ddlBusinessCenter.SelectedValue = SessionManager.SessionName.BusinessCentetid.ToString();
            //BindData();
        }
    }
    private void LoadDdl()
    {
        Common cm = new Common();
        ddlRole.DataSource = objUser.GetRolesForUserSetup();
        ddlRole.DataBind();
        ddlBusinessCenter.DataSource = cm.GetAll("BS_BUSINESSCENTER");
        ddlBusinessCenter.DataBind();
        ddlDesignation.DataSource = cm.Get_AllDesc("BS_DESIGNATION");
        ddlDesignation.DataBind();
        ddlCompany.DataSource = cm.GetAll("BS_COMPANY");
        ddlCompany.DataBind();
        ddlDepartment.DataSource = cm.GetAll("BS_DEPARTMENT");
        ddlDepartment.DataBind();
    }
    string Id
    {
        set { ViewState["Id"] = value; }
        get
        {
            try
            {
                return ViewState["Id"].ToString();
            }
            catch
            {
                return "";
            }
        }
    }

    protected void BindData()
    {

        if (!string.IsNullOrEmpty(ddlBusinessCenter.SelectedValue))
        {
            DataTable dt = objUser.GetUsers(Convert.ToInt32(ddlBusinessCenter.SelectedValue));
            rpt.DataSource = dt;
        }
        else
        {
            rpt.DataSource = null;
        }
        rpt.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (objUser.CheckDuplicate(tbxStaffId.Text))
        {
            try
            {
                new dalEmployee().InsertUser(tbxStaffId.Text.Trim(), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlRole.SelectedValue));

                //objUser.CreateUsers(tbxUserName.Text, tbxStaffId.Text, Convert.ToInt32(ddlRole.SelectedValue), Encryption.EncryptionWithSh(tbxpassword.Text), Convert.ToInt32(ddlDepartment.SelectedValue));
                //objUser.Insert(tbxStaffId.Text, Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text, tbxMobile.Text, tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
                //if (Convert.ToInt32(ddlRole.SelectedValue) == 12 || Convert.ToInt32(ddlRole.SelectedValue) == 22)
                //{
                //    new dalApprover().Insert(Convert.ToInt32(ddlCompany.SelectedValue), tbxUserName.Text.Trim(), 1, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), 1);
                //}
                //
                int a = new dalEmployee().Update(tbxStaffId.Text.Trim(), Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text, tbxMobile.Text, tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
                if (a == 0)
                    new dalEmployee().Insert(tbxStaffId.Text.Trim(), Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text, tbxMobile.Text, tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
                if (Convert.ToInt32(ddlRole.SelectedValue) == 12 || Convert.ToInt32(ddlRole.SelectedValue) == 22)
                {
                    try
                    {
                        new dalApprover().Insert(Convert.ToInt32(ddlCompany.SelectedValue), tbxStaffId.Text.Trim(), 1, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), 1);
                    }
                    catch
                    {
                        //
                    }
                }
                new dalUserRole().Insert(tbxStaffId.Text.Trim(), Convert.ToInt32(ddlRole.SelectedValue), 1);
                //
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
        else
        {
            MessageController.Show("This username/Staff ID already exists. Please try another.", MessageType.Error, Page);
        }
    }
    protected void ClearAll()
    {
        tbxConfirmPass.Text = "";
        tbxEmail.Text = "";
        tbxStaffId.Text = "";
        tbxMobile.Text = "";
        tbxpassword.Text = "";
        tbxUserName.Text = "";
        tbxName.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            objUser.UpdateUsers(tbxStaffId.Text, Convert.ToInt32(ddlRole.SelectedValue), Encryption.EncryptionWithSh(tbxpassword.Text), Convert.ToInt32(ddlDepartment.SelectedValue));
            //objUser.Update(tbxStaffId.Text, Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text, tbxMobile.Text, tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
            new dalEmployee().Update(tbxStaffId.Text.Trim(), Convert.ToInt32(ddlDesignation.SelectedValue), tbxName.Text, tbxMobile.Text, tbxEmail.Text, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
            if (Convert.ToInt32(ddlRole.SelectedValue) == 12 || Convert.ToInt32(ddlRole.SelectedValue) == 22)
            {
                try
                {
                    new dalApprover().Insert(Convert.ToInt32(ddlCompany.SelectedValue), tbxStaffId.Text.Trim(), 1, Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), 1);

                }
                catch
                {
                    //
                }
            }
            //new dalUserRole().Insert(tbxStaffId.Text.Trim(), Convert.ToInt32(ddlRole.SelectedValue), 1);
            new dalUserRole().Update(tbxStaffId.Text.Trim(), Convert.ToInt32(ddlRole.SelectedValue), 1);

            //
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
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        Id = e.CommandArgument.ToString();
        DataTable dt = objUser.GetUserById(Id);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["EMPLOYEENAME"].ToString();
            tbxMobile.Text = dt.Rows[0]["OFFICEMOBILE"].ToString();
            tbxEmail.Text = dt.Rows[0]["OFFICEMAIL"].ToString();
            ddlRole.SelectedValue = dt.Rows[0]["ROLEID"].ToString();
            tbxStaffId.Text = dt.Rows[0]["STAFFID"].ToString();
            tbxUserName.Text = dt.Rows[0]["USERNAME"].ToString();
            ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
            ddlBusinessCenter.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
            ddlDepartment.SelectedValue = dt.Rows[0]["DEPTID"].ToString();
            ddlDesignation.SelectedValue = dt.Rows[0]["DESIGNATIONID"].ToString();
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
        tbxStaffId.Enabled = false;
        tbxUserName.Enabled = false;
        //tbxpassword.Enabled = false;
        //tbxConfirmPass.Enabled = false;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {

    }
    protected void ddlBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(tbxSearch.Text))
            {
                DataTable dt = objUser.GetUsersSearch(tbxSearch.Text);
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.DataSource = null;
                MessageController.Show("Data not found !", MessageType.Error, Page);
            }
        }
        catch (Exception ex)
        {

            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
}