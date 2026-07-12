using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_SingleJobDetails : BasePage
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
            LoadData();
        }

    }

    private void LoadData()
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = obj.SingleJobDetails(tbxJobId.Text);
        if (dt.Rows.Count > 0)
        {
            // When date Fild blank Update .........
            DateTime odate = DateTime.MinValue;
            DateTime.TryParse(dt.Rows[0]["UPDATEDATE"].ToString(), out odate);
            if (odate == DateTime.MinValue)
            {
                lblUpdateDate.Text = "";
            }
            else
            {
                lblUpdateDate.Text = odate.ToString();//("- Time :" + odate.ToString("HH:MM");
            }

            lblBranchAddress.Text = dt.Rows[0]["WADDRESS"].ToString();
            lblworkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
            lblJobNo.Text = dt.Rows[0]["JOBID"].ToString();
            lblDate.Text = Convert.ToDateTime(dt.Rows[0]["CDS"]).ToString();//("dd/MM/yyyy") + "  - Time :" + Convert.ToDateTime(dt.Rows[0]["CDS"]).ToString("HH:MM");
            //lblUpdateDate.Text = Convert.ToDateTime(dt.Rows[0]["UPDATEDATE"]).ToString("dd/MM/yyyy") + " -Time :"+ Convert.ToDateTime(dt.Rows[0]["UPDATEDATE"]).ToString("HH:MM"); 
            lblCustName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
            lblOfficeAddress.Text = dt.Rows[0]["BADDRESS"].ToString();
            lblDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
            lblDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
            lblLpg.Text = dt.Rows[0]["SERVICETYPE"].ToString();
            lblMobile.Text = dt.Rows[0]["MOBILE"].ToString();
            lblRegNo.Text = dt.Rows[0]["REGNO"].ToString();
            lblChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
            lblMeterReading.Text = dt.Rows[0]["MILLAGE"].ToString();
            lblEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
            lblEngineModuleNo.Text = dt.Rows[0]["MODELNAME"].ToString();
            lblcompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();

            // lblPreparedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
            rpt.DataSource = new dalPreInspection().GetProblems(Convert.ToInt32(dt.Rows[0]["PIID"]));
            rpt.DataBind();
        }
        else
        {
            MessageController.Show("Data Not Found !", MessageType.Warning, Page);
        }
    }
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchVehicle(string prefixText, int count)
    {
        List<string> jobCard = new List<string>();
        DataTable dt = DatabaseManagerORCL.StaticDT(@"SELECT J.ID||'-'|| V.REGNO ||'-'||V.CHASSISNO ||'-'|| C.CUSTOMERNAME ||'-'||J.CREATEDATE  JOBCARD FROM RVS_JOBCARD J
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID = V.CUSTOMERID  where LOWER( J.ID||'-'|| V.REGNO ||'-'||V.CHASSISNO ||'-'|| C.CUSTOMERNAME ||'-'||J.CREATEDATE ) like LOWER('%" + prefixText + "%') AND ROWNUM <= 20");
        foreach (DataRow dr in dt.Rows)
        {
            jobCard.Add(dr["JOBCARD"].ToString());
        }
        return jobCard;
    }

    protected void tbxVehicle_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] jobId = tbxJobId.Text.Trim().Split('-');
            DataTable dt = ob.GetByJobId(Convert.ToInt32(jobId[0]));
            if (dt.Rows.Count > 0)
            {
                tbxJobId.Text = dt.Rows[0]["ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            tbxJobId.Text = "";
        }
    }
}
