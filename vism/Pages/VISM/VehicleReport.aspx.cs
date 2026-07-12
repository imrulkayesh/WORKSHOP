using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Pages_VISM_StayingVehicle : BasePage
{
    dalGatein obj = new dalGatein();
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
            //ddlLoad();
            //SelectWorkshop();
        }
    }

    private string criteria = "";

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetFuelCriteria();
        BindRpt();

    }

    private void GetFuelCriteria()
    {

        if (tbxCom.Text != "") criteria += "  AND co.COMPANYNAME='" + tbxCom.Text + "' ";
        if (tbxRecVeh.Text != "") criteria += " AND (vh.id='" + tbxRecVeh.Text.Split('-')[0] + "')";

    }
    private void BindRpt()
    {
        qry = @"Select vh.id,vh.REGNO ""Reg. Number"",vh.CHASSISNO ""Chassis Number"" ,vh.ENGINENO ""Engine Number"",mo.MODELNAME ""Model"",
            TO_CHAR(vh.MANUFACTURINGYEAR, 'DD/MM/YYYY') AS ""Reg.Date"",
            mn.MANUFACTURERNAME ""Manufacturer"",vt.VEHICLETYPENAME ""Vehicle Type"",ft.FUELTYPENAME ""Fuel Type"",vh.DRIVERID ""Driver ID"",vh.DRIVERNAME ""Driver Name"",
            vh.DRIVERMOBILE ""Driver Mobile No."",
            bc.CENTERNAME ""Location"",cu.CUSTOMERNAME ""Customer"",co.COMPANYNAME ""Company"",cn.COUNTRYNAME ""Country"",vh.CNGCYLINDERREDDATE ""CNG Retest-Date"",
            TO_CHAR(vh.HANDOVERRECVDATE, 'DD/MM/YYYY') AS ""Handover Date"",
            vh.KPL ""KPL"",vh.MBC ""Engine Oil Capacity"" from rvs_Vehicle vh
            left join rvs_model mo on vh.MODELID=mo.ID
            left join RVS_MANUFACTURER mn on vh.MANUFACTURERID=mn.ID
            left join RVS_VEHICLETYPE vt on vh.VEHICLETYPEID=vt.ID
            left join RVS_FUELTYPE ft on vh.FUELYTYPEID=ft.ID
            left join bs_businesscenter bc on vh.BUSINESSCENTERID=bc.ID
            left join RVS_CUSTOMER cu on vh.CUSTOMERID=cu.ID
            left join bs_company co on vh.COMPANYID=co.id
            left join bs_country cn on vh.COUNTRYID=cn.ID where vh.ISACTIVE in ('0','1') " + criteria + @" ";
        DtCommon = dm.ExecuteQuery(qry);
        rptMain.DataSource = DtCommon;
        rptMain.DataBind();
    }

    private int ID = 0;
    private int rn = 0;
    private DataTable DtCommon = new DataTable();
    protected void rptMain_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
            if (headerRpt == null) return;
            headerRpt.DataSource = GetHeader(DtCommon, false);
            headerRpt.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            if (bodyRpt == null) return;
            bodyRpt.DataSource = DtToList(rn, DtCommon, false);
            bodyRpt.DataBind();
            rn++;
            //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
        }

    }

    protected void btnExport_OnClick(object sender, EventArgs e)
    {
       // dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCom(string prefixText, int count)
    {

        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {

                cmd.CommandText = @"select COMPANYNAME from ya.BS_COMPANY where COMPANYNAME like UPPER ('%" + prefixText + "%') ";

                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["COMPANYNAME"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void tbxCom_OnTextChanged(object sender, EventArgs e)
    {
        string cn = tbxCom.Text.Split('-')[0];
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

    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    {

        string cn = tbxRecVeh.Text.Split('-')[0];
    }
}
