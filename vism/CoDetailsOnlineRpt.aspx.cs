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

public partial class CoDetailsOnlineRpt : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }

    }
    private void BindData()
    {
        try
        {
            string regno = Request.QueryString["regno"];

            regno = Server.UrlDecode(regno);

            string qry = @"SELECT VISMWHNAME WH,JOBID, REGNO,servicetype,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,ITEMCODE, ITEMNAME,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT
                        FROM V_RVS_CO_AC CO 
                        WHERE CO.REGNO = '" + regno + @"'  AND CO.TRDATE BETWEEN TRUNC(SYSDATE)-30 AND TRUNC(SYSDATE)
                        GROUP BY CO.VISMWHNAME,CO.JOBID,CO.REGNO,co.servicetype,
                        CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.IDATE,CO.ID
                        HAVING SUM(QTY) <> 0 order by CO.IDATE DESC";
            DataTable dt = dm.ExecuteQuery(qry);
            dtr.DataSource = dt;
            dtr.DataBind();

        }
        catch (Exception ex)
        {
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
    protected void btndownloadNormalExcel_OnClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbxVehicle.Text))
        {
            GetCriteria();
            // Retrieve REGNO from the QueryString
            string VehicleId = tbxVehicle.Text.Split('-')[0];

            DataTable dtVehicle = DatabaseManagerORCL.StaticDT(@"SELECT V.REGNO AS regno  FROM RVS_VEHICLE V  where  V.ID = ' " + VehicleId + " '");
            if (dtVehicle.Rows.Count > 0)
            {
                string regno = dtVehicle.Rows[0]["REGNO"].ToString();

                string qry = @"SELECT VISMWHNAME WH,JOBID, REGNO,servicetype,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,ITEMCODE, ITEMNAME,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT
                            FROM V_RVS_CO_AC CO 
                            WHERE CO.REGNO = '" + regno + @"'  AND CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
                            GROUP BY CO.VISMWHNAME,CO.JOBID,CO.REGNO,co.servicetype,
                            CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.IDATE,CO.ID
                            HAVING SUM(QTY) <> 0 order by CO.IDATE DESC";
                DataTable dt = dm.ExecuteQuery(qry);
                dtr.DataSource = dt;
                dtr.DataBind();
            }

            //GridView2.DataSource = dt;
            //GridView2.DataBind();
            //ExportGridToImpToDistExcel();
        }
        else
        {
            string regno = Request.QueryString["regno"];

            regno = Server.UrlDecode(regno);
            string qry = @"SELECT VISMWHNAME WH,JOBID, REGNO,servicetype,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,ITEMCODE, ITEMNAME,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT
                            FROM V_RVS_CO_AC CO 
                            WHERE CO.REGNO = '" + regno + @"'  AND CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
                            GROUP BY CO.VISMWHNAME,CO.JOBID,CO.REGNO,co.servicetype,
                            CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.IDATE,CO.ID
                            HAVING SUM(QTY) <> 0 order by CO.IDATE DESC";
            DataTable dt = dm.ExecuteQuery(qry);
            dtr.DataSource = dt;
            dtr.DataBind();
        }
    }

    private string criteria = "";
    private void GetCriteria()
    {
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
            string FileName = "COrpt" + DateTime.Now + ".xls";
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
}