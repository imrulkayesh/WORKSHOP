using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalGuestKhata
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalGuestKhata()
    {
    }
    public int Insert(int locationId, string guestname, int designationId, string mobile, string address, string reason, string inDate, string inTime, string outDate, string outTime, string Remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into RK_GUESTKHATA(ID,LOCATIONID,GUESTNAME,DESIGNATIONID,MOBILE,ADDRESS,REASON,INDATE,INTIME,OUTDATE,OUTTIME,REMARKS,CREATEDBY)
        VALUES(SQ_RK_GUESTKHATA.NEXTVAL,'" + locationId + "','" + guestname + "','" + designationId + "','" + mobile + "','" + address + "','" + reason + "',TO_DATE('" + inDate + "','DD/MM/YYYY'),'" + inTime + "',TO_DATE('"+outDate+"','DD/MM/YYYY'),'" + outTime + "','" + Remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int locationId, string guestname, int designationId, string mobile, string address, string reason, string inDate, string inTime, string outDate,string outTime, string Remarks)
    {
        return dm.ExecuteNonQuery("update RK_GUESTKHATA set LOCATIONID='" + locationId + "',GUESTNAME='" + guestname + "',DESIGNATIONID='" + designationId + "',MOBILE='" + mobile + "',ADDRESS='" + address + "',REASON='" + reason + "',INDATE=TO_DATE('" + inDate + "','DD/MM/YYYY'),INTIME='" + inTime + "',OUTDATE=TO_DATE('"+outDate+"','DD/MM/YYYY'),OUTTIME='" + outTime + "',REMARKS='" + Remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RK_GUESTKHATA WHERE ID=" + id);
    }

    public DataTable GetALL(int id)
    {
        return dm.ExecuteQuery(@"SELECT G.*, B.CENTERNAME, D.DESIGNATIONNAME FROM RK_GUESTKHATA G
        INNER JOIN BS_BUSINESSCENTER B ON B.ID = G.LOCATIONID
        INNER JOIN BS_DESIGNATION D ON D.ID = G.DESIGNATIONID WHERE G.ID> 0 AND B.ID='"+id+"'ORDER BY G.CREATEDATE DESC");
    }
    public bool CheckDuplicate(string guestname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RK_GUESTKHATA WHERE GUESTNAME='" + guestname + "' ");
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