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

public partial class Pages_VISM_VISMFuelCoDetail : BasePage
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