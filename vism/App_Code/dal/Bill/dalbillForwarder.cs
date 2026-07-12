using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalbillForwarder
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalbillForwarder()
    {
        
    }
    public int Insert(string forwarder, string frdcode, string mobile, string phone, string email, int country, string address)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_FORWARDER(ID,FORWARDER,FRDCODE,MOBILE,PHONE,EMAIL,COUNTRYID,ADDRESS,CREATEDBY)
        VALUES(SQ_BIL_FORWARDER.NEXTVAL,'" + forwarder + "','" + frdcode + "','" + mobile + "','" + phone + "','" + email + "','" + country + "','" + address + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string forwarder, string frdcode, string mobile, string phone, string email, int country, string address)
    {
        return dm.ExecuteNonQuery("update BIL_FORWARDER set FORWARDER='" + forwarder + "',FRDCODE='" + frdcode + "',MOBILE='" + mobile + "',PHONE='" + phone + "',EMAIL='" + email + "',COUNTRYID='" + country + "',ADDRESS='" + address + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllCon()
    {
        return dm.ExecuteQuery(@"Select F.*, C.COUNTRYCODE,C.COUNTRYNAME from BIL_FORWARDER F 
        INNER JOIN BS_COUNTRY C ON C.ID = F.COUNTRYID");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_FORWARDER WHERE ID=" + id);
    }
    public bool CheckDuplicate(string forwarder)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_FORWARDER WHERE FORWARDER='" + forwarder + "'");
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
