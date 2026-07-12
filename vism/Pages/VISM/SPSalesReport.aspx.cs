using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_SPSalesReport : BasePage
{
    dalSPSales obj = new dalSPSales();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + System.IO.Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            LoadDDL();

        }
    }
    private void LoadDDL()
    {
        ddlWorkshop.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlWorkshop.DataBind();
    }
    protected void ClearAll()
    {
        tbxfromdate.Text = "";
        tbxtodate.Text = "";
        tbxStaffID.Text = "";
    }
    protected void BindData()
    {
        DataTable dt = obj.GetByCriteria(Criteria());
        rpt.DataSource = dt;
        rpt.DataBind();
    }
    private string Criteria()
    {
        string ctr = "";
        if (!string.IsNullOrEmpty(ddlWorkshop.SelectedValue))
        {
            ctr += " AND S.WORKSHOPID=" + ddlWorkshop.SelectedValue;
        }
        if (tbxStaffID.Text.Length > 0)
        {
            ctr += " AND S.CREATEDBY='" + tbxStaffID.Text.Trim() + "'";
        }
        if (tbxfromdate.Text.Length > 0 && tbxtodate.Text.Length > 0)
        {
            ctr += " AND TRUNC(S.SELLDATE) BETWEEN TO_DATE('" + tbxfromdate.Text.Trim() + "','DD/MM/YYYY') AND TO_DATE('" + tbxtodate.Text.Trim() + "','DD/MM/YYYY')";
        }
        else if (tbxfromdate.Text.Length > 0 && tbxtodate.Text.Length == 0)
        {
            ctr += " AND TRUNC(S.SELLDATE)=TO_DATE('" + tbxfromdate.Text.Trim() + "','DD/MM/YYYY')";
        }
        return ctr;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}

