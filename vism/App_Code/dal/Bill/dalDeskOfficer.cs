using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalDeskOfficer
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalDeskOfficer()
	{
	}
    public int Insert(string deskOfficer)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_DESKOFFICER(ID,DOFFICER,CREATEDBY)
        VALUES(SQ_BIL_DESKOFFICER.NEXTVAL,'" + deskOfficer + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string deskOfficer)
    {
        return dm.ExecuteNonQuery("update BIL_DESKOFFICER set DOFFICER='" + deskOfficer + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE= SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_DESKOFFICER WHERE ID=" + id);
    }
    public bool CheckDuplicate(string deskOfficer)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_DESKOFFICER WHERE DOFFICER='" + deskOfficer + "'");
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