using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


public partial class Pages_VISM_RptToolsTrn : BasePage
{
    dalGatein obj = new dalGatein();
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
            LoadDDL();
        }
    }

    private void LoadDDL()
    {
        ListItem li = new ListItem("All", "");
        string qry =
            @"select ID id,WORKSHOP text from RVS_WORKSHOP order by workshop";
        ddlwksp.DataSource = dm.ExecuteQuery(qry);
        ddlwksp.DataBind();
        ddlwksp.Items.Insert(0, li);

    }

    protected void ddlwksp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("All", "");
        string qry = @"SELECT TM.STAFFID id,TM.STAFFID||'_'||T.TEAMNAME ||'_'||E.EMPLOYEENAME text FROM RVS_TEAMMEMBER TM
        INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
        where TM.WORKSHOPID='" + ddlwksp.SelectedValue + "'";
        ddlTech.DataSource = dm.ExecuteQuery(qry);
        ddlTech.DataBind();
        ddlTech.Items.Insert(0, li);
        // ddlTech.Enabled = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
        criteria = "";
      //  BindRpt();
    }


    private string criteria = "";
    private void GetCriteria()
    {

        if (ddlwksp.SelectedValue != "") criteria += " AND (td.WKSPID='" + ddlwksp.SelectedValue + "')";
        if (ddlTech.Text != "") criteria += " AND (td.STAFFID='" + ddlTech.SelectedValue + "')";


    }
    private string GetQry()
    {


        string qry = @"SELECT E.EMPLOYEENAME,TI.ITEMNAME,TI.SCMUNITCODE,TD.QTY,SC.CATNAME,WK.WORKSHOP,TD.REMARKS,TO_CHAR(TD.CREATEDATE,'DD/MM/YYYY') DT from RVS_TOOLSDTL TD
INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TD.STAFFID
INNER JOIN RVS_TOOLSITEMS TI ON TI.TOOLITEMID=TD.TOOLITEMID
INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=TI.SERVICECATEGORYID 
inner join RVS_WORKSHOP wk on wk.ID=TD.WKSPID
where td.CREATEDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('"+ tbxtodate.Text + "','DD/MM/YYYY')" + criteria + @" ORDER BY TD.CREATEDATE DESC";

        return qry;
    }
//    private void BindRpt()
//    {
//        qry = @"select td.*,E.EMPLOYEENAME,TI.ITEMNAME,TI.SCMUNITCODE,SC.CATNAME from RVS_TOOLSDTL TD
//INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TD.STAFFID
//INNER JOIN RVS_TOOLSITEMS TI ON TI.TOOLITEMID=TD.TOOLITEMID
//INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=TI.SERVICECATEGORYID 
//where td.CREATEDATE BETWEEN to_date('01/02/2020','DD/MM/YYYY') AND to_date('29/02/2020','DD/MM/YYYY') and td.WKSPID='10' and td.STAFFID='239076'";
//        DtCommon = dm.ExecuteQuery(qry);
//        rptMain.DataSource = DtCommon;
//        rptMain.DataBind();
//    }

    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        //dalCommon.ExportToExcel(pnlExport, DateTime.Now.ToString("ddMMyyyyHHMM"));
    }
}
