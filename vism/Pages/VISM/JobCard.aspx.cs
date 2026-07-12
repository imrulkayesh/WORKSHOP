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

public partial class Pages_VISM_JobCard : BasePage
{
    dalJobCard obj = new dalJobCard();
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
            else if (!dalCommon.IsPermitted("/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath)))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            try
            {
                JOBID = (int)0;

                LoadDDL();
                tbxEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (!string.IsNullOrEmpty(Request.QueryString["JobID"]))
                {
                    JOBID = Convert.ToInt32(Request.QueryString["JobID"]);
                    LoadData();
                }
                LoadTechnician();
                LoadCharges();
                LoadParts();
                //BindData();
                BindRptOnLOad();
            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
        }

    }

    int JOBID
    {
        set { ViewState["JOBID"] = value; }
        get
        {
            try
            {
                return (int)ViewState["JOBID"];
            }
            catch
            {
                return 0;
            }
        }
    }
    private void LoadDDL()
    {
        ddlServiceType.DataSource = new Common().GetAll("RVS_SERVICETYPE");
        ddlServiceType.DataBind();
        ddlworkshopid.DataSource = new dalWorkShop().GetAll(SessionManager.SessionName.BusinessCentetid);
        ddlworkshopid.DataBind();
        //ddlgroup.DataSource = new Common().GetAll("RVS_GROUP");
        //ddlgroup.DataBind();

      //  ListItem li = new ListItem("Select", "");
        string qry = @"Select ID,GROUPNAME text FROM RVS_GROUP";
        ddlgroup.DataSource = dm.ExecuteQuery(qry);
        ddlgroup.DataBind();
      //  ddlgroup.Items.Insert(0, li);

    }
    private void LoadTechnician()
    {

        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
        {
            rptTechs.DataSource = obj.GetTechnician(Convert.ToInt32(ddlworkshopid.SelectedValue), 0);
            rptTechs.DataBind();
        }
        if (JOBID > 0)
        {
            DataTable dt = obj.GetTechnician(Convert.ToInt32(ddlworkshopid.SelectedValue), JOBID);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem r in rptTechs.Items)
                {
                    HiddenField StaffId = (HiddenField)r.FindControl("hdnStaffID");
                    if (StaffId.Value == dr["STAFFID"].ToString())
                    {
                        CheckBox chk = (CheckBox)r.FindControl("chkrow");
                        TextBox remarks = (TextBox)r.FindControl("tbxTecRemarks");
                        remarks.Text = dr["REMARKS"].ToString();
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    private void LoadCharges()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
        {
            rptCharges.DataSource = obj.GetCharges(10, 0);
            rptCharges.DataBind();
        }
        if (JOBID > 0)
        {
            DataTable dt = obj.GetCharges(Convert.ToInt32(ddlworkshopid.SelectedValue), JOBID);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem r in rptCharges.Items)
                {
                    HiddenField ChargeId = (HiddenField)r.FindControl("hdnChargeId");
                    if (ChargeId.Value == dr["ID"].ToString())
                    {
                        CheckBox chk = (CheckBox)r.FindControl("chkrow");
                        TextBox qty = (TextBox)r.FindControl("tbxChargeQty");
                        TextBox tbxChargeAmount = (TextBox)r.FindControl("tbxChargeAmount");
                        TextBox tbxChargeRemarks = (TextBox)r.FindControl("tbxChargeRemarks");
                        qty.Text = dr["QUANTITY"].ToString();
                        tbxChargeAmount.Text = dr["AMOUNT"].ToString();
                        tbxChargeRemarks.Text = dr["REMARKS"].ToString();
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    private void LoadParts()
    {
        if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
        {
            rptParts.DataSource = obj.GetParts(10, 0);
            rptParts.DataBind();
        }
        if (JOBID > 0)
        {
            DataTable dt = obj.GetParts(10, JOBID);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem r in rptParts.Items)
                {
                    HiddenField hdnPartsId = (HiddenField)r.FindControl("hdnPartsId");
                    if (hdnPartsId.Value == dr["ID"].ToString())
                    {
                        CheckBox chk = (CheckBox)r.FindControl("chkrow");
                        TextBox tbxPartsQty = (TextBox)r.FindControl("tbxPartsQty");
                        TextBox tbxPartsAmount = (TextBox)r.FindControl("tbxPartsAmount");
                        TextBox tbxPartsRemarks = (TextBox)r.FindControl("tbxPartsRemarks");
                        tbxPartsQty.Text = dr["QUANTITY"].ToString();
                        tbxPartsAmount.Text = dr["AMOUNT"].ToString();
                        tbxPartsRemarks.Text = dr["REMARKS"].ToString();
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (JOBID > 0)
            {
                decimal totalAmount = 0;
                obj.Update(JOBID, tbxEffectiveDate.Text, 0, Convert.ToInt32(ddlServiceType.SelectedValue), tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue));
                InsertTechs(JOBID);
                totalAmount = InsertCharge(JOBID);
                totalAmount += InsertParts(JOBID);
                obj.InsertBill(JOBID, totalAmount);
                MessageBox(SaveSuccess);
                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
                //BindData();
                ClearAll();
            }
            else
            {

                MessageController.Show("Please select a jobcard", MessageType.Error, Page);
            }
        }
        catch (Exception ex)
        {
            MessageBox(SaveFail+ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    private void InsertTechs(int JobID)
    {
        obj.DeleteTechs(JobID);
        foreach (RepeaterItem r in rptTechs.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField StaffId = (HiddenField)r.FindControl("hdnStaffID");
                TextBox remarks = (TextBox)r.FindControl("tbxTecRemarks");
                obj.InsertTechs(JobID, StaffId.Value, remarks.Text);
            }
        }
    }
    private decimal InsertCharge(int JobID)
    {
        obj.DeleteCharges(JobID);
        decimal amt = 0;
        foreach (RepeaterItem r in rptCharges.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField ChargeId = (HiddenField)r.FindControl("hdnChargeId");
                TextBox qty = (TextBox)r.FindControl("tbxChargeQty");
                TextBox tbxChargeAmount = (TextBox)r.FindControl("tbxChargeAmount");
                TextBox tbxChargeRemarks = (TextBox)r.FindControl("tbxChargeRemarks");
                decimal qtys = Convert.ToDecimal(qty.Text);
                decimal amts = Convert.ToDecimal(tbxChargeAmount.Text);
                obj.InsertCharges(JobID, Convert.ToInt32(ChargeId.Value), qtys, amts, tbxChargeRemarks.Text);
                amt += amts * qtys;
            }
        }
        return amt;
    }
    private decimal InsertParts(int JobID)
    {
        obj.DeleteParts(JobID);
        decimal amt = 0;
        foreach (RepeaterItem r in rptParts.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkrow");
            if (chk.Checked)
            {
                HiddenField hdnPartsId = (HiddenField)r.FindControl("hdnPartsId");
                TextBox tbxPartsQty = (TextBox)r.FindControl("tbxPartsQty");
                TextBox tbxPartsAmount = (TextBox)r.FindControl("tbxPartsAmount");
                TextBox tbxPartsRemarks = (TextBox)r.FindControl("tbxPartsRemarks");
                decimal qtys = Convert.ToDecimal(tbxPartsQty.Text);
                decimal amts = Convert.ToDecimal(tbxPartsAmount.Text);
                obj.InsertParts(JobID, Convert.ToInt32(hdnPartsId.Value), qtys, amts, tbxPartsRemarks.Text);
                amt += amts * qtys;
            }
        }
        return amt;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
      //  tbxremark.Text = "";
        JOBID = (int)0;
        LoadTechnician();
        LoadCharges();
        LoadParts();
    }
    //protected void BindData()
    //{
    //    if (!string.IsNullOrEmpty(ddlworkshopid.SelectedValue))
    //    {
    //        DataTable dt = obj.GetAll(Convert.ToInt32(ddlworkshopid.SelectedValue));
    //        rpt.DataSource = dt;
    //    }
    //    else
    //    {
    //        rpt.DataSource = null;
    //    }
    //    rpt.DataBind();
    //}


    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        JOBID = ID;
        LoadData();
    }
    private void LoadData()
    {
        DataTable dt = obj.GetById(JOBID);
        if (dt.Rows.Count > 0)
        {
            tbxVehicle.Text = dt.Rows[0]["Veh"].ToString();
            ddlServiceType.SelectedValue = dt.Rows[0]["SERVICETYPEID"].ToString();
            ddlworkshopid.SelectedValue = dt.Rows[0]["WORKSHOPID"].ToString();
            ddlgroup.SelectedValue = dt.Rows[0]["GROUPID"].ToString();
            tbxPIID.Text = dt.Rows[0]["PIID"].ToString();
            tbxremark.Text = dt.Rows[0]["REMARKS"].ToString();
            tbxEffectiveDate.Text = Convert.ToDateTime(dt.Rows[0]["EFFECTIVEDATE"]).ToString("dd/MM/yyyy");
            LoadTechnician();
            LoadCharges();
            LoadParts();
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_JOBCARD", ID);
            MessageBox(DeleteSuccess);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            //BindData();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail+ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

    //protected void ddlcategoryid_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindData();
    //}

    protected void ddlworkshopid_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCharges();
        LoadParts();
        LoadTechnician();
    }

    protected void btnPrint_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"JobPrint.aspx?JobID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnBillPrint_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"PrintBill.aspx?JobID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnClose_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        obj.CloseJobCard(ID);
        SendSms(ID);
        MessageBox("Close Successfull");
        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        BindRpt();

    }
    DbManagerSms ds = new DbManagerSms();
    private void SendSms(int jid)
    {
        try
        {
            string qry = @"select VEHICLEID from RVS_JOBCARD where id='"+jid+"'";
            string vid = dm.ExecuteScalar(qry);
             qry = @"SELECT VH.REGNO,CS.CUSTOMERNAME,CS.MOBILE,VH.DRIVERNAME,VH.DRIVERMOBILE,WS.WORKSHOP FROM RVS_PREINSPECTION PR
INNER JOIN RVS_WORKSHOP WS ON PR.WORKSHOPID=WS.ID
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN RVS_CUSTOMER CS ON VH.CUSTOMERID=CS.ID
WHERE VH.ID='" + vid + "' AND PR.ISOPEN=0";
            DataTable dt = dm.ExecuteQuery(qry);
            if (dt.Rows.Count > 0)
            {
                string sbd = "Dear Sir,\nYour vehicle " + dt.Rows[0]["REGNO"] + " has just left from " + dt.Rows[0]["WORKSHOP"] +
                             " after servicing.\nDriver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " +
                             dt.Rows[0]["DRIVERMOBILE"];
                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
VALUES ('VISM', TO_DATE(SYSDATE), '" + dt.Rows[0]["MOBILE"] + "', '" + sbd + "', 0, '201262', '201262', NULL)";
                ds.ExecuteNonQuery(qry);
                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
VALUES ('VISM', TO_DATE(SYSDATE), '" + dt.Rows[0]["DRIVERMOBILE"] + "', '" + sbd + "', 0, '201262', '201262', NULL)";
                ds.ExecuteNonQuery(qry);
                //need comment bellow
//                qry = @"INSERT INTO RFL.T_DSMS(DSMS_DONO, DSMS_DODT, DSMS_SBJT, DSMS_BODY, DSMS_ACTV, IUSER, EUSER, ACTION_TAKEN)
//VALUES ('VISM', TO_DATE(SYSDATE), '01992665872', '" + sbd + "', 0, '201262', '201262', NULL)";
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
                qry = @"DELETE RFL.T_DSMS WHERE DSMS_DONO = 'VISM' AND IUSER= '201262' AND DSMS_ACTV = 1";
                ds.ExecuteNonQuery(qry);
            }
             qry = @"select piid from RVS_JOBCARD where id='" + jid + "'";
            string pid = dm.ExecuteScalar(qry);
            qry = @"SELECT SU.MOBILE FROM RVS_PREINSPECTION PR
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID 
INNER JOIN BS_MOTHERCOMPANY MC ON CO.MOTHERCOMPANYID=MC.ID
INNER JOIN RVS_SMSUSER SU ON SU.MCOMPANYID=CO.MOTHERCOMPANYID
WHERE VH.ID='" + vid + "' AND PR.ISOPEN=0 AND SU.ISACTIVE=1 and pr.id='"+ pid + "'";
            DataTable dtt = dm.ExecuteQuery(qry);
            foreach (DataRow r in dtt.Rows)
            {
                string sbd = "Dear Sir,\nThe vehicle " + dt.Rows[0]["REGNO"] + " has just left from " + dt.Rows[0]["WORKSHOP"] +
                             " after servicing.\nDriver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " +
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
    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"JobCardView.aspx?JobID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnPreInsSearch_Click(object sender, EventArgs e)
    {
        
        BindRpt();
    }
    private void BindRpt()
    {
        qry = @"SELECT j.Id, j.id ""Job Id"",to_char(pi.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Received date"", v.REGNO ""Reg No."",v.CHASSISNO ""Chassis No."",
mo.MODELNAME ""Model"",st.SERVICETYPE ""Service Type"",c.CUSTOMERNAME ""Customer"",co.COMPANYNAME ""Company"", v.DRIVERNAME ""Driver Name"",v.DRIVERMOBILE ""Driver Mobile"", j.REMARKS ""Remarks"" from RVS_JOBCARD J
        LEFT JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        LEFT JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT join BS_company co on v.COMPANYID=co.id
        LEFT join rvs_model mo on v.MODELID=mo.ID
        LEFT join rvs_preinspection pi on j.PIID=pi.id
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        WHERE J.ISOPEN=1 and pi.ISACTIVE=1 and j.ISACTIVE=1 and j.vehicleid='" + tbxVehileForJobIns.Text.Split('-')[0].Trim() + "'";
        DtCommon = dm.ExecuteQuery(qry);
        rptMain.DataSource = DtCommon;
        rptMain.DataBind();
    }
    private void BindRptOnLOad()
    {
        qry = @"SELECT j.Id, j.id ""Job Id"",to_char(pi.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Received date"", v.REGNO ""Reg No."",v.CHASSISNO ""Chassis No."",
mo.MODELNAME ""Model"",st.SERVICETYPE ""Service Type"",c.CUSTOMERNAME ""Customer"",co.COMPANYNAME ""Company"", v.DRIVERNAME ""Driver Name"",v.DRIVERMOBILE ""Driver Mobile"", j.REMARKS ""Remarks"" from RVS_JOBCARD J
        LEFT JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        LEFT JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT join BS_company co on v.COMPANYID=co.id
        LEFT join rvs_model mo on v.MODELID=mo.ID
        LEFT join rvs_preinspection pi on j.PIID=pi.id
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        WHERE J.ISOPEN=1 and pi.ISACTIVE=1 and j.ISACTIVE=1 and j.id='" + JOBID + "'";
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
            headerRpt.DataSource = GetHeader(DtCommon, true);
            headerRpt.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            if (bodyRpt == null) return;
            bodyRpt.DataSource = DtToList(rn, DtCommon, true);
            bodyRpt.DataBind();
            rn++;
            if (SessionManager.SessionName.RoleId == 31 || SessionManager.SessionName.RoleId == 38 || SessionManager.SessionName.UserId == "201262")
            {
                var lnkd = (ImageButton)e.Item.FindControl("btnNoWork");
                lnkd.Visible = true;
            }

            //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
        }

    }
    //Reapeter bind end
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchVehicle(string prefixText, int count)
    {
        string BI = SessionManager.SessionName.BusinessCentetid.ToString();


        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT distinct (V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) VEHICLE FROM RVS_VEHICLE V
                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                INNER JOIN RVS_JOBCARD J ON J.VEHICLEID=V.ID
                INNER JOIN RVS_WORKSHOP W ON J.WORKSHOPID=W.ID
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%')) and W.BUSINESSCENTERID='" + BI + @"' AND ROWNUM <= 20 AND V.ISACTIVE='1' AND J.ISOPEN='1'";
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

    protected void btnNoWork_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            obj.CloseJobCard(ID);
            qry = @"update rvs_jobcard set isactive=0 where id =" + ID;
            dm.ExecuteNonQuery(qry);
            qry = @"update RVS_PREINSPECTION set isactive=0 where id in (select PIID from rvs_jobcard where id=" + ID + ")";
            dm.ExecuteNonQuery(qry);
            MessageBox(DeleteSuccess);
            BindRpt();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail+ex.Message);
        }
        
    }

   
}