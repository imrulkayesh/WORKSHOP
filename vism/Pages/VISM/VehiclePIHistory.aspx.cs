using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_VehiclePIHistory : BasePage
{
    dalPreInspection obj = new dalPreInspection();
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
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
           
            ddlLoad();
            SelectWorkshop();
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
                    qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws where ws.BUSINESSCENTERID in (select e.BUSINESSCENTERID from  em_employee e where USERID='" +
           SessionManager.SessionName.UserId + "')";
                    ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
                    ddlWorkShop.DataBind();
                }
                else
                {
                    qry = @"select ws.id,ws.WORKSHOP from RVS_WORKSHOP ws  ORDER BY WORKSHOP ASC";
                    ddlWorkShop.DataSource = dm.ExecuteQuery(qry);
                    ddlWorkShop.DataBind();
                    ddlWorkShop.Items.Insert(0, li);
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
    private void ddlLoad()
    {
        ListItem li = new ListItem("Select", "0");
        ddlWorkShop.DataSource = new Common().GetAll("RVS_WORKSHOP" + " " + " ORDER BY WORKSHOP ASC");
        ddlWorkShop.DataBind();
        ddlWorkShop.Items.Insert(0, li);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRpt();
        //if (!string.IsNullOrEmpty(tbxfromdate.Text) && !string.IsNullOrEmpty(tbxtodate.Text) && !string.IsNullOrEmpty(ddlWorkShop.SelectedValue))
        //{
        //    DataTable dt = obj.VehiclePIHistory(Criteria());
        //    rpt.DataSource = dt;
        //    rpt.DataBind();
        //    lblfromDate.Text = tbxfromdate.Text;
        //    lbltoDate.Text = tbxtodate.Text;
        //}
        //else
        //{

        //    MessageController.Show("Data Not Found !.", MessageType.Error, Page);
        //}

    }
    private void BindRpt()
    {
        qry = @"SELECT J.ID,V.REGNO ""Reg No."",V.CHASSISNO ""Chassis No."",M.MODELNAME ""Model"",V.MILLAGE,ST.SERVICETYPE ""Service Type"",
to_char(J.CREATEDATE,'DD-MM-YYYY HH:MI AM') ""Eff. Date"",J.DRIVERNAME ""Driver Name"",v.DRIVERID ""Staff ID"",J.DRIVERMOBILE ""Driver Mobile"",J.REMARKS ""Remarks"" FROM RVS_PREINSPECTION J
        INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        LEFT OUTER JOIN RVS_BILL B ON B.JOBID=J.ID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
         WHERE J.ID>0 AND J.ISACTIVE=1 " + Criteria() + " ORDER BY J.CREATEDATE DESC";
        DtCommon = dm.ExecuteQuery(qry);
        rptMain.DataSource = DtCommon;
        rptMain.DataBind();
    }

    private int ID = 0;
    private int rn = 0;
    private  DataTable DtCommon=new DataTable();
    protected void rptMain_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
            if (headerRpt == null) return;
            headerRpt.DataSource = GetHeader(DtCommon, false);
            headerRpt.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            if (bodyRpt == null) return;
            bodyRpt.DataSource = DtToList(rn, DtCommon, false);
            bodyRpt.DataBind();
            rn++;
            //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
        }

    }
    //Reapeter bind end
    private string Criteria()
    {
        string val = "";
        if (!string.IsNullOrEmpty(ddlWorkShop.SelectedValue) && ddlWorkShop.SelectedValue != "0")
        {
            val += " AND J.WORKSHOPID=" + ddlWorkShop.SelectedValue;
        }
        if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length > 7)
        {
            val += " AND trunc(J.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
        }
        if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
        {
            val += " AND trunc(J.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
        }

        return val;
    }

    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        //dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }
}
