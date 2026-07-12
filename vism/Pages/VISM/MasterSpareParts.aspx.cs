using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_MasterSpareParts : BasePage
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
        ddlCountry.DataSource = new Common().GetAll("BS_BUSINESSCENTER");
        ddlCountry.DataBind();
        ddlWorkshop.DataSource = new Common().GetAll("PR_MLINE");
        ddlWorkshop.DataBind();
        ddlWorkshop.Items.Insert(0, li);
        rpt.DataSource = new Common().GetAll("PR_TEXTBOX");
        rpt.DataBind();

    }
    private bool InsertTextboxList(int lineId)
    {
        bool isSelelected = false;
       // obj.Delete(Convert.ToInt32(ddlLine.SelectedValue));

        foreach (RepeaterItem r in rpt.Items)
        {
            CheckBox chk = (CheckBox)r.FindControl("chkRecom");
            if (chk.Checked)
            {
                isSelelected = true;

                HiddenField LabelId = (HiddenField)r.FindControl("hdnLabelId");
                HiddenField TextboxId = (HiddenField)r.FindControl("hdnTextboxId");
                CheckBox chkrecom = (CheckBox)r.FindControl("chkRecom");

                obj.Insert( Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(LabelId.Value), Convert.ToInt32(TextboxId.Value), Convert.ToInt32(chkrecom.Checked));
               // obj.Insert(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlLine.SelectedValue), Convert.ToInt32(LabelId.Value), Convert.ToInt32(TextboxId.Value), Convert.ToInt32(chkrecom.Checked));
            }
        }

        if (!isSelelected)
        {
            MessageController.Show("Please select at least One Textbox !", MessageType.Warning, Page);
            return false;
        }
        return true;
    }


    protected void BindData()
    {
        //if (!string.IsNullOrEmpty(ddlLine.SelectedValue))
        //{
        //    DataTable dt = obj.GetAll(Convert.ToInt32(ddlLine.SelectedValue));
        //    rpt.DataSource = dt;
        //}
        //else
        //{
        //    rpt.DataSource = null;
        //}
        //rpt.DataBind();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
           // InsertTextboxList(Convert.ToInt32(ddlLine.SelectedValue));
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

    protected void ddlLine_OnTextChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlLine.SelectedValue) > 0)
        //{
        //    DataTable dtt = obj.loadTextboxlist(Convert.ToInt32(ddlLine.SelectedValue));
        //    foreach (DataRow dr in dtt.Rows)
        //    {
        //        foreach (RepeaterItem i in rpt.Items)
        //        {
        //            HiddenField LabelId = (HiddenField)i.FindControl("hdnLabelId");
        //            if (LabelId.Value == dr["LABELID"].ToString())
        //            {
        //                CheckBox chkrecom = (CheckBox)i.FindControl("chkRecom");
        //                chkrecom.Checked = Convert.ToBoolean(dr["ACTIVE"]);
        //                chkrecom.Checked = false;
        //            }
        //        }
        //    }

        //    DataTable dt = obj.loadTextboxlist(Convert.ToInt32(ddlLine.SelectedValue));
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        foreach (RepeaterItem i in rpt.Items)
        //        {
        //            HiddenField LabelId = (HiddenField)i.FindControl("hdnLabelId");
        //            if (LabelId.Value == dr["LABELID"].ToString())
        //            {
        //                CheckBox chkrecom = (CheckBox)i.FindControl("chkRecom");
        //                chkrecom.Checked = Convert.ToBoolean(dr["ACTIVE"]);
        //                chkrecom.Checked = true;
        //            }
        //        }
        //    }
       // }
    }
}