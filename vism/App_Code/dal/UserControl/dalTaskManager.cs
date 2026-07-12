
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalTaskManager
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalTaskManager()
    {
    }
    public int Insert(string textBan, string textEng, string url, int parentId, int OrderColumn, string icon, int ModuleId)
    {
        return dm.ExecuteNonQuery(@"insert into UC_TASKMANAGER(ID,TEXTBAN,TEXTENG,URL,PARENTID,CREATEDBY,ORDERCOLUMN,ICON,MODULEID)
        Values(SQ_UC_TASKMANAGER.NEXTVAL,'" + textBan + "','" + textEng + "','" + url + "','" + parentId + "','" + SessionManager.SessionName.StaffId + "','" + OrderColumn + "','" + icon + "','" + ModuleId + "')");
    }

    public int Update(int id, string textBan, string textEng, string url, int parentId, int OrderColumn, string icon, int ModuleId)
    {
        return dm.ExecuteNonQuery("update UC_TASKMANAGER set TEXTBAN='" + textBan + "',TEXTENG='" + textEng + "',URL='" + url + "',PARENTID='" + parentId + "',ORDERCOLUMN='" + OrderColumn + "',ICON='" + icon + "',MODULEID='" + ModuleId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "','dd/MM/yyyy HH:MI:SSAM') where ID=" + id);
    }

    //public DataTable GetParanet(int role, int ModuleId)
    //{
    //    DataTable dt = new DataTable();
    //    if (role == 1)
    //    {
    //        dt = dm.ExecuteQuery("select * from UC_TASKMANAGER where MODULEID='" + ModuleId + "' AND PARENTID=0 order by ORDERCOLUMN asc");
    //    }
    //    else
    //    {
    //        dt = dm.ExecuteQuery("select T.* from UC_TASKPERMISSION TP INNER JOIN  UC_TASKMANAGER T ON T.ID=TP.TASKID where T.MODULEID='" + ModuleId + "' AND T.PARENTID=0 AND TP.ROLEID=" + role + " order by T.ORDERCOLUMN asc");
    //    }


    //    return dt;
    //}
    public DataTable GetParanet(int role, int ModuleId)
    {
        DataTable dt = new DataTable();
        if (role == 1)
        {
            dt =
                dm.ExecuteQuery("select * from UC_TASKMANAGER where MODULEID='" + ModuleId + "' AND PARENTID=0 order by ORDERCOLUMN asc");
        }
        else
        {
            dt = dm.ExecuteQuery(@"select distinct T.* from UC_TASKPERMISSION TP INNER JOIN  UC_TASKMANAGER T ON T.ID=TP.TASKID where T.MODULEID='" + ModuleId + "' AND T.PARENTID=0 AND TP.ROLEID in (SELECT ROLEID FROM UC_USERROLE WHERE STAFFID  = '" + SessionManager.SessionName.StaffId + "' ) order by T.ORDERCOLUMN asc");
        }
        return dt;
    }
    public DataTable GetPermissionParanet(int role)
    {
        DataTable dt = new DataTable();
        if (role == 1)
        {
            dt = dm.ExecuteQuery("select * from UC_TASKMANAGER where  PARENTID=0 order by ORDERCOLUMN asc");
        }
        else
        {
            dt = dm.ExecuteQuery("select T.* from UC_TASKPERMISSION TP INNER JOIN  UC_TASKMANAGER T ON T.ID=TP.TASKID where T.PARENTID=0 AND TP.ROLEID=" + role + " order by T.ORDERCOLUMN asc");
        }


        return dt;
    }
    public DataTable GetChild(int ParentId, int roleId)
    {
        DataTable dt = new DataTable();
        if (roleId == 1)
        {
            dt = dm.ExecuteQuery("select UC_TASKMANAGER.*,UC_TASKMANAGER.Id TaskId from UC_TASKMANAGER where PARENTID=" + ParentId + " order by ORDERCOLUMN asc");
        }
        else
        {
            dt = dm.ExecuteQuery("select T.* from UC_TASKPERMISSION TP INNER JOIN  UC_TASKMANAGER T ON T.ID=TP.TASKID where T.PARENTID=" + ParentId + " AND TP.ROLEID in (SELECT ROLEID FROM UC_USERROLE WHERE STAFFID  = '" + SessionManager.SessionName.StaffId + "' )  order by T.ORDERCOLUMN asc");
        }
        return dt;
    }
    //public DataTable GetModule(int roleId)
    //{
    //    DataTable dt = new DataTable();
    //    if (roleId == 1)
    //    {
    //        dt = dm.ExecuteQuery("select * FROM UC_MODULE order by MODULEORDER asc");
    //    }
    //    else
    //    {
    //        dt = dm.ExecuteQuery(@"SELECT DISTINCT M.* FROM UC_TASKPERMISSION TP INNER JOIN UC_TASKMANAGER T ON T.ID=TP.TASKID
    //        INNER JOIN UC_MODULE M ON M.ID=T.MODULEID WHERE TP.ROLEID='" + roleId + "' order by M.MODULEORDER asc");
    //    }
    //    return dt;
    //}

    public DataTable GetModule(int roleId)
    {

        DataTable dt = new DataTable();
        if (roleId == 1)
        {
            dt = dm.ExecuteQuery("select * FROM UC_MODULE order by MODULEORDER asc");
        }
        else
        {
            dt = dm.ExecuteQuery(@"SELECT DISTINCT M.* FROM UC_TASKPERMISSION TP INNER JOIN UC_TASKMANAGER T ON T.ID=TP.TASKID
            INNER JOIN UC_MODULE M ON M.ID=T.MODULEID WHERE TP.ROLEID in (SELECT ROLEID FROM UC_USERROLE WHERE STAFFID  = '" + SessionManager.SessionName.StaffId + "' )  order by M.MODULEORDER asc");
        }
        return dt;
    }
    public DataTable GetAllByRoleId(int roleId, int ModuleId)
    {
        //DataTable dt = new DataTable();
        //if (roleId == 1)
        //{
        //    dt = dm.ExecuteQuery("select T.*,M.URL MURL from UC_TASKMANAGER T INNER JOIN UC_MODULE M ON M.ID=T.MODULEID WHERE T.MODULEID='" + ModuleId + "' order by ORDERCOLUMN asc");
        //}
        //else
        //{
        //    return dm.ExecuteQuery(@"select distinct T.* from UC_TASKPERMISSION TP INNER JOIN  UC_TASKMANAGER T ON T.ID=TP.TASKID where T.MODULEID='" + ModuleId + "' AND TP.ROLEID in (SELECT ROLEID FROM UC_USERROLE WHERE STAFFID  = '" + SessionManager.SessionName.StaffId + "' ) order by T.ORDERCOLUMN asc");
        //}
        //return dt;
        return dm.ExecuteQuery(@"select distinct T.*,M.URL MURL from UC_TASKPERMISSION TP INNER JOIN  UC_TASKMANAGER T ON T.ID=TP.TASKID INNER JOIN UC_MODULE M ON M.ID=T.MODULEID where T.MODULEID='" + ModuleId + "' AND TP.ROLEID in (SELECT ROLEID FROM UC_USERROLE WHERE STAFFID  = '" + SessionManager.SessionName.StaffId + "' ) order by T.ORDERCOLUMN asc");
    }
    public DataTable GetAllTask(int ModuleId)
    {
        return dm.ExecuteQuery("Select * from UC_TASKMANAGER WHERE MODULEID=" + ModuleId);
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("select * from UC_TASKMANAGER where ID=" + id + " order by ORDERCOLUMN asc");
    }

    public int TaskToRoleInsert(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            dm.ExecuteNonQuery(@"Insert into UC_TASKPERMISSION(TASKID,ROLEID,CREATEDBY)
            VALUES(" + Convert.ToInt32(dr["TId"]) + ",'" + Convert.ToInt32(dr["RId"]) + "','" + SessionManager.SessionName.StaffId + "')");
        }
        return 1;
    }
    public int DeleteTaskToRole(int roleId)
    {
        return dm.ExecuteNonQuery("Delete from UC_TASKPERMISSION where ROLEID=" + roleId);
    }
}