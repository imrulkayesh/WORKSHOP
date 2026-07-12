using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_ToForm : BasePage
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
        //ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlSendLoc.DataBind();
        //ddlSendLoc.Items.Insert(0, li);

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

        ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        ddlSendLoc.DataBind();
        ddlSendLoc.Items.Insert(0, li);
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
        DtDetail.Columns.Add("DRQty");
        DtDetail.Columns.Add("TOQty");
        DtDetail.Columns.Add("TotalAmount");
        DtDetail.Columns.Add("Remarks");

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
        DtDetail.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlItemName.SelectedValue, ddlItemName.SelectedItem.Text.Substring(ddlItemName.SelectedItem.Text.IndexOf('-')+1), tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxQty.Text.Trim(), tbxAmt.Text, tbxItemRemarks.Text);

        BindRpt();
        ddlCategory_OnSelectedIndexChanged(null, null);
        tbxUnitCode.Text = "";
        tbxRate.Text = "";
        tbxQty.Text = "";
        tbxAmt.Text = "";
        tbxItemRemarks.Text = "";
        tbxQty.Enabled = false;

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

            int cnt = Convert.ToInt32(DtDetail.Compute("Count(Rate)", "Rate<0.1"));

            if (cnt == 0)
            {

                string qryc = @"SELECT count(ID) id FROM RVS_TRMASTER WHERE REFTRID='" + ddlDrId.SelectedValue + "'";
                DataTable dt = dm.ExecuteQuery(qryc);
                int count = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                if (count == 0)
                {
                    string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='TO' RETURNING LASTNO INTO :RETVAL";
                    string lno = dm.ExecuteScalar(qry, "RETVAL");
                    db.BeginTransaction();
                    const string trType = "TO";
                    string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
                    qry = @"INSERT INTO RVS_TRMASTER(ID,TRTYPE,REFTRID,SENDLOCATION,SENDWH,SENDCOMPANY,SENDDATE,SENDREMARKS,SENDUSER,
                    RECVLOCATION,RECVWH,RECVCOMPANY,IUSER) VALUES(
                    '" + trid + "','" + trType + "','" + ddlDrId.SelectedValue + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"',TO_DATE(SYSDATE,'DD/MM/YYYY'),'" + tbxSendRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + @"',
                    '" + ddlRecvLoc.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + ddlRecvCom.SelectedValue + "','" + SessionManager.SessionName.UserId + "')";
                    db.ExecuteNonQuery(qry);
                    int i = 1;
                    foreach (DataRow r in DtDetail.Rows)
                    {

                        string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                        qry = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,
                        ITEMCODE,UNITID,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH,REMARKS,OLDQTY) values
                        ('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["ItemCode"] + @"',
                        '" + r["UnitCode"] + "','" + r["TOQty"] + "','" + r["Rate"] + @"',
                        '" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + r["Remarks"] + "','1')";
                        db.ExecuteNonQuery(qry);
                        i++;


                        qry = @"UPDATE RVS_REQDETAIL SET GQTY = '" + r["TOQty"] + "' where REQMASTERID = '" + ddlDrId.SelectedValue + "' AND ITEMCODE = '" + r["ItemCode"] + "'";
                        db.ExecuteNonQuery(qry);

                        //qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlDrId.SelectedValue + "'";
                        //db.ExecuteNonQuery(qry);

                        //int DRQty = Convert.ToInt32(r["DOQty"]);
                        //int TOQty = Convert.ToInt32(r["TOQty"]);

                        //if (TOQty < DRQty)
                        //{
                        //    qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=0 WHERE ID='" + ddlDrId.SelectedValue + "'";
                        //    db.ExecuteNonQuery(qry);
                        //}
                        //else
                        //{

                        //}

                    }
                    //qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlDrId.SelectedValue + "'";
                    //db.ExecuteNonQuery(qry);

                    db.Commit();
                 

                    db.BeginTransaction();

                    string qrydq = @"select A.QTY-B.QTY QTY
                    from
                    (
                    select sum(QTY) qty from RVS_REQDETAIL where REQMASTERID='" + ddlDrId.SelectedValue + @"') A,
                    (select sum(GQTY) qty from RVS_REQDETAIL where REQMASTERID= '" + ddlDrId.SelectedValue + @"') B";
                    DataTable dtdq = dm.ExecuteQuery(qrydq);

                    int DQ = Convert.ToInt32(dtdq.Rows[0][0].ToString());

                    if (DQ == 0)
                    {
                        qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlDrId.SelectedValue + "'";
                        db.ExecuteNonQuery(qry);

                        db.Commit();

                    }

                    else
                    {

                    }


      
                    btnSave.Enabled = false;
                    tbxTrId.Text = trid;
                    ViewState["TrId"] = trid;
                    MessageBox(SaveSuccess);

                }

                else
                {
                    string qrymax = @"SELECT max(line) LINE,TRMASTERID FROM RVS_TRDETAIL
                    WHERE TRMASTERID IN (SELECT id FROM RVS_TRMASTER WHERE REFTRID='" + ddlDrId.SelectedValue + "') GROUP BY TRMASTERID";
                    DataTable dtt = dm.ExecuteQuery(qrymax);

                    int i = Convert.ToInt32(dtt.Rows[0][0].ToString()) + 1;
                    string trid = dtt.Rows[0][1].ToString();
          

                    //string sqlSL = "select max(OLDQTY) SL from RVS_TRDETAIL where TRMASTERID='"+ trid + "'";
                    //DataTable dtSL = dm.ExecuteQuery(sqlSL);
                    //int lastSL = Convert.ToInt32(dtSL.Rows[0][0].ToString()) + 1;


                    db.BeginTransaction();

                    foreach (DataRow r in DtDetail.Rows)
                    {
                        string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;

                        string qry2 = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,
                        ITEMCODE,UNITID,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH,REMARKS) values
                        ('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["ItemCode"] + @"',
                        '" + r["UnitCode"] + "','" + r["TOQty"] + "','" + r["Rate"] + @"',
                        '" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + r["Remarks"] + "')";
                        db.ExecuteNonQuery(qry2);
                        i++;


                        //qry = @"UPDATE RVS_TRDETAIL SET QTY = QTY +'" + r["TOQty"] + "' where ID = '" + dt.Rows[0][0] + "';";
                        //db.ExecuteNonQuery(qry);


                        qry2 = @"UPDATE RVS_REQDETAIL SET GQTY = GQTY +'" + r["TOQty"] + "' where REQMASTERID = '" + ddlDrId.SelectedValue + "' AND ITEMCODE = '" + r["ItemCode"] + "'";
                         db.ExecuteNonQuery(qry2);

                            //qry2 = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlDrId.SelectedValue + "'";
                            //db.ExecuteNonQuery(qry2);

                            //int DRQty = Convert.ToInt32(r["DRQty"]);
                            //int TOQty = Convert.ToInt32(r["TOQty"]);

                            //if (TOQty < DRQty)
                            //{
                            //    qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=0 WHERE ID='" + ddlDrId.SelectedValue + "'";
                            //    db.ExecuteNonQuery(qry2);
                            //}
                            //else
                            //{

                            //}
                     
                                 
                    }
                    //qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlDrId.SelectedValue + "'";
                    //db.ExecuteNonQuery(qry);
                    db.Commit();

                    db.BeginTransaction();

                    string qrydq = @"select A.QTY-B.QTY QTY
                    from
                    (
                    select sum(QTY) qty from RVS_REQDETAIL where REQMASTERID='" + ddlDrId.SelectedValue + @"') A,
                    (select sum(GQTY) qty from RVS_REQDETAIL where REQMASTERID= '" + ddlDrId.SelectedValue + @"') B";
                    DataTable dtdq = dm.ExecuteQuery(qrydq);

                    int DQ = Convert.ToInt32(dtdq.Rows[0][0].ToString());

                    if (DQ == 0)
                    {
                        qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlDrId.SelectedValue + "'";
                        db.ExecuteNonQuery(qry);

                        db.Commit();
                    }

                    else
                    {

                    }

                  
                    btnSave.Enabled = false;
                    tbxTrId.Text = trid;
                    ViewState["TrId"] = trid;
                    MessageBox(SaveSuccess);
                }
            }

            else
            {
                MessageBox("Please update item rate which have 0 value");
                return;
            }

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
    }



    protected void ddlRecvLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlRecvLoc.SelectedValue + "' order by VISMWHNAME";
        ddlRecvWh.DataSource = dm.ExecuteQuery(qry);
        ddlRecvWh.DataBind();
        ddlRecvWh.Items.Insert(0, li);
        //tbxRecvCom.Text = "";
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
        DataTable dt=new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(val.Rows[0]["ID"].ToString(), val.Rows[0]["TEXT"]);
        ddlSendCom.DataSource = dt;
        ddlSendCom.DataBind();
        

        ListItem li = new ListItem("Select", "");
        qry = @"SELECT RQ.ID,WH.SHORTNAME||' - '||RQ.ID||' - '||TO_CHAR(RQ.IDATE,'DD/MM/YYYY') TEXT FROM RVS_REQMASTER RQ
