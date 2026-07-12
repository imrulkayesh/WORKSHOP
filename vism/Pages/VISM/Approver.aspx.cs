using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Approver : BasePage
{
    dalVISMApprover obj = new dalVISMApprover();
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
            LoadDdl();
            BindData();

        }

    }
    private void BindData()
    {
        if (!string.IsNullOrEmpty(ddlBusinessCenter.SelectedValue))
        {
            rpt.DataSource = obj.GetAll(Convert.ToInt32(ddlBusinessCenter.SelectedValue));
        }
        else
        {
            rpt.DataSource = null;
        }
        rpt.DataBind();
    }
    private void LoadDdl()
    {
        ddlBusinessCenter.DataSource = new Common().GetAll("BS_BUSINESSCENTER");
        ddlBusinessCenter.DataBind();
        ddlDepartment.DataSource = new Common().Get_AllDesc("BS_DEPARTMENT");
        ddlDepartment.DataBind();
        ddlCompany.DataSource = new Common().GetAll("BS_COMPANY");
        ddlCompany.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Insert(Convert.ToInt32(ddlCompany.SelectedValue), tbxStaffID.Text.Trim(), Convert.ToInt32(tbxPriority.Text), Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
            BindData();
            ClearAll();

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
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(ddlCompany.SelectedValue), tbxStaffID.Text.Trim(), Convert.ToInt32(tbxPriority.Text), Convert.ToInt32(ddlBusinessCenter.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
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
        tbxName.Text = "";
        tbxEmail.Text = "";
        tbxMobile.Text = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxStaffID.Text = dt.Rows[0]["APPROVERID"].ToString();
            tbxPriority.Text = dt.Rows[0]["PRIORITY"].ToString();
            tbxName.Text = dt.Rows[0]["EMPLOYEENAME"].ToString();
            tbxMobile.Text = dt.Rows[0]["OFFICEMOBILE"].ToString();
            tbxEmail.Text = dt.Rows[0]["OFFICEMAIL"].ToString();
            ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
            ddlBusinessCenter.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
            ddlDepartment.SelectedValue = dt.Rows[0]["DEPARTMENTID"].ToString();
            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_APPROVER", ID);
            BindData();
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(ex.Message, MessageType.Error, Page);
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchEmployee(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"Select E.STAFFID||'-'||B.CENTERCODE ||'-'|| D.DEPARTMENTCODE ||'-'||DE.DESIGNATIONNAME||'-'||E.EMPLOYEENAME Employee from EM_EMPLOYEE E inner join BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID INNER JOIN BS_LOCATIONS L ON L.ID=B.LOCATIONID
INNER JOIN BS_COUNTRY C ON  C.ID=L.COUNTRYID INNER JOIN BS_DEPARTMENT D on D.ID=E.DEPARTMENTID Inner join BS_DESIGNATION DE on DE.ID=E.DESIGNATIONID where L.NAME like '" + prefixText + "%' or B.CENTERNAME like '" + prefixText + "%' or E.EMPLOYEENAME like '" + prefixText + "%' or E.STAFFID like '" + prefixText + "%' AND ROWNUM <= 50";


                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Employee"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    protected void tbxStaffID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] stfid = tbxStaffID.Text.Trim().Split('-');
            DataTable dt = new dalDispatch().GetEmployeeDetails(stfid[0]);
            if (dt.Rows.Count > 0)
            {
                //tbxName.Text = dt.Rows[0]["EMPLOYEENAME"].ToString();
                //tbxStaffID.Text = dt.Rows[0]["STAFFID"].ToString();
                //tbxMobile.Text = dt.Rows[0]["OFFICEMOBILE"].ToString();
                //tbxEmail.Text = dt.Rows[0]["OFFICEMAIL"].ToString();


                tbxName.Text = dt.Rows[0]["EMPLOYEENAME"].ToString();
                tbxStaffID.Text = dt.Rows[0]["STAFFID"].ToString();
                ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
                ddlBusinessCenter.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DEPARTMENTID"].ToString();
                tbxMobile.Text = dt.Rows[0]["OFFICEMOBILE"].ToString();
                tbxEmail.Text = dt.Rows[0]["OFFICEMAIL"].ToString();
            }
        }
        catch (Exception ex)
        {
            tbxStaffID.Text = "";
        }
    }
    protected void ddlBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}