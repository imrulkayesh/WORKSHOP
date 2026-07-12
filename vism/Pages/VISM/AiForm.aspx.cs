using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_AiForm : BasePage
{
    DatabaseManagerORCL da = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    private static DataTable dtAll = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
            DtDetailAddColumn();
            BindRpt();
        }

    }

    private void LoadDdl()
    {
  
        string BI = SessionManager.SessionName.BusinessCentetid.ToString();
        ListItem li = new ListItem("Select", "");
        string qry = "";
        if (BI == "2")
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        }
        else
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
        }

        ddlToLoc.DataSource = da.ExecuteQuery(qry);
        ddlToLoc.DataBind();
        ddlToLoc.Items.Insert(0, li);
    }

    private void BindRpt()
    {
        DtDetailItem.AcceptChanges();
        rpt.DataSource = DtDetailItem;
        rpt.DataBind();
    }

    protected void ddlToLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id||'-'||SCMWHID id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlToLoc.SelectedValue + "' order by VISMWHNAME";
        DataTable dt = da.ExecuteQuery(qry);
        
        ddlToWhh.DataSource = dt;
        ddlToWhh.DataBind();
        ddlToWhh.Items.Insert(0, li);
        
        tbxToCompany.Text = "";
        ddlOiId.Enabled = false;
        ddlToWhh.Enabled = true;
    }


    protected void ddlOiId_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = dtAll.Select("id = '" + ddlOiId.SelectedValue + "'").CopyToDataTable();


        tbxFromLoc.Text = dt.Rows[0]["fmloname"].ToString();
        hdnOiLocCode.Value = dt.Rows[0]["fmlocode"].ToString();
        tbxFromWh.Text = dt.Rows[0]["fmwhname"].ToString();
        hdnOiWhCode.Value = dt.Rows[0]["fmwhcode"].ToString();
        tbxFromCompany.Text = dt.Rows[0]["fmconame"].ToString();
        hdnOiComCode.Value = dt.Rows[0]["fmcocode"].ToString();
        tbxFromDate.Text = dt.Rows[0].Field<DateTime>("oidate").ToString("dd/MM/yyyy");
        hdnOiUser.Value = dt.Rows[0]["OIUSER"].ToString();
        hdnOiRemark.Value = dt.Rows[0]["OIMNOTE"].ToString();
        DtDetailItem.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            DtDetailItem.Rows.Add(dr["grcode"].ToString(), dr["grname"].ToString(), dr["imcode"].ToString(),
                dr["imname"].ToString(), dr["utcode"].ToString(), dr["utname"].ToString(), dr["qty"].ToString(),
                dr["rate"].ToString(), dr["TOTALAMT"].ToString(), dr["oidnote"].ToString());
        }
        BindRpt();
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
        DtDetailItem.Columns.Add("Category Code");
        DtDetailItem.Columns.Add("Category Name");
        DtDetailItem.Columns.Add("Item Code");
        DtDetailItem.Columns.Add("Item Name");
        DtDetailItem.Columns.Add("Unit Code");
        DtDetailItem.Columns.Add("Unit");
        DtDetailItem.Columns.Add("Qty");
        DtDetailItem.Columns.Add("Rate");
        DtDetailItem.Columns.Add("Total Amount");
        DtDetailItem.Columns.Add("Remark");

    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
           // string ordate = ddlOiId.SelectedItem.Text.Substring(ddlOiId.SelectedItem.Text.Length - 10).ToDate();

            //string opdate = "01-" + tbxfromdate.Text.ToDate().Substring(3, 3) + "-" + tbxfromdate.Text.ToDate().Substring(7, 4);
            //string ffdate = Convert.ToDateTime(tbxfromdate.Text.ToDate()).AddDays(-1).ToString("dd/MM/yyyy").ToDate();

            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='OR' RETURNING LASTNO INTO :RETVAL";
            string lno = da.ExecuteScalar(qry, "RETVAL");
            db.BeginTransaction();
            string trid = "OR" + DateTime.Today.ToString("MMyy")+ lno.PadLeft(10, '0') ;
            qry = @"INSERT INTO RVS_TRMASTER(ID,TRTYPE,REFTRID,SENDLOCATION,SENDWH,SENDCOMPANY,
