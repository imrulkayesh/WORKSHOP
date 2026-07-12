using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_OtherReceiveScm : BasePage
{
    DatabaseManagerORCL da = new DatabaseManagerORCL();
    DbOra db = new DbOra();

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
        ListItem li = new ListItem("Select", "");
        string qry =
            @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        ddlToLoc.DataSource = da.ExecuteQuery(qry);
        ddlToLoc.DataBind();
        ddlToLoc.Items.Insert(0, li);
    }

    private void BindRpt()
    {
        rpt.DataSource = DtDetail;
        rpt.DataBind();
    }

    protected void ddlToLoc_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select", "");
        string qry = @"select id||'-'||SCMWHID id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
                     ddlToLoc.SelectedValue + "' order by VISMWHNAME";
        ddlToWh.DataSource = da.ExecuteQuery(qry);
        ddlToWh.DataBind();
        ddlToWh.Items.Insert(0, li);
        tbxToCompany.Text = "";
        ddlOiId.Enabled = false;
        ddlToWh.Enabled = true;
    }

    private static DataTable dtAll = new DataTable();

    protected void ddlToWh_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = @"select ws.id||'-'||ws.WORKSHOP from rvs_vismwh wh
inner join rvs_workshop ws on wh.WORKSHOPID=ws.ID
where wh.ID='" + ddlToWh.SelectedValue.Split('-')[0] + "'";
        string val = da.ExecuteScalar(qry);
        tbxToCompany.Text = val.Split('-')[1];
        hdnOrCompnay.Value = val.Split('-')[0];


        qry = @"SELECT * FROM OIALL@SCM_LINK SC
--INNER JOIN YA.RVS_SCMWH WH ON SC.TOWHCODE =WH.SCMWHCODE
LEFT JOIN YA.RVS_TRMASTER TM ON SC.ID=TM.REFTRID
WHERE SC.TOWHCODE='2003663400'
AND TM.ID IS NULL
AND SC.OIDATE='19-feb-2017'"; //need comment finally and uncomment above
        dtAll = da.ExecuteQuery(qry);
        DataTable uniqueTable = dtAll.DefaultView.ToTable(true, "id", "text");
        ddlOiId.DataSource = uniqueTable;
        ddlOiId.DataBind();
        ddlOiId.Enabled = true;


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
        DtDetail.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            DtDetail.Rows.Add(dr["grcode"].ToString(), dr["grname"].ToString(), dr["imcode"].ToString(),
                dr["imname"].ToString(), dr["utcode"].ToString(), dr["utname"].ToString(), dr["qty"].ToString(),
                dr["rate"].ToString(), dr["TOTALAMT"].ToString(), dr["oidnote"].ToString());
        }
        BindRpt();
    }

    private void DtDetailAddColumn()
    {
        DtDetail = new DataTable();
        DtDetail.Columns.Add("Category Code");
        DtDetail.Columns.Add("Category Name");
        DtDetail.Columns.Add("Item Code");
        DtDetail.Columns.Add("Item Name");
        DtDetail.Columns.Add("Unit Code");
        DtDetail.Columns.Add("Unit");
        DtDetail.Columns.Add("Qty");
        DtDetail.Columns.Add("Rate");
        DtDetail.Columns.Add("Total Amount");
        DtDetail.Columns.Add("Remark");

    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            string qry = @"UPDATE RVS_LASTNO SET LASTNO=LASTNO+1,EUSER='" + SessionManager.SessionName.UserId + @"' 
                       WHERE ID='OR' RETURNING LASTNO INTO :RETVAL";
            string lno = da.ExecuteScalar(qry, "RETVAL");
            db.BeginTransaction();
            string trid = "OR"+lno.PadLeft(8,'0') + DateTime.Today.ToString("MMyyyy");
            qry = @"INSERT INTO RVS_TRMASTER(ID,TRTYPE,REFTRID,SENDLOCATION,SENDWH,SENDCOMPANY,
SENDDATE,SENDUSER,SENDREMARKS,RECVLOCATION,RECVWH,RECVCOMPANY,RECVDATE,RECVUSER,RECVREMARKS,IUSER) VALUES(
'" + trid + "','OR','" + ddlOiId.SelectedValue + "','" + hdnOiLocCode.Value + "','" + hdnOiWhCode.Value + @"',
'" + hdnOiComCode.Value + "','01-jan-2018','" + hdnOiUser.Value + "','" + hdnOiRemark.Value + @"',
'" + ddlToLoc.SelectedValue + "','" + ddlToWh.SelectedValue.Split('-')[0] + "','" + hdnOrCompnay.Value + @"',
"+"sysdate"+",'" + SessionManager.SessionName.UserId + "','" + tbxToRemark.Text.Trim().Replace("'", "''") + "','" + SessionManager.SessionName.UserId + "')";
            db.ExecuteNonQuery(qry);
            int i = 1;
            foreach (DataRow r in DtDetail.Rows)
            {
                string trdId = "L"+i.ToString().PadLeft(3,'0') + trid;
                qry = @"INSERT INTO RVS_TRDETAIL (ID,TRMASTERID,ITEMCATEGORYCODE,ITEMCATEGORYNAME,
ITEMCODE,ITEMNAME,UNITID,UNITNAME,QTY,RATE,TOTALAMT,LINE,REMARKS,IUSER) values
('" + trdId + "','" + trid + "','" + r["Category Code"] + "','" + r["Category Name"] + "','" + r["Item Code"] + @"',
'" + r["Item Name"] + "','" + r["Unit Code"] + "','" + r["Unit"] + "','" + r["Qty"] + "','" + r["Rate"] + @"',
'" + r["Total Amount"] + "','" + i + "','" + r["Remark"] + "','" + SessionManager.SessionName.UserId + "')";
                db.ExecuteNonQuery(qry);
                i++;
            }
            db.Commit();
            btnSave.Enabled = false;
            MessageBox(SaveSuccess);
        }
        catch (Exception ex)
        {
            MessageBox(SaveFail + ex.Message);
        }
    }


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}