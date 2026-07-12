using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_GateInn : BasePage
{
    dalPreInspection obj = new dalPreInspection();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbxVehicleid.Focus();
            LoadDdl();
            LoadTechnician();
            DtDetailAddColumn();
            BindRpt();
        }

    }
    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "0");


        ddlcategoryid.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlcategoryid.DataBind();
        ddlcategoryid.Items.Insert(0, li);
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();
        //ddlworkshopid.Items.Insert(0, li);


        string qry1 = @"Select ID,GROUPNAME text FROM RVS_GROUP";
        ddlgroup.DataSource = dm.ExecuteQuery(qry1);
        ddlgroup.DataBind();

    }

    private void DtDetailAddColumn()
    {
        DtDetail = new DataTable();
        DtDetail.Columns.Add("CategoryCode");
        DtDetail.Columns.Add("CategoryName");
        DtDetail.Columns.Add("ProblemCode");
        DtDetail.Columns.Add("ProblemName");
        DtDetail.Columns.Add("ProblemRemarks");

    }

    private void BindRpt()
    {

    }


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void imgbtnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        for (int i = 0; i < DtDetail.Rows.Count; i++)
        {
            if (DtDetail.Rows[i]["ProblemCode"].ToString() == e.CommandArgument.ToString())
            {
                DtDetail.Rows[i].Delete();
            }
        }
        BindRpt();
    }




    ///////////////
    protected void tbxVehicleid_TextChanged(object sender, EventArgs e)
    {
        string b = tbxVehicleid.Text.Replace("'", "''");
        string qry = @"select ID from RVS_VEHICLE where REGNO='" + b + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count > 0)
        {
            int id = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
            VehicleId(id);
            SaveIn(id);
        }
        else
        {

        }


        //try
        //{
        //    id = string.IsNullOrEmpty(a[0]) ? 0 : Convert.ToInt32(a[0]);
        //}
        //catch
        //{
        //    id = 0;
        //}

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
                // tbxManufactureYear.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();

                //string iDate = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
                tbxManufactureYear.Text = Convert.ToDateTime(dt.Rows[0]["MANUFACTURINGYEAR"].ToString()).ToString("dd-MM-yyyy");
                tbxVehicleType.Text = dt.Rows[0]["VEHICLETYPENAME"].ToString();
                tbxFuelType.Text = dt.Rows[0]["FUELTYPENAME"].ToString();
                tbxLocation.Text = dt.Rows[0]["CENTERNAME"].ToString();
                tbxCompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();
                tbxCountry.Text = dt.Rows[0]["COUNTRYNAME"].ToString();
                tbxCusMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                tbxDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
                tbxDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();

                if (dt.Rows[0]["DLdate"].ToString() == "" || dt.Rows[0]["DLdate"].ToString() == null)
                {
                    tbxDateLine1.Text = "";
                }
                else
                {
                    tbxDateLine1.Text = Convert.ToDateTime(dt.Rows[0]["DLdate"].ToString()).ToString("dd-MM-yyyy");
                }


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
    protected void ddlworkshopid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTechnician();
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

    private void SaveIn(int ID)
    {
        try
        {
            qry = @"update YA.rvs_vehicle set DRIVERNAME='" + tbxDriverName.Text + "',DRIVERMOBILE='" + tbxDriverMobile.Text + "' where id='" + ID + "'";
            dm.ExecuteNonQuery(qry);
            qry = @"update YA.RVS_CUSTOMER set MOBILE='" + tbxCusMobile.Text + "' where id in (select CUSTOMERID from YA.rvs_vehicle where id='" + ID + "')";
            dm.ExecuteNonQuery(qry);
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);
            string vldt = obj.CheckDuplicate(Convert.ToInt32(hdnVehicleId.Value));
            int Pid;
            int vid = Convert.ToInt32(hdnVehicleId.Value);
            //int SEVTYPE = Convert.ToInt32(ddlcategoryid.SelectedValue);
            if (vldt == "0")
            {

                Pid = obj.Insert(Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text, Millage, tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue), tbxEftDate.Text, tbxDateLine1.Text, "");
                //       IsSave = obj.SaveAll(newInNo, newVCExNo, tbxChDate.Text, tbxDispDate.Text, tbxDistCode.Text,
                //tbxDistrName.Text.Replace("'", "''"), tbxAdd1.Text.Replace("'", "''"), tbxAdd2.Text.Replace("'", "''"), tbxAdd3.Text.Replace("'", "''"), tbxDestination.Text.Replace("'", "''"), tbxCustVatIdNo.Text.Replace("'", "''"),
                //tbxTruckNo.Text.Replace("'", "''"), ddlWh.SelectedValue, ddlCompany.SelectedValue,
                //ddlChType.SelectedValue, totAmt, 'Y', dt);

                // SendSms();
                SendInitialMail(vid);
                MessageBox(SaveSuccess);
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

                // BindData();

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
    protected void btnSave_Click(object sender, EventArgs e)
    {



    }

    DbManagerSms ds = new DbManagerSms();

    public void SendInitialMail(long dispatchId)
    {
        string vid = tbxVehicleid.Text.Split('-')[0].Trim();
        string qry = @"SELECT VH.REGNO,CS.CUSTOMERNAME,CS.MOBILE,VH.DRIVERNAME,VH.DRIVERMOBILE,WS.WORKSHOP,CS.EMAIL,CS.CC1,CS.CC2 FROM RVS_PREINSPECTION PR
INNER JOIN RVS_WORKSHOP WS ON PR.WORKSHOPID=WS.ID
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN RVS_CUSTOMER CS ON VH.CUSTOMERID=CS.ID
WHERE VH.ID='" + vid + "' AND PR.ISOPEN=1";
        DataTable dt = dm.ExecuteQuery(qry);
        string fromuser = dt.Rows[0]["EMAIL"].ToString();
        List<string> Approver = new dalApprover().GetByCreatedby(vid);
        //string MessageforSender = "<h3>Your dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " is waiting for approval</h3>";
        //MessageforSender += "<b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        //MessageforSender += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        //MessageforSender += "<h4>Item Details : </4>";

        //string MessageApprover = "<h3>New dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " waiting for approval</h3>";
        //MessageApprover += "<b>Sendar : </b>" + dt.Rows[0]["EMPLOYEENAME"].ToString();
        //MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["OFFICEMOBILE"].ToString();
        //MessageApprover += "<hr><b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        //MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        //MessageApprover += "<h4>Item Details : </4>";

        //string sbd = "Dear Sir,\nYour vehicle " + dt.Rows[0]["REGNO"] + " has just entered in " + dt.Rows[0]["WORKSHOP"] +
        //                     " for servicing.\nDriver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " +
        //                     dt.Rows[0]["DRIVERMOBILE"];

        string sbd = "Dear Sir,";
        sbd += "<br>Your vehicle " + dt.Rows[0]["REGNO"] + " has just entered in " + dt.Rows[0]["WORKSHOP"] + "\nfor servicing.";
        sbd += "<br>Driver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " + dt.Rows[0]["DRIVERMOBILE"];

        //SendEmail.SendMultipleMail(Approver, "Vehicle has entered in workshop", sbd);

        string path = "";
        if (Approver.Count > 0)
        {
            SendEmail.SendMultipleMail(Approver, "Vehicle has entered in workshop", sbd);
        }
        else
        {

        }
        //SendEmail.SendSingleMail(fromuser, "Vehicle has entered in workshop", sbd);

    }


    private void SendSms()
    {
        try
        {
            string vid = tbxVehicleid.Text.Split('-')[0].Trim();
            string qry = @"SELECT VH.REGNO,CS.CUSTOMERNAME,CS.MOBILE,VH.DRIVERNAME,VH.DRIVERMOBILE,WS.WORKSHOP,CS.CC1,CS.CC2 FROM RVS_PREINSPECTION PR
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
VALUES ('VISM', TO_DATE(SYSDATE), '" + dt.Rows[0]["MOBILE"] + "', '" + sbd + "', 0, '135323', '135323', NULL)";
                ds.ExecuteNonQuery(qry);
                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
VALUES ('VISM', TO_DATE(SYSDATE), '" + dt.Rows[0]["DRIVERMOBILE"] + "', '" + sbd + "', 0, '135323', '135323', NULL)";
                ds.ExecuteNonQuery(qry);
                //need comment bellow
                //                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
                //VALUES ('VISM', TO_DATE(SYSDATE), '01992665872', '" + sbd + "', 0, '135323', '135323', NULL)";
                //                ds.ExecuteNonQuery(qry);
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
                qry = @"DELETE RFL.T_DSMS WHERE DSMS_DONO = 'VISM' AND IUSER= '135323' AND DSMS_ACTV = 1";
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
VALUES ('VISM', TO_DATE(SYSDATE), '" + r["MOBILE"] + "', '" + sbd + "', 0, '135323', '135323', NULL)";
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
        //foreach (RepeaterItem r in rpt.Items)
        //{
        //    CheckBox chk = (CheckBox)r.FindControl("chkrow");
        //    if (chk.Checked)
        //    {
        //        HiddenField itemId = (HiddenField)r.FindControl("hdnInsItemId");
        //        RadioButtonList status = (RadioButtonList)r.FindControl("rdbStatus");
        //        TextBox remarks = (TextBox)r.FindControl("tbxInsRemarks");
        //        obj.InsertPIDetails(Pid, Convert.ToInt32(itemId.Value), Convert.ToInt32(status.SelectedValue), remarks.Text);
        //    }
        //}
        foreach (DataRow r in DtDetail.Rows)
        {
            obj.InsertPIDetails(Pid, Convert.ToInt32(r["ProblemCode"].ToString()), 2, r["ProblemRemarks"].ToString());
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);

            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text, Millage, tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue), tbxEftDate.Text, tbxDateLine1.Text);
            InsertPIDetails(Convert.ToInt32(ViewState["ID"]));
            MessageBox(UpdateSuccess);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            //BindData();
            //ClearAll();
        }
        catch (Exception ex)
        {
            MessageBox(UpdateFail + ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        //ClearAll();
    }

    protected void ClearAll()
    {
        tbxVehicleid.Text = "";
        tbxremark.Text = "";
        hdnVehicleId.Value = "0";
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

}