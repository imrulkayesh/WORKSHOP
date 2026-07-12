using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_UserWorkshop : BasePage
{
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        string qry = @"update em_employee set workshopid='" + ddlWorkshop.SelectedValue + "' where staffid='" +
                     tbxUser.Text.Split('-')[0].Trim() + "'";
       int a= dm.ExecuteNonQuery(qry);
        if (a > 0)
        {
            MessageBox(SaveSuccess);
        }
        else
        {
            MessageBox("No User Found.");
        }

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
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

    protected void tbxUser_OnTextChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID in(select BUSINESSCENTERID from EM_EMPLOYEE
 where STAFFID='" + tbxUser.Text.Split('-')[0].Trim() + "') order by VISMWHNAME";
        ddlWorkshop.DataSource = dm.ExecuteQuery(qry);
        ddlWorkshop.DataBind();
        ddlWorkshop.Items.Insert(0, li);
    }
}