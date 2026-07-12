using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_CustWrksp : BasePage
{
    dalVISMCustomer obj = new dalVISMCustomer();
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + System.IO.Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            ViewState["ID"] = (int)0;
            LoadDdl();
            //BindData();

        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //Allows for printing
    }
    public override bool EnableEventValidation
    {
        get { return false; }
        set { /*Do nothing*/ }
    }
    private void BindData(string crt)
    {
        rpt.DataSource = obj.GetCustomer(crt);
        rpt.DataBind();
    }
    private void LoadDdl()
    {
        ListItem li = new ListItem("All", "");
        ddlGender.DataSource = new Common().GetAll("BS_GENDER");
        ddlGender.DataBind();
        ddlGender.Items.Insert(0, li);
        ddlReligion.DataSource = new Common().GetAll("BS_RELIGION");
        ddlReligion.DataBind();
        ddlReligion.Items.Insert(0, li);

    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            obj.Insert(tbxCustomerName.Text, tbxMobile.Text.Trim(), tbxPhone.Text, "", tbxEmail.Text, Convert.ToInt32(ddlGender.SelectedValue), tbxAddress.Text, 1,
                1, 1, 1, "", tbxNid.Text, tbxPassport.Text, tbxBirthCertificate.Text,
                tbxTin.Text, Convert.ToInt32(ddlReligion.SelectedValue), tbxProfession.Text, "", "", "", "", "");

            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            //BindData();
            ClearInputs(Controls);
            
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);
        }
    }
    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            obj.Update(Convert.ToInt32(ViewState["ID"]), tbxCustomerName.Text, tbxMobile.Text.Trim(), tbxPhone.Text, "", tbxEmail.Text, Convert.ToInt32(ddlGender.SelectedValue), tbxAddress.Text, 1,
                   1, 1, 1, "", tbxNid.Text, tbxPassport.Text, tbxBirthCertificate.Text,
                   tbxTin.Text, Convert.ToInt32(ddlReligion.SelectedValue), tbxProfession.Text, "", "", "", "", "");

            //qry = @"";
            //dm.ExecuteNonQuery(qry);
            MessageBox(UpdateSuccess);
            ChangeToSaveMode(Controls);
            ClearInputs(Controls);
            
        }
        catch (Exception ex)
        {
            MessageBox(UpdateFail + ex.Message);
        }
        //try
        //{
        //    obj.Update(Convert.ToInt32(ViewState["ID"]), tbxCustomerName.Text, tbxMobile.Text.Trim(), tbxPhone.Text, "", tbxEmail.Text, Convert.ToInt32(ddlGender.SelectedValue), tbxAddress.Text, 1,
        //        1, 1, 1, "", tbxNid.Text, tbxPassport.Text, tbxBirthCertificate.Text,
        //        tbxTin.Text, Convert.ToInt32(ddlReligion.SelectedValue), tbxProfession.Text, "", "", "", "", "");

        //    MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
        //    //BindData();
        //    ClearAll();
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteLog(ex);
        //    MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        //}
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearInputs(Controls);
    }
    //protected void ClearAll()
    //{
    //    tbxAddress.Text = "";
    //    tbxBirthCertificate.Text = "";
    //    tbxCustomerName.Text = "";
    //    tbxMobile.Text = "";
    //    tbxNid.Text = "";
    //    tbxPassport.Text = "";
    //    tbxPhone.Text = "";
    //    tbxProfession.Text = "";
    //    tbxTin.Text = "";
    //    tbxEmail.Text = "";
    //    btnSave.Visible = true;
    //    btnUpdate.Visible = false;
    //}

    //protected void btnEdit_Command(object sender, CommandEventArgs e)
    //{
    //    int ID = Convert.ToInt32(e.CommandArgument);
    //    DataTable dt = obj.GetById(ID);
    //    if (dt.Rows.Count > 0)
    //    {
    //        tbxAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
    //        tbxBirthCertificate.Text = dt.Rows[0]["BIRTHCERTIFICATE"].ToString();
    //        tbxCustomerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
    //        tbxMobile.Text = dt.Rows[0]["MOBILE"].ToString();
    //        tbxNid.Text = dt.Rows[0]["NID"].ToString();
    //        tbxPassport.Text = dt.Rows[0]["PASSPORT"].ToString();
    //        tbxPhone.Text = dt.Rows[0]["PHONE"].ToString();
    //        tbxProfession.Text = dt.Rows[0]["PROFESSION"].ToString();
    //        tbxTin.Text = dt.Rows[0]["TIN"].ToString();
    //        tbxEmail.Text = dt.Rows[0]["EMAIL"].ToString();
    //        ddlReligion.SelectedValue = dt.Rows[0]["RELIGIONID"].ToString();
    //        ddlGender.SelectedValue = dt.Rows[0]["GENDERID"].ToString();
    //        ViewState["ID"] = ID;
    //    }
    //    btnSave.Visible = false;
    //    btnUpdate.Visible = true;
    //}
    //protected void btnDelete_Command(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        int ID = Convert.ToInt32(e.CommandArgument);
    //        new Common().Delete("RVS_CUSTOMER", ID);
    //        //BindData();
    //        MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteLog(ex);
    //        MessageController.Show(ex.Message, MessageType.Error, Page);
    //    }
    //}

    private string crt = " WHERE 1=1 ";
    protected void btnCustomerSearch_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        BindRpt();
    }
    protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            qry = @"SELECT * FROM RVS_CUSTOMER WHERE ID='" + e.CommandArgument + "'";
            DataTable dt = dm.ExecuteQuery(qry);
            if (dt.Rows.Count > 0)
            {
                tbxAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                tbxBirthCertificate.Text = dt.Rows[0]["BIRTHCERTIFICATE"].ToString();
                tbxCustomerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                tbxMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                tbxNid.Text = dt.Rows[0]["NID"].ToString();
                tbxPassport.Text = dt.Rows[0]["PASSPORT"].ToString();
                tbxPhone.Text = dt.Rows[0]["PHONE"].ToString();
                tbxProfession.Text = dt.Rows[0]["PROFESSION"].ToString();
                tbxTin.Text = dt.Rows[0]["TIN"].ToString();
                tbxEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                ddlReligion.SelectedValue = dt.Rows[0]["RELIGIONID"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["GENDERID"].ToString();
                ViewState["ID"] = e.CommandArgument.ToString();
                ChangeToUpdateMode(Controls);
            }
            
            

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
            qry = @"DELETE FROM RVS_CUSTOMER WHERE ID='" + e.CommandArgument + "'";
            dm.ExecuteNonQuery(qry);
            MessageBox(DeleteSuccess);
            BindRpt();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail + ex.Message);
        }

    }
    private void BindRpt()
    {
        rptMain.DataSource = RptData();
        rptMain.DataBind();
        if (rptMain.Items.Count>0)
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
            qry = @"Select CU.ID,CU.CUSTOMERNAME,CU.MOBILE,CU.ADDRESS,CU.EMAIL,CU.NID from RVS_CUSTOMER CU " + crt + " ORDER BY CU.CUSTOMERNAME";

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
    private void GetCriteria()
    {
        if (tbxCustomerName.Text != "") crt += " AND CUSTOMERNAME='" + tbxCustomerName.Text + "'";
        if (tbxMobile.Text != "") crt += " AND MOBILE='" + tbxMobile.Text + "'";
        if (tbxPhone.Text != "") crt += " AND PHONE='" + tbxPhone.Text + "'";
        if (tbxEmail.Text != "") crt += " AND EMAIL='" + tbxEmail.Text + "'";
        if (tbxAddress.Text != "") crt += " AND ADDRESS='" + tbxAddress.Text + "'";
        if (ddlGender.SelectedValue != "") crt += " AND GENDERID='" + ddlGender.SelectedValue + "'";
        if (tbxNid.Text != "") crt += " AND NID='" + tbxNid.Text + "'";
        if (tbxPassport.Text != "") crt += " AND PASSPORT='" + tbxPassport.Text + "'";
        if (tbxBirthCertificate.Text != "") crt += " AND BIRTHCERTIFICATE='" + tbxBirthCertificate.Text + "'";
        if (tbxTin.Text != "") crt += " AND TIN='" + tbxTin.Text + "'";
        if (ddlReligion.SelectedValue != "") crt += " AND RELIGIONID='" + ddlReligion.SelectedValue + "'";
        if (tbxProfession.Text != "") crt += " AND PROFESSION='" + tbxProfession.Text + "'";

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

    //protected void btndownloadExcel_OnClick(object sender, EventArgs e)
    //{
    //    //GetCriteria();
    //    //string qry = GetQry();
    //    //DataTable dt = dm.ExecuteQuery(qry);
    //    //string path = "/Pages/ProductionReport/ReportFile/CrProductionDetail.rpt";
    //    //GetCrReport(path, dt, ReportAction.DownloadExcel);
    //    ScriptManager.RegisterStartupScript(this,GetType(),"UnderdevelopmentMsg","alert('Under Development')",true );
    //}

    protected void lnkDownloadExcel_OnClick(object sender, EventArgs e)
    {
        try
        {
            ExportToExcel(rptMain);
        }
        catch (Exception ex)
        {
            MessageBox("Error: " + ex.Message);
        }
    }
}