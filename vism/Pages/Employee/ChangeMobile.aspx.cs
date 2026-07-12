using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Employee_ChangeMobile : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GetData();

        }
    }

    //private void GetData()
    //{
    //    qry = @"select USERID,OFFICEMOBILE from em_employee where USERID='" + tbxusername.Text.Split('-')[0].Trim() + "'";
    //    DataTable dt = dm.ExecuteQuery(qry);
    //    tbxusername.Text = dt.Rows[0][0].ToString();
    //    tbxexistMobile.Text = dt.Rows[0][1].ToString();
    //}

    protected void tbnChange_OnClick(object sender, EventArgs e)
    {
        try
        {
            qry = @"update em_employee set OFFICEMOBILE='" + tbsNewMobile.Text.Trim() + "',UPDATEDBY='"+SessionManager.SessionName.UserId+ "',UPDATEDATE=sysdate where userid='" + tbxusername.Text.Split('-')[0].Trim() + "'";
            dm.ExecuteNonQuery(qry);
            MessageBox(UpdateSuccess);
            //GetData();
            tbsNewMobile.Text = "";
        }
        catch (Exception ex)
        {

            MessageBox(UpdateFail + ex.Message);
        }

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchStaffId(string prefixText, int count)
    {
        List<string> staffID = new List<string>();
        DataTable dt = DatabaseManagerORCL.StaticDT(@"SELECT E.STAFFID||'-'||E.EMPLOYEENAME||'-'||OFFICEMOBILE STAFFID FROM EM_EMPLOYEE E where LOWER( E.STAFFID ) like LOWER('%" + prefixText + "%') AND ROWNUM <= 8");
        foreach (DataRow dr in dt.Rows)
        {
            staffID.Add(dr["STAFFID"].ToString());
        }
        return staffID;
    }
}