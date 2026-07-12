using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Pages_VISM_TiForm : BasePage
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
        //ListItem li = new ListItem("Select", "");
        //string qry =
        //    @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        //ddlRecvLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlRecvLoc.DataBind();
        //ddlRecvLoc.Items.Insert(0, li);

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
        DtDetail.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlItemName.SelectedValue, ddlItemName.SelectedItem.Text.Split('-')[1], tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxQty.Text.Trim(), tbxAmt.Text);
        BindRpt();
        ddlCategory_OnSelectedIndexChanged(null, null);
        tbxUnitCode.Text = "";
        tbxRate.Text = "";
        tbxQty.Text = "";
        tbxAmt.Text = "";
        tbxQty.Enabled = false;
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (DtDetail.Rows.Count == 0)
            {
                MessageBox("No item found.");
                return;
            }

            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='TI' RETURNING LASTNO INTO :RETVAL";
            string lno = dm.ExecuteScalar(qry, "RETVAL");
            db.BeginTransaction();
            const string trType = "TI";
            string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
            //qry = @"pro_rvs_ti('" + ddlToId.SelectedValue + "','" + trid + "','" + tbxRecvRemarks.Text + "','" + SessionManager.SessionName.UserId + "')";
            //int a = dm.ExecuteNonQuery(qry, true);
            //btnSave.Enabled = false;
            // tbxTrId.Text = trid;
            // MessageBox(SaveSuccess);


            qry = @"INSERT INTO VISMF.RVS_TRMASTER(ID,TRTYPE,REFTRID,SENDLOCATION,SENDWH,SENDCOMPANY,SENDDATE,SENDREMARKS,SENDUSER,
            RECVLOCATION,RECVWH,RECVCOMPANY,recvdate,recvuser,iscomplete,IUSER) VALUES(
            '" + trid + "','" + trType + "','" + ddlToId.SelectedValue + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + "',TO_DATE(SYSDATE,'DD/MM/YYYY'),'" + tbxRecvRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + @"',
            '" + ddlRecvLoc.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + ddlRecvCom.SelectedValue + "',SYSDATE,'" + SessionManager.SessionName.UserId + "','1','" + SessionManager.SessionName.UserId + "')";

            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetail.Rows)
            {
                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                qry = @"INSERT INTO VISMF.RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,
            ITEMCODE,UNITID,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH) values
            ('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["ItemCode"] + @"',
            '" + r["UnitCode"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
            '" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + ddlRecvWh.SelectedValue + "')";
                db.ExecuteNonQuery(qry);
                i++;

                ///New
                string qry2 = @"select * from RVS_ITEMMASTER_WH where ITEMCODE='" + r["ItemCode"] + @"' and WHID='" + ddlRecvWh.SelectedValue + "' and LOCATIONID='" + ddlRecvLoc.SelectedValue + "'";
                string id = dm.ExecuteScalar(qry2);
                if (string.IsNullOrWhiteSpace(id))
                {
                    qry = @"INSERT INTO VISMF.RVS_ITEMMASTER_WH (ITEMCODE,WHID,LOCATIONID,RATE) VALUES('" + r["ItemCode"] + @"','" + ddlRecvWh.SelectedValue + "','" + ddlRecvLoc.SelectedValue + "'," + r["Rate"] + @")";
                    // db.BeginTransaction();
                    db.ExecuteNonQuery(qry);
                    // int i = 1;
                    // db.Commit();

                }
                else
                {

                }

                qry = @"update RVS_ITEMMASTER_WH set RATE='" + r["Rate"] + "' where ITEMCODE='" + r["ItemCode"] + "' AND WHID='" + ddlRecvWh.SelectedValue + "' AND LOCATIONID='" + ddlRecvLoc.SelectedValue + "'";
                db.ExecuteNonQuery(qry);
                //End New




            }
            qry = @"UPDATE RVS_TRMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlToId.SelectedValue + "'";
            db.ExecuteNonQuery(qry);
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

      //  Response.Redirect(Request.RawUrl);
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
    }



    protected void ddlRecvLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlRecvLoc.SelectedValue + "' order by VISMWHNAME";
        ddlRecvWh.DataSource = dm.ExecuteQuery(qry);
        ddlRecvWh.DataBind();
        ddlRecvWh.Items.Insert(0, li);
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
        qry = @"SELECT TM.ID,TM.ID||' - '||TO_CHAR(TM.IDATE,'DD/MM/YYYY') TEXT FROM RVS_TRMASTER TM
