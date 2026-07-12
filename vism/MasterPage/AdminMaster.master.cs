using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            if (Common.SessionInfo == null)
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];
            if (cookie != null && cookie.Value != null)
            {
                if (cookie.Value.IndexOf("en-") >= 0)
                {
                    ddlLanguage.SelectedValue = "en-US";
                }
                else
                {
                    ddlLanguage.SelectedValue = "bn-BD";
                }
            }
            lblYear.Text = DateTime.Now.Year.ToString();
            if (Page.Title != "")
            {
                litPage.Text = Page.Title;
            }
            BuCenterId = SessionManager.SessionName.BusinessCentetid;
            CompanyId = SessionManager.SessionName.CompanyId;
            deptId = SessionManager.SessionName.DepartmentId;
            DesigId = SessionManager.SessionName.DesignationId;
            StaffId = SessionManager.SessionName.StaffId;
            RoleId = SessionManager.SessionName.RoleId;
            Page.Header.DataBind();
        }
        else
        {
            SessionManager.SessionName.BusinessCentetid = BuCenterId;
            SessionManager.SessionName.CompanyId = CompanyId;
            SessionManager.SessionName.DepartmentId = deptId;
            SessionManager.SessionName.DesignationId = DesigId;
            SessionManager.SessionName.StaffId = StaffId;
            SessionManager.SessionName.UserId = StaffId;
            SessionManager.SessionName.UserName = StaffId;
            SessionManager.SessionName.RoleId = RoleId;
        }
    }

    string StaffId
    {
        set { ViewState["StaffId"] = value; }
        get
        {
            try
            {
                return (string)ViewState["StaffId"];
            }
            catch
            {
                return "";
            }
        }
    }
    int RoleId
    {
        set { ViewState["RoleId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["RoleId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    int BuCenterId
    {
        set { ViewState["BuCenterId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["BuCenterId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    int DesigId
    {
        set { ViewState["DesigId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["DesigId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    int CompanyId
    {
        set { ViewState["CompanyId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["CompanyId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    int deptId
    {
        set { ViewState["deptId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["deptId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    int LocationId
    {
        set { ViewState["LocationId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["LocationId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    int ModuleId
    {
        set { ViewState["ModuleId"] = value; }
        get
        {
            try
            {
                return (int)ViewState["ModuleId"];
            }
            catch
            {
                return 0;
            }
        }
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("CurrentLanguage");
        cookie.Value = ddlLanguage.SelectedValue;
        cookie.Expires = DateTime.Now.AddMonths(6);
        Response.SetCookie(cookie);
        Response.Cookies.Add(cookie);
        Response.Redirect(Request.RawUrl);
    }
}
