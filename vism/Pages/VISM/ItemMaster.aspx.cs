using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;


public partial class Pages_VISM_ItemMaster : System.Web.UI.Page
{
    //dalTeam obj = new dalTeam();
    dalItemMaster obj = new dalItemMaster();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);
        if (!IsPostBack)
        {
            string url = "/Pages/VISM/" + Path.GetFileName(Request.PhysicalPath) + Request.Url.Query;
            if (SessionManager.SessionName.UserName == null)
            {
                dalCommon.LoginReturnUrl(url);
            }
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            ViewState["ID"] = (int)0;
            //   loadDDl();
            BindData();
            //SelectWorkshop();
            SelectItemCategory();
            SelectItemUnit();

        }

    }
    private void SelectItemCategory()
    {
        ListItem li = new ListItem("Select", "");
        string qry = "";
        qry = @"SELECT ITEMCATEGORYCODE, ITEMCATEGORYNAME FROM RVS_ITEMCATEGORY WHERE ISACTIVE=1 ORDER BY ITEMCATEGORYNAME ASC";
        ddlItemCategory.DataSource = dm.ExecuteQuery(qry);
        ddlItemCategory.DataBind();
        ddlItemCategory.Items.Insert(0, li);
    }
    private void SelectItemUnit()
    {
        ListItem li = new ListItem("Select", "");
        string qry = "";
        qry = @"SELECT UNITCODE, UNITNAME FROM RVS_SCMUNIT WHERE ISACTIVE=1 ORDER BY UNITNAME ASC";
        ddlUnit.DataSource = dm.ExecuteQuery(qry);
        ddlUnit.DataBind();
        ddlUnit.Items.Insert(0, li);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(tbxItemCode.Text))
        {
            try
            {
                obj.Insert(tbxItemCode.Text, tbxItemName.Text, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue);

                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                BindData();
                ClearAll();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

            }
        }
        else
        {
            MessageController.Show("This Item Code already exists. Please try anothor.", MessageType.Error, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.Update( ddlItemCategory.SelectedValue, tbxItemCode.Text, tbxItemName.Text, ddlUnit.SelectedValue);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            //BindData();
            ClearAll();
            BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        tbxItemCode.Text = "";
        tbxItemName.Text = "";
        ddlUnit.SelectedValue = "";

        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {

            DataTable dt = obj.GetAll();
            rpt.DataSource = dt;
        rpt.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        string ITEMCODE = e.CommandArgument.ToString();
        DataTable dt = obj.GetById(ITEMCODE);
        if (dt.Rows.Count > 0)
        {
            ddlItemCategory.SelectedValue = dt.Rows[0]["ITEMCATEGORYCODE"].ToString();
            tbxItemCode.Text = dt.Rows[0]["ITEMCODE"].ToString();
            tbxItemName.Text = dt.Rows[0]["ITEMNAME"].ToString();
            ddlUnit.SelectedValue = dt.Rows[0]["SCMUNITCODE"].ToString();

            ViewState["ITEMCODE"] = ITEMCODE;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
        tbxItemCode.Enabled = false;
    }
    protected void btnUploadExcel_Click(object sender, EventArgs e)
    {
        if (fuExcel.HasFile)
        {
            string filePath = Server.MapPath("~/TempExcel/" + fuExcel.FileName);
            fuExcel.SaveAs(filePath);

            DataTable dtExcel = ReadExcel(filePath);

            if (dtExcel.Rows.Count > 0)
            {
                SaveBulkItem(dtExcel);
                BindData();

                MessageController.Show("Excel Upload Successfully", MessageType.Information, Page);
            }
        }
    }
    private DataTable ReadExcel(string filePath)
    {
        string connString = "";

        if (Path.GetExtension(filePath) == ".xls")
        {
            connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                         ";Extended Properties='Excel 8.0;HDR=YES;'";
        }
        else
        {
            connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                         ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
        }

        DataTable dt = new DataTable();

        using (OleDbConnection conn = new OleDbConnection(connString))
        {
            conn.Open();

            DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", conn);
            da.Fill(dt);
        }

        return dt;
    }
    private void SaveBulkItem(DataTable dt)
    {
        string msg = "";

        foreach (DataRow row in dt.Rows)
        {
            string category = row["ITEMCATEGORYCODE"].ToString().Trim();
            string code = row["ITEMCODE"].ToString().Trim();
            string name = row["ITEMNAME"].ToString().Trim();
            string unit = row["UNITCODE"].ToString().Trim();

            if (code == "" || name == "")
                continue;

            // Category Check
            if (!IsItemCategoryExist(category))
            {
                msg += "Item Code: " + code + " → Invalid ITEMCATEGORYCODE <br/>";
                continue;
            }

            // Unit Check
            if (!IsUnitExist(unit))
            {
                msg += "Item Code: " + code + " → Invalid UNITCODE <br/>";
                continue;
            }

            // Duplicate Check
            if (!obj.CheckDuplicate(code))
            {
                msg += "Item Code: " + code + " already exists <br/>";
                continue;
            }

            // Insert
            obj.Insert(code, name, category, unit);
        }

        if (msg != "")
        {
            lblError.Text = msg;
        }
    }
    private bool IsItemCategoryExist(string categoryCode)
    {
        string qry = "SELECT COUNT(*) FROM RVS_ITEMCATEGORY WHERE ITEMCATEGORYCODE = '" + categoryCode + "'";

        DataTable dt = dm.ExecuteQuery(qry);

        if (Convert.ToInt32(dt.Rows[0][0]) > 0)
            return true;
        else
            return false;
    }

    private bool IsUnitExist(string unitCode)
    {
        string qry = "SELECT COUNT(*) FROM RVS_SCMUNIT WHERE UNITCODE = '" + unitCode + "'";

        DataTable dt = dm.ExecuteQuery(qry);

        if (Convert.ToInt32(dt.Rows[0][0]) > 0)
            return true;
        else
            return false;
    }

    protected void btnDownloadTemplate_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/Templates/Item_Master_Template.xlsx");

        if (File.Exists(filePath))
        {
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Item_Master_Template.xlsx");
            Response.TransmitFile(filePath);
            Response.End();
        }
        else
        {
            Response.Write("File not found.");
        }
    }

}