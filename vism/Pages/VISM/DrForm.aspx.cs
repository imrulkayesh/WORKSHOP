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

public partial class Pages_VISM_DrForm : BasePage
{
    //DatabaseManagerORCL da = new DatabaseManagerORCL();
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

        string BI = SessionManager.SessionName.BusinessCentetid.ToString();
        ListItem li = new ListItem("Select", "");
        string qry = "";
        if (BI == "2")
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        }
        else
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
        }

        ddlRecvLoc.DataSource = dm.ExecuteQuery(qry);
        ddlRecvLoc.DataBind();
        ddlRecvLoc.Items.Insert(0, li);
    }
    private void DtDetailAddColumn()
    {
        DtDetailItem = new DataTable();
        DtDetailItem.Columns.Add("CategoryCode");
        DtDetailItem.Columns.Add("CategoryName");
        DtDetailItem.Columns.Add("ItemCode");
        DtDetailItem.Columns.Add("ItemName");
        DtDetailItem.Columns.Add("UnitCode");
        DtDetailItem.Columns.Add("UnitName");
        DtDetailItem.Columns.Add("Rate");
        DtDetailItem.Columns.Add("Qty");
        DtDetailItem.Columns.Add("TotalAmount");
        DtDetailItem.Columns.Add("Remarks");

    }
    private void BindRpt()
    {
        DtDetailItem.AcceptChanges();
        rpt.DataSource = DtDetailItem;
        rpt.DataBind();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        //if (tbxRate.Text == "0")
        //{
        //    MessageBox("Please ask Sender to update Item Rate");
        //    ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxRate').focus();", true);
        //}

        //else
        //{
            foreach (DataRow row in DtDetailItem.Rows)
            {
                if (ddlItemName.SelectedValue != row["ItemCode"].ToString()) continue;
                MessageBox(ddlItemName.SelectedItem.Text + " is already added.");
                return;
            }
            string totAmt = (Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQty.Text)).ToString();
            tbxAmt.Text = totAmt;

            DtDetailItem.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlItemName.SelectedValue, ddlItemName.SelectedItem.Text.Substring(ddlItemName.SelectedItem.Text.IndexOf('-') + 1), tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxQty.Text.Trim(), tbxAmt.Text,tbxItemRemarks.Text);

            BindRpt();
            ddlCategory_OnSelectedIndexChanged(null, null);
        tbxItemRemarks.Text = "";
        //}


    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (DtDetailItem.Rows.Count == 0)
            {
                MessageBox("Please add item first.");
                return;
            }
            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='DR' RETURNING LASTNO INTO :RETVAL";
            string lno = dm.ExecuteScalar(qry, "RETVAL");
            db.BeginTransaction();
            const string trType = "DR";
            string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
            qry = @"INSERT INTO RVS_REQMASTER(ID,REQTYPE,SENDLOCATION,SENDWH,SENDCOMPANY,
RECVLOCATION,RECVWH,RECVCOMPANY,RECVREMARKS,IUSER) VALUES(
'" + trid + "','" + trType + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"',
'" + ddlRecvLoc.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + ddlRecvCom.SelectedValue + "','" + tbxRecRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + "')";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailItem.Rows)
            {
                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                qry = @"INSERT INTO RVS_REQDETAIL (ID,REQMASTERID,ITEMCATEGORYCODE,ITEMCATEGORYNAME,
ITEMCODE,ITEMNAME,UNITID,UNITNAME,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH,REMARKS) values
('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["CategoryName"] + "','" + r["ItemCode"] + @"',
'" + r["ItemName"].ToString().Replace("'", "''") + "','" + r["UnitCode"] + "','" + r["UnitName"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','"+ r["Remarks"] + "')";
                db.ExecuteNonQuery(qry);
                i++;
            }
            db.Commit();
            btnSave.Enabled = false;
            tbxTrId.Text = trid;
            ViewState["TrId"] = trid;
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
        for (int i = 0; i < DtDetailItem.Rows.Count; i++)
        {
            if (DtDetailItem.Rows[i]["ItemCode"].ToString() == e.CommandArgument.ToString())
            {
                DtDetailItem.Rows[i].Delete();
            }
        }
        BindRpt();
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



    protected void ddlRecvLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlRecvLoc.SelectedValue + "' order by VISMWHNAME";
        ddlRecvWh.DataSource = dm.ExecuteQuery(qry);
        ddlRecvWh.DataBind();
        ddlRecvWh.Items.Insert(0, li);
        ddlRecvCom.Items.Clear();
        ddlRecvWh.Enabled = true;
    }

    protected void ddlRecvWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
