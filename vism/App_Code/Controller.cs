using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;


public class Controller
{   
    public Controller()
    {
    }
    public static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;
        int destWidth = (int)size.Width;
        int destHeight = (int)size.Height;
        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage(b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();
        imgToResize.Dispose();
        return (System.Drawing.Image)b;
    }

    public static int RoleIdByUserName(string userName)
    {
        DataTable dt = new dalRole().GetIdByUserName(userName);
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public static string RoleNameById(int id)
    {
        DataTable dt = new dalRole().RoleNameById(id);
        return dt.Rows[0][0].ToString();
    }

    public static string RegistrationNo()
    {
        string studentId = "";
        string year = (DateTime.Now.Year).ToString().Substring(2, 2);
        string month = (DateTime.Now.Month).ToString();
        DataTable dt = new dalCommon().GetRegistrationNo();
        if(dt.Rows.Count>0)
        {
            studentId = "88" + year + month + dt.Rows[0][0].ToString();
        }
        return studentId;
    }

    public static int UpdateRegistrationNo()
    {
        return new dalCommon().UpdateRegistrationNo();
    }
    public static void DeleteFile(string path)
    {
        try
        {
            System.IO.File.Delete(path);
        }
        catch (Exception)
        {
        }
    }
    public static int Delete(string table, int id)
    {
        return new Common().Delete(table, id);
    }
    public static int PersonIdByUserName(string userName)
    {
        return new Common().PersonIdByUserName(userName);
    }

    public static int GetLastRoll(string criteria)
    {
        return new Common().GetLastRoll(criteria);
    }
}