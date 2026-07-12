using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLWMould
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLWMould()
    {
    }
    public int Insert(int businesscenter, int departmentId, int mouldId, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MOULD(ID,BUSINESSUNITID,DEPARTMENTID,MOULDID,REMARKS,CREATEDBY)
        VALUES(SQ_PR_MOULD.NEXTVAL,'" + businesscenter + "','" + departmentId + "','" + mouldId + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int businesscenter, int departmentId, int mouldId, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_MOULD set BUSINESSUNITID='" + businesscenter + "',DEPARTMENTID='" + departmentId + "', MOULDID='" + mouldId + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MOULD WHERE ID=" + id);
    }
    public DataTable GetLWMould()
    {
        return dm.ExecuteQuery(@"SELECT M.*, MM.MOULDNAME,C.COMPANYNAME , D.DEPARTMENTNAME FROM PR_MOULD M 
        INNER JOIN PR_MMOULD MM ON MM.ID = M.MOULDID   
        INNER JOIN BS_COMPANY C ON C.ID = M.BUSINESSUNITID
        INNER JOIN BS_DEPARTMENT D ON D.ID = M.DEPARTMENTID");
    }
    public bool CheckDuplicate(int businesscenter, int mouldId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MOULD WHERE BUSINESSUNITID='" + businesscenter + "' AND MOULDID='" + mouldId + "'");
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