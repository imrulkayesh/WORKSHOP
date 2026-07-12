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

public partial class Pages_VISM_CoForm : BasePage
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
        //qry = @"select BUSINESSCENTERID from EM_EMPLOYEE where USERID='" + SessionManager.SessionName.UserId + "'";
        //ddlSendLoc.SelectedValue = dm.ExecuteScalar(qry);
        //ddlSendLoc_OnSelectedIndexChanged(null, null);

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
        DtDetailItem.Columns.Add("CategoryCode");
        DtDetailItem.Columns.Add("CategoryName");
        DtDetailItem.Columns.Add("ItemCode");
        DtDetailItem.Columns.Add("ItemName");
        DtDetailItem.Columns.Add("UnitCode");
        DtDetailItem.Columns.Add("UnitName");
        DtDetailItem.Columns.Add("Rate");
        DtDetailItem.Columns.Add("MARGINRATE");
        DtDetailItem.Columns.Add("Qty");
        DtDetailItem.Columns.Add("TotalAmount");
        DtDetailItem.Columns.Add("SRate");

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
            if (ddlItemName.SelectedValue != row["ItemCode"].ToString()) continue;
            MessageBox(ddlItemName.SelectedItem.Text + " is already added.");
            return;
        }
        string totAmt = (Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQty.Text)).ToString();
        tbxAmt.Text = totAmt;
        DtDetailItem.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlItemName.SelectedValue, ddlItemName.SelectedItem.Text.Substring(ddlItemName.SelectedItem.Text.IndexOf('-') + 1), tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxQty.Text.Trim(), tbxAmt.Text);

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


            if (DtDetailItem.Rows.Count == 0)
            {
                MessageBox("Please add item first.");
                return;
            }

            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='CO' RETURNING LASTNO INTO :RETVAL";
            string lno = dm.ExecuteScalar(qry, "RETVAL");
            db.BeginTransaction();
            const string trType = "CO";
            string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
            qry = @"INSERT INTO RVS_TRMASTER(ID,TRTYPE,REFTRID,SENDLOCATION,SENDWH,SENDCOMPANY,SENDDATE,SENDREMARKS,SENDUSER,
RECVLOCATION,RECVWH,RECVCOMPANY,ISCOMPLETE,IUSER,CATEGORYCODE) VALUES(
'" + trid + "','" + trType + "','" + ddlCdId.SelectedValue + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"',TO_DATE(SYSDATE,'DD/MM/YYYY'),'" + tbxSendRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + @"',
'" + ddlRecvLoc.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + ddlRecvCom.SelectedValue + "','1','" + SessionManager.SessionName.UserId + "','" + ddlCOcatagory.SelectedValue + "')";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailItem.Rows)
            {
                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                qry = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,
ITEMCODE,UNITID,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH,SRATE) values
('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["ItemCode"] + @"',
'" + r["UnitCode"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + ddlRecvWh.SelectedValue + "','" + r["SRate"] + @"')";
                db.ExecuteNonQuery(qry);
                i++;
            }

            foreach (DataRow r in DtDetailItem.Rows)
            {
                qry = @"update VISMF.RVS_JOBCARDITEMS set QUANTITY='" + r["Qty"] + "',TOTALAMOUNT='" + r["TotalAmount"] + "',COID='" + trid + "',SRATE='" + r["SRate"] + "' WHERE CDID='" + ddlCdId.SelectedValue + "' and ITEMCODE='" + r["ItemCode"] + "'";
                db.ExecuteNonQuery(qry);
                i++;
            }
            qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlCdId.SelectedValue + "' AND ISACTIVE=1";
            db.ExecuteNonQuery(qry);
            db.Commit();
            btnSave.Enabled = false;
            tbxCoId.Text = trid;
            tbxCoId.BackColor = Color.DarkCyan;
            tbxCoId.ForeColor = Color.White;
            tbxCoId.Enabled = true;
            tbxCoId.ReadOnly = true;
            ddlCdId.Enabled = false;
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


        try
        {


            string qry = @"update RVS_REQMASTER set ISACTIVE='0', ISCOMPLETE='99',STATUSID='99',UPDATEDBY='" + SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID='" + ddlCdId.SelectedValue + "'";
            dm.ExecuteScalar(qry);

            btnDelete.Enabled = false;
            tbxCoId.BackColor = Color.DarkCyan;
            tbxCoId.ForeColor = Color.White;
            tbxCoId.Enabled = true;
            tbxCoId.ReadOnly = true;
            ddlCdId.Enabled = false;
            MessageBox(DeleteSuccess);
            
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
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
        try
        {
            qry = @"select workshopid from em_employee where staffid='" + SessionManager.SessionName.UserId + "'";
            string wh = dm.ExecuteScalar(qry);
            ddlSendWh.SelectedValue = wh;
            ddlSendWh_OnSelectedIndexChanged(null, null);
        }
        catch
        {
            //
        }

    }

    protected void ddlSendWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
