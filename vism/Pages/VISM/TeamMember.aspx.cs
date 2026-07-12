using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_TeamMember : BasePage
{
    dalTeamMember obj = new dalTeamMember();
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
            //   LoadDDL();
            LoadTeam();
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
                ddlWorkshop.DataSource = dm.ExecuteQuery(qry);
                ddlWorkshop.DataBind();

            }
            else
            {
                qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws  ORDER BY WORKSHOP ASC";
                ddlWorkshop.DataSource = dm.ExecuteQuery(qry);
                ddlWorkshop.DataBind();
                ddlWorkshop.Items.Insert(0, li);
            }


        }
        catch
        {

            //
        }

    }


    private void LoadDDL()
    {
        ddlWorkshop.DataSource = new Common().GetAll("RVS_WORKSHOP");
        ddlWorkshop.DataBind();
    }
    private void LoadTeam()
    {
        if (!string.IsNullOrEmpty(ddlWorkshop.SelectedValue))
        {
            ddlTeamid.DataSource = new dalTeam().GetAll(Convert.ToInt32(ddlWorkshop.SelectedValue));
            ddlTeamid.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Insert(Convert.ToInt32(ddlTeamid.SelectedValue), tbxstaffid.Text, tbxFdate.Text, Convert.ToInt32(ddlWorkshop.SelectedValue));
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(ddlTeamid.SelectedValue), tbxstaffid.Text, tbxFdate.Text, Convert.ToInt32(ddlWorkshop.SelectedValue), Convert.ToInt32(ChkStatus.Checked));
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
        tbxstaffid.Text = "";
        tbxFdate.Text = "";

        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlTeamid.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlTeamid.SelectedValue));
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
            ddlWorkshop.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
            LoadTeam();
            ddlTeamid.SelectedValue = dt.Rows[0]["TEAMID"].ToString();
            tbxstaffid.Text = dt.Rows[0]["STAFFID"].ToString();
            tbxFdate.Text = Convert.ToDateTime(dt.Rows[0]["FDATE"]).ToString("dd/MM/yyyy ");

            ChkStatus.Checked = Convert.ToBoolean(dt.Rows[0]["ISACTIVE"]);

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
            new Common().DeleteTeam("RVS_TEAMMEMBER", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
    protected void ddlTeamid_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTeam();
        BindData();
    }
}
