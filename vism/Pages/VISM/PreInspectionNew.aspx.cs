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

public partial class Pages_VISM_PreInspectionNew : BasePage
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
        //ddlcategoryid.Items.Insert(0, li);
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();

        //ddlworkshopid.Items.Insert(0, li);
        //ddlCompany.DataSource = obj.GetDdlViewComp();
        //ddlCompany.DataTextField = "COMP_NAME";
        //ddlCompany.DataValueField = "COMP_ID";
        //ddlCompany.DataBind();
        //ddlCompany.Items.Insert(0, new ListItem("-------------Please Select------------", "0"));

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
        DtDetail.AcceptChanges();
        rptDetails.DataSource = DtDetail;
        rptDetails.DataBind();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {

        //DataTable dt = new DataTable();
        //string qry = @"SELECT FILLTYPE FROM YA.RVS_INSPECTIONITEMS WHERE ID='" + ddlProblemName.SelectedValue + "'";
        //dt = dm.ExecuteQuery(qry);

        //string FillType = dt.Rows[0]["FILLTYPE"].ToString();

        //if (FillType == "1")
        //{
        //    MessageBox("Please input Item code On Remarks Field");
        //    ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxRate').focus();", true);
        //}
        //else
        //{
        foreach (DataRow row in DtDetail.Rows)
        {
            if (ddlProblemName.SelectedValue != row["ProblemCode"].ToString()) continue;
            MessageBox(ddlProblemName.SelectedItem.Text + " is already added.");
            return;
        }
        //string totAmt = (Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQty.Text)).ToString();
        //tbxAmt.Text = totAmt;
        DtDetail.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlProblemName.SelectedValue, ddlProblemName.SelectedItem.Text, tbxProbRemarks.Text);
        BindRpt();
        ddlCategory_OnSelectedIndexChanged(null, null);
        tbxProbRemarks.Text = "";
        //}

    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        //        try
        //        {
        //            if (DtDetail.Rows.Count == 0)
        //            {
        //                MessageBox("Please add item first.");
        //                return;
        //            }
        //            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
        //                       WHERE ID='TO' RETURNING LASTNO INTO :RETVAL";
        //            string lno = dm.ExecuteScalar(qry, "RETVAL");
        //            db.BeginTransaction();
        //            const string trType = "TO";
        //            string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
        //            qry = @"INSERT INTO RVS_TRMASTER(ID,TRTYPE,REFTRID,SENDLOCATION,SENDWH,SENDCOMPANY,SENDDATE,SENDREMARKS,SENDUSER,
        //RECVLOCATION,RECVWH,RECVCOMPANY,IUSER) VALUES(
        //'" + trid + "','" + trType + "','" + ddlDrId.SelectedValue + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"','" + DateChange(DateTime.Today.ToString("dd/MM/yyyy")) + @"','" + tbxSendRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + @"',
        //'" + ddlRecvLoc.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + ddlRecvCom.SelectedValue + "','" + SessionManager.SessionName.UserId + "')";
        //            db.ExecuteNonQuery(qry);
        //            int i = 1;
        //            foreach (DataRow r in DtDetail.Rows)
        //            {
        //                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
        //                qry = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,ITEMCATEGORYNAME,
        //ITEMCODE,ITEMNAME,UNITID,UNITNAME,QTY,RATE,TOTALAMT,LINE,IUSER) values
        //('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["CategoryName"] + "','" + r["ItemCode"] + @"',
        //'" + r["ItemName"] + "','" + r["UnitCode"] + "','" + r["UnitName"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
        //'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "')";
        //                db.ExecuteNonQuery(qry);
        //                i++;
        //            }
        //            qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlDrId.SelectedValue + "'";
        //            db.ExecuteNonQuery(qry);
        //            db.Commit();
        //            btnSave.Enabled = false;
        //            MessageBox(SaveSuccess);
        //        }
        //        catch (Exception ex)
        //        {
        //            db.Rollback();
        //            MessageBox(SaveFail + ex.Message);
        //        }

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



    //protected void ddlRecvLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ListItem li = new ListItem("Select", "");
    //    string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
    //                 ddlRecvLoc.SelectedValue + "' order by VISMWHNAME";
    //    ddlRecvWh.DataSource = dm.ExecuteQuery(qry);
    //    ddlRecvWh.DataBind();
    //    ddlRecvWh.Items.Insert(0, li);
    //    //tbxRecvCom.Text = "";
    //    ddlRecvWh.Enabled = true;
    //}

    //    protected void ddlRecvWh_OnSelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
    //inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
    //where wh.ID='" + ddlRecvWh.SelectedValue + "'";
    //        DataTable val = dm.ExecuteQuery(qry);
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID");
    //        dt.Columns.Add("TEXT");
    //        dt.Rows.Add(val.Rows[0]["ID"].ToString(), val.Rows[0]["TEXT"]);
    //        ddlRecvCom.DataSource = dt;
    //        ddlRecvCom.DataBind();

    //        ListItem li = new ListItem("Select", "");
    //        qry =
    //           @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
    //        ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
    //        ddlSendLoc.DataBind();
    //        ddlSendLoc.Items.Insert(0, li);

    //        ddlSendLoc.Enabled = true;
    //    }

    //protected void ddlSendLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ListItem li = new ListItem("Select", "");
    //    string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
    //                 ddlSendLoc.SelectedValue + "' order by VISMWHNAME";
    //    ddlSendWh.DataSource = dm.ExecuteQuery(qry);
    //    ddlSendWh.DataBind();
    //    ddlSendWh.Items.Insert(0, li);
    //    ddlSendCom.Items.Clear();
    //    ddlSendWh.Enabled = true;

    //    if (ddlRecvLoc.SelectedValue == ddlSendLoc.SelectedValue)
    //    {
    //        ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("disabled", "disabled");
    //        ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");

    //    }

    //}

    //    protected void ddlSendWh_OnSelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
    //inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
    //where wh.ID='" + ddlSendWh.SelectedValue + "'";
    //        DataTable val = dm.ExecuteQuery(qry);
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID");
    //        dt.Columns.Add("TEXT");
    //        dt.Rows.Add(val.Rows[0]["ID"].ToString(), val.Rows[0]["TEXT"]);
    //        ddlSendCom.DataSource = dt;
    //        ddlSendCom.DataBind();


    //        ListItem li = new ListItem("Select", "");
    //        qry = @"SELECT RQ.ID,RQ.ID||' - '||TO_CHAR(RQ.IDATE,'DD/MM/YYYY') TEXT FROM RVS_REQMASTER RQ
    //WHERE RQ.ISACTIVE=1 AND RQ.ISCOMPLETE=0 AND RQ.REQTYPE='DR'
    // AND RQ.SENDLOCATION='" + ddlSendLoc.SelectedValue + @"' 
    // AND RQ.SENDWH='" + ddlSendWh.SelectedValue + @"' 
    // AND RQ.SENDCOMPANY='" + ddlSendCom.SelectedValue + @"'
    // ORDER BY RQ.IDATE";
    //        ddlDrId.DataSource = dm.ExecuteQuery(qry);
    //        ddlDrId.DataBind();
    //        ddlDrId.Items.Insert(0, li);
    //    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,ITEMNAME text from RVS_INSPECTIONITEMS where SERVICECATID='" + ddlCategory.SelectedValue + "' order by ITEMNAME";
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

    //protected void ddlProblemName_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ListItem li = new ListItem("Select", "");
    //    string qry = @"SELECT FILLTYPE FROM YA.RVS_INSPECTIONITEMS WHERE ID='" + ddlProblemName .SelectedValue+ "'";

    //}

    //    protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME,IM.RATE FROM RVS_ITEMMASTER IM
    //inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE where im.isactive=1 and IM.ITEMCODE='" + ddlItemName.SelectedValue + "'";
    //        DataTable dt = dm.ExecuteQuery(qry);
    //        tbxUnitCode.Text = dt.Rows[0]["SCMUNITCODE"].ToString();
    //        hdnUnitName.Value = dt.Rows[0]["UNITNAME"].ToString();
    //        tbxRate.Text = dt.Rows[0]["RATE"].ToString();
    //        tbxQty.Text = "";
    //        tbxAmt.Text = "";
    //        tbxQty.Enabled = true;
    //    }

    //    protected void ddlDrId_OnSelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        string qry = @"SELECT RM.RECVLOCATION,lo.CENTERNAME, RM.RECVWH,wh.VISMWHNAME, RM.RECVCOMPANY,ws.WORKSHOP,RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,
    //RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,RD.RATE,RD.QTY,RD.TOTALAMT,RD.REMARKS FROM RVS_REQDETAIL RD
    //INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
    //inner join BS_BUSINESSCENTER lo on RM.RECVLOCATION=lo.id
    //inner join RVS_VISMWH wh on RM.RECVWH=wh.id
    //inner join rvs_workshop ws on RM.RECVCOMPANY=ws.id
    //WHERE RD.REQMASTERID='" + ddlDrId.SelectedValue + @"'
    //ORDER BY RD.LINE";
    //        DataTable dtd = dm.ExecuteQuery(qry);
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID");
    //        dt.Columns.Add("TEXT");
    //        dt.Rows.Add(dtd.Rows[0]["RECVLOCATION"].ToString(), dtd.Rows[0]["CENTERNAME"].ToString());
    //        ddlRecvLoc.DataSource = dt;
    //        ddlRecvLoc.DataBind();
    //        dt.Clear();
    //        dt.Rows.Add(dtd.Rows[0]["RECVWH"].ToString(), dtd.Rows[0]["VISMWHNAME"].ToString());
    //        ddlRecvWh.DataSource = dt;
    //        ddlRecvWh.DataBind();
    //        dt.Clear();
    //        dt.Rows.Add(dtd.Rows[0]["RECVCOMPANY"].ToString(), dtd.Rows[0]["WORKSHOP"].ToString());
    //        ddlRecvCom.DataSource = dt;
    //        ddlRecvCom.DataBind();

    //        DtDetail.Clear();
    //        foreach (DataRow r in dtd.Rows)
    //        {
    //            DtDetail.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
    //                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
    //                r["UNITID"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["QTY"].ToString(),
    //                r["TOTALAMT"].ToString());
    //        }
    //        BindRpt();
    //        ListItem li = new ListItem("Select", "");
    //        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
    //        ddlCategory.DataSource = dm.ExecuteQuery(qry);
    //        ddlCategory.DataBind();
    //        ddlCategory.Items.Insert(0, li);

    //    }

    protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            DataRow rows = DtDetail.Select("ProblemCode = '" + e.CommandArgument + "'").FirstOrDefault();
            ddlCategory.SelectedValue = rows["CategoryCode"].ToString();
            ddlCategory_OnSelectedIndexChanged(null, null);
            ddlProblemName.SelectedValue = e.CommandArgument.ToString();
            tbxProbRemarks.Text = rows["ProblemRemarks"].ToString();
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            ddlCategory.Enabled = false;
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

        int ProbCode = Convert.ToInt32(rows["ProblemCode"]);

        string pr = rows["ProblemRemarks"].ToString();

        try
        {
            string qry = @"update YA.RVS_PIDETAILS set REMARKS='" + pr + "' where INSPECTIONITEMID='" + ProbCode + "' and PIID='" + JI + @"'";
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
                tbxCCRD.Text = dt.Rows[0]["CNGCYLINDERREDDATE"].ToString();


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
        //try
        //{
        //    qry = @"update rvs_vehicle set DRIVERNAME='" + tbxDriverName.Text + "',DRIVERMOBILE='" + tbxDriverMobile.Text + "' where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "'";
        //    dm.ExecuteNonQuery(qry);
        //    dm.ExecuteNonQuery(qry);
        //    int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);
        //    string vldt = obj.CheckDuplicate(Convert.ToInt32(hdnVehicleId.Value));
        //    if (vldt == "0")
        //    {
        //        int Pid = obj.Insert(Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text, Millage, tbxremark.Text);
        //        if (Pid > 0)
        //        {
        //            InsertPIDetails(Pid);
        //        }
        //        MessageBox(SaveSuccess);
        //        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);

        //        BindData();
        //    }
        //    else
        //    {

        //        MessageController.Show(vldt, MessageType.Error, Page);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox(SaveFail + ex.Message);
        //    ErrorLog.WriteLog(ex);
        //    MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

        //}

        try
        {
            qry = @"update YA.rvs_vehicle set DRIVERNAME='" + tbxDriverName.Text + "',DRIVERMOBILE='" + tbxDriverMobile.Text.Replace("'", "") + "',DRIVERID='" + tbxStaffId.Text + "' where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "'";
            dm.ExecuteNonQuery(qry);
            qry = @"update YA.RVS_CUSTOMER set MOBILE='" + tbxCusMobile.Text.Replace("'", "") + "' where id in (select CUSTOMERID from YA.rvs_vehicle where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "')";
            dm.ExecuteNonQuery(qry);
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);
            string vldt = obj.CheckDuplicate(Convert.ToInt32(hdnVehicleId.Value));

            //int SEVTYPE = Convert.ToInt32(ddlcategoryid.SelectedValue);
            if (vldt == "0")
            {
                int Pid = obj.Insert(Convert.ToInt32(hdnVehicleId.Value), Convert.ToInt32(ddlworkshopid.SelectedValue), ddlTechnician.SelectedValue, Convert.ToInt32(ddlcategoryid.SelectedValue), tbxDriverName.Text, tbxDriverMobile.Text.Replace("'", ""), Millage, tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue), tbxEftDate.Text, tbxDateLine.Text, txtCAS.Text);
                if (Pid > 0)
                {
                    InsertPIDetails(Pid);
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
            obj.InsertPIDetails(Pid, Convert.ToInt32(r["ProblemCode"].ToString()), 2, r["ProblemRemarks"].ToString());
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToInt32(tbxMillage.Text);

            qry = @"update YA.rvs_vehicle set DRIVERNAME='" + tbxDriverName.Text + "',DRIVERMOBILE='" + tbxDriverMobile.Text + "',DRIVERID='" + tbxStaffId.Text + "' where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "'";
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
        qry = @"SELECT PI.*,to_char(PI.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Entry Date"", SC.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS,V.CNGCYLINDERREDDATE ""CNG Cylinder Re-Test Date"" FROM YA.RVS_PREINSPECTION PI
        INNER JOIN YA.RVS_SERVICETYPE SC ON SC.ID = PI.SERVICETYPEID
        INNER JOIN YA.RVS_VEHICLE V ON V.ID = PI.VEHICLEID
        INNER JOIN YA.RVS_CUSTOMER C ON C.ID = V.CUSTOMERID WHERE PI.ISOPEN = 1 and VEHICLEID='" +
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
        string qry = @"SELECT SC.ID CATEGORYCODE,SC.CATNAME CATEGORYNAME,PI.ID PROBLEMCODE,PI.ITEMNAME PROBLEMNAME,PD.REMARKS PROBLEMREMARKS FROM RVS_PIDETAILS PD
INNER JOIN RVS_PREINSPECTION PR ON PD.PIID=PR.ID
INNER JOIN RVS_INSPECTIONITEMS PI ON PD.INSPECTIONITEMID=PI.ID
INNER JOIN RVS_SERVICECATEGORY SC ON PI.SERVICECATID=SC.ID
WHERE PR.ID='" + id + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        foreach (DataRow r in dt.Rows)
        {
            DtDetail.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
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
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%')) AND ISACTIVE='1'  AND ROWNUM <= 20";
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
(SELECT PI.*,to_char(PI.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Entry Date"", SC.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE,C.ADDRESS FROM YA.RVS_PREINSPECTION PI
        INNER JOIN RVS_SERVICETYPE SC ON SC.ID= PI.SERVICETYPEID
        INNER JOIN RVS_VEHICLE V ON V.ID=PI.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID WHERE PI.ISOPEN=1 and PI.SERVICETYPEID='" + ddlcategoryid.SelectedValue + "' and PI.WORKSHOPID='" + ddlworkshopid.SelectedValue + @"'
        and pi.createdby = '" + SessionManager.SessionName.UserId + @"' order by pi.CREATEDATE desc
        ) where rownum< 2";
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
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