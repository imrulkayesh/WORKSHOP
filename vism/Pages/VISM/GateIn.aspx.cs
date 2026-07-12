using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_GateIn : BasePage
{
    dalGatein obj = new dalGatein();
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
            LoadDDL();
            BindData();
        }
    }

    private void LoadDDL()
    {
        ddlSerCatid.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlSerCatid.DataBind();
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(tbxchassisno.Text))
        {
            try
            {
                obj.Insert(tbxchassisno.Text, tbxregno.Text, tbxowner.Text, tbxownermob.Text, tbxDriverName.Text, tbxDriverMobile.Text, Convert.ToInt32(ddlSerCatid.SelectedValue), Convert.ToInt32(ddlworkshopid.SelectedValue));

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
            MessageController.Show("This Vehicle already inside the workshop. Please exit the vehicle.", MessageType.Error, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxchassisno.Text, tbxregno.Text, tbxowner.Text, tbxownermob.Text, tbxDriverName.Text, tbxDriverMobile.Text, Convert.ToInt32(ddlSerCatid.SelectedValue), Convert.ToInt32(ddlworkshopid.SelectedValue));
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
        tbxchassisno.Text = "";
        tbxregno.Text = "";
        tbxowner.Text = "";
        tbxownermob.Text = "";
        tbxDriverName.Text = "";
        tbxDriverMobile.Text = "";

        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlSerCatid.SelectedValue) && !string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlSerCatid.SelectedValue), Convert.ToInt32(ddlworkshopid.SelectedValue));
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
            tbxchassisno.Text = dt.Rows[0]["CHASSISNO"].ToString();
            tbxregno.Text = dt.Rows[0]["REGNO"].ToString();
            tbxowner.Text = dt.Rows[0]["OWNERNAME"].ToString();
            tbxownermob.Text = dt.Rows[0]["OWNERMOBILE"].ToString();
            ddlSerCatid.SelectedValue = dt.Rows[0]["SERVICECATID"].ToString();
            ddlworkshopid.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
            tbxDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
            tbxDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();

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
            new Common().Delete("RVS_GATEIN", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

    protected void ddlSerCatid_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlworkshopid_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
