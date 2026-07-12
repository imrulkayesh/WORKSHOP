using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalSales
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalSales()
    {
    }
    public int UpdateSales(int salesId, int VehicleId, decimal price, int currencyid, decimal paid, DateTime nextPayDate, DateTime Selldate, int JointOwner)
    {
        return dm.ExecuteNonQuery(@"Update RVS_SALES Set VEHICLEID='" + VehicleId + "',PRICE='" + price + "',CURRENCYID='" + currencyid + "',PAIDAMOUNT='" + paid + "',NEXTPAYDATE=TO_DATE('" + nextPayDate + "','MM/dd/yyyy HH:MI:SSAM'),SELLDATE=TO_DATE('" + Selldate + "','MM/dd/yyyy HH:MI:SSAM'),CREATEDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UDPATEDATE=SYSDATE,JOINTOWNER='" + JointOwner + "' WHERE ID='" + salesId + "'");
    }
    public int InsertOwner(int VehicleId, int CustomerId, string BAddress, string BcontactPerson, string bMobile, string bPhone, string bFax, string bEmail, int SalesId)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_OWNERS(ID,VEHICLEID,CUSTOMERID,BADDRESS,BCONTACTPERSON,BMOBILE,BPHONE,BFAX,BEMAIL,SALESID) 
            VALUES(SQ_RVS_OWNERS.NEXTVAL,'" + VehicleId + "','" + CustomerId + "','" + BAddress + "','" + BcontactPerson + "','" + bMobile + "','" + bPhone + "','" + bFax + "','" + bEmail + "','" + SalesId + "')");
    }
    public int UpdateOwner(int VehicleId, int CustomerId, string BAddress, string BcontactPerson, string bMobile, string bPhone, string bFax, string bEmail, int SalesId)
    {
        return dm.ExecuteNonQuery(@"Update RVS_OWNERS Set BADDRESS='" + BAddress + "',BCONTACTPERSON='" + BcontactPerson + "',BMOBILE='" + bMobile + "',BPHONE='" + bPhone + "',BFAX='" + bFax + "',BEMAIL='" + bEmail + "' where  VEHICLEID='" + VehicleId + "' and CUSTOMERID='" + CustomerId + "'");
    }
    public int CheckSales()
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_SALES WHERE VEHICLEID=0 AND CREATEDBY='" + SessionManager.SessionName.StaffId + "'");
        if (dt.Rows.Count == 0)
        {
            dm.ExecuteNonQuery(@"Insert into RVS_SALES(ID,CREATEDBY)  VALUES(SQ_RVS_SALES.NEXTVAL,'" + SessionManager.SessionName.StaffId + "')");
            dt = dm.ExecuteQuery("Select * from RVS_SALES WHERE VEHICLEID=0 AND CREATEDBY='" + SessionManager.SessionName.StaffId + "'");
        }
        return Convert.ToInt32(dt.Rows[0]["ID"]);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_SALES WHERE ID=" + id);
    }
}