where wh.ID='" + ddlRecvWh.SelectedValue + "'";
        DataTable val = dm.ExecuteQuery(qry);
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(val.Rows[0]["ID"].ToString(), val.Rows[0]["TEXT"]);
        ddlRecvCom.DataSource = dt;
        ddlRecvCom.DataBind();

        ListItem li = new ListItem("Select", "");
        qry =
           @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        ddlSendLoc.DataBind();
        ddlSendLoc.Items.Insert(0, li);

        ddlSendLoc.Enabled = true;
    }

    protected void ddlSendLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlSendLoc.SelectedValue + "' order by VISMWHNAME";
        ddlSendWh.DataSource = dm.ExecuteQuery(qry);
        ddlSendWh.DataBind();
        ddlSendWh.Items.Insert(0, li);
        ddlSendCom.Items.Clear();
        ddlSendWh.Enabled = true;

        if (ddlRecvLoc.SelectedValue == ddlSendLoc.SelectedValue)
        {
            ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("disabled", "disabled");
            ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");

        }

    }

    protected void ddlSendWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
where wh.ID='" + ddlSendWh.SelectedValue + "'";
        DataTable val = dm.ExecuteQuery(qry);
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(val.Rows[0]["ID"].ToString(), val.Rows[0]["TEXT"]);
        ddlSendCom.DataSource = dt;
        ddlSendCom.DataBind();

        ListItem li = new ListItem("Select", "");
        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);
        ddlCategory.Enabled = true;
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");



        //       string qry = @"select ITEMCODE id,ITEMCODE||'-'||ITEMNAME text  from RVS_ITEMMASTER where isactive=1
        //and ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "'order by ITEMNAME";


        string qry = @"select IM.ITEMCODE id,IM.ITEMCODE||'-'||IM.ITEMNAME text  from RVS_ITEMMASTER IM 
INNER JOIN RVS_ITEMMASTER_WH IW ON IM.ITEMCODE=IW.ITEMCODE where IM.isactive=1
and IM.ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "' AND IW.LOCATIONID='" + ddlRecvLoc.SelectedValue + "' AND IW.WHID='" + ddlRecvWh.SelectedValue + "' order by ITEMNAME";


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
        tbxUnitCode.Text = "";
        tbxRate.Text = "";
        tbxQty.Text = "";
        tbxAmt.Text = "";
        ddlItemName.Enabled = true;
        tbxQty.Enabled = false;

    }

    protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME,IM.RATE FROM RVS_ITEMMASTER IM
        //inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE where im.isactive=1 and IM.ITEMCODE='" + ddlItemName.SelectedValue + "'";


        string SL = ddlSendLoc.SelectedValue;
        string SW = ddlSendWh.SelectedValue;

        string qry = @" SELECT im.SCMUNITCODE,ut.UNITNAME,iw.RATE FROM RVS_ITEMMASTER IM
inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE 
INNER JOIN rvs_itemmaster_wh iw ON im.itemcode = iw.itemcode
where im.isactive=1 AND iw.isactive = '1' and IM.ITEMCODE='" + ddlItemName.SelectedValue + "' AND IW.LOCATIONID='" + ddlSendLoc.SelectedValue + "' AND IW.WHID='" + ddlSendWh.SelectedValue + "'";

        DataTable dt = dm.ExecuteQuery(qry);
        tbxUnitCode.Text = dt.Rows[0]["SCMUNITCODE"].ToString();
        hdnUnitName.Value = dt.Rows[0]["UNITNAME"].ToString();
        tbxRate.Text = dt.Rows[0]["RATE"].ToString();
        tbxQty.Text = "";
        tbxAmt.Text = "";
        tbxQty.Enabled = true;


    }

    protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            DataRow rows = DtDetailItem.Select("ItemCode = '" + e.CommandArgument + "'").FirstOrDefault();
            ddlCategory.SelectedValue = rows["CategoryCode"].ToString();
            ddlCategory_OnSelectedIndexChanged(null, null);
            ddlItemName.SelectedValue = e.CommandArgument.ToString();
            tbxUnitCode.Text = rows["UnitCode"].ToString();
            tbxRate.Text = rows["Rate"].ToString();
            tbxQty.Text = rows["Qty"].ToString();
            tbxItemRemarks.Text =rows["Remarks"].ToString();
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
        DataRow rows = DtDetailItem.Select("ItemCode = '" + (string)ViewState["ID"] + "'").FirstOrDefault();
        rows["Remarks"] = tbxItemRemarks.Text;
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
        tbxItemRemarks.Text = "";
        tbxAmt.Text = "";
        tbxQty.Enabled = false;

    }

    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        if (DtDetailItem.Rows.Count == 0 || tbxTrId.Text == "")
        {
            MessageBox("No DR found to print.");
            return;
        }
        string qry = @"SELECT 'DR No: '|| RM.ID CDNO,'DR Date: '||to_char(RM.IDATE,'DD/MM/YYYY HH:MI AM') CDDATE,LO.CENTERNAME FROMLOC,WH.VISMWHNAME FROMWH,WS.WORKSHOP FROMCOM,RM.IUSER||' - '||EM.EMPLOYEENAME CREATEDBY,
