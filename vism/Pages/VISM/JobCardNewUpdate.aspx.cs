using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using it = iTextSharp.text;

public partial class Pages_VISM_JobCardNewUpdate : BasePage
{
    DbOra db = new DbOra();
    dalJobCard obj = new dalJobCard();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    protected static decimal partQtyTot = 0;
    protected static decimal partActTot = 0;
    protected static decimal partMarginTot = 0;
    protected static decimal partAmtTot = 0;
    protected static decimal serQtyTot = 0;
    protected static decimal serActTot = 0;
    protected static decimal serMarginTot = 0;
    protected static decimal serAmtTot = 0;

    protected static decimal ProblemserAmtTot = 0;

    protected static decimal disPercentTot = 0;
    protected static decimal disAmtTot = 0;
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
                LoadDdl();
                tbxEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                JOBID = (int)0;
                // LoadDdl();
                if (!string.IsNullOrEmpty(Request.QueryString["JobID"]))
                {
                    JOBID = Convert.ToInt32(Request.QueryString["JobID"]);
                    LoadMasterData(JOBID);
                }

                DtDetailPartAddColumn();
                DtDetailSerAddColumn();
                DtDetailTechAddColumn();
                DtDetailProblemServiceChargeAddColumn();
                BindRptPart();
                BindRptSer();
                BindRptTech();

