using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Pages_VISM_VclWrksp : BasePage
{
    dalVehicle obj = new dalVehicle();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
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
            else if (!dalCommon.IsPermitted(url))
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
            }
            ViewState["ID"] = (int)0;
            ViewState["DrawingPath"] = "";
            LoadDDL();
            //BindData();

        }

    }

    private void LoadDDL()
    {
        ListItem li = new ListItem("All", "0");
        ddlCompany.DataSource = new Common().GetAll("BS_COMPANY");
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, li);
        ddlBusinessCenter.DataSource = new Common().GetAll("BS_BUSINESSCENTER");
        ddlBusinessCenter.DataBind();
        ddlBusinessCenter.Items.Insert(0, li);
        ddlVehicleType.DataSource = new Common().GetAll("RVS_VEHICLETYPE");
        ddlVehicleType.DataBind();
        ddlVehicleType.Items.Insert(0, li);
        ddlEngineModel.DataSource = new Common().GetAll("RVS_ENGINEMODEL");
        ddlEngineModel.DataBind();
        ddlEngineModel.Items.Insert(0, li);
        ddlManufacturer.DataSource = new Common().GetAll("RVS_MANUFACTURER");
        ddlManufacturer.DataBind();
        ddlManufacturer.Items.Insert(0, li);
        ddlBrand.DataSource = new Common().GetAll("RVS_BRAND");
        ddlBrand.DataBind();
        ddlBrand.Items.Insert(0, li);
        ddlModel.DataSource = new Common().GetAll("RVS_MODEL");
        ddlModel.DataBind();
        ddlModel.Items.Insert(0, li);
        ddlBodyType.DataSource = new Common().GetAll("RVS_BODYTYPE");
        ddlBodyType.DataBind();
        ddlBodyType.Items.Insert(0, li);
        ddlFuleType.DataSource = new Common().GetAll("RVS_FUELTYPE");
        ddlFuleType.DataBind();
        ddlFuleType.Items.Insert(0, li);
        ddlOrigin.DataSource = new Common().GetAll("BS_COUNTRY");
        ddlOrigin.DataBind();
        ddlOrigin.Items.Insert(0, li);
        ddlCountryId.DataSource = new Common().GetAll("BS_COUNTRY");
        ddlCountryId.DataBind();
        ddlCountryId.Items.Insert(0, li);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(tbxChassisNo.Text))
        {
            try
            {
                int Company = string.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
                int BusinessCenter = string.IsNullOrEmpty(ddlBusinessCenter.SelectedValue) ? 0 : Convert.ToInt32(ddlBusinessCenter.SelectedValue);
                int CostCurrency = 0;
                int SellingCurrency = 0;
                int VehicleType = string.IsNullOrEmpty(ddlVehicleType.SelectedValue) ? 0 : Convert.ToInt32(ddlVehicleType.SelectedValue);
                int EngineModel = string.IsNullOrEmpty(ddlEngineModel.SelectedValue) ? 0 : Convert.ToInt32(ddlEngineModel.SelectedValue);
                int Manufacturer = string.IsNullOrEmpty(ddlManufacturer.SelectedValue) ? 0 : Convert.ToInt32(ddlManufacturer.SelectedValue);
                int Brand = string.IsNullOrEmpty(ddlBrand.SelectedValue) ? 0 : Convert.ToInt32(ddlBrand.SelectedValue);
                int Model = string.IsNullOrEmpty(ddlModel.SelectedValue) ? 0 : Convert.ToInt32(ddlModel.SelectedValue);
                int BodyType = string.IsNullOrEmpty(ddlBodyType.SelectedValue) ? 0 : Convert.ToInt32(ddlBodyType.SelectedValue);
                int FuleType = string.IsNullOrEmpty(ddlFuleType.SelectedValue) ? 0 : Convert.ToInt32(ddlFuleType.SelectedValue);
                int Origin = string.IsNullOrEmpty(ddlOrigin.SelectedValue) ? 0 : Convert.ToInt32(ddlOrigin.SelectedValue);
                int CountryId = string.IsNullOrEmpty(ddlCountryId.SelectedValue) ? 0 : Convert.ToInt32(ddlCountryId.SelectedValue);
                int TypeofUse = 0;
                int ManuFacturingYear = string.IsNullOrEmpty(tbxManufacturingYear.Text) ? 0 : Convert.ToInt32(tbxManufacturingYear.Text);
                decimal EngineCapacity = string.IsNullOrEmpty(tbxEngineCapacity.Text) ? 0 : Convert.ToDecimal(tbxEngineCapacity.Text);
                //decimal Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToDecimal(tbxMillage.Text);
                int NoofCylinder = 0;
                decimal CylinderCapacity = 0;
                int SeatingCapacity = 0;
                int NoofTyres = 0;
                int NoofAxles = 0;
                decimal CostPrice = 0;
                decimal SellingPrice = 0;
                decimal HorsePower = 0;
                decimal rpm = 0;

                int vehicleId = obj.Insert(Convert.ToInt32(hdnOwnerId1.Value), tbxChassisNo.Text, tbxRegistrationNo.Text, tbxEngineNo.Text, VehicleType, EngineModel, ManuFacturingYear, Manufacturer, tbxVIN.Text, EngineCapacity,
                   Brand, Model, BodyType, Origin, CountryId, FuleType, NoofCylinder, CylinderCapacity, "", SeatingCapacity, "", "", "", NoofTyres,
                    NoofAxles, TypeofUse, CostPrice, CostCurrency, SellingPrice, SellingCurrency, Company, BusinessCenter, "", HorsePower, rpm, "", "", "", "");

                if (Convert.ToInt32(hdnOwnerId1.Value) > 0)
                {
                    new dalSales().InsertOwner(vehicleId, Convert.ToInt32(hdnOwnerId1.Value), tbxBillingAddress1.Text, tbxContactPerson1.Text, tbxBillingMobile1.Text, tbxContactPhone1.Text, tbxContactFax1.Text, tbxContactEmail.Text, 0);
                }
                MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
                //BindData();
                ClearInputs(Controls);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog(ex);
                MessageController.Show(MessageCode.SaveFailed, MessageType.Error, Page);

            }
        }
        else
        {
            MessageController.Show("Chassis Number already exists. Please try anothor.", MessageType.Error, Page);
        }
    }
    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            int Company = string.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
            int BusinessCenter = string.IsNullOrEmpty(ddlBusinessCenter.SelectedValue) ? 0 : Convert.ToInt32(ddlBusinessCenter.SelectedValue);
            int CostCurrency = 0;
            int SellingCurrency = 0;
            int VehicleType = string.IsNullOrEmpty(ddlVehicleType.SelectedValue) ? 0 : Convert.ToInt32(ddlVehicleType.SelectedValue);
            int EngineModel = string.IsNullOrEmpty(ddlEngineModel.SelectedValue) ? 0 : Convert.ToInt32(ddlEngineModel.SelectedValue);
            int Manufacturer = string.IsNullOrEmpty(ddlManufacturer.SelectedValue) ? 0 : Convert.ToInt32(ddlManufacturer.SelectedValue);
            int Brand = string.IsNullOrEmpty(ddlBrand.SelectedValue) ? 0 : Convert.ToInt32(ddlBrand.SelectedValue);
            int Model = string.IsNullOrEmpty(ddlModel.SelectedValue) ? 0 : Convert.ToInt32(ddlModel.SelectedValue);
            int BodyType = string.IsNullOrEmpty(ddlBodyType.SelectedValue) ? 0 : Convert.ToInt32(ddlBodyType.SelectedValue);
            int FuleType = string.IsNullOrEmpty(ddlFuleType.SelectedValue) ? 0 : Convert.ToInt32(ddlFuleType.SelectedValue);
            int Origin = string.IsNullOrEmpty(ddlOrigin.SelectedValue) ? 0 : Convert.ToInt32(ddlOrigin.SelectedValue);
            int CountryId = string.IsNullOrEmpty(ddlCountryId.SelectedValue) ? 0 : Convert.ToInt32(ddlCountryId.SelectedValue);
            int TypeofUse = 0;
            int ManuFacturingYear = string.IsNullOrEmpty(tbxManufacturingYear.Text) ? 0 : Convert.ToInt32(tbxManufacturingYear.Text);
            decimal EngineCapacity = string.IsNullOrEmpty(tbxEngineCapacity.Text) ? 0 : Convert.ToDecimal(tbxEngineCapacity.Text);
            // decimal Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToDecimal(tbxMillage.Text);
            int NoofCylinder = 0;
            decimal CylinderCapacity = 0;
            int SeatingCapacity = 0;
            int NoofTyres = 0;
            int NoofAxles = 0;
            decimal CostPrice = 0;
            decimal SellingPrice = 0;
            decimal HorsePower = 0;
            decimal rpm = 0;
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(hdnOwnerId1.Value), tbxChassisNo.Text, tbxRegistrationNo.Text, tbxEngineNo.Text, VehicleType, EngineModel, ManuFacturingYear, Manufacturer, tbxVIN.Text, EngineCapacity,
                    Brand, Model, BodyType, Origin, CountryId, FuleType, NoofCylinder, CylinderCapacity, "", SeatingCapacity, "", "", "", NoofTyres,
                    NoofAxles, TypeofUse, CostPrice, CostCurrency, SellingPrice, SellingCurrency, Company, BusinessCenter, "", HorsePower, rpm, "", "", "", "");
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            //BindData();

            ClearInputs(Controls);
            MessageBox(UpdateSuccess);
            ChangeToSaveMode(Controls);
        }
        catch (Exception ex)
        {
            MessageBox(UpdateFail + ex.Message);
            //ErrorLog.WriteLog(ex);
            //MessageController.Show(MessageCode.UpdateFailed, MessageType.Error, Page);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearInputs(Controls);
    }
    //protected void ClearAll()
    //{
    //    tbxChassisNo.Text = "";
    //    tbxEngineCapacity.Text = "";
    //    tbxEngineNo.Text = "";
    //    tbxManufacturingYear.Text = "";
    //    //tbxMillage.Text = "";
    //    tbxRegistrationNo.Text = "";
    //    tbxVIN.Text = "";
    //    btnSave.Visible = true;
    //    btnEdit.Visible = false;
    //}
    //protected void BindData(string crt)
    //{
    //   // DataTable dt = new Common().GetAll("RVS_VEHICLE");
    //    DataTable dt = obj.VehicleInfo(crt);
    //    rpt.DataSource = dt;
    //    rpt.DataBind();
    //}

    //protected void btnEdit_Command(object sender, CommandEventArgs e)
    //{
    //    int ID = Convert.ToInt32(e.CommandArgument);
    //    DataTable dt = obj.GetById(ID);
    //    if (dt.Rows.Count > 0)
    //    {
    //        tbxChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
    //        tbxEngineCapacity.Text = dt.Rows[0]["ENGINECAPACITY"].ToString();
    //        tbxEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
    //        tbxManufacturingYear.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
    //       // tbxMillage.Text = dt.Rows[0]["MILLAGE"].ToString();
    //        tbxRegistrationNo.Text = dt.Rows[0]["REGNO"].ToString();
    //        tbxVIN.Text = dt.Rows[0]["VIN"].ToString();
    //        ddlBodyType.SelectedValue = dt.Rows[0]["BODYTYPEID"].ToString();
    //        ddlBrand.SelectedValue = dt.Rows[0]["BRANDID"].ToString();
    //        ddlBusinessCenter.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
    //        ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
    //        ddlEngineModel.SelectedValue = dt.Rows[0]["ENGINEMODELID"].ToString();
    //        ddlFuleType.SelectedValue = dt.Rows[0]["FUELYTYPEID"].ToString();
    //        ddlManufacturer.SelectedValue = dt.Rows[0]["MANUFACTURERID"].ToString();
    //        ddlModel.SelectedValue = dt.Rows[0]["MODELID"].ToString();
    //        ddlOrigin.SelectedValue = dt.Rows[0]["COUNTRYOFORIGINID"].ToString();
    //        ddlVehicleType.SelectedValue = dt.Rows[0]["VEHICLETYPEID"].ToString();
    //        ViewState["DrawingPath"] = dt.Rows[0]["DRAWINGFILE"].ToString();
    //        ViewState["ID"] = ID;
    //    }
    //    btnSave.Visible = false;
    //    btnEdit.Visible = true;
    //}
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_VEHICLE", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            //BindData();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
            MessageController.Show(MessageCode.DeleteFailed, MessageType.Error, Page);
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomer(string prefixText, int count)
    {
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = @"SELECT ID||'-'||CUSTOMERNAME ||'-'|| MOBILE CUSTOMERNAME FROM RVS_CUSTOMER WHERE (CUSTOMERNAME LIKE '" + prefixText + "%' OR ID LIKE '" + prefixText + "%' OR MOBILE LIKE '" + prefixText + "%')  AND ROWNUM <= 20";
                cmd.Connection = conn;
                conn.Open();
                List<string> items = new List<string>();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        items.Add(sdr["CUSTOMERNAME"].ToString());
                    }
                }
                conn.Close();
                return items;
            }
        }
    }

    protected void tbxOwnerName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] a = tbxOwnerName.Text.Trim().Split('-');
            DataTable dt = new dalVISMCustomer().GetById(Convert.ToInt32(a[0]));
            if (dt.Rows.Count > 0)
            {
                tbxOwnerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                tbxOwnerMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                hdnOwnerId1.Value = a[0];
            }
            else
            {
                tbxOwnerName.Text = "";
                hdnOwnerId1.Value = "0";
            }
        }
        catch
        {
            tbxOwnerName.Text = "";
            hdnOwnerId1.Value = "0";
        }
    }

    protected void btnVehicleSearch_OnClick(object sender, EventArgs e)
    {
        GetCriteria();
        BindRpt();
    }


    private void BindRpt()
    {
        rptMain.DataSource = RptData();
        rptMain.DataBind();
        if (rptMain.Items.Count > 0)
        {
            pnlReportButton.Visible = true;
        }
    }
    private bool _firstTime = true;
    private DataTable dtRpt = new DataTable();
    public DataTable RptData()
    {
        if (_firstTime)
        {
            qry = @"Select vh.id,vh.REGNO,vh.CHASSISNO,vh.ENGINENO,mo.MODELNAME,mn.MANUFACTURERNAME,
            vh.MANUFACTURINGYEAR,vt.VEHICLETYPENAME,ft.FUELTYPENAME,bc.CENTERNAME,
            cu.CUSTOMERNAME,co.COMPANYNAME,cn.COUNTRYNAME,vh.KPL from rvs_Vehicle vh
            left join rvs_model mo on vh.MODELID=mo.ID
            left join RVS_MANUFACTURER mn on vh.MANUFACTURERID=mn.ID
            left join RVS_VEHICLETYPE vt on vh.VEHICLETYPEID=vt.ID
            left join RVS_FUELTYPE ft on vh.FUELYTYPEID=ft.ID
            left join bs_businesscenter bc on vh.BUSINESSCENTERID=bc.ID
            left join RVS_CUSTOMER cu on vh.CUSTOMERID=cu.ID
            left join bs_company co on vh.COMPANYID=co.id
            left join bs_country cn on vh.COUNTRYID=cn.ID" + crt;

            dtRpt = dm.ExecuteQuery(qry);
            _firstTime = false;

        }

        return dtRpt;
    }

    protected int ID = 0;
    private int _rw;
    protected void rptMain_ItemBound(object sender, RepeaterItemEventArgs e)
    {
        bool hideIdColumn = true;
        if (e.Item.ItemType == ListItemType.Header)
        {
            Repeater headerRpt = e.Item.FindControl("hdrRpt") as Repeater;
            headerRpt.DataSource = GetHeader(RptData(), hideIdColumn);
            headerRpt.DataBind();


        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            bodyRpt.DataSource = DtToList(_rw, RptData(), hideIdColumn);
            bodyRpt.DataBind();
            _rw++;
            //ID+= Convert.ToInt32(((DataRowView)e.Item.DataItem).Row.ItemArray[0]);
        }

    }
    protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            qry = @"SELECT * FROM rvs_Vehicle where ID='" + e.CommandArgument + "'";
            DataTable dt = dm.ExecuteQuery(qry);
            tbxChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
            tbxRegistrationNo.Text = dt.Rows[0]["REGNO"].ToString();
            tbxEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
            ddlVehicleType.SelectedValue = dt.Rows[0]["VEHICLETYPEID"].ToString();
            tbxVIN.Text = dt.Rows[0]["VIN"].ToString();
            ddlEngineModel.SelectedValue = dt.Rows[0]["ENGINEMODELID"].ToString();
            ddlManufacturer.SelectedValue = dt.Rows[0]["MANUFACTURERID"].ToString();
            tbxVIN.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
            tbxEngineCapacity.Text = dt.Rows[0]["ENGINECAPACITY"].ToString();
            ddlBrand.SelectedValue = dt.Rows[0]["BRANDID"].ToString();
            ddlModel.SelectedValue = dt.Rows[0]["MODELID"].ToString();
            ddlBodyType.SelectedValue = dt.Rows[0]["BODYTYPEID"].ToString();
            ddlFuleType.SelectedValue = dt.Rows[0]["FUELYTYPEID"].ToString();
            ddlOrigin.SelectedValue = dt.Rows[0]["COUNTRYOFORIGINID"].ToString();
            ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
            ddlBusinessCenter.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
            ddlCountryId.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();


            ViewState["ID"] = e.CommandArgument.ToString();
            ChangeToUpdateMode(Controls);

        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
        }

    }

    protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            qry = @"DELETE FROM rvs_Vehicle WHERE ID='" + e.CommandArgument + "'";
            dm.ExecuteNonQuery(qry);
            MessageBox(DeleteSuccess);
            BindRpt();
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail + ex.Message);
        }

    }
    private string crt = " WHERE 1=1 ";
    private void GetCriteria()
    {
        if (tbxChassisNo.Text != "") crt += " AND VH.CHASSISNO='" + tbxChassisNo.Text + "'";
        if (tbxRegistrationNo.Text != "") crt += " AND VH.REGNO='" + tbxRegistrationNo.Text + "'";
        if (tbxEngineNo.Text != "") crt += " AND VH.ENGINENO='" + tbxEngineNo.Text + "'";
        if (ddlVehicleType.SelectedValue != "0") crt += " AND VH.VEHICLETYPEID='" + ddlVehicleType.SelectedValue + "'";
        if (tbxVIN.Text != "") crt += " AND VH.VIN='" + tbxVIN.Text + "'";
        if (ddlEngineModel.SelectedValue != "0") crt += " AND VH.ENGINEMODELID='" + ddlEngineModel.SelectedValue + "'";
        if (ddlManufacturer.SelectedValue != "0") crt += " AND VH.MANUFACTURERID='" + ddlManufacturer.SelectedValue + "'";
        if (tbxManufacturingYear.Text != "") crt += " AND VH.MANUFACTURINGYEAR='" + tbxManufacturingYear.Text + "'";
        if (tbxEngineCapacity.Text != "") crt += " AND VH.ENGINECAPACITY='" + tbxEngineCapacity.Text + "'";
        if (ddlBrand.SelectedValue != "0") crt += " AND VH.BRANDID='" + ddlBrand.SelectedValue + "'";
        if (ddlModel.SelectedValue != "0") crt += " AND VH.MODELID='" + ddlModel.SelectedValue + "'";
        if (ddlBodyType.SelectedValue != "0") crt += " AND VH.BODYTYPEID='" + ddlBodyType.SelectedValue + "'";
        if (ddlFuleType.SelectedValue != "0") crt += " AND VH.FUELYTYPEID='" + ddlFuleType.SelectedValue + "'";
        if (ddlOrigin.SelectedValue != "0") crt += " AND VH.COUNTRYOFORIGINID='" + ddlOrigin.SelectedValue + "'";
        if (ddlCompany.SelectedValue != "0") crt += " AND VH.COMPANYID='" + ddlCompany.SelectedValue + "'";
        if (ddlBusinessCenter.SelectedValue != "0") crt += " AND VH.BUSINESSCENTERID='" + ddlBusinessCenter.SelectedValue + "'";
        if (ddlCountryId.SelectedValue != "0") crt += " AND VH.COUNTRYID='" + ddlCountryId.SelectedValue + "'";

    }

    private string GetQry()
    {
        string qry = @"";
        return qry;
    }
    protected void btnOpenPdf_OnClick(object sender, EventArgs e)
    {


        //GetCriteria();
        //string qry = GetQry();
        //DataTable dt = dm.ExecuteQuery(qry);
        //string path = "/Pages/ProductionReport/ReportFile/CrProductionDetail.rpt";
        //GetCrReport(path, dt, ReportAction.OpenPdf);
        ScriptManager.RegisterStartupScript(this, GetType(), "UnderdevelopmentMsg", "alert('Under Development')", true);
    }

    protected void btnDownloadPdf_OnClick(object sender, EventArgs e)
    {
        //GetCriteria();
        //DataTable dt = RptData();//your datatable
        //StringBuilder sb = new StringBuilder();
        //string[] columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
        //sb.AppendLine(string.Join(",", columnNames));
        //foreach (DataRow row in dt.Rows)
        //{
        //    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
        //    sb.AppendLine(string.Join(",", fields));
        //}
        //Response.ClearContent();
        //Response.Clear();
        //Response.ContentType = "text/csv";
        //Response.AddHeader("Content-Disposition", "attachment; filename=DownloadedData.csv;");
        //Response.Write(sb.ToString());
        //Response.Flush();
        //Response.End();

        //GetCriteria();
        //string qry = GetQry();
        //DataTable dt = dm.ExecuteQuery(qry);
        //string path = "/Pages/ProductionReport/ReportFile/CrProductionDetail.rpt";
        //GetCrReport(path, dt, ReportAction.DownloadPdf);
        //ScriptManager.RegisterStartupScript(this, GetType(), "UnderdevelopmentMsg", "alert('Under Development')", true);
    }

    //protected void btndownloadExcel_OnClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ExportToExcel(rptMain);
    //    }
    //    catch (Exception ex)
    //    {
    //        string s = ex.Message;
    //    }



    //    //GetCriteria();
    //    //string qry = GetQry();
    //    //DataTable dt = dm.ExecuteQuery(qry);
    //    //string path = "/Pages/ProductionReport/ReportFile/CrProductionDetail.rpt";
    //    //GetCrReport(path, dt, ReportAction.DownloadExcel);
    //    // ScriptManager.RegisterStartupScript(this, GetType(), "UnderdevelopmentMsg", "alert('Under Development')", true);
    //}

    //protected void ExportToExcel()
    //{
    //    StringWriter swr = new StringWriter();
    //    HtmlTextWriter htmlwr = new HtmlTextWriter(swr);
    //    rptMain.RenderControl(htmlwr);
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("content-disposition",
    //        "attachment;filename=ExportData-" + DateTime.Now.ToString("dd-mm-yyyy--hh-MM-tt") + ".xls");
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.Output.Write(swr.ToString());
    //    Response.Flush();
    //    Response.End();
    //}
    protected void lnkDownloadExcel_OnClick(object sender, EventArgs e)
    {
        try
        {
            ExportToExcel(rptMain);
        }
        catch (Exception ex)
        {
            MessageBox("Error: " + ex.Message);
        }
    }
}