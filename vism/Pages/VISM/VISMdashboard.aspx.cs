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


public partial class Pages_VISM_ItemAdd : BasePage
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
        string prms = Session["PRMS"].ToString();
        if (prms == "Y")
        {
            BindDivChart();
        }
        else
        {

        }
    }

    private void BindDivChart()
    {

        string country_code = Session["COUNTRY_CODE"].ToString();
        string cid = Session["COUNTRYID"].ToString();
        string qry = @"rd.SHORTNAME Workshop,sum(rd.RV) RV,sum(rd.DV) DV,sum(rd.SV) SV
FROM 
(
SELECT J.WORKSHOPID,WS.WORKSHOP,WS.SHORTNAME, count(J.ID) RV,0 DV,0 SV   FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        INNER JOIN RVS_WORKSHOP WS ON WS.ID=J.WORKSHOPID
        WHERE J.ISACTIVE=1 AND PI.ISACTIVE=1 --AND J.WORKSHOPID='10'
        AND trunc(J.CREATEDATE)= trunc(sysdate)
        GROUP BY J.WORKSHOPID,WS.WORKSHOP,WS.SHORTNAME 
UNION all
SELECT  J.WORKSHOPID,WS.WORKSHOP,WS.SHORTNAME,0 RV,COUNT(J.ID) DV,0 SV  FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        INNER JOIN RVS_WORKSHOP WS ON WS.ID=J.WORKSHOPID
        WHERE J.ID>0 AND J.ISOPEN=0 AND PI.ISACTIVE=1 AND J.ISACTIVE=1 --AND J.WORKSHOPID='10'
        AND trunc(J.CREATEDATE)= trunc(sysdate)   
        GROUP BY J.WORKSHOPID,WS.WORKSHOP,WS.SHORTNAME          
UNION all
SELECT J.WORKSHOPID,WS.WORKSHOP,WS.SHORTNAME,0 RV,0 DV,COUNT (J.ID) SV FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        INNER JOIN RVS_WORKSHOP WS ON WS.ID=J.WORKSHOPID
        WHERE J.ID>0 AND J.ISOPEN=1 AND J.ISACTIVE=1 AND PI.ISACTIVE=1 --AND J.WORKSHOPID='10'
        AND trunc(J.CREATEDATE)= trunc(sysdate)
        GROUP BY J.WORKSHOPID,WS.WORKSHOP,WS.SHORTNAME
        ) rd
group by rd.WORKSHOPID,rd.WORKSHOP,rd.SHORTNAME";
        DataTable dt = new DataTable();
        dt = dm.ExecuteQuery(qry);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        int c = ds.Tables[0].Rows.Count;
        DataTable dsChartData = ds.Tables[0];
        DataTable sdsChartData = new DataTable();
        sdsChartData.Columns.Add(dsChartData.Columns[1].ColumnName);
        sdsChartData.Columns.Add(dsChartData.Columns[2].ColumnName);
        sdsChartData.Columns.Add(dsChartData.Columns[3].ColumnName);
        sdsChartData.Columns.Add(dsChartData.Columns[4].ColumnName);
        for (int i = 0; i < c; i++)
        {
            DataRow dr = sdsChartData.NewRow();
            dr[0] = dsChartData.Rows[i][1];
            dr[1] = Convert.ToDecimal("0" + dsChartData.Rows[i][2]);
            dr[2] = Convert.ToDecimal("0" + dsChartData.Rows[i][3]);
            dr[3] = Convert.ToDecimal("0" + dsChartData.Rows[i][4]);
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

            strScript.Append("var options = { title : 'Todays job chart', vAxis: {title: 'Workshop'},  hAxis: {title: 'Job Function'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('TWCGraphChart'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
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

}