INNER JOIN RVS_VISMWH WH on RQ.RECVWH=WH.ID
WHERE RQ.ISACTIVE=1 AND RQ.ISCOMPLETE=0 AND RQ.REQTYPE='DR' AND RQ.IDATE > SYSDATE - 30
 AND RQ.SENDLOCATION='" + ddlSendLoc.SelectedValue+ @"' 
 AND RQ.SENDWH='" + ddlSendWh.SelectedValue+ @"' 
 AND RQ.SENDCOMPANY='" + ddlSendCom.SelectedValue+ @"'
 ORDER BY RQ.IDATE";
        ddlDrId.DataSource = dm.ExecuteQuery(qry);
        ddlDrId.DataBind();
        ddlDrId.Items.Insert(0, li);
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");

//        string qry = @"select ITEMCODE id,ITEMCODE||'-'||ITEMNAME text  from RVS_ITEMMASTER where isactive=1
// and ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "'order by ITEMNAME";
       

        string qry = @"select IM.ITEMCODE id,IM.ITEMCODE||'-'||IM.ITEMNAME text  from RVS_ITEMMASTER IM 
INNER JOIN RVS_ITEMMASTER_WH IW ON IM.ITEMCODE=IW.ITEMCODE where IM.isactive=1
and IM.ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "' AND IW.LOCATIONID='" + ddlSendLoc.SelectedValue + "' AND IW.WHID='" + ddlSendWh.SelectedValue + "' order by ITEMNAME";
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

    protected void ddlDrId_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //        string qry = @"SELECT RM.RECVLOCATION,lo.CENTERNAME, RM.RECVWH,wh.VISMWHNAME, RM.RECVCOMPANY,ws.WORKSHOP,RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,
        //RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,RD.RATE,RD.QTY,RD.TOTALAMT,RD.REMARKS,RM.RECVREMARKS  FROM RVS_REQDETAIL RD
        //INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
        //inner join BS_BUSINESSCENTER lo on RM.RECVLOCATION=lo.id
        //inner join RVS_VISMWH wh on RM.RECVWH=wh.id
        //inner join rvs_workshop ws on RM.RECVCOMPANY=ws.id
        //WHERE RD.REQMASTERID='" + ddlDrId.SelectedValue+@"'
        //ORDER BY RD.LINE";

        string qry = @"SELECT RM.RECVLOCATION,lo.CENTERNAME, RM.RECVWH,wh.VISMWHNAME, RM.RECVCOMPANY,ws.WORKSHOP,RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,
RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,RD.RATE,(RD.QTY-RD.GQTY) DRQty,(RD.QTY-RD.GQTY) TOQty,(RD.RATE*(RD.QTY-RD.GQTY)) TOTALAMT,RD.REMARKS,RM.RECVREMARKS  FROM RVS_REQDETAIL RD
INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
inner join BS_BUSINESSCENTER lo on RM.RECVLOCATION=lo.id
inner join RVS_VISMWH wh on RM.RECVWH=wh.id
inner join rvs_workshop ws on RM.RECVCOMPANY=ws.id
WHERE RD.REQMASTERID='" + ddlDrId.SelectedValue + @"'
AND (RD.QTY-RD.GQTY)>'0'
ORDER BY RD.LINE";
        DataTable dtd = dm.ExecuteQuery(qry);
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(dtd.Rows[0]["RECVLOCATION"].ToString(), dtd.Rows[0]["CENTERNAME"].ToString());
        ddlRecvLoc.DataSource = dt;
        ddlRecvLoc.DataBind();
        dt.Clear();
        dt.Rows.Add(dtd.Rows[0]["RECVWH"].ToString(), dtd.Rows[0]["VISMWHNAME"].ToString());
        ddlRecvWh.DataSource = dt;
        ddlRecvWh.DataBind();
        dt.Clear();
        dt.Rows.Add(dtd.Rows[0]["RECVCOMPANY"].ToString(), dtd.Rows[0]["WORKSHOP"].ToString());
        ddlRecvCom.DataSource = dt;
        ddlRecvCom.DataBind();

        DtDetail.Clear();
        foreach (DataRow r in dtd.Rows)
        {
            DtDetail.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
                r["UNITID"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["DRQTY"].ToString(), r["TOQTY"].ToString(),
                r["TOTALAMT"].ToString(), r["REMARKS"].ToString());
        }
        BindRpt();
        ListItem li = new ListItem("Select", "");
        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);

        tbxSendRemarks.Text = dtd.Rows[0]["RECVREMARKS"].ToString();

    }

    protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            DataRow rows = DtDetail.Select("ItemCode = '"+e.CommandArgument+"'").FirstOrDefault();
            ddlCategory.SelectedValue = rows["CategoryCode"].ToString();
            ddlCategory_OnSelectedIndexChanged(null, null);
            ddlItemName.SelectedValue = e.CommandArgument.ToString();
            tbxUnitCode.Text = rows["UnitCode"].ToString();
            tbxRate.Text = rows["Rate"].ToString();
            tbxQty.Text = rows["TOQty"].ToString();

            tbxQtyOrg.Text = rows["DRQty"].ToString();
            tbxAmt.Text = rows["TotalAmount"].ToString();
            tbxItemRemarks.Text = rows["Remarks"].ToString();
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
        DataRow rows = DtDetail.Select("ItemCode = '"+(string)ViewState["ID"] +"'").FirstOrDefault();
        rows["Rate"] = tbxRate.Text; 
        rows["TotalAmount"] = tbxAmt.Text;
        rows["Remarks"] = tbxItemRemarks.Text;
        if (rows != null) rows["TOQty"] = tbxQty.Text;
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

    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        if (DtDetail.Rows.Count == 0 || tbxTrId.Text == "")
        {
            MessageBox("No TO found to print.");
            return;
        }
        string qry = @"SELECT 'TO No: '|| RM.ID CDNO,'TO Date: '||to_char(RM.IDATE,'DD/MM/YYYY HH:MI AM') CDDATE,LO.CENTERNAME FROMLOC,WH.VISMWHNAME FROMWH,WS.WORKSHOP FROMCOM,RM.IUSER||' - '||EM.EMPLOYEENAME CREATEDBY,
