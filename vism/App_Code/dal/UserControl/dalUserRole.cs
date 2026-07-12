using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalUserRole
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalUserRole()
    {
    }
    public int Insert(string UserName, int RoleId, int active)
    {
        return dm.ExecuteNonQuery(@"Insert into UC_USERROLE(STAFFID,ROLEID,ACTIVE,CREATEDBY)
        VALUES('" + UserName + "','" + RoleId + "','" + active + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(string UserName, int RoleId, int active)
    {
        return dm.ExecuteNonQuery("update UC_USERROLE set STAFFID='" + UserName + "',ROLEID='" + RoleId + "',ACTIVE='" + active + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE where STAFFID='"+ UserName + "'");
    }
    public DataTable GetById(string UserName)
    {
        return dm.ExecuteQuery("Select * from UC_USERROLE where STAFFID='"+UserName+"'");
    }

    public int Delete(string UserName)
    {
        return dm.ExecuteNonQuery(@"DELETE FROM UC_USERROLE where STAFFID='"+ UserName + "'");
    }

    public DataTable GetAll(string UserName)
    {
        return dm.ExecuteQuery(@"SELECT RS.ID, RS.ROLENAME,UR.ROLEID, UR.STAFFID FROM UC_ROLESETUP RS
        INNER JOIN UC_USERROLE UR ON UR.ROLEID  = RS.ID
        WHERE UR.STAFFID = '"+ UserName + "'");
    }
    public DataTable loadRolelist(string UserName)
    {
        return dm.ExecuteQuery(@"SELECT RS.ID, UR.ROLEID, UR.STAFFID, UR.ACTIVE FROM UC_USERROLE UR 
        INNER JOIN UC_ROLESETUP RS ON RS.ID = UR.ROLEID 
        WHERE UR.STAFFID ='"+ UserName + "'");
    }
    public bool CheckDuplicate(string UserName)
    {
        DataTable dt = dm.ExecuteQuery("Select * from UC_USERROLE WHERE STAFFID='" + UserName + "'");
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