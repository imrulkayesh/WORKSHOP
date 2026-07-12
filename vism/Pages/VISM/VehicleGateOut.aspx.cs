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
public partial class Pages_VISM_VehicleGateOut : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadWorkshop();
            BindVehicleInOutData();
            //tbxGateOutDate.Text = DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");
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
                            tbxGateOutDate.Text = sysDate.ToString("dd/MM/yyyy h:mm:ss tt");
                        }
                    }
                    catch (Exception ex)
                    {
                        tbxGateOutDate.Text = "Error loading date";
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
        SELECT IO.ID,IO.WORKORDERNO,IO.VEHICLEID,IO.REGNO,IO.VEHICLEID,IO.INDATETIME,IO.DRIVERSTAFFID,IO.JOBID FROM RVS_GATEINOUT IO WHERE IO.ISACTIVE = 1 AND TRUNC(IO.CREATEDATE) = TRUNC(SYSDATE) AND IO.ISOUT = 1 AND IO.WORKSHOPID  = '" + selectedWorkshopId + @"'
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

            if (String.IsNullOrEmpty(tbxJobID.Text))
            {
                MessageBox("Please select Workorder number");
                tbxVehicleid.Focus();
                return;
            }

            string fullJobIDText = tbxJobID.SelectedItem.Text;
            string JobIdNo = fullJobIDText.Split('-')[5].Trim();

            //string WorkOrderNo = fullWorkOrderText.Split('-')[4].Trim();

            db.BeginTransaction();

            //qry = @"update YA.RVS_GATEINOUT set ISOUT=1, JOBID='" + JobIdNo + "', OUTDATETIME=TO_DATE('" + tbxGateOutDate.Text + "','DD/MM/YYYY HH:MI:SS AM'), UPDATEDATE = SYSDATE, UPDATEBY='" + SessionManager.SessionName.UserId + "' where VEHICLEID='" + tbxVehicleid.Text + "' AND  ISOUT=0";
            qry = @"update YA.RVS_GATEINOUT set ISOUT=1, JOBID='" + JobIdNo + "', OUTDATETIME=TO_DATE('" + tbxGateOutDate.Text + "', 'DD/MM/YYYY HH:MI:SS AM'), UPDATEDATE = SYSDATE, UPDATEBY = '" + SessionManager.SessionName.UserId + "'where VEHICLEID = '" + tbxVehicleid.Text + "' AND ISOUT = 0 AND WORKORDERNO in (SELECT WORKORDERNO FROM (SELECT * FROM RVS_GATEINOUT WHERE ISOUT = 0 AND VEHICLEID = '" + tbxVehicleid.Text + "' ORDER BY CREATEDATE DESC) WHERE ROWNUM = 1)";
            dm.ExecuteNonQuery(qry);

            qry = @"update YA.RVS_JOBCARD set ISGATEOUT=1 where ID='" + JobIdNo + "'";
            dm.ExecuteNonQuery(qry);

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

    //private void VehicleWorkOrderdata(int ID)
    //{
    //    ListItem li = new ListItem("Select", "");
    //        string qry = @" SELECT DISTINCT J.ID,'J - '||  J.ID || ' - ' || TO_CHAR(J.CREATEDATE,'DD/MM/YYYY') AS JOBID  
    //         FROM RVS_GATEINOUT IO
    //        LEFT JOIN RVS_JOBCARD J ON IO.VEHICLEID = J.VEHICLEID AND IO.ISOUT = 0
    //         WHERE
    //          J.CREATEDATE > SYSDATE - 100
    //         AND J.VEHICLEID = '" + ID + "' AND J.ISGATEOUT = 0 AND J.ISACTIVE = 1";
    //    tbxJobID.DataSource = dm.ExecuteQuery(qry);
    //    tbxJobID.DataBind();
    //    tbxJobID.Items.Insert(0, li);
    //}
    private void VehicleWorkOrderdata(int ID)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @" SELECT A.JOB_ID AS ID,
          'WO - '
       || WORKORDERNO
       || ' -PI - '
       || PREINSPECTION_ID
       || ' -J - '
       || A.JOB_ID
       || ' - '
       || TO_CHAR (A.JOB_CREATE_DATE, 'DD/MM/YYYY')    AS JOBID
      FROM V_VEHICLE_GATEOUT_PENDING_DATA A
      WHERE A.JOB_OPEN_STATUS=0 AND A.PI_OPEN_STATUS=0 AND A.VEHICLEID = '" + ID + "'";
        tbxJobID.DataSource = dm.ExecuteQuery(qry);
        tbxJobID.DataBind();
        tbxJobID.Items.Insert(0, li);
    }

}