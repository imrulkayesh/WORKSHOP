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

public partial class Pages_VISM_CoForm_New : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
            DtDetailAddColumn();
            tbxRecVeh.Focus();
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

      //  ListItem li = new ListItem("Select", "");
        qry =@"select ID,CATAGORYNAME text  from RVS_COCATAGORY";
        ddlCOcatagory.DataSource = dm.ExecuteQuery(qry);
        ddlCOcatagory.DataBind();
        ddlCOcatagory.Items.Insert(0, li);

        ddlCOcatagory.Enabled = true;



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
        DtDetailItem.Columns.Add("Margin");
        DtDetailItem.Columns.Add("MarginRate");
        DtDetailItem.Columns.Add("Qty");
        DtDetailItem.Columns.Add("TotalAmount");

    }
    private void BindRpt()
    {
        DtDetailItem.AcceptChanges();
        rpt.DataSource = DtDetailItem;
        rpt.DataBind();
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
                cmd.CommandText = @"SELECT V.ID||'-'||V.REGNO VEHICLE FROM RVS_VEHICLE V
                INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
                INNER JOIN RVS_JOBCARD JC ON JC.VEHICLEID=V.ID
                WHERE (C.MOBILE LIKE '" + prefixText + "%' OR LOWER(V.ID||'-'||V.REGNO||'-'||V.CHASSISNO||'-'||C.CUSTOMERNAME) LIKE LOWER('%" + prefixText + "%'))  AND ROWNUM <= 20  AND JC.ISOPEN='1'";
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

    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    {
        string qry = @"SELECT DISTINCT ID FROM RVS_JOBCARD JC
WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";
        string jobid = dm.ExecuteScalar(qry);
        if (string.IsNullOrWhiteSpace(jobid))
        {
            MessageBox("No job card open for this vehicle.");
            tbxRecVeh.Attributes.Add("onfocus", "javascript:this.select();");
            tbxRecVeh.Focus();
            return;
        }



        tbxJobCard.Text = jobid.ToString();
        qry = @"SELECT VH.BUSINESSCENTERID,LO.CENTERNAME,VH.COMPANYID,CO.COMPANYNAME,VH.MODELID,MO.MODELNAME,VH.CUSTOMERID,CU.CUSTOMERNAME FROM RVS_VEHICLE VH
INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
INNER JOIN rvs_model MO on VH.MODELID=MO.ID
INNER JOIN RVS_CUSTOMER CU on VH.CUSTOMERID=CU.ID
WHERE VH.ID='" + tbxRecVeh.Text.Split('-')[0] + "'";

        DataTable dtd = dm.ExecuteQuery(qry);
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TEXT");
        dt.Rows.Add(dtd.Rows[0]["BUSINESSCENTERID"].ToString(), dtd.Rows[0]["CENTERNAME"].ToString());
        ddlRecvLoc.DataSource = dt;
        ddlRecvLoc.DataBind();
        dt.Clear();

        //dt.Rows.Add(dtd.Rows[0]["RECVWH"].ToString(), dtd.Rows[0]["REGNO"].ToString());
        //ddlRecvWh.DataSource = dt;
        //ddlRecvWh.DataBind();
        //dt.Clear();

        dt.Rows.Add(dtd.Rows[0]["COMPANYID"].ToString(), dtd.Rows[0]["COMPANYNAME"].ToString());
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

        //dt.Rows.Add(dtd.Rows[0]["CATEGORYCODE"].ToString(), dtd.Rows[0]["CATAGORY"].ToString());
        //ddlCOcatagory.DataSource = dt;
        //ddlCOcatagory.DataBind();
        //dt.Clear();

        //ListItem li = new ListItem("Select", "");
        //qry = @"select ITEMCATEGORYCODE id,ITEMCATEGORYNAME text  from RVS_ITEMCATEGORY where isactive=1 order by ITEMCATEGORYNAME";
        //ddlCategory.DataSource = dm.ExecuteQuery(qry);
        //ddlCategory.DataBind();
        //ddlCategory.Items.Insert(0, li);
    }

    protected void tbxItem_OnTextChanged(object sender, EventArgs e)
    {
        string SL = ddlSendLoc.SelectedValue;
        string SW = ddlSendWh.SelectedValue;

        //        string qry = @"SELECT IM.SCMUNITCODE,UT.UNITNAME,IM.RATE,IM.MARGIN,IM.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME FROM RVS_ITEMMASTER IM
        //INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE=UT.UNITCODE
        //INNER JOIN RVS_ITEMCATEGORY CT ON IM.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE WHERE IM.ISACTIVE=1 AND IM.ITEMCODE='" + tbxItem.Text.Split('-')[0] + "'";

        string qry = @"SELECT IM.SCMUNITCODE,UT.UNITNAME,IW.RATE,IM.MARGIN,IM.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME FROM RVS_ITEMMASTER IM
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
               // LoadItems();
            }
        }
    }

    //private static void LoadItems()
    //{
    //    string SL = ddlSendLoc.SelectedValue;
    //    string SW = ddlSendWh.SelectedValue;
    //}

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
            DtDetailItem.Rows.Add(tbxCategory.Text, tbxCategoryName.Text, tbxItem.Text.Split('-')[0].Trim(), tbxItem.Text.Trim().Substring(tbxItem.Text.IndexOf('-') + 1).Trim(), tbxUnitCode.Text, hdnUnitName.Value, tbxRate.Text, tbxMargin.Text, tbxMarginRate.Text, tbxQty.Text.Trim(), tbxAmt.Text);
            BindRpt();
            tbxItem.Text = "";
            tbxUnitCode.Text = "";
            tbxRate.Text = "";
            tbxMargin.Text = "";
            tbxMarginRate.Text = "";
            tbxQty.Text = "";
            tbxAmt.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxItem').focus();", true);
        }
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
            qry = @"INSERT INTO RVS_TRMASTER(ID,TRTYPE,SENDLOCATION,SENDWH,SENDCOMPANY,SENDDATE,SENDREMARKS,SENDUSER,
