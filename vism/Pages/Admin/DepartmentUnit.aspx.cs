using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_DepartmentUnit : BasePage
{
    dalDepartmentUnit obj = new dalDepartmentUnit();
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
            LoadDDL();
            BindData();

        }

    }
    private void LoadDDL()
    {
        ddlDepartment.DataSource = new Common().GetAll("BS_DEPARTMENT");
        ddlDepartment.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(tbxName.Text, Convert.ToInt32(ddlDepartment.SelectedValue)))
        {
            try
            {
                obj.Insert(tbxName.Text, Convert.ToInt32(ddlDepartment.SelectedValue), tbxUnitCode.Text);

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
            MessageController.Show("This Company name already exists. Please try anothor.", MessageType.Error, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxName.Text, Convert.ToInt32(ddlDepartment.SelectedValue), tbxUnitCode.Text);
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
        tbxUnitCode.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlDepartment.SelectedValue));
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
            tbxName.Text = dt.Rows[0]["DEPTUNITNAME"].ToString();
            tbxUnitCode.Text = dt.Rows[0]["DEPTUNITCODE"].ToString();
            ddlDepartment.SelectedValue = dt.Rows[0]["DEPARTMENTID"].ToString();
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
            new Common().Delete("BS_DEPARTMENTUNIT", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}