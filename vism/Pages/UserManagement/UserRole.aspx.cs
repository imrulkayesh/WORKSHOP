using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_UserManagement_UserRole : BasePage
{
    dalUserRole obj = new dalUserRole();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);

        if (!IsPostBack)
        {
            string url = "/Pages/UserManagement/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            //Common.LoadDropdown(ddlPriority, "UC_ROLESETUP", 1, 0);
            LoadDDL();
            //BindData();
        }
    }

    private void LoadDDL()
    {
        rpt.DataSource = new Common().GetAll("UC_ROLESETUP");
        rpt.DataBind();
    }

    private bool InsertUserRole(string UserName )
    {
        bool isSelelected = false;

        obj.Delete(UserName);

        foreach (RepeaterItem r in rpt.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkRecom");
            if (chk.Checked)
            {
                isSelelected = true;

                HiddenField RoleId = (HiddenField)r.FindControl("hdRoleId");
                CheckBox chkrecom = (CheckBox)r.FindControl("chkRecom");

                obj.Insert(UserName, Convert.ToInt32(RoleId.Value), Convert.ToInt32(chkrecom.Checked));
            }
        }

        if (!isSelelected)
        {
            MessageController.Show("Please select at least One Role !", MessageType.Warning, Page);
            return false;
        }
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUserRole(tbxUserName.Text.Split('-')[0]);
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(tbxUserName.Text))
        
        {
            DataTable dt = obj.GetAll(tbxUserName.Text);
            rpt.DataSource = dt;
        }
        else
        {
            rpt.DataSource = null;
        }
        rpt.DataBind();
    }
    
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        
        btnEdit.Visible = true;
        btnSave.Visible = false;
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchStaffId(string prefixText, int count)
    {
        List<string> staffID = new List<string>();
        DataTable dt = DatabaseManagerORCL.StaticDT(@"SELECT E.STAFFID||'-'||E.EMPLOYEENAME STAFFID FROM EM_EMPLOYEE E where LOWER( E.STAFFID ) like LOWER('%" + prefixText + "%') AND ROWNUM <= 8");
        foreach (DataRow dr in dt.Rows)
        {
            staffID.Add(dr["STAFFID"].ToString());
        }
        return staffID;
    }

    protected void tbxUserName_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] staffid = tbxUserName.Text.Trim().Split('-');
            DataTable dt = obj.GetById(staffid[0]);
            if (dt.Rows.Count > 0)
            {
                tbxUserName.Text = dt.Rows[0]["STAFFID"].ToString();
            }
        }
        catch (Exception ex)
        {
            tbxUserName.Text = "";
        }

        //DataTable dtt = obj.loadRolelist(tbxUserName.Text);
        //foreach (DataRow dr in dtt.Rows)
        //{
        //    foreach (RepeaterItem i in rpt.Items)
        //    {
        //        HiddenField RoleId = (HiddenField)i.FindControl("hdRoleId");
        //        if (RoleId.Value == dr["ROLEID"].ToString())
        //        {
        //            CheckBox chkrecom = (CheckBox)i.FindControl("chkRecom");
        //            chkrecom.Checked = Convert.ToBoolean(dr["ACTIVE"]);
        //            chkrecom.Checked = false;
        //        }
        //    }
        //}


        DataTable dts = obj.loadRolelist(tbxUserName.Text);
        foreach (DataRow dr in dts.Rows)
        {
            foreach (RepeaterItem i in rpt.Items)
            {
                HiddenField RoleId = (HiddenField)i.FindControl("hdRoleId");
                if (RoleId.Value == dr["ROLEID"].ToString())
                {
                    CheckBox chkrecom = (CheckBox)i.FindControl("chkRecom");
                    chkrecom.Checked = Convert.ToBoolean(dr["ACTIVE"]);
                    chkrecom.Checked = true;
                }
            }
        }

    }
}