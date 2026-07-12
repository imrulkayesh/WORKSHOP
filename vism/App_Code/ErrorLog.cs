using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

public class ErrorLog
{
    static string m_baseDir = null;
	public ErrorLog()
	{
        m_baseDir = @"C:\Inetpub\wwwroot\HRMS\log\";
	}
    public static string GetFilenameYYYMMDD(string suffix, string extension)
    {
        return System.DateTime.Now.ToString("yyyy_MM_dd")
            + suffix
            + extension;
    }

    public static void WriteLog(String message)
    {
        //try
        //{
        //    string filename = m_baseDir
        //        + GetFilenameYYYMMDD("_LOG", ".log");
        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true);
        //    XElement xmlEntry = new XElement("logEntry",
        //        new XElement("Date", System.DateTime.Now.ToString()),
        //        new XElement("Message", message));
        //    sw.WriteLine(xmlEntry);
        //    sw.Close();
        //}
        //catch (Exception) { }
    }

    public static void WriteLog(Exception ex)
    {
        //try
        //{
        //    string filename = m_baseDir
        //        + GetFilenameYYYMMDD("_LOG", ".log");
        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true);
        //    XElement xmlEntry = new XElement("logEntry",
        //        new XElement("Date", System.DateTime.Now.ToString()),
        //        new XElement("Exception",
        //            new XElement("Source", ex.Source),
        //            new XElement("Message", ex.Message),
        //            new XElement("Stack", ex.StackTrace)
        //         )
        //    );
        //    if (ex.InnerException != null)
        //    {
        //        xmlEntry.Element("Exception").Add(
        //            new XElement("InnerException",
        //                new XElement("Source", ex.InnerException.Source),
        //                new XElement("Message", ex.InnerException.Message),
        //                new XElement("Stack", ex.InnerException.StackTrace))
        //            );
        //    }
        //    sw.WriteLine(xmlEntry);
        //    sw.Close();
        //}
        //catch (Exception) { }
    }
}