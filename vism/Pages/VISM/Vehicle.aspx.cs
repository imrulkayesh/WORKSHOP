using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_Vehicle : BasePage
{
    dalVehicle obj = new dalVehicle();
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
            ViewState["DrawingPath"] = "";
            LoadDDL();
            BindData();

        }

    }
    private void LoadDDL()
    {
        ListItem li = new ListItem("Select","0");
        ddlCompany.DataSource = new Common().GetAll("BS_COMPANY");
        ddlCompany.DataBind();
        ddlBusinessCenter.DataSource = new Common().GetAll("BS_BUSINESSCENTER");
        ddlBusinessCenter.DataBind();
        ddlBusinessCenter.Items.Insert(0,li);
        ddlCostCurrency.DataSource = new Common().GetAll("ASSET_CURRENCY");
        ddlCostCurrency.DataBind();
        ddlCostCurrency.Items.Insert(0,li);
        ddlSellingCurrency.DataSource = new Common().GetAll("ASSET_CURRENCY");
        ddlSellingCurrency.DataBind();
        ddlSellingCurrency.Items.Insert(0,li);
        ddlVehicleType.DataSource = new Common().GetAll("RVS_VEHICLETYPE");
        ddlVehicleType.DataBind();
        ddlEngineModel.DataSource = new Common().GetAll("RVS_ENGINEMODEL");
        ddlEngineModel.DataBind();
        ddlManufacturer.DataSource = new Common().GetAll("RVS_MANUFACTURER");
        ddlManufacturer.DataBind();
        ddlBrand.DataSource = new Common().GetAll("RVS_BRAND");
        ddlBrand.DataBind();
        ddlModel.DataSource = new Common().GetAll("RVS_MODEL");
        ddlModel.DataBind();
        ddlBodyType.DataSource = new Common().GetAll("RVS_BODYTYPE");
        ddlBodyType.DataBind();
        ddlFuleType.DataSource = new Common().GetAll("RVS_FUELTYPE");
        ddlFuleType.DataBind();
        ddlOrigin.DataSource = new Common().GetAll("BS_COUNTRY");
        ddlOrigin.DataBind();
        ddlTypeofUse.DataSource = new Common().GetAll("RVS_USETYPE");
        ddlTypeofUse.DataBind();
        ddlTypeofUse.Items.Insert(0,li);
    }
    private string FileUpload()
    {
        string AttachFile = ViewState["DrawingPath"].ToString();
        if (flDrawing.HasFile)
        {
            AttachFile = tbxChassisNo.Text + "_" + flDrawing.FileName;
            string MediumImagePath = Server.MapPath("~/Content/Attachment/VISMDrawing/" + AttachFile);
            flDrawing.SaveAs(string.Concat(MediumImagePath));

        }
        return AttachFile;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (obj.CheckDuplicate(tbxChassisNo.Text))
        {
            try
            {
                int Company = string.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
                int BusinessCenter = string.IsNullOrEmpty(ddlBusinessCenter.SelectedValue) ? 0 : Convert.ToInt32(ddlBusinessCenter.SelectedValue);
                int CostCurrency = string.IsNullOrEmpty(ddlCostCurrency.SelectedValue) ? 0 : Convert.ToInt32(ddlCostCurrency.SelectedValue);
                int SellingCurrency = string.IsNullOrEmpty(ddlSellingCurrency.SelectedValue) ? 0 : Convert.ToInt32(ddlSellingCurrency.SelectedValue);
                int VehicleType = string.IsNullOrEmpty(ddlVehicleType.SelectedValue) ? 0 : Convert.ToInt32(ddlVehicleType.SelectedValue);
                int EngineModel = string.IsNullOrEmpty(ddlEngineModel.SelectedValue) ? 0 : Convert.ToInt32(ddlEngineModel.SelectedValue);
                int Manufacturer = string.IsNullOrEmpty(ddlManufacturer.SelectedValue) ? 0 : Convert.ToInt32(ddlManufacturer.SelectedValue);
                int Brand = string.IsNullOrEmpty(ddlBrand.SelectedValue) ? 0 : Convert.ToInt32(ddlBrand.SelectedValue);
                int Model = string.IsNullOrEmpty(ddlModel.SelectedValue) ? 0 : Convert.ToInt32(ddlModel.SelectedValue);
                int BodyType = string.IsNullOrEmpty(ddlBodyType.SelectedValue) ? 0 : Convert.ToInt32(ddlBodyType.SelectedValue);
                int FuleType = string.IsNullOrEmpty(ddlFuleType.SelectedValue) ? 0 : Convert.ToInt32(ddlFuleType.SelectedValue);
                int Origin = string.IsNullOrEmpty(ddlOrigin.SelectedValue) ? 0 : Convert.ToInt32(ddlOrigin.SelectedValue);
                int TypeofUse = string.IsNullOrEmpty(ddlTypeofUse.SelectedValue) ? 0 : Convert.ToInt32(ddlTypeofUse.SelectedValue);
                int ManuFacturingYear = string.IsNullOrEmpty(tbxManufacturingYear.Text) ? 0 : Convert.ToInt32(tbxManufacturingYear.Text);
                decimal EngineCapacity = string.IsNullOrEmpty(tbxEngineCapacity.Text) ? 0 : Convert.ToDecimal(tbxEngineCapacity.Text);
                //  decimal Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToDecimal(tbxMillage.Text);
                int CountryId = string.IsNullOrEmpty(ddlCountryId.SelectedValue) ? 0 : Convert.ToInt32(ddlCountryId.SelectedValue);
                int NoofCylinder = string.IsNullOrEmpty(tbxNoofCylinder.Text) ? 0 : Convert.ToInt32(tbxNoofCylinder.Text);
                decimal CylinderCapacity = string.IsNullOrEmpty(tbxCylCapacity.Text) ? 0 : Convert.ToDecimal(tbxCylCapacity.Text);
                int SeatingCapacity = string.IsNullOrEmpty(tbxSeats.Text) ? 0 : Convert.ToInt32(tbxSeats.Text);
                int NoofTyres = string.IsNullOrEmpty(tbxNoofTyre.Text) ? 0 : Convert.ToInt32(tbxNoofTyre.Text);
                int NoofAxles = string.IsNullOrEmpty(tbxNoofAxles.Text) ? 0 : Convert.ToInt32(tbxNoofAxles.Text);
                decimal CostPrice = string.IsNullOrEmpty(tbxCost.Text) ? 0 : Convert.ToDecimal(tbxCost.Text);
                decimal SellingPrice = string.IsNullOrEmpty(tbxSellingPrice.Text) ? 0 : Convert.ToDecimal(tbxSellingPrice.Text);
                decimal HorsePower = string.IsNullOrEmpty(tbxHorsePower.Text) ? 0 : Convert.ToDecimal(tbxHorsePower.Text);
                decimal rpm = string.IsNullOrEmpty(tbxRpm.Text) ? 0 : Convert.ToDecimal(tbxRpm.Text);
                

               int vehicleId= obj.Insert(Convert.ToInt32(hdnOwnerId1.Value),tbxChassisNo.Text, tbxRegistrationNo.Text, tbxEngineNo.Text,
                   VehicleType, EngineModel, ManuFacturingYear, Manufacturer, tbxVIN.Text, EngineCapacity,Brand, Model, BodyType, Origin, CountryId,
                   FuleType, NoofCylinder, CylinderCapacity, tbxWheelBase.Text, SeatingCapacity, tbxLadenWeight.Text, tbxUnladenWeight.Text,
                   tbxTyreSize.Text, NoofTyres, NoofAxles, TypeofUse, CostPrice, CostCurrency, SellingPrice, SellingCurrency, Company, 
                   BusinessCenter, tbxAxleWeights.Text, HorsePower, rpm, tbxImportDate.Text, tbxApprovedBy.Text, FileUpload(), tbxApproveDate.Text);
                if (Convert.ToInt32(hdnOwnerId1.Value) > 0)
                {
                    new dalSales().InsertOwner(vehicleId, Convert.ToInt32(hdnOwnerId1.Value), tbxBillingAddress1.Text, tbxContactPerson1.Text, tbxBillingMobile1.Text, tbxContactPhone1.Text, tbxContactFax1.Text, tbxContactEmail.Text,0);
                }
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
            MessageController.Show("Chassis Number already exists. Please try anothor.", MessageType.Error, Page);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int Company = string.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
            int BusinessCenter = string.IsNullOrEmpty(ddlBusinessCenter.SelectedValue) ? 0 : Convert.ToInt32(ddlBusinessCenter.SelectedValue);
            int CostCurrency = string.IsNullOrEmpty(ddlCostCurrency.SelectedValue) ? 0 : Convert.ToInt32(ddlCostCurrency.SelectedValue);
            int SellingCurrency = string.IsNullOrEmpty(ddlSellingCurrency.SelectedValue) ? 0 : Convert.ToInt32(ddlSellingCurrency.SelectedValue);
            int VehicleType = string.IsNullOrEmpty(ddlVehicleType.SelectedValue) ? 0 : Convert.ToInt32(ddlVehicleType.SelectedValue);
            int EngineModel = string.IsNullOrEmpty(ddlEngineModel.SelectedValue) ? 0 : Convert.ToInt32(ddlEngineModel.SelectedValue);
            int Manufacturer = string.IsNullOrEmpty(ddlManufacturer.SelectedValue) ? 0 : Convert.ToInt32(ddlManufacturer.SelectedValue);
            int Brand = string.IsNullOrEmpty(ddlBrand.SelectedValue) ? 0 : Convert.ToInt32(ddlBrand.SelectedValue);
            int Model = string.IsNullOrEmpty(ddlModel.SelectedValue) ? 0 : Convert.ToInt32(ddlModel.SelectedValue);
            int BodyType = string.IsNullOrEmpty(ddlBodyType.SelectedValue) ? 0 : Convert.ToInt32(ddlBodyType.SelectedValue);
            int FuleType = string.IsNullOrEmpty(ddlFuleType.SelectedValue) ? 0 : Convert.ToInt32(ddlFuleType.SelectedValue);
            int Origin = string.IsNullOrEmpty(ddlOrigin.SelectedValue) ? 0 : Convert.ToInt32(ddlOrigin.SelectedValue);
            int TypeofUse = string.IsNullOrEmpty(ddlTypeofUse.SelectedValue) ? 0 : Convert.ToInt32(ddlTypeofUse.SelectedValue);
            int ManuFacturingYear = string.IsNullOrEmpty(tbxManufacturingYear.Text) ? 0 : Convert.ToInt32(tbxManufacturingYear.Text);
            decimal EngineCapacity = string.IsNullOrEmpty(tbxEngineCapacity.Text) ? 0 : Convert.ToDecimal(tbxEngineCapacity.Text);
            // decimal Millage = string.IsNullOrEmpty(tbxMillage.Text) ? 0 : Convert.ToDecimal(tbxMillage.Text);
            int CountryId = string.IsNullOrEmpty(ddlCountryId.SelectedValue) ? 0 : Convert.ToInt32(ddlCountryId.SelectedValue);
            int NoofCylinder = string.IsNullOrEmpty(tbxNoofCylinder.Text) ? 0 : Convert.ToInt32(tbxNoofCylinder.Text);
            decimal CylinderCapacity = string.IsNullOrEmpty(tbxCylCapacity.Text) ? 0 : Convert.ToDecimal(tbxCylCapacity.Text);
            int SeatingCapacity = string.IsNullOrEmpty(tbxSeats.Text) ? 0 : Convert.ToInt32(tbxSeats.Text);
            int NoofTyres = string.IsNullOrEmpty(tbxNoofTyre.Text) ? 0 : Convert.ToInt32(tbxNoofTyre.Text);
            int NoofAxles = string.IsNullOrEmpty(tbxNoofAxles.Text) ? 0 : Convert.ToInt32(tbxNoofAxles.Text);
            decimal CostPrice = string.IsNullOrEmpty(tbxCost.Text) ? 0 : Convert.ToDecimal(tbxCost.Text);
            decimal SellingPrice = string.IsNullOrEmpty(tbxSellingPrice.Text) ? 0 : Convert.ToDecimal(tbxSellingPrice.Text);
            decimal HorsePower = string.IsNullOrEmpty(tbxHorsePower.Text) ? 0 : Convert.ToDecimal(tbxHorsePower.Text);
            decimal rpm = string.IsNullOrEmpty(tbxRpm.Text) ? 0 : Convert.ToDecimal(tbxRpm.Text);
         
            obj.Update(Convert.ToInt32(ViewState["ID"]), Convert.ToInt32(hdnOwnerId1.Value), tbxChassisNo.Text, tbxRegistrationNo.Text, tbxEngineNo.Text, VehicleType, EngineModel, ManuFacturingYear, Manufacturer, tbxVIN.Text, EngineCapacity,
                   Brand, Model, BodyType, Origin, CountryId, FuleType, NoofCylinder, CylinderCapacity, tbxWheelBase.Text, SeatingCapacity, tbxLadenWeight.Text, tbxUnladenWeight.Text, tbxTyreSize.Text, NoofTyres,
                   NoofAxles, TypeofUse, CostPrice, CostCurrency, SellingPrice, SellingCurrency, Company, BusinessCenter, tbxAxleWeights.Text, HorsePower, rpm,tbxImportDate.Text, tbxApprovedBy.Text, FileUpload(), tbxApproveDate.Text);
            MessageController.Show(MessageCode.UpdateSucceeded, MessageType.Information, Page);
            BindData();
            ClearAll();
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
        tbxAxleWeights.Text = "";
        tbxChassisNo.Text = "";
        tbxCost.Text = "";
        tbxCylCapacity.Text = "";
        tbxEngineCapacity.Text = "";
        tbxEngineNo.Text = "";
        tbxHorsePower.Text = "";
        tbxLadenWeight.Text = "";
        tbxManufacturingYear.Text = "";
        //tbxMillage.Text = "";
        tbxNoofAxles.Text = "";
        tbxNoofCylinder.Text = "";
        tbxNoofTyre.Text = "";
        tbxRegistrationNo.Text = "";
        tbxRpm.Text = "";
        tbxSeats.Text = "";
        tbxSellingPrice.Text = "";
        tbxTyreSize.Text = "";
        tbxUnladenWeight.Text = "";
        tbxVIN.Text = "";
        tbxWheelBase.Text = "";
        tbxImportDate.Text = "";
        ViewState["DrawingPath"] = "";
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void BindData()
    {
       // DataTable dt = new Common().GetAll("RVS_VEHICLE");
        DataTable dt = obj.VehicleInfo();
        rpt.DataSource = dt;
        rpt.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);
        DataTable dt = obj.GetById(ID);
        if (dt.Rows.Count > 0)
        {
            tbxAxleWeights.Text = dt.Rows[0]["AXLEWEIGHT"].ToString();
            tbxChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
            tbxCost.Text = dt.Rows[0]["COSTPRICE"].ToString();
            tbxCylCapacity.Text = dt.Rows[0]["CYLINDERCAPACITY"].ToString();
            tbxEngineCapacity.Text = dt.Rows[0]["ENGINECAPACITY"].ToString();
            tbxEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
            tbxHorsePower.Text = dt.Rows[0]["HORSEPOWER"].ToString();
            tbxLadenWeight.Text = dt.Rows[0]["LADENWEIGHT"].ToString();
            tbxManufacturingYear.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
           // tbxMillage.Text = dt.Rows[0]["MILLAGE"].ToString();
            tbxNoofAxles.Text = dt.Rows[0]["NOOFAXLE"].ToString();
            tbxNoofCylinder.Text = dt.Rows[0]["NOOFCYLINDER"].ToString();
            tbxNoofTyre.Text = dt.Rows[0]["NOOFTYRES"].ToString();
            tbxRegistrationNo.Text = dt.Rows[0]["REGNO"].ToString();
            tbxRpm.Text = dt.Rows[0]["RPM"].ToString();
            tbxSeats.Text = dt.Rows[0]["SEATINGCAPACITY"].ToString();
            tbxSellingPrice.Text = dt.Rows[0]["SELLINGPRICE"].ToString();
            tbxTyreSize.Text = dt.Rows[0]["TYRESIZE"].ToString();
            tbxUnladenWeight.Text = dt.Rows[0]["UNLADENWEIGHT"].ToString();
            tbxVIN.Text = dt.Rows[0]["VIN"].ToString();
            tbxWheelBase.Text = dt.Rows[0]["WHEELBASE"].ToString();
            tbxImportDate.Text = Convert.ToDateTime(dt.Rows[0]["IMPORTDATE"]).ToString("dd/MM/yyyy");
            tbxApprovedBy.Text = dt.Rows[0]["DRAPPROVER"].ToString();
            tbxApproveDate.Text = Convert.ToDateTime(dt.Rows[0]["DRDATE"]).ToString("dd/MM/yyyy");
            tbxWheelBase.Text = dt.Rows[0]["WHEELBASE"].ToString();
            ddlBodyType.SelectedValue = dt.Rows[0]["BODYTYPEID"].ToString();
            ddlBrand.SelectedValue = dt.Rows[0]["BRANDID"].ToString();
            ddlBusinessCenter.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
            ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
            ddlCostCurrency.SelectedValue = dt.Rows[0]["COSTCURRENCYID"].ToString();
            ddlSellingCurrency.SelectedValue = dt.Rows[0]["SELLINGCURRENCYID"].ToString();
            ddlEngineModel.SelectedValue = dt.Rows[0]["ENGINEMODELID"].ToString();
            ddlFuleType.SelectedValue = dt.Rows[0]["FUELYTYPEID"].ToString();
            ddlManufacturer.SelectedValue = dt.Rows[0]["MANUFACTURERID"].ToString();
            ddlModel.SelectedValue = dt.Rows[0]["MODELID"].ToString();
            ddlOrigin.SelectedValue = dt.Rows[0]["COUNTRYOFORIGINID"].ToString();
            ddlSellingCurrency.SelectedValue = dt.Rows[0]["SELLINGCURRENCYID"].ToString();
            ddlTypeofUse.SelectedValue = dt.Rows[0]["TYPEOFUSEID"].ToString();
            ddlVehicleType.SelectedValue = dt.Rows[0]["VEHICLETYPEID"].ToString();
            ViewState["DrawingPath"] = dt.Rows[0]["DRAWINGFILE"].ToString();
            ViewState["ID"] = ID;
        }
        btnSave.Visible = false;
        btnEdit.Visible = true;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            new Common().Delete("RVS_VEHICLE", ID);
            MessageController.Show(MessageCode.DeleteSucceeded, MessageType.Information, Page);
            BindData();
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
                cmd.CommandText = @"SELECT ID||'-'||CUSTOMERNAME CUSTOMERNAME FROM RVS_CUSTOMER WHERE (CUSTOMERNAME LIKE '" + prefixText + "%' OR ID LIKE '" + prefixText + "%' OR MOBILE LIKE '" + prefixText + "%')  AND ROWNUM <= 20";
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
}