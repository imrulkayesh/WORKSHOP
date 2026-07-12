using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalBillType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalBillType()
	{
	}
    public int Insert(string billtype)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_BILLTYPE(ID,BILLTYPE,CREATEDBY)
        VALUES(SQ_BIL_BILLTYPE.NEXTVAL,'" + billtype + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string billtype)
    {
        return dm.ExecuteNonQuery("update BIL_BILLTYPE set BILLTYPE='" + billtype + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_BILLTYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string billtype)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_BILLTYPE WHERE BILLTYPE='" + billtype + "'");
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