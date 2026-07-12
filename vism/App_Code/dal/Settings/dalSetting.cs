
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalSetting
/// </summary>
public class dalSetting
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalSetting()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int UpdateSchoolInfo(string name, string code,string year, string estBy, string address, string details)
    {
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@Code", code);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@EstdBy", estBy);
        dm.AddParameteres("@Address", address);
        dm.AddParameteres("@Description", details);
        return dm.ExecuteNonQuery("USP_Setting_SchoolInfoUpdate");
    }

    public int InsertNotification(string title, bool sendEmail, bool sendSMS)
    {
        dm.AddParameteres("@Title", title);
        dm.AddParameteres("@SendEmail", sendEmail);
        dm.AddParameteres("@SendSMS", sendSMS);
        return dm.ExecuteNonQuery("USP_Setting_NotificationInset");
    }

    public int UpdateSMSGetway(string url, string userName, string pass)
    {
        dm.AddParameteres("@URL", url);
        dm.AddParameteres("@UserName", userName);
        dm.AddParameteres("@Password", pass);
        return dm.ExecuteNonQuery("USP_Setting_SMSGetwayUpdate");
    }

    public int UpdateGeneralSetting(string theme, string dateFormat, string timeZone, string button, string panel)
    {
        dm.AddParameteres("@Theme", theme);
        dm.AddParameteres("@DateFormat", dateFormat);
        dm.AddParameteres("@TimeZone", timeZone);
        dm.AddParameteres("@Button", button);
        dm.AddParameteres("@Panel", panel);
        return dm.ExecuteNonQuery("USP_Setting_GeneralSettingUpdate");
    }
    public DataTable GetNotificationById(int Id)
    {
        dm.AddParameteres("@Id", Id);
        return dm.ExecuteQuery("USP_Setting_GetNotificationById");
    }
}