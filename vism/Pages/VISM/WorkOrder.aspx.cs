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

public partial class Pages_VISM_WorkOrder : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
            DtDetailAddColumn();
            BindRpt();
            LoadWorkOrderItemName();
            LoadWorkshop();
            tbxWorkOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

    }

    private void LoadWorkOrderItemName()
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"SELECT ID,ITEMNAME text from RVS_WORKORDERITEMS  WHERE ISACTIVE=1 ORDER BY ID";
        ddlItemName.DataSource = dm.ExecuteQuery(qry);
        ddlItemName.DataBind();
        ddlItemName.Items.Insert(0, li);
        foreach (DataRow r in DtDetailItem.Rows)
        {
            foreach (ListItem ltItem in ddlItemName.Items)
            {
                if (ltItem.Value == r["ItemCode"].ToString())
                {
                    ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("disabled", "disabled");
                    ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");
                }
            }

        }
        ddlItemName.Enabled = true;
    }

    protected void LoadWorkshop()
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select ws.id, ws.WORKSHOP as TEXT from rvs_workshop ws where ws.isactive = 1";
        DataTable workshopData = dm.ExecuteQuery(qry);

        ddlSendCom.DataSource = workshopData;
        ddlSendCom.DataTextField = "TEXT"; // Set the display text field
        ddlSendCom.DataValueField = "ID";  // Set the value field
        ddlSendCom.DataBind();
        ddlSendCom.Items.Insert(0, li);
    }


    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "0");
        ddlCustomer.DataSource = dm.ExecuteQuery("select id,Customername||' - '||mobile||' - '||Address text from RVS_CUSTOMER WHERE ISACTIVE=1");
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, li);

        

        //string BI = SessionManager.SessionName.BusinessCentetid.ToString();
        //ListItem li = new ListItem("Select", "");
        //string qry = "";
        //if (BI == "2")
        //{
        //    qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        //}
        //else
        //{
        //    qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
        //}

        //ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlSendLoc.DataBind();
        //ddlSendLoc.Items.Insert(0, li);

    }
    protected DataTable DtDetailItem
    {
        set
        {
            Session["DtDetailPart"] = value;
        }
        get
        {
            try
            {
                return (DataTable)Session["DtDetailPart"];
            }
            catch
            {
                return null;
            }
        }
    }
    private void DtDetailAddColumn()
    {
        DtDetailItem = new DataTable();
        DtDetailItem.Columns.Add("ItemId");
        DtDetailItem.Columns.Add("ItemName");
        DtDetailItem.Columns.Add("Reason");
    }
    private void BindRpt()
    {
        DtDetailItem.AcceptChanges();
        rpt.DataSource = DtDetailItem;
        rpt.DataBind();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        foreach (DataRow row in DtDetailItem.Rows)
        {
            if (ddlItemName.SelectedValue != row["ItemId"].ToString()) continue;
            MessageBox(ddlItemName.SelectedItem.Text + " is already added.");
            return;
        }
        DtDetailItem.Rows.Add(ddlItemName.SelectedValue, ddlItemName.SelectedItem.Text, tbxReason.Text);
        BindRpt();
        tbxReason.Text = "";
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
            if (DtDetailItem.Rows.Count == 0)
            {
                MessageBox("Please add item first.");
                return;
            }
            string cusId = ddlCustomer.SelectedValue;
            if (String.IsNullOrEmpty(cusId) || cusId == "0")
            {
                MessageBox("Please select Customer Name data");
                return;
            }
            string qry = @"SELECT LASTNO FROM RVS_LASTNO WHERE ID ='WO'";
            int lastId = Convert.ToInt32(dm.ExecuteScalar(qry));
            int incrementedId = lastId + 1;
            ////string lno = incrementedId.ToString().PadLeft(6, '0');
            ////string WORKORDERNO = "WO" + DateTime.Now.ToString("yyMMddHHmmss") + lno;
            string WORKORDERNO = incrementedId.ToString();

            db.BeginTransaction();

            qry = @"update YA.rvs_vehicle set DRIVERNAME='" + tbxDriverName.Text + "',DRIVERMOBILE='" + tbxDriverMobile.Text.Replace("'", "") + "',DRIVERID='" + tbxStaffId.Text + "',CUSTOMERID='" + cusId + "' where id='" + tbxVehicleid.Text.Split('-')[0].Trim() + "'";
            dm.ExecuteNonQuery(qry);

            if (chkEditCustomer.Checked)
            {
                
                qry = @"update rvs_customer set CUSTOMERNAME='" + tbxCustomer.Text + "', MOBILE='" + tbxMobile.Text + "',ADDRESS='" + tbxAddress.Text + "',EMAIL='" + tbxEmail.Text + "',NID='" + tbxNid.Text + "',CC1='" + tbxcc1.Text + "',CC2='" + tbxcc2.Text + "', UPDATEDATE = SYSDATE, UPDATEDBY='" + SessionManager.SessionName.UserId + "' where id='" + cusId + "'";
                dm.ExecuteNonQuery(qry);

            }

            //qry = @"INSERT INTO RVS_WORKORDERMASTER(ID,LOCATIONID,WAREHOUSEID,WORKSHOPID,WORKORDERNO,WORKORDERDATE,VEHICLEID,DRIVERSTAFFID,RUNNINGMILLAGE,CREATEDBY) 
            //VALUES( SQ_RVS_WORKORDERMASTER.NEXTVAL ,'" + ddlSendLoc.Text + "','" + ddlSendWh.Text + "','" + ddlSendCom.Text + "','" + WORKORDERNO + "',TO_DATE('" + tbxWorkOrderDate.Text + "','DD/MM/YYYY'),'" +  tbxVehicleid.Text + "','" + tbxDriverName.Text + "','"+ tbxMillage.Text + "',  '" + SessionManager.SessionName.UserId + "')";
            //db.ExecuteNonQuery(qry);
            qry = @"INSERT INTO RVS_WORKORDERMASTER(ID,WORKSHOPID,WORKORDERNO,WORKORDERDATE,VEHICLEID,VEHICLEIDNEW,DRIVERSTAFFID,RUNNINGMILLAGE,CREATEDBY) 
            VALUES( SQ_RVS_WORKORDERMASTER.NEXTVAL ,'" + ddlSendCom.Text + "','" + WORKORDERNO + "',TO_DATE('" + tbxWorkOrderDate.Text + "','DD/MM/YYYY'),'" + tbxVehicleid.Text + "','" + tbxVehicleid.Text + "','" + tbxDriverName.Text + "','" + tbxMillage.Text + "',  '" + SessionManager.SessionName.UserId + "')";
            db.ExecuteNonQuery(qry);

            //qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1 WHERE ID ='WO'";
            //qry = @"UPDATE RVS_LASTNO SET LASTNO='" + WORKORDERNO + "' WHERE ID ='WO'";
            //db.ExecuteNonQuery(qry);
            //Details data
            int i = 1;
            foreach (DataRow r in DtDetailItem.Rows)
            {
                qry = @"INSERT INTO RVS_WORKORDERDETAILS (ID, WORKORDERNO, WORKORDERITEMID, REASON, ISACTIVE) 
                VALUES (SQ_RVS_WORKORDERDETAILS.NEXTVAL , '" + WORKORDERNO + "', " + r["ItemId"] + ", '" + r["Reason"] + "'," + 1 + ")";
                db.ExecuteNonQuery(qry);
                i++;
            }
            db.Commit();
            btnSave.Enabled = false;
            tbxWorkOrderNo.Text = WORKORDERNO;
            tbxWorkOrderNo.BackColor = Color.DarkCyan;
            tbxWorkOrderNo.ForeColor = Color.White;
            tbxWorkOrderNo.Enabled = true;
            tbxWorkOrderNo.ReadOnly = true;
            tbxWorkOrderNo.Enabled = false;
            
            MessageBox(SaveSuccess);
        }
        catch (Exception ex)
        {
            db.Rollback();
            MessageBox(SaveFail + ex.Message);
        }

    }

    protected void btnDelete_OnClick(object sender, EventArgs e)
    {


        //try
        //{


        //    string qry = @"update RVS_REQMASTER set ISACTIVE='0', ISCOMPLETE='99',STATUSID='99',UPDATEDBY='" + SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID='" + ddlCdId.SelectedValue + "'";
        //    dm.ExecuteScalar(qry);

        //    btnDelete.Enabled = false;
        //    tbxCoId.BackColor = Color.DarkCyan;
        //    tbxCoId.ForeColor = Color.White;
        //    tbxCoId.Enabled = true;
        //    tbxCoId.ReadOnly = true;
        //    ddlCdId.Enabled = false;
        //    MessageBox(DeleteSuccess);

        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteLog(ex);
        //    MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        //}
    }



    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void imgbtnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        for (int i = 0; i < DtDetailItem.Rows.Count; i++)
        {
            if (DtDetailItem.Rows[i]["ItemId"].ToString() == e.CommandArgument.ToString())
            {
                DtDetailItem.Rows[i].Delete();
            }
        }
        BindRpt();
    }

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

    //}

    //protected void ddlSendWh_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
    //inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
    //where wh.ID='" + ddlSendWh.SelectedValue + "'";
    //    DataTable val = dm.ExecuteQuery(qry);
    //    ListItem li = new ListItem(val.Rows[0]["TEXT"].ToString(), val.Rows[0]["ID"].ToString());
    //    ddlSendCom.DataSource = null;
    //    ddlSendCom.DataBind();
    //    ddlSendCom.Items.Insert(0, li);


    //    li = new ListItem("Select", "");

    //}


    protected void ddlCdId_OnSelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            DataRow rows = DtDetailItem.Select("ItemCode = '" + e.CommandArgument + "'").FirstOrDefault();
            ddlItemName.SelectedValue = e.CommandArgument.ToString();
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            ddlItemName.Enabled = false;
            ViewState["ID"] = e.CommandArgument.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        DataRow rows = DtDetailItem.Select("ItemCode = '" + (string)ViewState["ID"] + "'").FirstOrDefault();
       

        BindRpt();
        ddlItemName.SelectedIndex = 0;
        ddlItemName.Enabled = true;
        btnUpdate.Visible = false;
        btnAdd.Visible = true;

        foreach (DataRow r in DtDetailItem.Rows)
        {
            foreach (ListItem ltItem in ddlItemName.Items)
            {
                if (ltItem.Value == r["ItemCode"].ToString())
                {
                    ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("disabled", "disabled");
                    ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");
                }
            }

        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
//    public static List<string> SearchTr(string prefixText, int count)
//    {

//        string BI = SessionManager.SessionName.BusinessCentetid.ToString();

//        using (OracleConnection conn = new OracleConnection())
//        {
//            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
//            using (OracleCommand cmd = new OracleCommand())
//            {

//                cmd.CommandText = @"SELECT  RM.ID||' - '||VH.REGNO||' - '||WH.VISMWHNAME||' - '||RM.IDATE||' - '||em.EMPLOYEENAME||' - '||replace(replace(rm.iscomplete,'0','No CO'),'1','CO done')||'-'||tm.ID||'-'||emc.EMPLOYEENAME CD 
//FROM RVS_REQMASTER RM
//INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
//INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
//inner join EM_EMPLOYEE em on rm.IUSER=em.USERID
//left join rvs_trmaster tm on tm.REFTRID=rm.id
//left join EM_EMPLOYEE emc on tm.IUSER=emc.USERID
//where rm.id like '%" + prefixText + "' or lower(vh.REGNO) like lower('%" + prefixText + "') AND ROWNUM <= 20 and RM.REQTYPE='CD' and RM.ISCOMPLETE='1' and RM.SENDLOCATION='" + BI + "' ORDER BY RM.IDATE DESC";

//                cmd.Connection = conn;
//                conn.Open();
//                List<string> items = new List<string>();
//                using (OracleDataReader sdr = cmd.ExecuteReader())
//                {
//                    while (sdr.Read())
//                    {
//                        items.Add(sdr["CD"].ToString());
//                    }
//                }
//                conn.Close();
//                return items;
//            }
//        }
//    }
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
                //tbxCustomer.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                tbxModel.Text = dt.Rows[0]["MODELNAME"].ToString();
                tbxDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
                tbxDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
                tbxStaffId.Text = dt.Rows[0]["DRIVERID"].ToString();

                tbxVehicleid.Enabled = false;
                tbxVehicleRegNumber.Enabled = false;
                tbxModel.Enabled = false;
                //tbxCustomer.Enabled = false;
                //tbxCustomer.Enabled = false;
                ddlCustomer.SelectedValue = dt.Rows[0]["CUSTOMERID"].ToString();

                tbxCustomer.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                tbxMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                tbxAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                tbxEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                tbxcc1.Text = dt.Rows[0]["CC1"].ToString();
                tbxcc2.Text = dt.Rows[0]["CC2"].ToString();

                tbxNid.Text = dt.Rows[0]["NID"].ToString();

                //ddlCustomer_OnSelectedIndexChanged(null, null);
            }
            else
            {
                //tbxVehicleid.Text = "";
                //hdnVehicleId.Value = "0";
            }
        }
        catch
        {
            //tbxVehicleid.Text = "";
            //hdnVehicleId.Value = "0";
        }
    }

    protected void ddlCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCustomer.SelectedIndex > 0)
        {
            DataTable dt = dm.ExecuteQuery(@"Select * from rvs_customer where id='" + ddlCustomer.SelectedValue + "'");
            tbxCustomer.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
            tbxMobile.Text = dt.Rows[0]["MOBILE"].ToString();
            tbxAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            tbxEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            tbxcc1.Text = dt.Rows[0]["CC1"].ToString();
            tbxcc2.Text = dt.Rows[0]["CC2"].ToString();

            tbxNid.Text = dt.Rows[0]["NID"].ToString();

        }

    }

    protected void chkEditCustomer_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkEditCustomer.Checked)
        {
            ddlCustomer.Visible = false;
            tbxCustomer.Visible = true;
            pnlCusInfo.Enabled = true;
            //tbxCustomer.Text = "";
            tbxMobile.Enabled = true;
            tbxAddress.Enabled = true;
            tbxEmail.Enabled = true;
            tbxcc1.Enabled = true;
            tbxcc2.Enabled = true;
            tbxNid.Enabled = true;
            RequiredFieldValidatorDD.Enabled = false;
            RequiredFieldValidatorTX.Enabled = true;
        }
        else
        {
            ddlCustomer.Visible = true;
            tbxCustomer.Visible = false;
            tbxMobile.Enabled = false;
            pnlCusInfo.Enabled = false;
            //tbxCustomer.Text = "";
            tbxMobile.Text = "";
            tbxAddress.Text = "";
            tbxEmail.Text = "";
            tbxcc1.Text = "";
            tbxcc2.Text = "";
            tbxNid.Text = "";
            ddlCustomer.SelectedValue = "0";
            RequiredFieldValidatorDD.Enabled = true;
            RequiredFieldValidatorTX.Enabled = false;
        }

    }

}