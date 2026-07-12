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


public partial class Pages_VISM_JobCardOpen : BasePage
{
    DbOra db = new DbOra();
    dalJobCard obj = new dalJobCard();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
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

            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
        }

    }
    private DataTable DtCommon = new DataTable();
    private int rn = 0;
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
            if (SessionManager.SessionName.RoleId == 311 || SessionManager.SessionName.RoleId == 381 || SessionManager.SessionName.UserId == "201262")
            {
                var lnkd = (ImageButton)e.Item.FindControl("btnNoWork");
                lnkd.Visible = true;
            }

        }

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
        WHERE J.ISOPEN=0 AND pi.ISOPEN=0 and pi.ISACTIVE=1 and j.ISACTIVE=1 and j.id='" + tbxJobId.Text.Trim() + "'";
        DtCommon = dm.ExecuteQuery(qry);
        rptMain.DataSource = DtCommon;
        rptMain.DataBind();
    }
    protected void btnOpen_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
            obj.OpenJobCard(ID);
            MessageBox("Job Card Open Successfull");
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
    }

}





