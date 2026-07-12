using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalVendor
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVendor()
    {
    }
    public int Insert(string name, int countryid, string address, string phone, string mobile, string fax, string email, string website, string contactperson, string contactmobile, string contactemail)
    {
        return dm.ExecuteNonQuery(@"Insert into ASSET_VENDOR(ID,VENDORNAME,COUNTRYID,ADDRESS,PHONE,MOBILE,FAX,EMAIL,WEBSITE,CONTACTPERSON,CONTACTPERSONMOBILE,CONTACTPERSONEMAIL,CREATEDBY)
        VALUES(SQ_ASSET_VENDOR.NEXTVAL,'" + name + "','" + countryid + "','" + address + "','" + phone + "','" + mobile + "','" + fax + "','" + email + "','" + website + "','" + contactperson + "','" + contactmobile + "','" + contactemail + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, int countryid, string address, string phone, string mobile, string fax, string email, string website, string contactperson, string contactmobile, string contactemail)
    {
        return dm.ExecuteNonQuery("update ASSET_VENDOR set VENDORNAME='" + name + "',COUNTRYID=" + countryid + ",ADDRESS='" + address + "',PHONE='" + phone + "',MOBILE='" + mobile + "',FAX='" + fax + "',EMAIL='" + email + "',WEBSITE='" + website + "',CONTACTPERSON='" + contactperson + "',CONTACTPERSONMOBILE='" + contactmobile + "',CONTACTPERSONEMAIL='" + contactemail + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= TO_DATE('" + DateTime.Now + "','MM/dd/yyyy HH:MI:SSAM') where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from ASSET_VENDOR where ID=" + id);
    }
    public DataTable GetByCountryId(int countryid)
    {
        return dm.ExecuteQuery("Select * from ASSET_VENDOR where COUNTRYID=" + countryid);
    }
}