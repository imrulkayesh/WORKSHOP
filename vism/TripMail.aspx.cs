using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TripMail : System.Web.UI.Page
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> mailto = new List<string>();
        mailto.Add("dist14@rflgroupbd.com");
        mailto.Add("dist402@rflgroupbd.com");
        mailto.Add("rfl148@rflgroupbd.com");
        mailto.Add("dist121@prangroup.net");
        mailto.Add("dist16@rflgroupbd.com");
        mailto.Add("dist31@rflgroupbd.com");
		mailto.Add("dist121@prangroup.com");
		mailto.Add("dic12067@prangroup.net");
        mailto.Add("dist371@rflgroupbd.com");
        mailto.Add("mis70@prangroup.com");
        string mail = "<table border='1' cellspacing='0' cellpadding='5' width='500'><tr><th>Date</th><th>Vehicle</th><th>#Trip</th></tr>";
        string qdate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");
        DataTable dt = dm.ExecuteQuery(@"Select * from VW_VEHICLETRIP WHERE OUTIME='" + qdate + "'");
        foreach (DataRow dr in dt.Rows)
        {
            mail += "<tr><td>" + dr["OUTIME"].ToString() + " </td><td>" + dr["CHASSISNO"].ToString() + " </td><td>" + dr["TIP"].ToString() + "</td></tr>";
        }
        mail += "</table>";

        if (dt.Rows.Count > 0)
        {
           // SendEmail.SendMultipleMail(mailto, "Vehicle Trip(" + DateTime.Now + ")", mail);
        }
    }
}