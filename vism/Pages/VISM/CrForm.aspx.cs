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

public partial class Pages_VISM_CrForm : BasePage
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
        }

    }

    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "");


    }
    private void DtDetailAddColumn()
    {
        DtDetail = new DataTable();
        DtDetail.Columns.Add("CategoryCode");
        DtDetail.Columns.Add("CategoryName");
        DtDetail.Columns.Add("ItemCode");
        DtDetail.Columns.Add("ItemName");
        DtDetail.Columns.Add("UnitCode");
        DtDetail.Columns.Add("UnitName");
        DtDetail.Columns.Add("Rate");
        DtDetail.Columns.Add("Qty");
        DtDetail.Columns.Add("TotalAmount");
        DtDetail.Columns.Add("SRate");

    }
    private void BindRpt()
    {
        DtDetail.AcceptChanges();
        rpt.DataSource = DtDetail;
        rpt.DataBind();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        foreach (DataRow row in DtDetail.Rows)
        {
            if (ddlItemName.SelectedValue != row["ItemCode"].ToString()) continue;
            MessageBox(ddlItemName.SelectedItem.Text + " is already added.");
            return;
        }
        string totAmt = (Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQty.Text)).ToString();
        tbxAmt.Text = totAmt;
        DtDetail.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlItemName.SelectedValue, ddlItemName.SelectedItem.Text.Substring(ddlItemName.SelectedItem.Text.IndexOf('-') + 1), tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxQty.Text.Trim(), tbxAmt.Text);

        BindRpt();
        ddlCategory_OnSelectedIndexChanged(null, null);
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (DtDetail.Rows.Count == 0)
            {
                MessageBox("Please add item first.");
                return;
            }

            string qry1 = @"SELECT DISTINCT ID FROM RVS_JOBCARD JC
WHERE JC.VEHICLEID='" + tbxSendVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";
            string jobid = dm.ExecuteScalar(qry1);

            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='CR' RETURNING LASTNO INTO :RETVAL";
            string lno = dm.ExecuteScalar(qry, "RETVAL");
            db.BeginTransaction();
            const string trType = "CR";
            string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
            qry = @"INSERT INTO RVS_TRMASTER(ID,TRTYPE,REFTRID,SENDLOCATION,SENDWH,SENDCOMPANY,SENDREMARKS,
RECVLOCATION,RECVWH,RECVCOMPANY,ISCOMPLETE,IUSER) VALUES(
'" + trid + "','" + trType + "','" + ddlCoId.SelectedValue + "','" + ddlSendLoc.SelectedValue + "','" + tbxSendVeh.Text.Split('-')[0] + "','" + ddlSendCom.SelectedValue + @"','" + tbxSendRemarks.Text.Trim().Replace("'", "''") + @"',
'" + ddlRecvLoc.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + ddlRecvCom.SelectedValue + "','1','" + SessionManager.SessionName.UserId + "')";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetail.Rows)
            {
                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                qry = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,
ITEMCODE,UNITID,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH,SRATE) values
('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["ItemCode"] + @"',
'" + r["UnitCode"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + tbxSendVeh.Text.Split('-')[0] + "','" + ddlRecvWh.SelectedValue + "','" + r["SRate"] + @"')";
                db.ExecuteNonQuery(qry);


                qry = @"insert into RVS_JOBCARDITEMS (JOBID,PARTSID,QUANTITY,ITEMCODE,RATE,TOTALAMOUNT,MARGINAMOUNT,LINE,AMOUNT,CDID,SRATE) values
('" + jobid + "','0','" + r["Qty"] + "','" + r["ItemCode"] + "','" + r["Rate"] +
                      "','" + r["TotalAmount"] + "','" + r["TotalAmount"] + "','" + i + "','0','" + trid + "','" + r["SRate"] + "')";
                db.ExecuteNonQuery(qry);
                i++;
            }
            //qry = @"UPDATE RVS_TRMASTER SET ISCOMPLETE=0 WHERE ID='" + ddlCoId.SelectedValue + "' AND ISACTIVE=1";
            //db.ExecuteNonQuery(qry);
            //db.Commit();

            db.Commit();
            btnSave.Enabled = false;
            tbxCrId.Text = trid;
            tbxCrId.BackColor = Color.DarkCyan;
            tbxCrId.ForeColor = Color.White;
            tbxCrId.Enabled = true;
            tbxCrId.ReadOnly = true;
            MessageBox(SaveSuccess);
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

    protected void imgbtnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        for (int i = 0; i < DtDetail.Rows.Count; i++)
        {
            if (DtDetail.Rows[i]["ItemCode"].ToString() == e.CommandArgument.ToString())
            {
                DtDetail.Rows[i].Delete();
            }
        }
        BindRpt();
        foreach (DataRow r in DtDetail.Rows)
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





