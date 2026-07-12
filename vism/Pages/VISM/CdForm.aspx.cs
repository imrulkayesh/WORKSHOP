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

public partial class Pages_VISM_CdForm : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.DefaultButton = btnAdd.UniqueID;
        if (!IsPostBack)
        {

            LoadDdl();
            DtDetailAddColumn();
            BindRpt();
            tbxRecVeh.Focus();
            //LoadCategory();
        }

    }

    //private void LoadCategory()
    //{

    //    string qry = @"SELECT V.COMPANYID FROM RVS_VEHICLE V WHERE V.ID='11074'";
    //    DataTable dt = dm.ExecuteQuery(qry);
    //    tbxRecVeh.Text = dt.Rows[0]["VH"].ToString();


    //    ListItem li = new ListItem("Select", "");
    //    qry =
    //       @"select ID,CATAGORYNAME text  from RVS_COCATAGORY";
    //    ddlCOcatagory.DataSource = dm.ExecuteQuery(qry);
    //    ddlCOcatagory.DataBind();
    //    ddlCOcatagory.Items.Insert(0, li);

    //    ddlCOcatagory.Enabled = true;
    //}

    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "");

        qry = @"SELECT TM.STAFFID id,TM.STAFFID||'_'||T.TEAMNAME ||'_'||E.EMPLOYEENAME text,'' REMARKS FROM RVS_TEAMMEMBER TM
        INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
        INNER JOIN EM_EMPLOYEE ES ON ES.BUSINESSCENTERID=E.BUSINESSCENTERID AND ES.STAFFID='" + SessionManager.SessionName.StaffId + "'   WHERE TM.ISACTIVE=1";
        ddlTech.DataSource = dm.ExecuteQuery(qry);
        //ddlTech.DataSource = obj.GetTechnicianByWorkShop();
        ddlTech.DataBind();
        ddlTech.Items.Insert(0, new ListItem("Select", ""));





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
        DtDetailItem.Columns.Add("Margin");
        DtDetailItem.Columns.Add("MarginRate");
        DtDetailItem.Columns.Add("Qty");
        DtDetailItem.Columns.Add("TotalAmount");
        DtDetailItem.Columns.Add("SalesRate");

    }
    private void BindRpt()
    {
        DtDetailItem.AcceptChanges();
        rpt.DataSource = DtDetailItem;
        rpt.DataBind();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        if (tbxRate.Text == "0")
        {
            MessageBox("Please update Item Rate");
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxRate').focus();", true);
        }
        else
        {

            foreach (DataRow row in DtDetailItem.Rows)
            {
                if (tbxItem.Text.Split('-')[0].Trim() != row["ItemCode"].ToString()) continue;
                MessageBox(tbxItem.Text + " is already added.");
                return;
            }
            string totAmt = (Convert.ToDecimal(tbxMarginRate.Text) * Convert.ToDecimal(tbxQty.Text)).ToString();
            tbxAmt.Text = totAmt;
            DtDetailItem.Rows.Add(tbxCategory.Text, tbxCategoryName.Text, tbxItem.Text.Split('-')[0].Trim(), tbxItem.Text.Trim().Substring(tbxItem.Text.IndexOf('-') + 1).Trim(), tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxMargin.Text, tbxMarginRate.Text, tbxQty.Text.Trim(), tbxAmt.Text, tbxSRate.Text);
            BindRpt();
            tbxItem.Text = "";
            tbxUnitCode.Text = "";
            tbxRate.Text = "";
            tbxMargin.Text = "";
            tbxMarginRate.Text = "";
            tbxQty.Text = "";
            tbxAmt.Text = "";
            tbxSRate.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxItem').focus();", true);
        }

    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (ddlTech.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
                  "<script language='javascript'>alert('Please select Receiver !!!');</script>", false);
                return;
            }

            string vid = tbxRecVeh.Text.Split('-')[0];

            string qry1 = @"SELECT V.COMPANYID cid FROM RVS_VEHICLE V WHERE V.ID='" + vid + "'";
            DataTable dt1 = dm.ExecuteQuery(qry1);
            int cid = Convert.ToInt32(dt1.Rows[0]["cid"].ToString());

            if (cid == 461)
            {
                string qry = @"SELECT SQ_RVS_JOBCARD.NEXTVAL from dual";
                string jobid = dm.ExecuteScalar(qry);


                if (DtDetailItem.Rows.Count == 0)
                {
                    MessageBox("Please add item first.");
                    return;
                }
                qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='CD' RETURNING LASTNO INTO :RETVAL";
                string lno = dm.ExecuteScalar(qry, "RETVAL");
                db.BeginTransaction();
                const string trType = "CD";
                string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
                qry = @"INSERT INTO RVS_REQMASTER(ID,REQTYPE,SENDLOCATION,SENDWH,SENDCOMPANY,
RECVLOCATION,RECVWH,RECVCOMPANY,RECVREMARKS,IUSER,JOBID,CATEGORYCODE,RECEIVER) VALUES(
'" + trid + "','" + trType + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"',
'" + ddlRecvLoc.SelectedValue + "','" + tbxRecVeh.Text.Split('-')[0] + "','" + ddlRecvCom.SelectedValue + "','" + tbxRecRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + "','" + jobid + "','" + ddlCOcatagory.SelectedValue + "','"+ddlTech.SelectedValue+"')";
                db.ExecuteNonQuery(qry);


                qry1 = @"Insert into RVS_JOBCARD(ID,VEHICLEID,WORKSHOPID)
        VALUES('" + jobid + "','" + tbxRecVeh.Text.Split('-')[0] + "','" + ddlSendWh.SelectedValue + "')";
                db.ExecuteNonQuery(qry1);


                int i = 1;
                foreach (DataRow r in DtDetailItem.Rows)
                {
                    string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;

                    string itemName = r["ItemName"].ToString();

                    qry = @"INSERT INTO RVS_REQDETAIL (ID,REQMASTERID,ITEMCATEGORYCODE,ITEMCATEGORYNAME,
ITEMCODE,ITEMNAME,UNITID,UNITNAME,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH,SRATE) values
('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["CategoryName"] + "','" + r["ItemCode"] + @"',
'" + itemName.Trim().Replace("'","''") + "','" + r["UnitCode"] + "','" + r["UnitName"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + tbxRecVeh.Text.Split('-')[0] + "','" + r["SalesRate"] + @"')";
                    db.ExecuteNonQuery(qry);

                    qry = @"insert into RVS_JOBCARDITEMS (JOBID,PARTSID,QUANTITY,ITEMCODE,RATE,MARGIN,MARGINRATE,TOTALAMOUNT,MARGINAMOUNT,LINE,AMOUNT,CDID,SRATE) values
('" + jobid + "','0','" + r["Qty"] + "','" + r["ItemCode"] + "','" + r["Rate"] +
                          "','" + r["Margin"] + "','" + r["MarginRate"] + "','" + r["TotalAmount"] + "','" + r["TotalAmount"] + "','" + i + "','0','" + trid + "','" + r["SalesRate"] + "')";
                    db.ExecuteNonQuery(qry);
                    qry = @"update RVS_BILL set TOTALBILL=TOTALBILL+'" + r["TotalAmount"] + "' where jobid='" + jobid + "'";
                    db.ExecuteNonQuery(qry);

                    i++;
                }
                db.Commit();
                btnSave.Enabled = false;
                tbxDrId.Text = trid;
                tbxDrId.BackColor = Color.DarkCyan;
                tbxDrId.ForeColor = Color.White;
                tbxDrId.Enabled = true;
                tbxDrId.ReadOnly = true;
                MessageBox(SaveSuccess);
                ViewState["TrId"] = trid;
            }

            else
            {

                string qry = @"SELECT DISTINCT ID FROM RVS_JOBCARD JC
WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";
                string jobid = dm.ExecuteScalar(qry);
                if (string.IsNullOrWhiteSpace(jobid))
                {
                    MessageBox("No job card found of this vehicle.");
                    return;
                }

                if (DtDetailItem.Rows.Count == 0)
                {
                    MessageBox("Please add item first.");
                    return;
                }
                qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='CD' RETURNING LASTNO INTO :RETVAL";
                string lno = dm.ExecuteScalar(qry, "RETVAL");
                db.BeginTransaction();
                const string trType = "CD";
                string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
                qry = @"INSERT INTO RVS_REQMASTER(ID,REQTYPE,SENDLOCATION,SENDWH,SENDCOMPANY,
RECVLOCATION,RECVWH,RECVCOMPANY,RECVREMARKS,IUSER,JOBID,CATEGORYCODE,RECEIVER) VALUES(
'" + trid + "','" + trType + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"',
'" + ddlRecvLoc.SelectedValue + "','" + tbxRecVeh.Text.Split('-')[0] + "','" + ddlRecvCom.SelectedValue + "','" + tbxRecRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + "','" + jobid + "','" + ddlCOcatagory.SelectedValue + "','"+ ddlTech.SelectedValue +"')";
                db.ExecuteNonQuery(qry);
                int i = 1;
                foreach (DataRow r in DtDetailItem.Rows)
                {
                    string itemname = r["ItemName"].ToString();

                    string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                    qry = @"INSERT INTO RVS_REQDETAIL (ID,REQMASTERID,ITEMCATEGORYCODE,ITEMCATEGORYNAME,
ITEMCODE,ITEMNAME,UNITID,UNITNAME,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH,SRATE) values
('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["CategoryName"] + "','" + r["ItemCode"] + @"',
'" + itemname.Trim().Replace("'", "''") + "','" + r["UnitCode"] + "','" + r["UnitName"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + tbxRecVeh.Text.Split('-')[0] + "','" + r["SalesRate"] + @"')";
                    db.ExecuteNonQuery(qry);

                    qry = @"insert into RVS_JOBCARDITEMS (JOBID,PARTSID,QUANTITY,ITEMCODE,RATE,MARGIN,MARGINRATE,TOTALAMOUNT,MARGINAMOUNT,LINE,AMOUNT,CDID,SRATE) values
('" + jobid + "','0','" + r["Qty"] + "','" + r["ItemCode"] + "','" + r["Rate"] +
                          "','" + r["Margin"] + "','" + r["MarginRate"] + "','" + r["TotalAmount"] + "','" + r["TotalAmount"] + "','" + i + "','0','" + trid + "','" + r["SalesRate"] + "')";
                    db.ExecuteNonQuery(qry);
                    qry = @"update RVS_BILL set TOTALBILL=TOTALBILL+'" + r["TotalAmount"] + "' where jobid='" + jobid + "'";
                    db.ExecuteNonQuery(qry);

                    i++;
                }
                db.Commit();
                btnSave.Enabled = false;
                tbxDrId.Text = trid;
                tbxDrId.BackColor = Color.DarkCyan;
                tbxDrId.ForeColor = Color.White;
                tbxDrId.Enabled = true;
                tbxDrId.ReadOnly = true;
                MessageBox(SaveSuccess);
                ViewState["TrId"] = trid;
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
        DtDetailItem.Clear();
        DtDetailItem.AcceptChanges();
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





    protected void ddlRecvWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
where wh.ID='" + tbxRecVeh.Text + "'";
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


        //if (ddlRecvLoc.SelectedValue == ddlSendLoc.SelectedValue)
        //{
        //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("disabled", "disabled");
        //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");

        //}

        //$('#btnRefresh').trigger('click');
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
        ScriptManager.RegisterStartupScript(this, GetType(), "focusewe", "$('body,html').animate({scrollTop: 310}, 1500);", true);

        //ScriptManager.RegisterStartupScript(this, GetType(), "focusewe", "window.scrollTo(0, 500);", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxItem').focus();", true);
        //ListItem li = new ListItem("Select", "");
        //qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        //ddlCategory.DataSource = dm.ExecuteQuery(qry);
        //ddlCategory.DataBind();
        //ddlCategory.Items.Insert(0, li);
    }
    //   protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    //   {
    //       ListItem li = new ListItem("Select", "");
    //       string qry = @"select ITEMCODE id,ITEMCODE||'-'||ITEMNAME text  from RVS_ITEMMASTER where isactive=1
    //and ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "'order by ITEMNAME";
    //       ddlItemName.DataSource = dm.ExecuteQuery(qry);
    //       ddlItemName.DataBind();
    //       ddlItemName.Items.Insert(0, li);
    //       foreach (DataRow r in DtDetailItem.Rows)
    //       {
    //           foreach (ListItem ltItem in ddlItemName.Items)
    //           {
    //               if (ltItem.Value == r["ItemCode"].ToString())
    //               {
    //                   ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("disabled", "disabled");
    //                   ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");
    //               }
    //           }

    //       }

    //   }

    //    protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME,IM.RATE,IM.MARGIN FROM RVS_ITEMMASTER IM
    //inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE where im.isactive=1 and IM.ITEMCODE='" + tbxItem.Text.Split('-')[0].Trim() + "'";
    //        DataTable dt = dm.ExecuteQuery(qry);
    //        tbxUnitCode.Text = dt.Rows[0]["SCMUNITCODE"].ToString();
    //        hdnUnitName.Value = dt.Rows[0]["UNITNAME"].ToString();
    //        tbxRate.Text = dt.Rows[0]["RATE"].ToString();
    //        tbxMargin.Text = dt.Rows[0]["MARGIN"].ToString();
    //        tbxMarginRate.Text = (Convert.ToDecimal(dt.Rows[0]["RATE"].ToString()) + Convert.ToDecimal(dt.Rows[0]["RATE"].ToString()) * Convert.ToDecimal(dt.Rows[0]["MARGIN"].ToString()) / 100).ToString("F2");
    //        tbxQty.Text = "";
    //        tbxAmt.Text = "";
    //        ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxQty').focus();", true);
    //    }

    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    {


        string vid = tbxRecVeh.Text.Split('-')[0];

        string qry1 = @"SELECT V.COMPANYID cid FROM RVS_VEHICLE V WHERE V.ID='" + vid + "'";
        DataTable dt1 = dm.ExecuteQuery(qry1);
        int cid = Convert.ToInt32(dt1.Rows[0]["cid"].ToString());

        if (cid == 461)
        {
            string qry2 =
               @"select ID,CATAGORYNAME text  from RVS_COCATAGORY WHERE ID='0'";
            ddlCOcatagory.DataSource = dm.ExecuteQuery(qry2);
            ddlCOcatagory.DataBind();
            ddlCOcatagory.Enabled = true;


            string qry = @"SELECT VH.BUSINESSCENTERID ID,LO.CENTERNAME TEXT,VH.COMPANYID CID,CO.COMPANYNAME CTEXT,VH.MODELID,MO.MODELNAME,VH.CUSTOMERID,CU.CUSTOMERNAME FROM RVS_VEHICLE VH
INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
INNER JOIN rvs_model MO on VH.MODELID=MO.ID
INNER JOIN RVS_CUSTOMER CU on VH.CUSTOMERID=CU.ID
WHERE VH.ID='" + tbxRecVeh.Text.Split('-')[0] + "'";

            DataTable dt = dm.ExecuteQuery(qry);
            ListItem li = new ListItem(dt.Rows[0]["TEXT"].ToString(), dt.Rows[0]["ID"].ToString());
            ddlRecvLoc.Items.Clear();
            //ddlRecvLoc.DataSource = null;
            //ddlRecvLoc.DataBind();
            ddlRecvLoc.Items.Insert(0, li);
            li = new ListItem(dt.Rows[0]["CTEXT"].ToString(), dt.Rows[0]["CID"].ToString());
            ddlRecvCom.Items.Clear();
            //ddlRecvCom.DataSource = null;
            //ddlRecvCom.DataBind();
            ddlRecvCom.Items.Insert(0, li);

            li = new ListItem(dt.Rows[0]["MODELNAME"].ToString(), dt.Rows[0]["MODELID"].ToString());
            ddlRecvModel.Items.Clear();
            ddlRecvModel.Items.Insert(0, li);

            li = new ListItem(dt.Rows[0]["CUSTOMERNAME"].ToString(), dt.Rows[0]["CUSTOMERID"].ToString());
            ddlRecvCustomer.Items.Clear();
            ddlRecvCustomer.Items.Insert(0, li);

            ddlSendLoc.Enabled = false;
            li = new ListItem("Select", "");
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
            ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
            ddlSendLoc.DataBind();
            ddlSendLoc.Items.Insert(0, li);

            string BI = SessionManager.SessionName.BusinessCentetid.ToString();

            if (BI != "2")
            {
                qry = @"select BUSINESSCENTERID from EM_EMPLOYEE where USERID='" + SessionManager.SessionName.UserId + "'";
                ddlSendLoc.SelectedValue = dm.ExecuteScalar(qry);
                ddlSendLoc_OnSelectedIndexChanged(null, null);
            }

            else
            {
                //                    qry3 = @"SELECT DISTINCT ID FROM RVS_JOBCARD JC
                //WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";
            }
        }

        else
        {

            string BI = SessionManager.SessionName.BusinessCentetid.ToString();

            string qry3 = "";

            if (BI != "2")
            {
                qry3 = @"SELECT DISTINCT JC.ID FROM RVS_JOBCARD JC, RVS_WORKSHOP WS
WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' and WS.BUSINESSCENTERID ='" + BI + @"' 
AND JC.WORKSHOPID=WS.ID AND JC.ISOPEN=1 AND JC.ISACTIVE=1";
            }

            else
            {
                qry3 = @"SELECT DISTINCT ID FROM RVS_JOBCARD JC
WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";
            }


            string jobid = dm.ExecuteScalar(qry3);
            if (string.IsNullOrWhiteSpace(jobid))
            {
                MessageBox("There is no job card open for this vehicle in your workshop.");
                tbxRecVeh.Attributes.Add("onfocus", "javascript:this.select();");
                tbxRecVeh.Focus();
                return;
            }

            else
            {
                ListItem lii = new ListItem("Select", "");
                string qry2 =
                   @"select ID,CATAGORYNAME text  from RVS_COCATAGORY";
                //ddlCOcatagory.DataSource = dm.ExecuteQuery(qry2);
                //ddlCOcatagory.DataBind();
                //ddlCOcatagory.Items.Insert(0, lii);
                //ddlCOcatagory.Enabled = true;

                //new code for default value show start
                ddlCOcatagory.DataSource = dm.ExecuteQuery(qry2);
                ddlCOcatagory.DataValueField = "ID";
                ddlCOcatagory.DataTextField = "TEXT";
                ddlCOcatagory.DataBind();
                ddlCOcatagory.Items.Insert(0, lii);
                string defaultValue = "0"; 
                ListItem defaultItem = ddlCOcatagory.Items.FindByValue(defaultValue);
                if (defaultItem != null)
                {
                    ddlCOcatagory.SelectedValue = defaultValue;
                }
                ddlCOcatagory.Enabled = true;
                //default value show end

                string qry = @"SELECT VH.BUSINESSCENTERID ID,LO.CENTERNAME TEXT,VH.COMPANYID CID,CO.COMPANYNAME CTEXT,VH.MODELID,MO.MODELNAME,VH.CUSTOMERID,CU.CUSTOMERNAME FROM RVS_VEHICLE VH
INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
INNER JOIN rvs_model MO on VH.MODELID=MO.ID
INNER JOIN RVS_CUSTOMER CU on VH.CUSTOMERID=CU.ID
WHERE VH.ID='" + tbxRecVeh.Text.Split('-')[0] + "'";

                DataTable dt = dm.ExecuteQuery(qry);
                ListItem li = new ListItem(dt.Rows[0]["TEXT"].ToString(), dt.Rows[0]["ID"].ToString());
                ddlRecvLoc.Items.Clear();
                //ddlRecvLoc.DataSource = null;
                //ddlRecvLoc.DataBind();
                ddlRecvLoc.Items.Insert(0, li);
                li = new ListItem(dt.Rows[0]["CTEXT"].ToString(), dt.Rows[0]["CID"].ToString());
                ddlRecvCom.Items.Clear();
                //ddlRecvCom.DataSource = null;
                //ddlRecvCom.DataBind();
                ddlRecvCom.Items.Insert(0, li);

                li = new ListItem(dt.Rows[0]["MODELNAME"].ToString(), dt.Rows[0]["MODELID"].ToString());
                ddlRecvModel.Items.Clear();
                ddlRecvModel.Items.Insert(0, li);

                li = new ListItem(dt.Rows[0]["CUSTOMERNAME"].ToString(), dt.Rows[0]["CUSTOMERID"].ToString());
                ddlRecvCustomer.Items.Clear();
                ddlRecvCustomer.Items.Insert(0, li);

                ddlSendLoc.Enabled = false;
                li = new ListItem("Select", "");
                qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
                ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
                ddlSendLoc.DataBind();
                ddlSendLoc.Items.Insert(0, li);

            

                if (BI != "2")
                {
                    qry = @"select BUSINESSCENTERID from EM_EMPLOYEE where USERID='" + SessionManager.SessionName.UserId + "'";
                    ddlSendLoc.SelectedValue = dm.ExecuteScalar(qry);
                    ddlSendLoc_OnSelectedIndexChanged(null, null);
                }

                else
                {
//                    qry3 = @"SELECT DISTINCT ID FROM RVS_JOBCARD JC
//WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";
                }


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

    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        DataRow rows = DtDetailItem.Select("ItemCode = '" + (string)ViewState["ID"] + "'").FirstOrDefault();
        if (rows != null)
            rows["Margin"] = tbxMargin.Text;
        rows["MarginRate"] = tbxMarginRate.Text;
        rows["Qty"] = tbxQty.Text;
        rows["TotalAmount"] = tbxAmt.Text;
        BindRpt();
        //ddlCategory.Enabled = true;
        tbxItem.Enabled = true;
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        tbxItem.Text = "";
        tbxUnitCode.Text = "";
        tbxRate.Text = "";
        tbxMargin.Text = "";
        tbxMarginRate.Text = "";
        tbxQty.Text = "";
        tbxAmt.Text = "";
        ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxItem').focus();", true);
        //tbxQty.Enabled = false;
        //foreach (DataRow r in DtDetailItem.Rows)
        //{
        //    foreach (ListItem ltItem in ddlItemName.Items)
        //    {
        //        if (ltItem.Value == r["ItemCode"].ToString())
        //        {
        //            ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("disabled", "disabled");
        //            ddlItemName.Items.FindByValue(r["ItemCode"].ToString()).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");
        //        }
        //    }

        //}
    }

    protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            DataRow rows = DtDetailItem.Select("ItemCode = '" + e.CommandArgument + "'").FirstOrDefault();
            tbxCategory.Text = rows["CategoryCode"].ToString();
            tbxCategoryName.Text = rows["CategoryName"].ToString();
            tbxItem.Text = e.CommandArgument.ToString() + " - " + rows["ItemName"].ToString();
            tbxUnitCode.Text = rows["UnitCode"].ToString();
            tbxRate.Text = rows["Rate"].ToString();
            tbxQty.Text = rows["Qty"].ToString();
            tbxMargin.Text = rows["Margin"].ToString();
            tbxMarginRate.Text = rows["MarginRate"].ToString();
            tbxAmt.Text = rows["TotalAmount"].ToString();
            tbxSRate.Text = rows["SalesRate"].ToString();
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            //ddlCategory.Enabled = false;
            tbxItem.Enabled = false;
            //tbxQty.Enabled = true;
            ViewState["ID"] = e.CommandArgument.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    protected void btnCdPrint_OnClick(object sender, EventArgs e)
    {
        if (DtDetailItem.Rows.Count == 0 || tbxDrId.Text == "")
        {
            MessageBox("No CD found to print.");
            return;
        }


        string TrnNo = ViewState["TrId"].ToString();
        //string lnk = "PrintCD.aspx?TrnNo=" + TrnNo.ToString();
        //ScriptManager.RegisterStartupScript(this, GetType(), "openInNewTab", "window.open('" + lnk + "', '_blank');", true);
        string qry = @"SELECT 'CD No: '|| RM.ID CDNO,'CD Date: '||to_char(RM.IDATE,'DD/MM/YYYY HH:MI AM') CDDATE,LO.CENTERNAME FROMLOC,WH.VISMWHNAME FROMWH,WS.WORKSHOP FROMCOM,RM.IUSER||' - '||EM.EMPLOYEENAME CREATEDBY,
        LOT.CENTERNAME TOLOC, VH.REGNO TOWH,CO.COMPANYNAME TOCOM,RM.RECVREMARKS SENDREMARKS,
        IM.ITEMCODE,IM.ITEMNAME,UT.UNITNAME,RD.QTY,RD.REMARKS,'Consumption Demand (CD)' RTITLE,'*'||RM.ID||'*' REQUESTNUMBERXXX,MO.MODELNAME,CU.CUSTOMERNAME,CC.CATAGORYNAME CATAGORY FROM RVS_REQDETAIL RD
        INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
        INNER JOIN RVS_ITEMMASTER IM ON RD.ITEMCODE=IM.ITEMCODE
        INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE =UT.UNITCODE
        INNER JOIN RVS_JOBCARD JC ON RM.JOBID=JC.ID
        INNER JOIN BS_BUSINESSCENTER LO ON RM.SENDLOCATION=LO.ID
        INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
        INNER JOIN RVS_WORKSHOP WS ON RM.SENDCOMPANY=WS.ID
        INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
        INNER JOIN BS_BUSINESSCENTER LOT ON VH.BUSINESSCENTERID=LOT.ID
        INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
        INNER JOIN EM_EMPLOYEE EM ON RM.IUSER=EM.STAFFID
        INNER JOIN rvs_model MO on VH.MODELID=MO.ID
        INNER JOIN RVS_CUSTOMER CU on VH.CUSTOMERID=CU.ID
        LEFT OUTER JOIN RVS_COCATAGORY CC on RM.CATEGORYCODE=CC.ID
        WHERE RM.ID='" + (string)ViewState["TrId"] + @"'
        ORDER BY RD.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/pages/VISM/CrReport/CrCdPrint.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);

    }

    //protected void Page_Unload(object sender, EventArgs e)

    //{

    //    CrDrPrint c = new CrDrPrint();

    //    if (c != null)

    //    {

    //        c.Close();

    //        c.Dispose();

    //    }

    //}

    protected void btnSearchTr_OnClick(object sender, EventArgs e)
    {
        string trid = tbxTrNo.Text.Split('-')[0].Trim();
        ViewState["TrId"] = trid;
        string qry = @"SELECT RM.RECVWH||'-' ||VH.REGNO VH,RM.RECVREMARKS, RM.SENDLOCATION,RM.SENDWH,RM.ID CDID,
RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,
RD.RATE,RD.MARGIN,RD.RATE+RD.RATE*RD.MARGIN/100 MARGINRATE,RD.QTY,(RD.RATE+RD.RATE*RD.MARGIN/100)*RD.QTY TOTALAMOUNTT
FROM RVS_REQDETAIL RD
INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
INNER JOIN EM_EMPLOYEE EM ON RM.IUSER=EM.USERID
WHERE RM.ID = '" + trid + @"'
ORDER BY RD.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        tbxRecVeh.Text = dt.Rows[0]["VH"].ToString();
        tbxRecVeh_OnTextChanged(null, null);
        ddlSendWh.SelectedValue = dt.Rows[0]["SENDWH"].ToString();
        ddlSendWh_OnSelectedIndexChanged(null, null);
        tbxDrId.Text = dt.Rows[0]["CDID"].ToString();
        btnSave.Enabled = false;
        tbxDrId.BackColor = Color.DarkCyan;
        tbxDrId.ForeColor = Color.White;
        tbxDrId.Enabled = true;
        tbxDrId.ReadOnly = true;
        tbxRecVeh.ReadOnly = true;
        tbxRecRemarks.ReadOnly = true;
        ddlSendLoc.Enabled = false;
        ddlSendWh.Enabled = false;
        tbxItem.Enabled = false;
        btnAdd.Enabled = false;
        pnlAddItem.Visible = false;
        DtDetailItem.Rows.Clear();
        foreach (DataRow r in dt.Rows)
        {
            DtDetailItem.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(), r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(), r["UNITID"].ToString(), r["ITEMNAME"].ToString(), r["RATE"].ToString(), r["MARGIN"].ToString(), r["MARGINRATE"].ToString(), r["QTY"].ToString(), r["TOTALAMOUNTT"].ToString());
        }
        BindRpt();
    }

    protected void tbxItem_OnTextChanged(object sender, EventArgs e)
    {
        string SL = ddlSendLoc.SelectedValue;
        string SW = ddlSendWh.SelectedValue;

        //        string qry = @"SELECT IM.SCMUNITCODE,UT.UNITNAME,IM.RATE,IM.MARGIN,IM.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME FROM RVS_ITEMMASTER IM
        //INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE=UT.UNITCODE
        //INNER JOIN RVS_ITEMCATEGORY CT ON IM.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE WHERE IM.ISACTIVE=1 AND IM.ITEMCODE='" + tbxItem.Text.Split('-')[0] + "'";

        string qry = @"SELECT IM.SCMUNITCODE,UT.UNITNAME,IW.RATE,IM.MARGIN,IM.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME,IW.SALESRATE FROM RVS_ITEMMASTER IM
INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE=UT.UNITCODE
INNER JOIN RVS_ITEMCATEGORY CT ON IM.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE 
INNER JOIN RVS_ITEMMASTER_WH IW ON IM.ITEMCODE=IW.ITEMCODE WHERE IM.ISACTIVE=1 AND IW.ISACTIVE='1' AND IM.ITEMCODE='" + tbxItem.Text.Split('-')[0] + "' AND IW.LOCATIONID='" + ddlSendLoc.SelectedValue + "' AND IW.WHID='" + ddlSendWh.SelectedValue + "'";


        DataTable dt = dm.ExecuteQuery(qry);

        if (dt.Rows.Count > 0)
        {

            tbxCategory.Text = dt.Rows[0]["ITEMCATEGORYCODE"].ToString();
            tbxCategoryName.Text = dt.Rows[0]["ITEMCATEGORYNAME"].ToString();
            tbxUnitCode.Text = dt.Rows[0]["SCMUNITCODE"].ToString();
            hdnUnitName.Value = dt.Rows[0]["UNITNAME"].ToString();
            tbxRate.Text = dt.Rows[0]["RATE"].ToString();
            tbxSRate.Text = dt.Rows[0]["SALESRATE"].ToString();
            tbxMargin.Text = dt.Rows[0]["MARGIN"].ToString();
            tbxMarginRate.Text = (Convert.ToDecimal(dt.Rows[0]["RATE"].ToString()) + Convert.ToDecimal(dt.Rows[0]["RATE"].ToString()) * Convert.ToDecimal(dt.Rows[0]["MARGIN"].ToString()) / 100).ToString("F2");
            tbxQty.Text = "";
            tbxAmt.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxQty').focus();", true);
        }

        else
        {
            MessageBox("Item not Available");
            tbxItem.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxItem').focus();", true);

            return;
        }


    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchItem(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT V.ITEMCODE||'-'||V.ITEMNAME ITEM
FROM RVS_itemmaster V
WHERE (V.ITEMCODE LIKE '" + prefixText + "%' OR LOWER(V.ITEMNAME) LIKE LOWER('%" + prefixText + "%'))  AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["ITEM"].ToString());
                    }
                }
                conn.Close();
                return items;
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
                //                cmd.CommandText = @"SELECT  RM.ID||' - '||VH.REGNO||' - '||WH.VISMWHNAME||' - '||RM.IDATE||' - '||em.EMPLOYEENAME||' - '||replace(replace(rm.iscomplete,'0','No CO'),'1','CO done') CD 
                //FROM RVS_REQMASTER RM
                //INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
                //INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
                //inner join EM_EMPLOYEE em on rm.IUSER=em.USERID
                //where rm.id like '%" + prefixText + "' or lower(vh.REGNO) like lower('%" + prefixText + "') or lower(em.EMPLOYEENAME) like lower('%" + prefixText + @"%') AND ROWNUM <= 20
                //ORDER BY RM.IDATE DESC";

                cmd.CommandText = @"SELECT  RM.ID||' - '||VH.REGNO||' - '||WH.VISMWHNAME||' - '||RM.IDATE||' - '||em.EMPLOYEENAME||' - '||replace(replace(rm.iscomplete,'0','No CO'),'1','CO done') CD 
FROM RVS_REQMASTER RM
INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
inner join EM_EMPLOYEE em on rm.IUSER=em.USERID
where rm.id like '%" + prefixText + "' or lower(vh.REGNO) like lower('%" + prefixText + "') AND ROWNUM <= 20 and RM.REQTYPE='CD' and RM.ISCOMPLETE='0' and RM.SENDLOCATION='" + BI + "' ORDER BY RM.IDATE DESC";


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
    protected DataTable DtDetailItem
    {
        set
        {
            ViewState["DtDetailItem"] = value;
        }
        get
        {
            try
            {
                return (DataTable)ViewState["DtDetailItem"];
            }
            catch
            {
                return null;
            }
        }
    }

}