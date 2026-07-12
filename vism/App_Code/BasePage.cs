using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    protected override void OnPreInit(EventArgs e)
    {
        if (Common.SessionInfo == null)
        {
            Common.SessionInfo = new bdoSessionInfo();
            //LoadSession();
            //LoadClassInformation();
        }

    }
    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //Allows for printing
    }
    public override bool EnableEventValidation
    {
        get { return false; }
        set { /*Do nothing*/ }
    }
    protected override void InitializeCulture()
    {
        string lang = String.Empty;
        HttpCookie cookie = Request.Cookies["CurrentLanguage"];
        if (cookie != null && cookie.Value != null)
        {
            lang = cookie.Value;
            CultureInfo Cul = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentUICulture = Cul;
            Thread.CurrentThread.CurrentCulture = Cul;
        }
        else
        {
            lang = "en-US";
            CultureInfo Cul = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentUICulture = Cul;
            Thread.CurrentThread.CurrentCulture = Cul;

            HttpCookie cookie_new = new HttpCookie("CurrentLanguage");
            cookie_new.Value = lang;
            cookie_new.Expires = DateTime.Now.AddMonths(6);
            Response.SetCookie(cookie_new);
        }
        base.InitializeCulture();
    }
    protected void LoadSession()
    {
        DataTable dt = new Common().GetAll("UC_USERS");
        if (dt.Rows.Count > 0)
        {
            //Common.SessionInfo.ColorTheme = dt.Rows[0]["Theme"].ToString();
            //Common.SessionInfo.DateFormat = dt.Rows[0]["DateFormat"].ToString();
            //Common.SessionInfo.TimeZone = dt.Rows[0]["TimeZone"].ToString();
            //Common.SessionInfo.Button = dt.Rows[0]["Button"].ToString();
            //Common.SessionInfo.Panel = dt.Rows[0]["Panel"].ToString();
        }
    }
    public void MessageBox(string msg) //added by Enamul
    {
        //System.Web.UI.WebControls.Label lblMessageBoxForAlert = new System.Web.UI.WebControls.Label();
        //lblMessageBoxForAlert.ID = "testjavascriptlabelid";
        //lblMessageBoxForAlert.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ");</script>";
        //Page.Controls.Add(lblMessageBoxForAlert);
        msg = msg.Replace("'", "\"");
        var sb = new StringBuilder(msg.Length);

        foreach (char i in msg)
            if (i != '\n' && i != '\r' && i != '\t')
                sb.Append(i);

        msg = sb.ToString();
        var script = String.Format("alert('{0}');", msg);
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
    }
    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>
    //bellows added by Enamul
    protected DataTable DtDetail 
    {
        set
        {
            ViewState["DtDetail"] = value;
        }
        get
        {
            try
            {
                return (DataTable)ViewState["DtDetail"];
            }
            catch
            {
                return null;
            }
        }
    }

    protected enum ReportAction
    {
        OpenPdf,
        DownloadPdf,
        DownloadExcel,
        Print
    }

    protected void GetCrReport(string path, DataTable dt, ReportAction act)
    {

        Session["pathReport"] = HttpContext.Current.Request.Url.Host == "localhost" ? path : "/vism" + path;
        Session["dtReport"] = dt;
        Session["ReportAction"] = act;
        ScriptManager.RegisterStartupScript(this, GetType(), "openInNewTab",
           HttpContext.Current.Request.Url.Host == "localhost"
               ? "window.open('/Pages/Viewer/ReportViewer.aspx', '_blank');"
               : "window.open('/vism/Pages/Viewer/ReportViewer.aspx', '_blank');", true);
       
    }

    public static string[] GetHeader(DataTable dt,bool hideIdCol)
    {
        List<string> myList = new List<string>();
        int x = 0;
        if (hideIdCol) x = 1;
        for (int i = x; i < dt.Columns.Count; i++)
            {
                myList.Add(dt.Columns[i].ColumnName);
            }
        return myList.ToArray();
    }

    public static string[] DtToList(int rw, DataTable dt, bool hideIdCol)
    {
        List<string> lt = new List<string>();
        int x = 0;
        if (hideIdCol) x = 1;
        for (int i = x; i < dt.Columns.Count; i++)
        {
            lt.Add(dt.Rows[rw][i].ToString());
        }
        string[] sa = lt.ToArray();
        return sa;
    }

    
    protected string SaveSuccess = "Saved Successfully.";
    protected string UpdateSuccess = "Updated Successfully.";
    protected string DeleteSuccess = "Deleted Successfully.";
    protected string SaveFail = "Saving Fail.";
    protected string UpdateFail = "Updating Fail.";
    protected string DeleteFail = "Deleting Fail.";
    protected string qry = "";


    protected void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl.GetType() == typeof(TextBox))
            {
                ((TextBox)(ctrl)).Text = string.Empty;
            }
            //else if (ctrl.GetType() == typeof(Label))
            //{
            //    ((Label)(ctrl)).Text = string.Empty;
            //}
            else if (ctrl.GetType() == typeof(DropDownList))
            {
                ((DropDownList)(ctrl)).SelectedIndex = 0;
            }
            //else if (ctrl.GetType() == typeof(CheckBox))
            //{
            //    ((CheckBox)(ctrl)).Checked = false;
            //}
            //else if (ctrl.GetType() == typeof(CheckBoxList))
            //{
            //    ((CheckBoxList)(ctrl)).ClearSelection();
            //}
            //else if (ctrl.GetType() == typeof(RadioButton))
            //{
            //    ((RadioButton)(ctrl)).Checked = false;
            //}
            //else if (ctrl.GetType() == typeof(RadioButtonList))
            //{
            //    ((RadioButtonList)(ctrl)).ClearSelection();
            //}
            ClearInputs(ctrl.Controls);
        }
    }
    protected void ChangeToUpdateMode(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl.GetType() == typeof(Button))
            {
                if (((Button)(ctrl)).ID == "btnSave")
                {
                    ((Button) (ctrl)).Visible = false;
                }
                else if (((Button)(ctrl)).ID == "btnUpdate")
                {
                    ((Button)(ctrl)).Visible = true;
                }
            }
            ChangeToUpdateMode(ctrl.Controls);
        }
       
    }
    protected void ChangeToSaveMode(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl.GetType() == typeof(Button))
            {
                if (((Button)(ctrl)).ID == "btnSave")
                {
                    ((Button)(ctrl)).Visible = true;
                }
                else if (((Button)(ctrl)).ID == "btnUpdate")
                {
                    ((Button)(ctrl)).Visible = false;
                }
            }
            ChangeToSaveMode(ctrl.Controls);
        }

    }

    protected void ExportToExcel(Repeater rptMain)
    {
        StringWriter swr = new StringWriter();
        HtmlTextWriter htmlwr = new HtmlTextWriter(swr);
        rptMain.RenderControl(htmlwr);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition","attachment;filename=ExportData-" + DateTime.Now.ToString("dd-MM-yyyy--hh-mm-tt") + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Output.Write(swr.ToString());
        Response.Flush();
        Response.End();
        GC.Collect();

        /*
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
         */
    }

    protected string TimeConvert(string hh)
    {
        string hhtt = "";
        if (Convert.ToInt32(hh) == 0 || Convert.ToInt32(hh)==24)
        {
            hhtt = "12 AM";
        }
        else if (Convert.ToInt32(hh)>0 && Convert.ToInt32(hh) < 12)
        {
            hhtt = Convert.ToInt32(hh).ToString("00") + " AM";
        }
        else if (Convert.ToInt32(hh) == 12)
        {
            hhtt = "12 PM";
        }
        else
        {
            hhtt = (Convert.ToInt32(hh) - 12).ToString("00") + " PM";
        }
        return hhtt;
    }

    protected string DateChange(string date)
    {
        date = Convert.ToDateTime(date).ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
        string ndt= DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
        return ndt;
    }
    
}