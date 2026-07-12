using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_CustomerWiseVehicleSearch :BasePage
{
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
        }
    }

    private void LoadDdl()
    {
        ListItem li=new ListItem("All","");
        qry = @"select id,Customername text from rvs_customer";
        DataTable dt = dm.ExecuteQuery(qry);
        ddlCustomer.DataSource = dt;
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0,li);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        BindRpt();
    }
    private string crt = " WHERE 1=1 ";
    private void GetCriteria()
    {
        if (ddlCustomer.SelectedValue != "") crt += " AND CUSTOMERID='" + ddlCustomer.SelectedValue + "'";

    }
    private void BindRpt()
    {
        rptMain.DataSource = RptData();
        rptMain.DataBind();
        if (rptMain.Items.Count > 0)
        {
            pnlReportButton.Visible = true;
        }
    }
    private bool _firstTime = true;
    public DataTable RptData()
    {
        DataTable dt = new DataTable();
        if (_firstTime)
        {
            qry = @"Select vh.id,vh.REGNO,vh.CHASSISNO,vh.ENGINENO,mo.MODELNAME,mn.MANUFACTURERNAME,
vh.MANUFACTURINGYEAR,vt.VEHICLETYPENAME,ft.FUELTYPENAME,bc.CENTERNAME,
cu.CUSTOMERNAME,co.COMPANYNAME,cn.COUNTRYNAME from rvs_Vehicle vh
left join rvs_model mo on vh.MODELID=mo.ID
left join RVS_MANUFACTURER mn on vh.MANUFACTURERID=mn.ID
left join RVS_VEHICLETYPE vt on vh.VEHICLETYPEID=vt.ID
left join RVS_FUELTYPE ft on vh.FUELYTYPEID=ft.ID
left join bs_businesscenter bc on vh.BUSINESSCENTERID=bc.ID
left join RVS_CUSTOMER cu on vh.CUSTOMERID=cu.ID
left join bs_company co on vh.COMPANYID=co.id
left join bs_country cn on vh.COUNTRYID=cn.ID" + crt;

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
    private int _rw;

    protected void rptMain_ItemBound(object sender, RepeaterItemEventArgs e)
    {
        bool hideIdColumn = false;
        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
            headerRpt.DataSource = GetHeader(RptData(), hideIdColumn);
            headerRpt.DataBind();


        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            bodyRpt.DataSource = DtToList(_rw, RptData(), hideIdColumn);
            bodyRpt.DataBind();
            _rw++;
            //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
        }

    }
    protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            qry = @"SELECT * FROM rvs_Vehicle ID='" + e.CommandArgument + "'";
            DataTable dt = dm.ExecuteQuery(qry);

            ViewState["ID"] = e.CommandArgument.ToString();
            ChangeToUpdateMode(Controls);

        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
        }

    }

    protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            qry = @"DELETE FROM rvs_Vehicle WHERE ID='" + e.CommandArgument + "'";
            dm.ExecuteNonQuery(qry);
            MessageBox(DeleteSuccess);
            BindRpt();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail + ex.Message);
        }

    }
    
    private string GetQry()
    {
        string qry = @"";
        return qry;
    }
    protected void btnOpenPdf_OnClick(object sender, EventArgs e)
    {
        //GetCriteria();
        //string qry = GetQry();
        //DataTable dt = dm.ExecuteQuery(qry);
        //string path = "/Pages/ProductionReport/ReportFile/CrProductionDetail.rpt";
        //GetCrReport(path, dt, ReportAction.OpenPdf);
        ScriptManager.RegisterStartupScript(this, GetType(), "UnderdevelopmentMsg", "alert('Under Development')", true);
    }

    protected void btnDownloadPdf_OnClick(object sender, EventArgs e)
    {
        //GetCriteria();
        //string qry = GetQry();
        //DataTable dt = dm.ExecuteQuery(qry);
        //string path = "/Pages/ProductionReport/ReportFile/CrProductionDetail.rpt";
        //GetCrReport(path, dt, ReportAction.DownloadPdf);
        ScriptManager.RegisterStartupScript(this, GetType(), "UnderdevelopmentMsg", "alert('Under Development')", true);
    }

    protected void btndownloadExcel_OnClick(object sender, EventArgs e)
    {
        //GetCriteria();
        //string qry = GetQry();
        //DataTable dt = dm.ExecuteQuery(qry);
        //string path = "/Pages/ProductionReport/ReportFile/CrProductionDetail.rpt";
        //GetCrReport(path, dt, ReportAction.DownloadExcel);
        ScriptManager.RegisterStartupScript(this, GetType(), "UnderdevelopmentMsg", "alert('Under Development')", true);
    }
}