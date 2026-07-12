using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Drawing;
using System.Text;


public partial class Pages_VISM_FuelDemand : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    dalFuelType obj = new dalFuelType();
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
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");

            }
            ViewState["ID"] = (int)0;
            BindData();
            LoadDdl();
           // Panel1.Visible=false;
        }

    }

    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "0");


        ddlFuelType.DataSource = dm.ExecuteQuery("SELECT ITEMCODE ID,ITEMNAME TEXT FROM RVS_ITEMMASTER where ITEMCODE='0525736000'");
        ddlFuelType.DataBind();
        ddlFuelType.Items.Insert(0, li);

        //string qry =
        //    @"select id,centername text  from BS_BUSINESSCENTER where ID in ('450','374') order by centername";
        //ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlSendLoc.DataBind();
        //ddlSendLoc.Items.Insert(0, li);

        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and ID in ('5','8086') order by VISMWHNAME";
        ddlPump.DataSource = dm.ExecuteQuery(qry);
        ddlPump.DataBind();
        ddlPump.Items.Insert(0, li);
        ddlPump.Enabled = true;


        ddlVehicleType.DataSource = dm.ExecuteQuery("select id ID,VEHICLECOMTYPENAME TEXT from ya.RVS_VEHICLECOMTYPE");
        ddlVehicleType.DataBind();
        ddlVehicleType.Items.Insert(0, li);

       
       // Session["CVTI"]= Convert.ToInt32(ddlVehicleType.SelectedValue);


    }

    protected void ddlVehicleType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SessionManager.SessionName.CompanyVehicleTypeId = Convert.ToInt32(ddlVehicleType.SelectedValue);
    }


    protected void ddlFuelType_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        qry = @"select RATE FROM RVS_ITEMMASTER where ITEMCODE='"+ ddlFuelType.SelectedValue + "'";
        tbxrate.Text = dm.ExecuteScalar(qry);
    }

    private void BindData()
    {
        DataTable dt = new Common().GetAllFD(SessionManager.SessionName.UserId);
        rpt.DataSource = dt;
        rpt.DataBind();

        DataTable dtt = new Common().GetAllAPPROVED(SessionManager.SessionName.UserId);
        rptApprove.DataSource = dtt;
        rptApprove.DataBind();
    }


    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            string ViD = tbxRecVeh.Text.Split('-')[0];
            obj.InsertFDMND(ViD,ddlFuelType.SelectedValue, ddlPump.SelectedValue,decimal.Parse(Request.Form[tbxQty.UniqueID]),decimal.Parse(Request.Form[tbxrate.UniqueID]), SessionManager.SessionName.DepartmentId, SessionManager.SessionName.BusinessCentetid, tbxRemarks.Text.Replace("'", "''"));

            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            BindData();
            ClearAll(Page.Controls);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

        }
    }


    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchVehicle(string prefixText, int count)
    {
        string cvti = SessionManager.SessionName.CompanyVehicleTypeId.ToString();

        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME VEHICLE FROM RVS_VEHICLE V
                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                INNER JOIN BS_COMPANY CP ON CP.ID=V.COMPANYID
                INNER JOIN RVS_VEHICLECOMTYPE VT ON VT.ID=CP.VEHICLECOMTYPEID AND VT.ID='"+ cvti + @"'
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%')) and ISACTIVE='1'  AND ROWNUM <= 20";
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


    protected void imgbtnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_FUELDMD", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

    protected void btnBillPrint_Command(object sender, CommandEventArgs e)
    {
        string intPartyId = ((ImageButton)sender).CommandArgument;
        string lnk = "FuelDemandPrint.aspx?intPartyId=" + intPartyId.ToString();
        ScriptManager.RegisterStartupScript(this, GetType(), "openInNewTab", "window.open('" + lnk + "', '_blank');", true);
    }

    private void ClearAll(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();
            else if (ctrl is CheckBox)
                ((CheckBox)ctrl).Checked = false;

            ClearAll(ctrl.Controls);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll(Page.Controls);
    }

}