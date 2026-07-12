using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing;

public partial class Pages_VISM_ReportLastTransFind : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
        }
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

    private void LoadDdl()
    {
        //ListItem li = new ListItem("All", "");
        //string qry =
        //    @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        //ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlSendLoc.DataBind();
        //ddlSendLoc.Items.Insert(0, li);


        string BI = SessionManager.SessionName.BusinessCentetid.ToString();
        ListItem li = new ListItem("Select", "");
        string qry = "";
        if (BI == "705")
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
            ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
            ddlSendLoc.DataBind();

            string qry1 = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
             ddlSendLoc.SelectedValue + "' order by VISMWHNAME";
            ddlSendWh.DataSource = dm.ExecuteQuery(qry1);
            ddlSendWh.DataBind();
            ddlSendCom.Items.Clear();
            ddlSendWh.Enabled = true;

        }
        else
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
            ddlSendLoc.DataSource = dm.ExecuteQuery(qry);
            ddlSendLoc.DataBind();
            ddlSendLoc.Items.Insert(0, li);
        }




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

        //
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
    }

    protected void btnSearchTO_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        //        string qry = @"SELECT IA.TRID,IA.TRDATE,IA.WHCODE,IA.WHNAME,IA.RECVWH,WH.VISMWHNAME, IA.CATEGORYCODE,IA.CATEGORYNAME,IA.ITEMCODE,IA.ITEMNAME, 
        //IA.UNITCODE,IA.UNITNAME,IA.ISRATE,IA.ISQTY,IA.ISAMT FROM YA.V_RVS_IS_ALL IA,YA.RVS_VISMWH WH 
        //WHERE IA.TRID LIKE '%TO%' AND IA.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY') " + criteria + @"
        //AND IA.RECVWH=WH.ID ORDER BY IA.TRDATE DESC";
        string qry = @"SELECT
                             WHCODE,
                             WHNAME,
                             TRDATE ,
                             TRID
                        FROM
                            V_LAST_TR_TI_FIND
                        ";
        DataTable dt = dm.ExecuteQuery(qry);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ExportGridToImpToDistExcel();
    }

    private string criteria = "";
    private void GetCriteria()
    {

        if (ddlSendWh.SelectedValue != "") criteria += " AND (WHCODE='" + ddlSendWh.SelectedValue + "')";


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
            string FileName = "TIrpt" + DateTime.Now + ".xls";
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