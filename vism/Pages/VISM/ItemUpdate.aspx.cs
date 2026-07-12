using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_ItemUpdate : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
            // DtDetailAddColumn();
            // BindRpt();
        }
    }

    private void LoadDdl()
    {
        ListItem li = new ListItem("Select", "");
        string qry =
            @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlRecvLoc.DataSource = dm.ExecuteQuery(qry);
        ddlRecvLoc.DataBind();
        ddlRecvLoc.Items.Insert(0, li);

    }

    protected DataTable DtDetailItem
    {
        set
        {
            Session["DtDetailPart"] = value;
        }
        get
        {
            try
            {
                return (DataTable)Session["DtDetailPart"];
            }
            catch
            {
                return null;
            }
        }
    }
    private void DtDetailAddColumn()
    {
        DtDetailItem = new DataTable();
        DtDetailItem.Columns.Add("ITEMCODE");
        DtDetailItem.Columns.Add("ITEMNAME");
        DtDetailItem.Columns.Add("SCMUNITCODE");
        DtDetailItem.Columns.Add("UNITNAME");
        DtDetailItem.Columns.Add("RATE");
        DtDetailItem.Columns.Add("MARGIN");
        DtDetailItem.Columns.Add("ITEMCATEGORYCODE");
        DtDetailItem.Columns.Add("ITEMCATEGORYNAME");


    }


    protected void ddlRecvLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlRecvLoc.SelectedValue + "' order by VISMWHNAME";
        ddlRecvWh.DataSource = dm.ExecuteQuery(qry);
        ddlRecvWh.DataBind();
        ddlRecvWh.Items.Insert(0, li);
        ddlRecvWh.Enabled = true;
        SessionManager.SessionName.RecvLoc = Convert.ToInt32(ddlRecvLoc.SelectedValue);

    }

    protected void ddlRecvWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SessionManager.SessionName.RecvWh = Convert.ToInt32(ddlRecvWh.SelectedValue);
    }



    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchItem(string prefixText, int count)
    {
        string wh = SessionManager.SessionName.RecvWh.ToString();
        string loc = SessionManager.SessionName.RecvLoc.ToString();

        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {


                cmd.CommandText = @"SELECT V.ITEMCODE||'-'||im.ITEMNAME ITEM FROM RVS_itemmaster_wh V,RVS_itemmaster im WHERE V.ITEMCODE=IM.ITEMCODE
AND (V.ITEMCODE LIKE '" + prefixText + "%' OR LOWER(im.ITEMNAME) LIKE LOWER('%" + prefixText + "%')) AND V.LOCATIONID='" + loc + "' AND V.WHID='" + wh + "' and IM.ISACTIVE=1 AND V.ISACTIVE='1' AND ROWNUM <= 20";


                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["ITEM"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void tbxItem_OnTextChanged(object sender, EventArgs e)
    {
        string loc = ddlRecvLoc.SelectedValue;
        string wh = ddlRecvWh.SelectedValue;

        string qry = @"SELECT iw.ITEMCODE,im.ITEMNAME,IM.SCMUNITCODE,UT.UNITNAME,IW.RATE,IM.MARGIN,
IM.ITEMCATEGORYCODE,CT.ITEMCATEGORYNAME FROM RVS_ITEMMASTER IM
INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE=UT.UNITCODE
INNER JOIN RVS_ITEMCATEGORY CT ON IM.ITEMCATEGORYCODE=CT.ITEMCATEGORYCODE 
INNER JOIN RVS_ITEMMASTER_WH IW ON IM.ITEMCODE=IW.ITEMCODE WHERE IM.ISACTIVE=1 AND IW.ISACTIVE='1' AND IW.LOCATIONID='" + loc + "' AND IW.WHID='" + wh + "' and iw.ITEMCODE='" + tbxItem.Text.Split('-')[0] + "'";


        DataTable dt = dm.ExecuteQuery(qry);

        if (dt.Rows.Count > 0)
        {
            tbxItemCode.Text = dt.Rows[0]["ITEMCODE"].ToString();
            tbxItemName.Text = dt.Rows[0]["ITEMNAME"].ToString();
            tbxCategoryName.Text = dt.Rows[0]["ITEMCATEGORYNAME"].ToString();
            tbxUnitName.Text = dt.Rows[0]["UNITNAME"].ToString();
            tbxRate.Text = dt.Rows[0]["RATE"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxQty').focus();", true);
            btnUpdate.Visible = true;
        }

        else
        {
            MessageBox("Item not Available");
            tbxItem.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxItem').focus();", true);

            return;
            btnUpdate.Visible = false;
        }


    }



    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            string qry = @"update RVS_ITEMMASTER_WH set RATE='" + tbxRate.Text + "' where ITEMCODE='" + tbxItemCode.Text + "' and WHID='" + ddlRecvWh.SelectedValue + @"' and LOCATIONID='" + ddlRecvLoc.SelectedValue + @"'";
            db.BeginTransaction();
            db.ExecuteNonQuery(qry);
            db.Commit();
            btnUpdate.Visible = false;
            tbxItemCode.Text = "";
            tbxItemName.Text = "";
            tbxCategoryName.Text = "";
            tbxUnitName.Text = "";
            tbxRate.Text = "";
            tbxItem.Text = "";
            MessageBox(UpdateSuccess);
            ScriptManager.RegisterStartupScript(this, GetType(), "focus", "document.getElementById('tbxItem').focus();", true);
        }


        catch (Exception ex)
        {
            db.Rollback();
            MessageBox(SaveFail + ex.Message);
        }

    }



}