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

public partial class Pages_VISM_ItemLedgerFuel : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
            //BindRpt();
        }
    }

    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "");
        string qry =
            @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlLocation.DataSource = dm.ExecuteQuery(qry);
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, li);


        qry = @"select BUSINESSCENTERID from EM_EMPLOYEE where USERID='" + SessionManager.SessionName.UserId + "'";
        ddlLocation.SelectedValue = dm.ExecuteScalar(qry);
        ddlLocation_OnSelectedIndexChanged(null, null);
    }




    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);

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

    protected void btndownloadNormalExcelonFUEL_OnClick(object sender, EventArgs e)
    {
       


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
WHERE  trunc(OP.TRDATE)= to_date('01/" + tbxfromdate.Text.Substring(3, 7) + @"','DD/MM/YYYY') AND OP.WHCODE='" + ddlWh.SelectedValue + @"'
UNION ALL
SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
FROM V_RVS_RE_ALL RE
WHERE RE.WHCODE='" + ddlWh.SelectedValue + @"' AND RE.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
UNION ALL
SELECT TRDATE,TRID,CATEGORYCODE,CATEGORYNAME,ITEMCODE,ITEMNAME,UNITCODE,UNITNAME,OPRATE,OPQTY,OPAMT,RERATE,REQTY,REAMT,ISRATE,ISQTY,ISAMT
FROM V_RVS_FUEL_IS_ALL IU
WHERE IU.WHCODE='" + ddlWh.SelectedValue + @"' AND IU.TRDATE BETWEEN to_date('" + tbxfromdate.Text + @"','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + @"','DD/MM/YYYY')
)
WHERE ITEMCODE='0525736000'
GROUP BY CATEGORYNAME,ITEMCODE,ITEMNAME,UNITNAME
order by CATEGORYNAME,ITEMCODE) A,
(SELECT IW.RATE MRATE,IW.ITEMCODE FROM RVS_ITEMMASTER_WH IW 
WHERE IW.WHID='" + ddlWh.SelectedValue + @"' AND ITEMCODE='0525736000') B
WHERE A.ITEMCODE=B.ITEMCODE(+)";


        DataTable dt = dm.ExecuteQuery(qry);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ExportGridFuelITEMLEDGERExcel();
    }


    private void ExportGridFuelITEMLEDGERExcel()
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
}