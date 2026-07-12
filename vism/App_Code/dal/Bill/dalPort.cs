using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalPort
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalPort()
	{
	}
    public int Insert(string port, int countryid, string address)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_PORT(ID,PORTNAME,COUNTRYID,ADDRESS,CREATEDBY)
        VALUES(SQ_BIL_PORT.NEXTVAL,'" + port + "','" + countryid + "','" + address + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string port, int countryid, string address)
    {
        return dm.ExecuteNonQuery("update BIL_PORT set PORTNAME='" + port + "',COUNTRYID='" + countryid + "',ADDRESS='" + address + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_PORT WHERE ID=" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery(@"Select * from BIL_PORT P
            INNER JOIN BS_COUNTRY C ON C.ID=P.COUNTRYID WHERE COUNTRYID=" + id);
    }
    public bool CheckDuplicate(string port)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_PORT WHERE PORTNAME='" + port + "'");
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