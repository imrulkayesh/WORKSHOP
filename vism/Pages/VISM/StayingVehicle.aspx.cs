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
            ddlLoad();
            SelectWorkshop();
        }
    }
    private void SelectWorkshop()
    {
        try
        {
            //qry = @"select ws.id from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
            //SessionManager.SessionName.UserId + "')";
            //string id = dm.ExecuteScalar(qry);
            //ddlWorkShop.SelectedValue = id;


            try
            {


                string BI = SessionManager.SessionName.BusinessCentetid.ToString();
                ListItem li = new ListItem("Select", "");
                string qry = "";
                if (BI == "705")
                {
                    qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
           SessionManager.SessionName.UserId + "')";
                    ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
                    ddlWorkShop.DataBind();
                }
                else
                {
                    qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws  ORDER BY WORKSHOP ASC";
                    ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
                    ddlWorkShop.DataBind();
                    ddlWorkShop.Items.Insert(0, li);
                }

      
            }
            catch
            {

                //
            }
        }
        catch
        {

            //
        }

    }
    private void ddlLoad()
    {
        ListItem li = new ListItem("Select", "0");
        //ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        //ddlWorkShop.DataBind();
        //ddlWorkShop.Items.Insert(0, li);


        ddlServiceType.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlServiceType.DataBind();
        ddlServiceType.Items.Insert(0, li);

        string qry = @"Select ID,GROUPNAME text FROM RVS_GROUP";
        ddlgroup.DataSource = dm.ExecuteQuery(qry);
        ddlgroup.DataBind();
        ddlgroup.Items.Insert(0, li);

        string qry1 = @"select ID,COMPANYNAME from ya.BS_COMPANY order by COMPANYNAME asc";
        ddlCompany.DataSource = dm.ExecuteQuery(qry1);
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, li);
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

                cmd.CommandText = @"select CP.COMPANYID ||'-'||CP.COMPANYNAME COMPANYNAME from ya.V_COMPANY CP where COMPANYNAME like UPPER ('%" + prefixText + "%') ";

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRpt();
        //DataTable dt = obj.StayingVehicle(Criteria());
        //rpt.DataSource = dt;
        //rpt.DataBind();
        //lblfromDate.Text = tbxfromdate.Text;
        //lbltoDate.Text = tbxtodate.Text;
    }
    private void BindRpt()
    {
        qry = @"SELECT distinct J.ID ""Job Id"",to_char(J.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Received Date"",to_char(PI.DLDATE,'DD-MM-YYYY') ""Deadline"",to_char(PI.EFCTDATE,'DD-MM-YYYY HH:MI AM') ""Effective Date"",PI.DRIVERNAME ""Driver Name"",v.DRIVERID ""Staff ID"",PI.DRIVERMOBILE ""Driver Mobile"",ST.SERVICETYPE ""Service Type"",
        V.REGNO ""Reg No."", V.CHASSISNO ""Chassis No."", M.MODELNAME ""Model"",CO.CUSTOMERNAME ""Customer"",
        C.COMPANYNAME ""Company"",G.GROUPNAME ""Group"", W.WORKSHOP ""Workshop"" FROM RVS_JOBCARD J
        LEFT OUTER JOIN RVS_JOBCARDITEMS JI ON JI.JOBID = J.ID
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        LEFT OUTER JOIN RVS_SPAREPARTS SP ON SP.ID = JI.PARTSID
        LEFT OUTER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN V_COMPANY C ON C.COMPANYID= V.COMPANYID
        INNER JOIN RVS_SERVICETYPE ST ON ST.ID = J.SERVICETYPEID
        INNER JOIN RVS_CUSTOMER CO ON CO.ID = V.CUSTOMERID
        INNER JOIN RVS_GROUP G ON J.GROUPID=G.ID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        LEFT OUTER JOIN ASSET_UNIT U ON U.ID = SP.UNITID 
        WHERE J.ID>0 AND J.ISOPEN=1 AND J.ISACTIVE=1 AND PI.ISACTIVE=1 " + Criteria() + " ORDER BY J.ID";
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
    //Reapeter bind end
    private string Criteria()
    {
        string val = "";
        if (!string.IsNullOrEmpty(ddlWorkShop.SelectedValue) && ddlWorkShop.SelectedValue != "0")
        {
            val += " AND J.WORKSHOPID=" + ddlWorkShop.SelectedValue;
        }
        if (!string.IsNullOrEmpty(ddlServiceType.SelectedValue) && ddlServiceType.SelectedValue != "0")
        {
            val += " AND J.SERVICETYPEID='" + ddlServiceType.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(ddlgroup.SelectedValue) && ddlgroup.SelectedValue != "0")
        {
            val += " AND J.GROUPID='" + ddlgroup.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(ddlCompany.SelectedValue) && ddlCompany.SelectedValue != "0")
        {
            val += " AND C.COMPANYID ='" + ddlCompany.SelectedValue + "'";
        }
        if (tbxfromdate.Text.Length > 7 && tbxfromdate.Text.Length > 7)
        {
            val += " AND trunc(J.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        else if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND trunc(J.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
        }

        return val;
    }
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
       // dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }
}
