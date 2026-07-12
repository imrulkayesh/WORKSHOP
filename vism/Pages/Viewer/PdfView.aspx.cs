using System;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Pages_Viewer_PdfView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowReport();
        }
    }
    private void ShowReport()
    {
        try
        {
            string path = Session["pathReport"] as string;
            DataTable dt = new DataTable();
            dt.Clear();
            dt = Session["dtReport"] as DataTable;
            Session.Remove("pathReport");
            Session.Remove("dtReport");
            Session.Remove("ReportAction");
            string filename = "Report--" + DateTime.Now.ToString("dd-MM-yyyy--hh-mm-ss-tt");
            ReportDocument boReportDocument = new ReportDocument();
            boReportDocument.Load(Server.MapPath(path));
            boReportDocument.SetDataSource(dt);

            //int reportAction = (int)Session["ReportAction"];
            //string path = Session["pathReport"] as string;
            //DataTable dt = new DataTable();
            //dt.Clear();
            //dt = Session["dtReport"] as DataTable;
            //Session.Remove("pathReport");
            //Session.Remove("dtReport");
            //Session.Remove("ReportAction");
            //string filename = "Report--" + DateTime.Now.ToString("dd-MM-yyyy--hh-mm-ss-tt");
            //ReportDocument boReportDocument = new ReportDocument();
            //boReportDocument.Load(Server.MapPath(path));
            //boReportDocument.SetDataSource(dt);



            //for open pdf in browser
            //boReportDocument.Load(Server.MapPath(path));
            //boReportDocument.SetDataSource(dt);
            //Stream oStream = null;
            //byte[] byteArray = null;
            //oStream = boReportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
            //byteArray = new byte[oStream.Length];
            //oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.AddHeader("Content-Disposition", "inline; filename="+filename+".pdf");
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(byteArray);
            //Response.Flush();
            //Response.Close();
            //boReportDocument.Close();
            //boReportDocument.Dispose();
            //oStream.Dispose();

            //for download as pdf
            //Stream oStream = boReportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
            //byte[] byteArray = new byte[oStream.Length];
            //oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + filename+".pdf");
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(byteArray);
            //Response.Flush();
            //Response.Close();
            //boReportDocument.Close();
            //boReportDocument.Dispose();
            //oStream.Dispose();

            //for download as excel
            ////Stream oStream = boReportDocument.ExportToStream(ExportFormatType.ExcelWorkbook);
            ////byte[] byteArray = new byte[oStream.Length];
            ////oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            ////Response.ClearContent();
            ////Response.ClearHeaders();
            ////Response.AddHeader("Content-Disposition", "attachment; filename=" + filename+".xlsx");
            ////Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            ////Response.BinaryWrite(byteArray);
            ////Response.Flush();
            ////Response.Close();
            ////boReportDocument.Close();
            ////boReportDocument.Dispose();
            ////oStream.Dispose();
            ////GC.Collect();

            Stream oStream = boReportDocument.ExportToStream(ExportFormatType.Excel);
            byte[] byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();
            boReportDocument.Close();
            boReportDocument.Dispose();
            boReportDocument.Refresh();
            oStream.Dispose();
            GC.Collect();


        }
        catch (Exception ex)
        {
            lblNodata.Visible = true;
            string err = ex.Message+" Please try again.";
            lblNodata.Text = err;

        }



    }
}