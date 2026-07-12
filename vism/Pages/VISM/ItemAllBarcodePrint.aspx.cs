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

public partial class Pages_VISM_ItemAllBarcodePrint : BasePage
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    DbOra db = new DbOra();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
            LoadCategory();
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

    private void LoadCategory()
    {
        ListItem li = new ListItem("Select", "");
        string qry =
            @"select ITEMCATEGORYCODE ID,ITEMCATEGORYNAME TEXT from RVS_ITEMCATEGORY";
        ddlItemCategory.DataSource = dm.ExecuteQuery(qry);
        ddlItemCategory.DataBind();
        ddlItemCategory.Items.Insert(0, li);

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


    protected void btnView_OnClick(object sender, EventArgs e)
    {

        BindData();
    }

    protected void BindData()
    {

        string qry = @"select iwh.ITEMCODE,im.ITEMNAME,ct.ITEMCATEGORYNAME,iwh.WHID,iwh.LOCATIONID 
from RVS_ITEMMASTER im, RVS_ITEMMASTER_WH iwh,RVS_ITEMCATEGORY ct
where im.ITEMCODE=iwh.ITEMCODE
and im.ITEMCATEGORYCODE=ct.ITEMCATEGORYCODE
and im.ITEMCATEGORYCODE='" + ddlItemCategory.SelectedValue + @"'
and iwh.WHID='" + ddlRecvWh.SelectedValue + @"'
and iwh.LOCATIONID = '" + ddlRecvLoc.SelectedValue + @"'";
        DataTable dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
        }
       
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {

    }


}