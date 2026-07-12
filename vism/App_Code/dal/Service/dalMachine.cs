using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMachine
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMachine()
    {
    }
    public int Insert(int buscenter, int company, int department,  string machinename, string machinecode )
    {
        return dm.ExecuteNonQuery(@"Insert into SC_MACHINE(ID,BUSINESSCENTERID,COMPANYID,DEPARTMENTID,MACHINENAME,MACHINECODE,CREATEDBY)
        VALUES(SQ_SC_MACHINE.NEXTVAL,'" + buscenter + "','" + company + "','" + department + "','" + machinename + "','" + machinecode + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int buscenter, int company, int department, string machinename, string machinecode)
    {
        return dm.ExecuteNonQuery("update SC_MACHINE set BUSINESSCENTERID='" + buscenter + "',COMPANYID='" + company + "',DEPARTMENTID='" + department + "',MACHINENAME='" + machinename + "',MACHINECODE='" + machinecode + "',UPDATEDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from SC_MACHINE where ID=" + id);
    }
    public DataTable GetByBusinessCenterId(int BusinessCenterId)
    {
        return dm.ExecuteQuery(@"Select M.*,B.CENTERNAME, C.COMPANYNAME, D.DEPARTMENTNAME from SC_MACHINE M
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=M.BUSINESSCENTERID 
        INNER JOIN BS_COMPANY C ON C.ID=M.COMPANYID 
        INNER JOIN BS_DEPARTMENT D ON D.ID= M.DEPARTMENTID where M.BUSINESSCENTERID=" + BusinessCenterId);
    }
    
    public bool CheckDuplicate(string machinename, int buscenter, int company)
    {
        DataTable dt = dm.ExecuteQuery("Select * from SC_MACHINE WHERE MACHINENAME='" + machinename + "'AND BUSINESSCENTERID=" + buscenter + " AND COMPANYID=" + company);
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