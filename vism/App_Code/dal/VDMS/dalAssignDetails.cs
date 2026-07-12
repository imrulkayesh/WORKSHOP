using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalAssignDetails
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalAssignDetails()
    {
    }
    public int Insert(int assignid, int demandid, int seat , int availedseat )
    {
        return dm.ExecuteNonQuery(@"Insert into VD_ASSIGNDETAILS(ASSIGNID,DEMANDID,SEAT,AVAILEDSEAT,CREATEDBY)
        VALUES('" + assignid + "','" + demandid + "','" + seat + "','" + availedseat + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update( int assignid, int demandid, int seat, int availedseat)
    {
        return dm.ExecuteNonQuery("update VD_ASSIGNDETAILS set ASSIGNID='" + assignid + "',DEMANDID='" + demandid + "',SEAT='" + seat + "',AVAILEDSEAT='" + availedseat + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ASSIGNID='" + assignid + "'");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_ASSIGNDETAILS WHERE ASSIGNID=" + id);
    }
    public DataTable GetALLAssign()
    {
        return dm.ExecuteQuery(@"Select AD.*,D.DEMANDSEAT  from VD_ASSIGNDETAILS AD
            INNER JOIN VD_DEMAND D ON D.ID=AD.DEMANDID ");
    }
    public bool CheckDuplicate()
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_ASSIGNDETAILS ");
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