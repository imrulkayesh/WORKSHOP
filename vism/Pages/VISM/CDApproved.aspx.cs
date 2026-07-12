using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;



public partial class Pages_VISM_CDApproved : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    dalFuelType obj = new dalFuelType();
    protected void Page_Load(object sender, EventArgs e)
    {

        //MessageController.Clear(Page);
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
       
        }

        BindData();

    }

    private void BindData()
    {
        //--DataTable dt = new Common().GetAllFA(SessionManager.SessionName.UserId);
        string UserId = SessionManager.SessionName.UserId;


        DataTable dt = new DataTable();
        //string qry = @"";
        //        dt = dm.ExecuteQuery(@"Select D.*,DT.*,CO.COMPANYNAME,VH.REGNO,wh.VISMWHNAME, im.ITEMNAME INAME
        //from YA.RVS_REQMASTER D
        //INNER JOIN YA.RVS_REQDETAIL DT ON D.ID=DT.REQMASTERID
        //INNER JOIN YA.EM_EMPLOYEE E ON E.USERID=D.IUSER 
        //INNER JOIN YA.RVS_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
        //INNER JOIN YA.BS_COMPANY CO ON A.COMPANYID=CO.ID 
        //INNER JOIN YA.RVS_VEHICLE vh ON D.RECVWH=VH.ID
        //INNER JOIN YA.RVS_VISMWH wh ON D.SENDWH = wh.ID
        //INNER JOIN YA.RVS_ITEMMASTER im ON DT.ITEMCODE = im.ITEMCODE
        //where E.DEPARTMENTID=A.DEPARTMENTID
        //AND A.COMPANYID=E.COMPANYID
        //AND D.ID='CD07200000442777'
        //AND A.APPROVERID='168504' and D.STATUSID='0'");


        if (UserId== "92570")
        {
            dt = dm.ExecuteQuery(@"Select D.*,CO.COMPANYNAME,VH.REGNO,EM.EMPLOYEENAME,P.SERVICETYPEID, S.SERVICETYPE
from VISMF.RVS_REQMASTER D
INNER JOIN VISMF.EM_EMPLOYEE E ON E.USERID=D.IUSER
INNER JOIN VISMF.RVS_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
INNER JOIN VISMF.BS_COMPANY CO ON A.COMPANYID=CO.ID 
INNER JOIN VISMF.RVS_VEHICLE vh ON D.RECVWH=VH.ID
INNER JOIN VISMF.EM_EMPLOYEE EM ON EM.STAFFID=D.RECEIVER

INNER JOIN RVS_PREINSPECTION P ON VH.ID = P.VEHICLEID AND P.ISOPEN=1 
INNER JOIN RVS_SERVICETYPE S ON S.ID = P.SERVICETYPEID

where E.DEPARTMENTID=A.DEPARTMENTID
AND A.COMPANYID=E.COMPANYID
--AND D.ID='CD07200000442777'
AND A.APPROVERID='" + UserId + @"' and D.STATUSID='0'
AND TO_DATE(D.IDATE,'DD/MM/YY') BETWEEN (SELECT TO_DATE(SYSDATE-3,'DD/MM/YY') FROM DUAL) AND (SELECT TO_DATE(SYSDATE,'DD/MM/YY') FROM DUAL)");
        }

        else
        {
            dt = dm.ExecuteQuery(@"Select D.*,CO.COMPANYNAME,VH.REGNO,EM.EMPLOYEENAME--, P.SERVICETYPEID, S.SERVICETYPE
from VISMF.RVS_REQMASTER D
INNER JOIN VISMF.EM_EMPLOYEE E ON E.USERID=D.IUSER
INNER JOIN VISMF.RVS_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
INNER JOIN VISMF.BS_COMPANY CO ON A.COMPANYID=CO.ID 
INNER JOIN VISMF.RVS_VEHICLE vh ON D.RECVWH=VH.ID
INNER JOIN VISMF.EM_EMPLOYEE EM ON EM.STAFFID=D.RECEIVER

--INNER JOIN RVS_PREINSPECTION P ON VH.ID = P.VEHICLEID AND P.ISOPEN=1 
--INNER JOIN RVS_SERVICETYPE S ON S.ID = P.SERVICETYPEID

where E.DEPARTMENTID=A.DEPARTMENTID
AND A.COMPANYID=E.COMPANYID
--AND D.ID='CD07200000442777'
AND A.APPROVERID='" + UserId + @"' and D.STATUSID='0'
AND TO_DATE(D.IDATE,'DD/MM/YY') BETWEEN (SELECT TO_DATE(SYSDATE-1,'DD/MM/YY') FROM DUAL) AND (SELECT TO_DATE(SYSDATE,'DD/MM/YY') FROM DUAL)");
        }




        if (dt == null)
        {
            gvMaster.Visible = false;
          //  gvMaster.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
          "<script language='javascript'>alert('Data not Available !!!');</script>", false);
            return;
      
        }
        if (dt.Rows.Count > 0)
        {

            gvMaster.DataSource = dt;
            gvMaster.DataBind();
        }
        else
        {
            gvMaster.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
           "<script language='javascript'>alert('Data not Available !!!');</script>", false);
        }


    }



    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string sl = gvMaster.DataKeys[e.Row.RowIndex].Value.ToString();
            //string sl = Session["sl"] as string;
            GridView gvD = e.Row.FindControl("gvDetail") as GridView;
            DataTable dt = new DataTable();
            dt = dm.ExecuteQuery(@"Select DT.*,im.ITEMNAME INAME
from VISMF.RVS_REQMASTER D
INNER JOIN VISMF.RVS_REQDETAIL DT ON D.ID=DT.REQMASTERID
INNER JOIN VISMF.RVS_ITEMMASTER im ON DT.ITEMCODE = im.ITEMCODE
where D.ID='" + sl + "'");
            gvD.DataSource = dt;
            gvD.DataBind();
        }

    }



    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string ID = ((ImageButton)sender).CommandArgument;
            //int ID = Convert.ToInt32(e.CommandArgument);
            new Common().ApproveCD("RVS_REQMASTER", ID);
          //  MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            MessageController.Show("Approved", MessageType.Confirmation, Page);

         //   SendInitialMail(vid, ID);

            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }

