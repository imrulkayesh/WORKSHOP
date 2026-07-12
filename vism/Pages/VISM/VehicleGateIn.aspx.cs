using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_VehicleGateIn : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadWorkshop();
            BindVehicleInOutData();
            VehicleWorkOrderalldata();
            //tbxGateInDate.Text = DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");
            using (OracleConnection conn = new OracleConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "SELECT SYSDATE FROM DUAL";
                    cmd.Connection = conn;

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            DateTime sysDate = Convert.ToDateTime(result);
                            tbxGateInDate.Text = sysDate.ToString("dd/MM/yyyy h:mm:ss tt"); 
                        }
                    }
                    catch (Exception ex)
                    {
                        tbxGateInDate.Text = "Error loading date";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

    }
    protected void BindVehicleInOutData()
    {
        string selectedWorkshopId = ddlWorkWorkshopId.SelectedValue;
        qry = @"
        SELECT IO.ID,IO.WORKORDERNO,IO.VEHICLEID,IO.REGNO,IO.VEHICLEID,IO.INDATETIME,IO.DRIVERSTAFFID FROM RVS_GATEINOUT IO WHERE IO.ISACTIVE = 1 AND TRUNC(IO.CREATEDATE) = TRUNC(SYSDATE) AND IO.WORKSHOPID  = '" + selectedWorkshopId + @"'
        ";
        DataTable dt = new DataTable();
        dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
    }

    protected void LoadWorkshop()
    {
        ddlWorkWorkshopId.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlWorkWorkshopId.DataBind();
    }
 
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(tbxVehicleid.Text))
            {
                MessageBox("Please fill up vehicle data");
                tbxVehicleid.Focus();
                return;
            }
            
            if (String.IsNullOrEmpty(tbxWorkOrderNumber.Text))
            {
                MessageBox("Please select Workorder number");
                tbxVehicleid.Focus();
                return;
            }

            string fullWorkOrderText = tbxWorkOrderNumber.SelectedItem.Text;
            string WorkOrderNo = fullWorkOrderText.Split('-')[0].Trim();

            db.BeginTransaction();

            qry = @"INSERT INTO RVS_GATEINOUT(ID,WORKSHOPID,REGNO,VEHICLEID,WORKORDERNO,DRIVERSTAFFID,INDATETIME,ISOUT,CREATEDBY) 
            VALUES( SQ_RVS_GATEINOUT.NEXTVAL ,'" + ddlWorkWorkshopId.Text + "','" + tbxVehicleRegNumber.Text + "','" + tbxVehicleid.Text + "','" + WorkOrderNo + "','" + tbxStaffId.Text + "',TO_DATE('" + tbxGateInDate.Text + "','DD/MM/YYYY HH:MI:SS AM'),0,'" + SessionManager.SessionName.UserId + "')";
            //db.ExecuteNonQuery(qry);

            //qry = @"update YA.RVS_WORKORDERMASTER set ISGATEIN=1 where WORKORDERNO='" + WorkOrderNo + "'";
            //dm.ExecuteNonQuery(qry);

            int rowsAffected = db.ExecuteNonQuery(qry);

            if (rowsAffected > 0)
            {
                qry = @"UPDATE RVS_WORKORDERMASTER 
            SET ISGATEIN = 1 
            WHERE WORKORDERNO = '" + WorkOrderNo + "'";

                dm.ExecuteNonQuery(qry);
            }

            db.Commit();
            btnSave.Enabled = false;

            MessageBox(SaveSuccess);
            BindVehicleInOutData();
        }
        catch (Exception ex)
        {
            db.Rollback();
            MessageBox(SaveFail + ex.Message);
        }

    }


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
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
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%')) AND V.ISACTIVE='1'  AND ROWNUM <= 20";
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
    protected void tbxVehicleid_TextChanged(object sender, EventArgs e)
    {
        string b = tbxVehicleid.Text;
        string[] a = tbxVehicleid.Text.Trim().Split('-');
        int id = 0;
        try
        {
            id = string.IsNullOrEmpty(a[0]) ? 0 : Convert.ToInt32(a[0]);
        }
        catch
        {
            id = 0;
        }
        VehicleId(id);
        VehicleWorkOrderdata(id);
    }

    private void VehicleId(int ID)
    {
        try
        {
            //DataTable dt2 = new dalVehicle().GetOutPendingCheckByVehicleId(ID);
            //if (dt2.Rows.Count > 0)
            //{
            //    string WORKORDERNO = dt2.Rows[0]["WORKORDERNO"].ToString();
            //    MessageBox("Please Gate Out the previous Gate In.Pending Work Order Number : '" + WORKORDERNO + "'");
            //    tbxVehicleid.Text = "";
            //    return;
            //}

            DataTable dt = new dalVehicle().GetById(ID);
            if (dt.Rows.Count > 0)
            {
                tbxVehicleid.Text = dt.Rows[0]["ID"].ToString();
                tbxVehicleRegNumber.Text = dt.Rows[0]["REGNO"].ToString();
                tbxDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
                tbxStaffId.Text = dt.Rows[0]["DRIVERID"].ToString();

                tbxVehicleid.Enabled = false;
                tbxVehicleRegNumber.Enabled = false;
                tbxDriverName.Enabled = false;
                tbxStaffId.Enabled = false;
            }
            else
            {

            }
        }
        catch
        {

        }
    }

    private void VehicleWorkOrderdata(int ID)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"SELECT ID,WORKORDERNO ||' - '||TO_CHAR(WORKORDERDATE,'DD/MM/YYYY') AS WORKORDERNO from RVS_WORKORDERMASTER 
        WHERE CREATEDATE > SYSDATE-7
        AND VEHICLEIDNEW='" + ID + "' AND WORKORDERSTATUS=1 AND ISGATEIN=0 AND ISACTIVE=1";
        tbxWorkOrderNumber.DataSource = dm.ExecuteQuery(qry);
        tbxWorkOrderNumber.DataBind();
        tbxWorkOrderNumber.Items.Insert(0, li);
    }

    private void VehicleWorkOrderalldata()
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"SELECT ID,WORKORDERNO ||' - '||TO_CHAR(WORKORDERDATE,'DD/MM/YYYY') AS WORKORDERNO from RVS_WORKORDERMASTER 
        WHERE CREATEDATE > SYSDATE-7
        AND WORKORDERSTATUS=1 AND ISGATEIN=0 AND ISACTIVE=1";
        tbxWorkOrderNumber.DataSource = dm.ExecuteQuery(qry);
        tbxWorkOrderNumber.DataBind();
        tbxWorkOrderNumber.Items.Insert(0, li);
    }

    protected void ddlWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string fullWorkOrderText = tbxWorkOrderNumber.SelectedItem.Text;
        string WorkOrderNo = fullWorkOrderText.Split('-')[0].Trim();
        string qry = @"SELECT 
              WOM.ID,
              WOM.WORKORDERNO,
              WOM.WORKORDERDATE,
              V.ID AS VID,
              V.REGNO,
              WOM.DRIVERSTAFFID,
              WOM.RUNNINGMILLAGE,
              WOI.ITEMNAME,
              WOD.REASON
         FROM RVS_WORKORDERMASTER  WOM
              INNER JOIN RVS_WORKORDERDETAILS WOD
                  ON WOM.WORKORDERNO = WOD.WORKORDERNO
                  INNER JOIN RVS_WORKORDERITEMS WOI ON WOD.WORKORDERITEMID = WOI.ID
                  INNER JOIN RVS_VEHICLE V ON WOM.VEHICLEID = V.ID
        WHERE WOM.ISACTIVE = 1 AND WOM.WORKORDERSTATUS = 1 AND WOD.ISACTIVE = 1 AND WOM.WORKORDERNO = '" + WorkOrderNo + @"'
        ORDER BY WOM.WORKORDERNO ";

        DataTable dtd = dm.ExecuteQuery(qry);
        //tbxVehiid.Text = dtd.Rows[0]["ID"].ToString() + " - " + dtd.Rows[0]["REGNO"].ToString();
        //tbxWorkOrderDate.Text = dtd.Rows[0]["WORKORDERDATE"].ToString();
        //tbxStafId.Text = dtd.Rows[0]["DRIVERSTAFFID"].ToString();
        //tbxRunningMillage.Text = dtd.Rows[0]["RUNNINGMILLAGE"].ToString();

        //DtDetailItem.Clear();
        //foreach (DataRow r in dtd.Rows)
        //{
        //    DtDetailItem.Rows.Add(
        //        r["ITEMNAME"].ToString(),
        //        r["REASON"].ToString());
        //}
        //BindWorkOrderRpt();

        //string idString = tbxVehiid.Text;
        int id = Convert.ToInt32(dtd.Rows[0]["VID"].ToString());
        VehicleId(id);
    }

}