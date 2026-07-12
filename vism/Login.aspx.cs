//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class Login : Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        Common.SessionInfo = new bdoSessionInfo();
//        tbxUserName.Focus();
//    }
//    protected void Login1_LoggedIn(object sender, EventArgs e)
//    {
//        Response.Redirect("~/Pages/Admin/SiteMap.aspx");
//    }
//    protected void LoginButton_Click(object sender, EventArgs e)
//    {

//        dalUser objUser = new dalUser();
//        string password = Encryption.EncryptionWithSh(tbxPassword.Text);
//        DataTable dt = objUser.GetLoginInfo(tbxUserName.Text.Trim(), password);
//        if (dt.Rows.Count > 0)
//        {
//            DataTable udt = objUser.GetEmployeeInfo(tbxUserName.Text);

//            SessionManager.SessionName.UserId = dt.Rows[0]["ID"].ToString();
//            SessionManager.SessionName.UserName = dt.Rows[0]["USERNAME"].ToString();
//            SessionManager.SessionName.UserFullName = udt.Rows[0]["EMPLOYEENAME"].ToString();
//            SessionManager.SessionName.RoleId = Convert.ToInt32(dt.Rows[0]["ROLEID"]);
//            if (udt.Rows.Count > 0)
//            {
//                SessionManager.SessionName.BusinessCentetid = Convert.ToInt32(udt.Rows[0]["BUSINESSCENTERID"]);
//                SessionManager.SessionName.CompanyId = Convert.ToInt32(udt.Rows[0]["COMPANYID"]);
//                SessionManager.SessionName.DepartmentId = Convert.ToInt32(udt.Rows[0]["DEPARTMENTID"]);
//                SessionManager.SessionName.DesignationId = Convert.ToInt32(udt.Rows[0]["DESIGNATIONID"]);
//                SessionManager.SessionName.StaffId = udt.Rows[0]["STAFFID"].ToString();
//            }

//            dalTaskManager objTask = new dalTaskManager();
//            DataTable dtModule = objTask.GetModule(Convert.ToInt32(dt.Rows[0]["ROLEID"]));
//            DataTable dtTaskList = objTask.GetAllByRoleId(Convert.ToInt32(dt.Rows[0]["ROLEID"]), Convert.ToInt32(dtModule.Rows[0]["ID"]));
//            string returnUrl = "~/Pages/Admin/Default.aspx";
//            if (Request.QueryString["returnUrl"] != null)
//            {
//                DataTable dtm = new DatabaseManagerORCL().ExecuteQuery("SELECT MODULEID FROM UC_TASKMANAGER WHERE URL='" + Request.QueryString["returnUrl"].Replace("%7e", "").Replace("%2f", "/").Replace("~", "") + "'");
//                if (dtm.Rows.Count > 0)
//                {
//                    SessionManager.SessionName.ModuleId = Convert.ToInt32(dtm.Rows[0][0]);
//                    dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, Convert.ToInt32(dtm.Rows[0][0]));
//                }
//                returnUrl = Request.QueryString["returnUrl"];
//            }
//            else
//            {
//                dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, 0);
//            }
//            Response.Redirect(returnUrl);
//        }
//        else
//        {
//            FailureText.Text = "Invalid Username or Password";
//        }

//    }

//    protected void btnLoginAndChange_OnClick(object sender, EventArgs e)
//    {
//        dalUser objUser = new dalUser();
//        string password = Encryption.EncryptionWithSh(tbxPassword.Text);
//        DataTable dt = objUser.GetLoginInfo(tbxUserName.Text.Trim(), password);
//        if (dt.Rows.Count > 0)
//        {
//            DataTable udt = objUser.GetEmployeeInfo(tbxUserName.Text);

