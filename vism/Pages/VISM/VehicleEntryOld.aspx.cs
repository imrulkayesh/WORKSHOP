using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_VehicleEntry : BasePage
{
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDdl();
           
        }
    }

    private void LoadDdl()
    {
        ListItem li=new ListItem("Select","0");

        ddlVehRegHead.DataSource = dm.ExecuteQuery("select id,REGHEADNAME text  from RVS_REGISTRATIONHEAD order by REGHEADNAME");
        ddlVehRegHead.DataBind();
        ddlVehRegHead.Items.Insert(0, li);

        ddlVehRegSeries.DataSource = dm.ExecuteQuery("select id,SERIESNAME text  from RVS_REGISTRATIONSERIES order by SERIESNAME");
        ddlVehRegSeries.DataBind();
        ddlVehRegSeries.Items.Insert(0, li);

        ddlCustomer.DataSource= dm.ExecuteQuery("select id,Customername||' - '||mobile||' - '||Address text from RVS_CUSTOMER");
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0,li);

        ddlModel.DataSource = dm.ExecuteQuery("select id,modelname text from RVS_model");
        ddlModel.DataBind();
        ddlModel.Items.Insert(0, li);

        ddlManufacturer.DataSource = dm.ExecuteQuery("select id,manufacturername text from RVS_manufacturer");
        ddlManufacturer.DataBind();
        ddlManufacturer.Items.Insert(0, li);

        ddlVehicleType.DataSource = dm.ExecuteQuery("select id,vehicletypename text from RVS_vehicletype");
        ddlVehicleType.DataBind();
        ddlVehicleType.Items.Insert(0, li);

        ddlFuelType.DataSource = dm.ExecuteQuery("select id,fueltypename text from RVS_fueltype");
        ddlFuelType.DataBind();
        ddlFuelType.Items.Insert(0, li);

        ddlLocation.DataSource = dm.ExecuteQuery("select id,centername text from bs_businesscenter");
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, li);

        ddlCompany.DataSource = dm.ExecuteQuery("select id,companyname text from bs_company");
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, li);

        ddlOrigin.DataSource = dm.ExecuteQuery("select id,Countryname text from bs_country");
        ddlOrigin.DataBind();
        ddlOrigin.Items.Insert(0, li);

        ddlCountry.DataSource = dm.ExecuteQuery("select id,Countryname text from bs_country");
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, li);

    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            string cusid = ddlCustomer.SelectedValue;
            int a = 0;
            if (chkNewCustomer.Checked)
            {
                qry = @"select SQ_RVS_CUSTOMER.nextval from dual";
                cusid = dm.ExecuteScalar(qry);
                qry = @"insert into rvs_customer (ID,CUSTOMERNAME,MOBILE,ADDRESS,EMAIL,NID,CREATEDBY) 
values('" + cusid + "','" + tbxCustomer.Text + "','" + tbxMobile.Text + "','" + tbxAddress.Text + @"',
'" + tbxEmail.Text + "','" + tbxNid.Text + "','" + SessionManager.SessionName.UserId + "')";
                a = dm.ExecuteNonQuery(qry);
            }
            if (a > 0 || !chkNewCustomer.Checked)
            {
                //string vh = ddlVehRegHead.SelectedItem.Text + "-" + ddlVehRegSeries.SelectedItem.Text + "-" +
                //          tbxVehReg2Digit.Text + "-" + tbxVehReg4Digit.Text;
                string vh = tbxVehReg.Text;
                qry = @"insert into rvs_vehicle (ID,REGNO,CHASSISNO,ENGINENO,MODELID,MANUFACTURERID,MANUFACTURINGYEAR,
VEHICLETYPEID,FUELYTYPEID,DRIVERNAME,DRIVERMOBILE,BUSINESSCENTERID,COMPANYID,COUNTRYOFORIGINID,COUNTRYID,CUSTOMERID,CREATEDBY,CNGCYLINDERREDDATE) VALUES
(SQ_RVS_VEHICLE.NEXTVAL,'" + vh + "','" + tbxChassis.Text + "','" + tbxEngine.Text + "','" + ddlModel.SelectedValue + "','" + ddlManufacturer.SelectedValue + @"',
TO_DATE('" + tbxManufacYear.Text + "','DD/MM/YYYY'),'" + ddlVehicleType.SelectedValue + "','" + ddlFuelType.SelectedValue + "','" + tbxDriverName.Text + @"',
'" + tbxDriverMobile.Text + "','" + ddlLocation.SelectedValue + "','" + ddlCompany.SelectedValue + "','" + ddlOrigin.SelectedValue + "','" + ddlCountry.SelectedValue + "','" + cusid + "','"+SessionManager.SessionName.UserId+ "',TO_DATE('" + tbxCNGReTestDate.Text + "','DD/MM/YYYY'))";
                dm.ExecuteNonQuery(qry);
            }
            MessageBox(SaveSuccess);
        }
        catch (Exception ex)
        {
            MessageBox(SaveFail+ex.Message);
        }
        

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void chkNewCustomer_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkNewCustomer.Checked)
        {
            ddlCustomer.Visible = false;
            tbxCustomer.Visible = true;
            tbxMobile.Enabled = true;
            pnlCusInfo.Enabled = true;
            tbxCustomer.Text = "";
            tbxMobile.Text = "";
            tbxAddress.Text = "";
            tbxEmail.Text = "";
            tbxNid.Text = "";
            RequiredFieldValidatorDD.Enabled = false;
            RequiredFieldValidatorTX.Enabled = true;
            //ddlVehicle.DataSource = dm.ExecuteQuery(@"SELECT ID,REGNO TEXT FROM RVS_VEHICLE");
            //ddlVehicle.DataBind();
            //ddlVehicle.Items.Insert(0, new ListItem("Select", "0"));

        }
        else
        {
            ddlCustomer.Visible = true;
            tbxCustomer.Visible = false;
            tbxMobile.Enabled = false;
            pnlCusInfo.Enabled = false;
            tbxCustomer.Text = "";
            tbxMobile.Text = "";
            tbxAddress.Text = "";
            tbxEmail.Text = "";
            tbxNid.Text = "";
            ddlCustomer.SelectedValue = "0";
            RequiredFieldValidatorDD.Enabled = true;
            RequiredFieldValidatorTX.Enabled = false;
        }
        
    }

    //protected void chkNewVehicle_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkNewVehicle.Checked)
    //    {
    //        pnlVehicle.Visible = false;
    //        pnlVehicleNew.Visible = true;
    //    }
    //    else
    //    {
    //        pnlVehicle.Visible = true;
    //        pnlVehicleNew.Visible = false;
    //    }
    //}

    protected void ddlCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCustomer.SelectedIndex>0)
        {
            DataTable dt = dm.ExecuteQuery(@"Select * from rvs_customer where id='"+ddlCustomer.SelectedValue+"'");
            tbxMobile.Text = dt.Rows[0]["MOBILE"].ToString();
            tbxAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            tbxEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            tbxNid.Text = dt.Rows[0]["NID"].ToString();

            //ddlVehicle.DataSource = dm.ExecuteQuery(@"SELECT ID,REGNO TEXT FROM RVS_VEHICLE WHERE CUSTOMERID='" + ddlCustomer.SelectedValue + "'");
            //ddlVehicle.DataBind();
            //ddlVehicle.Items.Insert(0, new ListItem("Select", "0"));

        }

    }

    //protected void ddlVehicle_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlVehicle.SelectedIndex>0)
    //    {
    //        DataTable dt = dm.ExecuteQuery(@"select * from rvs_vehicle where id='" + ddlVehicle.SelectedValue + "'");
    //        tbxChassis.Text = dt.Rows[0]["CHASSISNO"].ToString();
    //        tbxEngine.Text = dt.Rows[0]["ENGINENO"].ToString();
    //        ddlModel.SelectedValue = dt.Rows[0]["MODELID"].ToString();
    //        ddlManufacturer.SelectedValue = dt.Rows[0]["MANUFACTURERID"].ToString();
    //        tbxManufacYear.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();
    //        ddlVehicleType.SelectedValue = dt.Rows[0]["VEHICLETYPEID"].ToString();
    //        ddlFuelType.SelectedValue = dt.Rows[0]["FUELYTYPEID"].ToString();
    //        tbxDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
    //        tbxDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
    //        ddlLocation.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
    //        ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
    //    }
        
    //}
    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        try
        {
            string cusId = ddlCustomer.SelectedValue;
            int a = 0;
            if (chkNewCustomer.Checked)
            {
                qry = @"select SQ_RVS_CUSTOMER.nextval from dual";
                cusId = dm.ExecuteScalar(qry);
                qry = @"insert into rvs_customer (ID,CUSTOMERNAME,MOBILE,ADDRESS,EMAIL,NID,CREATEDBY) 
values('" + cusId + "','" + tbxCustomer.Text + "','" + tbxMobile.Text + "','" + tbxAddress.Text + @"',
'" + tbxEmail.Text + "','" + tbxNid.Text + "','" + SessionManager.SessionName.UserId + "')";
                a = dm.ExecuteNonQuery(qry);

            }
            if (a > 0 || !chkNewCustomer.Checked)
            {
                qry = @"update RVS_VEHICLE set REGNO='" + tbxVehReg.Text + "',CHASSISNO='" + tbxChassis.Text + @"',
                    ENGINENO='" + tbxEngine.Text + @"',MODELID='" + ddlModel.SelectedValue + @"',
                    MANUFACTURERID='" + ddlManufacturer.SelectedValue + "',MANUFACTURINGYEAR=TO_DATE('" + tbxManufacYear.Text + @"','DD/MM/YYYY'),
                    VEHICLETYPEID ='" + ddlVehicleType.SelectedValue + "',FUELYTYPEID='" + ddlFuelType.SelectedValue + @"',
                    DRIVERNAME='" + tbxDriverName.Text + @"',DRIVERMOBILE='" + tbxDriverMobile.Text + @"',
                    BUSINESSCENTERID='" + ddlLocation.SelectedValue + "',COMPANYID='" + ddlCompany.SelectedValue + @"',
                    COUNTRYOFORIGINID='" + ddlOrigin.SelectedValue + "',COUNTRYID='" + ddlCountry.SelectedValue + @"',
                    CUSTOMERID='" + cusId + "',CNGCYLINDERREDDATE='" + tbxCNGReTestDate + "' where ID='" + ViewState["ID"] + "'";
                dm.ExecuteNonQuery(qry);
            }
            crt = (string)ViewState["CRT"];
            BindRpt();
            MessageBox(UpdateSuccess);
            ChangeToSaveMode(Controls);
            
        }
        catch (Exception ex)
        {
            MessageBox(UpdateFail+ex.Message);
        }
        
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        // GetCriteria();
          BindRpt();



    }
    private void BindRpt()
    {
        
        rptMain.DataSource = RptData();
        rptMain.DataBind();
        //if (rptMain.Items.Count > 0)
        //{
        //    pnlReportButton.Visible = true;
        //}
    }
    private bool _firstTime = true;
    private DataTable dtRpt = new DataTable();
    public DataTable RptData()
    {
        if (_firstTime)
        {
            //qry = @"Select vh.id,vh.REGNO ""Reg. Number"",vh.CHASSISNO ""Chassis Number"" ,vh.ENGINENO ""Engine Number"",mo.MODELNAME ""Model"",vh.MANUFACTURINGYEAR ""Reg. Date"",
            //mn.MANUFACTURERNAME ""Manufacturer"",vt.VEHICLETYPENAME ""Vehicle Type"",ft.FUELTYPENAME ""Fuel Type"",vh.DRIVERNAME ""Driver Name"",vh.DRIVERMOBILE ""Driver Mobile No."",
            //bc.CENTERNAME ""Location"",cu.CUSTOMERNAME ""Customer"",co.COMPANYNAME ""Company"",cn.COUNTRYNAME ""Country"" from rvs_Vehicle vh
            //left join rvs_model mo on vh.MODELID=mo.ID
            //left join RVS_MANUFACTURER mn on vh.MANUFACTURERID=mn.ID
            //left join RVS_VEHICLETYPE vt on vh.VEHICLETYPEID=vt.ID
            //left join RVS_FUELTYPE ft on vh.FUELYTYPEID=ft.ID
            //left join bs_businesscenter bc on vh.BUSINESSCENTERID=bc.ID
            //left join RVS_CUSTOMER cu on vh.CUSTOMERID=cu.ID
            //left join bs_company co on vh.COMPANYID=co.id
            //left join bs_country cn on vh.COUNTRYID=cn.ID" + crt;

            qry = @"Select vh.id,vh.REGNO ""Reg. Number"",vh.CHASSISNO ""Chassis Number"" ,vh.ENGINENO ""Engine Number"",mo.MODELNAME ""Model"",vh.MANUFACTURINGYEAR ""Reg. Date"",
            mn.MANUFACTURERNAME ""Manufacturer"",vt.VEHICLETYPENAME ""Vehicle Type"",ft.FUELTYPENAME ""Fuel Type"",vh.DRIVERNAME ""Driver Name"",vh.DRIVERMOBILE ""Driver Mobile No."",
            bc.CENTERNAME ""Location"",cu.CUSTOMERNAME ""Customer"",co.COMPANYNAME ""Company"",cn.COUNTRYNAME ""Country"" from rvs_Vehicle vh
            left join rvs_model mo on vh.MODELID=mo.ID
            left join RVS_MANUFACTURER mn on vh.MANUFACTURERID=mn.ID
            left join RVS_VEHICLETYPE vt on vh.VEHICLETYPEID=vt.ID
            left join RVS_FUELTYPE ft on vh.FUELYTYPEID=ft.ID
            left join bs_businesscenter bc on vh.BUSINESSCENTERID=bc.ID
            left join RVS_CUSTOMER cu on vh.CUSTOMERID=cu.ID
            left join bs_company co on vh.COMPANYID=co.id
            left join bs_country cn on vh.COUNTRYID=cn.ID";


            dtRpt = dm.ExecuteQuery(qry);
            _firstTime = false;

        }

        return dtRpt;
    }
    private string crt = " WHERE isactive=1 ";
    private void GetCriteria()
    {
        if (tbxChassis.Text != "") crt += " AND VH.CHASSISNO='" + tbxChassis.Text + "'";
        if (tbxVehReg.Text != "") crt += " AND VH.REGNO like'%" + tbxVehReg.Text + "%'";
        if (tbxEngine.Text != "") crt += " AND VH.ENGINENO='" + tbxEngine.Text + "'";
        if (ddlVehicleType.SelectedValue != "0") crt += " AND VH.VEHICLETYPEID='" + ddlVehicleType.SelectedValue + "'";
        if (ddlModel.SelectedValue != "0") crt += " AND VH.MODELID='" + ddlModel.SelectedValue + "'";
        if (ddlManufacturer.SelectedValue != "0") crt += " AND VH.MANUFACTURERID='" + ddlManufacturer.SelectedValue + "'";
        if (tbxManufacYear.Text != "") crt += " AND VH.MANUFACTURINGYEAR='" + tbxManufacYear.Text + "'";
        if (tbxDriverName.Text != "") crt += " AND lower(VH.DRIVERNAME) like '%" + tbxDriverName.Text.ToLower() + "%'";
        if (tbxDriverMobile.Text != "") crt += " AND VH.DRIVERMOBILE like '%" + tbxDriverMobile.Text + "%'";
        if (ddlFuelType.SelectedValue != "0") crt += " AND VH.FUELYTYPEID='" + ddlFuelType.SelectedValue + "'";
        if (ddlOrigin.SelectedValue != "0") crt += " AND VH.COUNTRYOFORIGINID='" + ddlOrigin.SelectedValue + "'";
        if (ddlCompany.SelectedValue != "0") crt += " AND VH.COMPANYID='" + ddlCompany.SelectedValue + "'";
        if (ddlLocation.SelectedValue != "0") crt += " AND VH.BUSINESSCENTERID='" + ddlLocation.SelectedValue + "'";
        if (ddlCountry.SelectedValue != "0") crt += " AND VH.COUNTRYID='" + ddlCountry.SelectedValue + "'";
        if (ddlCustomer.SelectedValue != "0") crt += " AND VH.CUSTOMERID='" + ddlCustomer.SelectedValue + "'";
        ViewState["CRT"] = crt;
    }
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

            //Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            //if (bodyRpt == null) return;
            //bodyRpt.DataSource = DtToList(rn, DtCommon, false);
            //bodyRpt.DataBind();
            //rn++;

            Repeater bodyRpt = e.Item.FindControl("bdyRpt") as Repeater;
            bodyRpt.DataSource = DtToList(_rw, RptData(), hideIdColumn);
            bodyRpt.DataBind();
            _rw++;
            if (SessionManager.SessionName.UserId == "112408" || SessionManager.SessionName.UserId == "135323")
            {
                var lnkd = (ImageButton)e.Item.FindControl("btnDelete");
                lnkd.Visible = true;
            }
        }

    }

    protected void btnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        try
        {
            chkNewCustomer.Checked = false;
            chkNewCustomer_OnCheckedChanged(null,null);
            qry = @"SELECT * FROM rvs_Vehicle where ID='" + e.CommandArgument + "'";
            DataTable dt = dm.ExecuteQuery(qry);

            tbxVehReg.Text = dt.Rows[0]["REGNO"].ToString();
            tbxChassis.Text = dt.Rows[0]["CHASSISNO"].ToString();
            tbxEngine.Text = dt.Rows[0]["ENGINENO"].ToString();
            ddlModel.SelectedValue = dt.Rows[0]["MODELID"].ToString();
            ddlManufacturer.SelectedValue = dt.Rows[0]["MANUFACTURERID"].ToString();


           // tbxManufacYear.Text = dt.Rows[0]["MANUFACTURINGYEAR"].ToString();

            tbxManufacYear.Text = Convert.ToDateTime(dt.Rows[0]["MANUFACTURINGYEAR"].ToString()).ToString("dd-MM-yyyy");
            ddlVehicleType.SelectedValue = dt.Rows[0]["VEHICLETYPEID"].ToString();
            ddlFuelType.SelectedValue = dt.Rows[0]["FUELYTYPEID"].ToString();
            tbxDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
            tbxDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
            ddlLocation.SelectedValue = dt.Rows[0]["BUSINESSCENTERID"].ToString();
            ddlCompany.SelectedValue = dt.Rows[0]["COMPANYID"].ToString();
            ddlOrigin.SelectedValue = dt.Rows[0]["COUNTRYOFORIGINID"].ToString();
            ddlCountry.SelectedValue = dt.Rows[0]["COUNTRYID"].ToString();
            ddlCustomer.SelectedValue = dt.Rows[0]["CUSTOMERID"].ToString();
            tbxCNGReTestDate.Text= Convert.ToDateTime(dt.Rows[0]["CNGCYLINDERREDDATE"].ToString()).ToString("dd-MM-yyyy");
            ddlCustomer_OnSelectedIndexChanged(null,null);
            pnltbxvehreg.Visible = true;
            pnlVehicleNew.Visible = false;
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
            qry = @"update rvs_vehicle set isactive=0 where id='" + e.CommandArgument + "'";
            dm.ExecuteNonQuery(qry);
            crt = (string)ViewState["CRT"];
            BindRpt();
            MessageBox(DeleteSuccess);
            
        }
        catch (Exception ex)
        {
            MessageBox(DeleteFail+ex.Message);
        }
        
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


    protected void lnkDownloadExcel_OnClick(object sender, EventArgs e)
    {
        try
        {
            ExportToExcel(rptMain);
        }
        catch (Exception ex)
        {
            //MessageBox("Error: " + ex.Message);
        }
    }
}