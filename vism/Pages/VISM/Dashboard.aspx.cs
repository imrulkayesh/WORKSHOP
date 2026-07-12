using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;
using System.Web.Services;
using System.Text;
using System.IO;

public partial class Pages_VISM_Dashboard : System.Web.UI.Page
{
    dalGatein obj = new dalGatein();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadChart();
        }
    }


    private void LoadChart()
    {
        //string prms = Session["PRMS"].ToString();
        //if (prms == "Y")
        //{
        //    BindDivChart();
        //}
        //else
        //{

        //}

        #region Session Check
        string url = "/Pages/Admin/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
        if (SessionManager.SessionName.StaffId == null)
        {
            dalCommon.LoginReturnUrl(url);
        }
        #endregion
        #region WorkShop Fetch
        string query = "SELECT DISTINCT VW.WORKSHOPID FROM EM_EMPLOYEE EM LEFT OUTER JOIN RVS_VISMWH VW ON EM.WORKSHOPID = VW.WORKSHOPID WHERE EM.STAFFID='" + SessionManager.SessionName.UserId + "'";
        string wid = dm.ExecuteScalar(query);
        #endregion

        string RI = SessionManager.SessionName.RoleId.ToString();

        BindDivChart(wid, RI);
        dashboarddetails(wid, RI);
        dashboardData();
        dashboardLastMonthJobData();
    }
    private void dashboardData()
    {
        DataTable dt_all = new DataTable();
        string sql_1 = "";

        #region All Data         
        sql_1 = @"
        SELECT 
        (SELECT COUNT(ID)
         FROM RVS_WORKORDERMASTER
         WHERE ISACTIVE = 1 AND TRUNC(CREATEDATE) = TRUNC(SYSDATE)) AS TODAY_WORK_ORDER,

        (SELECT COUNT(ID)
         FROM RVS_WORKORDERMASTER
         WHERE ISACTIVE = 1 AND TRUNC(CREATEDATE) BETWEEN TRUNC(SYSDATE, 'MM') 
         AND TRUNC(LAST_DAY(SYSDATE))) AS THIS_MONTH_WORK_ORDER,

        (SELECT COUNT(ID)
         FROM RVS_WORKORDERMASTER
         WHERE ISACTIVE = 1 AND TRUNC(CREATEDATE) BETWEEN TRUNC(ADD_MONTHS(SYSDATE, -1), 'MM')  
         AND LAST_DAY(ADD_MONTHS(SYSDATE, -1))) AS LAST_MONTH_WORK_ORDER
         FROM DUAL";

        dt_all = dm.ExecuteQuery(sql_1);
        #endregion

        #region Daily Job Data
        if (dt_all != null && dt_all.Rows.Count > 0)
        {
            DataRow row = dt_all.Rows[0];

            string JRT = row["TODAY_WORK_ORDER"].ToString();
            string JDT = row["THIS_MONTH_WORK_ORDER"].ToString();
            string JP = row["LAST_MONTH_WORK_ORDER"].ToString();

            lblJI.Text = JRT;
            lblJO.Text = JDT;
            lblper.Text = JP;
        }
        #endregion
    }
    private void dashboardLastMonthJobData()
    {
        string sql_1 = @"
        SELECT
          'LAST_MONTH' AS PERIOD,
          COUNT(J.ID) AS JOB_RECEIVED,
          SUM(CASE WHEN J.ISOPEN = 0 THEN 1 ELSE 0 END) AS JOB_DONE,
          COUNT(J.ID) - SUM(CASE WHEN J.ISOPEN = 0 THEN 1 ELSE 0 END) AS JOB_PENDING
        FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        WHERE J.ISACTIVE = 1
          AND PI.ISACTIVE = 1
          AND TRUNC(J.CREATEDATE) BETWEEN TRUNC(ADD_MONTHS(SYSDATE, -1), 'MM') 
                                      AND LAST_DAY(ADD_MONTHS(SYSDATE, -1))

        UNION ALL

        SELECT
          'TODAY' AS PERIOD,
          COUNT(J.ID) AS JOB_RECEIVED,
          SUM(CASE WHEN J.ISOPEN = 0 AND TRUNC(J.UPDATEDATE) = TRUNC(SYSDATE) THEN 1 ELSE 0 END) AS JOB_DONE,
          COUNT(J.ID) - SUM(CASE WHEN J.ISOPEN = 0 AND TRUNC(J.UPDATEDATE) = TRUNC(SYSDATE) THEN 1 ELSE 0 END) AS JOB_PENDING
        FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        WHERE J.ISACTIVE = 1
          AND PI.ISACTIVE = 1
          AND TRUNC(J.CREATEDATE) = TRUNC(SYSDATE)

        UNION ALL

        SELECT
          'THIS_MONTH' AS PERIOD,
          COUNT(J.ID) AS JOB_RECEIVED,
          SUM(CASE WHEN J.ISOPEN = 0 THEN 1 ELSE 0 END) AS JOB_DONE,
          COUNT(J.ID) - SUM(CASE WHEN J.ISOPEN = 0 THEN 1 ELSE 0 END) AS JOB_PENDING
        FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        WHERE J.ISACTIVE = 1
          AND PI.ISACTIVE = 1
          AND TRUNC(J.CREATEDATE) BETWEEN TRUNC(SYSDATE, 'MM') AND LAST_DAY(SYSDATE)
    ";

        DataTable dt_all = dm.ExecuteQuery(sql_1);

        if (dt_all != null && dt_all.Rows.Count > 0)
        {
            rpt_JR.DataSource = dt_all;
            rpt_JR.DataBind();

            // Optional: set a total if needed (sum across rows)
            //int totalJobs = dt_all.AsEnumerable().Sum(row => Convert.ToInt32(row["JOB_RECEIVED"]));
            //lblJRTotal.Text = totalJobs.ToString();
        }
    }

    private void dashboarddetails(string wid, string RI)
    {
        DataTable dt_all = new DataTable();
        string sql_1 = "";
        if (RI == "31" || RI == "60")
        {
            #region All Data         
            sql_1 = @"SELECT   R.DID,
         R.DNAME,
         R.DTYPE,
         NVL (R.RVS_C1, 0) RVS_C1,
         R.RVS_C1VALUE,
         R.RVS_C2,
         NVL (R.RVS_C2VALUE, 0) RVS_C2VALUE,
         R.RVS_C3,
         NVL (R.RVS_C3VALUE, 0) RVS_C3VALUE,
         R.RVS_C4,
         NVL (R.RVS_C4VALUE, 0) RVS_C4VALUE,
         R.RVS_C5,
         NVL (R.RVS_C5VALUE, 0) RVS_C5VALUE,
         R.RVS_C6,
         R.RVS_C6VALUE,
         R.RVS_C7,
         R.RVS_C7VALUE,
         R.RVS_C8,
         R.RVS_C8VALUE,
         R.RVS_C9,
         R.RVS_C9VALUE,
         R.RVS_C10,
         R.RVS_C10VALUE,
         R.LUDATE
  FROM   YA.RVS_DASHBOARD_WKSP R where R.DWKSP = '" + wid + "'";
            dt_all = dm.ExecuteQuery(sql_1);

            #endregion
        }
        else if (RI == "1" || RI == "38")
        {
            #region All Data
            sql_1 = @"SELECT   R.DID,
         R.DNAME,
         R.DTYPE,
         NVL (R.RVS_C1, 0) RVS_C1,
         R.RVS_C1VALUE,
         R.RVS_C2,
         NVL (R.RVS_C2VALUE, 0) RVS_C2VALUE,
         R.RVS_C3,
         NVL (R.RVS_C3VALUE, 0) RVS_C3VALUE,
         R.RVS_C4,
         NVL (R.RVS_C4VALUE, 0) RVS_C4VALUE,
         R.RVS_C5,
         NVL (R.RVS_C5VALUE, 0) RVS_C5VALUE,
         R.RVS_C6,
         R.RVS_C6VALUE,
         R.RVS_C7,
         R.RVS_C7VALUE,
         R.RVS_C8,
         R.RVS_C8VALUE,
         R.RVS_C9,
         R.RVS_C9VALUE,
         R.RVS_C10,
         R.RVS_C10VALUE,
         R.LUDATE
  FROM   YA.RVS_DASHBOARD R";
            dt_all = dm.ExecuteQuery(sql_1);

            #endregion
        }
        else
        {
            return;
        }

        #region CO Data
        //DataRow[] co_rows = dt_all.Select("DTYPE = 'CO' AND RVS_C2VALUE<>'0'");
        DataTable dt_co = new DataTable();
        //dt_co.Columns.Add("RVS_C1VALUE");
        //dt_co.Columns.Add("RVS_C2VALUE");
        //foreach (DataRow row in co_rows)
        //{
        //    dt_co.Rows.Add(row["RVS_C1VALUE"], row["RVS_C2VALUE"]);
        //}
        //rpt_co.DataSource = dt_co;
        //rpt_co.DataBind();
        ////decimal totalCO =Math.Round( (dt_co.Select().Sum(p => Convert.ToDecimal(p["RVS_C2VALUE"])))/1000,2);
        //decimal totalCO = dt_co.Select().Sum(p => Convert.ToDecimal(p["RVS_C2VALUE"]));
        //(rpt_co.Controls[rpt_co.Controls.Count - 1].Controls[0].FindControl("lblCOTotal") as Label).Text = totalCO.ToString();
        #endregion
        #region STOCK Data
        //DataRow[] stock_rows = dt_all.Select("DTYPE = 'STOCK' AND RVS_C2VALUE<>'0'");
        //DataTable dt_stock = new DataTable();
        //dt_stock.Columns.Add("RVS_C1VALUE");
        //dt_stock.Columns.Add("RVS_C2VALUE");
        //foreach (DataRow row in stock_rows)
        //{
        //    dt_stock.Rows.Add(row["RVS_C1VALUE"], row["RVS_C2VALUE"]);
        //}
        //dt_co.Rows.Clear();
        //foreach (DataRow row in stock_rows)
        //{
        //    dt_co.Rows.Add(row["RVS_C1VALUE"], row["RVS_C2VALUE"]);
        //}
        //rpt_stock.DataSource = dt_co;
        //rpt_stock.DataBind();
        ////decimal totalST = Math.Round((dt_co.Select().Sum(p => Convert.ToDecimal(p["RVS_C2VALUE"])))/1000,2);
        //decimal totalST = dt_co.Select().Sum(p => Convert.ToDecimal(p["RVS_C2VALUE"]));
        //(rpt_stock.Controls[rpt_stock.Controls.Count - 1].Controls[0].FindControl("lblSTTotal") as Label).Text = totalST.ToString();
        #endregion
        #region Job Receive Data
        //DataRow[] JR_rows = dt_all.Select("DTYPE = 'JR' AND RVS_C2VALUE<>'0'");
        //dt_co.Rows.Clear();
        //foreach (DataRow row in JR_rows)
        //{
        //    dt_co.Rows.Add(row["RVS_C1VALUE"], row["RVS_C2VALUE"]);
        //}
        //rpt_JR.DataSource = dt_co;
        //rpt_JR.DataBind();
        //int totalJR = dt_co.Select().Sum(p => Convert.ToInt32(p["RVS_C2VALUE"]));
        //(rpt_JR.Controls[rpt_JR.Controls.Count - 1].Controls[0].FindControl("lblJRTotal") as Label).Text = totalJR.ToString();
        #endregion
        #region Job Delivery Data
        //DataRow[] JD_rows = dt_all.Select("DTYPE = 'JD' AND RVS_C2VALUE<>'0'");
        //dt_co.Rows.Clear();
        //foreach (DataRow row in JD_rows)
        //{
        //    dt_co.Rows.Add(row["RVS_C1VALUE"], row["RVS_C2VALUE"]);
        //}
        //rpt_JD.DataSource = dt_co;
        //rpt_JD.DataBind();
        //int totalJD = dt_co.Select().Sum(p => Convert.ToInt32(p["RVS_C2VALUE"]));
        //(rpt_JD.Controls[rpt_JD.Controls.Count - 1].Controls[0].FindControl("lblJDTotal") as Label).Text = totalJD.ToString();
        #endregion
        #region Job Staying Data
        //DataRow[] JS_rows = dt_all.Select("DTYPE = 'JS' AND RVS_C2VALUE<>'0'");
        //dt_co.Rows.Clear();
        //foreach (DataRow row in JS_rows)
        //{
        //    dt_co.Rows.Add(row["RVS_C1VALUE"], row["RVS_C2VALUE"]);
        //}
        //rpt_JS.DataSource = dt_co;
        //rpt_JS.DataBind();
        //int totalJS = dt_co.Select().Sum(p => Convert.ToInt32(p["RVS_C2VALUE"]));
        //(rpt_JS.Controls[rpt_JS.Controls.Count - 1].Controls[0].FindControl("lblTotal") as Label).Text = totalJS.ToString();
        #endregion
        #region  Daily Job Data
        //DataRow[] DI_rows = dt_all.Select("DTYPE = 'DJC' AND DNAME ='DAILY JOB'");
        ////DataTable dt_djc = new DataTable();
        ////dt_djc.Columns.Add("RVS_C2VALUE");
        ////dt_djc.Columns.Add("RVS_C3VALUE");
        ////dt_djc.Columns.Add("RVS_C4VALUE");
        ////dt_djc.Rows.Add(DI_rows[0]["RVS_C2VALUE"], DI_rows[0]["RVS_C3VALUE"], DI_rows[0]["RVS_C4VALUE"]);

        //string JRT = DI_rows.Select(row => (string)row["RVS_C2VALUE"]).First();
        //string JDT = DI_rows.Select(row => (string)row["RVS_C3VALUE"]).First();
        //string JP = DI_rows.Select(row => (string)row["RVS_C4VALUE"]).First();
        //string DATE = DI_rows.Select(row => (DateTime)row["LUDATE"]).First().ToString();

        //lblJI.Text = JRT.ToString();
        //lblJO.Text = JDT.ToString();
        //lblper.Text = JP.ToString();
        //UDATE.Text = DATE.ToString();
        //UDATE1.Text = UDATE.Text;
        #endregion
        #region Monthly Job Data
        DataRow[] MI_rows = dt_all.Select("DTYPE = 'MJC' AND DNAME ='MONTHLY JOB'");
        //DataTable dt_djc = new DataTable();
        //dt_djc.Columns.Add("RVS_C2VALUE");
        //dt_djc.Columns.Add("RVS_C3VALUE");
        //dt_djc.Columns.Add("RVS_C4VALUE");
        //dt_djc.Rows.Add(DI_rows[0]["RVS_C2VALUE"], DI_rows[0]["RVS_C3VALUE"], DI_rows[0]["RVS_C4VALUE"]);

        //string MJRT = MI_rows.Select(row => (string)row["RVS_C2VALUE"]).First();
        //string MJDT = MI_rows.Select(row => (string)row["RVS_C3VALUE"]).First();
        //string MJPP = MI_rows.Select(row => (string)row["RVS_C4VALUE"]).First();
        //string MDATE = MI_rows.Select(row => (DateTime)row["LUDATE"]).First().ToString();

        //MJR.Text = MJRT.ToString();
        //MJD.Text = MJDT.ToString();
        //MJP.Text = MJPP.ToString();
        //Mtime.Text = MDATE.ToString();
        #endregion

    }

    private void BindDivChart(string wid, string RI)
    {
        #region WKSP Wise Job Data
        //string query = "select vw.workshopid from em_employee em,RVS_VISMWH vw where em.WORKSHOPID=vw.ID and staffid='" + SessionManager.SessionName.UserId + "'";
        //string wid = dm.ExecuteScalar(query);



        //string RI = SessionManager.SessionName.RoleId.ToString(); ;
        //string cid = Session["COUNTRYID"].ToString();
        string qry = "";
        if (RI == "31" || RI == "60")
        {
            qry = @"select R.WORKSHOP,R.RV Receive_Vehicel,R.SV Stayig_Vehicle,R.DV Delivery_Vehicle from ya.V_RVS_RDS R
WHERE R.WORKSHOPID='" + wid + "'";

            DataTable dt = new DataTable();
            dt = dm.ExecuteQuery(qry);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            int c = ds.Tables[0].Rows.Count;
            DataTable dsChartData = ds.Tables[0];
            DataTable sdsChartData = new DataTable();
            sdsChartData.Columns.Add(dsChartData.Columns[0].ColumnName);
            sdsChartData.Columns.Add(dsChartData.Columns[1].ColumnName);
            sdsChartData.Columns.Add(dsChartData.Columns[2].ColumnName);
            sdsChartData.Columns.Add(dsChartData.Columns[3].ColumnName);
            for (int i = 0; i < c; i++)
            {
                DataRow dr = sdsChartData.NewRow();
                dr[0] = dsChartData.Rows[i][0];
                dr[1] = Convert.ToDecimal("0" + dsChartData.Rows[i][1]);
                dr[2] = Convert.ToDecimal("0" + dsChartData.Rows[i][2]);
                dr[3] = Convert.ToDecimal("0" + dsChartData.Rows[i][3]);
                sdsChartData.Rows.Add(dr);
            }
            StringBuilder strScript = new StringBuilder();
            try
            {

                strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['" + sdsChartData.Columns[0] + "', '" + sdsChartData.Columns[1] + "','" + sdsChartData.Columns[2] + "','" + sdsChartData.Columns[3] + "'],");

                foreach (DataRow row in sdsChartData.Rows)
                {
                    strScript.Append("['" + row[0] + "'," + row[1] + "," + row[2] + "," + row[3] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append("var options = { title : '', vAxis: {title: 'Job Function'},  hAxis: {title: 'Workshop'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");

                ltScripts.Text = strScript.ToString();
            }
            catch
            {
            }
            finally
            {
                sdsChartData.Dispose();
                dsChartData.Dispose();
                strScript.Clear();
            }
        }

        else if (RI == "1" || RI == "38")
        {
            qry = @"select R.WORKSHOP,R.RV Receive_Vehicel,R.SV Stayig_Vehicle,R.DV Delivery_Vehicle from ya.V_RVS_RDS R";

            DataTable dt = new DataTable();
            dt = dm.ExecuteQuery(qry);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            int c = ds.Tables[0].Rows.Count;
            DataTable dsChartData = ds.Tables[0];
            DataTable sdsChartData = new DataTable();
            sdsChartData.Columns.Add(dsChartData.Columns[0].ColumnName);
            sdsChartData.Columns.Add(dsChartData.Columns[1].ColumnName);
            sdsChartData.Columns.Add(dsChartData.Columns[2].ColumnName);
            sdsChartData.Columns.Add(dsChartData.Columns[3].ColumnName);
            for (int i = 0; i < c; i++)
            {
                DataRow dr = sdsChartData.NewRow();
                dr[0] = dsChartData.Rows[i][0];
                dr[1] = Convert.ToDecimal("0" + dsChartData.Rows[i][1]);
                dr[2] = Convert.ToDecimal("0" + dsChartData.Rows[i][2]);
                dr[3] = Convert.ToDecimal("0" + dsChartData.Rows[i][3]);
                sdsChartData.Rows.Add(dr);
            }
            StringBuilder strScript = new StringBuilder();
            try
            {

                strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['" + sdsChartData.Columns[0] + "', '" + sdsChartData.Columns[1] + "','" + sdsChartData.Columns[2] + "','" + sdsChartData.Columns[3] + "'],");

                foreach (DataRow row in sdsChartData.Rows)
                {
                    strScript.Append("['" + row[0] + "'," + row[1] + "," + row[2] + "," + row[3] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append("var options = { title : '', vAxis: {title: 'Job Function'},  hAxis: {title: 'Workshop'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");

                ltScripts.Text = strScript.ToString();
            }
            catch
            {
            }
            finally
            {
                sdsChartData.Dispose();
                dsChartData.Dispose();
                strScript.Clear();
            }
        }

        else
        {
            // qry = @"select R.WORKSHOP,R.RV Receive_Vehicel,R.SV Stayig_Vehicle,R.DV Delivery_Vehicle from ya.V_RVS_RDS R";
        }
        #endregion
    }
}