//            SessionManager.SessionName.UserId = dt.Rows[0]["ID"].ToString();
//            SessionManager.SessionName.UserName = dt.Rows[0]["USERNAME"].ToString();
//            SessionManager.SessionName.UserFullName = dt.Rows[0]["EMPLOYEENAME"].ToString();
//            SessionManager.SessionName.RoleId = Convert.ToInt32(dt.Rows[0]["ROLEID"]);
//            if (udt.Rows.Count > 0)
//            {
//                SessionManager.SessionName.BusinessCentetid = Convert.ToInt32(udt.Rows[0]["BUSINESSCENTERID"]);
//                SessionManager.SessionName.CompanyId = Convert.ToInt32(udt.Rows[0]["COMPANYID"]);
//                SessionManager.SessionName.DepartmentId = Convert.ToInt32(udt.Rows[0]["DEPARTMENTID"]);
//                SessionManager.SessionName.DesignationId = Convert.ToInt32(udt.Rows[0]["DESIGNATIONID"]);
//                SessionManager.SessionName.StaffId = udt.Rows[0]["STAFFID"].ToString();
//            }

//            dalTaskManager objTask = new dalTaskManager();
//            DataTable dtModule = objTask.GetModule(Convert.ToInt32(dt.Rows[0]["ROLEID"]));
//            DataTable dtTaskList = objTask.GetAllByRoleId(Convert.ToInt32(dt.Rows[0]["ROLEID"]), Convert.ToInt32(dtModule.Rows[0]["ID"]));

//            string returnUrl = "http://hrms.prangroup.com:8283/ya/Pages/User/Profile.aspx";
//            if (Request.QueryString["returnUrl"] != null)
//            {
//                DataTable dtm = new DatabaseManagerORCL().ExecuteQuery("SELECT MODULEID FROM UC_TASKMANAGER WHERE URL='" + Request.QueryString["returnUrl"].Replace("%7e", "").Replace("%2f", "/").Replace("~", "") + "'");
//                if (dtm.Rows.Count > 0)
//                {
//                    SessionManager.SessionName.ModuleId = Convert.ToInt32(dtm.Rows[0][0]);
//                    dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, Convert.ToInt32(dtm.Rows[0][0]));
//                }
//            }
//            else
//            {
//                dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, 0);
//            }
//            Response.Redirect(returnUrl);
//        }
//        else
//        {
//            FailureText.Text = "Invalid Username or Password";
//        }


//    }
//}

