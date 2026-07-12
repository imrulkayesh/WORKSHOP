using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Team : BasePage
{
    dalTeam obj = new dalTeam();
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
         //   loadDDl();
            BindData();
            SelectWorkshop();

        }

    }

    private void SelectWorkshop()
    {
        try
        {
            string BI = SessionManager.SessionName.BusinessCentetid.ToString();
            ListItem li = new ListItem("Select", "");
            string qry = "";
            if (BI == "705")
            {
                qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
       SessionManager.SessionName.UserId + "')";
                ddlworkshop.DataSource = dm.ExecuteQuery(qry);
                ddlworkshop.DataBind();
            }
            else
            {
                qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws  ORDER BY WORKSHOP ASC";
                ddlworkshop.DataSource = dm.ExecuteQuery(qry);
                ddlworkshop.DataBind();
                ddlworkshop.Items.Insert(0, li);
            }


        }
        catch
        {

            //
        }

    }

    private void loadDDl()
    {
        ddlworkshop.DataSource = new Common().GetAll("RVS_WORKSHOP");
        ddlworkshop.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(Convert.ToInt32(ddlworkshop.SelectedValue), tbxteam.Text))
        {
            try
            {
                obj.Insert(Convert.ToInt32(ddlworkshop.SelectedValue), tbxteam.Text, tbxIncStaffid.Text, Convert.ToInt32(tbxCorder.Text));

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
            MessageController.Show("This Team already exists. Please try anothor.", MessageType.Error, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(ddlworkshop.SelectedValue), tbxteam.Text, tbxIncStaffid.Text, Convert.ToInt32(tbxCorder.Text));
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
        tbxteam.Text = "";
        tbxIncStaffid.Text = "";
        tbxCorder.Text = "";

        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlworkshop.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlworkshop.SelectedValue));
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
            ddlworkshop.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
            tbxteam.Text = dt.Rows[0]["TEAMNAME"].ToString();
            tbxIncStaffid.Text = dt.Rows[0]["INCHARGESTAFFID"].ToString();
            tbxCorder.Text = dt.Rows[0]["CORDER"].ToString();

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
            new Common().Delete("RVS_TEAM", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }


    protected void ddlworkshop_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindData();
    }
}