RECVLOCATION,RECVWH,RECVCOMPANY,ISCOMPLETE,IUSER,CATEGORYCODE,RECVID) VALUES(
'" + trid + "','" + trType + "','" + ddlSendLoc.SelectedValue + "','" + ddlSendWh.SelectedValue + "','" + ddlSendCom.SelectedValue + @"',TO_DATE(SYSDATE,'DD/MM/YYYY'),'" + tbxSendRemarks.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + @"',
'" + ddlRecvLoc.SelectedValue + "','" + tbxRecVeh.Text.Split('-')[0] + "','" + ddlRecvCom.SelectedValue + "','1','" + SessionManager.SessionName.UserId + "','" + ddlCOcatagory.SelectedValue + "','"+ tbxRecvID.Text + "')";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailItem.Rows)
            {
                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                qry = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,
ITEMCODE,UNITID,QTY,RATE,TOTALAMT,LINE,IUSER,WH,RECVWH) values
('" + trdId + "','" + trid + "','" + r["CategoryCode"] + "','" + r["ItemCode"] + @"',
'" + r["UnitCode"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["TotalAmount"] + "','" + i + "','" + SessionManager.SessionName.UserId + "','" + ddlSendWh.SelectedValue + "','" + tbxRecVeh.Text.Split('-')[0] + "')";
                db.ExecuteNonQuery(qry);
                i++;


                qry = @"insert into RVS_JOBCARDITEMS (JOBID,PARTSID,QUANTITY,ITEMCODE,RATE,MARGIN,MARGINRATE,TOTALAMOUNT,MARGINAMOUNT,LINE,AMOUNT,CDID,COID) values
