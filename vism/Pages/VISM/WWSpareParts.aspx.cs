using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_WWSpareParts : BasePage
{
    dalWWSpareParts obj = new dalWWSpareParts();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + System.IO.Path.GetFileName(Request.PhysicalPath);
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url + Request.Url.Query);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            LoadDDL();
        }
    }
    private void LoadDDL()
    {
        ListItem li = new ListItem("Select", "0");
        ddlCountry.DataSource = new Common().GetAll("BS_COUNTRY");
        ddlCountry.DataBind();
        ddlWorkshop.DataSource = new Common().GetAll("RVS_WORKSHOP");
        ddlWorkshop.DataBind();
        ddlWorkshop.Items.Insert(0, li);
        rpt.DataSource = new Common().GetAll("RVS_MASTERSPAREPARTS");
        rpt.DataBind();

    }
    private bool InsertSparePartsList(int workshopId)
    {
        bool isSelelected = false;
        obj.Delete(Convert.ToInt32(ddlWorkshop.SelectedValue));

        foreach (RepeaterItem r in rpt.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkRecom");
            if (chk.Checked)
            {
                isSelelected = true;

                HiddenField SparePartsId = (HiddenField)r.FindControl("hdSparePartsId");
                CheckBox chkrecom = (CheckBox)r.FindControl("chkRecom");

                obj.Insert(Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlWorkshop.SelectedValue), Convert.ToInt32(SparePartsId.Value), Convert.ToInt32(chkrecom.Checked));
            }
        }

        if (!isSelelected)
        {
            MessageController.Show("Please select at least One SpareParts !", MessageType.Warning, Page);
            return false;
        }
        return true;
    }


    protected void BindData()
    {
        if (!string.IsNullOrEmpty(ddlWorkshop.SelectedValue))
        {
            DataTable dt = obj.GetAll(Convert.ToInt32(ddlWorkshop.SelectedValue));
            rpt.DataSource = dt;
        }
        else
        {
            rpt.DataSource = null;
        }
        rpt.DataBind();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            InsertSparePartsList(Convert.ToInt32(ddlWorkshop.SelectedValue));
            MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }

    }
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        //loadTextboxlist();
    }
    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        throw new NotImplementedException();
    }
    protected void ddlWorkshop_OnTextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlWorkshop.SelectedValue) > 0)
        {
            DataTable dtt = obj.loadSparePartslist(Convert.ToInt32(ddlWorkshop.SelectedValue));
            foreach (DataRow dr in dtt.Rows)
            {
                foreach (RepeaterItem i in rpt.Items)
                {
                    HiddenField SparePartsId = (HiddenField)i.FindControl("hdSparePartsId");
                    if (SparePartsId.Value == dr["SPAREPARTSID"].ToString())
                    {
                        CheckBox chkrecom = (CheckBox)i.FindControl("chkRecom");
                        chkrecom.Checked = Convert.ToBoolean(dr["ACTIVE"]);
                        chkrecom.Checked = false;
                    }
                }
            }

            DataTable dt = obj.loadSparePartslist(Convert.ToInt32(ddlWorkshop.SelectedValue));
            foreach (DataRow dr in dt.Rows)
            {
                foreach (RepeaterItem i in rpt.Items)
                {
                    HiddenField SparePartsId = (HiddenField)i.FindControl("hdSparePartsId");
                    if (SparePartsId.Value == dr["SPAREPARTSID"].ToString())
                    {
                        CheckBox chkrecom = (CheckBox)i.FindControl("chkRecom");
                        chkrecom.Checked = Convert.ToBoolean(dr["ACTIVE"]);
                        chkrecom.Checked = true;
                    }
                }
            }
        }
    }
}