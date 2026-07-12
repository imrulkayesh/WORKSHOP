using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SendSMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);

        if (!IsPostBack)
        {
            //SendSMS("smsgw", "smsgw", "PRPS", "01912257257", "Test From PRPS School Management System");
        }
    }

    private static void SendSMS(string username,string password,string SenderTitle, string MobileNumber, string message)
    {
        try
        {
            //string url = "http://172.17.4.97:13014/cgi-bin/sendsms?username=smsgw&password=smsgw&from=PRPS&to=01912257257&coding=2&charset=UTF-8&text=Dear Sir, This message Generated from PRPS School Management System";
            string url = "http://172.17.4.97:13014/cgi-bin/sendsms?username="+username+"&password="+password+"&from="+SenderTitle+"&to="+MobileNumber+"&coding=2&charset=UTF-8&text="+message;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
        }
        catch { }
    }
}