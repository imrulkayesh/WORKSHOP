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
            LoadDdl();
          //  Panel2.Visible = false;
            //BindRpt();
        }
    }

    private void LoadDdl()
    {

        ListItem li = new ListItem("All", "");
        string qry =
            @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlLocation.DataSource = dm.ExecuteQuery(qry);
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, li);
    }

    protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlLocation.SelectedValue + "' and id not in ('8086','5') order by VISMWHNAME";
        ddlWh.DataSource = dm.ExecuteQuery(qry);
        ddlWh.DataBind();
        ddlWh.Items.Insert(0, li);
        ddlWh.Enabled = true;
    }

    protected void btnShow_OnClickClick(object sender, EventArgs e)
    {
        GetCriteria();
        BindRpt();
    }

    private string criteria = "";
    private string criteriaB = "";
    private void GetCriteria()
    {

        if (ddlWh.SelectedValue != "") criteria += " AND WHCODE='" + ddlWh.SelectedValue + "'";

        if (ddlWh.SelectedValue != "") criteriaB += " AND WHID='" + ddlWh.SelectedValue + "'";

    }


    private void BindRpt()
    {
        GridView2.DataSource = RptData();
        GridView2.DataBind();
    }
    private bool _firstTime = true;
    public DataTable RptData()
    {
        DataTable dt = new DataTable();
        if (_firstTime)
        {
            //string opdate = "01-" + tbxfromdate.Text.ToDate().Substring(3, 3) + "-" + tbxfromdate.Text.ToDate().Substring(7, 4);
            //string ffdate = Convert.ToDateTime(tbxfromdate.Text.ToDate()).AddDays(-1).ToString("dd/MM/yyyy").ToDate();


            string qry = @"select A.WHNAME, A.CATEGORYNAME,A.ITEMCODE,A.ITEMNAME,A.UNITNAME,NVL(B.MRATE,0) RATE,A.OPQTY,A.OPAMT,A.REQTY,A.REAMT,A.ISQTY,A.ISAMT,A.CLQTY,NVL((B.MRATE*A.CLQTY),0) CLAMT from
            (SELECT WHCODE,WHNAME,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME,
            trunc((((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY))+((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY))-((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY)))/decode((SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY)),0,1,(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY))),2) RATE,
            trunc(SUM(OPQTY),2) OPQTY,trunc((SUM(OPRATE*OPQTY)/DECODE(SUM(OPQTY),0,1,SUM(OPQTY)))*SUM(OPQTY),2) OPAMT,
            round(SUM(REQTY),2) REQTY,round((SUM(RERATE*REQTY)/DECODE(SUM(REQTY),0,1,SUM(REQTY)))* SUM(REQTY),2) REAMT,
            round(SUM(ISQTY),2) ISQTY,round((SUM(ISRATE*ISQTY)/DECODE(SUM(ISQTY),0,1,SUM(ISQTY)))*SUM(ISQTY),2) ISAMT,
            round(SUM(OPQTY)+SUM(REQTY)-SUM(ISQTY),2) CLQTY
            FROM   
            (
            SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT,WHNAME,WHCODE 
            FROM V_RVS_OP_ALL OP
            WHERE WHCODE not in ('8086','5') and trunc(OP.TRDATE)= to_date('01/" + tbxfromdate.Text.Substring(3, 7) + @"','DD/MM/YYYY') " + criteria + @" 
            UNION ALL
            SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT,WHNAME,WHCODE 
            FROM V_RVS_RE_ALL RE
            WHERE WHCODE not in ('8086','5') and RE.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY') " + criteria + @" 
            UNION ALL
            SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT,WHNAME,
            CAST (WHCODE AS INTEGER) WHCODE 
            FROM V_RVS_IS_ALL IU
            WHERE WHCODE not in ('8086','5') and IU.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY') " + criteria + @")
            GROUP BY WHCODE,WHNAME,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME
            order by WHCODE,WHNAME,CATEGORYNAME,ITEMCODE) A,
            (SELECT IW.RATE MRATE,IW.ITEMCODE,IW.WHID FROM RVS_ITEMMASTER_WH IW where 1 = 1 " + criteriaB + @") B
            WHERE A.ITEMCODE=B.ITEMCODE(+)
            AND A.WHCODE=B.WHID(+)
            order by A.WHCODE asc";


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

    protected void btndownloadNormalExcel_OnClick(object sender, EventArgs e)
    {
        ExportGridToImpToDistExcel();
    }

    private void ExportGridToImpToDistExcel()
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "COrpt" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView2.GridLines = GridLines.Both;
            GridView2.HeaderStyle.Font.Bold = true;
            GridView2.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception Ex)
        {
            // lblError.Text = Ex.Message.ToString();
        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    //protected void rptMain_ItemBound(object sender, RepeaterItemEventArgs e)
    //{

    //    if (e.Item.ItemType == ListItemType.Header)
    //    {
    //        Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
    //        headerRpt.DataSource = GetHeader(RptData());
    //        headerRpt.DataBind();
    //    }
    //    else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {

    //        Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;

    //        bodyRpt.DataSource = DtToList(_rw, RptData());

    //        bodyRpt.DataBind();
    //        _rw++;
    //        //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
    //        totalAmt += Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[12]);
    //    }

    //}
    //public static string[] GetHeader(DataTable dt)
    //{
    //    List<string> myList = new List<string>();
    //    for (int i = 0; i < dt.Columns.Count; i++)
    //    {

    //        myList.Add(dt.Columns[i].ColumnName);

    //    }

    //    return myList.ToArray();
    //}

    //public static string[] DtToList(int rw, DataTable dt)
    //{
    //    List<string> lt = new List<string>();
    //    for (int i = 0; i < dt.Columns.Count; i++)
    //    {
    //        lt.Add(dt.Rows[rw][i].ToString());
    //    }

    //    string[] sa = lt.ToArray();
    //    return sa;
    //}


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);

    }
  
}