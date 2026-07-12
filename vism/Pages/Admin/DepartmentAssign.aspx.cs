using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_DepartmentAssign : BasePage
{
    dalDepartment obj = new dalDepartment();
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
            LoadCountry();
            LoadLocation();
            LoadBusinessCenter();
            BindData();
        }
    }
    protected void BindData()
    {
        DataTable dt = new Common().GetAll("BS_DEPARTMENT");
        gvDepartment.DataSource = dt;
        gvDepartment.DataBind();
    }
    protected void LoadCountry()
    {
        ddlCountry.DataSource = new Common().GetAll("BS_COUNTRY");
        ddlCountry.DataBind();
        ddlCompany.DataSource = new Common().GetAll("BS_COMPANY");
        ddlCompany.DataBind();
    }
    private void LoadLocation()
    {
        DataTable dt = obj.GetByCountryId(string.IsNullOrEmpty(ddlCountry.SelectedValue) ? 0 : Convert.ToInt32(ddlCountry.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            ddlLocation.DataSource = dt;
        }
        else
        {
            ddlLocation.DataSource = null;
            ddlLocation.Items.Clear();
        }
        ddlLocation.DataBind();
    }
    private void LoadBusinessCenter()
    {
        DataTable dt = obj.GetByLocationId(string.IsNullOrEmpty(ddlLocation.SelectedValue)?0:Convert.ToInt32(ddlLocation.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            ddlBusinessCenter.DataSource = dt;
        }
        else
        {
            ddlBusinessCenter.DataSource = null;
            ddlBusinessCenter.Items.Clear();
        }
        ddlBusinessCenter.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "" && ddlBusinessCenter.SelectedValue != "")
        {
            try
            {
                obj.DeleteAssignedDepartment(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlBusinessCenter.SelectedValue));
                foreach (GridViewRow gr in gvDepartment.Rows)
                {
                    CheckBox chk = (CheckBox)gr.FindControl("chkrow");
                    if (chk.Checked)
                    {
                        obj.AssignDepartment(Convert.ToInt32(gr.Cells[0].Text.Trim()),Convert.ToInt32(ddlBusinessCenter.SelectedValue),Convert.ToInt32(ddlCompany.SelectedValue));
                    }
                }

                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            }
            catch(Exception ex) 
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);
            }
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
        LoadLocation();
        LoadBusinessCenter();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
        LoadBusinessCenter();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        DataTable dt = obj.GetAssignedDepartment(Convert.ToInt32(ddlCompany.SelectedValue),Convert.ToInt32(ddlBusinessCenter.SelectedValue));
        foreach (DataRow dr in dt.Rows)
        {
            string deptId = dr["DEPARTMENTID"].ToString();
            foreach (GridViewRow gr in gvDepartment.Rows)
            {
                if (deptId == gr.Cells[0].Text)
                {
                    CheckBox chk = (CheckBox)gr.FindControl("chkrow");
                    chk.Checked = true;
                    break;
                }
            }
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}