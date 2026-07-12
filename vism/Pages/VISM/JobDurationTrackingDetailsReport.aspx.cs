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

public partial class Pages_VISM_JobDurationTrackingDetailsReport : BasePage
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
        try
        {

        }
        catch (Exception ex)
        {
            MessageBox("Invalid date");
            return;
        }


        DataTable dt = obj.GetJobDurationTrackingDetailsData(Criteria());
        rpt.DataSource = dt;
        rpt.DataBind();


        lblfromDate.Text = tbxfromdate.Text;
        lbltoDate.Text = tbxtodate.Text;

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