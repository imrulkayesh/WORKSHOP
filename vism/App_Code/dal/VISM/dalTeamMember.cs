using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalTeamMember
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalTeamMember()
    {
    }
    public int Insert(int teamid, string staffid, string fdate, int workshopid)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_TEAMMEMBER(ID,TEAMID,STAFFID,FDATE,WORKSHOPID,CREATEDBY)
        VALUES(SQ_RVS_TEAMMEMBER.NEXTVAL,'" + teamid + "','" + staffid + "',TO_DATE('" + fdate + "','DD/MM/YYYY'),'" + workshopid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int teamid, string staffid, string fdate, int workshopid,int isActive)
    {
        return dm.ExecuteNonQuery("update RVS_TEAMMEMBER set TEAMID='" + teamid + "',STAFFID='" + staffid + "',FDATE=TO_DATE('" + fdate + "','DD/MM/YYYY'),WORKSHOPID='" + workshopid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE,ISACTIVE='"+ isActive + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_TEAMMEMBER WHERE ID=" + id);
    }
    public DataTable GetAll(int teamId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM RVS_TEAMMEMBER M WHERE M.TEAMID='" + teamId + "' ");
    }

    public bool CheckDuplicate(string teamid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_TEAMMEMBER WHERE TEAMID='" + teamid + "'");
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