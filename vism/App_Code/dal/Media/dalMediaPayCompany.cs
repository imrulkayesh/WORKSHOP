using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMediaPayCompany
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMediaPayCompany()
    {
    }
    public int Insert(string comName, string conPerson, string mobile, string phone, string email, string fax, string website, string address)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_PAYMENTCOMPANY(ID,COMPANYNAME,CONTACTPERSON,MOBILE,PHONE,EMAIL,FAX,WEBSITE,ADDRESS,CREATEDBY)
        VALUES(SQ_MD_PAYMENTCOMPANY.NEXTVAL,'" + comName + "','" + conPerson + "','" + mobile + "','" + phone + "','" + email + "','" + fax + "','" + website + "','" + address + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string comName, string conPerson, string mobile, string phone, string email, string fax, string website, string address)
    {
        return dm.ExecuteNonQuery("update MD_PAYMENTCOMPANY set COMPANYNAME='" + comName + "',CONTACTPERSON='" + conPerson + "',MOBILE='" + mobile + "',PHONE='" + phone + "',EMAIL='" + email + "',FAX='" + fax + "',WEBSITE='" + website + "',ADDRESS='" + address + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_PAYMENTCOMPANY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string comName)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_PAYMENTCOMPANY WHERE COMPANYNAME='" + comName + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