//    protected void ddlRecvWh_OnSelectedIndexChanged(object sender, EventArgs e)
//    {
//        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
//inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
//where wh.ID='" + tbxRecVeh.Text + "'";
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

    //    //if (ddlRecvLoc.SelectedValue == ddlSendLoc.SelectedValue)
    //    //{
    //    //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("disabled", "disabled");
    //    //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");

    //    //}

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
//        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
//        ddlCategory.DataSource = dm.ExecuteQuery(qry);
//        ddlCategory.DataBind();
//        ddlCategory.Items.Insert(0, li);
//    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select ITEMCODE id,ITEMCODE||'-'||ITEMNAME text  from RVS_ITEMMASTER where isactive=1
 and ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "'order by ITEMNAME";
        ddlItemName.DataSource = dm.ExecuteQuery(qry);
        ddlItemName.DataBind();
        ddlItemName.Items.Insert(0, li);
        foreach (DataRow r in DtDetail.Rows)
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

    protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME,IM.RATE FROM RVS_ITEMMASTER IM
inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE where im.isactive=1 and IM.ITEMCODE='" + ddlItemName.SelectedValue + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        tbxUnitCode.Text = dt.Rows[0]["SCMUNITCODE"].ToString();
        hdnUnitName.Value = dt.Rows[0]["UNITNAME"].ToString();
        tbxRate.Text = dt.Rows[0]["RATE"].ToString();
        tbxQty.Text = "";
        tbxAmt.Text = "";
        tbxQty.Enabled = true;
    }

//    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
//    {
//        string qry = @"SELECT VH.BUSINESSCENTERID ID,LO.CENTERNAME TEXT,VH.COMPANYID CID,CO.COMPANYNAME CTEXT FROM RVS_VEHICLE VH
//INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
//INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
//WHERE VH.ID='" + tbxRecVeh.Text.Split('-')[0] + "'";
//        DataTable dt = dm.ExecuteQuery(qry);
//        ListItem li = new ListItem(dt.Rows[0]["TEXT"].ToString(), dt.Rows[0]["ID"].ToString());
//        ddlRecvLoc.DataSource = null;
//        ddlRecvLoc.DataBind();
//        ddlRecvLoc.Items.Insert(0, li);
//        li = new ListItem(dt.Rows[0]["CTEXT"].ToString(), dt.Rows[0]["CID"].ToString());
//        ddlRecvCom.DataSource = null;
//        ddlRecvCom.DataBind();
//        ddlRecvCom.Items.Insert(0, li);
//        ddlSendLoc.Enabled = true;
//        li = new ListItem("Select", "");
//        qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
//        ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
//        ddlSendLoc.DataBind();
//        ddlSendLoc.Items.Insert(0, li);
//    }
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
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%'))  AND ROWNUM <= 20";
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

    protected void ddlCoId_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"SELECT TM.SENDLOCATION,LO.CENTERNAME, TM.SENDWH,WH.VISMWHNAME , TM.SENDCOMPANY,WS.WORKSHOP ,TD.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME ,