WHERE TM.ISACTIVE=1 AND TM.ISCOMPLETE=0 AND TM.TRTYPE='TO'
 AND TM.RECVLOCATION='" + ddlRecvLoc.SelectedValue + @"' 
 AND TM.RECVWH='" + ddlRecvWh.SelectedValue + @"' 
 AND TM.RECVCOMPANY='" + ddlRecvCom.SelectedValue + @"'
 ORDER BY TM.IDATE";
        ddlToId.DataSource = dm.ExecuteQuery(qry);
        ddlToId.DataBind();
        ddlToId.Items.Insert(0, li);
        ddlToId.Enabled = true;
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


    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");

//        string qry = @"select ITEMCODE id,ITEMCODE||'-'||ITEMNAME text  from RVS_ITEMMASTER where isactive=1
// and ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "'order by ITEMNAME";

        string qry = @"select IM.ITEMCODE id,IM.ITEMCODE||'-'||IM.ITEMNAME text  from RVS_ITEMMASTER IM 
INNER JOIN RVS_ITEMMASTER_WH IW ON IM.ITEMCODE=IW.ITEMCODE where IM.isactive=1
and IM.ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "' AND IW.LOCATIONID='" + ddlRecvLoc.SelectedValue + "' AND IW.WHID='" + ddlRecvWh.SelectedValue + "' order by ITEMNAME";

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
        ddlItemName.Enabled = true;

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

    protected void ddlToId_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"SELECT TM.SENDLOCATION,LO.CENTERNAME, TM.SENDWH,WH.VISMWHNAME, TM.SENDCOMPANY,WS.WORKSHOP,TD.ITEMCATEGORYCODE,ic.ITEMCATEGORYNAME,
TD.ITEMCODE,im.ITEMNAME,TD.UNITID,su.UNITNAME,TD.RATE,TD.QTY,TD.TOTALAMT,TD.REMARKS,TM.SENDREMARKS FROM RVS_TRDETAIL TD
INNER JOIN RVS_TRMASTER TM ON TD.TRMASTERID=TM.ID
inner join BS_BUSINESSCENTER LO on TM.SENDLOCATION=lo.id
inner join RVS_VISMWH wh on TM.SENDWH=wh.id
inner join rvs_workshop ws on TM.SENDCOMPANY=ws.id
inner join RVS_ITEMCATEGORY ic on TD.ITEMCATEGORYCODE=ic.ITEMCATEGORYCODE
inner join RVS_ITEMMASTER im on TD.ITEMCODE=im.ITEMCODE
INNER JOIN RVS_SCMUNIT su on TD.UNITID=su.UNITCODE
WHERE TD.TRMASTERID='" + ddlToId.SelectedValue + @"'
ORDER BY TD.LINE";
        DataTable dtd = dm.ExecuteQuery(qry);
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(dtd.Rows[0]["SENDLOCATION"].ToString(), dtd.Rows[0]["CENTERNAME"].ToString());
        ddlSendLoc.DataSource = dt;
        ddlSendLoc.DataBind();
        dt.Clear();
        dt.Rows.Add(dtd.Rows[0]["SENDWH"].ToString(), dtd.Rows[0]["VISMWHNAME"].ToString());
        ddlSendWh.DataSource = dt;
        ddlSendWh.DataBind();
        dt.Clear();
        dt.Rows.Add(dtd.Rows[0]["SENDCOMPANY"].ToString(), dtd.Rows[0]["WORKSHOP"].ToString());
        ddlSendCom.DataSource = dt;
        ddlSendCom.DataBind();
        DtDetail.Clear();
        foreach (DataRow r in dtd.Rows)
        {
            DtDetail.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
                r["UNITID"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["QTY"].ToString(),
                r["TOTALAMT"].ToString());
        }
        BindRpt();
        ListItem li = new ListItem("Select", "");
        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);

        tbxRecvRemarks.Text = dtd.Rows[0]["SENDREMARKS"].ToString();

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