using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Dashboard : BasePage
{
    dalApprover obj = new dalApprover();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
       
        if (!IsPostBack)
        {
            string url = "/Pages/Admin/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            ViewState["ID"] = (int)0;
          //  BindChart();
         //   ServiceChart();
        }
    }
    //private void BindChart()
    //{
    //    DataTable dsChartData = new DataTable();
    //    dsChartData.Columns.Add("DispatchType");
    //    dsChartData.Columns.Add("Dispatch");
    //    dsChartData.Rows.Add("ReceiveList", obj.GetReceiveList().Rows.Count);
    //    dsChartData.Rows.Add("ReturnList", obj.GetReturnList().Rows.Count);
    //    dsChartData.Rows.Add("Rejected", obj.GetRejectList().Rows.Count);
    //    dsChartData.Rows.Add("ApprovalList", obj.WaitingApprovalList().Rows.Count);
    //    StringBuilder strScript = new StringBuilder();

    //    try
    //    {
    //        strScript.Append(@"<script type='text/javascript'>  
    //                google.load('visualization', '1.1', {packages: ['corechart']}); </script>  
                      
    //                <script type='text/javascript'>  
                     
    //                function drawChart() {         
    //                var data = google.visualization.arrayToDataTable([  
    //                ['Class Name', 'Number of Student'],");

    //        foreach (DataRow row in dsChartData.Rows)
    //        {
    //            strScript.Append("['" + row["DispatchType"] + "'," + row["Dispatch"] + "],");
    //        }
    //        strScript.Remove(strScript.Length - 1, 1);
    //        strScript.Append("]);");

    //        strScript.Append(@" var options = {     
    //                                title: '',            
    //                                is3D: true,          
    //                                };   ");

    //        strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('DispatchChart'));          
    //                            chart.draw(data, options);        
    //                            }    
    //                        google.setOnLoadCallback(drawChart);  
    //                        ");
    //        strScript.Append(" </script>");

    //        ltScripts.Text = strScript.ToString();
    //    }
    //    catch
    //    {
    //    }
    //    finally
    //    {
    //        dsChartData.Dispose();
    //        strScript.Clear();
    //    }
    //}
    //private void ServiceChart()
    //{
    //    DataTable dsChartData = new DataTable();
    //    dsChartData.Columns.Add("DispatchType");
    //    dsChartData.Columns.Add("Dispatch");
    //    dsChartData.Rows.Add("ReceiveList", new dalServiceRequest().MyServicesRequest(0).Rows.Count);
    //    dsChartData.Rows.Add("ReturnList", new dalServiceRequest().MyServices(2).Rows.Count);
    //    dsChartData.Rows.Add("Rejected", new dalServiceRequest().MyServicesRequest(2).Rows.Count);
    //    dsChartData.Rows.Add("ApprovalList", obj.WaitingApprovalList().Rows.Count);
    //    StringBuilder strScript = new StringBuilder();

    //    try
    //    {
    //        strScript.Append(@"<script type='text/javascript'>  
    //                google.load('visualization', '1.1', {packages: ['corechart']}); </script>  
                      
    //                <script type='text/javascript'>  
                     
    //                function drawChart() {         
    //                var data = google.visualization.arrayToDataTable([  
    //                ['Class Name', 'Number of Student'],");

    //        foreach (DataRow row in dsChartData.Rows)
    //        {
    //            strScript.Append("['" + row["DispatchType"] + "'," + row["Dispatch"] + "],");
    //        }
    //        strScript.Remove(strScript.Length - 1, 1);
    //        strScript.Append("]);");

    //        strScript.Append(@" var options = {     
    //                                title: '',            
    //                                is3D: true,          
    //                                };   ");

    //        strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('ServiceChart'));          
    //                            chart.draw(data, options);        
    //                            }    
    //                        google.setOnLoadCallback(drawChart);  
    //                        ");
    //        strScript.Append(" </script>");

    //        ltService.Text = strScript.ToString();
    //    }
    //    catch
    //    {
    //    }
    //    finally
    //    {
    //        dsChartData.Dispose();
    //        strScript.Clear();
    //    }
    //}
}