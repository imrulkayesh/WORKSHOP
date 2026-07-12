using System;
using System.Data;

public class MessageGateway
{
    public string SendSms(string mobile, string txt)
    {
        string endpoint = "";
        string userName = "";
        string password = "";
        DataTable dt = new Common().GetAll("bs_SMSGetway");
        if (dt.Rows.Count > 0)
        {
            endpoint = dt.Rows[0]["URL"].ToString();
            userName = dt.Rows[0]["UserName"].ToString();
            password = EncryptionDecryption.EncryptDecryptString(dt.Rows[0]["Password"].ToString());
        }
        try
        {
            if (mobile.Substring(0, 1) != "+")
            {
                switch (mobile.Length)
                {
                    case 11:
                        mobile = "88" + mobile;
                        break;
                    case 10:
                        mobile = "880" + mobile;
                        break;
                }
            }
            else
            {
                mobile = mobile.Substring(1, 13);
            }
            string requestUrl = endpoint
                                + "?ms=" + mobile
                                + "&txt=" + txt
                                + "&username=" + userName + "&password=" + password;
            var aClient = new System.Net.WebClient();

            if (mobile.Length == 13)
            {
                return aClient.DownloadString(requestUrl);
            }
            else
            {
                return "Not A valid Number";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }



    #region Nested type: ConnectedHandler

    private delegate void ConnectedHandler(bool connected);

    #endregion
}