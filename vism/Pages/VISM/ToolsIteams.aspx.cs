using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_ToolsIteams : BasePage
{
    dalInsIteams obj = new dalInsIteams();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
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
        ddlSectionCategory.DataSource = new Common().GetAllServiceCat("RVS_SERVICECATEGORY");
        ddlSectionCategory.DataBind();

        ddlUnit.DataSource = new Common().GetAllSCM("RVS_SCMUNIT");
        ddlUnit.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckToolsDuplicate(tbxItemCode.Text))
        {
            try
            {
                obj.InsertTools(tbxItemCode.Text, tbxItemName.Text.Trim().Replace("'","''"), Convert.ToInt32(ddlSectionCategory.SelectedValue), ddlUnit.SelectedValue);

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
            obj.UpdateToolItem(Convert.ToInt32(ViewState["ID"]), tbxItemName.Text.Trim().Replace("'", "''"), Convert.ToInt32(ddlSectionCategory.SelectedValue), ddlUnit.SelectedValue);
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
        tbxItemCode.Text = "";
        tbxItemName.Text = "";

        btnSave.Visible = true;
        //btnEdit.Visible = false;
    }
    protected void BindData()
    {
        //if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
        //{
        //    DataTable dt = obj.GetAll(Convert.ToInt32(chkInspectinType.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
        //    rpt.DataSource = dt;
        //}
        //else
        //{
        //    rpt.DataSource = null;
        //}

        qry = @"select TI.TOOLITEMID,TI.ITEMNAME,CT.CATNAME,SU.UNITNAME 
from RVS_TOOLSITEMS TI,RVS_SCMUNIT SU, RVS_SERVICECATEGORY CT
WHERE TI.SCMUNITCODE=SU.UNITCODE
AND TI.SERVICECATEGORYID=CT.ID";
        DataTable dt = new DataTable();
        dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetByToolId(ID);
        if (dt.Rows.Count > 0)
        {
            tbxItemCode.Text = dt.Rows[0]["TOOLITEMID"].ToString();
            tbxItemName.Text = dt.Rows[0]["ITEMNAME"].ToString();
            ddlSectionCategory.SelectedValue = dt.Rows[0]["SERVICECATEGORYID"].ToString();
            ddlUnit.SelectedValue = dt.Rows[0]["SCMUNITCODE"].ToString();

            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    //protected void btnDelete_Command(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        int ID = Convert.ToInt32(e.CommandArgument);
    //        new Common().Delete("RVS_INSPECTIONITEMS", ID);
    //        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
    //        BindData();
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteLog(ex);
    //        MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
    //    }
    //}
    //protected void chkInspectinType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindData();
    //}

    //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindData();
    //}
}