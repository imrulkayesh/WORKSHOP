using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Service_Default : BasePage
{
    dalServiceRequest obj = new dalServiceRequest();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Reports/Service/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            DataTable dt = new Common().GetAll("VW_SERVICECENTER");
            DataTable dtc = new DataTable();
            dtc.Columns.Add("ChartTitle");
            dtc.Columns.Add("Chart");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int complete = string.IsNullOrEmpty(dt.Rows[i]["COMPLETED"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["COMPLETED"]);
                int pending = string.IsNullOrEmpty(dt.Rows[i]["PENDING"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["PENDING"]);
                int assigned = string.IsNullOrEmpty(dt.Rows[i]["PROCESSING"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["PROCESSING"]);
                int frds = string.IsNullOrEmpty(dt.Rows[i]["FORWARDED"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["FORWARDED"]);
                int instsc = string.IsNullOrEmpty(dt.Rows[i]["INSTANTSERVICE"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["INSTANTSERVICE"]);
                dtc.Rows.Add(dt.Rows[i]["CENTERCODE"].ToString() + '-' + dt.Rows[i]["SERVICECENTERNAME"].ToString() + '-' + dt.Rows[i]["SERVICECOUNT"].ToString(), BindChart(i + 1, complete, pending, assigned, frds, instsc));
            }
            rpt.DataSource = dtc;
            rpt.DataBind();
            Page.Title= "Service Summary- "+DateTime.Now.ToString("MMMM yyyy"); 
        }
    }
    private string BindChart(int id, int Completed, int Pending, int Assigned, int Forwared, int instsc)
    {
        DataTable dsChartData = new DataTable();
        dsChartData.Columns.Add("Type");
        dsChartData.Columns.Add("Count");
        dsChartData.Rows.Add("Solved", Completed);
        dsChartData.Rows.Add("Pending", Pending);
        dsChartData.Rows.Add("Assigned", Assigned);
        dsChartData.Rows.Add("Forwared", Forwared);
        dsChartData.Rows.Add("Instant", instsc);
        StringBuilder strScript = new StringBuilder();

        try
        {
            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1.1', {packages: ['corechart']}); </script>  
                      
                    <script type='text/javascript'>  
                     
                    function drawChart() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Type', 'Count'],");

            foreach (DataRow row in dsChartData.Rows)
            {
                strScript.Append("['" + row["Type"] + "'," + row["Count"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append(@" var options = {     
                                    title: '',
                                    pieHole: 0.4, 
                                    slices: {0: {color: '#009900'}, 1:{color: '#ff6600'}, 2:{color: 'blue'}, 3: {color: 'red'}, 4:{color: '#003300'}},          
                                    };   ");
            strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('Chart" + id + "'));" +
                                @"chart.draw(data, options);        
                                }    
                            google.setOnLoadCallback(drawChart);  
                            ");
            strScript.Append(" </script>");

            return strScript.ToString();
        }
        catch
        {
            return "";
        }
        finally
        {
            dsChartData.Dispose();
            strScript.Clear();
        }
    }
}