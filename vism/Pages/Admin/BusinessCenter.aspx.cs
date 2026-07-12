using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_BusinessCenter : BasePage
{
    dalBusinessCenter obj = new dalBusinessCenter();
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
            LoadDdl();
            LoadLocation();
            BindData();
        }
    }

    private void LoadDdl()
    {
        ddlCountry.DataSource = new Common().GetAll("BS_COUNTRY");
        ddlCountry.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (obj.CheckDuplicate(tbxName.Text, Convert.ToInt32(ddlLocation.SelectedValue)))
            {
                obj.Insert(tbxName.Text, tbxCenterCode.Text, tbxAddress.Text, Convert.ToInt32(ddlLocation.SelectedValue));
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
                ClearAll();
            }
            else
            {
                MessageController.Show("This Business Center already exists. Please try anothor.", MessageType.Error, Page);
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
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxName.Text, tbxCenterCode.Text, tbxAddress.Text, Convert.ToInt32(ddlLocation.SelectedValue));
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
        tbxCenterCode.Text = "";
        tbxAddress.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (ddlLocation.SelectedValue != "")
        {
            DataTable dt = obj.GetByLocationId(Convert.ToInt32(ddlLocation.SelectedValue));
            rptYear.DataSource = dt;
        }
        else
        {
            rptYear.DataSource = null;
        }
        rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["CENTERNAME"].ToString();
            tbxCenterCode.Text = dt.Rows[0]["CENTERCODE"].ToString();
            tbxAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            ddlCountry.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();
            LoadLocation();
            ddlLocation.SelectedValue = dt.Rows[0]["LOCATIONID"].ToString();
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
            new Common().Delete("BS_BUSINESSCENTER", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLocation();
        BindData();
    }

    private void LoadLocation()
    {
        ListItem li=new ListItem("Select","0");
        DataTable dt = obj.GetByCountryId(Convert.ToInt32(ddlCountry.SelectedValue));
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
        ddlLocation.Items.Insert(0,li);
    }
}