using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PrintBRTAFrom : BasePage
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
            ViewState["RefUrl"] = string.Empty;
            try
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
            catch
            {
                ViewState["RefUrl"] = "~/Pages/Admin/Dashboard.aspx";
            }
            if (SessionManager.SessionName.VehicleId > 0)
            {
                bindData();
            }
            else
            {
                Response.Redirect("~/Pages/Admin/Dashboard.aspx");
            }
        }
    }
    private void bindData()
    {
        try
        {
            int id = SessionManager.SessionName.VehicleId;
            DataTable dt = obj.getVehicleDetails(id);
            if (dt.Rows.Count > 0)
            {
                DataTable dtOwner = obj.GetOwnerDetails(id);
                if (dtOwner.Rows.Count > 0)
                {
                    lblPage1OwnerName.Text = dtOwner.Rows[0]["CUSTOMERNAME"].ToString();
                    lblPage1OwnerDob.Text = Convert.ToDateTime(dtOwner.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                    lblPage1Father.Text = dtOwner.Rows[0]["FATHERNAME"].ToString();
                    lblPage1Gender.Text = dtOwner.Rows[0]["GENDERNAME"].ToString();
                    lblPage1Guardian.Text = dtOwner.Rows[0]["GUARDIANNAME"].ToString();
                    lblPage1Address.Text = dtOwner.Rows[0]["ADDRESS"].ToString();
                    lblPage1Phone.Text = dtOwner.Rows[0]["MOBILE"].ToString();
                    int ownertype = Convert.ToInt32(dtOwner.Rows[0]["JOINTOWNER"]);
                    if (ownertype == 1)
                    {
                        lblPage1JointOwner.Text = "Yes";
                        lblPage1OwnerType.Text = "Joint";
                    }
                    else
                    {
                        lblPage1JointOwner.Text = "No";
                        lblPage1OwnerType.Text = "Individual";
                    }
                    lblPage1Nationality.Text = dtOwner.Rows[0]["COUNTRYNAME"].ToString();

                    Page2JointOwner1Name.Text = dtOwner.Rows[0]["CUSTOMERNAME"].ToString();
                    Page2JointOwner1Father.Text = dtOwner.Rows[0]["FATHERNAME"].ToString();
                    if (dtOwner.Rows.Count > 1)
                    {
                        Page2JointOwner2Name.Text = dtOwner.Rows[1]["CUSTOMERNAME"].ToString();
                        Page2JointOwner2Father.Text = dtOwner.Rows[1]["FATHERNAME"].ToString();
                    }
                    page3OwnerName.Text = dtOwner.Rows[0]["CUSTOMERNAME"].ToString();
                    page3Father.Text = dtOwner.Rows[0]["FATHERNAME"].ToString();
                    page3Address.Text = dtOwner.Rows[0]["ADDRESS"].ToString();
                    page3Gender.Text = dtOwner.Rows[0]["GENDERNAME"].ToString();
                    page3Phone.Text = dtOwner.Rows[0]["MOBILE"].ToString();
                    page3Nationality.Text = dtOwner.Rows[0]["COUNTRYNAME"].ToString();
                    page3dob.Text = Convert.ToDateTime(dtOwner.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                    page3Guardian.Text = dtOwner.Rows[0]["GUARDIANNAME"].ToString();

                    page4CustomerDetails.Text = dtOwner.Rows[0]["CUSTOMERNAME"].ToString() + " " + dtOwner.Rows[0]["ADDRESS"].ToString();
                    page5CustomerDetails.Text = dtOwner.Rows[0]["CUSTOMERNAME"].ToString() + " " + dtOwner.Rows[0]["ADDRESS"].ToString();
                }
                lblPage1Vehicle.Text = dt.Rows[0]["VEHICLETYPENAME"].ToString();
                lblPage1RegNo.Text = dt.Rows[0]["REGNO"].ToString();
                lblPage1VehicleType.Text = dt.Rows[0]["VEHICLETYPENAME"].ToString();
                lblPage1ManuFacturer.Text = dt.Rows[0]["MANUFACTURERNAME"].ToString();
                lblPage1TypeOfBody.Text = dt.Rows[0]["BODYTYPENAME"].ToString();
                lblPage1CountryofOrigin.Text = dt.Rows[0]["COUNTRYNAME"].ToString();
                lblPage1YearofManuf.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
                lblPage1NoofCyl.Text = dt.Rows[0]["NOOFCYLINDER"].ToString();
                lblPage1ChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                lblPage1EngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                lblPage1FuelType.Text = dt.Rows[0]["FUELTYPENAME"].ToString();
                lblPage1HorsePower.Text = dt.Rows[0]["HORSEPOWER"].ToString();
                lblPage1Rpm.Text = dt.Rows[0]["RPM"].ToString();
                lblPage1EngineCapacity.Text = dt.Rows[0]["ENGINECAPACITY"].ToString();
                lblPage1Seats.Text = dt.Rows[0]["SEATINGCAPACITY"].ToString();
                lblPage1WheelBase.Text = dt.Rows[0]["WHEELBASE"].ToString();
                lblPage1UnladenWeight.Text = dt.Rows[0]["UNLADENWEIGHT"].ToString();
                lblPage1LadenWeight.Text = dt.Rows[0]["LADENWEIGHT"].ToString();
                lblPage1NoofTyres.Text = dt.Rows[0]["NOOFTYRES"].ToString();
                lblPage1TyreSize.Text = dt.Rows[0]["TYRESIZE"].ToString();
                lblPage1NoofAxle.Text = dt.Rows[0]["NOOFAXLE"].ToString();
                lblPage1DrawingApprover.Text = dt.Rows[0]["DRAPPROVER"].ToString();
                lblPage1DrawingApvDate.Text = Convert.ToDateTime(dt.Rows[0]["DRDATE"]).ToString("dd/MM/yyyy");


                Page2ChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                Page2EngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();

                page3ChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                page3EngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                Page3YearofManuf.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
                page3RegNo.Text = dt.Rows[0]["REGNO"].ToString();

                page4VehiclType.Text = dt.Rows[0]["VEHICLETYPENAME"].ToString();
                page4TypeofBody.Text = dt.Rows[0]["BODYTYPENAME"].ToString();
                page4Manufacturer.Text = dt.Rows[0]["MANUFACTURERNAME"].ToString();
                page4CountryofOrigin.Text = dt.Rows[0]["COUNTRYNAME"].ToString();
                Page4Model.Text = dt.Rows[0]["MODELNAME"].ToString();
                page4ManufacturingYear.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
                page4ChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                Page4EngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                Page4NoofCyl.Text = dt.Rows[0]["NOOFCYLINDER"].ToString();
                page4FuelType.Text = dt.Rows[0]["FUELTYPENAME"].ToString();
                Page4MaxPower.Text = dt.Rows[0]["HORSEPOWER"].ToString();
                Page4WheelBase.Text = dt.Rows[0]["WHEELBASE"].ToString();
                Page4TyreSize.Text = dt.Rows[0]["TYRESIZE"].ToString();
                Page4LadenWeight.Text = dt.Rows[0]["LADENWEIGHT"].ToString();
                Page4unladenWeight.Text = dt.Rows[0]["UNLADENWEIGHT"].ToString();
                Page4SeatingCapacity.Text = dt.Rows[0]["SEATINGCAPACITY"].ToString();
                page4SellingPrice.Text = dt.Rows[0]["CURRENCYCODE"].ToString() + ". " + dt.Rows[0]["SELLINGPRICE"].ToString() + "/-";

                Page5VehicleDetails.Text = "Model " + dt.Rows[0]["MODELNAME"].ToString() + " " + dt.Rows[0]["BODYTYPENAME"].ToString();
                page5ChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                page5EngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                Page5SellingPrice.Text = dt.Rows[0]["CURRENCYCODE"].ToString() + ". " + dt.Rows[0]["SELLINGPRICE"].ToString() + "/-(" + dt.Rows[0]["CURRENCYNAME"].ToString() + " in words : " + dalCommon.NumberToText(Convert.ToInt32(dt.Rows[0]["SELLINGPRICE"])) + ")";
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteLog(ex);
        }
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        SessionManager.SessionName.DispatchId = 0;
        Response.Redirect((string)ViewState["RefUrl"]);
    }
}