('" + tbxJobCard.Text + "','0','" + r["Qty"] + "','" + r["ItemCode"] + "','" + r["Rate"] +
      "','" + r["Margin"] + "','" + r["MarginRate"] + "','" + r["TotalAmount"] + "','" + r["TotalAmount"] + "','" + i + "','0','" + trid + "','"+ trid + "')";
                db.ExecuteNonQuery(qry);
                qry = @"update RVS_BILL set TOTALBILL=TOTALBILL+'" + r["TotalAmount"] + "' where jobid='" + tbxJobCard.Text + "'";
                db.ExecuteNonQuery(qry);
            }

            //foreach (DataRow r in DtDetailItem.Rows)
            //{
            //    qry = @"update ya.RVS_JOBCARDITEMS set QUANTITY='" + r["Qty"] + "',TOTALAMOUNT='" + r["TotalAmount"] + "',COID='" + trid + "' WHERE CDID='" + ddlCdId.SelectedValue + "' and ITEMCODE='" + r["ItemCode"] + "'";
            //    db.ExecuteNonQuery(qry);
            //    i++;
            //}
            //qry = @"UPDATE RVS_REQMASTER SET ISCOMPLETE=1 WHERE ID='" + ddlCdId.SelectedValue + "' AND ISACTIVE=1";
            //db.ExecuteNonQuery(qry);
            db.Commit();
            btnSave.Enabled = false;
            tbxCoId.Text = trid;
            tbxCoId.BackColor = Color.DarkCyan;
            tbxCoId.ForeColor = Color.White;
            tbxCoId.Enabled = true;
            tbxCoId.ReadOnly = true;
            //ddlCdId.Enabled = false;
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

        //if (ddlRecvLoc.SelectedValue == ddlSendLoc.SelectedValue)
        //{
        //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("disabled", "disabled");
        //    ddlSendWh.Items.FindByValue(ddlRecvWh.SelectedValue).Attributes.Add("style", "color: red!important;text-decoration: line-through!important;");

        //}
        //try
        //{
        //    qry = @"select workshopid from em_employee where staffid='" + SessionManager.SessionName.UserId + "'";
        //    string wh = dm.ExecuteScalar(qry);
        //    ddlSendWh.SelectedValue = wh;
        //    ddlSendWh_OnSelectedIndexChanged(null, null);
        //}
        //catch
        //{
        //    //
        //}

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


//        li = new ListItem("Select", "");
//        qry = @"SELECT RQ.ID,RQ.ID||' - '||TO_CHAR(RQ.IDATE,'DD/MM/YYYY') TEXT FROM RVS_REQMASTER RQ
//WHERE RQ.ISACTIVE=1 AND RQ.ISCOMPLETE=0 AND RQ.REQTYPE='CD'
// AND RQ.SENDLOCATION='" + ddlSendLoc.SelectedValue + @"' 
// AND RQ.SENDWH='" + ddlSendWh.SelectedValue + @"' 
// AND RQ.SENDCOMPANY='" + ddlSendCom.SelectedValue + @"'
// ORDER BY RQ.IDATE Desc";
//        ddlCdId.DataSource = dm.ExecuteQuery(qry);
//        ddlCdId.DataBind();
//        ddlCdId.Items.Insert(0, li);
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
 //       ddlItemName.Enabled = true;

 //   }

//    protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
//    {


//        string SL = ddlSendLoc.SelectedValue;
//        string SW = ddlSendWh.SelectedValue;

//        //        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME, FROM RVS_ITEMMASTER IM
//        //inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE where im.isactive=1 and IM.ITEMCODE='" + ddlItemName.SelectedValue + "'";

//        string qry = @"SELECT im.SCMUNITCODE,ut.UNITNAME,IW.RATE FROM RVS_ITEMMASTER IM
//inner join rvs_scmunit ut on im.scmunitcode=ut.UNITCODE 
//INNER JOIN RVS_ITEMMASTER_WH IW ON IM.ITEMCODE=IW.ITEMCODE where im.isactive=1 AND IW.ISACTIVE='1' and IM.ITEMCODE='" + ddlItemName.SelectedValue + "' AND IW.LOCATIONID='" + ddlSendLoc.SelectedValue + "' AND IW.WHID='" + ddlSendWh.SelectedValue + "'";

//        DataTable dt = dm.ExecuteQuery(qry);
//        tbxUnitCode.Text = dt.Rows[0]["SCMUNITCODE"].ToString();
//        hdnUnitName.Value = dt.Rows[0]["UNITNAME"].ToString();
//        tbxRate.Text = dt.Rows[0]["RATE"].ToString();
//        tbxQty.Text = "";
//        tbxAmt.Text = "";
//        tbxQty.Enabled = true;
//    }

//    protected void ddlCdId_OnSelectedIndexChanged(object sender, EventArgs e)
//    {


 