TD.ITEMCODE,IM.ITEMNAME,TD.UNITID,UT.UNITNAME,TD.RATE,TD.QTY,TD.TOTALAMT,TD.REMARKS, TD.SRATE FROM RVS_TRDETAIL TD
INNER JOIN RVS_TRMASTER TM ON TD.TRMASTERID=TM.ID
INNER JOIN BS_BUSINESSCENTER LO ON TM.SENDLOCATION=LO.ID
INNER JOIN RVS_VISMWH WH ON TM.SENDWH=WH.ID
INNER JOIN RVS_WORKSHOP WS ON TM.SENDCOMPANY=WS.ID
INNER JOIN RVS_ITEMCATEGORY CT ON TD.ITEMCATEGORYCODE= CT.ITEMCATEGORYCODE
INNER JOIN RVS_ITEMMASTER IM ON TD.ITEMCODE=IM.ITEMCODE
INNER JOIN RVS_SCMUNIT UT ON TD.UNITID=UT.UNITCODE
WHERE TM.ID='" + ddlCoId.SelectedValue + @"'
ORDER BY TD.LINE";
        //DataTable dtd = dm.ExecuteQuery(qry);
        //ListItem li = new ListItem(dtd.Rows[0]["CENTERNAME"].ToString(), dtd.Rows[0]["SENDLOCATION"].ToString());
        //ddlRecvLoc.DataSource = null;
        //ddlRecvLoc.DataBind();
        //ddlRecvLoc.Items.Insert(0, li);

        //li = new ListItem(dtd.Rows[0]["VISMWHNAME"].ToString(), dtd.Rows[0]["SENDWH"].ToString());
        //ddlRecvWh.DataSource = null;
        //ddlRecvWh.DataBind();
        //ddlRecvWh.Items.Insert(0, li);

        //li = new ListItem(dtd.Rows[0]["WORKSHOP"].ToString(), dtd.Rows[0]["SENDCOMPANY"].ToString());
        //ddlRecvCom.DataSource = null;
        //ddlRecvCom.DataBind();
        //ddlRecvCom.Items.Insert(0, li);

        DataTable dtd = dm.ExecuteQuery(qry);
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(dtd.Rows[0]["SENDLOCATION"].ToString(), dtd.Rows[0]["CENTERNAME"].ToString());
        ddlRecvLoc.DataSource = dt;
        ddlRecvLoc.DataBind();
        dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["SENDWH"].ToString(), dtd.Rows[0]["VISMWHNAME"].ToString());
        ddlRecvWh.DataSource = dt;
        ddlRecvWh.DataBind();
        dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["SENDCOMPANY"].ToString(), dtd.Rows[0]["WORKSHOP"].ToString());
        ddlRecvCom.DataSource = dt;
        ddlRecvCom.DataBind();
        dt.Clear();


        DtDetail.Clear();
        foreach (DataRow r in dtd.Rows)
        {
            DtDetail.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
                r["UNITID"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["QTY"].ToString(),
                r["TOTALAMT"].ToString(), r["SRATE"].ToString());
        }
        BindRpt();

        ListItem li = new ListItem("Select", "");
        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);

    }

    protected void tbxSendVeh_OnTextChanged(object sender, EventArgs e)
    {
        string qry = @"SELECT VH.BUSINESSCENTERID ID,LO.CENTERNAME TEXT,VH.COMPANYID CID,CO.COMPANYNAME CTEXT FROM RVS_VEHICLE VH
INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
WHERE VH.ID='" + tbxSendVeh.Text.Split('-')[0] + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        ListItem li = new ListItem(dt.Rows[0]["TEXT"].ToString(), dt.Rows[0]["ID"].ToString());
        ddlSendLoc.Items.Clear();
        ddlSendLoc.DataSource = null;
        ddlSendLoc.DataBind();
        ddlSendLoc.Items.Insert(0, li);
        li = new ListItem(dt.Rows[0]["CTEXT"].ToString(), dt.Rows[0]["CID"].ToString());
        ddlSendCom.Items.Clear();
        ddlSendCom.DataSource = null;
        ddlSendCom.DataBind();
        ddlSendCom.Items.Insert(0, li);
        ddlCoId.Enabled = true;
        li = new ListItem("Select", "");
        //        qry = @"select tm.id,tm.id||' - '||to_char(tm.IDATE,'DD/MM/YYY') text from rvs_trmaster tm
        //where tm.RECVLOCATION='"+ddlSendLoc.SelectedValue+"' and tm.RECVWH='"+tbxSendVeh.Text.Split('-')[0] +"' and tm.RECVCOMPANY='"+ddlSendCom.SelectedValue+@"' and tm.isactive=1
        //order by tm.IDATE desc";

        //        qry = @"select tm.id,tm.id||' - '||to_char(tm.IDATE,'DD/MM/YYY') text from rvs_trmaster tm
        //where tm.RECVLOCATION='" + ddlSendLoc.SelectedValue + "' and tm.RECVWH='" + tbxSendVeh.Text.Split('-')[0] + "' and tm.isactive='1' order by tm.IDATE desc";

                  qry = @"select tm.id,tm.id||' - '||to_char(tm.IDATE,'DD/MM/YYYY') text from rvs_trmaster tm
        where tm.RECVLOCATION='" + ddlSendLoc.SelectedValue + "' and tm.RECVWH='" + tbxSendVeh.Text.Split('-')[0] + "' and tm.isactive='1' AND TO_CHAR(tm.TRDATE, 'MM-YYYY')=TO_CHAR(SYSDATE,'MM-YYYY')";


        ddlCoId.Items.Clear();
        ddlCoId.DataSource = dm.ExecuteQuery(qry);
        ddlCoId.DataBind();
        ddlCoId.Items.Insert(0, li);
    }

    protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            DataRow rows = DtDetail.Select("ItemCode = '" + e.CommandArgument + "'").FirstOrDefault();


            ddlCategory.SelectedValue = rows["CategoryCode"].ToString();
            ddlCategory_OnSelectedIndexChanged(null, null);
            ddlItemName.SelectedValue = e.CommandArgument.ToString();
            tbxUnitCode.Text = rows["UnitCode"].ToString();
            tbxRate.Text = rows["Rate"].ToString();
            tbxQty.Text = rows["Qty"].ToString();
            tbxAmt.Text = rows["TotalAmount"].ToString();
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            ddlCategory.Enabled = false;
            ddlItemName.Enabled = false;
            tbxQty.Enabled = true;
            ViewState["ID"] = e.CommandArgument.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        DataRow rows = DtDetail.Select("ItemCode = '" + (string)ViewState["ID"] + "'").FirstOrDefault();


        if (rows != null) rows["Qty"] = tbxQty.Text;
        BindRpt();
        ddlCategory.Enabled = true;
        ddlItemName.SelectedIndex = 0;
        ddlItemName.Enabled = true;
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        tbxRate.Text = "";
        tbxUnitCode.Text = "";
        tbxQty.Text = "";
        tbxAmt.Text = "";
        tbxQty.Enabled = false;
        foreach (DataRow r in DtDetail.Rows)
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
}