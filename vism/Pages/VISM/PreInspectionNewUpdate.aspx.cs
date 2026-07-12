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


public partial class Pages_VISM_PreInspectionNewUpdate : BasePage
{
        dalPreInspection obj = new dalPreInspection();
        DatabaseManagerORCL dm = new DatabaseManagerORCL();
        DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCAS.Attributes.Add("placeholder", "1)Name - Condition :\r\n2)Name - Condition :\r\n3)Name - Condition :\r\n*)Name - Condition :");
        if (!IsPostBack)
        {
            LoadDdl();
            LoadTechnician();
            DtDetailAddColumn();
            BindRpt();
            //WorkOrderLoad(id);
            WorkOrderLoad();
        }

    }
    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "0");
        string qry =
            @"select id,CATNAME text  from RVS_SERVICECATEGORY order by CATNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);

        ddlcategoryid.DataSource = new Common().GetAllServiceType("RVS_SERVICETYPE");
        ddlcategoryid.DataBind();

        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();

        ddlWorkWorkshopId.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlWorkWorkshopId.DataBind();

        string qry1 = @"Select ID,GROUPNAME text FROM RVS_GROUP";
        ddlgroup.DataSource = dm.ExecuteQuery(qry1);
        ddlgroup.DataBind();
        DtWorkOrderDetailAddColumn();
    }
    //================ Service Item Code ===============
    

    //================ Work Order Code ===============
    private void BindWorkOrderRpt()
    {
        DtDetailItem.AcceptChanges();
        rptWorkOrder.DataSource = DtDetailItem;
        rptWorkOrder.DataBind();
    }

    private void DtWorkOrderDetailAddColumn()
    {
        DtDetailItem = new DataTable();
        DtDetailItem.Columns.Add("ITEMNAME");
        DtDetailItem.Columns.Add("REASON");
    }
    protected DataTable DtDetailItem
    {
        set
        {
            Session["DtDetailItem"] = value;
        }
        get
        {
            try
            {
                return (DataTable)Session["DtDetailItem"];
            }
            catch
            {
                return null;
            }
        }
    }

    //================ Work Order Code End ===============

    private void DtDetailAddColumn()
    {
        DtDetail = new DataTable();
        DtDetail.Columns.Add("CategoryCode");
        DtDetail.Columns.Add("CategoryName");
        DtDetail.Columns.Add("ProblemCode");
        DtDetail.Columns.Add("ProblemName");
        DtDetail.Columns.Add("ProblemRemarks");
        //serrvice
        DtDetail.Columns.Add("Qty");
        DtDetail.Columns.Add("LaborRate");
        DtDetail.Columns.Add("LaborQty");
        DtDetail.Columns.Add("WorkingHour");
        DtDetail.Columns.Add("TotAmt");
    }

    private void BindRpt()
    {
        DtDetail.AcceptChanges();
        rptDetails.DataSource = DtDetail;
        rptDetails.DataBind();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        foreach (DataRow row in DtDetail.Rows)
        {
            if (ddlProblemName.SelectedValue != row["ProblemCode"].ToString()) continue;
            MessageBox(ddlProblemName.SelectedItem.Text + " is already added.");
            return;
        }
        //string totAmt = (Convert.ToDecimal(tbxQty.Text) * Convert.ToDecimal(tbxLaborRate.Text)).ToString();
        string totAmt = (Convert.ToDecimal(tbxQty.Text) * Convert.ToDecimal(tbxLaborRate.Value) * Convert.ToDecimal(tbxLaborQty.Value) * Convert.ToDecimal(tbxWorkingHour.Value)).ToString();
        //tbxAmt.Text = totAmt;
        DtDetail.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlProblemName.SelectedValue, ddlProblemName.SelectedItem.Text, tbxProbRemarks.Text, tbxQty.Text, tbxLaborRate.Value, tbxLaborQty.Value, tbxWorkingHour.Value, totAmt);
        BindRpt();
        ddlCategory_OnSelectedIndexChanged(null, null);
        tbxProbRemarks.Text = "";
        tbxQty.Text = "";
        tbxAmount.Text = "";
        //}

    }

    protected void btnSave_OnClick(object sender, EventArgs e)
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

    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        //string qry = @"select id,ITEMNAME text from RVS_INSPECTIONITEMS where SERVICECATID='" + ddlCategory.SelectedValue + "' order by ITEMNAME";
        string qry = @"select   id,ITEMNAME || ' L-' || LABORQTY || ' WH-' || WORKINGHOUR text from RVS_INSPECTIONITEMS where INSPECTIONTYPE=0 and SERVICECATID='" + ddlCategory.SelectedValue + "' order by ITEMNAME";
        ddlProblemName.DataSource = dm.ExecuteQuery(qry);
        ddlProblemName.DataBind();
        ddlProblemName.Items.Insert(0, li);
        foreach (DataRow r in DtDetail.Rows)
        {
            foreach (ListItem ltItem in ddlProblemName.Items)
            {
                if (ltItem.Value == r["ProblemCode"].ToString())
                {
                    ddlProblemName.Items.FindByValue(r["ProblemCode"].ToString()).Attributes.Add("disabled", "disabled");
                    ddlProblemName.Items.FindByValue(r["ProblemCode"].ToString()).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");
                }
            }

        }
        ddlProblemName.Enabled = true;

    }

    protected void ddlProblemName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"SELECT ID,ITEMNAME,LABORRATE,LABORQTY,WORKINGHOUR,LABORCHARGE FROM RVS_INSPECTIONITEMS WHERE ID='" + ddlProblemName.SelectedValue + "'";
        var data = dm.ExecuteQuery(qry);
        if (data != null && data.Rows.Count > 0)
        {
            tbxAmount.Text = data.Rows[0]["LABORCHARGE"].ToString();
            tbxLaborRate.Value = data.Rows[0]["LABORRATE"].ToString();
            tbxLaborQty.Value = data.Rows[0]["LABORQTY"].ToString();
            tbxWorkingHour.Value = data.Rows[0]["WORKINGHOUR"].ToString();
            tbxAmount.Enabled = false;
        }
    }

    protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            DataRow rows = DtDetail.Select("ProblemCode = '" + e.CommandArgument + "'").FirstOrDefault();
            ddlCategory.SelectedValue = rows["CategoryCode"].ToString();
            ddlCategory_OnSelectedIndexChanged(null, null);
            ddlProblemName.SelectedValue = e.CommandArgument.ToString();
            tbxProbRemarks.Text = rows["ProblemRemarks"].ToString();
            tbxQty.Text = rows["Qty"].ToString();
            //tbxAmount.Text = rows["LABORCHARGE"].ToString();
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            ddlCategory.Enabled = false;
            tbxAmount.Enabled = false;
            ddlProblemName.Enabled = false;
            ViewState["ID"] = e.CommandArgument.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        // int ID = Convert.ToInt32(e.CommandArgument);

        string JI = ViewState["JI"].ToString();


        DataRow rows = DtDetail.Select("ProblemCode = '" + (string)ViewState["ID"] + "'").FirstOrDefault();
        if (rows != null) rows["ProblemRemarks"] = tbxProbRemarks.Text;
        rows["Qty"] = tbxQty.Text;

        int ProbCode = Convert.ToInt32(rows["ProblemCode"]);

        int Qty = Convert.ToInt32(rows["Qty"]);

        string pr = rows["ProblemRemarks"].ToString();

        try
        {
            string qry = @"update RVS_PIDETAILS set REMARKS='" + pr + "', QUANTITY='" + Qty + "' where INSPECTIONITEMID='" + ProbCode + "' and PIID='" + JI + @"'";
            db.BeginTransaction();
            db.ExecuteNonQuery(qry);
            db.Commit();

            BindRpt();
            ddlCategory.Enabled = true;
            ddlProblemName.SelectedIndex = 0;
            ddlProblemName.Enabled = true;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            tbxProbRemarks.Text = "";
            foreach (DataRow r in DtDetail.Rows)
            {
                foreach (ListItem ltItem in ddlProblemName.Items)
                {
                    if (ltItem.Value == r["ProblemCode"].ToString())
                    {
                        ddlProblemName.Items.FindByValue(r["ProblemCode"].ToString()).Attributes.Add("disabled", "disabled");
                        ddlProblemName.Items.FindByValue(r["ProblemCode"].ToString()).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");


                    }
                }

            }

        }
        catch (Exception ex)
        {
            db.Rollback();
            MessageBox(SaveFail + ex.Message);
        }

    }

    ///////////////
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
        //WorkOrderLoad(id);
    }

    private void WorkOrderLoad()
    {
        string selectedValue = ddlworkshopid.SelectedValue;

        if (!string.IsNullOrEmpty(selectedValue))
        {
            int VehWorkShopId;
            if (int.TryParse(selectedValue, out VehWorkShopId))
            {
                string qry = @"SELECT ID, WORKORDERNO FROM RVS_WORKORDERMASTER WHERE ISACTIVE=1 AND WORKORDERSTATUS=1 AND WORKSHOPID='" + VehWorkShopId + "' AND CREATEDATE > SYSDATE-7 AND ISGATEIN=1";
                ListItem li = new ListItem("Select", "0");

                ddlWorkOrderNo.DataSource = dm.ExecuteQuery(qry);
                ddlWorkOrderNo.DataBind();
                ddlWorkOrderNo.Items.Insert(0, li);
            }
            else
            {
                // Handle the case where the selected value cannot be converted to an integer
                // For example, display an error message or set a default value for ddlWorkOrderNo
            }
        }
        else
        {
            ddlWorkOrderNo.DataSource = null;
            ddlWorkOrderNo.DataBind();
            ListItem li = new ListItem("Select", "0");
            ddlWorkOrderNo.Items.Insert(0, li);
        }
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

                tbxStaffId.Text = dt.Rows[0]["DRIVERID"].ToString();
                //tbxCCRD.Text = dt.Rows[0]["CNGCYLINDERREDDATE"].ToString();
                //tbxCCRD.Text = Convert.ToDateTime(dt.Rows[0]["CNGCYLINDERREDDATE"].ToString()).ToString("dd-MM-yyyy");

                // Null-check and validate CNG Cylinder Red Date
                object cngCylinderRedDateObject = dt.Rows[0]["CNGCYLINDERREDDATE"];
                if (cngCylinderRedDateObject != DBNull.Value)
                {
                    DateTime cngCylinderRedDate;
                    if (DateTime.TryParse(cngCylinderRedDateObject.ToString(), out cngCylinderRedDate))
                    {
                        tbxCCRD.Text = cngCylinderRedDate.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        // Handle invalid date format
                        // You might want to log or notify the user about this issue
                    }
                }
                else
                {
                    // Handle null value
                    // You might want to provide a default value or notify the user about the absence of data
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            qry = @"update rvs_vehicle set DRIVERNAME='" + tbxDriverName.Text + "',DRIVERMOBILE='" + tbxDriverMobile.Text.Replace("'", "") + "',DRIVERID='" + tbxStaffId.Text + "' where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "'";
            dm.ExecuteNonQuery(qry);
            qry = @"update RVS_CUSTOMER set MOBILE='" + tbxCusMobile.Text.Replace("'", "") + "' where id in (select CUSTOMERID from rvs_vehicle where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "')";
            dm.ExecuteNonQuery(qry);
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);
            string vldt = obj.CheckDuplicate(Convert.ToInt32(hdnVehicleId.Value));
            //string WorkOrdNo = ddlWorkOrderNo.SelectedItem.Text;
            string WorkOrdNo = string.Empty;

            if (ddlWorkOrderNo.SelectedItem != null && ddlWorkOrderNo.SelectedItem.Text != "Select" && !string.IsNullOrEmpty(ddlWorkOrderNo.SelectedItem.Text))
            {
                WorkOrdNo = ddlWorkOrderNo.SelectedItem.Text;
            }
            //int SEVTYPE = Convert.ToInt32(ddlcategoryid.SelectedValue);
            if (vldt == "0")
            {
                int Pid = obj.Insert(Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text.Replace("'", ""), Millage, tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue), tbxEftDate.Text, tbxDateLine.Text, txtCAS.Text);
                if (Pid > 0)
                {
                    InsertPIDetails(Pid);
                    if (!string.IsNullOrEmpty(WorkOrdNo) && ddlWorkOrderNo.SelectedItem.Text != "Select" && Pid > 0)
                    {
                        UpdateWorkOrderStatus(WorkOrdNo, Pid);
                    }
                }
                //  SendSms();
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

    private void UpdateWorkOrderStatus(string WorkOrdNo, int Pid)
    {
        qry = @"update rvs_workordermaster set WORKORDERSTATUS=0, PREINSPECTIONID= '"+ Pid + "'  where WORKORDERNO='" + WorkOrdNo + "'";
        dm.ExecuteNonQuery(qry);
    }

    DbManagerSms ds = new DbManagerSms();

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
            //obj.InsertPIDetailsUpdate(Pid, Convert.ToInt32(r["ProblemCode"].ToString()), 2, r["ProblemRemarks"].ToString(), r["Qty"].ToString(), r["LaborRate"].ToString(), r["LaborQty"].ToString(), r["WorkingHour"].ToString());
           
            //obj.InsertPIDetailsUpdate(
            //    Pid,
            //    Convert.ToInt32(r["ProblemCode"]),
            //    2,
            //    r["ProblemRemarks"].ToString(),
            //    Convert.ToInt32(r["Qty"]),
            //    Convert.ToInt32(r["LaborRate"]),
            //    Convert.ToInt32(r["LaborQty"]),
            //    Convert.ToInt32(r["WorkingHour"].ToString()) 
            //);
            obj.InsertPIDetailsUpdateNew(
               Pid,
               Convert.ToInt32(r["ProblemCode"]),
               2,
               r["ProblemRemarks"].ToString(),
               Convert.ToInt32(r["Qty"]),
               Convert.ToInt32(r["LaborRate"]),
               Convert.ToInt32(r["LaborQty"]),
               float.Parse(r["WorkingHour"].ToString()) // Convert to int before passing
           );

        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);

            qry = @"update rvs_vehicle set DRIVERNAME='" + tbxDriverName.Text + "',DRIVERMOBILE='" + tbxDriverMobile.Text + "',DRIVERID='" + tbxStaffId.Text + "' where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "'";
            dm.ExecuteNonQuery(qry);

            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text, Millage, tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue), tbxEftDate.Text, tbxDateLine.Text);
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
        LoadProblems(0);
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void btnPreInsSearch_Click(object sender, EventArgs e)
    {
        qry = @"SELECT PI.*,to_char(PI.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Entry Date"", SC.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS,V.CNGCYLINDERREDDATE ""CNG Cylinder Re-Test Date"" FROM RVS_PREINSPECTION PI
        INNER JOIN RVS_SERVICETYPE SC ON SC.ID = PI.SERVICETYPEID
        INNER JOIN RVS_VEHICLE V ON V.ID = PI.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID = V.CUSTOMERID WHERE PI.ISOPEN = 1 and VEHICLEID='" +
              tbxVehileForJobIns.Text.Split('-')[0].Trim() + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
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
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        ViewState["JI"] = ID;
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
            tbxremark.Text = dt.Rows[0]["REMARKS"].ToString();
            ddlgroup.Text = dt.Rows[0]["GROUPID"].ToString();
            tbxEftDate.Text = Convert.ToDateTime(dt.Rows[0]["etdate"].ToString()).ToString("dd-MM-yyyy");
            //tbxEftDate.Text = dt.Rows[0]["etdate"].ToString();

            tbxCCRD.Text = dt.Rows[0]["CNGCYLINDERREDDATE"].ToString();
            //  tbxDateLine.Text= Convert.ToDateTime(dt.Rows[0]["DLdate"].ToString()).ToString("dd-MM-yyyy");

            if (dt.Rows[0]["DLdate"].ToString() == "" || dt.Rows[0]["DLdate"].ToString() == null)
            {
                tbxDateLine.Text = "";
            }
            else
            {
                tbxDateLine.Text = Convert.ToDateTime(dt.Rows[0]["DLdate"].ToString()).ToString("dd-MM-yyyy");
            }

            tbxStaffId.Text = dt.Rows[0]["DRIVERID"].ToString();
            LoadProblems(ID);
            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }

    private void LoadProblems(int id)
    {
        DtDetail.Clear();
        string qry = @"SELECT SC.ID CATEGORYCODE,SC.CATNAME CATEGORYNAME,PI.ID PROBLEMCODE,PI.ITEMNAME PROBLEMNAME,PD.REMARKS PROBLEMREMARKS,
PD.QUANTITY Qty,PD.LABORRATE,PD.LABORQTY,PD.WORKINGHOUR, (PD.QUANTITY * PD.LABORRATE * PD.LABORQTY * PD.WORKINGHOUR )  LABORCHARGE
FROM RVS_PIDETAILS PD
INNER JOIN RVS_PREINSPECTION PR ON PD.PIID=PR.ID
INNER JOIN RVS_INSPECTIONITEMS PI ON PD.INSPECTIONITEMID=PI.ID
INNER JOIN RVS_SERVICECATEGORY SC ON PI.SERVICECATID=SC.ID
WHERE PR.ID='" + id + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        foreach (DataRow r in dt.Rows)
        {
            DtDetail.Rows.Add(r[0], r[1], r[2], r[3], r[4], r[5],r[6], r[7], r[8] , r[9]);
        }
        BindRpt();
    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_PREINSPECTION", ID);
            MessageBox(DeleteSuccess);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindRpt();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail + ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
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
                ////  Response.Redirect("JobCardNew.aspx?JobID=" + JobID.ToString());

                MessageBox(SaveSuccess);
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);


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
    //===========================  Work Order Data ======================

    protected void ddlWorkOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string TRNO = ddlWorkOrderNo.SelectedItem.Text;
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
        WHERE WOM.ISACTIVE = 1 AND WOM.WORKORDERSTATUS = 1 AND WOD.ISACTIVE = 1 AND WOM.WORKORDERNO = '" + TRNO + @"'
        ORDER BY WOM.WORKORDERNO ";

        DataTable dtd = dm.ExecuteQuery(qry);
        tbxVehiid.Text = dtd.Rows[0]["ID"].ToString() + " - " + dtd.Rows[0]["REGNO"].ToString();
        tbxWorkOrderDate.Text = dtd.Rows[0]["WORKORDERDATE"].ToString();
        tbxStafId.Text = dtd.Rows[0]["DRIVERSTAFFID"].ToString();
        tbxRunningMillage.Text = dtd.Rows[0]["RUNNINGMILLAGE"].ToString();

        DtDetailItem.Clear();
        foreach (DataRow r in dtd.Rows)
        {
            DtDetailItem.Rows.Add(
                r["ITEMNAME"].ToString(),
                r["REASON"].ToString());
        }
        BindWorkOrderRpt();

        //string idString = tbxVehiid.Text;
        int id = Convert.ToInt32(dtd.Rows[0]["VID"].ToString());
        VehicleId(id);
    }
    //================================  End Work Order Data ===============
    protected void BindData()
    {
       
        qry = @"select * from 
(SELECT PI.*,to_char(PI.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Entry Date"", SC.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS FROM RVS_PREINSPECTION PI
        INNER JOIN RVS_SERVICETYPE SC ON SC.ID= PI.SERVICETYPEID
        INNER JOIN RVS_VEHICLE V ON V.ID=PI.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID WHERE PI.ISOPEN=1 and PI.SERVICETYPEID='" + ddlcategoryid.SelectedValue + "' and PI.WORKSHOPID='" + ddlworkshopid.SelectedValue + @"'
        and pi.createdby = '" + SessionManager.SessionName.UserId + @"' order by pi.CREATEDATE desc
        ) where rownum< 2";
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
    }

    protected void btnPrintPJob_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        if (!string.IsNullOrEmpty(ID))
        {
            string url = @"PreInspectionPrint.aspx?PreInspectionID=" + ID.Trim();
            Response.Redirect(url); // Test with direct redirection
        }
    }


    //protected void tbxBarcode_TextChanged_TextChanged(object sender, EventArgs e)
    //{
    //    LoadDetails();
    //    OrderTaken();
    //    GridView2.DataSource = GetSingleData();
    //    GridView2.DataBind();
    //    GridView1.DataSource = GetData();
    //    GridView1.DataBind();
    //    SearchStaffTextBox.Focus();
    //}

}
