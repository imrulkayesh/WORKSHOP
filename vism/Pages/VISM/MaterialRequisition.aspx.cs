using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_MaterialRequisition : BasePage
{
    dalJobCard obj = new dalJobCard();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + System.IO.Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted("/Pages/VISM/" + System.IO.Path.GetFileName(Request.PhysicalPath)))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            try
            {
                JOBID = (int)0;
                dtParts = dtStoreParts();
                BindData();
            }
            catch (Exception ex)
            {
                MessageController.Show(ex.Message, MessageType.Error, Page);
            }
        }

    }
    int JOBID
    {
        set { ViewState["JOBID"] = value; }
        get
        {
            try
            {
                return (int)ViewState["JOBID"];
            }
            catch
            {
                return 0;
            }
        }
    }
    DataTable dtParts
    {
        set { ViewState["dtParts"] = value; }
        get
        {
            try
            {
                return (DataTable)ViewState["dtParts"];
            }
            catch
            {
                return null;
            }
        }
    }
    private DataTable dtStoreParts()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARTSID");
        dt.Columns.Add("JOBID");
        dt.Columns.Add("STOREQTY");
        return dt;
    }
    private void LoadParts()
    {
        rptParts.DataSource = obj.GetPartsStore(JOBID);
        rptParts.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (JOBID > 0)
            {
                foreach (RepeaterItem r in rptParts.Items)
                {
                    CheckBox chk = (CheckBox)r.FindControl("chkrow");
                    if (chk.Checked)
                    {
                        TextBox StoreQty = (TextBox)r.FindControl("tbxStoreQty");
                        HiddenField PartsId = (HiddenField)r.FindControl("hdnPartsId");
                        dtParts.Rows.Add(PartsId.Value, JOBID, StoreQty.Text);
                    }
                }
                obj.UpdateStoreParts(dtParts);
                dtParts = dtStoreParts();
                MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            }
            else
            {
                MessageController.Show("Please select a jobcard", MessageType.Error, Page);
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void BindData()
    {
        DataTable dt = obj.JobCardForStore();
        rpt.DataSource = dt;
        rpt.DataBind();
    }


    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        JOBID = Convert.ToInt32(e.CommandArgument);
        LoadParts();
    }
}