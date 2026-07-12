using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionInfo
/// </summary>
public class SessionInfo
{
	public SessionInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string SessionID
    {
        get { return HttpContext.Current.Session.SessionID; }
    }
    public int TimeOut
    {
        get
        {
            try
            {
                return HttpContext.Current.Session.Timeout;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        set
        {
            HttpContext.Current.Session.Timeout = value;
        }
    }

    #region GeneralConfiguration


    public string ColorTheme
    {
        get
        {
            try
            {
                return HttpContext.Current.Session["ColorTheme"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["ColorTheme"] = value;
        }
    }

    public string DateFormat
    {
        get
        {
            try
            {
                return HttpContext.Current.Session["DateFormat"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["DateFormat"] = value;
        }
    }


    public string Button
    {
        get
        {
            try
            {
                return HttpContext.Current.Session["Button"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["Button"] = value;
        }
    }
    public string Panel
    {
        get
        {
            try
            {
                return HttpContext.Current.Session["Panel"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["Panel"] = value;
        }
    }


    public string TimeZone
    {
        get
        {
            try
            {
                return HttpContext.Current.Session["TimeZone"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["TimeZone"] = value;
        }
    }

 #endregion GeneralConfiguration
}