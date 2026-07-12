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

public partial class Pages_VISM_ReportJobCard : BasePage
{
    private string qry = string.Empty;
    private DataTable dtRpt = new DataTable();
    private int rn;
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            ddlLoad();
            SelectWorkshop();
        }
    }
    private void SelectWorkshop()
    {
        try
        {
            qry = @"select ws.id from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
            SessionManager.SessionName.UserId + "')";
            string id = dm.ExecuteScalar(qry);
            ddlWorkShop.SelectedValue = id;
        }
        catch
        {

            //
        }

    }
    private void ddlLoad()
    {
        ListItem li = new ListItem("Select", "0");
        ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        ddlWorkShop.DataBind();
        ddlWorkShop.Items.Insert(0, li);
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
                cmd.CommandText = @"SELECT V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME VEHICLE FROM RVS_VEHICLE V
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

    protected void btnShowPdf_OnClick(object sender, EventArgs e)
    {
        try
        {
            DateTime dtt = Convert.ToDateTime(tbxfromdate.Text);
             dtt = Convert.ToDateTime(tbxtodate.Text);
        }
        catch (Exception ex)
        {
            MessageBox("Invalid Date");
            return;
        }
        GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrReportJobCard.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);
        //criteria = "";
    }
    protected void btnDownloadPdf_OnClick(object sender, EventArgs e)
    {
        try
        {
            DateTime dtt = Convert.ToDateTime(tbxfromdate.Text);
            dtt = Convert.ToDateTime(tbxtodate.Text);
        }
        catch (Exception ex)
        {
            MessageBox("Invalid Date");
            return;
        }
        GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrReportJobCard.rpt";
        GetCrReport(path, dt, ReportAction.DownloadPdf);

    }
    protected void btnDownloadExcel_OnClick(object sender, EventArgs e)
    {

        try
        {
            DateTime dtt = Convert.ToDateTime(tbxfromdate.Text);
            dtt = Convert.ToDateTime(tbxtodate.Text);
        }
        catch (Exception ex)
        {
            MessageBox("Invalid Date");
            return;
        }
        GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrReportJobCard.rpt";
        GetCrReport(path, dt, ReportAction.DownloadExcel);
    }
    private string criteria = "";
    private void GetCriteria()
    {
        if (!string.IsNullOrEmpty(tbxVehicleid.Text))
        {
            criteria += " AND V.ID ='"+ tbxVehicleid.Text.Split('-')[0]+"'";
        }
        if (ddlWorkShop.SelectedIndex>0)
        {
            criteria += " AND PI.WORKSHOPID=" + ddlWorkShop.SelectedValue;
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            criteria += " AND trunc(J.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY')) ";
        }
        if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            criteria += " trunc(J.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) ";
        }

    }

    private string GetQry()
    {
        string qry = @"SELECT V.REGNO,M.MODELNAME, J.ID JOBID, PI.MILLAGE,TO_CHAR(J.CREATEDATE,'DD-MM-YYYY') RECEIVEDDATE, TO_CHAR(J.UPDATEDATE,'DD-MM-YYYY') DELIVERYDATE,
            ST.SERVICETYPE, SC.CATNAME,II.ITEMNAME,PD.REMARKS,'" + ddlWorkShop.SelectedItem.Text+@"'WORKSHOP, '"+Convert.ToDateTime(tbxfromdate.Text).ToString("dd-MM-yyyy") + @"' FROMDATE, '" + Convert.ToDateTime(tbxtodate.Text).ToString("dd-MM-yyyy") + @"' TODATE,CO.COMPANYNAME,
            TO_CHAR(J.CREATEDATE,'HH:MI AM') RECTIME,TO_CHAR(J.UPDATEDATE,'HH:MI AM') DELTIME
            FROM RVS_INSPECTIONITEMS II
            INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID
            INNER JOIN RVS_PIDETAILS PD ON PD.INSPECTIONITEMID = II.ID 
            INNER JOIN RVS_PREINSPECTION PI ON PI.ID = PD.PIID
            INNER JOIN RVS_JOBCARD J ON J.PIID  = PI.ID
            INNER JOIN RVS_VEHICLE V ON V.ID = PI.VEHICLEID
            INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
            INNER JOIN RVS_SERVICETYPE ST ON ST.ID = PI.SERVICETYPEID
            INNER JOIN RVS_WORKSHOP W ON W.ID = PI.WORKSHOPID
            LEFT JOIN BS_COMPANY CO ON V.COMPANYID=CO.ID
            WHERE II.INSPECTIONTYPE=0 
            AND PD.STATUSID=2 AND V.ISACTIVE=1 AND PI.ISACTIVE=1  AND J.ISACTIVE=1 " + criteria + @" 
            ORDER BY V.REGNO,  J.CREATEDATE ASC,ST.SERVICETYPE, SC.CATNAME,II.ITEMNAME";
        return qry;
    }
    protected void rptMain_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
            if (headerRpt == null) return;
            headerRpt.DataSource = GetHeader(dtRpt, false);
            headerRpt.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            if (bodyRpt == null) return;
            bodyRpt.DataSource = DtToList(rn, dtRpt, false);
            bodyRpt.DataBind();
            rn++;
        }

    }
    //Reapeter bind end
   
}