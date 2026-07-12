using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalParty
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalParty()
    {
       
    }
    public int Insert(string pname, string partyadd, string mobile, int countryid, string phone, string email, string partycode)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_PARTY(ID,PARTYNAME,PARTYADDRESS,MOBILE,COUNTRYID,PHONE,EMAIL,PARTYCODE,CREATEDBY)
        VALUES(SQ_BIL_PARTY.NEXTVAL,'" + pname + "','" + partyadd + "','" + mobile + "','" + countryid + "','" + phone + "','" + email + "','" + partycode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string pname, string partyadd, string mobile, int countryid, string phone, string email, string partycode)
    {
        return dm.ExecuteNonQuery("update BIL_PARTY set PARTYNAME='" + pname + "',PARTYADDRESS='" + partyadd + "',MOBILE='" + mobile + "',COUNTRYID='" + countryid + "',PHONE='" + phone + "',EMAIL='" + email + "',PARTYCODE='" + partycode + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_PARTY WHERE ID=" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery(@"Select * from BIL_PARTY P
            INNER JOIN BS_COUNTRY C ON C.ID=P.COUNTRYID WHERE COUNTRYID=" + id);
    }
    public bool CheckDuplicate(string pname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_PARTY WHERE PARTYNAME='" + pname + "'");
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

































































































































































































