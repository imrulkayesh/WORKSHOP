using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Pages_VISM_ReportCompanyWise : BasePage
{
    DbOra db = new DbOra();
    dalJobCard obj = new dalJobCard();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    protected static decimal partCdTot = 0;
    protected static decimal partCoTot = 0;

    protected static decimal serAmtTot = 0;
     private bool _firstTime = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted("/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath)))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            try
            {

            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCom(string prefixText, int count)
    {
       
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {

                cmd.CommandText = @"select COMPANYNAME from ya.BS_COMPANY where COMPANYNAME like UPPER ('%" + prefixText + "%') ";

                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["COMPANYNAME"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void tbxCom_OnTextChanged(object sender, EventArgs e)
    {
        string cn = tbxCom.Text.Split('-')[0];
    }

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        BindRpt();
    }

    private void BindRpt()
    {
        rptMain.DataSource = RptData();
        rptMain.DataBind();
        if (rptMain.Items.Count > 0)
        {
            pnlReportButton.Visible = true;
        }
        else
        {
            pnlReportButton.Visible = false;
        }
    }

    public DataTable RptData()
    {
        DataTable dt = new DataTable();
        if (_firstTime)
        {
    

              string CN = tbxCom.Text;


            string qry = @"select companyname,ITEMCODE,itemname,unit,SUM (qty) qty,SUM (qty * marginrate) Value from ya.v_company_bill where TRDATE BETWEEN '" + tbxfromdate.Text.ToDate() + @"' AND '" + tbxtodate.Text.ToDate() + @"' and companyname='" + CN + "' group by companyname,ITEMCODE,itemname,unit order by itemname asc";


            dt = dm.ExecuteQuery(qry);
            Session["dt"] = dt;
            _firstTime = false;

        }
        else
        {
            dt = Session["dt"] as DataTable;
        }

        return dt;
    }

     protected int ID = 0;
    protected int totalAmt = 0;
    private int _rw;



    protected void rptMain_ItemBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
            headerRpt.DataSource = GetHeader(RptData());
            headerRpt.DataBind();
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;

            bodyRpt.DataSource = DtToList(_rw, RptData());

            bodyRpt.DataBind();
            _rw++;
            //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
            totalAmt += Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[5]);
        }

    }

    public static string[] GetHeader(DataTable dt)
    {
        List<string> myList = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {

            myList.Add(dt.Columns[i].ColumnName);

        }

        return myList.ToArray();
    }

    public static string[] DtToList(int rw, DataTable dt)
    {
        List<string> lt = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            lt.Add(dt.Rows[rw][i].ToString());
        }

        string[] sa = lt.ToArray();
        return sa;
    }
}