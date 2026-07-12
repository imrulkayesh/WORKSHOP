using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLawyer
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public dalLawyer()
    {
    }
    public int Insert(string lawyer, string mobile, string phone, string email, string fax, string chamber, int courtid)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_LAWYER(ID,LAWYERNAME,MOBILE,PHONE,EMAIL,FAX,CHAMBER,COURTID,CREATEDBY)
        VALUES(SQ_LEG_LAWYER.NEXTVAL,'" + lawyer + "','" + mobile + "','" + phone + "','" + email + "','" + fax + "','" + chamber + "','" + courtid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string lawyer, string mobile, string phone, string email, string fax, string chamber, int courtid)
    {
        return dm.ExecuteNonQuery("update LEG_LAWYER set LAWYERNAME='" + lawyer + "', MOBILE='" + mobile + "', PHONE='" + phone + "', EMAIL='" + email + "', FAX='" + fax + "', CHAMBER='" + chamber + "', COURTID='" + courtid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_LAWYER WHERE ID=" + id);
    }
    public DataTable GetAllCourt()
    {
        return dm.ExecuteQuery(@"Select L.*, C.COURTNAME from LEG_LAWYER L
        INNER JOIN LEG_COURT C ON C.ID = L.COURTID");
    }
    public bool CheckDuplicate(string lawyer, int courtid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_LAWYER WHERE LAWYERNAME='" + lawyer + "' AND COURTID='" + courtid + "'");
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