using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_NPTForm : BasePage
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
            ViewState["JobID"] = (int)0;
            LoadDDL();
            BindData();
            BindJobData();
            LoadWorkshop();
        }

    }
    private void LoadDDL()
    {
        ddlResponsibleDepartment.DataSource = new Common().GetAll("RVS_RESPONSIBLEDEPARTMENT");
        ddlResponsibleDepartment.DataBind();

        ddlDelayCauses.DataSource = new Common().GetAll("RVS_DELAYCAUSES");
        ddlDelayCauses.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckNPTsDuplicate(ddlJobId.SelectedValue))
        {
            try
            {
                obj.InsertNPT(tbxVehicleid.Text, ddlJobId.SelectedValue, ddlDelayCauses.SelectedValue, ddlResponsibleDepartment.SelectedValue, tbxTime.Text, tbxRemarks.Text, SessionManager.SessionName.UserId,"1", ddlWorkWorkshopId.SelectedValue);

                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
                BindJobData();
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
            MessageController.Show("This Job ID already exists. Please try anothor.", MessageType.Error, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.UpdateNPT(Convert.ToInt32(ViewState["JobID"]), tbxTime.Text, Convert.ToInt32(ddlResponsibleDepartment.SelectedValue), ddlDelayCauses.SelectedValue, tbxRemarks.Text, ddlWorkWorkshopId.SelectedValue);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            BindData();
            BindJobData();
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
        tbxTime.Text = "";
        tbxRemarks.Text = "";

        btnSave.Visible = true;
        //btnEdit.Visible = false;
        tbxVehicleid.Enabled = true;
        tbxVehicleid.Text = "";
        ddlJobId.SelectedIndex = -1;
        tbxVehicleRegNumber.Text = "";
        tbxVehicleRegNumber.Enabled = true;
        ddlJobId.Enabled = true;
    }
    protected void BindData()
    {
        ddlWorkWorkshopId.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlWorkWorkshopId.DataBind();
        string selectedWorkshopId = ddlWorkWorkshopId.SelectedValue;
        qry = @"
        SELECT NPT.JOBID,D.DELAYCAUSESNAME,RD.RESPONSIBLEDEPARTMENTNAME,NPT.TIME,NPT.REMARKS
        FROM RVS_NPT NPT
        LEFT JOIN RVS_RESPONSIBLEDEPARTMENT RD ON NPT.RESPONSIBLEDEPARTMENTID = RD.ID
        LEFT JOIN RVS_DELAYCAUSES D ON NPT.DELAYCAUSESID = D.ID
        WHERE NPT.ISACTIVE = 1 AND WORKSHOPID = '" + selectedWorkshopId + @"'
        AND TRUNC (NPT.CREATEDATE) = TRUNC (SYSDATE)
        ";
        DataTable dt = new DataTable();
        dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
    }
    protected void BindJobData()
    {

        qry = @"SELECT J.ID, J.ID AS JOBID from RVS_JOBCARD J WHERE J.CREATEDATE > SYSDATE - 7 AND J.ISOPEN =0";
        DataTable dt = new DataTable();
        dt = dm.ExecuteQuery(qry);
        ddlJobId.DataSource = dt;
        ddlJobId.DataBind();
    }
    protected void LoadWorkshop()
    {
        ddlWorkWorkshopId.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlWorkWorkshopId.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int JobID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetByNPTJobId(JobID);
        if (dt.Rows.Count > 0)
        {
            tbxVehicleid.Text = dt.Rows[0]["VEHICLEID"].ToString();
            tbxTime.Text = dt.Rows[0]["TIME"].ToString();
            tbxRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            ddlResponsibleDepartment.SelectedValue = dt.Rows[0]["RESPONSIBLEDEPARTMENTID"].ToString();
            ddlDelayCauses.SelectedValue = dt.Rows[0]["DELAYCAUSESID"].ToString();
            ddlJobId.SelectedValue = dt.Rows[0]["JOBID"].ToString();
            tbxVehicleRegNumber.Text = dt.Rows[0]["REGNO"].ToString();
            ddlWorkWorkshopId.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();


            ViewState["JobID"] = JobID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
        tbxVehicleid.Enabled = false;
        tbxVehicleRegNumber.Enabled = false;
        ddlJobId.Enabled = false;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchVehicle(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME VEHICLE FROM RVS_VEHICLE V
                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%')) AND V.ISACTIVE='1'  AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["VEHICLE"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void tbxVehicleid_TextChanged(object sender, EventArgs e)
    {
        string b = tbxVehicleid.Text;
        string[] a = tbxVehicleid.Text.Trim().Split('-');
        int id = 0;
        try
        {
            id = string.IsNullOrEmpty(a[0]) ? 0 : Convert.ToInt32(a[0]);
        }
        catch
        {
            id = 0;
        }
        VehicleId(id);
        VehicleWiseJobData(id);
    }

    protected void VehicleWiseJobData(int id)
    {

        qry = @"SELECT J.ID, J.ID AS JOBID from RVS_JOBCARD J WHERE J.CREATEDATE > SYSDATE - 7 AND J.ISOPEN =0 AND J.VEHICLEID= '" + id + "' ";
        DataTable dt = new DataTable();
        dt = dm.ExecuteQuery(qry);
        ddlJobId.DataSource = dt;
        ddlJobId.DataBind();
    }

    private void VehicleId(int ID)
    {
        try
        {
            DataTable dt = new dalVehicle().GetById(ID);
            if (dt.Rows.Count > 0)
            {
                tbxVehicleid.Text = dt.Rows[0]["ID"].ToString();
                tbxVehicleRegNumber.Text = dt.Rows[0]["REGNO"].ToString();

                tbxVehicleid.Enabled = false;
              
            }
            else
            {

            }
        }
        catch
        {

        }
    }

    protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string JobIDData = ddlJobId.SelectedItem.Text;

        string qry = @"SELECT  J.VEHICLEID AS VID from RVS_JOBCARD J WHERE J.CREATEDATE > SYSDATE - 7 AND J.ISOPEN =0 AND J.ID= '" + JobIDData + @"'
        ORDER BY J.VEHICLEID  ";

        DataTable dtd = dm.ExecuteQuery(qry);

        int id = Convert.ToInt32(dtd.Rows[0]["VID"].ToString());
        VehicleId(id);
    }

}