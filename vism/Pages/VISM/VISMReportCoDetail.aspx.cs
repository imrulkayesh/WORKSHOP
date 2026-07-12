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
using System.Globalization;




using System.IO;
using System.Drawing;

public partial class Pages_VISM_VISMReportCoDetail : BasePage
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
        //ListItem li = new ListItem("All", "");
        //string qry =
        //    @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        //ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlSendLoc.DataBind();
        //ddlSendLoc.Items.Insert(0, li);


        string BI = SessionManager.SessionName.BusinessCentetid.ToString();
        ListItem li = new ListItem("Select", "");
        string qry = "";
        if (BI == "2")
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        }
        else
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
        }

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
        //        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE, WH.VISMWHNAME WH,VH.REGNO COUNIT,CT.ITEMCATEGORYNAME CATEGORY,IM.ITEMCODE||' - '|| IM.ITEMNAME ITEMNAME,
        //UT.UNITNAME,TD.RATE,SUM(TD.QTY) QTY,SUM(TD.TOTALAMT) AMOUNT
        //FROM RVS_TRDETAIL TD
        //INNER JOIN RVS_TRMASTER TM ON TD.TRMASTERID=TM.ID
        //INNER JOIN RVS_ITEMCATEGORY CT ON TD.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE
        //INNER JOIN RVS_ITEMMASTER IM ON TD.ITEMCODE=IM.ITEMCODE
        //INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE=UT.UNITCODE
        //INNER JOIN BS_BUSINESSCENTER LO ON TM.RECVLOCATION=LO.ID
        //INNER JOIN RVS_VEHICLE VH ON TM.RECVWH=VH.ID
        //INNER JOIN BS_COMPANY CO ON TM.RECVCOMPANY=CO.ID
        //INNER JOIN RVS_VISMWH WH ON TM.SENDWH=WH.ID
        //WHERE TM.TRTYPE='CO' AND WH.ID= '"+ddlSendWh.SelectedValue+"' AND TM.ISACTIVE=1 " + criteria+@" 
        //GROUP BY WH.VISMWHNAME,VH.REGNO,CT.ITEMCATEGORYNAME,IM.ITEMCODE||' - '|| IM.ITEMNAME,UT.UNITNAME,TD.RATE
        //ORDER BY IM.ITEMCODE||' - '|| IM.ITEMNAME";
        //        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE ||'-'||ITEMNAME ITEMNAME,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
        //FROM V_RVS_CO_AC CO
        //WHERE CO.TRDATE BETWEEN '" + tbxfromdate.Text.ToDate() + @"' AND '" + tbxtodate.Text.ToDate() + @"' " + criteria + @"
        //GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,CO.COMPANYNAME,CO.CATEGORYNAME,
        //CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY
        //HAVING SUM(QTY) <> 0 order by CO.REGNO asc";

        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE ||'-'||ITEMNAME ITEMNAME,UNITNAME UNITNAME,ROUND((RATE*QTY)/(QTY),2) RATE, ROUND((QTY),2) QTY,ROUND((RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
FROM V_RVS_CO_AC CO
WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @" ";


        //" + tbxfromdate.Text.ToDate() + "
        //" + tbxtodate.Text.ToDate() + "
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
        //string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,servicetype,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE, 
        //ITEMNAME,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE,DECODE(NVL(SUM(MQTY),0),0,MARGINRATE,ROUND(NVL(SUM(MARGINRATE*MQTY),0)/NVL(SUM(MQTY),0),2)) MARGINRATE,
        //ROUND(SUM(QTY),2) QTY, ROUND(SUM(RATE*QTY),2) AMOUNT,ROUND(SUM(MARGINRATE*QTY),2) MARGINAMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
        //FROM V_RVS_CO_AC_new_ok CO
        //WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
        //GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,CO.servicetype,CO.COMPANYNAME,CO.CATEGORYNAME,
        //CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.IDATE,CO.ID,MARGINRATE
        //HAVING SUM(QTY) <> 0 order by CO.REGNO asc";

        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,servicetype,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE, ITEMNAME,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
FROM V_RVS_CO_AC CO
WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,co.servicetype,CO.COMPANYNAME,CO.CATEGORYNAME,
CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.IDATE,CO.ID
HAVING SUM(QTY) <> 0 order by CO.REGNO asc";



        //        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,servicetype,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE, ITEMNAME,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
        //FROM V_RVS_CO_AC CO
        //WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
        //GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,co.servicetype,CO.COMPANYNAME,CO.CATEGORYNAME,
        //CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.IDATE,CO.ID
        //HAVING SUM(QTY) <> 0 order by CO.REGNO asc";


        DataTable dt = dm.ExecuteQuery(qry);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ExportGridToImpToDistExcel();
    }
    protected void btndownloadNormalExcelMargin_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        //string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,servicetype,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE, 
        //ITEMNAME,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE,DECODE(NVL(SUM(MQTY),0),0,MARGINRATE,ROUND(NVL(SUM(MARGINRATE*MQTY),0)/NVL(SUM(MQTY),0),2)) MARGINRATE,
        //ROUND(SUM(QTY),2) QTY, ROUND(SUM(RATE*QTY),2) AMOUNT,ROUND(SUM(MARGINRATE*QTY),2) MARGINAMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
        //FROM V_RVS_CO_AC_new_ok CO
        //WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
        //GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,CO.servicetype,CO.COMPANYNAME,CO.CATEGORYNAME,
        //CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.IDATE,CO.ID,MARGINRATE
        //HAVING SUM(QTY) <> 0 order by CO.REGNO asc";

        //        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,servicetype,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE, ITEMNAME,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.MARGIN,CO.MARGINRATE,CO.MARGINAMOUNT FROM YA.V_RVS_CO_AC_MARGIN_TEST3 CO
        //WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
        //GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,co.servicetype,CO.COMPANYNAME,CO.CATEGORYNAME,
        //CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.IDATE,CO.ID,CO.MARGIN,CO.MARGINRATE,CO.MARGINAMOUNT
        //HAVING SUM(QTY) <> 0 order by CO.REGNO asc";
        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,servicetype,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE, ITEMNAME,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.MARGIN,CO.MARGINRATE,CO.MARGINAMOUNT FROM YA.V_RVS_CO_AC_MARGIN CO
WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,co.servicetype,CO.COMPANYNAME,CO.CATEGORYNAME,
CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.IDATE,CO.ID,CO.MARGIN,CO.MARGINRATE,CO.MARGINAMOUNT
HAVING SUM(QTY) <> 0 order by CO.REGNO asc";



        //        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,VISMWHNAME WH, REGNO COUNIT,MODELNAME,servicetype,COMPANYNAME COMPANY,CATEGORYNAME CATEGORY, ITEMCODE, ITEMNAME,to_char(co.IDATE,'DD/MM/yyyy HH:MI:SS AM') TRDATE,ID,UNITNAME UNITNAME,ROUND(SUM(RATE*QTY)/SUM(QTY),2) RATE, ROUND(SUM(QTY),2) QTY,ROUND(SUM(RATE*QTY),2) AMOUNT,CO.CUSTOMERNAME,CO.CUSCATEGORY
        //FROM V_RVS_CO_AC CO
        //WHERE CO.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
        //GROUP BY CO.VISMWHNAME,CO.REGNO,CO.MODELNAME,co.servicetype,CO.COMPANYNAME,CO.CATEGORYNAME,
        //CO.ITEMCODE,CO.ITEMNAME,CO.UNITNAME,CO.CUSTOMERNAME,CO.CUSCATEGORY,CO.IDATE,CO.ID
        //HAVING SUM(QTY) <> 0 order by CO.REGNO asc";


        DataTable dt = dm.ExecuteQuery(qry);
        GridView3.DataSource = dt;
        GridView3.DataBind();
        ExportGridToImpToDistExcelMargin();
    }

    protected void btndownloadNormalExcelonFUEL_OnClick(object sender, EventArgs e)
    {
        GetFuelCriteria();

        // DataTable dt = new DataTable();
        DateTime fd = DateTime.ParseExact(tbxfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string fDate = fd.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime td = DateTime.ParseExact(tbxtodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        td = td.AddDays(1);
        string tDate = td.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);


        string qry = @"SELECT '" + tbxfromdate.Text + @"' FROMDATE,'" + tbxtodate.Text + @"' TODATE,D.*,to_char(D.CREATEDATE,'DD/MM/yyyy HH:MI:SS AM') DDT,to_char(D.UPDATEDATE,'DD/MM/yyyy HH:MI:SS AM') CDT, VH.REGNO,WH.VISMWHNAME,ct.ITEMCATEGORYNAME,IM.ITEMNAME,IM.SCMUNITCODE,MO.MODELNAME,CU.CUSTOMERNAME,co.COMPANYNAME,ROUND(D.GQTY*D.RATE,4) AMOUNT
 FROM YA.RVS_FUELDMD D
 INNER JOIN YA.RVS_VEHICLE vh ON D.VEHICLEID=VH.ID
 INNER JOIN YA.RVS_VISMWH wh ON D.PUMPID = wh.ID
 INNER JOIN YA.RVS_ITEMMASTER im ON D.FUELID = im.ITEMCODE
 INNER JOIN YA.BS_COMPANY CO ON VH.COMPANYID=CO.ID
 INNER JOIN rvs_model mo ON vh.modelid = mo.ID
 INNER JOIN rvs_customer cu ON vh.customerid = cu.ID
 INNER JOIN rvs_itemcategory ct ON IM.ITEMCATEGORYCODE = ct.itemcategorycode
 WHERE D.CODATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') and STATUSID='3'" + criteria + @" ";


        DataTable dt = dm.ExecuteQuery(qry);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ExportGridFuelCOExcel();
    }

    private void GetFuelCriteria()
    {

        if (ddlSendWh.SelectedValue != "") criteria += " AND D.PUMPID='" + ddlSendWh.SelectedValue + "'";
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
    private void ExportGridToImpToDistExcelMargin()
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "COMarginrpt" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView3.GridLines = GridLines.Both;
            GridView3.HeaderStyle.Font.Bold = true;
            GridView3.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception Ex)
        {
            // lblError.Text = Ex.Message.ToString();
        }


    }

    private void ExportGridFuelCOExcel()
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "FuelCOrpt" + DateTime.Now + ".xls";
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}