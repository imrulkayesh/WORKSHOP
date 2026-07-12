using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLWMachine
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalLWMachine()
	{
	}
    public int Insert(int businesscenter, int departmentId, int machineId, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MACHINE(ID,BUSINESSUNITID,DEPARTMENTID,MACHINEID,REMARKS,CREATEDBY)
        VALUES(SQ_PR_MACHINE.NEXTVAL,'" + businesscenter + "','" + departmentId + "','" + machineId + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int businesscenter, int departmentId, int machineId, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_MACHINE set BUSINESSUNITID='" + businesscenter + "',DEPARTMENTID='" + departmentId + "', MACHINEID='" + machineId + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MACHINE WHERE ID=" + id);
    }
    public DataTable GetLWMachine()
    {
        return dm.ExecuteQuery(@"SELECT M.*,C.COMPANYNAME , D.DEPARTMENTNAME, MM.MACHINENAME FROM PR_MACHINE M 
        INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID 
        INNER JOIN BS_COMPANY C ON C.ID = M.BUSINESSUNITID
        INNER JOIN BS_DEPARTMENT D ON D.ID = M.DEPARTMENTID ");
    }
    public bool CheckDuplicate(int businesscenter, int machineId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MACHINE WHERE BUSINESSUNITID='" + businesscenter + "' AND MACHINEID='" + machineId + "'");
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