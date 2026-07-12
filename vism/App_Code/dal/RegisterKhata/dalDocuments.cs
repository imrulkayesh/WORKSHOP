using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalDocuments
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDocuments()
    {
    }
    public int Insert(int locationId, string documentName, string senderName, string senderMobile, string receiverName, string receiverMobile, string transport, string inDate, string inTime, string outDate, string outTime, string Remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into RK_DOCUMENT(ID,LOCATIONID,DOCUMENTNAME,SENDERNAME,SENDERMOBILE,RECEIVERNAME,RECEIVERMOBILE,TRANSPORT,INDATE,INTIME,OUTDATE,OUTTIME,REMARKS,CREATEDBY)
        VALUES(SQ_RK_DOCUMENT.NEXTVAL,'" + locationId + "','" + documentName + "','" + senderName + "','" + senderMobile + "','" + receiverName + "','" + receiverMobile + "','" + transport + "',TO_DATE('" + inDate + "','DD/MM/YYYY'),'" + inTime + "',TO_DATE('" + outDate + "','DD/MM/YYYY'),'" + outTime + "','" + Remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int locationId, string documentName, string senderName, string senderMobile, string receiverName, string receiverMobile, string transport, string inDate, string inTime, string outDate, string outTime, string Remarks)
    {
        return dm.ExecuteNonQuery("update RK_DOCUMENT set LOCATIONID='" + locationId + "',DOCUMENTNAME='" + documentName + "',SENDERNAME='" + senderName + "',SENDERMOBILE='" + senderMobile + "',RECEIVERNAME='" + receiverName + "',RECEIVERMOBILE='" + receiverMobile + "',TRANSPORT='" + transport + "',INDATE=TO_DATE('" + inDate + "','DD/MM/YYYY'),INTIME='" + inTime + "',OUTDATE=TO_DATE('" + outDate + "','DD/MM/YYYY'),OUTTIME='" + outTime + "',REMARKS='" + Remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RK_DOCUMENT WHERE ID=" + id);
    }

    public DataTable GetALL(int id)
    {
        return dm.ExecuteQuery(@"SELECT G.*, B.CENTERNAME FROM RK_DOCUMENT G
        INNER JOIN BS_BUSINESSCENTER B ON B.ID = G.LOCATIONID
        WHERE G.ID>0 AND B.ID='"+id+"'ORDER BY G.CREATEDATE DESC");
    }
    public bool CheckDuplicate(string guestname)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RK_DOCUMENT WHERE GUESTNAME='" + guestname + "' ");
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