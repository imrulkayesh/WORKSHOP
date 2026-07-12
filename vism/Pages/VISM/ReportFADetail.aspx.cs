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


public partial class Pages_VISM_ReportCoDetail : BasePage
{
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
        }
    }
    private void LoadDdl()
    {
        ListItem li = new ListItem("All", "");
        string qry =
            @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        ddlSendLoc.DataBind();
        ddlSendLoc.Items.Insert(0, li);


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

        //ddlVehicleRegNo.Text = dt.Rows[0]["regno"].ToString();
        //li = new ListItem("Select", "");
        //qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        //ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlSendLoc.DataBind();
        //ddlSendLoc.Items.Insert(0, li);
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

        if (ddlSendWh.SelectedValue != "") criteria += " AND (SENDWH='" + ddlSendWh.SelectedValue + "' OR RECVWH='" + ddlSendWh.SelectedValue + "')";
        if (tbxRecVeh.Text != "") criteria += " AND (SENDWH='" + tbxRecVeh.Text.Split('-')[0] + "' OR RECVWH='" + tbxRecVeh.Text.Split('-')[0] + "')";


        //if (ddlSendWh.SelectedValue != "") criteria += " AND (SENDWH='" + ddlSendWh.SelectedValue + "' OR RECVWH='" + ddlSendWh.SelectedValue + "')";
        //if (tbxRecVeh.Text != "") criteria += " AND (SENDWH='" + tbxRecVeh.Text.Split('-')[0] + "' OR RECVWH='" + tbxRecVeh.Text.Split('-')[0] + "')";

        //string vehicle = tbxRecVeh.Text;
        //if (ddlSendWh.SelectedValue != "") criteria += " AND (SENDWH='" + ddlSendWh.SelectedValue + "')";
        //if (tbxRecVeh.Text != "") criteria += " AND (REGNO='" + ddlVehicleRegNo.SelectedItem + "')";


    }
    private string GetQry()
    {



        string qry = @"SELECT * FROM V_RVS_CO_FA FA
WHERE FA.TRDATE BETWEEN TO_DATE('" + tbxfromdate.Text.ToDate() + "','DD/MM/YYYY') AND TO_DATE('" + tbxtodate.Text.ToDate() + "','DD/MM/YYYY') " + criteria + @"";


        return qry;
    }
    protected void btnOpenPdf_OnClick(object sender, EventArgs e)
    {
        //GetCriteria();
        //string qry = GetQry();
        //DataTable dt = new DataTable();
        //dt = dm.ExecuteQuery(qry);
        //string path = Server.MapPath("/Pages/VISM/CrReport/CrCoDetail.rpt");
        //GetCrReport(path, dt, ReportAction.OpenPdf);
        //criteria = "";

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

    protected void btndownloadNormalExcel_OnClick(object sender, EventArgs e)
    {
        GetCriteria();


        //        string qry = @"SELECT FA.*,FA.RATE*FA.QTY AMOUNT FROM YA.V_RVS_CO_FA FA
        //WHERE FA.TRDATE BETWEEN TO_DATE('" + tbxfromdate.Text.ToDate() + "','DD/MM/YYYY') AND TO_DATE('" + tbxtodate.Text.ToDate() + "','DD/MM/YYYY') " + criteria + @"";

        string qry = @"SELECT   ID,
           REFTRID,
           TRDATE,
           SENDWH,
           VISMWHNAME,
           RECVWH,
           REGNO,
           MODELNAME,
           COMPANYNAME,
           CATEGORYCODE,
           CATEGORYNAME,
           ITEMCODE,
           ITEMNAME,
           UNITID,
           UNITNAME,
           RATE,
           QTY-PP QTY,
           CUSTOMERNAME,
           CUSCATEGORY,
           IDATE,
           RTITLE,
           REMARKS,
           (QTY-PP) * RATE AMOUNT,PP,PP * RATE CRAMOUNT
    FROM   (SELECT   FA.ID,
                     FA.REFTRID,
                     FA.TRDATE,
                     FA.SENDWH,
                     FA.VISMWHNAME,
                     FA.RECVWH,
                     FA.REGNO,
                     FA.MODELNAME,
                     FA.COMPANYNAME,
                     FA.CATEGORYCODE,
                     FA.CATEGORYNAME,
                     FA.ITEMCODE,
                     FA.ITEMNAME,
                     FA.UNITID,
                     FA.UNITNAME,
                     Fa.RATE,
                     FA.QTY,
                     FA.CUSTOMERNAME,
                     FA.CUSCATEGORY,
                     FA.IDATE,
                     FA.RTITLE,
                     FA.REMARKS,
                     (SELECT   NVL (SUM (YY.QTY), 0) QT
                        FROM   YA.RVS_TRMASTER XX, YA.RVS_TRDETAIL YY
                       WHERE   XX.ID = YY.TRMASTERID AND FA.ID = XX.REFTRID AND FA.ITEMCODE = YY.ITEMCODE)
                        PP
              FROM   V_RVS_CO_FA FA
             WHERE   FA.TRDATE BETWEEN TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY') AND TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @")
GROUP BY   ID,
           REFTRID,
           TRDATE,
           SENDWH,
           VISMWHNAME,
           RECVWH,
           REGNO,
           MODELNAME,
           COMPANYNAME,
           CATEGORYCODE,
           CATEGORYNAME,
           ITEMCODE,
           ITEMNAME,
           UNITID,
           UNITNAME,
           RATE,
           QTY,
           CUSTOMERNAME,
           CUSCATEGORY,
           IDATE,
           RTITLE,
           REMARKS,
           pp";

        // CR less FA
        //        string qry = @"SELECT FA.*,FA.RATE*FA.QTY AMOUNT FROM YA.V_RVS_CO_FA FA
        //WHERE FA.TRDATE BETWEEN TO_DATE('" + tbxfromdate.Text.ToDate() + "','DD/MM/YYYY') AND TO_DATE('" + tbxtodate.Text.ToDate() + "','DD/MM/YYYY') and ID not in (select REFTRID from YA.RVS_TRMASTER where TRTYPE = 'CR') " + criteria + @"";



        DataTable dt = dm.ExecuteQuery(qry);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ExportGridToImpToDistExcel();
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
            string FileName = "FArpt" + DateTime.Now + ".xls";
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