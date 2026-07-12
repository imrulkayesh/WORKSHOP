using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;


public partial class Pages_VISM_FuelDemand : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    dalFuelType obj = new dalFuelType();
    protected void Page_Load(object sender, EventArgs e)
    {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["intPartyId"]))
                {
                    string trno = Request.QueryString["intPartyId"].ToString();
                    LoadBill(trno);
                }
                else
                {

                }
            }

        //if (!string.IsNullOrEmpty(Request.QueryString["RoomTitle"]))
        //{
        //    lblRoomTitle.Text = Request.QueryString["RoomTitle"];
        //    lblCanteenTitle.Text = Request.QueryString["RoomTitle"];
        //    LoadCurrentMeeting();
        //}
    }

    //[WebMethod]
    //public static string UpdatePrint(int demandId, int nop)
    //{
    //    string message = "";
    //    try
    //    {
    //        int updatecn = nop + 1;
    //        new Common().PNupdate("RVS_FUELDMD", demandId, updatecn);

    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //    return message;
    //}

    private void LoadBill(string trno)
    {
        string qry = @"select fm.TRANSACTIONID,co.COMPANYNAME, vh.REGNO,wh.VISMWHNAME PUMPNAME,fm.QTY,fm.AQTY,fm.REMARKS,fm.UPDATEDATE A_DATE,fm.FUELID itemid, im.ITEMNAME,md.MODELNAME,fm.CREATEDBY,fm.UPDATEDBY APPROVEBY,fm.NOOFPRINT   
from RVS_FUELDMD fm, RVS_VEHICLE vh,RVS_VISMWH wh, bs_company co, RVS_ITEMMASTER im,YA.RVS_MODEL md
where fm.VEHICLEID = vh.ID
and fm.FUELID = im.ITEMCODE
and fm.PUMPID = wh.ID
and vh.companyid = co.ID
and vh.MODELID=md.ID
and fm.STATUSID = '2'
AND fm.ID='" + trno + @"'";
        DataTable dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count > 0)
        {

            lblregNo.Text = dt.Rows[0]["REGNO"].ToString();
            lblModel.Text = dt.Rows[0]["MODELNAME"].ToString();
            lblCompanyName.Text = dt.Rows[0]["COMPANYNAME"].ToString();
            lblPump.Text = dt.Rows[0]["PUMPNAME"].ToString();
            lblRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            lblDate.Text = dt.Rows[0]["A_DATE"].ToString();

            //lblDate1.Text = dt.Rows[0]["CREATEDATE"].ToString();
            lblDid.Text= dt.Rows[0]["TRANSACTIONID"].ToString();
            lblApproveBy.Text= dt.Rows[0]["APPROVEBY"].ToString();
            lblCreator.Text= dt.Rows[0]["CREATEDBY"].ToString();

            int cn = Convert.ToInt32(dt.Rows[0]["NOOFPRINT"].ToString());
            int updatecn = cn + 1;
            //lblnop.Text = cn.ToString();

            if (updatecn != 1)
            {
                printStatus.Text = "Duplicate Copy";
            }
            else
            {
                printStatus.Text = "Orginal Copy";
            }

            rptItems.DataSource = dt;
            rptItems.DataBind();

            decimal sumq = 0;
            int i = dt.Rows.Count;
            for (int j = 0; i > j; j++)
            {

                decimal valueq = Convert.ToDecimal(dt.Rows[j]["AQTY"].ToString());
                sumq = sumq + valueq;


            }

          //  int ID = Convert.ToInt32(e.CommandArgument);
            new Common().PNupdate("RVS_FUELDMD", trno, updatecn);

          //  lblTotalQ.Text = sumq.ToString("#0.00");
        }
        else
        {

        }
    }

    private string NumberToText(int number)
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
}