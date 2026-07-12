using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_VehicleDetailsReport : BasePage
{
    dalVehicle obj = new dalVehicle();
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
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbxVehicleID.Text))
        {
            DataTable dt = obj.VehicleDetailReport(Convert.ToInt32(tbxVehicleID.Text));
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {

            MessageController.Show("This Vehicle Not Found !.", MessageType.Error, Page);
        }
    }
    //private string Criteria()
    //{
    //    string val = "";
    //    if (!string.IsNullOrEmpty(tbxVehicleID.Text) && tbxVehicleID.Text != "0")
    //    {
    //        val += " AND V.ID=" + tbxVehicleID.Text;
    //    }
    //    if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length > 7)
    //    {
    //        val += " AND trunc(V.CREATEDATE) Between trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY')) AND trunc(TO_DATE('" + tbxtodate.Text + "','DD/MM/YYYY'))";
    //    }
    //    if (tbxfromdate.Text.Length > 7 && tbxtodate.Text.Length < 7)
    //    {
    //        val += " AND trunc(V.CREATEDATE)=trunc(TO_DATE('" + tbxfromdate.Text + "','DD/MM/YYYY'))";
    //    }

    //    return val;
    //}
}
