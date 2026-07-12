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

public partial class Pages_VISM_JobCardWithCo : BasePage
{
    DbOra db = new DbOra();
    dalJobCard obj = new dalJobCard();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    protected static decimal partCdTot = 0;
    protected static decimal partCoTot = 0;

    protected static decimal serAmtTot = 0;
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

                DtDetailPartAddColumn();
                DtDetailSerAddColumn();
                BindRptPart();
                BindRptSer();
            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
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
        DtDetailPart.Columns.Add("CdQty");
        DtDetailPart.Columns.Add("CoQty");
        DtDetailPart.Columns.Add("CdTotalAmount");
        DtDetailPart.Columns.Add("CoTotalAmount");
        DtDetailPart.Columns.Add("CdRemarks");
        DtDetailPart.Columns.Add("CoRemarks");
    }
    private void BindRptPart()
    {
        partCdTot = 0;
        partCoTot = 0;
        DtDetailPart.AcceptChanges();
        rptDetailPart.DataSource = DtDetailPart;
        rptDetailPart.DataBind();
        CalculateBill();
    }
    private void BindRptSer()
    {
        serAmtTot = 0;
        DtDetailSer.AcceptChanges();
        rptDetailSer.DataSource = DtDetailSer;
        rptDetailSer.DataBind();
        CalculateBill();
    }


    private void CalculateBill()
    {
        tbxBillTotal.Text = (partCoTot + serAmtTot).ToString("F2");

    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {

            string qry = @"UPDATE RVS_BILL
SET TOTALBILL='" + tbxBillTotal.Text + "',DISCOUNTPERCENT='" + tbxBillDisc.Text + @"',
DISCOUNTAMOUNT='" + tbxBillDiscAmt.Text + "',NETBILL='" + tbxBillNetAmt.Text + "',REMARKS='" + tbxBillRemarks.Text + @"'
 WHERE JOBID IN (SELECT ID  FROM RVS_JOBCARD JC WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1)";
            dm.ExecuteNonQuery(qry);
            MessageBox(SaveSuccess);

        }
        catch (Exception ex)
        {
            MessageBox(SaveFail + ex.Message);
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }

    }


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }



    protected void tbxRecVeh_OnTextChanged(object sender, EventArgs e)
    
    {

        string qry = @"SELECT MO.MODELNAME,CU.CUSTOMERNAME,LO.CENTERNAME,CO.COMPANYNAME,ST.SERVICETYPE,TO_CHAR(PR.CREATEDATE,'DD/MM/YYYY') EFFDATE FROM RVS_VEHICLE VH
INNER JOIN BS_BUSINESSCENTER LO ON VH.BUSINESSCENTERID=LO.ID
INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
INNER JOIN RVS_MODEL MO ON VH.MODELID=MO.ID
INNER JOIN RVS_CUSTOMER CU ON VH.CUSTOMERID=CU.ID
INNER JOIN RVS_PREINSPECTION PR ON PR.VEHICLEID=VH.ID
INNER JOIN RVS_SERVICETYPE ST ON PR.SERVICETYPEID=ST.ID
WHERE VH.ID='" + tbxRecVeh.Text.Split('-')[0] + "' AND PR.ISOPEN=1";
        DataTable dt = dm.ExecuteQuery(qry);
        tbxModel.Text = dt.Rows[0]["MODELNAME"].ToString();
        tbxCustomer.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
        tbxLoc.Text = dt.Rows[0]["CENTERNAME"].ToString();
        tbxCom.Text = dt.Rows[0]["COMPANYNAME"].ToString();
        tbxSerType.Text = dt.Rows[0]["SERVICETYPE"].ToString();
        tbxEffectiveDate.Text = dt.Rows[0]["EFFDATE"].ToString();

        //        qry = @"SELECT LO.CENTERNAME,WH.VISMWHNAME,WS.WORKSHOP,RM.ID FROM RVS_JOBCARD JC 
        //INNER JOIN RVS_VEHICLE VH ON JC.VEHICLEID=VH.ID
        //INNER JOIN RVS_REQMASTER RM ON JC.ID=RM.JOBID
        //INNER JOIN BS_BUSINESSCENTER LO ON RM.SENDLOCATION=LO.ID
        //INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
        //INNER JOIN RVS_WORKSHOP WS ON RM.SENDCOMPANY=WS.ID
        //WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";

        qry = @"SELECT LO.CENTERNAME,WH.VISMWHNAME,WS.WORKSHOP,TM.ID FROM RVS_JOBCARD JC 
INNER JOIN RVS_VEHICLE VH ON JC.VEHICLEID=VH.ID
LEFT OUTER JOIN RVS_REQMASTER RM ON JC.ID=RM.JOBID
LEFT OUTER JOIN RVS_TRMASTER TM ON TM.REFTRID=RM.ID
LEFT OUTER JOIN BS_BUSINESSCENTER LO ON RM.SENDLOCATION=LO.ID
LEFT OUTER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
INNER JOIN RVS_WORKSHOP WS ON JC.WORKSHOPID=WS.ID
WHERE JC.VEHICLEID='" + tbxRecVeh.Text.Split('-')[0] + "' AND JC.ISOPEN=1 AND JC.ISACTIVE=1";


        dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count > 0)
        {
            tbxSendLoc.Text = dt.Rows[0]["CENTERNAME"].ToString();
            tbxSendWh.Text = dt.Rows[0]["VISMWHNAME"].ToString();
            tbxSendCom.Text = dt.Rows[0]["WORKSHOP"].ToString();
            string cd = "";
            foreach (DataRow row in dt.Rows)
            {
                cd += row["ID"].ToString() + "\n";
            }
            tbxCd.Text = cd;
            LoadParts(Convert.ToInt32(tbxRecVeh.Text.Split('-')[0]));
            LoadCharges(Convert.ToInt32(tbxRecVeh.Text.Split('-')[0]));


            tbxBillDisc.Text = "0";
            tbxBillDiscAmt.Text =
                 (Convert.ToDecimal(tbxBillTotal.Text) * Convert.ToDecimal(tbxBillDisc.Text) / 100).ToString("F2");
            tbxBillNetAmt.Text =
                (Convert.ToDecimal(tbxBillTotal.Text) -
                 Convert.ToDecimal(tbxBillDiscAmt.Text)).ToString("F2");

        }




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
        DtDetailSer.Columns.Add("ServiceAmount");
        DtDetailSer.Columns.Add("ServiceRemarks");
    }





    private void LoadParts(int id)
    {
//        string qry = @"SELECT RD.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME, RD.ITEMCODE,IM.ITEMNAME,UT.UNITCODE, UT.UNITNAME, RD.RATE,RD.MARGIN,RD.MARGINRATE,
//SUM(NVL(RD.QTY,0)) CDQTY,SUM(NVL(TD.QTY,0)) COQTY,SUM(NVL(RD.TOTALAMT,0)) CDAMOUNT,SUM(NVL(TD.RATE*TD.QTY,0)) COAMOUNT,
//RD.REMARKS CDREMARKS,TD.REMARKS COREMARKS
//FROM RVS_REQDETAIL RD
//INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID AND RM.REQTYPE='CD' AND RM.ISACTIVE=1
//INNER JOIN RVS_JOBCARD JC ON RM.JOBID=JC.ID
//INNER JOIN RVS_VEHICLE VH ON JC.VEHICLEID=VH.ID
//LEFT JOIN RVS_TRMASTER TM ON  TM.REFTRID=RM.ID AND TM.TRTYPE='CO' AND TM.ISACTIVE=1
//LEFT JOIN RVS_TRDETAIL TD ON TD.TRMASTERID=TM.ID AND RD.ITEMCODE=TD.ITEMCODE
//INNER JOIN RVS_ITEMMASTER IM ON RD.ITEMCODE=IM.ITEMCODE
//INNER JOIN RVS_ITEMCATEGORY CT ON IM.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE
//INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE=UT.UNITCODE
//WHERE JC.VEHICLEID='" + id + @"' AND JC.ISOPEN=1 AND JC.ISACTIVE=1
//GROUP BY RD.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME,
//RD.ITEMCODE,IM.ITEMNAME,UT.UNITCODE,UT.UNITNAME,
//RD.RATE,RD.MARGIN,RD.MARGINRATE,RD.REMARKS,TD.REMARKS
//HAVING SUM(NVL(TD.QTY,0))>0
//ORDER BY IM.ITEMNAME";

        string qry = @"select RD.ITEMCATEGORYCODE,ct.ITEMCATEGORYNAME,im.ITEMCODE,im.ITEMNAME,UT.UNITCODE,
ut.UNITNAME,ji.RATE,ji.MARGIN,ji.MARGINRATE ,RD.QTY CDQTY,TD.QTY COQTY,(RD.QTY*ji.MARGINRATE) CDAMOUNT,
(TD.QTY*ji.MARGINRATE) COAMOUNT,RD.REMARKS CDREMARKS,TD.REMARKS COREMARKS
from rvs_jobcarditems ji
INNER JOIN RVS_JOBCARD JC ON ji.JOBID=JC.ID
INNER JOIN RVS_REQMASTER RM ON RM.JOBID=JI.JOBID AND RM.REQTYPE='CD' AND RM.ISACTIVE=1 AND RM.ISCOMPLETE='1'
INNER JOIN RVS_REQDETAIL RD ON RD.REQMASTERID=RM.ID AND RD.ITEMCODE=JI.ITEMCODE AND RD.REQMASTERID=JI.CDID
LEFT JOIN RVS_TRMASTER TM ON  TM.REFTRID=RM.ID AND TM.TRTYPE='CO' AND TM.ISACTIVE=1
LEFT JOIN RVS_TRDETAIL TD ON TD.TRMASTERID=TM.ID AND JI.ITEMCODE=TD.ITEMCODE
inner join RVS_ITEMmaster im on ji.ITEMCODE=im.ITEMCODE
inner join RVS_ITEMCATEGORY ct on im.ITEMCATEGORYCODE=ct.ITEMCATEGORYCODE
inner join RVS_SCMUNIT ut on im.SCMUNITCODE=ut.UNITCODE
inner join RVS_BILL bl on bl.JOBID=ji.JOBID
where JC.VEHICLEID='" + id+ "' AND JC.ISOPEN=1";

        DataTable dt = dm.ExecuteQuery(qry);
        DtDetailPart.Rows.Clear();
        DtDetailPart.AcceptChanges();
        foreach (DataRow r in dt.Rows)
        {
            DtDetailPart.Rows.Add(r["ITEMCATEGORYCODE"].ToString(), r["ITEMCATEGORYNAME"].ToString(),
                r["ITEMCODE"].ToString(), r["ITEMNAME"].ToString(),
                r["UNITCODE"].ToString(), r["UNITNAME"].ToString(), r["RATE"].ToString(), r["MARGIN"].ToString(), r["MARGINRATE"].ToString(),
                r["CDQTY"].ToString(), r["COQTY"].ToString(), r["CDAMOUNT"].ToString(), r["COAMOUNT"].ToString(),
                r["CDREMARKS"].ToString(), r["COREMARKS"].ToString());
          
        }

        BindRptPart();
    }
    private void LoadCharges(int id)
    {
        string qry = @"SELECT CM.ID,CM.CHARGENAME,JC.RATE,JC.MARGIN,JC.MARGINRATE,JC.QUANTITY,
JC.TOTALAMOUNT,JC.REMARKS
FROM RVS_JOBCARDSCCHARGE JC
INNER JOIN RVS_JOBCARD JB ON JC.JOBID=JB.ID
LEFT JOIN RVS_CHARGENAME CM ON JC.CHARGEID=CM.ID
INNER JOIN RVS_VEHICLE VH ON JB.VEHICLEID=VH.ID
WHERE JB.VEHICLEID ='" + id + @"' AND JB.ISOPEN=1
AND JB.ISACTIVE=1 ORDER BY JC.LINE";
        DataTable dt = dm.ExecuteQuery(qry);
        DtDetailSer.Rows.Clear();
        DtDetailSer.AcceptChanges();
        foreach (DataRow r in dt.Rows)
        {
            DtDetailSer.Rows.Add(r["ID"].ToString(), r["CHARGENAME"].ToString(),
                r["RATE"].ToString(), r["MARGIN"].ToString(),
                r["MARGINRATE"].ToString(), r["QUANTITY"].ToString(),
                r["TOTALAMOUNT"].ToString(), r["REMARKS"].ToString());
        }
        BindRptSer();
    }








    protected void rptDetailPart_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            partCdTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[11]);
            partCoTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[12]);
        }
    }

    protected void rptDetailSer_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            serAmtTot += Convert.ToDecimal(((DataRowView)e.Item.DataItem).Row.ItemArray[6]);
        }
    }


    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
//        string qry = @"SELECT B.*,to_CHAR(sysdate,'DD-Mon-YYYY') CURDATE from dual, V_RVS_BILL B
//WHERE VEHICLEID='" + tbxRecVeh.Text.Split('-')[0].Trim() + "'";

        string qry = @"SELECT * from V_RVS_BILL_NEW
WHERE VEHICLEID='" + tbxRecVeh.Text.Split('-')[0].Trim() + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/pages/VISM/CrReport/CrPrintBill.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);

    }
}