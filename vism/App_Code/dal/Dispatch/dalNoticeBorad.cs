using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalNoticeBorad
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalNoticeBorad()
    {
    }
    public int Insert(long id, int dispatchType, string docfrom, string recdate, string trackingno, string frommobile, string fromemail, string fromaddress, string fromremarks, string tstaffid, string tname, string tlocation, string tcompany, string tdepartment, string tlevel, string tmobile, string temail, string taddress, string tremarks)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_NOTICEBOARD(ID,DISPATCHTYPE,DOCFROM,RECDATE,TRACKINGNO,FROMMOBILE,FROMEMAIL,FROMADDRESS,FROMREMARKS,TSTAFFID,TNAME,TLOCATION,TCOMPANY,TDEPARTMENT,TLEVEL,TMOBILE,TEMAIL,TADDRESS,TREMARKS,CREATEDBY)
        VALUES(SQ_DP_NOTICEBOARD.NEXTVAL,'" + dispatchType + "','" + docfrom + "',TO_DATE('" + recdate + "','DD/MM/YYYY'),'" + trackingno + "','" + frommobile + "','" + fromemail + "','" + fromaddress + "','" + fromremarks + "','" + tstaffid + "','" + tname + "','" + tlocation + "','" + tcompany + "','" + tdepartment + "','" + tlevel + "','" + tmobile + "','" + temail + "','" + taddress + "','" + tremarks + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public int Update(long id, int dispatchType, string lcAmt, string docfrom, string recdate, string trackingno, string frommobile,
        string fromemail, string fromaddress, string fromremarks, string tstaffid, string tname, string tlocation,
        string tcompany, string tdepartment, string tlevel, string tmobile, string temail, string taddress,
        string tremarks)
    {
        return dm.ExecuteNonQuery("update DP_NOTICEBOARD set ID='" + id + "',DISPATCHTYPE='" + dispatchType + "',LCAMOUNT='" + lcAmt + "',DOCFROM='" + docfrom + "',RECDATE=TO_DATE('" + recdate + "','DD/MM/YYYY'),TRACKINGNO='" + trackingno + "',FROMMOBILE='" + frommobile + "',FROMEMAIL='" + fromemail + "',FROMADDRESS='" + fromaddress + "', FROMREMARKS='" + fromremarks + "',TSTAFFID='" + tstaffid + "',TNAME='" + tname + "',TLOCATION='" + tlocation + "',TCOMPANY='" + tcompany + "',TDEPARTMENT='" + tdepartment + "', TLEVEL='" + tlevel + "', TMOBILE='" + tmobile + "', TEMAIL='" + temail + "', TADDRESS='" + taddress + "', TREMARKS='" + tremarks + "',UPDATEDBY='" + SessionManager.SessionName.UserId + "',ISOPEN=0, UPDATEDATE= SYSDATE where ID=" + id);
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select E.*,B.CENTERNAME,L.NAME,C.COUNTRYNAME,L.COUNTRYID from EM_EMPLOYEE E inner join BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID INNER JOIN BS_LOCATIONS L ON L.ID=B.LOCATIONID
        INNER JOIN BS_COUNTRY C ON  C.ID=L.COUNTRYID where E.ID=" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery("Select I.*,C.PARENTID from EM_EMPLOYEE I inner join ASSET_CATEGORY C ON C.ID=I.CATEGORYID where I.ID=" + id);
    }
    public DataTable GetEmployeeDetails(string staffId)
    {
        return dm.ExecuteQuery(@"SELECT E.*,B.CENTERNAME,CO.COMPANYNAME,B.CENTERCODE,D.DEPARTMENTCODE,D.DEPARTMENTNAME, DE.DESIGNATIONNAME, B.CENTERNAME, L.NAME,C.COUNTRYNAME,L.COUNTRYID from EM_EMPLOYEE E
         INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID 
         INNER JOIN BS_COMPANY CO ON E.COMPANYID=CO.ID
         INNER JOIN BS_LOCATIONS L ON L.ID=B.LOCATIONID
         INNER JOIN BS_COUNTRY C ON  C.ID=L.COUNTRYID 
         INNER JOIN BS_DEPARTMENT D on D.ID=E.DEPARTMENTID 
         INNER JOIN BS_DESIGNATION DE on DE.ID=E.DESIGNATIONID where E.STAFFID='" + staffId + "'");
    }
    public long CheckDispatch(string UserID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_NOTICEBOARD where ISOPEN=1 AND CREATEDBY='" + UserID + "'");
        if (dt.Rows.Count == 0)
        {
            dm.ExecuteNonQuery(@"Insert into DP_NOTICEBOARD(ID,ISOPEN,CREATEDBY) VALUES(SQ_DP_NOTICEBOARD.NEXTVAL,1,'" + SessionManager.SessionName.StaffId + "')");
            dt = dm.ExecuteQuery("Select * from DP_NOTICEBOARD where ISOPEN=1 AND CREATEDBY='" + UserID + "'");
        }
        return Convert.ToInt64(dt.Rows[0]["ID"]);
    }
    public void DeleteDispatchItems(long DispatchId)
    {
        dm.ExecuteNonQuery("DELETE FROM DP_NOTICEBOARDITEM WHERE DISPATCHID=" + DispatchId);
    }
    public DataTable GetDispatchItems(long dispatchid)
    {
        return dm.ExecuteQuery("Select * from DP_NOTICEBOARDITEM WHERE DISPATCHID=" + dispatchid);

    }
    public int InsertDispatchItems(long dispatchid, string ItemName, int ItemId, string Attachment, decimal quantity, string unit, string Remarks)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_NOTICEBOARDITEM where DISPATCHID=" + dispatchid + " and ITEMNAME='" + ItemName + "'");
        if (dt.Rows.Count > 0)
        {
            return dm.ExecuteNonQuery("UPDATE DP_NOTICEBOARDITEM SET QUANTITY=QUANTITY+" + quantity + " WHERE ID=" + dt.Rows[0]["ID"]);
        }
        else
        {
            return dm.ExecuteNonQuery("INSERT INTO DP_NOTICEBOARDITEM(ID,ITEMID,QUANTITY,UNIT,ITEMNAME,DISPATCHID,ATTACHMENT,NOTES) Values(SQ_DP_NOTICEBOARDITEM.NEXTVAL," + ItemId + "," + quantity + ",'" + unit + "','" + ItemName + "'," + dispatchid + ",'" + Attachment + "','" + Remarks + "')");
        }
    }
    public int UpdateDispatchItems(long Id, decimal quantity, string unit, string remarks)
    {
        return dm.ExecuteNonQuery("UPDATE DP_NOTICEBOARDITEM SET QUANTITY='" + quantity + "',UNIT='" + unit + "', NOTES='" + remarks + "' WHERE ID=" + Id);
    }
    public DataTable GetDispatchDetails(long id)
    {
        return dm.ExecuteQuery(@"SELECT N.*, ND.ITEMNAME, ND.QUANTITY, ND.UNIT, ND.DISPATCHID FROM DP_NOTICEBOARD N
        LEFT OUTER JOIN DP_NOTICEBOARDITEM ND ON ND.DISPATCHID = N.ID 
        WHERE N.ID=" + id);
    }
    public void UpdatePrint(long id)
    {
        dm.ExecuteNonQuery(@"Update DP_NOTICEBOARD set NOOFPRINT=NOOFPRINT+1 where ID=" + id);
    }

    public DataTable GetDispatcNoticehDetails(string Criteria)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT N.*,TO_CHAR(N.RECEIVEDDATE,'DD-MM-YYYY HH:MI:SS AM') RECDATETIME, DT.DTYPENAME FROM DP_NOTICEBOARD N
        LEFT OUTER JOIN DP_DISPATCHTYPE DT ON DT.ID= N.DISPATCHTYPE
        WHERE N.ID >0 " + Criteria + " ");
    }

    public int UpdateReceiver(long id, string receiverStaffid,string receiverName, string receiverCompany, string receiverDepartment, string receiverMobile, string receiverEmail, string receiverRrmarks)
    {
        return dm.ExecuteNonQuery("Update DP_NOTICEBOARD set RECSTAFFID='" + receiverStaffid + "',RECDEPARTMENT='" + receiverDepartment + "',RECMOBILE='" + receiverMobile + "',RECEMAIL='" + receiverEmail + "',RECREMARKS='" + receiverRrmarks + "', UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE,RECEIVEDDATE=SYSDATE,RECEIVERNAME='"+ receiverName + "', RECEIVERCOMPANY='"+receiverCompany+"', ACTIVE=0 where ACTIVE=1 AND ID=" + id);
    }

    public bool ReceiverCheck(long id, string receiverStaffID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_NOTICEBOARD where ACTIVE = 1 AND ID='" + id + "' AND RECSTAFFID='" + receiverStaffID + "'");

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