//        string qry = @"SELECT RM.RECVLOCATION,LO.CENTERNAME, RM.RECVWH,WH.REGNO, RM.RECVCOMPANY,WS.COMPANYNAME ,RD.ITEMCATEGORYCODE,RD.ITEMCATEGORYNAME,
//RD.ITEMCODE,RD.ITEMNAME,RD.UNITID,RD.UNITNAME,RD.RATE,JI.MARGIN,JI.MARGINRATE, RD.QTY,RD.TOTALAMT,RD.REMARKS,WH.MODELID,MO.MODELNAME,WH.CUSTOMERID,CU.CUSTOMERNAME,RM.CATEGORYCODE,CC.CATAGORYNAME CATAGORY FROM RVS_REQDETAIL RD
//INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
//LEFT outer JOIN RVS_JOBCARDITEMS JI ON RM.JOBID=JI.JOBID AND RD.ITEMCODE=JI.ITEMCODE AND RD.REQMASTERID=JI.CDID
//INNER JOIN BS_BUSINESSCENTER LO ON RM.RECVLOCATION=LO.ID
//INNER JOIN RVS_VEHICLE WH ON RM.RECVWH=WH.ID
//INNER JOIN BS_COMPANY WS ON RM.RECVCOMPANY=WS.ID
//INNER JOIN rvs_model MO on WH.MODELID=MO.ID
//INNER JOIN RVS_CUSTOMER CU on WH.CUSTOMERID=CU.ID
//LEFT OUTER JOIN RVS_COCATAGORY CC on RM.CATEGORYCODE=CC.ID
//WHERE RD.REQMASTERID='" + ddlCdId.SelectedValue + @"'
//ORDER BY RD.LINE";


//        DataTable dtd = dm.ExecuteQuery(qry);
//        DataTable dt = new DataTable();
//        dt.Columns.Add("ID");
//        dt.Columns.Add("TEXT");
//        dt.Rows.Add(dtd.Rows[0]["RECVLOCATION"].ToString(), dtd.Rows[0]["CENTERNAME"].ToString());
//        ddlRecvLoc.DataSource = dt;
//        ddlRecvLoc.DataBind();
//        dt.Clear();

//        dt.Rows.Add(dtd.Rows[0]["RECVWH"].ToString(), dtd.Rows[0]["REGNO"].ToString());
//        ddlRecvWh.DataSource = dt;
//        ddlRecvWh.DataBind();
//        dt.Clear();

//        dt.Rows.Add(dtd.Rows[0]["RECVCOMPANY"].ToString(), dtd.Rows[0]["COMPANYNAME"].ToString());
//        ddlRecvCom.DataSource = dt;
//        ddlRecvCom.DataBind();
//        dt.Clear();

//        dt.Rows.Add(dtd.Rows[0]["CUSTOMERID"].ToString(), dtd.Rows[0]["CUSTOMERNAME"].ToString());
//        ddlRecvCustomer.DataSource = dt;
//        ddlRecvCustomer.DataBind();
//        dt.Clear();

//        dt.Rows.Add(dtd.Rows[0]["MODELID"].ToString(), dtd.Rows[0]["MODELNAME"].ToString());
//        ddlRecvModel.DataSource = dt;
//        ddlRecvModel.DataBind();
//        dt.Clear();

//        dt.Rows.Add(dtd.Rows[0]["CATEGORYCODE"].ToString(), dtd.Rows[0]["CATAGORY"].ToString());
//        ddlCOcatagory.DataSource = dt;
//        ddlCOcatagory.DataBind();
//        dt.Clear();


//        DtDetailItem.Clear();
//        foreach (DataRow r in dtd.Rows)
//        {
//            DtDetailItem.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
//                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
//                r["UNITID"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["MARGINRATE"].ToString(), r["QTY"].ToString(),
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
            DataRow rows = DtDetailItem.Select("ItemCode = '" + e.CommandArgument + "'").FirstOrDefault();
            tbxCategory.Text = rows["CategoryCode"].ToString();
            tbxCategoryName.Text = rows["CategoryName"].ToString();
            tbxItem.Text = e.CommandArgument.ToString() + " - " + rows["ItemName"].ToString(); ;
            tbxUnitCode.Text = rows["UnitCode"].ToString();
            tbxRate.Text = rows["Rate"].ToString();
            tbxQty.Text = rows["Qty"].ToString();
            tbxMargin.Text = rows["Margin"].ToString();
            tbxMarginRate.Text = rows["MarginRate"].ToString();
            tbxAmt.Text = rows["TotalAmount"].ToString();
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