LOT.CENTERNAME TOLOC, WHT.VISMWHNAME TOWH,WST.WORKSHOP TOCOM,RM.RECVREMARKS SENDREMARKS,
IM.ITEMCODE,IM.ITEMNAME,UT.UNITNAME,RD.QTY,RD.REMARKS,'Demand Request (DR)' RTITLE FROM RVS_REQDETAIL RD
INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
INNER JOIN RVS_ITEMMASTER IM ON RD.ITEMCODE=IM.ITEMCODE
INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE =UT.UNITCODE
INNER JOIN BS_BUSINESSCENTER LO ON RM.SENDLOCATION=LO.ID
INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
INNER JOIN RVS_WORKSHOP WS ON RM.SENDCOMPANY=WS.ID
INNER JOIN BS_BUSINESSCENTER LOT ON RM.RECVLOCATION=LOT.ID
INNER JOIN RVS_VISMWH WHT ON RM.RECVWH=WHT.ID
INNER JOIN RVS_WORKSHOP WST ON RM.SENDCOMPANY=WST.ID
INNER JOIN EM_EMPLOYEE EM ON RM.IUSER=EM.STAFFID
WHERE RM.ID='" + (string)ViewState["TrId"] + @"'
ORDER BY RD.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/pages/VISM/CrReport/CrCdPrint.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);
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
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchTr(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT  RM.ID||' - TO From: '||WHF.VISMWHNAME||' -------- TI To: '||WHT.VISMWHNAME||' - '||RM.IDATE||' - '||em.EMPLOYEENAME||' - '||replace(replace(rm.iscomplete,'0','No TO'),'1','TO Done') DR 
FROM RVS_REQMASTER RM
INNER JOIN RVS_VISMWH WHF ON RM.SENDWH=WHF.ID
INNER JOIN RVS_VISMWH WHT ON RM.RECVWH=WHT.ID
inner join EM_EMPLOYEE em on rm.IUSER=em.staffid
where rm.REQTYPE='DR' and rm.id like '%" + prefixText + "'  or lower(em.EMPLOYEENAME) like lower('%" + prefixText + @"%') AND ROWNUM <= 20
ORDER BY RM.IDATE DESC";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["DR"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void btnSearchTr_OnClick(object sender, EventArgs e)
    {
        string trid = tbxTrNo.Text.Split('-')[0].Trim();
        ViewState["TrId"] = trid;
        string qry = @"SELECT RM.RECVLOCATION RECLOC, RM.RECVWH RECWH,RM.RECVREMARKS, RM.SENDLOCATION SENDLOC,RM.SENDWH,RM.ID DRID,
RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,
RD.RATE,RD.QTY,RD.RATE*RD.QTY TOTALAMOUNTT,RD.REMARKS
FROM RVS_REQDETAIL RD
INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
INNER JOIN EM_EMPLOYEE EM ON RM.IUSER=EM.USERID
WHERE RM.REQTYPE='DR' AND  RM.ID ='" + trid + @"'
ORDER BY RD.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        ddlRecvLoc.SelectedValue = dt.Rows[0]["RECLOC"].ToString();
        ddlRecvLoc_OnSelectedIndexChanged(null, null);
        ddlRecvWh.SelectedValue = dt.Rows[0]["RECWH"].ToString();
        ddlRecvWh_OnSelectedIndexChanged(null, null);
        ddlSendLoc.SelectedValue = dt.Rows[0]["SENDLOC"].ToString();
        ddlSendLoc_OnSelectedIndexChanged(null, null);
        ddlSendWh.SelectedValue = dt.Rows[0]["SENDWH"].ToString();
        ddlSendWh_OnSelectedIndexChanged(null, null);
        tbxTrId.Text = dt.Rows[0]["DRID"].ToString();
        ViewState["TrId"] = dt.Rows[0]["DRID"].ToString();
        btnSave.Enabled = false;
        tbxTrId.BackColor = Color.DarkCyan;
        tbxTrId.ForeColor = Color.White;
        tbxTrId.Enabled = true;
        tbxTrId.ReadOnly = true;
        ddlRecvLoc.Enabled = false;
        ddlRecvWh.Enabled = false;
        tbxRecRemarks.ReadOnly = true;
        ddlSendLoc.Enabled = false;
        ddlSendWh.Enabled = false;
        btnAdd.Enabled = false;
        pnlAddItem.Visible = false;
        DtDetailItem.Rows.Clear();
        foreach (DataRow r in dt.Rows)
        {
            DtDetailItem.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(), r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(), r["UNITID"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["QTY"].ToString(), r["TOTALAMOUNTT"].ToString(), r["REMARKS"].ToString());
        }
        BindRpt();
    }
}