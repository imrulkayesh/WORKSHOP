using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RDIM : System.Web.UI.Page
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Rdim"] != null)
        {
            try
            {
                //List<string> mailto = new List<string>();
                //mailto.Add("dist14@rflgroupbd.com");
                //mailto.Add("dist402@rflgroupbd.com");
                //mailto.Add("rfl148@rflgroupbd.com");
                //mailto.Add("dist121@prangroup.net");
                //mailto.Add("dist16@rflgroupbd.com");
                //mailto.Add("dist31@rflgroupbd.com");
                //mailto.Add("dist371@rflgroupbd.com");
                //mailto.Add("mis70@prangroup.com");
                //string mail = "";
                DateTime qdate = DateTime.Now;
                try
                {
                    qdate = Convert.ToDateTime(Request.QueryString["dt"]);
                }
                catch
                {
                    qdate = DateTime.Now;
                }
                dm.ExecuteNonQuery(@"Insert into VD_TEMPRFIDRECORD(ID,CARDNUMBER,CARDLOCATIONID,INTIME)
                                VALUES(SQ_VD_TEMPRFIDRECORD.NEXTVAL,'" + Request.QueryString["Rdim"] + "','1',TO_DATE('" + qdate + "','MM/dd/yyyy hh:mi:ss am'))");
                if (Request.QueryString["Rdim"].Length > 5)
                {
                    //string ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    //if (ipaddress == "" || ipaddress == null)
                    //{
                    //    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                    //}
                    //DataTable dt = dm.ExecuteQuery("SELECT * FROM VD_RFIDREADERS WHERE IPADDRESS='" + ipaddress + "'");
                    //if (dt.Rows.Count > 0)
                    //{
                    DataTable dtCards = dm.ExecuteQuery(@"SELECT * FROM VD_RFIDCARDS WHERE CARDNO='" + Request.QueryString["Rdim"] + "'");
                    if (dtCards.Rows.Count > 0)
                    {
                        DataTable dtRecords = dm.ExecuteQuery(@"SELECT * FROM VD_RFIDRECORDS WHERE ROWNUM<=1 AND CARDID='" + dtCards.Rows[0]["ID"].ToString() + "' ORDER BY ID DESC");
                        if (dtRecords.Rows.Count > 0)
                        {

                            DateTime dtdiff = Convert.ToDateTime(dtRecords.Rows[0]["INTIME"]);
                            DateTime dtdffOut = string.IsNullOrEmpty(dtRecords.Rows[0]["OUTTIME"].ToString()) ? DateTime.Now.AddMonths(-200) : Convert.ToDateTime(dtRecords.Rows[0]["OUTTIME"]);
                            int isOut = Convert.ToInt32(dtRecords.Rows[0]["ISOUT"]);
                            double difout = (qdate - dtdffOut).Duration().TotalMinutes;
                            double Diff = (qdate - dtdiff).Duration().TotalMinutes;
                            if (Diff > 10)
                            {
                                if (isOut == 0)
                                {
                                    //mail += "Vehicle No: " + dtCards.Rows[0]["CHASSISNO"].ToString() + " <br>IN Time: " + dtRecords.Rows[0]["INTIME"].ToString() + " <br> OUT Time: " + DateTime.Now.ToString();
                                    dm.ExecuteNonQuery("Update VD_RFIDRECORDS set OUTTIME=TO_DATE('" + qdate + "','MM/dd/yyyy hh:mi:ss am'),ISOUT=1 where ID='" + dtRecords.Rows[0]["ID"].ToString() + "'");
                                    //SendEmail.SendMultipleMail(mailto, "Vehicle OUT", mail);
                                }
                                else
                                {
                                    if (difout > 10)
                                    {
                                        dm.ExecuteNonQuery(@"Insert into VD_RFIDRECORDS(ID,CARDID,CARDLOCATIONID,INTIME)
                                VALUES(SQ_VD_RFIDRECORDS.NEXTVAL,'" + dtCards.Rows[0]["ID"].ToString() + "','1',TO_DATE('" + qdate + "','MM/dd/yyyy hh:mi:ss am'))");
                                    }
                                    else
                                    {
                                        //mail += "Vehicle No: " + dtCards.Rows[0]["CHASSISNO"].ToString() + " <br>IN Time: " + dtCards.Rows[0]["INTIME"].ToString() + " <br> OUT Time: " + DateTime.Now.ToString();
                                        dm.ExecuteNonQuery("Update VD_RFIDRECORDS set OUTTIME=TO_DATE('" + qdate + "','MM/dd/yyyy hh:mi:ss am'),ISOUT=1 where ID='" + dtRecords.Rows[0]["ID"].ToString() + "'");
                                        //SendEmail.SendMultipleMail(mailto, "Vehicle OUT", mail);
                                    }

                                }
                            }
                            else
                            {
                                dm.ExecuteNonQuery("Update VD_RFIDRECORDS set INTIME=TO_DATE('" + qdate + "','MM/dd/yyyy hh:mi:ss am') where ID='" + dtRecords.Rows[0]["ID"].ToString() + "'");
                            }
                        }
                        else
                        {
                            //mail += "Vehicle No: " + dtCards.Rows[0]["CHASSISNO"].ToString() + " <br> IN Time: " + DateTime.Now.ToString();
                            dm.ExecuteNonQuery(@"Insert into VD_RFIDRECORDS(ID,CARDID,CARDLOCATIONID,INTIME)
                                VALUES(SQ_VD_RFIDRECORDS.NEXTVAL,'" + dtCards.Rows[0]["ID"].ToString() + "','1',TO_DATE('" + qdate + "','MM/dd/yyyy hh:mi:ss am'))");
                            //SendEmail.SendMultipleMail(mailto, "Vehicle IN", mail);
                        }
                        Response.Write("1");
                        //}
                        //Response.Write(ipaddress);
                    }
                    Response.Write("1");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        else
        {
            Response.Write(Request.QueryString["Rdim"] + Request.QueryString["CenterId"] + Request.QueryString["GateNo"]);
        }
    }
}