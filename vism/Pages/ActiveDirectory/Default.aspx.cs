using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ActiveDirectory_Default : BasePage
{
    dalActiveDirecotry obj = new dalActiveDirecotry();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserPrincipal myUser = obj.GetUser("Test");
            myUser.GivenName = "Given Name";
            myUser.Surname = "Surname";
            myUser.MiddleName = "Middle Name";
            myUser.EmailAddress = "Email Address";
            myUser.EmployeeId = "Employee ID";
            myUser.Save();
        }
    }

}