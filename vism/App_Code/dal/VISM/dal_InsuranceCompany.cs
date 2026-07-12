using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dal_InsuranceCompany
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dal_InsuranceCompany()
    {
    }
    public int Insert(string name,string address,string mobile,string phone,string fax,string email)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_INSURANCECOMPANY(ID,COMPANYNAME,ADDRESS,MOBILE,PHONE,FAX,EMAIL,CREATEDBY)
        VALUES(SQ_RVS_INSURANCECOMPANY.NEXTVAL,'" + name + "','" + address + "','" + mobile + "','" + phone + "','" + fax + "','" + email + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, string address, string mobile, string phone, string fax, string email)
    {
        return dm.ExecuteNonQuery("update RVS_INSURANCECOMPANY set COMPANYNAME='" + name + "',ADDRESS='" + address + "',MOBILE='" + mobile + "',PHONE='" + phone + "',FAX='" + fax + "',EMAIL='" + email + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_INSURANCECOMPANY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_INSURANCECOMPANY WHERE COMPANYNAME='" + name + "'");
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