where wh.ID='" + ddlSendWh.SelectedValue + "'";
        DataTable val = dm.ExecuteQuery(qry);
        ListItem li = new ListItem(val.Rows[0]["TEXT"].ToString(), val.Rows[0]["ID"].ToString());
        ddlSendCom.DataSource = null;
        ddlSendCom.DataBind();
        ddlSendCom.Items.Insert(0, li);


        li = new ListItem("Select", "");
        qry = @"SELECT RQ.ID,RQ.ID||' - '||TO_CHAR(RQ.IDATE,'DD/MM/YYYY') TEXT FROM RVS_REQMASTER RQ
WHERE RQ.ISACTIVE=1 AND RQ.ISCOMPLETE=0 AND RQ.REQTYPE IN ('CD','FA') and RQ.STATUSID='1'
 AND RQ.SENDLOCATION='" + ddlSendLoc.SelectedValue + @"' 
 AND RQ.SENDWH='" + ddlSendWh.SelectedValue + @"' 
 AND RQ.SENDCOMPANY='" + ddlSendCom.SelectedValue + @"'
 AND TO_DATE(RQ.IDATE,'DD/MM/YY') BETWEEN (SELECT TO_DATE(SYSDATE-7,'DD/MM/YY') FROM DUAL) AND (SELECT TO_DATE(SYSDATE,'DD/MM/YY') FROM DUAL)
 ORDER BY RQ.IDATE Desc";
        ddlCdId.DataSource = dm.ExecuteQuery(qry);
        ddlCdId.DataBind();
        ddlCdId.Items.Insert(0, li);
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select ITEMCODE id,ITEMCODE||'-'||ITEMNAME text  from RVS_ITEMMASTER where isactive=1
 and ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "'order by ITEMNAME";
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

    protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        string SL = ddlSendLoc.SelectedValue;
        string SW = ddlSendWh.SelectedValue;

        //        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME, FROM RVS_ITEMMASTER IM
        //inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE where im.isactive=1 and IM.ITEMCODE='" + ddlItemName.SelectedValue + "'";

        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME,IW.RATE FROM RVS_ITEMMASTER IM
inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE 
INNER JOIN RVS_ITEMMASTER_WH IW ON IM.ITEMCODE=IW.ITEMCODE where im.isactive=1 AND IW.ISACTIVE='1' and IM.ITEMCODE='" + ddlItemName.SelectedValue + "' AND IW.LOCATIONID='" + ddlSendLoc.SelectedValue + "' AND IW.WHID='" + ddlSendWh.SelectedValue + "'";

        DataTable dt = dm.ExecuteQuery(qry);
        tbxUnitCode.Text = dt.Rows[0]["SCMUNITCODE"].ToString();
        hdnUnitName.Value = dt.Rows[0]["UNITNAME"].ToString();
        tbxRate.Text = dt.Rows[0]["RATE"].ToString();
        tbxQty.Text = "";
        tbxAmt.Text = "";
        tbxQty.Enabled = true;
    }

    protected void ddlCdId_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        //        string qry = @"SELECT RM.RECVLOCATION,LO.CENTERNAME, RM.RECVWH,WH.REGNO , RM.RECVCOMPANY,WS.COMPANYNAME ,RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,
        //RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,RD.RATE,RD.QTY,RD.TOTALAMT,RD.REMARKS,WH.MODELID,MO.MODELNAME,WH.CUSTOMERID,CU.CUSTOMERNAME,RM.CATEGORYCODE,CC.CATAGORYNAME CATAGORY FROM RVS_REQDETAIL RD
        //INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
        //INNER JOIN BS_BUSINESSCENTER LO ON RM.RECVLOCATION=LO.ID
        //INNER JOIN RVS_VEHICLE WH ON RM.RECVWH=WH.ID
        //INNER JOIN BS_COMPANY WS ON RM.RECVCOMPANY=WS.ID
        //INNER JOIN rvs_model MO on WH.MODELID=MO.ID
        //INNER JOIN RVS_CUSTOMER CU on WH.CUSTOMERID=CU.ID
        //LEFT OUTER JOIN RVS_COCATAGORY CC on RM.CATEGORYCODE=CC.ID
        //WHERE RD.REQMASTERID='" + ddlCdId.SelectedValue + @"'
        //ORDER BY RD.LINE";

        string qry = @"SELECT RM.RECVLOCATION,LO.CENTERNAME, RM.RECVWH,WH.REGNO, RM.RECVCOMPANY,WS.COMPANYNAME ,RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,
RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,RD.RATE,RD.SRATE,JI.MARGIN,JI.MARGINRATE, RD.QTY,RD.TOTALAMT,RD.REMARKS,WH.MODELID,MO.MODELNAME,WH.CUSTOMERID,CU.CUSTOMERNAME,
EM.EMPLOYEENAME RECEIVER, RM.CATEGORYCODE,CC.CATAGORYNAME CATAGORY FROM RVS_REQDETAIL RD
INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
LEFT outer JOIN RVS_JOBCARDITEMS JI ON RM.JOBID=JI.JOBID AND RD.ITEMCODE=JI.ITEMCODE AND RD.REQMASTERID=JI.CDID
INNER JOIN BS_BUSINESSCENTER LO ON RM.RECVLOCATION=LO.ID
INNER JOIN RVS_VEHICLE WH ON RM.RECVWH=WH.ID
INNER JOIN BS_COMPANY WS ON RM.RECVCOMPANY=WS.ID
INNER JOIN rvs_model MO on WH.MODELID=MO.ID
INNER JOIN RVS_CUSTOMER CU on WH.CUSTOMERID=CU.ID
LEFT OUTER JOIN RVS_COCATAGORY CC on RM.CATEGORYCODE=CC.ID
INNER JOIN VISMF.EM_EMPLOYEE EM ON EM.STAFFID=RM.RECEIVER
WHERE RD.REQMASTERID='" + ddlCdId.SelectedValue + @"'
ORDER BY RD.LINE";
        //DataTable dtd = dm.ExecuteQuery(qry);
        ////ListItem li = new ListItem("Select", "");
        //ListItem li = new ListItem(dtd.Rows[0]["CENTERNAME"].ToString(), dtd.Rows[0]["RECVLOCATION"].ToString());
        ////li=new ListItem(dtd.Rows[0]["CENTERNAME"].ToString(), dtd.Rows[0]["RECVLOCATION"].ToString());
        //ddlRecvLoc.DataSource = null;
        //ddlRecvLoc.DataBind();
        //ddlRecvLoc.Items.Insert(0,li);

        //li = new ListItem(dtd.Rows[0]["REGNO"].ToString(), dtd.Rows[0]["RECVWH"].ToString());
        //ddlRecvWh.DataSource = null;
        //ddlRecvWh.DataBind();
        //ddlRecvWh.Items.Insert(0,li);

        //li=new ListItem(dtd.Rows[0]["COMPANYNAME"].ToString(), dtd.Rows[0]["RECVCOMPANY"].ToString());
        //ddlRecvCom.DataSource = null;
        //ddlRecvCom.DataBind();
        //ddlRecvCom.Items.Insert(0,li);

        DataTable dtd = dm.ExecuteQuery(qry);
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(dtd.Rows[0]["RECVLOCATION"].ToString(), dtd.Rows[0]["CENTERNAME"].ToString());
        ddlRecvLoc.DataSource = dt;
        ddlRecvLoc.DataBind();
        dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["RECVWH"].ToString(), dtd.Rows[0]["REGNO"].ToString());
        ddlRecvWh.DataSource = dt;
        ddlRecvWh.DataBind();
        dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["RECVCOMPANY"].ToString(), dtd.Rows[0]["COMPANYNAME"].ToString());
        ddlRecvCom.DataSource = dt;
        ddlRecvCom.DataBind();
        dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["CUSTOMERID"].ToString(), dtd.Rows[0]["CUSTOMERNAME"].ToString());
        ddlRecvCustomer.DataSource = dt;
        ddlRecvCustomer.DataBind();
        dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["MODELID"].ToString(), dtd.Rows[0]["MODELNAME"].ToString());
        ddlRecvModel.DataSource = dt;
        ddlRecvModel.DataBind();
        dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["CATEGORYCODE"].ToString(), dtd.Rows[0]["CATAGORY"].ToString());
        ddlCOcatagory.DataSource = dt;
        ddlCOcatagory.DataBind();
        dt.Clear();

        tbxReceiver.Text = dtd.Rows[0]["RECEIVER"].ToString();



        DtDetailItem.Clear();
        foreach (DataRow r in dtd.Rows)
        {
            DtDetailItem.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
                r["UNITID"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["MARGINRATE"].ToString(), r["QTY"].ToString(),
                r["TOTALAMT"].ToString(), r["SRATE"].ToString());
        }
        BindRpt();

        ListItem li = new ListItem("Select", "");
        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);

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
            tbxMarginRate.Text = rows["MARGINRATE"].ToString();
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
        DataRow rows = DtDetailItem.Select("ItemCode = '" + (string)ViewState["ID"] + "'").FirstOrDefault();
        var a = decimal.Parse(rows["Qty"].ToString());
        if (decimal.Parse(tbxQty.Text)< decimal.Parse(rows["Qty"].ToString())) {
            rows["Qty"] = tbxQty.Text;
            rows["TotalAmount"] = tbxAmt.Text;
        }
        
        BindRpt();
        ddlCategory.Enabled = true;
        ddlItemName.SelectedIndex = 0;
        ddlItemName.Enabled = true;
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        tbxRate.Text = "";
        tbxMarginRate.Text = "";
        tbxUnitCode.Text = "";
        tbxQty.Text = "";
        tbxAmt.Text = "";
        tbxQty.Enabled = false;
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
    public static List<string> SearchTr(string prefixText, int count)
    {

        string BI = SessionManager.SessionName.BusinessCentetid.ToString();

        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                //                cmd.CommandText = @"SELECT  RM.ID||' - '||VH.REGNO||' - '||WH.VISMWHNAME||' - '||RM.IDATE||' - '||em.EMPLOYEENAME||' - '||replace(replace(rm.iscomplete,'0','No CO'),'1','CO done')||'-'||tm.ID||'-'||emc.EMPLOYEENAME CD 
                //FROM RVS_REQMASTER RM
                //INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
                //INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
                //inner join EM_EMPLOYEE em on rm.IUSER=em.USERID
                //left join rvs_trmaster tm on tm.REFTRID=rm.id
                //left join EM_EMPLOYEE emc on tm.IUSER=emc.USERID
                //where rm.id like '%" + prefixText + "' or lower(vh.REGNO) like lower('%" + prefixText + "') or lower(emc.EMPLOYEENAME) like lower('%" + prefixText + @"%') AND ROWNUM <= 20 and RM.REQTYPE='CD' and RM.ISCOMPLETE='1' and RM.SENDLOCATION='"+BI+"' ORDER BY RM.IDATE DESC";

                cmd.CommandText = @"SELECT  RM.ID||' - '||VH.REGNO||' - '||WH.VISMWHNAME||' - '||RM.IDATE||' - '||em.EMPLOYEENAME||' - '||replace(replace(rm.iscomplete,'0','No CO'),'1','CO done')||'-'||tm.ID||'-'||emc.EMPLOYEENAME CD 
FROM RVS_REQMASTER RM
INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
inner join EM_EMPLOYEE em on rm.IUSER=em.USERID
left join rvs_trmaster tm on tm.REFTRID=rm.id
left join EM_EMPLOYEE emc on tm.IUSER=emc.USERID
where rm.id like '%" + prefixText + "' or lower(vh.REGNO) like lower('%" + prefixText + "') AND ROWNUM <= 20 and RM.REQTYPE='CD' and RM.ISCOMPLETE='1' and RM.SENDLOCATION='" + BI + "' ORDER BY RM.IDATE DESC";

                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["CD"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }
}