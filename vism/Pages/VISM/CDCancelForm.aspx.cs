using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing;

public partial class Pages_VISM_CdCancel : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtMRR.Text = "";
        btnClc.Enabled = false;
        btnDetails.Enabled = false;
    }

    string _txtMRR = "";

    protected void btnShow_OnClickClick(object sender, EventArgs e)
    {
        string txt = txtMRR.Text;
         _txtMRR = RemoveMistakes(txt);

        if (_txtMRR.Length != 16)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
           "<script language='javascript'>alert('enter a valid MRR number');</script>", false);
        }
        else
        {
            BindRpt();
            btnDetails.Enabled = true;
            btnClc.Enabled = true;
            btnShow.Enabled = false;
        }

    }
    public string RemoveMistakes(string _badString)
    {
        string _goodString = string.Empty;
        _goodString = _badString.Replace(" ", "");
        _goodString = _goodString.Replace("-", "");
        return _goodString;
    }

    private void BindRpt()
    {
        GridView2.DataSource = RptData();
        GridView2.DataBind();
    }
    private bool _firstTime = true;
    public DataTable RptData()
    {
        DataTable dt = new DataTable();
        if (_firstTime)
        {
            string qry = @"Select CD.ID CD,CD.JOBID Job, CD.IUSER Created_By, CD.IDATE Create_Date  FROM   YA.RVS_REQMASTER CD WHERE   CD.ID = '" + _txtMRR + "' and CD.REQTYPE = 'CD' and CD.ISACTIVE = '1'";


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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    protected void btnClc_OnClickClick(object sender, EventArgs e)
    {

        txtMRR.Text = ""; 
        Page.Response.Redirect(Page.Request.Url.ToString(), true);

    }
    protected void btnDetails_OnClickClick(object sender, EventArgs e)
    {
        _txtMRR = RemoveMistakes(txtMRR.Text);

        GridView1.DataSource = DetailsData();
        GridView1.DataBind();

    }
    private bool _firstTimes = true;
    public DataTable DetailsData()
    {
        btnClc.Enabled = true;
        DataTable dt = new DataTable();
        if (_firstTimes)
        {
            string qry = @"SELECT   CDD.REQMASTERID CD,CDD.ITEMCODE ITEM_CODE,CDD.ITEMNAME ITEM_NAME,CDD.ITEMCATEGORYNAME CATEGORY_NAME, CDD.UNITID UNIT, CDD.QTY,CDD.RATE,CDD.MARGIN,CDD.MARGINRATE MARGIN_RATE,CDD.TOTALAMT TOTAL_AMOUNT
  FROM   YA.RVS_REQMASTER CDM, YA.RVS_REQDETAIL CDD
 WHERE   CDM.ID = '" + _txtMRR + "' AND CDM.REQTYPE = 'CD' AND CDD.REQMASTERID = CDM.ID AND CDD.QTY <> 0";


            dt = dm.ExecuteQuery(qry);
            Session["dt"] = dt;
            _firstTimes = false;

        }
        else
        {
            dt = Session["dt"] as DataTable;
        }

        return dt;
    }
}