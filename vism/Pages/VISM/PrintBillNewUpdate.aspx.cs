using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VISM_PrintBillNewUpdate : BasePage
{
    dalJobCard obj = new dalJobCard();
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageController.Clear(Page);

        if (!IsPostBack)
        {
            if (SessionManager.SessionName == null)
            {
                Response.Redirect("~/Pages/Admin/SiteMap.aspx");
                return;
            }
            else
            {
                JOBID = (int)0;
                if (Request.QueryString["JobID"] != null)
                {
                    JOBID = Convert.ToInt32(Request.QueryString["JobID"]);
                }
                else
                {
                    MessageController.Show("No Data Found", MessageType.Error, Page);
                }
            }
            DataTable dt = obj.GetJobCardPrint(JOBID);
            if (dt.Rows.Count > 0)
            {
                lblBranchAddress.Text = dt.Rows[0]["WADDRESS"].ToString();
                lblworkshop.Text = dt.Rows[0]["WORKSHOP"].ToString();
                lblJobNo.Text = dt.Rows[0]["JOBID"].ToString();
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["CREATEDATE"]).ToString("dd/MM/yyyy") + " - Time :" + Convert.ToDateTime(dt.Rows[0]["CREATEDATE"]).ToString("hh:mm tt");
                lblCustName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                //  lblOfficeAddress.Text = dt.Rows[0]["BADDRESS"].ToString();
                lblStaffId.Text = dt.Rows[0]["DRIVERID"].ToString();

                lblDriverName.Text = dt.Rows[0]["DRIVERNAME"].ToString();
                lblDriverMobile.Text = dt.Rows[0]["DRIVERMOBILE"].ToString();
                lblLpg.Text = dt.Rows[0]["SERVICETYPE"].ToString();
                lblMobile.Text = dt.Rows[0]["MOBILE"].ToString();
                lblRegNo.Text = dt.Rows[0]["REGNO"].ToString();
                lblChassisNo.Text = dt.Rows[0]["CHASSISNO"].ToString();
                lblMeterReading.Text = dt.Rows[0]["MILLAGE"].ToString();
                lblEngineNo.Text = dt.Rows[0]["ENGINENO"].ToString();
                lblEngineModuleNo.Text = dt.Rows[0]["MODELNAME"].ToString();

                lblPowerCost.Text = dt.Rows[0]["POWERCOST"].ToString();
                lblCMCost.Text = dt.Rows[0]["CM"].ToString();

                lblSubTotal.Text = dt.Rows[0]["NETAMOUNT"].ToString();
                lblDiscount.Text = dt.Rows[0]["DISCOUNT"].ToString();
                //lblTotal.Text = dt.Rows[0]["GRAND_TOTAL"].ToString();

                decimal discount = Convert.ToDecimal(dt.Rows[0]["DISCOUNTAMOUNT"]);
                decimal grandTotal = Convert.ToDecimal(dt.Rows[0]["GRAND_TOTAL"]);
                decimal result = grandTotal - discount;
                lblTotal.Text = result.ToString("0.00");

                lblcompany.Text = dt.Rows[0]["COMPANYNAME"].ToString();
                lblgroup.Text = dt.Rows[0]["GROUPNAME"].ToString();
                lblRemark.Text = dt.Rows[0]["REMARKS"].ToString();

                lblDiscountPrct.Text = dt.Rows[0]["DISCOUNTPERCENT"].ToString();
                lblDiscount.Text = dt.Rows[0]["DISCOUNTAMOUNT"].ToString();

                lblPreparedBy.Text = dt.Rows[0]["CREATEDBY"].ToString() + " - " + SessionManager.SessionName.UserFullName;
                //int Total = string.IsNullOrEmpty(dt.Rows[0]["GRAND_TOTAL"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["GRAND_TOTAL"]);
                int Total = string.IsNullOrEmpty(dt.Rows[0]["GRAND_TOTAL"].ToString()) ? 0 : Convert.ToInt32(result);
                lblTakaInword.Text = dalCommon.NumberToText(Total);
                LoadCharges();
                // LoadTotal();
            }
            else
            {
                MessageController.Show("No Data Found", MessageType.Error, Page);
            }
        }
    }

    private void LoadTotal()
    {
        string qry = @"SELECT ROUND(NVL(B.TOTALBILL,0),2) TOTBILL,ROUND(NVL(B.DISCOUNTAMOUNT ,0),2) DISCOUNT,ROUND(NVL(B.NETBILL ,0),2) NETBILL FROM RVS_BILL B
WHERE B.JOBID='" + JOBID + "'";
        DataTable dt = dm.ExecuteQuery(qry);
        if (dt.Rows.Count > 0)
        {
            lblSubTotal.Text = Convert.ToDecimal(dt.Rows[0]["TOTBILL"].ToString()).ToString("F2");
            lblDiscount.Text = Convert.ToDecimal(dt.Rows[0]["DISCOUNT"].ToString()).ToString("F2");
            lblTotal.Text = Convert.ToDecimal(dt.Rows[0]["NETBILL"]).ToString("F2");
            decimal Total = string.IsNullOrEmpty(dt.Rows[0]["NETBILL"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["NETBILL"]);
            int paisa = (int)((Total - (int)Total) * 100);
            if (paisa == 0)
            {
                lblTakaInword.Text = dalCommon.NumberToText((int)Total) + " Taka Only.";

            }
            else
            {
                lblTakaInword.Text = dalCommon.NumberToText((int)Total) + " Taka " + dalCommon.NumberToText(paisa) + " Paisa Only.";
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
    private void LoadCharges()
    {
        try
        {
            decimal total = 0;
            decimal totalParts = 0;
            //string qry = @"select JOBID,ITEMNAME CHARGENAME,AMOUNT,QTY QUANTITY,'' REMARKS, UNIT UNITCODE from V_RVS_BILL_NEW where jobid='" + JOBID + "' and CHARGETYPE='Service Charge'";
            //DataTable dt = dm.ExecuteQuery(qry);
            //rptCharge.DataSource = dt;
            //rptCharge.DataBind();

            //if (dt.Rows.Count > 0)
            //{
            //    total = Convert.ToDecimal(dt.Compute("Sum(AMOUNT)", ""));

            //}

            ////string qrys = @"SELECT JI.JOBID, IM.ITEMNAME PARTSNAME, NVL(JI.TOTALAMOUNT,0) AMOUNT, JI.QUANTITY, JI.REMARKS,UT.UNITNAME UNITCODE
            ////FROM RVS_JOBCARDITEMS JI INNER JOIN RVS_JOBCARD JC ON JI.JOBID = JC.ID
            ////INNER JOIN RVS_ITEMMASTER IM ON JI.ITEMCODE = IM.ITEMCODE
            ////INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE = UT.UNITCODE
            ////WHERE JI.JOBID = '" + JOBID + "' order by ji.line";

            string qrys = @"SELECT JOBID, ITEMNAME AS PARTSNAME, ROUND(QTY * RATE + 0.004999, 2) AS AMOUNT, QTY AS QUANTITY,'' AS REMARKS, UNITNAME AS UNITCODE FROM  V_RVS_CO_AC WHERE jobid='" + JOBID + "'";


            DataTable dts = dm.ExecuteQuery(qrys);
            rptParts.DataSource = dts;
            rptParts.DataBind();
            if (dts.Rows.Count > 0)
            {
                totalParts += Convert.ToDecimal(dts.Compute("Sum(AMOUNT)", ""));
            }
            //txtSprice.Text = price.ToString("#0.00");
            lblSubTotalParts.Text = totalParts.ToString("#0.00");

            //Labour Charge data

            string qryLabor = @"SELECT 
            PID.PIID, PID.INSPECTIONITEMID,ITEM.ITEMNAME,PID.QUANTITY,PID.LABORRATE,PID.LABORQTY,PID.WORKINGHOUR,
            PID.LABORRATE * PID.LABORQTY* PID.WORKINGHOUR AS UNIT_AMOUNT,
            PID.QUANTITY * PID.LABORRATE * PID.LABORQTY* PID.WORKINGHOUR AS AMOUNT,
            PID.REMARKS
            FROM  RVS_PIDETAILS PID
            LEFT JOIN RVS_PREINSPECTION PI  ON  PI.ID = PID.PIID
            LEFT JOIN  RVS_JOBCARD J ON  PI.ID = J.PIID
            LEFT JOIN  RVS_INSPECTIONITEMS ITEM ON  ITEM.ID = PID.INSPECTIONITEMID
            WHERE J.ID='" + JOBID + "'";

            DataTable dtLabor = dm.ExecuteQuery(qryLabor);
            rptLaborCharge.DataSource = dtLabor;
            rptLaborCharge.DataBind();
            if (dtLabor.Rows.Count > 0)
            {
                total += Convert.ToDecimal(dtLabor.Compute("Sum(AMOUNT)", ""));
            }
            lblSubTotal.Text = total.ToString("#0.00");

        }
        catch (Exception ex)
        {

        }
    }
}