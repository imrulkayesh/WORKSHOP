using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalSalesTerm
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalSalesTerm()
	{
	}
    public int Insert(string term)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_SALESTERM(ID,TERM,CREATEDBY)
        VALUES(SQ_BIL_SALESTERM.NEXTVAL,'" + term + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string term)
    {
        return dm.ExecuteNonQuery("update BIL_SALESTERM set TERM='" + term + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_SALESTERM WHERE ID=" + id);
    }
    public bool CheckDuplicate(string term)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_SALESTERM WHERE TERM='" + term + "'");
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