// LOGIN By HRIS API
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Common.SessionInfo = new bdoSessionInfo();
        tbxUserName.Focus();
    }
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Admin/SiteMap.aspx");
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        dalUser objUser = new dalUser();
        DataTable dt = objUser.GetLoginInfoByUserID(tbxUserName.Text.Trim());

        // =================  HRIS API start : 2  ===============================================================
        string user_hr = tbxUserName.Text.Trim();
        string password_hr = tbxPassword.Text;

        var response = "";
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://hrisapi.prangroup.com:8083/v1/Login/HrisLogin");
        httpWebRequest.Headers.Add("S_KEYL", "RxsJ4LQdkVFTv37rYfW9b6");
        httpWebRequest.Headers.Add("Authorization", "Basic YXV0aDoxMlByYW5AMTIzNDU2JA==");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        string json = new JavaScriptSerializer().Serialize(new
        {
            username = user_hr,
            password = password_hr
        });

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(json);
        }

        HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            response = streamReader.ReadToEnd();
        }

        var responseData = new JavaScriptSerializer().Deserialize<dynamic>(response);

        //================= HRIS API end  ===============================================================

        if (responseData != null && responseData["succesS_CODE"] == "2000" && dt.Rows.Count > 0)
        {
            DataTable udt = objUser.GetEmployeeInfo(tbxUserName.Text);

            SessionManager.SessionName.UserId = dt.Rows[0]["ID"].ToString();
            SessionManager.SessionName.UserName = dt.Rows[0]["USERNAME"].ToString();
            SessionManager.SessionName.UserFullName = udt.Rows[0]["EMPLOYEENAME"].ToString();
            SessionManager.SessionName.RoleId = Convert.ToInt32(dt.Rows[0]["ROLEID"]);
            if (udt.Rows.Count > 0)
            {
                SessionManager.SessionName.BusinessCentetid = Convert.ToInt32(udt.Rows[0]["BUSINESSCENTERID"]);
                SessionManager.SessionName.CompanyId = Convert.ToInt32(udt.Rows[0]["COMPANYID"]);
                SessionManager.SessionName.DepartmentId = Convert.ToInt32(udt.Rows[0]["DEPARTMENTID"]);
                SessionManager.SessionName.DesignationId = Convert.ToInt32(udt.Rows[0]["DESIGNATIONID"]);
                SessionManager.SessionName.StaffId = udt.Rows[0]["STAFFID"].ToString();
            }

            dalTaskManager objTask = new dalTaskManager();
            DataTable dtModule = objTask.GetModule(Convert.ToInt32(dt.Rows[0]["ROLEID"]));
            DataTable dtTaskList = objTask.GetAllByRoleId(Convert.ToInt32(dt.Rows[0]["ROLEID"]), Convert.ToInt32(dtModule.Rows[0]["ID"]));

            string returnUrl = "~/Pages/Admin/Default.aspx";
            if (Request.QueryString["returnUrl"] != null)
            {
                DataTable dtm = new DatabaseManagerORCL().ExecuteQuery("SELECT MODULEID FROM UC_TASKMANAGER WHERE URL='" + Request.QueryString["returnUrl"].Replace("%7e", "").Replace("%2f", "/").Replace("~", "") + "'");
                if (dtm.Rows.Count > 0)
                {
                    SessionManager.SessionName.ModuleId = Convert.ToInt32(dtm.Rows[0][0]);
                    dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, Convert.ToInt32(dtm.Rows[0][0]));
                }
                returnUrl = Request.QueryString["returnUrl"];
            }
            else
            {
                dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, 0);
            }
            Response.Redirect(returnUrl);
        }
        else
        {
            FailureText.Text = "Invalid Username or Password.";
        }
    }

    protected void btnLoginAndChange_OnClick(object sender, EventArgs e)
    {
        dalUser objUser = new dalUser();
        string password = Encryption.EncryptionWithSh(tbxPassword.Text);
        DataTable dt = objUser.GetLoginInfo(tbxUserName.Text.Trim(), password);
        if (dt.Rows.Count > 0)
        {
            DataTable udt = objUser.GetEmployeeInfo(tbxUserName.Text);

            SessionManager.SessionName.UserId = dt.Rows[0]["ID"].ToString();
            SessionManager.SessionName.UserName = dt.Rows[0]["USERNAME"].ToString();
            SessionManager.SessionName.UserFullName = dt.Rows[0]["EMPLOYEENAME"].ToString();
            SessionManager.SessionName.RoleId = Convert.ToInt32(dt.Rows[0]["ROLEID"]);
            if (udt.Rows.Count > 0)
            {
                SessionManager.SessionName.BusinessCentetid = Convert.ToInt32(udt.Rows[0]["BUSINESSCENTERID"]);
                SessionManager.SessionName.CompanyId = Convert.ToInt32(udt.Rows[0]["COMPANYID"]);
                SessionManager.SessionName.DepartmentId = Convert.ToInt32(udt.Rows[0]["DEPARTMENTID"]);
                SessionManager.SessionName.DesignationId = Convert.ToInt32(udt.Rows[0]["DESIGNATIONID"]);
                SessionManager.SessionName.StaffId = udt.Rows[0]["STAFFID"].ToString();
            }

            dalTaskManager objTask = new dalTaskManager();
            DataTable dtModule = objTask.GetModule(Convert.ToInt32(dt.Rows[0]["ROLEID"]));
            DataTable dtTaskList = objTask.GetAllByRoleId(Convert.ToInt32(dt.Rows[0]["ROLEID"]), Convert.ToInt32(dtModule.Rows[0]["ID"]));

            string returnUrl = "http://hrms.prangroup.com:8283/ya/Pages/User/Profile.aspx";
            if (Request.QueryString["returnUrl"] != null)
            {
                DataTable dtm = new DatabaseManagerORCL().ExecuteQuery("SELECT MODULEID FROM UC_TASKMANAGER WHERE URL='" + Request.QueryString["returnUrl"].Replace("%7e", "").Replace("%2f", "/").Replace("~", "") + "'");
                if (dtm.Rows.Count > 0)
                {
                    SessionManager.SessionName.ModuleId = Convert.ToInt32(dtm.Rows[0][0]);
                    dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, Convert.ToInt32(dtm.Rows[0][0]));
                }
            }
            else
            {
                dtTaskList = objTask.GetAllByRoleId(SessionManager.SessionName.RoleId, 0);
            }
            Response.Redirect(returnUrl);
        }
        else
        {
            FailureText.Text = "Invalid Username or Password";
        }


    }
}