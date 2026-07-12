using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CsHelper
/// </summary>
public static class CsHelper
{
    public static string ToDate(this string str)
    {
        try
        {
            str = Convert.ToDateTime(str).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string ndt = DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            return ndt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    //   public static string FromDate(this string str)
    //{
    //    try
    //    {
    //        str = Convert.ToDateTime(str).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
    //        string ndt = DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
    //        return ndt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
        
    //}

   
}