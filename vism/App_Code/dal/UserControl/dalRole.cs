
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalRole
/// </summary>
public class dalRole
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalRole()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Insert(string roleId, string roleName, string createdBy, DateTime createDate)
    {
        dm.AddParameteres("@RoleId", roleId);
        dm.AddParameteres("@RoleName", roleName);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreateDate", createDate);
        DataTable dt = dm.ExecuteQuery("USP_Role_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int Update(string roleId, string roleName, string updatedBy, DateTime updateDate)
    {
        dm.AddParameteres("@RoleId", roleId);
        dm.AddParameteres("@RoleName", roleName);
        dm.AddParameteres("@UpdatedBy", updatedBy);
        dm.AddParameteres("@UpdateDate", updateDate);
        return dm.ExecuteNonQuery("USP_Role_Update");

    }
    public int UpdateRoles(string roleId, string roleName)
    {
        dm.AddParameteres("@RoleId", roleId);
        dm.AddParameteres("@RoleName", roleName);
        DataTable dt = dm.ExecuteQuery("aspnet_Roles_Update");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetByRoleName(string RoleName)
    {
        dm.AddParameteres("@RoleName", RoleName);
        return dm.ExecuteQuery("USP_Role_GetByRoleName");
    }
    public DataTable GetIdByUserName(string userName)
    {
        dm.AddParameteres("@UserName", userName);
        return dm.ExecuteQuery("USP_Role_IdByUserName");
    }

    public DataTable RoleNameById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Role_RoleNameById");
    }

    public int Insert(string roleName, string description, int parentId, int priority)
    {
        return dm.ExecuteNonQuery(@"Insert into UC_ROLESETUP(ID,ROLENAME,PRIORITY,PARRENTID,DESCRIPTION,CREATEDBY)
        VALUES(SQ_UC_ROLESETUP.NEXTVAL,'" + roleName + "','" + priority + "','" + parentId + "','" + description + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public int Update(int id, string roleName, string description, int parentId, int priority)
    {
        return dm.ExecuteNonQuery(@"UPDATE UC_ROLESETUP set ROLENAME='" + roleName + "',PRIORITY='" + priority + "',PARRENTID='" + parentId + "',DESCRIPTION='" + description + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= TO_DATE('" + DateTime.Now + "','MM/dd/yyyy HH:MI:SSAM') where ID=" + id);
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from UC_ROLESETUP where ID=" + id);
    }
    public DataTable GetAllRole()
    {
        return dm.ExecuteQuery("Select * from UC_ROLESETUP where ID>4");
    }
    public int Delete(string id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteNonQuery("USP_Role_Delete");
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from UC_ROLESETUP where ROLENAME='" + name + "'");
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