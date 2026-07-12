using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLineApprove
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLineApprove()
    {
    }

    public DataTable GetAllLine(int status, string approver)
    {
        return dm.ExecuteQuery(@"SELECT P.ID,P.LOCATIONID,P.LINEID, P.BQUANTITY,P.CQUANTITY, P.QUANTITY,P.CREATEDATE, BC.CENTERNAME, ML.LINENAME, MI.ITEMNAME, MS.SHIFTNAME, MM.MOULDNAME,P.PRODTIONDATE, P.STATUS, P.ACTIVE FROM PR_PRODUCTION P
        INNER JOIN PR_MLINE ML ON ML.ID = P.LINEID
        INNER JOIN BS_BUSINESSCENTER BC ON BC.ID = P.LOCATIONID
        INNER JOIN PR_SHIFT S ON S.ID = P.SHIFTID
        INNER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        INNER JOIN PR_ITEM I ON I.ID = P.ITEMID
        INNER JOIN PR_MITEM MI ON MI.ID= I.ITEMID
        INNER JOIN PR_MOULD M ON M.ID = P.MOULDID
        INNER JOIN PR_MMOULD MM ON MM.ID= M.MOULDID
        INNER JOIN PR_APPROVER A ON A.LINEID = P.LINEID
        INNER JOIN EM_EMPLOYEE EM ON EM.USERID = A.APPROVERID
        WHERE P.ID>0 AND P.STATUS='" + status + "' AND A.APPROVERID='" + SessionManager.SessionName.StaffId + "'");
    }

    //    public DataTable GetAllLine(int status)
    //    {
    //        return dm.ExecuteQuery(@"SELECT P.ID, P.QUANTITY, ML.LINENAME, MI.ITEMNAME, MS.SHIFTNAME, MM.MOULDNAME,P.PRODTIONDATE FROM PR_PRODUCTION P
    //        INNER JOIN PR_MLINE ML ON ML.ID = P.LINEID
    //        INNER JOIN PR_SHIFT S ON S.ID = P.SHIFTID
    //        INNER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
    //        INNER JOIN PR_ITEM I ON I.ID = P.ITEMID
    //        INNER JOIN PR_MITEM MI ON MI.ID= I.ITEMID
    //        INNER JOIN PR_MOULD M ON M.ID = P.MOULDID
    //        INNER JOIN PR_MMOULD MM ON MM.ID= M.MOULDID
    //        WHERE P.STATUS='" + status + "' ORDER BY P.PRODTIONDATE DESC");
    //    }
    public void LineApproval(int id, int status)
    {
        dm.ExecuteNonQuery("update PR_PRODUCTION set STATUS='" + status + "',APPROVER='" + SessionManager.SessionName.StaffId + "',APPDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable LineReject(int lineId, int status)
    {
        return dm.ExecuteQuery(@"");
    }
    public DataTable GetByID(int lineId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PR_PRODUCTION WHERE ID=" + lineId);
    }

    public DataTable GetLineDetails(int id)
    {
        return dm.ExecuteQuery(@"SELECT P.*, P.QUANTITY, ML.LINENAME, MI.ITEMNAME, MS.SHIFTNAME, MM.MOULDNAME,
        FH.HOURNAME FHOUR, TH.HOURNAME THOUR, MP.OPERATORNAME, MMH.MACHINENAME,
        MZ.SIZENAME, MQ.QUALITYNAME,MR.REASONNAME FROM PR_PRODUCTION P
        INNER JOIN PR_MLINE ML ON ML.ID = P.LINEID
        INNER JOIN PR_SHIFT S ON S.ID = P.SHIFTID
        INNER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        INNER JOIN PR_ITEM I ON I.ID = P.ITEMID
        INNER JOIN PR_MITEM MI ON MI.ID= I.ITEMID
        INNER JOIN PR_MOULD M ON M.ID = P.MOULDID
        INNER JOIN PR_MMOULD MM ON MM.ID= M.MOULDID
        INNER JOIN PR_MHOUR FH ON FH.ID= P.FHOURID
        INNER JOIN PR_MHOUR TH ON TH.ID = P.THOURID
        INNER JOIN PR_OPERATOR OP ON OP.ID = P.OPERATORID
        INNER JOIN PR_MOPERATOR MP ON MP.ID = OP.OPERATORID
        INNER JOIN PR_MACHINE MH ON MH.ID = P.MACHINEID
        INNER JOIN PR_MMACHINE MMH ON MMH.ID= MH.MACHINEID
        INNER JOIN PR_MSIZE MZ ON MZ.ID = P.SIZEID
        INNER JOIN PR_MQUALITY MQ ON MQ.ID = P.QUALITYID
        INNER JOIN PR_REJECTREASON RR ON RR.ID = P.REASONID
        INNER JOIN PR_MREJECTREASON MR ON MR.ID = RR.REASONID WHERE P.ID=" + id);
    }
}