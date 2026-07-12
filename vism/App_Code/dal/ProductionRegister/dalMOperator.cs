using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMOperator
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMOperator()
    {
    }
    public int Insert(int locationId, int businesscenter, int departmentId, int lineId, string staffid, string name, string mobile, string email)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MOPERATOR(ID,LOCATIONID,BUSINESSUNITID,DEPARTMENTID,LINEID,STAFFID,OPERATORNAME,MOBILE,EMAIL,CREATEDBY)
        VALUES(SQ_PR_MOPERATOR.NEXTVAL,'" + locationId + "','" + businesscenter + "','" + departmentId + "','" + lineId + "','" + staffid + "','" + name + "','" + mobile + "','" + email + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int locationId, int businesscenter, int departmentId, int lineId, string staffid, string name, string mobile, string email)
    {
        return dm.ExecuteNonQuery("update PR_MOPERATOR set LOCATIONID='" + locationId + "',BUSINESSUNITID='" + businesscenter + "',DEPARTMENTID='" + departmentId + "',LINEID='" + lineId + "',STAFFID='" + staffid + "',OPERATORNAME='" + name + "',MOBILE='" + mobile + "',EMAIL='" + email + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MOPERATOR WHERE ID=" + id);
    }

    public DataTable GetAllOperator(int id)
    {
        return dm.ExecuteQuery(@"SELECT OP.*, BS.CENTERNAME, L.LINENAME FROM PR_MOPERATOR OP
        INNER JOIN BS_BUSINESSCENTER BS ON BS.ID = OP.LOCATIONID
        INNER JOIN PR_MLINE L ON L.ID = OP.LINEID WHERE OP.LOCATIONID=" + id);
    }
    public bool CheckDuplicate(int locationId, int lineId, string staffid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MOPERATOR WHERE LOCATIONID='" + locationId + "' AND LINEID='" + lineId + "' AND STAFFID='" + staffid + "'");
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