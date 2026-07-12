using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Company : BasePage
{
    dalCompany obj = new dalCompany();
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
        ddlGroupofCompany.DataSource = new Common().GetAll("BS_GROUPOFCOMPANY");
        ddlGroupofCompany.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(tbxName.Text, Convert.ToInt32(ddlGroupofCompany.SelectedValue)))
        {
            try
            {
                obj.Insert(tbxName.Text, Convert.ToInt32(ddlGroupofCompany.SelectedValue), dalCommon.DateFormatYYYYMMDD(tbxEstablishDate.Text));

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
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxName.Text, Convert.ToInt32(ddlGroupofCompany.SelectedValue), dalCommon.DateFormatYYYYMMDD(tbxEstablishDate.Text));
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
        tbxEstablishDate.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlGroupofCompany.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlGroupofCompany.SelectedValue));
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
            tbxName.Text = dt.Rows[0]["COMPANYNAME"].ToString();
            tbxEstablishDate.Text = Convert.ToDateTime(dt.Rows[0]["COMESTABLISHDATE"]).ToString("dd/MM/yyyy");
            ddlGroupofCompany.SelectedValue = dt.Rows[0]["GROUPOFCOMPANYID"].ToString();
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
            new Common().Delete("BS_COMPANY", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
    protected void ddlGroupofCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}