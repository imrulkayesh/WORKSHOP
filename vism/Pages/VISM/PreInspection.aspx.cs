using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PreInspection : BasePage
{
    dalPreInspection obj = new dalPreInspection();
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
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            ViewState["ID"] = (int)0;
            LoadDDL();
            LoadProblems(0);
            LoadTechnician();
            //BindData();
            //SendSms();
        }

    }
    private void LoadDDL()
    {
        ddlcategoryid.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlcategoryid.DataBind();
        ddlcategoryid.Items.Insert(0,new ListItem("Select",""));
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();

        string qry = @"Select ID,GROUPNAME text FROM RVS_GROUP";
        ddlgroup.DataSource = dm.ExecuteQuery(qry);
        ddlgroup.DataBind();
    }
    //private void LoadProblems(int PiId)
    //{
    //    if (!string.IsNullOrEmpty(ddlcategoryid.SelectedValue))
    //    {
    //        rptInspection.DataSource = obj.LoadInspectionItems(0);
    //        rptInspection.DataBind();
    //    }
    //    //if (PiId > 0)
    //    //{
    //    //    DataTable dt = obj.GetInspectionItems(PiId);
    //    //    foreach (DataRow dr in dt.Rows)
    //    //    {
    //    //        foreach (RepeaterItem r in rptInspection.Items)
    //    //        {
    //    //            HiddenField itemId = (HiddenField)r.FindControl("hdnInsItemId");
    //    //            if (itemId.Value == dr["ID"].ToString())
    //    //            {
    //    //                CheckBox chk = (CheckBox)r.FindControl("chkrow");
    //    //                RadioButtonList rd = (RadioButtonList)r.FindControl("rdbStatus");
    //    //                TextBox remarks = (TextBox)r.FindControl("tbxInsRemarks");
    //    //                rd.SelectedValue = dr["STATUSID"].ToString();
    //    //                remarks.Text = dr["REMARKS"].ToString();
    //    //                chk.Checked = true;
    //    //            }
    //    //        }
    //    //    }
    //    //}
    //}

    private void LoadProblems(int PiId)
    {
        //if (!string.IsNullOrEmpty(ddlcategoryid.SelectedValue))
        //{
            rptInspection.DataSource = obj.LoadInspectionItems(0);
            rptInspection.DataBind();
        //}
        if (PiId > 0)
        {
            DataTable dt = obj.GetInspectionItems(PiId, 2);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem r in rptInspection.Items)
                {
                    HiddenField itemId = (HiddenField)r.FindControl("hdnInsItemId");
                    if (itemId.Value == dr["ID"].ToString())
                    {
                        CheckBox chk = (CheckBox)r.FindControl("chkrow");
                        RadioButtonList rd = (RadioButtonList)r.FindControl("rdbStatus");
                        TextBox remarks = (TextBox)r.FindControl("tbxInsRemarks");
                        rd.SelectedValue = dr["STATUSID"].ToString();
                        remarks.Text = dr["REMARKS"].ToString();
                        chk.Checked = true;
                    }
                }
            }
        }
    }
    private void LoadTechnician()
    {
        ListItem li = new ListItem("Select", "0");
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
        {
            ddlTechnician.DataSource = new Common().GetAllTechnician(Convert.ToInt32(ddlworkshopid.SelectedValue));

        }
        else
        {
            ddlTechnician.DataSource = null;
        }
        ddlTechnician.DataBind();
        ddlTechnician.Items.Insert(0, li);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            qry = @"update rvs_vehicle set DRIVERNAME='"+ tbxDriverName.Text + "',DRIVERMOBILE='"+tbxDriverMobile.Text+"' where id='"+ tbxVehicleid.Text.Split('-')[0].Trim() + "'";
            dm.ExecuteNonQuery(qry);
            qry = @"update RVS_CUSTOMER set MOBILE='" + tbxCusMobile.Text + "' where id in (select CUSTOMERID from rvs_vehicle where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "')";
            dm.ExecuteNonQuery(qry);
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);
            string vldt = obj.CheckDuplicate(Convert.ToInt32(hdnVehicleId.Value));
            if (vldt == "0")
            {
                // int Pid = obj.Insert(Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text, Millage, tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue));
                int Pid=0;
                if (Pid > 0)
                {
                    InsertPIDetails(Pid);
                }
                SendSms();
                MessageBox(SaveSuccess);
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

                BindData();
                
                ClearAll();
            }
            else
            {
                
                MessageController.Show(vldt, MessageType.Error, Page);
            }
        }
        catch (Exception ex)
        {
            MessageBox(SaveFail + ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

        }
    }
    DbManagerSms ds=new DbManagerSms();
    private void SendSms()
    {
        try
        {
            string vid = tbxVehicleid.Text.Split('-')[0].Trim();
            string qry = @"SELECT VH.REGNO,CS.CUSTOMERNAME,CS.MOBILE,VH.DRIVERNAME,VH.DRIVERMOBILE,WS.WORKSHOP FROM RVS_PREINSPECTION PR
INNER JOIN RVS_WORKSHOP WS ON PR.WORKSHOPID=WS.ID
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN RVS_CUSTOMER CS ON VH.CUSTOMERID=CS.ID
WHERE VH.ID='" + vid + "' AND PR.ISOPEN=1";
            DataTable dt = dm.ExecuteQuery(qry);
            if (dt.Rows.Count > 0)
            {
                string sbd = "Dear Sir,\nYour vehicle " + dt.Rows[0]["REGNO"] + " has just entered in " + dt.Rows[0]["WORKSHOP"] +
                             " for servicing.\nDriver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " +
                             dt.Rows[0]["DRIVERMOBILE"];
                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
VALUES ('VISM', TO_DATE(SYSDATE), '" + dt.Rows[0]["MOBILE"] + "', '" + sbd + "', 0, '201262', '201262', NULL)";
                ds.ExecuteNonQuery(qry);
                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
VALUES ('VISM', TO_DATE(SYSDATE), '" + dt.Rows[0]["DRIVERMOBILE"] + "', '" + sbd + "', 0, '201262', '201262', NULL)";
                ds.ExecuteNonQuery(qry);
                //need comment bellow
                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
VALUES ('VISM', TO_DATE(SYSDATE), '01992665872', '" + sbd + "', 0, '201262', '201262', NULL)";
                ds.ExecuteNonQuery(qry);
                //imtiaz sir
//                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
//VALUES ('VISM', TO_DATE(SYSDATE), '01844609176', '" + sbd + "', 0, '201262', '201262', NULL)";
//                ds.ExecuteNonQuery(qry);
                //sorif vai
//                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
//VALUES ('VISM', TO_DATE(SYSDATE), '01841357475', '" + sbd + "', 0, '201262', '201262', NULL)";
//                ds.ExecuteNonQuery(qry);
                //tonu sir
//                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
//VALUES ('VISM', TO_DATE(SYSDATE), '01912257259', '" + sbd + "', 0, '201262', '201262', NULL)";
//                ds.ExecuteNonQuery(qry);
                //chief sir
                //qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
                //VALUES ('VISM', TO_DATE(SYSDATE), '01912257257', '" + sbd + "', 0, '201262', '201262', NULL)";
                //ds.ExecuteNonQuery(qry);
                qry = @"DELETE RFL.T_DSMS WHERE DSMS_DONO = 'VISM' AND IUSER= '201262' AND DSMS_ACTV = 1";
                ds.ExecuteNonQuery(qry);
            }
            qry = @"SELECT SU.MOBILE FROM RVS_PREINSPECTION PR
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID 
INNER JOIN BS_MOTHERCOMPANY MC ON CO.MOTHERCOMPANYID=MC.ID
INNER JOIN RVS_SMSUSER SU ON SU.MCOMPANYID=CO.MOTHERCOMPANYID
WHERE VH.ID='" + vid + "' AND PR.ISOPEN=1 AND SU.ISACTIVE=1";
            DataTable dtt = dm.ExecuteQuery(qry);
            foreach (DataRow r in dtt.Rows)
            {
                string sbd = "Dear Sir,\nThe vehicle " + dt.Rows[0]["REGNO"] + " has just entered in " + dt.Rows[0]["WORKSHOP"] +
                             " for servicing.\nDriver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " +
                             dt.Rows[0]["DRIVERMOBILE"];
                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
VALUES ('VISM', TO_DATE(SYSDATE), '" + r["MOBILE"] + "', '" + sbd + "', 0, '201262', '201262', NULL)";
                ds.ExecuteNonQuery(qry);
            }
        }
        catch
        {
            //
        }
        
         
    }

    private void InsertPIDetails(int Pid)
    {
        obj.DeletePIDetails(Pid);
        foreach (RepeaterItem r in rptInspection.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField itemId = (HiddenField)r.FindControl("hdnInsItemId");
                RadioButtonList status = (RadioButtonList)r.FindControl("rdbStatus");
                TextBox remarks = (TextBox)r.FindControl("tbxInsRemarks");
                obj.InsertPIDetails(Pid, Convert.ToInt32(itemId.Value), Convert.ToInt32(status.SelectedValue), remarks.Text);
            }
        }
    }

  
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32 (tbxMillage.Text);

        //    obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text, Millage, tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue));
            InsertPIDetails(Convert.ToInt32(ViewState["ID"]));
            MessageBox(UpdateSuccess);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            //BindData();
            ClearAll();
        }
        catch (Exception ex)
        {
            MessageBox(UpdateFail+ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxVehicleid.Text = "";
        tbxremark.Text = "";
        hdnVehicleId.Value = "0";
        LoadProblems(0);
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
        //if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue) && !string.IsNullOrEmpty(ddlcategoryid.SelectedValue))
        //{
        //    DataTable dt = obj.GetAll(Convert.ToInt32(ddlworkshopid.SelectedValue), Convert.ToInt32(ddlcategoryid.SelectedValue));
        //    rpt.DataSource = dt;
        //}
        //else
        //{
        //    rpt.DataSource = null;
        //}
        qry = @"select * from 
(SELECT PI.*,to_char(PI.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Entry Date"", SC.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS FROM RVS_PREINSPECTION PI
        INNER JOIN RVS_SERVICETYPE SC ON SC.ID= PI.SERVICETYPEID
        INNER JOIN RVS_VEHICLE V ON V.ID=PI.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID WHERE PI.ISOPEN=1 and PI.SERVICETYPEID='" + ddlcategoryid.SelectedValue + "' and PI.WORKSHOPID='" + ddlworkshopid.SelectedValue + @"'
        and pi.createdby = '"+SessionManager.SessionName.UserId+@"' order by pi.CREATEDATE desc
        ) where rownum< 2";
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            VehicleId(Convert.ToInt32(dt.Rows[0]["VEHICLEID"]));
            ddlTechnician.SelectedValue = dt.Rows[0]["TECSTAFFID"].ToString();
            ddlcategoryid.SelectedValue = dt.Rows[0]["SERVICETYPEID"].ToString();
            ddlworkshopid.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
            tbxDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
            tbxDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
            tbxMillage.Text = dt.Rows[0]["MILLAGE"].ToString();
            ddlgroup.SelectedValue = dt.Rows[0]["GROUPID"].ToString();
            tbxremark.Text = dt.Rows[0]["REMARKS"].ToString();
            LoadProblems(ID);
            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_PREINSPECTION", ID);
            MessageBox(DeleteSuccess);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail+ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
    protected void ddlcategoryid_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindData();
    }

    protected void ddlworkshopid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTechnician();
    }
    protected void btnJobCard_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            DataTable dts = new dalJobCard().GetByVehicleId(Convert.ToInt32(dt.Rows[0]["VEHICLEID"]));
            if (dts.Rows.Count == 0)
            {
                int JobID = new dalJobCard().Insert(Convert.ToInt32(dt.Rows[0]["VEHICLEID"]), DateTime.Now.ToString("dd/MM/yyyy"), Convert.ToInt32(dt.Rows[0]["WORKSHOPID"]), ID, 2, Convert.ToInt32(dt.Rows[0]["SERVICETYPEID"]), "", "", dt.Rows[0]["REMARKS"].ToString(), Convert.ToInt32(dt.Rows[0]["GROUPID"]));
                //obj.ClosePI(ID);
                Response.Redirect("JobCard.aspx?JobID=" + JobID.ToString());
            }
            else
            {
                MessageController.Show("Open jobcard exists at " + dts.Rows[0]["WORKSHOP"] + " for this vehicle.", MessageType.Error, Page);
            }
        }


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
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%'))  AND ROWNUM <= 20 AND ISACTIVE='1'";
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
        //tbxVehicleid.Text = b;
    }
    private void VehicleId(int ID)
    {
        try
        {
            DataTable dt = new dalVehicle().GetById(ID);
            if (dt.Rows.Count > 0)
            {
                hdnVehicleId.Value = dt.Rows[0]["ID"].ToString();
                tbxVehicleid.Text = dt.Rows[0]["ID"].ToString() + " - " + dt.Rows[0]["REGNO"].ToString();
                tbxCustomer.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                tbxChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                tbxEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                tbxModel.Text = dt.Rows[0]["MODELNAME"].ToString();
                tbxManufactureYear.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
                tbxVehicleType.Text = dt.Rows[0]["VEHICLETYPENAME"].ToString();
                tbxFuelType.Text = dt.Rows[0]["FUELTYPENAME"].ToString();
                tbxLocation.Text = dt.Rows[0]["CENTERNAME"].ToString();
                tbxCompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();
                tbxCountry.Text = dt.Rows[0]["COUNTRYNAME"].ToString();

                tbxDriverName.Text= dt.Rows[0]["DRIVERNAME"].ToString();
                tbxDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
                tbxCusMobile.Text = dt.Rows[0]["MOBILE"].ToString();

            }
            else
            {
                tbxVehicleid.Text = "";
                hdnVehicleId.Value = "0";
            }
        }
        catch
        {
            tbxVehicleid.Text = "";
            hdnVehicleId.Value = "0";
        }
    }
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"PreInspectionView.aspx?PIID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnClosePI_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            obj.ClosePI(ID);
            //BindData();
            MessageController.Show("PI Closed Successfully.", MessageType.Confirmation, Page);
        }
        catch (Exception ex)
        {
            MessageController.Show("Something is Wrong.", MessageType.Error, Page);
        }

    }

    protected void btnPreInsSearch_Click(object sender, EventArgs e)
    {
        qry = @"SELECT PI.*,to_char(PI.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Entry Date"", SC.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS FROM RVS_PREINSPECTION PI
        INNER JOIN RVS_SERVICETYPE SC ON SC.ID = PI.SERVICETYPEID
        INNER JOIN RVS_VEHICLE V ON V.ID = PI.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID = V.CUSTOMERID WHERE PI.ISOPEN = 1 and VEHICLEID='" +
              tbxVehileForJobIns.Text.Split('-')[0].Trim() + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
    }
}