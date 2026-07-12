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

public partial class Pages_VISM_ConsumptionReportSingleVehicle : BasePage
{
    dalItem obj = new dalItem();
    dalVehicle ob = new dalVehicle();
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
            ddlLoad();
            LoadData();
        }

    }
    private void ddlLoad()
    {
        ListItem li = new ListItem("Select", "0");
        ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        ddlWorkShop.DataBind();
        ddlWorkShop.Items.Insert(0, li);
    }

    private void LoadData()
    {
        if (!string.IsNullOrEmpty(tbxVehicle.Text) && !string.IsNullOrEmpty(tbxfromdate.Text) && !string.IsNullOrEmpty(tbxtodate.Text) && !string.IsNullOrEmpty(ddlWorkShop.SelectedValue))
        {
            DataTable dt = obj.GetConsumptionDetails(Criteria());
            if (dt.Rows.Count > 0)
            {
                lblCompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();
                lblRegistration.Text = dt.Rows[0]["REGNO"].ToString();
                lblChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                lblWorkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();

                lblfromDate.Text = tbxfromdate.Text;
                lbltoDate.Text = tbxtodate.Text;
            }
        }
        else
        {
            MessageController.Show("Data Not Found !", MessageType.Warning, Page);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(tbxVehicle.Text) && !string.IsNullOrEmpty(tbxfromdate.Text) && !string.IsNullOrEmpty(tbxtodate.Text) && !string.IsNullOrEmpty(ddlWorkShop.SelectedValue))
        {
            DataTable dt = obj.GetConsumptionDetails(Criteria());
            rpt.DataSource = dt;
            rpt.DataBind();
            LoadData();
        }
        else
        {
            MessageController.Show("Data Not Found !", MessageType.Warning, Page);
        }
    }
    private string Criteria()
    {
        string val = "";

        if (!string.IsNullOrEmpty(tbxVehicle.Text))
        {
            val += "J.VEHICLEID=" + tbxVehicle.Text;
        }
        if (!string.IsNullOrEmpty(ddlWorkShop.SelectedValue) && ddlWorkShop.SelectedValue != "0")
        {
            val += " AND J.WORKSHOPID=" + ddlWorkShop.SelectedValue;
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(J.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " trunc(J.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
        }

        return val;
    }

    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchVehicle(string prefixText, int count)
    {
        List<string> vehicle = new List<string>();
        DataTable dt = DatabaseManagerORCL.StaticDT(@"SELECT V.ID ||'-'||V.REGNO||'-'||V.CHASSISNO VEHICLE FROM RVS_VEHICLE V  where LOWER( V.ID ||'-'||V.REGNO||'-'||V.CHASSISNO ) like LOWER('%" + prefixText + "%') AND ROWNUM <= 20");
        foreach (DataRow dr in dt.Rows)
        {
            vehicle.Add(dr["VEHICLE"].ToString());
        }
        return vehicle;
    }

    protected void tbxVehicle_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] vehicleId = tbxVehicle.Text.Trim().Split('-');
            DataTable dt = ob.GetById(Convert.ToInt32(vehicleId[0]));
            if (dt.Rows.Count > 0)
            {

                tbxVehicle.Text = dt.Rows[0]["ID"].ToString();
                lblRegNo.Text = dt.Rows[0]["REGNO"].ToString();
                lblChissNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            tbxVehicle.Text = "";
        }
    }
}
