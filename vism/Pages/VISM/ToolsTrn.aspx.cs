using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_ToolsIteams : BasePage
{
    dalInsIteams obj = new dalInsIteams();
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
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            ViewState["ID"] = (int)0;
            LoadDDL();
            LoadTrn();
          //  BindData();
          //  LoadItem();
        }

    }

    private void LoadTrn()
    {
        string qry = @"SELECT E.EMPLOYEENAME,TI.ITEMNAME,TI.SCMUNITCODE,TD.QTY,SC.CATNAME,WK.WORKSHOP,TD.REMARKS,TO_CHAR(TD.CREATEDATE,'DD/MM/YYYY') DT,TD.ID from RVS_TOOLSDTL TD
INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TD.STAFFID
INNER JOIN RVS_TOOLSITEMS TI ON TI.TOOLITEMID=TD.TOOLITEMID
INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=TI.SERVICECATEGORYID 
inner join RVS_WORKSHOP wk on wk.ID=TD.WKSPID
where to_date(td.CREATEDATE,'dd/mm/yyyy')=to_date(sysdate,'dd/mm/yyyy')
ORDER BY TD.CREATEDATE DESC";
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
    }

    private void LoadDDL()
    {
        ListItem li = new ListItem("Select", "");
        string qry =
            @"select ID id,WORKSHOP text from RVS_WORKSHOP order by workshop";
        ddlwksp.DataSource = dm.ExecuteQuery(qry);
        ddlwksp.DataBind();
        ddlwksp.Items.Insert(0, li);

        ddlSectionCategory.DataSource = new Common().GetAllServiceCat("RVS_SERVICECATEGORY");
        ddlSectionCategory.DataBind();
        ddlSectionCategory.Items.Insert(0, li);
    }

    protected void ddlwksp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"SELECT TM.STAFFID id,TM.STAFFID||'_'||T.TEAMNAME ||'_'||E.EMPLOYEENAME text FROM RVS_TEAMMEMBER TM
        INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
        where TM.WORKSHOPID='"+ ddlwksp.SelectedValue + "'";
        ddlTech.DataSource = dm.ExecuteQuery(qry);
        ddlTech.DataBind();
        ddlTech.Items.Insert(0, li);
       // ddlTech.Enabled = true;
    }

    protected void ddlSectionCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select TOOLITEMID id,ITEMNAME text from RVS_TOOLSITEMS where SERVICECATEGORYID='" + ddlSectionCategory.SelectedValue + "'";
        ddlItemName.DataSource = dm.ExecuteQuery(qry);
        ddlItemName.DataBind();
        ddlItemName.Items.Insert(0, li);
        // ddlTech.Enabled = true;
    }

    //private void LoadItem()
    //{
    //    ListItem li = new ListItem("Select", "");
    //    string qry =
    //        @"select TOOLITEMID id,ITEMNAME text from RVS_TOOLSITEMS";
    //    ddlItemName.DataSource = dm.ExecuteQuery(qry);
    //    ddlItemName.DataBind();
    //    ddlItemName.Items.Insert(0, li);
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {

            try
            {
                decimal QTY = Convert.ToDecimal(tbxQty.Text);
                obj.InsertToolsTrn(Convert.ToInt32(ddlwksp.SelectedValue), ddlTech.SelectedValue,Convert.ToInt32(ddlItemName.SelectedValue), QTY, tbxRemarks.Text.Trim().Replace("'","''"));

                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            // BindData();
            // ClearAll();
            //   LoadDDL();
            LoadTrn();

            tbxQty.Text = "";
            tbxRemarks.Text = "";

        }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

            }


    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(ViewState["ID"].ToString());
            decimal QTY = Convert.ToDecimal(tbxQty.Text);
            obj.UpdateToolTrn(id,Convert.ToInt32(ddlwksp.SelectedValue), ddlTech.SelectedValue, Convert.ToInt32(ddlItemName.SelectedValue), QTY, tbxRemarks.Text.Trim().Replace("'", "''"));
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            BindData();
            ClearAll();

        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxQty.Text = "";
        tbxRemarks.Text = "";

        btnSave.Visible = true;
        //btnEdit.Visible = false;
    }
    protected void BindData()
    {

        GetCriteria();
        string qry = GetQry();
        DataTable dt = dm.ExecuteQuery(qry);
        rpt.DataSource = dt;
        rpt.DataBind();
        criteria = "";
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.RVS_TOOLSDTL(ID);
        if (dt.Rows.Count > 0)
        {

            ddlwksp.SelectedValue = dt.Rows[0]["WKSPID"].ToString();
            ddlTech.SelectedValue= dt.Rows[0]["STAFFID"].ToString();
            ddlSectionCategory.SelectedValue = dt.Rows[0]["SID"].ToString();
            int sc =Convert.ToInt32(dt.Rows[0]["SID"].ToString());
            loadItemName(sc);

            ddlItemName.SelectedValue= dt.Rows[0]["TOOLITEMID"].ToString();
            tbxQty.Text = dt.Rows[0]["QTY"].ToString();
            tbxRemarks.Text= dt.Rows[0]["REMARKS"].ToString();

            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }

    private void loadItemName(int sc)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select TOOLITEMID id,ITEMNAME text from RVS_TOOLSITEMS where SERVICECATEGORYID='" + sc + "'";
        ddlItemName.DataSource = dm.ExecuteQuery(qry);
        ddlItemName.DataBind();
        ddlItemName.Items.Insert(0, li);
    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().ToolsDelete("RVS_TOOLSDTL", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            LoadTrn();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
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


        string qry = @"SELECT E.EMPLOYEENAME,TI.ITEMNAME,TI.SCMUNITCODE,TD.QTY,SC.CATNAME,WK.WORKSHOP,TD.REMARKS,TO_CHAR(TD.CREATEDATE,'DD/MM/YYYY') DT,TD.ID from RVS_TOOLSDTL TD
INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TD.STAFFID
INNER JOIN RVS_TOOLSITEMS TI ON TI.TOOLITEMID=TD.TOOLITEMID
INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=TI.SERVICECATEGORYID 
inner join RVS_WORKSHOP wk on wk.ID=TD.WKSPID
where td.CREATEDATE BETWEEN to_date('" + tbxfromdate.Text + "','DD/MM/YYYY') AND to_date('" + tbxtodate.Text + "','DD/MM/YYYY')" + criteria + @" ORDER BY TD.CREATEDATE DESC";

        return qry;
    }
}