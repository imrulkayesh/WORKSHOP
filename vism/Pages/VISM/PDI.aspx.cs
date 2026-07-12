using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PDI : BasePage
{
    dalPDI obj = new dalPDI();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            MessageController.Clear(Page);
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
            LoadProblems(0);
            LoadTechnician();
            BindData();
        }

    }
    private void LoadDDL()
    {
        ddlcategoryid.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlcategoryid.DataBind();
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();
    }
    private void LoadProblems(int PDIID)
    {
        if (!string.IsNullOrEmpty(ddlcategoryid.SelectedValue))
        {
            rptInspection.DataSource = obj.GetInspectionItems(0);
            rptInspection.DataBind();
        }
        if (PDIID > 0)
        {
            DataTable dt = obj.GetInspectionItems(PDIID);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem r in rptInspection.Items)
                {
                    HiddenField itemId = (HiddenField)r.FindControl("hdnInsItemId");
                    if (itemId.Value == dr["ID"].ToString())
                    {
                        CheckBox chk = (CheckBox)r.FindControl("chkrow");
                        RadioButtonList rd = (RadioButtonList)r.FindControl("rdbStatus");
                        TextBox remarks = (TextBox)r.FindControl("tbxInsRemarks");
                        rd.SelectedValue = dr["STATUSID"].ToString();
                        remarks.Text = dr["REMARKS"].ToString();
                        chk.Checked = true;
                    }
                }
            }
        }

    }
    private void LoadTechnician()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
        {

            ddlTechnician.DataSource = new Common().GetAllTechnician(Convert.ToInt32(ddlworkshopid.SelectedValue));
            ddlTechnician.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            int PDid = obj.Insert(Convert.ToInt32(hdnJobID.Value), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxremark.Text);
            if (PDid > 0)
            {
                InsertPDIDetails(PDid);
            }
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
    private void InsertPDIDetails(int PDid)
    {
        obj.DeletePDIDetails(PDid);
        foreach (RepeaterItem r in rptInspection.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField itemId = (HiddenField)r.FindControl("hdnInsItemId");
                RadioButtonList status = (RadioButtonList)r.FindControl("rdbStatus");
                TextBox remarks = (TextBox)r.FindControl("tbxInsRemarks");
                obj.InsertPDIDetails(PDid, Convert.ToInt32(itemId.Value), Convert.ToInt32(status.SelectedValue), remarks.Text);
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(hdnJobID.Value), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxremark.Text);
            InsertPDIDetails(Convert.ToInt32(ViewState["ID"]));
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
        tbxJobID.Text = "";
        tbxremark.Text = "";
        hdnJobID.Value = "0";
        LoadProblems(0);
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
        {
            DataTable dt = obj.GetAllPDI(Convert.ToInt32(ddlworkshopid.SelectedValue));
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
            GetJobID(Convert.ToInt32(dt.Rows[0]["JOBID"]));
            ddlTechnician.SelectedValue = dt.Rows[0]["TECSTAFFID"].ToString();
            tbxremark.Text = dt.Rows[0]["REMARKS"].ToString();
            LoadProblems(ID);
            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"PDIView.aspx?PDIID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_PDI", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

    protected void ddlworkshopid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTechnician();
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchJob(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT J.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME JOBNO FROM RVS_JOBCARD J
                INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(J.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%')) AND J.ISOPEN=1 AND W.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "'  AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["JOBNO"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }
    protected void tbxJobID_TextChanged(object sender, EventArgs e)
    {
        string b = tbxJobID.Text;
        string[] a = tbxJobID.Text.Trim().Split('-');
        int id = 0;
        try
        {
            id = string.IsNullOrEmpty(a[0]) ? 0 : Convert.ToInt32(a[0]);
        }
        catch
        {
            id = 0;
        }
        GetJobID(id);
        tbxJobID.Text = b;
    }
    private void GetJobID(int JobId)
    {
        try
        {
            DataTable dt = new dalJobCard().GetJobCardPrint(JobId);
            if (dt.Rows.Count > 0)
            {
                ddlcategoryid.SelectedValue = dt.Rows[0]["SERVICETYPEID"].ToString();
                ddlworkshopid.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
                tbxJobID.Text = dt.Rows[0]["ID"].ToString() + "-" + dt.Rows[0]["CHASSISNO"].ToString() + "-" + dt.Rows[0]["CUSTOMERNAME"].ToString();
                hdnJobID.Value = dt.Rows[0]["ID"].ToString();
            }
            else
            {
                tbxJobID.Text = "";
                hdnJobID.Value = "0";
                MessageController.Show("Job Card Not Found", MessageType.Error, Page);
            }
        }
        catch (Exception ex)
        {
            tbxJobID.Text = "";
            hdnJobID.Value = "0";
            ErrorLog.WriteLog(ex);
            MessageController.Show("Job Card Not Found", MessageType.Error, Page);
        }
    }
}