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

public partial class Pages_VISM_RptCRDetails : BasePage
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
            ddlRecLoc.DataSource = dm.ExecuteQuery(qry);
            ddlRecLoc.DataBind();

            string qry1 = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
            ddlRecLoc.SelectedValue + "' order by VISMWHNAME";
            ddlRecWh.DataSource = dm.ExecuteQuery(qry1);
            ddlRecWh.DataBind();
            ddlRecCom.Items.Clear();
            ddlRecWh.Enabled = true;

        }
        else
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
            ddlRecLoc.DataSource = dm.ExecuteQuery(qry);
            ddlRecLoc.DataBind();
            ddlRecLoc.Items.Insert(0, li);
        }




    }
    protected void ddlRecLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
        ddlRecLoc.SelectedValue + "' order by VISMWHNAME";
        ddlRecWh.DataSource = dm.ExecuteQuery(qry);
        ddlRecWh.DataBind();
        ddlRecWh.Items.Insert(0, li);
        ddlRecCom.Items.Clear();
        ddlRecWh.Enabled = true;

        //
    }

    protected void ddlRecWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id,ws.WORKSHOP text from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
where wh.ID='" + ddlRecWh.SelectedValue + "'";
        DataTable val = dm.ExecuteQuery(qry);
        ListItem li = new ListItem(val.Rows[0]["TEXT"].ToString(), val.Rows[0]["ID"].ToString());
        ddlRecCom.DataSource = null;
        ddlRecCom.DataBind();
        ddlRecCom.Items.Insert(0, li);
    }

    protected void btnSearchCR_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        string qry = @"select RA.TRDATE, RA.TRID,RA.WHCODE,RA.WHNAME, RA.ITEMCODE, RA.ITEMNAME, RA.UNITNAME,RA.RERATE,RA.REQTY,RA.REAMT  from V_RVS_RE_ALL RA
                    where RA.TRDATE BETWEEN to_date('" + tbxfromdate.Text + "', 'DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "', 'DD/MM/YYYY') " + criteria + @"
                    AND RA.TRID LIKE '%CR%'
                    ORDER BY RA.TRDATE ASC";
        DataTable dt = dm.ExecuteQuery(qry);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ExportGridToImpToDistExcel();
    }

    private string criteria = "";
    private void GetCriteria()
    {

        if (ddlRecWh.SelectedValue != "") criteria += " AND (RA.WHCODE='" + ddlRecWh.SelectedValue + "')";


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
            string FileName = "CRrpt" + DateTime.Now + ".xls";
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