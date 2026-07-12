using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalCNFparty
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalCNFparty()
	{
	}
    public int Insert(string pname, string mobile, int countryid, string address)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_CNFPARTY(ID,PARTYNAME,MOBILE,COUNTRYID,ADDRESS,CREATEDBY)
        VALUES(SQ_BIL_CNFPARTY.NEXTVAL,'" + pname + "','" + mobile + "','" + countryid + "','" + address + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string pname, string mobile, int countryid, string address)
    {
        return dm.ExecuteNonQuery("update BIL_CNFPARTY set PARTYNAME='" + pname + "',MOBILE='" + mobile + "',COUNTRYID='" + countryid + "',ADDRESS='" + address + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_CNFPARTY WHERE ID=" + id);
    }
    public DataTable GetAllCNFParty(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_CNFPARTY C WHERE C.BILTYPEID =" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery(@"Select * from BIL_CNFPARTY CP
            INNER JOIN BS_COUNTRY C ON C.ID=CP.COUNTRYID WHERE COUNTRYID=" + id);
    }
    public bool CheckDuplicate(string pname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_CNFPARTY WHERE PARTYNAME='" + pname + "'");
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