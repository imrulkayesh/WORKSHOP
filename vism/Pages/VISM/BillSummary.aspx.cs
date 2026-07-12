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



public partial class Pages_VISM_BillSummary : BasePage
{
    dalGatein obj = new dalGatein();
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
            ddlLoad();
        }
    }

    private void ddlLoad()
    {
        ListItem li = new ListItem("Select", "0");
        ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        ddlWorkShop.DataBind();
        ddlWorkShop.Items.Insert(0, li);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = obj.BillSummary(Criteria());
        rpt.DataSource = dt;
        rpt.DataBind();
        lblfromDate.Text = tbxfromdate.Text;
        lbltoDate.Text = tbxtodate.Text;
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
                cmd.CommandText = @"SELECT V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME VEHICLE,V.REGNO regno FROM RVS_VEHICLE V
                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%'))  AND ROWNUM <= 20";
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

    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    {
       
    }

    private string Criteria()
    {
        string val = "";
        if (!string.IsNullOrEmpty(ddlWorkShop.SelectedValue) && ddlWorkShop.SelectedValue != "0")
        {
            val += " AND J.WORKSHOPID=" + ddlWorkShop.SelectedValue;
        }
        if (!string.IsNullOrEmpty(tbxRecVeh.Text.Split('-')[0]) && tbxRecVeh.Text.Split('-')[0] != "0")
        {
            val += " AND J.VEHICLEID=" + tbxRecVeh.Text.Split('-')[0];
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(J.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        else if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND trunc(J.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
        }

        

        return val;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }

    protected void btnBillPrint_Command(object sender, CommandEventArgs e)
    {
      //  string ID = e.CommandArgument.ToString();


        ImageButton objImage = (ImageButton)sender;

        string[] commandArgs = objImage.CommandArgument.ToString().Split(new char[] { ',' });
        string vID = commandArgs[0];
        string jID = commandArgs[1];

        string qry = @"SELECT * from V_RVS_BILL_NEW_OK
WHERE VEHICLEID='" + vID + "' and JOBID='"+jID+"'";
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/pages/VISM/CrReport/CrPrintBill.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);
    }
}
