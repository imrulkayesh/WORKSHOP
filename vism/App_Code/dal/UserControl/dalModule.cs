using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalModule
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalModule()
    {
    }
    public int Insert(string name, int ModuleOrder, string url, string icon)
    {
        return dm.ExecuteNonQuery(@"Insert into UC_MODULE(ID,MODULENAME,MODULEORDER,URL,ICON,CREATEDBY)
        VALUES(SQ_UC_MODULE.NEXTVAL,'" + name + "','" + ModuleOrder + "','" + url + "','" + icon + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string name, int ModuleOrder, string url, string icon)
    {
        return dm.ExecuteNonQuery("update UC_MODULE set MODULENAME='" + name + "',MODULEORDER='" + ModuleOrder + "',URL='" + url + "',ICON='" + icon + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from UC_MODULE where ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from UC_MODULE WHERE MODULENAME='" + name + "'");
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