SENDDATE,SENDUSER,SENDREMARKS,RECVLOCATION,RECVWH,RECVCOMPANY,RECVDATE,RECVUSER,RECVREMARKS,ISCOMPLETE,IUSER) VALUES(
'" + trid + "','OR','" + ddlOiId.SelectedValue + "','" + hdnOiLocCode.Value + "','" + hdnOiWhCode.Value + @"',
'" + hdnOiComCode.Value + "',TO_DATE(SYSDATE,'DD/MM/YYYY'),'" + hdnOiUser.Value + "','" + hdnOiRemark.Value + @"',
'" + ddlToLoc.SelectedValue + "','" + ddlToWhh.SelectedValue.Split('-')[0] + "','" + hdnOrCompnay.Value + @"',
TO_DATE(SYSDATE,'DD/MM/YYYY'),'" + SessionManager.SessionName.UserId + "','" + tbxToRemark.Text.Trim().Replace("'", "''") + "','1','" + SessionManager.SessionName.UserId + "')";

//to_date('" + tbxfromdate.Text + "','DD/MM/YYYY')

            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetailItem.Rows)
            {
                string trdId = "L" + i.ToString().PadLeft(3, '0') + trid;
                qry = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,
ITEMCODE,UNITID,QTY,RATE,TOTALAMT,LINE,REMARKS,IUSER,WH,RECVWH) values
('" + trdId + "','" + trid + "','" + r["Category Code"] + "','" + r["Item Code"] + @"',
'" + r["Unit Code"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["Total Amount"] + "','" + i + "','" + r["Remark"].ToString().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + "','" + hdnOiWhCode.Value + @"','" + ddlToWhh.SelectedValue.Split('-')[0] + "')";
                db.ExecuteNonQuery(qry);
                i++;

                string qry2 = @"select * from RVS_ITEMMASTER_WH where ITEMCODE='" + r["Item Code"] + @"' and WHID='" + ddlToWhh.SelectedValue.Split('-')[0] + "' and LOCATIONID='" + ddlToLoc.SelectedValue + "'";
                string id = da.ExecuteScalar(qry2);
                if (string.IsNullOrWhiteSpace(id))
                {
                  qry = @"INSERT INTO YA.RVS_ITEMMASTER_WH (ITEMCODE,WHID,LOCATIONID,RATE) VALUES('" + r["Item Code"] + @"','" + ddlToWhh.SelectedValue.Split('-')[0] + "','" + ddlToLoc.SelectedValue + "'," + r["Rate"] + @")";
                   // db.BeginTransaction();
                    db.ExecuteNonQuery(qry);
                   // int i = 1;
                   // db.Commit();

                }
                else
                {
                  
                }

                qry = @"update RVS_ITEMMASTER_WH set RATE='" + r["Rate"] + "' where ITEMCODE='" + r["Item Code"] + "' AND WHID='" + ddlToWhh.SelectedValue.Split('-')[0] + "' AND LOCATIONID='" + ddlToLoc.SelectedValue + "'";
                db.ExecuteNonQuery(qry);
            }
            db.Commit();
            btnSave.Enabled = false;
            tbxTrId.Text = trid;
            MessageBox(SaveSuccess);
        }
        catch (Exception ex)
        {
            db.Rollback();
            MessageBox(SaveFail + ex.Message);
        }
    }


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void ddlToWhh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id||'-'||ws.WORKSHOP from rvs_vismwh wh
        inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
        where wh.ID='" + ddlToWhh.SelectedValue.Split('-')[0] + "'";
        string val = da.ExecuteScalar(qry);
        tbxToCompany.Text = val.Split('-')[1];
        hdnOrCompnay.Value = val.Split('-')[0];


        qry = @"SELECT * FROM OIALL@SCM_LINK SC
        LEFT JOIN YA.RVS_TRMASTER TM ON SC.ID=TM.REFTRID
        WHERE SC.FMWHCODE ='" + ddlToWhh.SelectedValue.Split('-')[1] + @"'
        AND TM.ID IS NULL"; //need comment finally and uncomment above
        dtAll = da.ExecuteQuery(qry);
        DataTable uniqueTable = dtAll.DefaultView.ToTable(true, "id", "text");
        ddlOiId.DataSource = uniqueTable;
        ddlOiId.DataBind();
        ddlOiId.Items.Insert(0,new ListItem("Select",""));
        ddlOiId.Enabled = true;
    }
}