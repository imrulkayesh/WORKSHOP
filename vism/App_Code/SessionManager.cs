using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionManager
/// </summary>
public class SessionManager
{
    public SessionManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bdoSessionInfo SessionName
    {
        get
        {
            try
            {
                if (HttpContext.Current.Session["SessionInfo"] != null)
                    return HttpContext.Current.Session["SessionInfo"] as bdoSessionInfo;
                else
                    return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["SessionInfo"] = value;
        }
    }
}