LOT.CENTERNAME TOLOC, WHT.VISMWHNAME TOWH,WST.WORKSHOP TOCOM,RM.SENDREMARKS SENDREMARKS,
IM.ITEMCODE,IM.ITEMNAME,UT.UNITNAME,RD.QTY,RD.REMARKS,'Transfer Out (TO)' RTITLE FROM RVS_TRDETAIL RD
INNER JOIN RVS_TRMASTER RM ON RD.TRMASTERID=RM.ID
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
and RD.OLDQTY IN (select max(OLDQTY) from RVS_TRDETAIL where TRMASTERID='" + (string)ViewState["TrId"] + @"')
ORDER BY RD.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/pages/VISM/CrReport/CrCdPrint.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);
    }

        protected DataTable DtDetail
    {
        set
        {
            Session["DtDetail"] = value;
        }
        get
        {
            try
            {
                return (DataTable)Session["DtDetail"];
            }
            catch
            {
                return null;
            }
        }
    }

    protected void btnTOPrint_OnClick(object sender, EventArgs e)
    {
        string qry = @"SELECT 'TO No: '|| RM.ID CDNO,'TO Date: '||to_char(RM.IDATE,'DD/MM/YYYY HH:MI AM') CDDATE,LO.CENTERNAME FROMLOC,WH.VISMWHNAME FROMWH,WS.WORKSHOP FROMCOM,RM.IUSER||' - '||EM.EMPLOYEENAME CREATEDBY,
        LOT.CENTERNAME TOLOC, WHT.VISMWHNAME TOWH,WST.WORKSHOP TOCOM,RM.SENDREMARKS SENDREMARKS,
        IM.ITEMCODE,IM.ITEMNAME,UT.UNITNAME,RD.QTY,RD.REMARKS,'Transfer Out (TO)' RTITLE FROM RVS_TRDETAIL RD
        INNER JOIN RVS_TRMASTER RM ON RD.TRMASTERID=RM.ID
        INNER JOIN RVS_ITEMMASTER IM ON RD.ITEMCODE=IM.ITEMCODE
        INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE =UT.UNITCODE
        INNER JOIN BS_BUSINESSCENTER LO ON RM.SENDLOCATION=LO.ID
        INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
        INNER JOIN RVS_WORKSHOP WS ON RM.SENDCOMPANY=WS.ID
        INNER JOIN BS_BUSINESSCENTER LOT ON RM.RECVLOCATION=LOT.ID
        INNER JOIN RVS_VISMWH WHT ON RM.RECVWH=WHT.ID
        INNER JOIN RVS_WORKSHOP WST ON RM.SENDCOMPANY=WST.ID
        INNER JOIN EM_EMPLOYEE EM ON RM.IUSER=EM.STAFFID
        WHERE RM.ID='"+ tbxTONumber.Text + @"'
        and RD.OLDQTY IN (select max(OLDQTY) from RVS_TRDETAIL where TRMASTERID='" + tbxTONumber.Text + @"')
        ORDER BY RD.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count == 0 || tbxTONumber.Text == "")
        {
            MessageBox("No TO found to print.");
            return;
        }
        else
        {
            string path = "/pages/VISM/CrReport/CrCdPrint.rpt";
            GetCrReport(path, dt, ReportAction.OpenPdf);
        }
        
    }

}

/////New
//string qry2 = @"select * from RVS_ITEMMASTER_WH where ITEMCODE='" + r["ItemCode"] + @"' and WHID='" + ddlRecvWh.SelectedValue + "' and LOCATIONID='" + ddlRecvLoc.SelectedValue + "'";
//string id = dm.ExecuteScalar(qry2);
//if (string.IsNullOrWhiteSpace(id))
//{
//    qry = @"INSERT INTO YA.RVS_ITEMMASTER_WH (ITEMCODE,WHID,LOCATIONID,RATE) VALUES('" + r["ItemCode"] + @"','" + ddlRecvWh.SelectedValue + "','" + ddlRecvLoc.SelectedValue + "'," + r["Rate"] + @")";
//    // db.BeginTransaction();
//    db.ExecuteNonQuery(qry);
//    // int i = 1;
//    // db.Commit();

//}
//else
//{

//}
//qry = @"update RVS_ITEMMASTER_WH set RATE='" + r["Rate"] + "' where ITEMCODE='" + r["ItemCode"] + "' AND WHID='" + ddlRecvWh.SelectedValue + "' AND LOCATIONID='" + ddlRecvLoc.SelectedValue + "'";
//db.ExecuteNonQuery(qry);
////End New