using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for dalCommon
/// </summary>
public class dalCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetRegistrationNo()
    {
        return dm.ExecuteQuery("USP_RegistrationNo");
    }

    public int UpdateRegistrationNo()
    {
        return dm.ExecuteNonQuery("USP_RegistrationNo_Update");
    }
    public DataTable DropdownByCondition(string table, string tableColumn, string whereby)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column", tableColumn);
        dm.AddParameteres("@Condition", whereby);
        return dm.ExecuteQuery("USP_GetById");
    }
    public DataTable LoadDropdown(string table)
    {
        return dm.ExecuteQuery("Select * from " + table);
    }
    public int GetRolePriority(int roleId)
    {
        DataTable dt = dm.ExecuteQuery("SELECT PRIORITY FROM UC_ROLESETUP WHERE ID=" + roleId);
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public DataTable GetAll(string table)
    {
        dm.AddParameteres("@Table", table);
        return dm.ExecuteQuery("USP_GetAll");
    }
    public static DateTime DateFormatYYYYMMDD(string date)
    {
        string[] d = date.Split('/');
        return Convert.ToDateTime(d[2] + "-" + d[1] + "/" + d[0]);
    }
    public static bool IsPermitted(string url)
    {
        bool isPermitted = false;
        if (SessionManager.SessionName.RoleId == 1)
        {
            isPermitted = true;
        }
        else
        {
            //DataTable dt = DatabaseManagerORCL.StaticDT("SELECT * FROM UC_TASKMANAGER T INNER JOIN UC_TASKPERMISSION P ON P.TASKID=T.ID WHERE P.ROLEID=" + SessionManager.SessionName.RoleId + " AND LOWER(T.URL) LIKE LOWER('" + url + "%')");
            DataTable dt = DatabaseManagerORCL.StaticDT("SELECT * FROM UC_TASKMANAGER T INNER JOIN UC_TASKPERMISSION P ON P.TASKID=T.ID WHERE P.ROLEID in (SELECT ROLEID FROM UC_USERROLE WHERE STAFFID  = '" + SessionManager.SessionName.StaffId + "' ) AND LOWER(T.URL) LIKE LOWER('" + url + "%')");
            if (dt.Rows.Count > 0)
            {
                isPermitted = true;
            }
        }
        return isPermitted;
    }

    public static bool IsPermittedWithoutPageLink(string url)
    {
        bool isPermitted = false;
        if (SessionManager.SessionName.RoleId == 1)
        {
            isPermitted = true;
        }
        else
        {
            //DataTable dt = DatabaseManagerORCL.StaticDT("SELECT * FROM UC_TASKMANAGER T INNER JOIN UC_TASKPERMISSION P ON P.TASKID=T.ID WHERE P.ROLEID=" + SessionManager.SessionName.RoleId + " AND LOWER(T.URL) LIKE LOWER('" + url + "%')");
            DataTable dt = DatabaseManagerORCL.StaticDT("SELECT * FROM UC_TASKMANAGER T INNER JOIN UC_TASKPERMISSION P ON P.TASKID=T.ID WHERE P.ROLEID in (SELECT ROLEID FROM UC_USERROLE WHERE STAFFID  = '" + SessionManager.SessionName.StaffId + "' ) AND ID=465");
            if (dt.Rows.Count > 0)
            {
                isPermitted = true;
            }
        }
        return isPermitted;
    }

    public static void LoginReturnUrl(string url)
    {
        if (SessionManager.SessionName.UserName == null)
        {
            string returnUrl = "~/Login.aspx?returnUrl=~" + url;
            HttpContext.Current.Response.Redirect(returnUrl);
            //HttpContext.Current.Server.Transfer(returnUrl);
            return;
        }
    }
    public static void ExportToWord(Panel tdReportInfo, string reportName)
    {
        try
        {
            reportName = reportName + ".doc";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AddHeader("content-disposition",
            "attachment;filename=" + reportName + "");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tdReportInfo.RenderControl(hw);
            string html = sw.ToString();
            html = Regex.Replace(html, "</?(a|A).*?>", "");
            HttpContext.Current.Response.Output.Write(html);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {


        }
    }

    public static void ExportToExcel(Panel tdReportInfo, string reportName)
    {
        try
        {
            reportName = reportName + ".xls";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + reportName + "");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            tdReportInfo.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style>.text-left {text-align: left;} .text-right {text-align: right;} .text-center {text-align: center;}.text-justify {text-align: justify;}.text-nowrap {white-space: nowrap;}.text-lowercase {text-transform: lowercase;}.text-uppercase {text-transform: uppercase;}.text-capitalize {text-transform: capitalize;}.img-thumbnail {display: inline-block;max-width: 100%;height: auto;padding: 4px;line-height: 1.42857143;background-color: #fff;border: 1px solid #ddd;border-radius: 4px;-webkit-transition: all .2s ease-in-out;-o-transition: all .2s ease-in-out;transition: all .2s ease-in-out;}.panel-title {margin-top: 0;margin-bottom: 0;font-size: 16px;color: inherit;}.table {width: 100%;max-width: 100%;margin-bottom: 20px;} .table {border-collapse: collapse !important;}.table td, .table th {background-color: #fff !important;}.table-bordered th, .table-bordered td {border: 1px solid #ddd !important;}.table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {padding: 8px;line-height: 1.42857143; vertical-align: top;border-top: 1px solid #ddd; text-align:left }.table > thead > tr > th {vertical-align: bottom;border-bottom: 2px solid #ddd;}.table > caption + thead > tr:first-child > th, .table > colgroup + thead > tr:first-child > th, .table > thead:first-child > tr:first-child > th, .table > caption + thead > tr:first-child > td, .table > colgroup + thead > tr:first-child > td, .table > thead:first-child > tr:first-child > td {border-top: 0;}.table > tbody + tbody {border-top: 2px solid #ddd;}.table .table {background-color: #fff;}.table-bordered {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #ddd;}.table-bordered > thead > tr > th, .table-bordered > thead > tr > td {border-bottom-width: 2px;} .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            string html = sw.ToString();
            html = Regex.Replace(html, "</?(a|A).*?>", "");
            HttpContext.Current.Response.Output.Write(html);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    public static void SendSMS(string MobileNumber, string message)
    {
        try
        {
            //string url = "http://172.17.4.97:13014/cgi-bin/sendsms?username=smsgw&password=smsgw&from=PRPS&to=01912257257&coding=2&charset=UTF-8&text=Dear Sir, This message Generated from PRPS School Management System";
            string url = "http://172.17.4.235/sms.aspx?m=" + MobileNumber + "&s=" + message;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
        }
        catch { }
    }
    public static string NumberToText(int number)
    {
        bool isUK = false;
        if (number == 0) return "Zero";
        string and = isUK ? "and " : ""; // deals with UK or US numbering
        if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
        "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
        "Six Hundred " + and + "Forty Eight";
        int[] num = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (number < 0)
        {
            sb.Append("Minus ");
            number = -number;
        }
        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
        string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
        string[] words3 = { "Thousand ", "Million ", "Billion " };
        num[0] = number % 1000;           // units
        num[1] = number / 1000;
        num[2] = number / 1000000;
        num[1] = num[1] - 1000 * num[2];  // thousands
        num[3] = number / 1000000000;     // billions
        num[2] = num[2] - 1000 * num[3];  // millions
        for (int i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;
            u = num[i] % 10;              // ones
            t = num[i] / 10;
            h = num[i] / 100;             // hundreds
            t = t - 10 * h;               // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i < first) sb.Append(and);
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();
    }
    public static void ExportGrid(GridView gv, string fileName, string contentType)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = contentType;

        StringWriter sw = new StringWriter();
        HtmlTextWriter HW = new HtmlTextWriter(sw);

        gv.RenderControl(HW);
        HttpContext.Current.Response.Output.Write(sw.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.Close();
        HttpContext.Current.Response.End();
    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    // this is required for avoid error (control must be placed inside form tag)
    //}
    //protected void btnExportWord_Click(object sender, EventArgs e)
    //{
    //    // Export Gridview to Word
    //    ExportGrid("GridviewData.doc", "application/vnd.ms-word");
    //}
    //protected void btnExportExcel_Click(object sender, EventArgs e)
    //{
    //    // Export Gridview to Excel
    //    ExportGrid("GridviewData.xls", "application/vnd.ms-excel");
    //}

}