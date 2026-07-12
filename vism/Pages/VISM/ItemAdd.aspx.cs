using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_ItemAdd : BasePage
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
        //ListItem li = new ListItem("Select", "");
        //string qry =
        //    @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
        //ddlRecvLoc.DataSource = dm.ExecuteQuery(qry);
        //ddlRecvLoc.DataBind();
        //ddlRecvLoc.Items.Insert(0, li);


        string BI = SessionManager.SessionName.BusinessCentetid.ToString();
        ListItem li = new ListItem("Select", "");
        string qry = "";
        if (BI == "705")
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where ID='" + BI + "' AND scmlocation is not null order by centername";
            ddlRecvLoc.DataSource = dm.ExecuteQuery(qry);
            ddlRecvLoc.DataBind();

            string qry1 = @"select id,VISMWHNAME text  from RVS_VISMWH where isactive=1 and LOCATIONID='" +
             ddlRecvLoc.SelectedValue + "' order by VISMWHNAME";
            ddlRecvWh.DataSource = dm.ExecuteQuery(qry1);
            ddlRecvWh.DataBind();
            //ddlRecvWh.Items.Insert(0, li);
            ddlRecvWh.Enabled = true;

        }
        else
        {
            qry = @"select id,centername text  from BS_BUSINESSCENTER where scmlocation is not null order by centername";
            ddlRecvLoc.DataSource = dm.ExecuteQuery(qry);
            ddlRecvLoc.DataBind();
            ddlRecvLoc.Items.Insert(0, li);
        }

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
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            string qry = @"select * from RVS_ITEMMASTER_WH where ITEMCODE='" + txtItemCode.Text + "' and WHID='" + ddlRecvWh.SelectedValue + "' and LOCATIONID='" + ddlRecvLoc.SelectedValue + "'";
            string id = dm.ExecuteScalar(qry);
            if (string.IsNullOrWhiteSpace(id))
            {
                qry = @"INSERT INTO VISMF.RVS_ITEMMASTER_WH (ITEMCODE,WHID,LOCATIONID) VALUES('" + txtItemCode.Text + "','" + ddlRecvWh.SelectedValue + "','" + ddlRecvLoc.SelectedValue + "')";
                db.BeginTransaction();
                db.ExecuteNonQuery(qry);
                int i = 1;
                db.Commit();
                txtItemCode.Text = "";
                LoadDdl();
                MessageBox(SaveSuccess);
              
            }
            else
            {
                MessageBox("This item have already created");
                return;
            }
        }
        catch (Exception ex)
        {
            db.Rollback();
            MessageBox(SaveFail + ex.Message);
        }

    }
}