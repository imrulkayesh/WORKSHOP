using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalVISMCustomer
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVISMCustomer()
    {
    }
    public int Insert(string name, string mobile, string phone, string fax, string email, int genderid, string address, int countryid, int divisionid, int districtid, int thanaid, string postcode, string nid, string passportNo, string birthcertificate, string tin, int religion, string profession, string fathername, string mothername, string guardianname, string dob, string photo)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_CUSTOMER(ID,CUSTOMERNAME,MOBILE,PHONE,FAX,EMAIL,GENDERID,ADDRESS,COUNTRYID,DIVISIONID,DISTRICTID,THANAID,POSTCODE,NID,PASSPORT,BIRTHCERTIFICATE,TIN,RELIGIONID,PROFESSION,FATHERNAME,MOTHERNAME,GUARDIANNAME,CREATEDBY,DOB,PHOTO)
        VALUES(SQ_RVS_CUSTOMER.NEXTVAL,'" + name + "','" + mobile + "','" + phone + "','" + fax + "','" + email + "','" + genderid + "','" + address + "','" + countryid + "','" + divisionid + "','" + districtid + "','" + thanaid + "','" + postcode + "','" + nid + "','" + passportNo + "','" + birthcertificate + "','" + tin + "','" + religion + "','" + profession + "','" + fathername + "','" + mothername + "','" + guardianname + "','" + SessionManager.SessionName.StaffId + "', TO_DATE('" + dob + "','DD/MM/YYYY'),'" + photo + "')");
    }
    public int Update(int id, string name, string mobile, string phone, string fax, string email, int genderid, string address, int countryid, int divisionid, int districtid, int thanaid, string postcode, string nid, string passportNo, string birthcertificate, string tin, int religion, string profession, string fathername, string mothername, string guardianname, string dob, string photo)
    {
        return dm.ExecuteNonQuery("update RVS_CUSTOMER set CUSTOMERNAME='" + name + "',MOBILE='" + mobile + "',PHONE='" + phone + "',FAX='" + fax + "',EMAIL='" + email + "',GENDERID='" + genderid + "',ADDRESS='" + address + "',COUNTRYID='" + countryid + "',DIVISIONID='" + divisionid + "',DISTRICTID='" + districtid + "',THANAID='" + thanaid + "',POSTCODE='" + postcode + "',NID='" + nid + "',PASSPORT='" + passportNo + "',BIRTHCERTIFICATE='" + birthcertificate + "',TIN='" + tin + "',RELIGIONID='" + tin + "',PHOTO='" + photo + "',PROFESSION='" + profession + "',FATHERNAME='" + fathername + "',MOTHERNAME='" + mothername + "',GUARDIANNAME='" + guardianname + "',DOB=TO_DATE('" + dob + "','DD/MM/YYYY'),UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_CUSTOMER WHERE ID=" + id);
    }
    public bool CheckDuplicate(string mobile)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_CUSTOMER WHERE MOBILE='" + mobile + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public DataTable GetCustomer(string crt)
    {
        return dm.ExecuteQuery("Select * from RVS_CUSTOMER"+crt);
    }
}