//    public void SendInitialMail(string vid, int ID)
//    {


//        try
//        {
//            string qry = @"SELECT VH.REGNO,CS.CUSTOMERNAME,CS.MOBILE,VH.DRIVERNAME,VH.DRIVERMOBILE,WS.WORKSHOP,CS.EMAIL,CS.CC1,CS.CC2 FROM RVS_PREINSPECTION PR
//INNER JOIN RVS_WORKSHOP WS ON PR.WORKSHOPID=WS.ID
//INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
//INNER JOIN RVS_CUSTOMER CS ON VH.CUSTOMERID=CS.ID
//INNER JOIN RVS_JOBCARD JC ON JC.PIID=PR.ID
//WHERE VH.ID='" + vid + @"' AND JC.ID='" + ID + @"'";
//            DataTable dt = dm.ExecuteQuery(qry);
//            List<string> Approver = new dalApprover().GetByCreatedbyJC(vid, ID);

//            string sbd = "Hello Sir,";
//            sbd += "<br>Job card of your vehicle " + dt.Rows[0]["REGNO"] + " has just closed in " + dt.Rows[0]["WORKSHOP"];
//            sbd += "<br>Your bill has attached in here.\nDriver: " + dt.Rows[0]["DRIVERNAME"] + "\nDriver Mobile: " + dt.Rows[0]["DRIVERMOBILE"];


//            if (Approver.Count > 0)
//            {
//                SendEmail.SendMultipleMailWithPath(Approver, "Job card has closed", sbd);
//            }
//            else
//            {

//            }

//        }
//        catch (Exception ex)
//        {
//            string err = ex.Message;
//        }


//    }

    protected void imgbtnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            string ID = ((ImageButton)sender).CommandArgument;
            new Common().RejectCD("RVS_REQMASTER", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
}