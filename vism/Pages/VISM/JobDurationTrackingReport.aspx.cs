using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Text;

public partial class Pages_VISM_JobDurationTrackingReport : BasePage
{
    dalItem obj = new dalItem();
    dalVehicle ob = new dalVehicle();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();


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
            //SelectWorkshop();
        }

    }

    //private void SelectWorkshop()
    //{
    //    try
    //    {
    //        string BI = SessionManager.SessionName.BusinessCentetid.ToString();
    //        ListItem li = new ListItem("Select", "");
    //        string qry = "";
    //        if (BI == "705")
    //        {
    //            qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
    //   SessionManager.SessionName.UserId + "')";

    //            ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
    //            ddlWorkShop.DataBind();
    //        }
    //        else
    //        {
    //            qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws  ORDER BY WORKSHOP ASC";
    //            ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
    //            ddlWorkShop.DataBind();
    //            ddlWorkShop.Items.Insert(0, li);
    //        }


    //    }
    //    catch
    //    {

    //        //
    //    }

    //}

    private void ddlLoad()
    {
        ListItem li = new ListItem("Select", "0");
        ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        ddlWorkShop.DataBind();
        ddlWorkShop.Items.Insert(0, li);

        string qry =
            @"select id,CATNAME text  from RVS_SERVICECATEGORY order by CATNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);
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

    //protected void tbxCom_OnTextChanged(object sender, EventArgs e)
    //{
    //    string cn = tbxCom.Text.Split('-')[0];
    //}



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            // lblfromDate.Text = Convert.ToDateTime(tbxfromdate.Text).ToString("dd-MM-yyyy");
            // lbltoDate.Text = Convert.ToDateTime(tbxtodate.Text).ToString("dd-MM-yyyy");
            //  lblfromDate.Text = Convert.ToDateTime(tbxfromdate.Text).ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            MessageBox("Invalid date");
            return;
        }


        //if (!string.IsNullOrEmpty(tbxVehicle.Text) && !string.IsNullOrEmpty(tbxfromdate.Text) && !string.IsNullOrEmpty(tbxtodate.Text) && !string.IsNullOrEmpty(ddlWorkShop.SelectedValue))
        //{
        DataTable dt = obj.GetJobDurationData(Criteria());
        rpt.DataSource = dt;
        rpt.DataBind();
        //  btnExport.Visible = true;

        lblfromDate.Text = tbxfromdate.Text;
        lbltoDate.Text = tbxtodate.Text;
        //LoadData();
        //}
        //else
        //{
        //    MessageController.Show("Data Not Found !", MessageType.Warning, Page);
        //}
    }
    private string Criteria()
    {
        string val = "";

        if (!string.IsNullOrEmpty(tbxVehicle.Text))
        {
            val += " AND JDT.VEHICLE_ID =" + tbxVehicle.Text.Split('-')[0];
        }
        if (!string.IsNullOrEmpty(ddlWorkShop.SelectedValue) && ddlWorkShop.SelectedValue != "0")
        {
            val += " AND  JDT.WORKSHOP_ID=" + ddlWorkShop.SelectedValue;
        }

        if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length > 7)
        {
            val += " AND JDT.WORKORDERDATE Between TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY') AND TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY') + 0.99999";
        }
        if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND JDT.WORKORDERDATE >= TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')";
        }

        return val;

        // BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY')
    }

    //protected void btnExport_OnClick(object sender, EventArgs e)
    //{
    //    dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    //}

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