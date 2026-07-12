using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;


public class dalDOFrom
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDOFrom()
    {

    }
    public int Insert(int shipmentId, string pino, string invoiceName, int deskOfficerId, string pertyCode, string depoName, string depoCode, string shipmentDate, string mail, string remarks, string attechOne, string attechTwo, string attechThree)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_DOFROM(ID,SHIPMENTID,PINO,INVOICENAME,DESKOFFICERID,PARTYCODE,DEPOFNAME,DEPOFCODE,SHIPMENTDATE,MAILADDRESS,REMARKS,ATTACHMENTONE,ATTACHMENTTWO,ATTACHMENTTHREE,CREATEDBY)
                VALUES(SQ_BIL_DOFROM.NEXTVAL,'" + shipmentId + "','" + pino + "','" + invoiceName + "','" + deskOfficerId + "','" + pertyCode + "','" + depoName + "','" + depoCode + "',TO_DATE('" + shipmentDate + "','DD/MM/YYYY'),'" + mail + "','" + remarks + "','" + attechOne + "','" + attechTwo + "','" + attechThree + "','" + SessionManager.SessionName.StaffId + "')");
        //string Query = "";
        //Query += "Insert into BIL_DOFROM(ID,PINO,INVOICENAME,DESKOFFICER,PARTYCODE,DEPOFNAME,DEPOFCODE,SHIPMENTDATE,MAILADDRESS,REMARKS,ATTACHMENTONE,ATTACHMENTTWO,ATTACHMENTTHREE,CREATEDBY) VALUES(SQ_BIL_DOFROM.NEXTVAL,'" + pino + "','" + invoiceName + "','" + deskOfficer + "','" + pertyCode + "','" + depoName + "','" + depoCode + "',TO_DATE('" + shipmentDate + "','dd/MM/yyyy HH:MI:SSAM'),'" + mail + "','" + remarks + "','" + attechOne + "','" + attechTwo + "','" + attechThree + "','" + SessionManager.SessionName.StaffId + "')";
        //dm.ExecuteNonQuery(Query);
        //SendDOMail(pino);
        //return 1;
    }
    public int Update(int id, int shipmentId, string pino, string invoiceName, int deskOfficerId, string pertyCode, string depoName, string depoCode, string shipmentDate, string mail, string remarks, string attechOne, string attechTwo, string attechThree)
    {
        int val = 0;
        val = dm.ExecuteNonQuery("update BIL_DOFROM set SHIPMENTID='" + shipmentId + "', PINO='" + pino + "',INVOICENAME='" + invoiceName + "',DESKOFFICERID='" + deskOfficerId + "',PARTYCODE='" + pertyCode + "',DEPOFNAME='" + depoName + "',DEPOFCODE='" + depoCode + "',SHIPMENTDATE=TO_DATE('" + shipmentDate + "','DD/MM/YYYY'),MAILADDRESS='" + mail + "',REMARKS='" + remarks + "',ATTACHMENTONE='" + attechOne + "',ATTACHMENTTWO='" + attechTwo + "',ATTACHMENTTHREE='" + attechThree + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE STATUS=0 AND ID=" + id);
        return val;
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_DOFROM WHERE ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery(@"Select * from BIL_DOFROM ");
    }
    public DataTable GetAllDoFrom( string pino)
    {
        if (pino.Trim().ToLower()=="all")
        {
            return dm.ExecuteQuery(@"SELECT D.*, DO.DOFFICER FROM BIL_DOFROM D
        left JOIN BIL_DESKOFFICER DO ON DO.ID = D.DESKOFFICERID");
        }
        else
        {
            return dm.ExecuteQuery(@"SELECT D.*, DO.DOFFICER FROM BIL_DOFROM D
        left JOIN BIL_DESKOFFICER DO ON DO.ID = D.DESKOFFICERID where d.pino='" + pino + "'");
        }
        
    }
    public DataTable GetShipmentFreight()
    {
        return dm.ExecuteQuery("Select S.*,S.ID ||'-'||S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE ISOPEN=1");
    }
    public DataTable GetShipmentFreightDo()
    {
        return dm.ExecuteQuery(@"Select S.ID,S.ID ||'-'||S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT,
S.PINO,S.EXPNO,TO_CHAR(S.EXPDATE,'DD/MM/YYYY') EXPDATE,ST.TERM, S.TOTALQUANTITY,pt.partycode
from BIL_SHIPMENT S
INNER JOIN BIL_SALESTERM ST ON ST.ID = S.SALESTERMID 
inner join BIL_PARTY pt on s.PARTYID=pt.id
WHERE S.ISOPEN=1 
AND S.STATUS <= 5 
ORDER BY S.ID DESC");

        //return dm.ExecuteQuery("Select S.*,S.ID ||'-'||S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE S.ISOPEN=1 AND S.STATUS <= 5 ORDER BY S.ID DESC");
    }
    public DataTable GetAllShipment(int id)
    {
        return dm.ExecuteQuery(@"SELECT S.*, ST.TERM FROM BIL_SHIPMENT S 
    INNER JOIN BIL_SALESTERM ST ON ST.ID = S.SALESTERMID WHERE S.ID=" + id);
    }

    public DataTable SenderInfo(string staffid)
    {
        return dm.ExecuteQuery(@"SELECT * FROM EM_EMPLOYEE E WHERE E.STAFFID='" + staffid + "' ");
    }
    public DataTable DOFromMail(string pino)
    {
        return dm.ExecuteQuery(@"SELECT D.*, DO.DOFFICER FROM BIL_DOFROM D
        INNER JOIN BIL_DESKOFFICER DO ON DO.ID = D.DESKOFFICERID WHERE PINO='" + pino + "'");
    }

    public void BillFirstApprovalDo(int id)
    {
        dm.ExecuteNonQuery("update BIL_DOFROM set STATUS=1 WHERE ID=" + id);
    }
    public bool CheckDuplicate(string pino)
    {
        DataTable dt = dm.ExecuteQuery("Select * from BIL_DOFROM WHERE PINO='" + pino + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Delete(string pi)
    {
        string qry = @"delete from BIL_DOFROM where PINO='" + pi + "'";
        dm.ExecuteNonQuery(qry);
    }

  
}