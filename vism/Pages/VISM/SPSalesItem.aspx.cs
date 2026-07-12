using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_SPSalesItem :BasePage
{
    dalSPSalesItems obj = new dalSPSalesItems();
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
            BindData();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (obj.CheckDuplicate(Convert.ToInt32(ddlspsales.SelectedValue),Convert.ToInt32(ddlparts.SelectedValue),Convert.ToInt32(ddlunit.SelectedValue)))
        //{
        //    try
        //    {
        //        obj.Insert(Convert.ToInt32(ddlspsales.SelectedValue),Convert.ToInt32(ddlparts.SelectedValue),Convert.ToInt32(tbxQuentity.Text), Convert.ToInt32(ddlunit.SelectedValue),Convert.ToDecimal(tbxUnitprice.Text),Convert.ToDecimal(tbxTotalPrice),Convert.ToInt32(ddlCurrency.SelectedValue),Convert.ToDecimal(ddlCurrency.SelectedValue),Convert.ToDecimal(tbxNetAmount.Text));

        //        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        //        BindData();
        //        ClearAll();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.WriteLog(ex);
        //        MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

        //    }
        //}
        //else
        //{
        //    MessageController.Show("This SPSales, Parts, Unit already exists. Please try anothor.", MessageType.Error, Page);
        //}
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(ddlspsales.SelectedValue), Convert.ToInt32(ddlparts.SelectedValue), Convert.ToInt32(tbxQuentity.Text), Convert.ToInt32(ddlunit.SelectedValue), Convert.ToDecimal(tbxUnitprice.Text), Convert.ToDecimal(tbxTotalPrice), Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToDecimal(ddlCurrency.SelectedValue), Convert.ToDecimal(tbxNetAmount.Text));
        //    MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        //    BindData();
        //    ClearAll();
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteLog(ex);
        //    MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        //}
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        //tbxDiscount.Text = "";
        //tbxNetAmount.Text = "";
        //tbxQuentity.Text = "";
        //tbxTotalPrice.Text = "";
        //tbxUnitprice.Text = "";

        //btnSave.Visible = true;
        //btnEdit.Visible = false;
    }
    protected void BindData()
    {
        //DataTable dt = new Common().GetAll("RVS_SPSALESITEMS");
        //rptYear.DataSource = dt;
        //rptYear.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        //int ID = Convert.ToInt32(e.CommandArgument);
        //DataTable dt = obj.GetById(ID);
        //if (dt.Rows.Count > 0)
        //{
        //    ddlspsales.SelectedValue = dt.Rows[0][""].ToString();
        //    ddlparts.SelectedValue = dt.Rows[0][""].ToString();
        //    tbxQuentity.Text = dt.Rows[0][""].ToString();
        //    ddlunit.SelectedValue = dt.Rows[0][""].ToString();
        //    tbxUnitprice.Text = dt.Rows[0][""].ToString();
        //    ddlCurrency.SelectedValue = dt.Rows[0][""].ToString();
        //    tbxDiscount.Text = dt.Rows[0][""].ToString();
        //    tbxNetAmount.Text = dt.Rows[0][""].ToString();

            
        //    ViewState["ID"] = ID;
        //}
        //btnSave.Visible = false;
        //btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_SPSALESITEMS", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
}