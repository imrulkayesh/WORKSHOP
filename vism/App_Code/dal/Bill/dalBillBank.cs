using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalBillBank
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalBillBank()
    {
    }
    public int Insert(string bank, string address)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_BANK(ID,BANKNAME,ADDRESS,CREATEDBY)
        VALUES(SQ_BIL_BANK.NEXTVAL,'" + bank + "','" + address + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string bank, string address)
    {
        return dm.ExecuteNonQuery("update BIL_BANK set BANKNAME='" + bank + "',ADDRESS='" + address + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE= SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_BANK WHERE ID=" + id);
    }
    public bool CheckDuplicate(string bank)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_BANK WHERE BANKNAME='" + bank + "'");
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