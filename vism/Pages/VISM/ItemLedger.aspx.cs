using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing;

public partial class Pages_VISM_ItemLedger : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //BindRpt();
            SelectWorkshop();
            LoadDdl();
        }
    }

    private void SelectWorkshop()
    {
        try
        {
            //qry = @"select ws.id from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
            //SessionManager.SessionName.UserId + "')";
            //string id = dm.ExecuteScalar(qry);
            //ddlWorkShop.SelectedValue = id;


            try
            {

                string BI = SessionManager.SessionName.BusinessCentetid.ToString();
                ListItem li = new ListItem("Select", "");
                string qry = "";
                if (BI == "705")
                {
                    qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
                    ddlLocation.DataSource = dm.ExecuteQuery(qry);
                    ddlLocation.DataBind();

                    string qry1 = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
               ddlLocation.SelectedValue + "' order by VISMWHNAME";
                    ddlWh.DataSource = dm.ExecuteQuery(qry1);
                    ddlWh.DataBind();
                    //ddlWh.Items.Insert(0, li);

                }
                else
                {
                    qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
                    ddlLocation.DataSource = dm.ExecuteQuery(qry);
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, li);


                    qry = @"select BUSINESSCENTERID from EM_EMPLOYEE where USERID='" + SessionManager.SessionName.UserId + "'";
                    ddlLocation.SelectedValue = dm.ExecuteScalar(qry);
                    ddlLocation_OnSelectedIndexChanged(null, null);

                    string qry1 = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
             ddlLocation.SelectedValue + "' order by VISMWHNAME";
                    ddlWh.DataSource = dm.ExecuteQuery(qry1);
                    ddlWh.DataBind();
                    //  ddlWh.Items.Insert(0, li);
                }



            }
            catch
            {

                //
            }
        }
        catch
        {

            //
        }

    }

    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "");
        //string qry =
        //    @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        //ddlLocation.DataSource = dm.ExecuteQuery(qry);
        //ddlLocation.DataBind();
        //ddlLocation.Items.Insert(0, li);



        qry =
           @"select itemcode id,itemcode||'-'||itemname text  from rvs_itemmaster order by itemcode";
        ddlFromItem.DataSource = dm.ExecuteQuery(qry);
        ddlFromItem.DataBind();
        ddlFromItem.Items.Insert(0, li);
        qry =
            @"select itemcode id,itemcode||'-'||itemname text  from rvs_itemmaster order by itemcode";
        ddlToItem.DataSource = dm.ExecuteQuery(qry);
        ddlToItem.DataBind();
        ddlToItem.Items.Insert(0, li);

        //qry = @"select BUSINESSCENTERID from EM_EMPLOYEE where USERID='" + SessionManager.SessionName.UserId + "'";
        //ddlLocation.SelectedValue = dm.ExecuteScalar(qry);
        //ddlLocation_OnSelectedIndexChanged(null, null);
    }

    protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlLocation.SelectedValue + "' order by VISMWHNAME";
        ddlWh.DataSource = dm.ExecuteQuery(qry);
        ddlWh.DataBind();
        ddlWh.Items.Insert(0, li);
    }

    private void BindRpt()
    {
        rptMain.DataSource = RptData();
        rptMain.DataBind();
        if (rptMain.Items.Count > 0)
        {
            pnlReportButton.Visible = true;
        }
        else
        {
            pnlReportButton.Visible = false;
        }
    }
    private bool _firstTime = true;
    public DataTable RptData()
    {
        DataTable dt = new DataTable();
        if (_firstTime)
        {
            //string opdate = "01-" + tbxfromdate.Text.ToDate().Substring(3, 3) + "-" + tbxfromdate.Text.ToDate().Substring(7, 4);
            //string ffdate = Convert.ToDateTime(tbxfromdate.Text.ToDate()).AddDays(-1).ToString("dd/MM/yyyy").ToDate();

            //string qry = @"select A.CATEGORYNAME,A.ITEMCODE,A.ITEMNAME,A.UNITNAME,NVL(B.MRATE,0) RATE,A.OPQTY,A.OPAMT,A.REQTY,A.REAMT,A.ISQTY,A.ISAMT,A.CLQTY,NVL((B.MRATE*A.CLQTY),0) CLAMT from
            //(
            //SELECT CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME,
            //trunc((((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY))+((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY))-((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY)))/decode((SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY)),0,1,(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY))),2) RATE,
            //trunc(SUM(OPQTY),2) OPQTY,trunc((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY),2) OPAMT,
            //round(SUM(REQTY),2) REQTY,round((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY),2) REAMT,
            //round(SUM(ISQTY),2) ISQTY,round((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY),2) ISAMT,
            //round(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY),2) CLQTY
            //FROM   
            //(
            //SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT 
            //FROM V_RVS_OP_ALL OP
            //WHERE  trunc(OP.TRDATE)= to_date('01/" + tbxfromdate.Text.Substring(3, 7) + @"','DD/MM/YYYY') AND OP.WHCODE='" + ddlWh.SelectedValue + @"'
            //UNION ALL
            //SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
            //FROM V_RVS_RE_ALL RE
            //WHERE RE.WHCODE='" + ddlWh.SelectedValue + @"' AND RE.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
            //UNION ALL
            //SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
            //FROM V_RVS_IS_ALL IU
            //WHERE IU.WHCODE='" + ddlWh.SelectedValue + @"' AND IU.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
            //)
            //WHERE ITEMCODE BETWEEN '" + ddlFromItem.SelectedValue + @"' AND '" + ddlToItem.SelectedValue + @"'
            //GROUP BY CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME
            //order by CATEGORYNAME,ITEMCODE) A,
            //(SELECT IW.RATE MRATE,IW.ITEMCODE FROM RVS_ITEMMASTER_WH IW 
            //WHERE IW.WHID='" + ddlWh.SelectedValue + @"' AND ITEMCODE BETWEEN '" + ddlFromItem.SelectedValue + @"' AND '" + ddlToItem.SelectedValue + @"') B
            //WHERE A.ITEMCODE=B.ITEMCODE(+)";

            string qry = @"select A.CATEGORYNAME,A.ITEMCODE,A.ITEMNAME,A.UNITNAME,NVL(B.MRATE,0) RATE,A.OPQTY,A.OPAMT,A.REQTY,A.REAMT,A.ISQTY,A.ISAMT,A.CLQTY,NVL((B.MRATE*A.CLQTY),0) CLAMT from
            (
            SELECT CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME,
            trunc((((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY))+((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY))-((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY)))/decode((SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY)),0,1,(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY))),2) RATE,
            trunc(SUM(OPQTY),2) OPQTY,trunc((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY),2) OPAMT,
            round(SUM(REQTY),2) REQTY,round((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY),2) REAMT,
            round(SUM(ISQTY),2) ISQTY,round((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY),2) ISAMT,
            round(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY),2) CLQTY
            FROM   
            (
            SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT 
            FROM V_RVS_OP_ALL OP
            WHERE  OP.TRDATE = TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY') AND OP.WHCODE='" + ddlWh.SelectedValue + @"'
            UNION ALL
            SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
            FROM V_RVS_RE_ALL RE
            WHERE RE.WHCODE='" + ddlWh.SelectedValue + @"' AND RE.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
            UNION ALL
            SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
            FROM V_RVS_IS_ALL IU
            WHERE IU.WHCODE='" + ddlWh.SelectedValue + @"' AND IU.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
            )
            WHERE ITEMCODE BETWEEN '" + ddlFromItem.SelectedValue + @"' AND '" + ddlToItem.SelectedValue + @"'
            GROUP BY CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME
            order by CATEGORYNAME,ITEMCODE) A,
            (SELECT IW.RATE MRATE,IW.ITEMCODE FROM RVS_ITEMMASTER_WH IW 
            WHERE IW.WHID='" + ddlWh.SelectedValue + @"' AND ITEMCODE BETWEEN '" + ddlFromItem.SelectedValue + @"' AND '" + ddlToItem.SelectedValue + @"') B
            WHERE A.ITEMCODE=B.ITEMCODE";


            dt = dm.ExecuteQuery(qry);
            Session["dt"] = dt;
            _firstTime = false;

        }
        else
        {
            dt = Session["dt"] as DataTable;
        }

        return dt;
    }

    protected int ID = 0;
    protected int totalAmt = 0;
    private int _rw;



    protected void rptMain_ItemBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
            headerRpt.DataSource = GetHeader(RptData());
            headerRpt.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;

            bodyRpt.DataSource = DtToList(_rw, RptData());

            bodyRpt.DataBind();
            _rw++;
            //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
            totalAmt += Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[12]);
        }

    }
    public static string[] GetHeader(DataTable dt)
    {
        List<string> myList = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {

            myList.Add(dt.Columns[i].ColumnName);

        }

        return myList.ToArray();
    }

    public static string[] DtToList(int rw, DataTable dt)
    {
        List<string> lt = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            lt.Add(dt.Rows[rw][i].ToString());
        }

        string[] sa = lt.ToArray();
        return sa;
    }


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);

    }
    //private string criteria = "";
    protected void btnShow_OnClickClick(object sender, EventArgs e)
    {
        //GetCriteria();
        BindRpt();
        //criteria = "";


    }

    //private void GetCriteria()
    //{

    //    if (ddlLocation.SelectedValue != "0") criteria += " AND PM.LOCATIONID='" + ddlLocation.SelectedValue + "'";


    //}

    private string GetQry()
    {
        //string opdate = "01-" + tbxfromdate.Text.ToDate().Substring(3, 3) + "-" + tbxfromdate.Text.ToDate().Substring(7, 4);
        //string ffdate = Convert.ToDateTime(tbxfromdate.Text.ToDate()).AddDays(-1).ToString("dd/MM/yyyy").ToDate();

        //        string qry = @"SELECT '"+ddlLocation.SelectedItem.Text+"' LOCATION,'"+ddlWh.SelectedItem.Text+"' WH,'"+tbxfromdate.Text+ "' FDATE,'" + tbxtodate.Text + @"' TDATE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME,
        //trunc((((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY))+((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY))-((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY)))/decode((SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY)),0,1,(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY))),2) RATE,
        //trunc(SUM(OPQTY),2) OPQTY,trunc((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY),2) OPAMT,
        //round(SUM(REQTY),2) REQTY,round((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY),2) REAMT,
        //round(SUM(ISQTY),2) ISQTY,round((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY),2) ISAMT,
        //round(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY),2) CLQTY,
        //round(((((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY))+((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY))-((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY)))/decode((SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY)),0,1,(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY))))*(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY)),2) CLAMT
        //FROM 
        //(
        //SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT 
        //FROM V_RVS_OP_ALL OP
        //WHERE  OP.TRDATE='" + opdate + @"' AND OP.WHCODE='" + ddlWh.SelectedValue + @"'
        //UNION ALL
        //SELECT TM.TRDATE,TM.ID,TD.ITEMCATEGORYCODE CATEGORYCODE,CT.ITEMCATEGORYNAME CATEGORYNAME,
        //TD.ITEMCODE,IM.ITEMNAME,TD.UNITID,UT.UNITNAME,SUM(TD.RATE*TD.QTY)/SUM(TD.QTY) OPRATE, SUM(TD.QTY*TY.TRTYPESIGN) OPQTY,
        //(SUM(TD.RATE*TD.QTY)/SUM(TD.QTY)) * SUM(TD.QTY*TY.TRTYPESIGN) OPAMT, 0 RERATE, 0 REQTY, 0 REAMT, 0 ISRATE,
        //0 ISQTY, 0 ISAMT
        //FROM RVS_TRDETAIL TD
        //INNER JOIN RVS_TRMASTER TM ON TD.TRMASTERID=TM.ID
        //INNER JOIN RVS_TRTYPE TY ON TM.TRTYPE=TY.ID
        //INNER JOIN RVS_ITEMCATEGORY CT ON TD.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE
        //INNER JOIN RVS_ITEMMASTER IM ON TD.ITEMCODE=IM.ITEMCODE
        //INNER JOIN RVS_SCMUNIT UT ON TD.UNITID=UT.UNITCODE
        //WHERE TD.QTY <> 0 AND TM.TRDATE BETWEEN '" + opdate + @"' AND '" + ffdate + @"' AND (TM.SENDWH ='" + ddlWh.SelectedValue + @"'oR TM.RECVWH='" + ddlWh.SelectedValue + @"')
        //GROUP BY  TM.TRDATE,TM.ID,TD.ITEMCATEGORYCODE ,CT.ITEMCATEGORYNAME ,
        //TD.ITEMCODE,IM.ITEMNAME,TD.UNITID,UT.UNITNAME
        //UNION ALL
        //SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
        //FROM V_RVS_RE_ALL RE
        //WHERE RE.WHCODE='" + ddlWh.SelectedValue + @"' AND RE.TRDATE BETWEEN '" + tbxfromdate.Text.ToDate() + @"' AND '" + tbxtodate.Text.ToDate() + @"'
        //UNION ALL
        //SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
        //FROM V_RVS_IS_ALL IU
        //WHERE IU.WHCODE='" + ddlWh.SelectedValue + @"' AND IU.TRDATE BETWEEN '" + tbxfromdate.Text.ToDate() + @"' AND '" + tbxtodate.Text.ToDate() + @"'
        //)
        //WHERE ITEMCODE BETWEEN '" + ddlFromItem.SelectedValue + @"' AND '" + ddlToItem.SelectedValue + @"'
        //GROUP BY CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME
        //order by CATEGORYNAME,ITEMCODE";


        //        SELECT TM.TRDATE,TM.ID,TD.ITEMCATEGORYCODE CATEGORYCODE,CT.ITEMCATEGORYNAME CATEGORYNAME,
        //TD.ITEMCODE,IM.ITEMNAME,TD.UNITID,UT.UNITNAME,SUM(TD.RATE*TD.QTY)/SUM(TD.QTY) OPRATE, SUM(TD.QTY*TY.TRTYPESIGN) OPQTY,
        //(SUM(TD.RATE*TD.QTY)/SUM(TD.QTY)) * SUM(TD.QTY*TY.TRTYPESIGN) OPAMT, 0 RERATE, 0 REQTY, 0 REAMT, 0 ISRATE,
        //0 ISQTY, 0 ISAMT
        //FROM RVS_TRDETAIL TD
        //INNER JOIN RVS_TRMASTER TM ON TD.TRMASTERID=TM.ID
        //INNER JOIN RVS_TRTYPE TY ON TM.TRTYPE=TY.ID
        //INNER JOIN RVS_ITEMCATEGORY CT ON TD.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE
        //INNER JOIN RVS_ITEMMASTER IM ON TD.ITEMCODE=IM.ITEMCODE
        //INNER JOIN RVS_SCMUNIT UT ON TD.UNITID=UT.UNITCODE
        //WHERE TD.QTY <> 0 AND TM.TRDATE BETWEEN to_date('" + opdate + @"','DD/MM/YYYY') AND to_date('" + ffdate + @"','DD/MM/YYYY') AND (TM.SENDWH ='" + ddlWh.SelectedValue + @"'oR TM.RECVWH='" + ddlWh.SelectedValue + @"')
        //GROUP BY  TM.TRDATE,TM.ID,TD.ITEMCATEGORYCODE ,CT.ITEMCATEGORYNAME ,
        //TD.ITEMCODE,IM.ITEMNAME,TD.UNITID,UT.UNITNAME
        //UNION ALL


        string qry = @"select A.LOCATION,A.WH,A.FDATE,A.TDATE,CATEGORYNAME,A.ITEMCODE,A.ITEMNAME,A.UNITNAME,NVL(B.MRATE,0)RATE,A.OPQTY,A.OPAMT,A.REQTY,A.REAMT,A.ISQTY,A.ISAMT,A.CLQTY,NVL((B.MRATE*A.CLQTY),0) CLAMT from
(
SELECT '" + ddlLocation.SelectedItem.Text + "' LOCATION,'" + ddlWh.SelectedItem.Text + "' WH,'" + tbxfromdate.Text + "' FDATE,'" + tbxtodate.Text + @"' TDATE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME,
trunc((((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY))+((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY))-((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY)))/decode((SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY)),0,1,(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY))),2) RATE,
trunc(SUM(OPQTY),2) OPQTY,trunc((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY),2) OPAMT,
round(SUM(REQTY),2) REQTY,round((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY),2) REAMT,
round(SUM(ISQTY),2) ISQTY,round((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY),2) ISAMT,
round(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY),2) CLQTY
FROM   
(
SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT 
FROM V_RVS_OP_ALL OP
WHERE  OP.TRDATE=to_date('01/" + tbxfromdate.Text.Substring(3, 7) + @"','DD/MM/YYYY') AND OP.WHCODE='" + ddlWh.SelectedValue + @"'
UNION ALL
SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
FROM V_RVS_RE_ALL RE
WHERE RE.WHCODE='" + ddlWh.SelectedValue + @"' AND RE.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
UNION ALL
SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
FROM V_RVS_IS_ALL IU
WHERE IU.WHCODE='" + ddlWh.SelectedValue + @"' AND IU.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
)
WHERE ITEMCODE BETWEEN '" + ddlFromItem.SelectedValue + @"' AND '" + ddlToItem.SelectedValue + @"'
GROUP BY CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME
order by CATEGORYNAME,ITEMCODE) A,
(SELECT IW.RATE MRATE,IW.ITEMCODE FROM RVS_ITEMMASTER_WH IW 
WHERE IW.WHID='" + ddlWh.SelectedValue + @"' AND ITEMCODE BETWEEN '" + ddlFromItem.SelectedValue + @"' AND '" + ddlToItem.SelectedValue + @"') B
WHERE A.ITEMCODE=B.ITEMCODE(+)";

        return qry;
    }
    protected void btnOpenPdf_OnClick(object sender, EventArgs e)
    {
        //GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrItemLedgerSummary.rpt";
        GetCrReport(path, dt, ReportAction.OpenPdf);
        //criteria = "";

    }

    protected void btnDownloadPdf_OnClick(object sender, EventArgs e)
    {
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        string path = "/Pages/VISM/CrReport/CrItemLedgerSummary.rpt";
        GetCrReport(path, dt, ReportAction.DownloadPdf);
    }


}