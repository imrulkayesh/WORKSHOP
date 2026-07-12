using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_GateOut : BasePage
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
            lblPrintedBy.Text = SessionManager.SessionName.StaffId.ToString();
        }
        pnPrint.Visible = false;
    }
    private void LoadDDL()
    {
        ddlSerCatid.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlSerCatid.DataBind();
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();
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
    protected void btnGateOut_Command(object sender, CommandEventArgs e)
    {
        string[] a = e.CommandArgument.ToString().Split('-');
        int ID = Convert.ToInt32(a[0]);
        if (obj.CheckOpenBill(a[1]))
        {
            try
            {
                obj.VehicleExit(ID);
                LoadData(ID);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintPanel", "PrintPanel()", true);
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

            }
        }
        else
        {
            MessageController.Show("Please pay dues first.", MessageType.Error, Page);
        }
    }

    private void LoadData(int id)
    {
        DataTable dt = obj.GetById(id);
        if (dt.Rows.Count > 0)
        {
            pnPrint.Visible = true;
            LblChassis.Text = dt.Rows[0]["CHASSISNO"].ToString();
            lblRegNo.Text = dt.Rows[0]["REGNO"].ToString();
            lblOwner.Text = dt.Rows[0]["OWNERNAME"].ToString();
            lblDriver.Text = dt.Rows[0]["DRIVERNAME"].ToString();
            lblOwnerMobile.Text = dt.Rows[0]["OWNERMOBILE"].ToString();
            lblDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();

            lblWorkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
            lblAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            lblPhone.Text = dt.Rows[0]["MOBILE"].ToString();
            lblPurpose.Text = dt.Rows[0]["SERVICETYPE"].ToString();
            lblInOut.Text = "IN-TIME: " + dt.Rows[0]["INDATE"].ToString() + "/ OUT-TIME: " + dt.Rows[0]["OUTDATE"].ToString();
            lblPrintedBy.Text = SessionManager.SessionName.StaffId.ToString();
        }
        else
        {
            pnPrint.Visible = false;
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