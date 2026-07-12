using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

public partial class Pages_Report_rptVehicleBarcode : System.Web.UI.Page
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!string.IsNullOrWhiteSpace(Request.QueryString["RegNo"]))
            {
                var RegNo = Request.QueryString["RegNo"];

                DataTable dt = new DataTable();

                dt = dm.ExecuteQuery(@"select VH.CHASSISNO,VH.REGNO BARCODE,VH.MANUFACTURINGYEAR,MD.MODELNAME  
from ya.RVS_VEHICLE VH,YA.RVS_MODEL MD
where VH.REGNO='"+ RegNo + @"'
AND VH.MODELID=MD.ID");


                try

                {
                    ReportDocument cryRpt = new ReportDocument();
                    vehicleBarcode objReport = new vehicleBarcode();
                    objReport.SetDataSource(dt);
                    byte[] byteArray = null;
                    ReportDocument rptReportDocument = new ReportDocument();
                    rptReportDocument = (ReportDocument)objReport;
                    System.IO.Stream oStream = rptReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(byteArray);
                    Response.Flush();
                    Response.Close();
                    rptReportDocument.Close();
                    rptReportDocument.Dispose();
                    oStream.Dispose();
                }

                catch (Exception)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<script language='javascript'>alert('Report not found, try again !!!');</script>", false);

                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
}