using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalTeam
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalTeam()
    {
        
    }
    public int Insert(int wshopid, string teamName,  string inchstaffid, int coder)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_TEAM(ID,WORKSHOPID,TEAMNAME,INCHARGESTAFFID,CORDER,CREATEDBY)
        VALUES(SQ_RVS_TEAM.NEXTVAL,'" + wshopid + "','" + teamName + "','" + inchstaffid + "','" + coder + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int wshopid, string teamName, string inchstaffid, int coder)
    {
        return dm.ExecuteNonQuery("update RVS_TEAM set WORKSHOPID='" + wshopid + "',TEAMNAME='" + teamName + "',INCHARGESTAFFID='" + inchstaffid + "',CORDER='" + coder + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_TEAM WHERE ID=" + id);
    }
    public DataTable GetAll(int workshopid)
    {
        return dm.ExecuteQuery("Select T.*, WS.WORKSHOP  from RVS_TEAM T INNER JOIN RVS_WORKSHOP WS ON WS.ID = T.WORKSHOPID WHERE T.WORKSHOPID=" + workshopid);
    }
  
    public bool CheckDuplicate(int wshopid, string teamName)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_TEAM WHERE WORKSHOPID='" + wshopid + "' AND TEAMNAME='" + teamName+"'");
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