                //ProblemserAmtTot = 0;



            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
        }

    }

    private void LoadDdl()
    {
        try
        {
            System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("None", "");

            //iTextSharp.text.ListItem li = new iTextSharp.text.ListItem("None", "");
            qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
            ddlCategory.DataSource = dm.ExecuteQuery(qry);
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, li);

            qry = @"select id,CHARGENAME text  from RVS_CHARGENAME order by CHARGENAME";
            ddlService.DataSource = dm.ExecuteQuery(qry);
            ddlService.DataBind();
            ddlService.Items.Insert(0, li);

            qry = @"select id,SERVICETYPE text from RVS_SERVICETYPE order by SERVICETYPE";
            ddlServiceType.DataSource = dm.ExecuteQuery(qry);
            ddlServiceType.DataBind();

            qry = @"select distinct ws.id,ws.WORKSHOP text from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
order by ws.WORKSHOP";
            ddlSendCom.DataSource = dm.ExecuteQuery(qry);
            ddlSendCom.DataBind();
            ddlSendCom.Items.Insert(0, li);

            qry = @"select id, centername text from BS_BUSINESSCENTER 
order by centername";
            ddlRecvLoc.DataSource = dm.ExecuteQuery(qry);
            ddlRecvLoc.DataBind();

            qry = @"select id, COMPANYNAME text from BS_COMPANY 
order by COMPANYNAME";
            ddlRecvCom.DataSource = dm.ExecuteQuery(qry);
            ddlRecvCom.DataBind();

            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
            ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
            ddlSendLoc.DataBind();
            ddlSendLoc.Items.Insert(0, li);

            qry = @"SELECT ID,VISMWHNAME text FROM RVS_VISMWH ORDER BY VISMWHNAME";
            ddlSendWh.DataSource = dm.ExecuteQuery(qry);
            ddlSendWh.DataBind();
            ddlSendWh.Items.Insert(0, li);

            qry = @"SELECT TM.STAFFID id,TM.STAFFID||'_'||T.TEAMNAME ||'_'||E.EMPLOYEENAME text,'' REMARKS FROM RVS_TEAMMEMBER TM
        INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
        INNER JOIN EM_EMPLOYEE ES ON ES.BUSINESSCENTERID=E.BUSINESSCENTERID AND ES.STAFFID='" + SessionManager.SessionName.StaffId + "'   WHERE TM.ISACTIVE=1";
            ddlTech.DataSource = dm.ExecuteQuery(qry);
            //ddlTech.DataSource = obj.GetTechnicianByWorkShop();
            ddlTech.DataBind();

            qry = @"Select ID,GROUPNAME text FROM RVS_GROUP";
            ddlgroup.DataSource = dm.ExecuteQuery(qry);
            ddlgroup.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DtDetailPartAddColumn()
    {
        DtDetailPart = new DataTable();
        DtDetailPart.Columns.Add("CategoryCode");
        DtDetailPart.Columns.Add("CategoryName");
        DtDetailPart.Columns.Add("ItemCode");
        DtDetailPart.Columns.Add("ItemName");
        DtDetailPart.Columns.Add("UnitCode");
        DtDetailPart.Columns.Add("UnitName");
        DtDetailPart.Columns.Add("Rate");
        DtDetailPart.Columns.Add("Margin");
        DtDetailPart.Columns.Add("MarginRate");
        DtDetailPart.Columns.Add("Qty");
        DtDetailPart.Columns.Add("ActualAmount");
        DtDetailPart.Columns.Add("MarginAmount");
        DtDetailPart.Columns.Add("TotalAmount");
        DtDetailPart.Columns.Add("Remarks");
        DtDetailPart.Columns.Add("CDID");

    }
    private void BindRptPart()
    {
        partQtyTot = 0;
        partActTot = 0;
        partMarginTot = 0;
        partAmtTot = 0;
        DtDetailPart.AcceptChanges();
        rptDetailPart.DataSource = DtDetailPart;
        rptDetailPart.DataBind();
        CalculateBill();
    }
    private void BindRptSer()
    {
        serQtyTot = 0;
        serActTot = 0;
        serMarginTot = 0;
        serAmtTot = 0;
        DtDetailSer.AcceptChanges();
        rptDetailSer.DataSource = DtDetailSer;
        rptDetailSer.DataBind();
        CalculateBill();
    }

    private void BindRptProblemServiceCharge()
    {
        ProblemserAmtTot = 0;
        
        //problem Service
        DtDetailProblemServiceCharge.AcceptChanges();
        rptDetailProblemServiceCharge.DataSource = DtDetailProblemServiceCharge;
        rptDetailProblemServiceCharge.DataBind();
        CalculateBill();
    }

    private void BindRptDiscount()
    {

        disPercentTot = 0;
        disAmtTot = 0;

        disPercentTot = Convert.ToDecimal(tbxBillDisc.Text);
        disAmtTot = Convert.ToDecimal(tbxBillDiscAmt.Text);

        CalculateBill();
    }
    private void BindRptTech()
    {
        DtDetailTech.AcceptChanges();
        rptDetailTech.DataSource = DtDetailTech;
        rptDetailTech.DataBind();
    }

    private void CalculateBill()
    {
        try
        {
            tbxBillActual.Text = (ProblemserAmtTot).ToString("F2");
            tbxBillMarginAmtTot.Text = (partAmtTot).ToString("F2");
            // tbxBillNetAmt.Text = (partAmtTot).ToString("F2");
            // tbxTotalBill.Text = (partActTot + serActTot + partAmtTot + serAmtTot).ToString("F2");
            //  tbxBillMarginAmt.Text = (partAmtTot + serAmtTot - partActTot - serActTot).ToString("F2");
            //  tbxBillMargin.Text = ((partAmtTot + serAmtTot - partActTot - serActTot) * 100 / (partActTot + serActTot)).ToString("F2");
            tbxTotalBill.Text = (partAmtTot + ProblemserAmtTot).ToString("F2");
            tbxBillNetAmt.Text = ((partAmtTot + ProblemserAmtTot) - disAmtTot).ToString("F2");

        }
        catch (Exception)
        {

        }

    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        foreach (DataRow row in DtDetailPart.Rows)
        {
            if (ddlItemName.SelectedValue != row["ItemCode"].ToString()) continue;
            MessageBox(ddlItemName.SelectedItem.Text + " is already added.");
            return;
        }
        //decimal marrate= Convert.ToDecimal(tbxRate.Text)
        //string totAmt = (Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQtyPart.Text)).ToString();
        string ac = (Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQtyPart.Text)).ToString("F2");
        string ma = (Convert.ToDecimal(tbxMarginRatePart.Text) * Convert.ToDecimal(tbxQtyPart.Text) - Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQtyPart.Text)).ToString("F2");
        DtDetailPart.Rows.Add(ddlCategory.SelectedValue, ddlCategory.SelectedItem.Text, ddlItemName.SelectedValue, ddlItemName.SelectedItem.Text.Substring(ddlItemName.SelectedItem.Text.IndexOf('-') + 1), tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxMarginPart.Text, tbxMarginRatePart.Text, tbxQtyPart.Text.Trim(), ac, ma, tbxAmtPart.Text, "");
        BindRptPart();
        tbxRate.Text = "";
        tbxMarginPart.Text = "";
        tbxMarginRatePart.Text = "";
        tbxUnitCode.Text = "";
        tbxQtyPart.Text = "";
        tbxAmtPart.Text = "";
        ddlItemName.SelectedIndex = 0;
    }

    //protected void btnSave_OnClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (JOBID > 0)
    //        {
    //            int Tech = DtDetailTech.Rows.Count;
    //            if (Tech > 0)
    //            {
    //                db.BeginTransaction();
    //                InsertMaster(JOBID);
    //                InsertParts(JOBID);
    //                InsertCharge(JOBID);
    //                InsertTechs(JOBID);
    //                InsertBillNew(JOBID);
    //                // CreateCd(JOBID);
    //                db.Commit();
    //                MessageBox(SaveSuccess);
    //            }

    //            else
    //            {
    //                MessageController.Show("Please select a Technician", MessageType.Error, Page);
    //            }

    //        }
    //        else
    //        {
    //            MessageController.Show("Please select a jobcard", MessageType.Error, Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        db.Rollback();
    //        MessageBox(SaveFail + ex.Message);
    //        ErrorLog.WriteLog(ex);
    //        MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
    //    }

    //}

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (JOBID > 0)
            {
                int Tech = DtDetailTech.Rows.Count;
                if (Tech > 0)
                {
                    string sql_1 = @"
                                    SELECT 
                                  JDT.WORKSHOP,
                                  JDT.REGNO,
                                  JDT.WORKORDERNO,
                                  JDT.WORKORDERDATE,
                                  JDT.GATEIN,
                                  JDT.PREINSPECTION_ID,
                                  JDT.PREINSPECTIONDATE,
                                  JDT.JOB_ID,
                                  JDT.JOBDATE,
                                  JDT.WORKINGHOURS,
                                  JDT.ACTUALWORKINGHOURS,
                                   CASE 
                                    WHEN (JDT.WORKINGHOURS - JDT.ACTUALWORKINGHOURS) < 0 
                                    THEN (JDT.WORKINGHOURS - JDT.ACTUALWORKINGHOURS)
                                    ELSE 0
                                  END AS DELAYTIME_NEW
                                FROM 
                                  RVS_DELAY_JOB_CHECK JDT
                                 WHERE  JDT.JOB_ID = " + JOBID;

                    DataTable dt_all = dm.ExecuteQuery(sql_1);

                    double delayTime = 0;
                    if (dt_all != null && dt_all.Rows.Count > 0)
                    {
                         delayTime = Convert.ToDouble(dt_all.Rows[0]["DELAYTIME_NEW"]);
                    }
 
                    if (delayTime < 0)
                    {
                        db.BeginTransaction();
                        InsertMaster(JOBID);
                        InsertParts(JOBID);
                        InsertCharge(JOBID);
                        InsertTechs(JOBID);
                        InsertBillNew(JOBID);
                        // CreateCd(JOBID);
                        db.Commit();
                        //MessageBox(SaveSuccess);
                        //MessageController.Show("This Job exceeded allocated working hours.Please select a delay reason and add remarks.", MessageType.Error, Page);
                        //Response.Redirect("~/Pages/VISM/NPTForm.aspx", false);
                        //                Response.Redirect("~/Pages/VISM/NPTFormNew.aspx" +
                        //"' target='_blank'>" + dt_all.Rows[0]["JOB_ID"].ToString() + "</a>", false);
                        string delayTimedata = dt_all.Rows[0]["DELAYTIME_NEW"].ToString();
                        string jobId = dt_all.Rows[0]["JOB_ID"].ToString();
                        string url = "~/Pages/VISM/NPTFormNew.aspx?delay=" + HttpUtility.UrlEncode(delayTimedata) + "&job=" + HttpUtility.UrlEncode(jobId);
                        Response.Redirect(url, false);

                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        db.BeginTransaction();
                        InsertMaster(JOBID);
                        InsertParts(JOBID);
                        InsertCharge(JOBID);
                        InsertTechs(JOBID);
                        InsertBillNew(JOBID);
                        // CreateCd(JOBID);
                        db.Commit();
                        MessageBox(SaveSuccess);
                    }

                }

                else
                {
                    MessageController.Show("Please select a Technician", MessageType.Error, Page);
                }

            }
            else
            {
                MessageController.Show("Please select a jobcard", MessageType.Error, Page);
            }
        }
        catch (Exception ex)
        {
            db.Rollback();
            MessageBox(SaveFail + ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }

    }

    private void InsertMaster(int jobid)
    {
        try
        {
            //string qry = @"update RVS_JOBCARD set EFFECTIVEDATE='" + tbxEffectiveDate.Text.ToDate() + "',REMARKS='" + tbxRecRemarks.Text + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE,SERVICETYPEID='" + ddlServiceType.SelectedValue + "',GROUPID='" + ddlgroup.SelectedValue + "' WHERE ID='" + jobid + "'";
            //db.ExecuteNonQuery(qry);

            string qry = @"UPDATE RVS_JOBCARD 
               SET EFFECTIVEDATE = TO_DATE('" + tbxEffectiveDate.Text + "', 'DD/MM/YYYY'), " +
                   "REMARKS = '" + tbxRecRemarks.Text + "', " +
                   "UPDATEDBY = '" + SessionManager.SessionName.StaffId + "', " +
                   "UPDATEDATE = SYSDATE, " +
                   "SERVICETYPEID = '" + ddlServiceType.SelectedValue + "', " +
                   "GROUPID = '" + ddlgroup.SelectedValue + "' " +
               "WHERE ID = '" + jobid + "'";

            db.ExecuteNonQuery(qry);


            //obj.Update(JOBID, tbxEffectiveDate.Text, 0, Convert.ToInt32(ddlServiceType.SelectedValue), tbxremark.Text, Convert.ToInt32(ddlgroup.SelectedValue));


            //return dm.ExecuteNonQuery("update RVS_JOBCARD set EFFECTIVEDATE=TO_DATE('" + EffDate + "','DD/MM/YYYY'),SERVICECATID='" + ServiceCat + "',SERVICETYPEID='" + ServiceType + "',REMARKS='" + remark + "',GROUPID='" + GrpupId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private void InsertBillNew(int jobid)
    {
        try
        {
            string qry = @"delete RVS_BILL where JOBID='" + jobid + "'";
            db.ExecuteNonQuery(qry);
            qry = @"insert into RVS_BILL (JOBID,ACTUALBILL,MARGIN,MARGINAMOUNT,TOTALBILL,DISCOUNTPERCENT,DISCOUNTAMOUNT,NETBILL,REMARKS,BILLNO) values
('" + jobid + "','" + tbxBillActual.Text + "','" + tbxBillMargin.Text + "','" + tbxBillMarginAmt.Text + "','" + tbxBillMarginAmtTot.Text + "','" + tbxBillDisc.Text + "','" + tbxBillDiscAmt.Text + "','" + tbxBillNetAmt.Text + "','" + tbxBillRemarks.Text + "',SQ_BIL_BILL.nextval)";
            db.ExecuteNonQuery(qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void CreateCd(int id)
    {
        try
        {
            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='CD' RETURNING LASTNO INTO :RETVAL";
            string lno = dm.ExecuteScalar(qry, "RETVAL");
            const string trType = "CD";
            string trid = trType + DateTime.Today.ToString("MMyy") + lno.PadLeft(10, '0');
            qry = @"INSERT INTO RVS_REQMASTER(ID,REQTYPE,SENDLOCATION,SENDWH,SENDCOMPANY,
RECVLOCATION,RECVWH,RECVCOMPANY,RECVREMARKS,IUSER,JOBID) VALUES(
'" + trid + "','" + trType + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"',
'" + ddlRecvLoc.SelectedValue + "','" + tbxRecVeh.Text.Split('-')[0] + "','" + ddlRecvCom.SelectedValue + "','" + tbxRecRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + "','" + id + "')";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailPart.Rows)
            {
                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                qry = @"INSERT INTO RVS_REQDETAIL (ID,REQMASTERID,ITEMCATEGORYCODE,ITEMCATEGORYNAME,
ITEMCODE,ITEMNAME,UNITID,UNITNAME,QTY,RATE,TOTALAMT,LINE,IUSER) values
('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["CategoryName"] + "','" + r["ItemCode"] + @"',
'" + r["ItemName"] + "','" + r["UnitCode"] + "','" + r["UnitName"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "')";
                db.ExecuteNonQuery(qry);
                i++;
            }
            btnSave.Enabled = false;
            tbxCdId.Text = trid;
            tbxCdId.BackColor = System.Drawing.Color.DarkCyan;
            tbxCdId.ForeColor = System.Drawing.Color.White;
            tbxCdId.Enabled = true;
            tbxCdId.ReadOnly = true;
            ViewState["TrId"] = trid;
            btnCdPrint.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InsertParts(int JobID)
    {
        try
        {
            string qry = @"Delete From RVS_JOBCARDITEMS Where JOBID='" + JobID + "'";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailPart.Rows)
            {
                qry = @"insert into RVS_JOBCARDITEMS (JOBID,PARTSID,QUANTITY,REMARKS,ITEMCODE,RATE,MARGIN,MARGINRATE,TOTALAMOUNT,MARGINAMOUNT,LINE,AMOUNT,CDID) values
('" + JobID + "','0','" + r["Qty"] + "','" + r["Remarks"] + "','" + r["ItemCode"] + "','" + r["Rate"] +
                            "','" + r["Margin"] + "','" + r["MarginRate"] + "','" + r["TotalAmount"] + "','" + r["MarginAmount"] + "','" + i + "','" + r["ActualAmount"] + "','" + r["CDID"] + "')";
                db.ExecuteNonQuery(qry);
                i++;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private void InsertCharge(int JobID)
    {
        try
        {
            string qry = @"Delete From RVS_JOBCARDSCCHARGE Where JOBID='" + JobID + "'";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailSer.Rows)
            {
                qry = @"insert into RVS_JOBCARDSCCHARGE (JOBID,CHARGEID,QUANTITY,REMARKS,RATE,MARGIN,MARGINRATE,TOTALAMOUNT,MARGINAMOUNT,LINE,AMOUNT) values
('" + JobID + "','" + r["ServiceCode"] + "','" + r["SerQty"] + "','" + r["ServiceRemarks"] + "','" + r["ServiceCharge"] +
                            "','" + r["ServiceChargeMargin"] + "','" + r["ServiceChargeMarginRate"] + "','" + r["ServiceAmount"] + "','" + r["SerMarginAmount"] + "','" + i + "','" + r["SerActualAmount"] + "')";
                db.ExecuteNonQuery(qry);
                i++;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private void InsertTechs(int JobID)
    {
        try
        {
            string qry = @"Delete From RVS_JOBCARDTECHS Where JOBID='" + JobID + "'";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailTech.Rows)
            {
                qry = @"Insert into RVS_JOBCARDTECHS(JOBID,STAFFID,REMARKS,LINE)
        VALUES('" + JobID + "','" + r["StaffId"] + "','" + r["TechRemark"] + "','" + i + "')";
                db.ExecuteNonQuery(qry);
                i++;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    //protected void imgbtnDelete_OnCommand(object sender, CommandEventArgs e)
    //{
    //    for (int i = 0; i < DtDetailPart.Rows.Count; i++)
    //    {
    //        if (DtDetailPart.Rows[i]["ItemCode"].ToString() == e.CommandArgument.ToString())
    //        {
    //            DtDetailPart.Rows[i].Delete();
    //        }
    //    }
    //    BindRptPart();
    //}





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

        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Select", "");
        qry =
           @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        ddlSendLoc.DataBind();
        ddlSendLoc.Items.Insert(0, li);

        ddlSendLoc.Enabled = true;
    }

    protected void ddlSendLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlSendLoc.SelectedValue + "' order by VISMWHNAME";
        ddlSendWh.DataSource = dm.ExecuteQuery(qry);
        ddlSendWh.DataBind();
        ddlSendWh.Items.Insert(0, li);
        ddlSendCom.Items.Clear();
        ddlSendWh.Enabled = true;

        //if (ddlRecvLoc.SelectedValue == ddlSendLoc.SelectedValue)
        //{
        //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("disabled", "disabled");
        //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");

        //}

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

        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Select", "");
        qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        ddlCategory.DataSource = dm.ExecuteQuery(qry);
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, li);
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("Select", "");
        string qry = @"select ITEMCODE id,ITEMCODE||'-'||ITEMNAME text  from RVS_ITEMMASTER where isactive=1
 and ITEMCATEGORYCODE='" + ddlCategory.SelectedValue + "'order by ITEMNAME";
        ddlItemName.DataSource = dm.ExecuteQuery(qry);
        ddlItemName.DataBind();
        ddlItemName.Items.Insert(0, li);
        foreach (DataRow r in DtDetailPart.Rows)
        {
            foreach (System.Web.UI.WebControls.ListItem ltItem in ddlItemName.Items)
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
        tbxQtyPart.Text = "";
        tbxAmtPart.Text = "";
        tbxQtyPart.Enabled = true;
    }

    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    {
        string qry = @"SELECT VH.BUSINESSCENTERID ID,LO.CENTERNAME TEXT,VH.COMPANYID CID,CO.COMPANYNAME CTEXT FROM RVS_VEHICLE VH
INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
WHERE VH.ID='" + tbxRecVeh.Text.Split('-')[0] + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(dt.Rows[0]["TEXT"].ToString(), dt.Rows[0]["ID"].ToString());
        ddlRecvLoc.DataSource = null;
        ddlRecvLoc.DataBind();
        ddlRecvLoc.Items.Insert(0, li);
        li = new System.Web.UI.WebControls.ListItem(dt.Rows[0]["CTEXT"].ToString(), dt.Rows[0]["CID"].ToString());
        ddlRecvCom.DataSource = null;
        ddlRecvCom.DataBind();
        ddlRecvCom.Items.Insert(0, li);
        ddlSendLoc.Enabled = true;
        li = new System.Web.UI.WebControls.ListItem("Select", "");
        qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        ddlSendLoc.DataBind();
        ddlSendLoc.Items.Insert(0, li);
    }
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
                //                cmd.CommandText = @"SELECT V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME VEHICLE FROM RVS_VEHICLE V
                //                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                //                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%'))  AND ROWNUM <= 20";

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

    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        string ac = (Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQtyPart.Text)).ToString("F2");
        string ma = (Convert.ToDecimal(tbxMarginRatePart.Text) * Convert.ToDecimal(tbxQtyPart.Text) - Convert.ToDecimal(tbxRate.Text) * Convert.ToDecimal(tbxQtyPart.Text)).ToString("F2");
        DataRow rows = DtDetailPart.Select("ItemCode = '" + (string)ViewState["ID"] + "'").FirstOrDefault();
        rows["Margin"] = tbxMarginPart.Text;
        rows["MarginRate"] = tbxMarginRatePart.Text;
        rows["Qty"] = tbxQtyPart.Text;
        //rows["ActualAmount"] = ac;
        //rows["MarginAmount"] = ma;
        rows["MarginAmount"] = tbxAmtPart.Text;
        rows["TotalAmount"] = tbxAmtPart.Text;
        BindRptPart();
        ddlCategory.Enabled = true;
        ddlItemName.SelectedIndex = 0;
        ddlItemName.Enabled = true;
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        tbxRate.Text = "";
        tbxMarginPart.Text = "";
        tbxMarginRatePart.Text = "";
        tbxUnitCode.Text = "";
        tbxQtyPart.Text = "";
        tbxAmtPart.Text = "";
        ddlItemName.SelectedIndex = 0;
        tbxQtyPart.Enabled = false;
        //foreach (DataRow r in DtDetailPart.Rows)
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

    //protected void imgbtnEdit_OnCommand(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        DataRow rows = DtDetailPart.Select("ItemCode = '" + e.CommandArgument + "'").FirstOrDefault();
    //        ddlCategory.SelectedValue = rows["CategoryCode"].ToString();
    //        ddlCategory_OnSelectedIndexChanged(null, null);
    //        ddlItemName.SelectedValue = e.CommandArgument.ToString();
    //        tbxUnitCode.Text = rows["UnitCode"].ToString();
    //        tbxRate.Text = rows["Rate"].ToString();
    //        tbxMarginPart.Text = rows["Margin"].ToString();
    //        tbxMarginRatePart.Text = rows["MarginRate"].ToString();
    //        tbxQtyPart.Text = rows["Qty"].ToString();
    //        tbxAmtPart.Text = rows["TotalAmount"].ToString();
    //        btnAdd.Visible = false;
    //        btnUpdate.Visible = true;
    //        ddlCategory.Enabled = false;
    //        ddlItemName.Enabled = false;
    //        tbxQtyPart.Enabled = true;
    //        ViewState["ID"] = e.CommandArgument.ToString();
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }

    //}


    protected void ddlService_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        string qry = @"select sr.ID,sr.CHARGENAME,ch.AMOUNT from RVS_CHARGES ch
inner join RVS_CHARGENAME sr on ch.CHARGEID=sr.ID
where sr.id='" + ddlService.SelectedValue + "' and ch.BUSINESSCENTERID='" + ddlSendLoc.SelectedValue + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count == 0)
        {

        }
        else
        {
            tbxSerChar.Text = dt.Rows[0]["AMOUNT"].ToString();
            tbxMarginSer.Text = @"0";
            tbxMarginRateSer.Text = (Convert.ToDecimal(tbxSerChar.Text) +
                 Convert.ToDecimal(tbxSerChar.Text) * Convert.ToDecimal(tbxMarginSer.Text) / 100).ToString();

        }

    }

    protected void btnSerAdd_OnClick(object sender, EventArgs e)
    {
        foreach (DataRow row in DtDetailSer.Rows)
        {
            if (ddlService.SelectedValue != row["ServiceCode"].ToString()) continue;
            MessageBox(ddlService.SelectedItem.Text + " is already added.");
            return;
        }
        string ac = (Convert.ToDecimal(tbxSerChar.Text) * Convert.ToDecimal(tbxSerQty.Text)).ToString("F2");
        string ma = (Convert.ToDecimal(tbxMarginRateSer.Text) * Convert.ToDecimal(tbxSerQty.Text) - Convert.ToDecimal(tbxSerChar.Text) * Convert.ToDecimal(tbxSerQty.Text)).ToString("F2");
        DtDetailSer.Rows.Add(ddlService.SelectedValue, ddlService.SelectedItem.Text, tbxSerChar.Text, tbxMarginSer.Text, tbxMarginRateSer.Text, tbxSerQty.Text, ac, ma, tbxSerAmt.Text, tbxSerRemarks.Text);

        // tbxBillMargin

        BindRptSer();
        ddlService.SelectedIndex = 0;
        tbxSerChar.Text = "";
        tbxMarginSer.Text = "";
        tbxMarginRateSer.Text = "";
        tbxSerQty.Text = "";
        tbxSerAmt.Text = "";
        tbxSerRemarks.Text = "";
    }

    protected void imgbtnEditSer_OnCommand(object sender, CommandEventArgs e)
    {
        DataRow rows = DtDetailSer.Select("ServiceCode = '" + e.CommandArgument + "'").FirstOrDefault();
        ddlService.SelectedValue = rows["ServiceCode"].ToString();
        tbxSerChar.Text = rows["ServiceCharge"].ToString();
        tbxMarginSer.Text = rows["ServiceChargeMargin"].ToString();
        tbxMarginRateSer.Text = rows["ServiceChargeMarginRate"].ToString();
        tbxSerQty.Text = rows["SerQty"].ToString();
        tbxSerAmt.Text = rows["ServiceAmount"].ToString();
        tbxSerRemarks.Text = rows["ServiceRemarks"].ToString();

        btnSerAdd.Visible = false;
        btnSerUpdate.Visible = true;
        ddlService.Enabled = false;
        ViewState["IdSer"] = e.CommandArgument.ToString();
    }
    protected void btnSerUpdate_OnClick(object sender, EventArgs e)
    {
        string ac = (Convert.ToDecimal(tbxSerChar.Text) * Convert.ToDecimal(tbxSerQty.Text)).ToString("F2");
        string ma = (Convert.ToDecimal(tbxMarginRateSer.Text) * Convert.ToDecimal(tbxSerQty.Text) - Convert.ToDecimal(tbxSerChar.Text) * Convert.ToDecimal(tbxSerQty.Text)).ToString("F2");
        DataRow rows = DtDetailSer.Select("ServiceCode = '" + (string)ViewState["IdSer"] + "'").FirstOrDefault();
        rows["ServiceChargeMargin"] = tbxMarginSer.Text;
        rows["ServiceChargeMarginRate"] = tbxMarginRateSer.Text;
        rows["SerQty"] = tbxSerQty.Text;
        rows["SerActualAmount"] = ac;
        rows["SerMarginAmount"] = ma;
        rows["ServiceAmount"] = tbxSerAmt.Text;
        rows["ServiceRemarks"] = tbxSerRemarks.Text;
        BindRptSer();
        ddlService.Enabled = true;
        ddlService.SelectedIndex = 0;
        btnSerUpdate.Visible = false;
        btnSerAdd.Visible = true;
        ddlService.SelectedIndex = 0;
        tbxSerChar.Text = "";
        tbxMarginSer.Text = "";
        tbxMarginRateSer.Text = "";
        tbxSerQty.Text = "";
        tbxSerAmt.Text = "";
        tbxSerRemarks.Text = "";
    }
    protected void imgbtnDeleteSer_OnCommand(object sender, CommandEventArgs e)
    {
        for (int i = 0; i < DtDetailSer.Rows.Count; i++)
        {
            if (DtDetailSer.Rows[i]["ServiceCode"].ToString() == e.CommandArgument.ToString())
            {
                DtDetailSer.Rows[i].Delete();
            }
        }
        BindRptSer();
    }
    protected DataTable DtDetailSer
    {
        set
        {
            ViewState["DtDetailSer"] = value;
        }
        get
        {
            try
            {
                return (DataTable)ViewState["DtDetailSer"];
            }
            catch
            {
                return null;
            }
        }
    }
    protected DataTable DtDetailProblemServiceCharge
    {
        set
        {
            ViewState["DtDetailProblemServiceCharge"] = value;
        }
        get
        {
            try
            {
                return (DataTable)ViewState["DtDetailProblemServiceCharge"];
            }
            catch
            {
                return null;
            }
        }
    }
    protected DataTable DtDetailPart
    {
        set
        {
            ViewState["DtDetailPart"] = value;
        }
        get
        {
            try
            {
                return (DataTable)ViewState["DtDetailPart"];
            }
            catch
            {
                return null;
            }
        }
    }
    private void DtDetailSerAddColumn()
    {
        DtDetailSer = new DataTable();
        DtDetailSer.Columns.Add("ServiceCode");
        DtDetailSer.Columns.Add("ServiceName");
        DtDetailSer.Columns.Add("ServiceCharge");
        DtDetailSer.Columns.Add("ServiceChargeMargin");
        DtDetailSer.Columns.Add("ServiceChargeMarginRate");
        DtDetailSer.Columns.Add("SerQty");
        DtDetailSer.Columns.Add("SerActualAmount");
        DtDetailSer.Columns.Add("SerMarginAmount");
        DtDetailSer.Columns.Add("ServiceAmount");
        DtDetailSer.Columns.Add("ServiceRemarks");
    }

    private void DtDetailProblemServiceChargeAddColumn()
    {
        DtDetailProblemServiceCharge = new DataTable();
        DtDetailProblemServiceCharge.Columns.Add("ITEMID");
        DtDetailProblemServiceCharge.Columns.Add("ITEMNAME");
        DtDetailProblemServiceCharge.Columns.Add("QUANTITY");
        DtDetailProblemServiceCharge.Columns.Add("LABORRATE");
        DtDetailProblemServiceCharge.Columns.Add("LABORQTY");
        DtDetailProblemServiceCharge.Columns.Add("WORKINGHOUR");
        DtDetailProblemServiceCharge.Columns.Add("LABORCHARGE");
        DtDetailProblemServiceCharge.Columns.Add("REMARKS");
    }

    protected void btnAddTech_OnClick(object sender, EventArgs e)
    {
        foreach (DataRow row in DtDetailTech.Rows)
        {
            if (ddlTech.SelectedValue != row["StaffId"].ToString()) continue;
            MessageBox(ddlTech.SelectedItem.Text + " is already added.");
            return;
        }
        DtDetailTech.Rows.Add(ddlTech.SelectedValue, ddlTech.SelectedItem.Text.Split('_')[2], ddlTech.SelectedItem.Text.Split('_')[1], tbxRemarkTech.Text);

        BindRptTech();
        ddlTech.SelectedIndex = 0;
        tbxRemarkTech.Text = "";
    }


    protected DataTable DtDetailTech
    {
        set
        {
            ViewState["DtDetailTech"] = value;
        }
        get
        {
            try
            {
                return (DataTable)ViewState["DtDetailTech"];
            }
            catch
            {
                return null;
            }
        }
    }
    private void DtDetailTechAddColumn()
    {
        DtDetailTech = new DataTable();
        DtDetailTech.Columns.Add("StaffId");
        DtDetailTech.Columns.Add("TechnicianName");
        DtDetailTech.Columns.Add("Group");
        DtDetailTech.Columns.Add("TechRemark");
    }

    protected void imgbtnEditTech_OnCommand(object sender, CommandEventArgs e)
    {
        DataRow rows = DtDetailTech.Select("StaffId = '" + e.CommandArgument + "'").FirstOrDefault();
        ddlTech.SelectedValue = rows["StaffId"].ToString();
        tbxRemarkTech.Text = rows["TechRemark"].ToString();
        btnAddTech.Visible = false;
        btnUpdateTech.Visible = true;
        ddlTech.Enabled = false;
        ViewState["IdTech"] = e.CommandArgument.ToString();
    }
    protected void btnUpdateTech_OnClick(object sender, EventArgs e)
    {
        DataRow rows = DtDetailTech.Select("StaffId = '" + (string)ViewState["IdTech"] + "'").FirstOrDefault();
        if (rows != null) rows["TechRemark"] = tbxRemarkTech.Text;
        BindRptTech();
        ddlTech.Enabled = true;
        ddlTech.SelectedIndex = 0;
        btnUpdateTech.Visible = false;
        btnAddTech.Visible = true;
        tbxRemarkTech.Text = "";
    }
    protected void imgbtnDeleteTech_OnCommand(object sender, CommandEventArgs e)
    {
        for (int i = 0; i < DtDetailTech.Rows.Count; i++)
        {
            if (DtDetailTech.Rows[i]["StaffId"].ToString() == e.CommandArgument.ToString())
            {
                DtDetailTech.Rows[i].Delete();
            }
        }
        BindRptTech();
    }

    protected void btnPreInsSearch_Click(object sender, EventArgs e)
    {

        BindRptMain();
    }
    private void BindRptMain()
    {
        qry = @"SELECT j.Id, j.id ""Job Id"",to_char(pi.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Received date"", v.REGNO ""Reg No."",v.CHASSISNO ""Chassis No."",
mo.MODELNAME ""Model"",st.SERVICETYPE ""Service Type"",c.CUSTOMERNAME ""Customer"",co.COMPANYNAME ""Company"", v.DRIVERNAME ""Driver Name"",v.DRIVERMOBILE ""Driver Mobile"", j.REMARKS ""Group"" from RVS_JOBCARD J
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


    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        DataTable DtDetailTech = new DataTable();

        int ID = Convert.ToInt32(e.CommandArgument);

        JOBID = ID;
        LoadMasterData(JOBID);
        // LoadParts(Convert.ToInt32(tbxRecVeh.Text.Split('-')[0]));
        LoadParts(JOBID);
        LoadCharges(JOBID);
        LoadTech(JOBID);
        LoadDiscount(JOBID);
        BindRptDiscount();
        LoadProblemServiceCharge(JOBID);

    }
    private void LoadProblemServiceCharge(int jobid)
    {
        string qry = @"SELECT PID.PIID,PITEM.ID AS ITEMID,PITEM.ITEMNAME,PID.QUANTITY,PID.LABORRATE, PID.LABORQTY,PID.WORKINGHOUR,
(PID.QUANTITY*PID.LABORRATE*PID.LABORQTY*PID.WORKINGHOUR) AS LABORCHARGE, PID.REMARKS
FROM RVS_PIDETAILS  PID
LEFT JOIN RVS_INSPECTIONITEMS PITEM ON PID.INSPECTIONITEMID = PITEM.ID
WHERE PIID in (select PIID from RVS_JOBCARD where ID='" + jobid + "')";
        DataTable dt = dm.ExecuteQuery(qry);
        foreach (DataRow r in dt.Rows)
        {
            DtDetailProblemServiceCharge.Rows.Add(r["ITEMID"].ToString(), r["ITEMNAME"].ToString(),
                r["QUANTITY"].ToString(), r["LABORRATE"].ToString(),
                r["LABORQTY"].ToString(), r["WORKINGHOUR"].ToString(), r["LABORCHARGE"].ToString(),
                r["REMARKS"].ToString());
        }
        BindRptProblemServiceCharge();
    }

    private void LoadDiscount(int id)
    {
        string qry = @"select DISCOUNTPERCENT,DISCOUNTAMOUNT,NETBILL from RVS_BILL where JOBID='1099291'";
        DataTable dt = dm.ExecuteQuery(qry);

        if (dt.Rows.Count > 0)
        {

            tbxBillDisc.Text = dt.Rows[0]["DISCOUNTPERCENT"].ToString();
            tbxBillDiscAmt.Text = dt.Rows[0]["DISCOUNTAMOUNT"].ToString();
        }
        BindRptSer();
    }

    private void LoadMasterData(int jobid)
    {
        //        string qry = @"SELECT JB.ID, PR.VEHICLEID || ' - '|| VH.REGNO RECWH, MO.MODELNAME,VH.BUSINESSCENTERID RECLOC,VH.COMPANYID RECCOM,PR.SERVICETYPEID,
        //JB.EFFECTIVEDATE,WH.LOCATIONID SENDLOC,WH.ID SENDWH,PR.WORKSHOPID SENDCOM,RM.ID CDID, JB.REMARKS FROM RVS_JOBCARD JB
        //INNER JOIN RVS_PREINSPECTION PR ON JB.PIID=PR.ID
        //INNER JOIN RVS_VEHICLE VH ON JB.VEHICLEID=VH.ID
        //INNER JOIN RVS_MODEL MO ON VH.MODELID=MO.ID
        //INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
        //INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
        //INNER JOIN RVS_VISMWH WH ON PR.WORKSHOPID=WH.WORKSHOPID
        //left JOIN RVS_REQMASTER RM ON JB.ID=RM.JOBID AND WH.ID=RM.SENDWH
        //where JB.ID='" + jobid + "' AND JB.ISOPEN=1 AND PR.ISOPEN=1";

        string qry = @"SELECT JB.ID, JB.VEHICLEID || ' - '|| VH.REGNO RECWH, MO.MODELNAME,VH.BUSINESSCENTERID RECLOC,VH.COMPANYID RECCOM,JB.SERVICETYPEID,
TO_CHAR(JB.EFFECTIVEDATE,'DD/MM/YYYY') EFFECTIVEDATE,WH.LOCATIONID SENDLOC,WH.ID SENDWH,JB.WORKSHOPID SENDCOM,TM.ID COID, JB.REMARKS,JB.GROUPID FROM RVS_JOBCARD JB
INNER JOIN RVS_VEHICLE VH ON VH.ID=JB.VEHICLEID
INNER JOIN RVS_MODEL MO ON MO.ID=VH.MODELID
INNER JOIN BS_BUSINESSCENTER LO ON LO.ID=VH.BUSINESSCENTERID
INNER JOIN BS_COMPANY CO ON CO.ID=VH.COMPANYID
INNER JOIN RVS_WORKSHOP WS ON WS.ID=JB.WORKSHOPID
left outer JOIN RVS_REQMASTER RM ON RM.JOBID=JB.ID
left outer JOIN RVS_TRMASTER TM ON TM.REFTRID=RM.ID
left outer JOIN RVS_VISMWH WH ON JB.WORKSHOPID =WH.WORKSHOPID AND WH.ID =RM.SENDWH
WHERE JB.ID='" + jobid + "'";

        DataTable dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count > 0)
        {
            tbxRecVeh.Text = dt.Rows[0]["RECWH"].ToString();
            tbxModel.Text = dt.Rows[0]["MODELNAME"].ToString();
            //ddlRecvLoc.SelectedValue = dt.Rows[0]["RECLOC"].ToString();
            //ddlRecvCom.SelectedValue = dt.Rows[0]["RECCOM"].ToString();
            //ddlServiceType.SelectedValue = dt.Rows[0]["SERVICETYPEID"].ToString();
            tbxEffectiveDate.Text = dt.Rows[0]["EFFECTIVEDATE"].ToString();

            ddlRecvLoc.SelectedValue = dt.Rows[0]["RECLOC"].ToString();
            ddlRecvCom.SelectedValue = dt.Rows[0]["RECCOM"].ToString();
            ddlServiceType.SelectedValue = dt.Rows[0]["SERVICETYPEID"].ToString();
            ddlSendLoc.SelectedValue = dt.Rows[0]["SENDLOC"].ToString();
            ddlSendCom.SelectedValue = dt.Rows[0]["SENDCOM"].ToString();
            ddlSendWh.SelectedValue = dt.Rows[0]["SENDWH"].ToString();
            ddlgroup.SelectedValue = dt.Rows[0]["GROUPID"].ToString();


            ///////
            ////try
            ////{
            ////    ddlRecvLoc.SelectedValue = dt.Rows[0]["RECLOC"].ToString();
            ////}
            ////catch
            ////{

            ////}
            ////try
            ////{
            ////    ddlRecvCom.SelectedValue = dt.Rows[0]["RECCOM"].ToString();
            ////}
            ////catch
            ////{

            ////}
            ////try
            ////{
            ////    ddlServiceType.SelectedValue = dt.Rows[0]["SERVICETYPEID"].ToString();
            ////}
            ////catch
            ////{

            ////}

            ////try
            ////{
            ////    ddlSendLoc.SelectedValue = dt.Rows[0]["SENDLOC"].ToString();
            ////}
            ////catch
            ////{

            ////}
            ////try
            ////{
            ////    ddlSendCom.SelectedValue = dt.Rows[0]["SENDCOM"].ToString();
            ////}
            ////catch
            ////{
            ////}
            ////try
            ////{
            ////    ddlSendWh.SelectedValue = dt.Rows[0]["SENDWH"].ToString();
            ////}
            ////catch
            ////{
            ////}
            ////try
            ////{
            ////    ddlgroup.SelectedValue = dt.Rows[0]["GROUPID"].ToString();
            ////}
            ////catch
            ////{
            ////}


            ///////


            tbxCdId.Text = dt.Rows[0]["COID"].ToString();
            tbxRecRemarks.Text = dt.Rows[0]["REMARKS"].ToString();

        }


        //qry = @"select id, VISMWHNAME text from RVS_VISMWH where WORKSHOPID='" + dt.Rows[0]["SENDCOM"] + "' order by VISMWHNAME";
        //ddlSendWh.DataSource = dm.ExecuteQuery(qry);
        //ddlSendWh.DataBind();
        //ddlSendWh.SelectedValue = dt.Rows[0]["SENDWH"].ToString();

        qry = @"SELECT distinct TM.STAFFID id,TM.STAFFID||'_'||E.EMPLOYEENAME ||'_'||T.TEAMNAME text FROM RVS_TEAMMEMBER TM
        INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
        inner join RVS_JOBCARD j on tm.WORKSHOPID=j.WORKSHOPID
        WHERE TM.ISACTIVE=1 and j.id='" + JOBID + @"'
        order by text";
        ddlTech.DataSource = dm.ExecuteQuery(qry);
        ddlTech.DataBind();
        ddlTech.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", ""));
    }
    private void LoadParts(int id)
    {
        string qry = @"select ct.ITEMCATEGORYCODE,ct.ITEMCATEGORYNAME,im.ITEMCODE,im.ITEMNAME,im.SCMUNITCODE,
ut.UNITNAME,ji.RATE,ji.MARGIN,ji.MARGINRATE ,TD.QTY,
ji.AMOUNT,((ji.TOTALAMOUNT-ji.AMOUNT)*CC.PERCENTAGE)/100 MARGINAMOUNT,(ji.TOTALAMOUNT*CC.PERCENTAGE)/100 TOTALAMOUNT,ji.REMARKS,ji.CDID
from rvs_jobcarditems ji
INNER JOIN RVS_REQMASTER RM ON RM.JOBID=JI.JOBID AND RM.REQTYPE='CD' AND RM.ISACTIVE=1 AND RM.ISCOMPLETE='1'
INNER JOIN RVS_REQDETAIL RD ON RD.REQMASTERID=RM.ID AND RD.ITEMCODE=JI.ITEMCODE AND RD.REQMASTERID=JI.CDID
LEFT JOIN RVS_TRMASTER TM ON  TM.REFTRID=RM.ID AND TM.TRTYPE='CO' AND TM.ISACTIVE=1
INNER JOIN RVS_TRDETAIL TD ON TD.TRMASTERID=TM.ID AND JI.ITEMCODE=TD.ITEMCODE
inner join RVS_ITEMmaster im on ji.ITEMCODE=im.ITEMCODE
inner join RVS_ITEMCATEGORY ct on im.ITEMCATEGORYCODE=ct.ITEMCATEGORYCODE
inner join RVS_SCMUNIT ut on im.SCMUNITCODE=ut.UNITCODE
inner join RVS_COCATAGORY cc on rm.CATEGORYCODE=cc.ID
where JI.jobid='" + id + "'";

        //        string qry = @"SELECT RD.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME, RD.ITEMCODE,IM.ITEMNAME,UT.UNITCODE SCMUNITCODE, UT.UNITNAME, TD.RATE,TD.MARGIN,TD.MARGINRATE,
        //SUM(NVL(TD.QTY,0)) QUANTITY,SUM(NVL(TD.RATE*TD.QTY,0)) AMOUNT,TD.MARGINRATE MARGINAMOUNT,(SUM(NVL(TD.RATE*TD.QTY,0))-TD.MARGINRATE) TOTALAMOUNT, TD.REMARKS REMARKS
        //FROM RVS_REQDETAIL RD
        //INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID AND RM.REQTYPE='CD' AND RM.ISACTIVE=1
        //INNER JOIN RVS_JOBCARD JC ON RM.JOBID=JC.ID
        //INNER JOIN RVS_VEHICLE VH ON JC.VEHICLEID=VH.ID
        //LEFT JOIN RVS_TRMASTER TM ON  TM.REFTRID=RM.ID AND TM.TRTYPE='CO' AND TM.ISACTIVE=1
        //LEFT JOIN RVS_TRDETAIL TD ON TD.TRMASTERID=TM.ID AND RD.ITEMCODE=TD.ITEMCODE
        //INNER JOIN RVS_ITEMMASTER IM ON RD.ITEMCODE=IM.ITEMCODE
        //INNER JOIN RVS_ITEMCATEGORY CT ON IM.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE
        //INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE=UT.UNITCODE
        //WHERE JC.VEHICLEID='" + id+@"' AND JC.ISOPEN=1 AND JC.ISACTIVE=1
        //GROUP BY RD.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME,
        //RD.ITEMCODE,IM.ITEMNAME,UT.UNITCODE,UT.UNITNAME,
        //TD.RATE,TD.MARGIN,TD.MARGINRATE,RD.REMARKS,TD.REMARKS
        //HAVING SUM(NVL(TD.QTY,0))>0
        //ORDER BY IM.ITEMNAME";
        DataTable dt = dm.ExecuteQuery(qry);
        foreach (DataRow r in dt.Rows)
        {
            DtDetailPart.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
                r["SCMUNITCODE"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["MARGIN"].ToString(), r["MARGINRATE"].ToString(),
                r["QTY"].ToString(), r["AMOUNT"].ToString(), r["MARGINAMOUNT"].ToString(),
                r["TOTALAMOUNT"].ToString(), r["REMARKS"].ToString(), r["CDID"].ToString());


            //DtDetailPart.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
            //   r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
            //   r["SCMUNITCODE"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["MARGIN"].ToString(), r["MARGINRATE"].ToString(),
            //   r["QUANTITY"].ToString(), r["AMOUNT"].ToString(), r["MARGINAMOUNT"].ToString(),
            //   r["TOTALAMOUNT"].ToString(), r["REMARKS"].ToString());
        }
        BindRptPart();
    }
    private void LoadCharges(int jobid)
    {
        string qry = @"SELECT CM.ID,CM.CHARGENAME,JC.RATE,JC.MARGIN,JC.MARGINRATE,JC.QUANTITY,
JC.AMOUNT,JC.TOTALAMOUNT-JC.AMOUNT MARGINAMOUNT,JC.TOTALAMOUNT,JC.REMARKS
FROM RVS_JOBCARDSCCHARGE JC
left JOIN RVS_CHARGENAME CM ON JC.CHARGEID=CM.ID
WHERE JC.JOBID='" + jobid + "' ORDER BY JC.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        foreach (DataRow r in dt.Rows)
        {
            DtDetailSer.Rows.Add(r["ID"].ToString(), r["CHARGENAME"].ToString(),
                r["RATE"].ToString(), r["MARGIN"].ToString(),
                r["MARGINRATE"].ToString(), r["QUANTITY"].ToString(), r["AMOUNT"].ToString(), r["MARGINAMOUNT"].ToString(),
                r["TOTALAMOUNT"].ToString(), r["REMARKS"].ToString());
        }
        BindRptSer();
    }
    private void LoadTech(int jobid)
    {
        string qry = @"select te.STAFFID,em.EMPLOYEENAME,ti.TEAMNAME,te.REMARKS from RVS_JOBCARDTECHS te
inner join em_employee em on te.STAFFID=em.STAFFID
inner join RVS_TEAMMEMBER tm on te.STAFFID=tm.STAFFID
inner join rvs_team ti on tm.TEAMID=ti.id
where te.JOBID='" + jobid + "' order by te.line";
        DataTable dt = dm.ExecuteQuery(qry);
        foreach (DataRow r in dt.Rows)
        {
            DtDetailTech.Rows.Add(r["STAFFID"].ToString(), r["EMPLOYEENAME"].ToString(),
                r["TEAMNAME"].ToString(), r["REMARKS"].ToString());
        }
        BindRptTech();
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
        string url = @"PrintBillNew.aspx?JobID=" + ID.Trim();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void btnBillPrintUpdate_Command(object sender, CommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        string url = @"PrintBillNewUpdate.aspx?JobID=" + ID.Trim();
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

        DataTable dt = new DataTable();
        string qry = @"Select II.ID,II.ITEMNAME,II.FILLTYPE  from RVS_INSPECTIONITEMS II
            INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID
            INNER JOIN RVS_PIDETAILS PD ON PD.INSPECTIONITEMID = II.ID WHERE II.INSPECTIONTYPE=0 AND PD.STATUSID=2 AND PD.PIID in (SELECT PIID from RVS_JOBCARD where ID='" + ID + "') AND II.FILLTYPE='1' and PD.REMARKS is null  ORDER BY II.SERVICECATID ASC";
        dt = dm.ExecuteQuery(qry);

        DataTable dt1 = new DataTable();
        string qry1 = @"SELECT JC.VEHICLEID FROM RVS_JOBCARD JC WHERE JC.ID='" + ID + "'";
        dt1 = dm.ExecuteQuery(qry1);

        DataTable dt2 = new DataTable();
        string qry2 = @"select B.JOBID  from RVS_JOBCARD J, RVS_BILL B where J.ID=B.JOBID AND J.ISOPEN=1 AND J.ID='" + ID + "'";
        dt2 = dm.ExecuteQuery(qry2);



        if (dt.Rows.Count > 0)
        {
            string ItemName = dt.Rows[0]["ITEMNAME"].ToString();
            MessageBox("Please input Item code On Remarks Field");
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxRate').focus();", true);
        }

        else
        {
            string vid = dt1.Rows[0]["VEHICLEID"].ToString();

            if (dt2.Rows.Count>0) { 
            obj.CloseJobCard(ID);
            SendInitialMail(vid, ID);
            MessageBox("Close Successfull");
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                //BindRptPart();
            }
            else
            {
                MessageBox("Please save job card information first then close the job card.");
            }
        }

    }

    public void SendInitialMail(string vid, int ID)
    {


        try
        {
            string qry = @"SELECT VH.REGNO,CS.CUSTOMERNAME,CS.MOBILE,VH.DRIVERNAME,VH.DRIVERMOBILE,WS.WORKSHOP,CS.EMAIL,CS.CC1,CS.CC2 FROM RVS_PREINSPECTION PR
INNER JOIN RVS_WORKSHOP WS ON PR.WORKSHOPID=WS.ID
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN RVS_CUSTOMER CS ON VH.CUSTOMERID=CS.ID
INNER JOIN RVS_JOBCARD JC ON JC.PIID=PR.ID
WHERE VH.ID='" + vid + @"' AND JC.ID='" + ID + @"'";
            DataTable dt = dm.ExecuteQuery(qry);
            //  string fromuser = dt.Rows[0]["EMAIL"].ToString();
            List<string> Approver = new dalApprover().GetByCreatedbyJC(vid, ID);

            string sbd = "Hello Sir,";
            sbd += "<br>Job card of your vehicle " + dt.Rows[0]["REGNO"] + " has just closed in " + dt.Rows[0]["WORKSHOP"];
            sbd += "<br>Your bill has attached in here.\nDriver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " + dt.Rows[0]["DRIVERMOBILE"];
            //sbd += "<br>For Details Information : http://localhost:30404/CoDetailsOnlineRpt.aspx?regno= " + dt.Rows[0]["REGNO"];
            sbd += "<br>For historical data click here: <a href='http://rsos.prangroup.com:8287/vism/CoDetailsOnlineRpt.aspx?regno=" +
            dt.Rows[0]["REGNO"].ToString() + "' target='_blank'>" + dt.Rows[0]["REGNO"].ToString() + "</a>";



            //string f = @"Files";
            //if (!Directory.Exists(f))
            //    Directory.CreateDirectory(f);


            //            string qry1 = @"select B.WORKSHOP WH,B.REGNO COUNIT,B.MODELNAME,B.COMPANYNAME COMPANY,B.CATEGORYNAME CATEGORY,B.ITEMNAME,B.UNIT UNITNAME,B.QTY,ROUND((B.RATE*B.QTY)/(B.QTY),2) RATE,
            //ROUND((B.RATE*B.QTY),2) AMOUNT,B.CUSTOMERNAME
            //from v_rvs_allbill_new B WHERE B.JOBID='" + ID + "'";

            string qry1 = @"select B.WORKSHOP WH,B.REGNO COUNIT,B.MODELNAME,B.COMPANYNAME COMPANY,B.CATEGORYNAME CATEGORY,B.ITEMNAME,B.UNIT UNITNAME,B.QTY,ROUND((B.RATE*B.QTY)/(B.QTY),2) RATE,
ROUND((B.RATE*B.QTY),2) AMOUNT,B.CUSTOMERNAME,B.CDID--,0 Total_AMOUNT
from v_rvs_allbill_new B WHERE B.JOBID='" + ID + @"'
union all
select CAST ('' AS NVARCHAR2 (50)) WH,CAST ('' AS NVARCHAR2 (70)) COUNIT,CAST ('' AS NVARCHAR2 (50)) MODELNAME,CAST ('' AS NVARCHAR2 (100)) COMPANY,
CAST ('' AS VARCHAR2 (50)) CATEGORY,CAST ('' AS VARCHAR2 (80)) ITEMNAME,CAST ('Total' AS VARCHAR2 (20)) UNITNAME,CAST ('' AS NUMBER) QTY,CAST ('' AS NUMBER) RATE,
round(sum(B.RATE*B.QTY),2) AMOUNT,CAST ('' AS NVARCHAR2 (120)) CUSTOMERNAME,CAST ('' AS VARCHAR2 (20)) CDID--,round(sum(B.RATE*B.QTY),2) Total_AMOUNT
from v_rvs_allbill_new B WHERE B.JOBID= '" + ID + @"'";

            DataTable dt1 = dm.ExecuteQuery(qry1);
            //string path = @"Files\Cancelled-JobOrder-" + DateTime.Today.ToString("dd-MM-yyyy") + ".csv";

            //            string qry2 = @"select round(sum(B.RATE*B.QTY),2) Total_AMOUNT
            //from v_rvs_allbill_new B WHERE B.JOBID='" + ID + "'";
            //            DataTable dt2 = dm.ExecuteQuery(qry2);

            string path = "";
            if (dt1.Rows.Count > 0)
            {
                //CreateCsvFile(dt1, path);
                path = ExportToPdf(dt1);
            }

            if (Approver.Count > 0)
            {
                SendEmail.SendMultipleMailWithPath(Approver, "Job card has closed", sbd, path);
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            string err = ex.Message;
        }


    }

    public string ExportToPdf(DataTable dt)
    {
        string path = Server.MapPath("~/Content/ClosedJobCard/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
        document.Open();
        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


        ///


        ///
        PdfPTable table = new PdfPTable(dt.Columns.Count);
        PdfPRow row = null;
        float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };

        table.SetWidths(widths);

        table.WidthPercentage = 100;

        //Creating paragraph for header  


        BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLUE);
        Paragraph prgHeading = new Paragraph();
        prgHeading.Alignment = Element.ALIGN_CENTER;
        prgHeading.Add(new Chunk("Consumption Report".ToUpper()));
        document.Add(prgHeading);


        //Adding a line  
        Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        document.Add(p);

        //Adding line break  
        document.Add(new Chunk("\n", fntHead));

        int iCol = 0;
        string colname = "";
        PdfPCell cell = new PdfPCell(new Phrase("Products"));

        cell.Colspan = dt.Columns.Count;

        foreach (DataColumn c in dt.Columns)
        {

            table.AddCell(new Phrase(c.ColumnName, font5));
        }

        foreach (DataRow r in dt.Rows)
        {
            if (dt.Rows.Count > 0)
            {
                table.AddCell(new Phrase(r[0].ToString(), font5));
                table.AddCell(new Phrase(r[1].ToString(), font5));
                table.AddCell(new Phrase(r[2].ToString(), font5));
                table.AddCell(new Phrase(r[3].ToString(), font5));
                table.AddCell(new Phrase(r[4].ToString(), font5));
                table.AddCell(new Phrase(r[5].ToString(), font5));
                table.AddCell(new Phrase(r[6].ToString(), font5));
                table.AddCell(new Phrase(r[7].ToString(), font5));
                table.AddCell(new Phrase(r[8].ToString(), font5));
                table.AddCell(new Phrase(r[9].ToString(), font5));
                table.AddCell(new Phrase(r[10].ToString(), font5));
                table.AddCell(new Phrase(r[11].ToString(), font5));
            }
        }
        document.Add(table);
        document.Close();
        return path;
    }

    //public void CreateCsvFile(DataTable dt, string strFilePath)
    //{
    //    try
    //    {
    //        StreamWriter sw = new StreamWriter(strFilePath, false);
    //        int iColCount = dt.Columns.Count;
    //        for (int i = 0; i < iColCount; i++)
    //        {
    //            sw.Write(dt.Columns[i]);
    //            if (i < iColCount - 1)
    //            {
    //                sw.Write(",");
    //            }
    //        }
    //        sw.Write(sw.NewLine);


    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            for (int i = 0; i < iColCount; i++)
    //            {
    //                if (!Convert.IsDBNull(dr[i]))
    //                {
    //                    sw.Write(dr[i].ToString());
    //                }


    //                if (i < iColCount - 1)
    //                {
    //                    sw.Write(",");
    //                }
    //            }

    //            sw.Write(sw.NewLine);
    //        }
    //        sw.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

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
            //BindRptPart();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail + ex.Message);
        }

    }

    protected void rptDetailPart_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            partQtyTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[9]);
            partActTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[10]);
            partMarginTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[11]);
            partAmtTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[12]);
        }
    }

    protected void rptDetailSer_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            serQtyTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[5]);
            serActTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[6]);
            serMarginTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[7]);
            serAmtTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[8]);
        }
    }

    protected void rptDetailProblemServiceCharge_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //serQtyTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[5]);
            //serActTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[6]);
            //serMarginTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[7]);
            ProblemserAmtTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[6]);
        }
    }


    protected void btnCdPrint_OnClick(object sender, EventArgs e)
    {
        string qry = @"SELECT RM.ID CDNO,RM.IDATE CDDATE,LO.CENTERNAME FROMLOC,WH.VISMWHNAME FROMWH,WS.WORKSHOP FROMCOM,RM.IUSER CREATEDBY,
LOT.CENTERNAME TOLOC, VH.REGNO TOWH,CO.COMPANYNAME TOCOM,RM.RECVREMARKS FROMREMARKS,
IM.ITEMCODE,IM.ITEMNAME,UT.UNITNAME,RD.QTY,RD.REMARKS FROM RVS_REQDETAIL RD
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
WHERE RM.ID='" + (string)ViewState["TrId"] + @"'
ORDER BY RD.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/pages/VISM/CrReport/CrCdPrint.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);
    }
}

namespace iTextSharp.text
{
    public class BaseColor
    {
        internal static Color BLACK;
        internal static Color BLUE;
    }
}