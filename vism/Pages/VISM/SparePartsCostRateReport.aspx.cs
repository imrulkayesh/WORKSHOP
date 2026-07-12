using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

using System.IO;
using System.Drawing;

public partial class Pages_VISM_SparePartsCostRateReport : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
        }
    }

    private void LoadDdl()
    {
        string BI = SessionManager.SessionName.BusinessCentetid.ToString();
        ListItem li = new ListItem("Select", "");
        string qry = "";
        if (BI == "705")
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
            ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
            ddlSendLoc.DataBind();
            //ddlSendLoc.Items.Insert(0, li);


            string qry1 = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
         ddlSendLoc.SelectedValue + "' order by VISMWHNAME";
            ddlSendWh.DataSource = dm.ExecuteQuery(qry1);
            ddlSendWh.DataBind();
            ddlSendCom.Items.Clear();
            ddlSendWh.Enabled = true;
        }
        else
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
            ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
            ddlSendLoc.DataBind();
            ddlSendLoc.Items.Insert(0, li);
        }



    }

    protected void ddlSendLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlSendLoc.SelectedValue + "' order by VISMWHNAME";
        ddlSendWh.DataSource = dm.ExecuteQuery(qry);
        ddlSendWh.DataBind();
        ddlSendWh.Items.Insert(0, li);
        ddlSendCom.Items.Clear();
        ddlSendWh.Enabled = true;
        //
    }

    protected void ddlSendWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
where wh.ID='" + ddlSendWh.SelectedValue + "'";
        DataTable val = dm.ExecuteQuery(qry);
        ListItem li = new ListItem(val.Rows[0]["TEXT"].ToString(), val.Rows[0]["ID"].ToString());
        ddlSendCom.DataSource = null;
        ddlSendCom.DataBind();
        ddlSendCom.Items.Insert(0, li);
    }

    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tbxRecVeh.Text))
        {
            ddlRecvLoc.Items.Clear();
            ddlRecvCom.Items.Clear();
            return;
        }
        string qry = @"SELECT VH.BUSINESSCENTERID ID,LO.CENTERNAME TEXT,VH.COMPANYID CID,CO.COMPANYNAME CTEXT,VH.ID REGID,VH.REGNO regno  FROM RVS_VEHICLE VH
INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
WHERE VH.ID='" + tbxRecVeh.Text.Split('-')[0] + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        ListItem li = new ListItem(dt.Rows[0]["TEXT"].ToString(), dt.Rows[0]["ID"].ToString());
        ddlRecvLoc.DataSource = null;
        ddlRecvLoc.DataBind();
        ddlRecvLoc.Items.Insert(0, li);
        li = new ListItem(dt.Rows[0]["CTEXT"].ToString(), dt.Rows[0]["CID"].ToString());
        ddlRecvCom.DataSource = null;
        ddlRecvCom.DataBind();
        ddlRecvCom.Items.Insert(0, li);
        ddlSendLoc.Enabled = true;

        li = new ListItem(dt.Rows[0]["REGNO"].ToString(), dt.Rows[0]["REGID"].ToString());
        ddlVehicleRegNo.DataSource = null;
        ddlVehicleRegNo.DataBind();
        ddlVehicleRegNo.Items.Insert(0, li);
        ddlVehicleRegNo.Enabled = true;
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

    protected void btnReset_OnClick(object sender, EventArgs e)
    {

    }
    private string criteria = "";
    private void GetCriteria()
    {

        if (ddlSendWh.SelectedValue != "") criteria += " AND (WHID='" + ddlSendWh.SelectedValue + "' OR WHID='" + ddlSendWh.SelectedValue + "')";
        if (ddlSendLoc.SelectedValue != "") criteria += " AND (LOCATIONID='" + ddlSendLoc.SelectedValue + "' OR LOCATIONID='" + ddlSendLoc.SelectedValue + "')";
        //if (tbxRecVeh.Text != "") criteria += " AND (SENDWH='" + tbxRecVeh.Text.Split('-')[0] + "' OR RECVWH='" + tbxRecVeh.Text.Split('-')[0] + "')";


    }
    private string GetQry()
    {

        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE ||'-'||ITEMNAME ITEMNAME,UNITNAME UNITNAME,ROUND((RATE*QTY)/(QTY),2) RATE, ROUND((QTY),2) QTY,ROUND((RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
FROM V_RVS_CO_AC CO
WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @" ";

        return qry;
    }
    protected void btnOpenPdf_OnClick(object sender, EventArgs e)
    {

        GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrCoDetail.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);
        criteria = "";
    }

    protected void btnDownloadPdf_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrCoDetail.rpt";
        GetCrReport(path, dt, ReportAction.DownloadPdf);
        criteria = "";
    }

    protected void btndownloadExcel_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        string qry = GetQry();
        //DataTable dt = new DataTable();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrCoDetail.rpt";
        GetCrReport(path, dt, ReportAction.DownloadExcel);
        // GetCrReport(path, dt, ReportAction.DownloadExcel);
        criteria = "";
    }

    protected void btnSparePartsCostRatelExcel_OnClick(object sender, EventArgs e)
    {
        GetCriteria();

        string qry = @" select  WHITEM.ITEMCODE,  ITEM.ITEMNAME, WHITEM.SALESRATE AVERAGE_SALES_RATE
                        from RVS_ITEMMASTER_WH WHITEM, RVS_ITEMMASTER ITEM 
                        where WHITEM.ITEMCODE = ITEM.ITEMCODE " + criteria + @" ";


        DataTable dt = dm.ExecuteQuery(qry);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ExportGridToImpToDistExcel();
    }

    protected void btndownloadSummaryExcel_OnClick(object sender, EventArgs e)
    {
        //GetCriteria();
        string qry = @"SELECT
    W.WORKSHOP AS Workshop,
    COUNT(*) AS Total_Jobs,
    SUM(CO.AMOUNT) AS Total_Spare_Parts_Cost,
    SUM(B.ACTUALBILL) AS Total_Labour_Cost,
    SUM(B.POWERCOST) AS Total_Power_Cost,
    SUM(CO.AMOUNT + B.ACTUALBILL + B.POWERCOST) AS Total_Variable_Cost,
    ROUND(SUM(((CO.AMOUNT + B.ACTUALBILL + B.POWERCOST) / 0.92) - (CO.AMOUNT + B.ACTUALBILL + B.POWERCOST)), 2) AS Total_CM,
    ROUND(SUM((CO.AMOUNT + B.ACTUALBILL + B.POWERCOST) / 0.92), 2) AS Total_Gross_Sales,
    SUM(DISCOUNTPERCENT) AS Total_Discount_Percent,
    SUM(DISCOUNTAMOUNT) AS Total_Discount_Amount,
    ROUND(SUM(((CO.AMOUNT + B.ACTUALBILL + B.POWERCOST) / 0.92) - DISCOUNTAMOUNT), 2) AS Total_Net_Sales
FROM
    RVS_BILL B
    LEFT JOIN RVS_JOBCARD J ON B.JOBID = J.ID
    LEFT JOIN RVS_WORKSHOP W ON J.WORKSHOPID = W.ID
    LEFT JOIN RVS_VEHICLE V ON J.VEHICLEID = V.ID
    INNER JOIN V_COMPANY C ON C.COMPANYID= V.COMPANYID
    LEFT JOIN V_RVS_CO_JOB_TOTAL_AMT CO ON B.JOBID = CO.JOBID
WHERE
    TRUNC(J.UPDATEDATE) BETWEEN TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY') AND TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY')  AND J.ID > 0  AND J.ISOPEN = 0  AND J.ISACTIVE = 1 GROUP BY W.WORKSHOP ";

        DataTable dt = dm.ExecuteQuery(qry);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ExportGridToImpToDistExcel1();
    }

    private void ExportGridToImpToDistExcel1()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "SalesSummaryrpt" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception Ex)
        {
            // lblError.Text = Ex.Message.ToString();
        }
    }
    private void ExportGridToImpToDistExcel()
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "SparePartsCostRateRpt" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView2.GridLines = GridLines.Both;
            GridView2.HeaderStyle.Font.Bold = true;
            GridView2.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception Ex)
        {
            // lblError.Text = Ex.Message.ToString();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}