using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_InsIteams : BasePage
{
    dalInsIteams obj = new dalInsIteams();
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
        ddlCategory.DataSource = new Common().GetAll("RVS_SERVICECATEGORY");
        ddlCategory.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(tbxitem.Text, Convert.ToInt32(chkInspectinType.SelectedValue)))
        {
            try
            {
                int LABORCHARGE = (Convert.ToInt32(tbxLaborQuantity.Text) * Convert.ToInt32(tbxLaborRate.Text) * Convert.ToInt32(tbxWorkingHour.Text));

                obj.Insert(tbxitem.Text, Convert.ToInt32(tbxcorder.Text), Convert.ToInt32(chkInspectinType.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), tbxLaborQuantity.Text, tbxLaborRate.Text, tbxWorkingHour.Text, LABORCHARGE);

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
            MessageController.Show("This Item already exists. Please try anothor.", MessageType.Error, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int LABORCHARGE = (Convert.ToInt32(tbxLaborQuantity.Text) * Convert.ToInt32(tbxLaborRate.Text) * Convert.ToInt32(tbxWorkingHour.Text));

            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxitem.Text, Convert.ToInt32(tbxcorder.Text), Convert.ToInt32(chkInspectinType.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), tbxLaborQuantity.Text, tbxLaborRate.Text, tbxWorkingHour.Text, LABORCHARGE);
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
        tbxitem.Text = "";
        tbxcorder.Text = "";

        tbxLaborQuantity.Text = "";
        tbxLaborRate.Text = "";
        tbxWorkingHour.Text = "";

        btnSave.Visible = true;
        btnEdit.Visible = false;

        tbxitem.Enabled = true;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(chkInspectinType.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
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
            tbxitem.Text = dt.Rows[0]["ITEMNAME"].ToString();
            chkInspectinType.SelectedValue = dt.Rows[0]["INSPECTIONTYPE"].ToString();
            tbxcorder.Text = dt.Rows[0]["CORDER"].ToString();

            tbxLaborRate.Text = dt.Rows[0]["LABORRATE"].ToString();
            tbxLaborQuantity.Text = dt.Rows[0]["LABORQTY"].ToString();
            tbxWorkingHour.Text = dt.Rows[0]["WORKINGHOUR"].ToString();

            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;

        tbxitem.Enabled = false;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_INSPECTIONITEMS", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
    protected void chkInspectinType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}