using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalDispatch
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDispatch()
    {
    }

    public int Update(long id, string ReceiverStaffId, string Staffid, string ReceiverName, string ReceiverMobile, string ReceiverAddress, string ReceiverDepartment, string ReceiverCenter, int IssueType, int SenderDept, int SenderCenter, int RecDept, int RecCenter, int DirectSend, string SenderName, string senderMobile)
    {
        try
        {
            string office = "";
            if (SessionManager.SessionName.BusinessCentetid >= 100)
            {
                office = SessionManager.SessionName.BusinessCentetid.ToString();
            }
            else if (SessionManager.SessionName.BusinessCentetid < 100 && SessionManager.SessionName.BusinessCentetid >= 10)
            {
                office = "0" + SessionManager.SessionName.BusinessCentetid.ToString();
            }
            else
            {
                office = "00" + SessionManager.SessionName.BusinessCentetid.ToString();
            }
            string TransID = "DG" + office + DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("d2");
            string TransIdQuery = "(Select '" + TransID + "'||TRIM(TO_CHAR((NVL(MAX(TO_NUMBER(SUBSTR(TRANSACTIONID,10,6))),0)+1),'000000')) ID from DP_DISPATCH where TRIM(TO_CHAR(NVL(SUBSTR(TRANSACTIONID,3,3),0),'000'))='" + office + "' and TRIM(TO_CHAR(NVL(SUBSTR(TRANSACTIONID,6,2),0),'00'))='" + DateTime.Now.ToString("yy") + "'  and TRIM(TO_CHAR(NVL(SUBSTR(TRANSACTIONID,8,2),0),'00'))='" + DateTime.Now.Month.ToString("d2") + "')";
            string Query = "BEGIN ";
            Query += "delete from dp_transits where DISPATCHID=" + id + "; COMMIT;";

            Query += "update DP_DISPATCH set TOUSER='" + ReceiverStaffId + "',FROMUSER='" + Staffid + "',SENDNAME='" + SenderName + "',SENDMOBILE='" + senderMobile + "',SENDDEPARTMENTID=" + SenderDept + ",SENDBUSINESSCENTERID='" + SenderCenter + "',RECDEPARTMENTID='" + RecDept + "',RECBUSINESSCENTERID='" + RecCenter + "',DIRECTSEND='" + DirectSend + "', ISOPEN=0,STATUSID=1,RECEIVERNAME='" + ReceiverName + "',ISSUETYPEID=" + IssueType + ",RECEIVERMOBILE='" + ReceiverMobile + "',RECEIVERADDRESS='" + ReceiverAddress + "',RECEIVERDEPARTMENT='" + ReceiverDepartment + "', BUSINESSCENTER='" + ReceiverCenter + "',UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE,CREATEDATE=SYSDATE, TRANSACTIONID=" + TransIdQuery + " where ID=" + id + ";";
            Query += "Insert into DP_TRANSITS(ID, DISPATCHID, STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL, " + id + ", 'Final Approver');";
            //dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'Final Approver')");
            // dm.ExecuteNonQuery(@"update DP_DISPATCH set TOUSER='" + ReceiverStaffId + "',FROMUSER='" + Staffid + "',SENDNAME='" + SenderName + "',SENDMOBILE='" + senderMobile + "',SENDDEPARTMENTID=" + SenderDept + ",SENDBUSINESSCENTERID='" + SenderCenter + "',RECDEPARTMENTID='" + RecDept + "',RECBUSINESSCENTERID='" + RecCenter + "',DIRECTSEND='" + DirectSend + "', ISOPEN=0,STATUSID=1,RECEIVERNAME='" + ReceiverName + "',ISSUETYPEID=" + IssueType + ",RECEIVERMOBILE='" + ReceiverMobile + "',RECEIVERADDRESS='" + ReceiverAddress + "',RECEIVERDEPARTMENT='" + ReceiverDepartment + "', BUSINESSCENTER='" + ReceiverCenter + "',UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE,CREATEDATE=SYSDATE, TRANSACTIONID=" + TransIdQuery + " where ID=" + id);
            Query += " COMMIT; END;";
            dm.ExecuteNonQuery(Query);
            SendInitialMail(id);
            return 1;
        }
        catch { return 0; }
    }

    public void GetDispatchReport()
    {
        dm.ExecuteQuery(@"");
    }
    public void SendInitialMail(long dispatchId)
    {
        DataTable dt = DispatchMailDetails(dispatchId);
        string fromuser = dt.Rows[0]["OFFICEMAIL"].ToString();
        List<string> Approver = new dalApprover().GetByCreatedby(dt.Rows[0]["CREATEDBY"].ToString());
        string MessageforSender = "<h3>Your dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " is waiting for approval</h3>";
        MessageforSender += "<b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageforSender += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageforSender += "<h4>Item Details : </4>";

        string MessageApprover = "<h3>New dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " waiting for approval</h3>";
        MessageApprover += "<b>Sendar : </b>" + dt.Rows[0]["EMPLOYEENAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["OFFICEMOBILE"].ToString();
        MessageApprover += "<hr><b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageApprover += "<h4>Item Details : </4>";

        string dispatchItems = "<table border='1' cellspacing='0' cellpadding='0' width='400'><tr><th>Item</th><th>Quantity</th><th>Unit</th></tr>";
        foreach (DataRow dr in dt.Rows)
        {
            dispatchItems += "<tr><td>" + dr["ITEMNAME"].ToString() + "</td><td>" + dr["QUANTITY"].ToString() + "</td><td>" + dr["UNIT"].ToString() + "</td><td>" + dr["NOTES"].ToString() + "</td></tr>";
        }
        dispatchItems += "</table>";

        MessageforSender += dispatchItems;
        MessageforSender += "<h3>NB: Please login to <a href='http://hrms.prangroup.com:8283/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://hrms.pranrflgroup.com:8283/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

        MessageApprover += dispatchItems;
        MessageApprover += "<h3>NB: Please login to <a href='http://hrms.prangroup.com:8283/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://hrms.pranrflgroup.com:8283/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";



        if (Approver.Count > 0)
        {
            SendEmail.SendMultipleMail(Approver, "New Dispatch for approval", MessageApprover);
        }
        SendEmail.SendSingleMail(fromuser, "New dispatch created for you", MessageforSender);

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
    public DataTable getIssuetypeDetails(int issuetypeid)
    {
        return dm.ExecuteQuery("Select * from DP_ISSUETYPE where ID=" + issuetypeid);
    }
    public DataTable getTransportDetails(int TransportID)
    {
        return dm.ExecuteQuery("Select * from DP_TRANSPORT where ID=" + TransportID);
    }
    public DataTable GetEmployeeDetails(string staffId)
    {
        return dm.ExecuteQuery(@"SELECT E.*,B.CENTERNAME,B.CENTERCODE,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,L.NAME,C.COUNTRYNAME,L.COUNTRYID from EM_EMPLOYEE E
         INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID 
         INNER JOIN BS_LOCATIONS L ON L.ID=B.LOCATIONID
         INNER JOIN BS_COUNTRY C ON  C.ID=L.COUNTRYID 
         INNER JOIN BS_DEPARTMENT D on D.ID=E.DEPARTMENTID 
         INNER JOIN BS_DESIGNATION DE on DE.ID=E.DESIGNATIONID where E.STAFFID='" + staffId + "'");
    }
    public long CheckDispatch(string UserID)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_DISPATCH where ISOPEN=1 AND CREATEDBY='" + UserID + "'");
        if (dt.Rows.Count == 0)
        {
            dm.ExecuteNonQuery(@"Insert into DP_DISPATCH(ID,ISOPEN,CREATEDBY) VALUES(SQ_DP_DISPATCH.NEXTVAL,1,'" + SessionManager.SessionName.StaffId + "')");
            dt = dm.ExecuteQuery("Select * from DP_DISPATCH where ISOPEN=1 AND CREATEDBY='" + UserID + "'");
        }
        return Convert.ToInt64(dt.Rows[0]["ID"]);
    }
    public void DeleteDispatchItems(long DispatchId)
    {
        dm.ExecuteNonQuery("DELETE FROM DP_DISPATCHITEMS WHERE DISPATCHID=" + DispatchId);
    }
    public DataTable GetDispatchItems(long dispatchid)
    {
        return dm.ExecuteQuery("Select * from DP_DISPATCHITEMS WHERE DISPATCHID=" + dispatchid);

    }
    public int InsertDispatchItems(long dispatchid, string ItemName, int ItemId, string Attachment, decimal quantity, string unit, string Remarks)
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_DISPATCHITEMS where DISPATCHID=" + dispatchid + " and ITEMNAME='" + ItemName + "'");
        if (dt.Rows.Count > 0)
        {
            return dm.ExecuteNonQuery("UPDATE DP_DISPATCHITEMS SET QUANTITY=QUANTITY+" + quantity + " WHERE ID=" + dt.Rows[0]["ID"]);
        }
        else
        {
            return dm.ExecuteNonQuery("INSERT INTO DP_DISPATCHITEMS(ID,ITEMID,QUANTITY,UNIT,ITEMNAME,DISPATCHID,ATTACHMENT,NOTES) Values(SQ_DP_DISPATCHITEMS.NEXTVAL," + ItemId + "," + quantity + ",'" + unit + "','" + ItemName + "'," + dispatchid + ",'" + Attachment + "','" + Remarks + "')");
        }

    }
    public int UpdateDispatchItems(long Id, decimal quantity, string unit, string remarks)
    {
        return dm.ExecuteNonQuery("UPDATE DP_DISPATCHITEMS SET QUANTITY='" + quantity + "',UNIT='" + unit + "', NOTES='" + remarks + "' WHERE ID=" + Id);
    }
    public DataTable GetDispatchDetails(long id)
    {
        return dm.ExecuteQuery(@"Select D.*,B.CENTERCODE,B.ADDRESS SENDERADDRESS,B.CENTERNAME,E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,TB.CENTERCODE||'-'||TB.ADDRESS ADDRESS,BD.DEPARTMENTCODE,
        BD.DEPARTMENTNAME,DE.DESIGNATIONNAME ,I.ISSUETYPENAME FROM DP_DISPATCH D 
        INNER JOIN DP_ISSUETYPE I ON I.ID = D.ISSUETYPEID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=D.FROMUSER
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID 
        INNER JOIN BS_DEPARTMENT BD ON BD.ID=E.DEPARTMENTID
        INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID 
        LEFT OUTER JOIN BS_BUSINESSCENTER TB ON TB.ID=D.RECBUSINESSCENTERID 
        WHERE D.ID=" + id);
    }
    public void UpdatePrint(long id)
    {
        dm.ExecuteNonQuery(@"Update DP_DISPATCH set NOOFPRINT=NOOFPRINT+1 where ID=" + id);
    }
    public DataTable GetTransitDetails(long id)
    {
        return dm.ExecuteQuery(@"Select * from DP_TRANSITS T INNER JOIN EM_EMPLOYEE E ON E.USERID=T.UPDATEDBY
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID INNER JOIN BS_DEPARTMENT BD ON BD.ID=E.DEPARTMENTID
        INNER JOIN BS_DESIGNATION DE ON DE.ID=E.DESIGNATIONID WHERE T.DISPATCHID=" + id + " ORDER BY T.ID ASC");
    }
    public int UpdateTransitRemarks(long id, string Remarks)
    {
        return dm.ExecuteNonQuery(@"Update DP_TRANSITS set REMARKS='" + Remarks + "',UPDATEDATE= SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
    }
    public DataTable GateCheckList()
    {
        return dm.ExecuteQuery(@"Select D.ID,  D.CREATEDATE, D.TRANSACTIONID,SE.EMPLOYEENAME,D.RECEIVERNAME  from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID 
        INNER JOIN EM_EMPLOYEE SE ON SE.STAFFID=D.FROMUSER INNER JOIN EM_EMPLOYEE E ON E.STAFFID=D.CREATEDBY 
        where DT.ISACTIVE=1 and D.STATUSID IN(2,3,6,7) and D.ISOPEN=0 and D.GATEPASS=0 AND E.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "' AND TO_DATE(D.CREATEDATE,'DD/MM/YY') BETWEEN (SELECT TO_DATE(SYSDATE-5,'DD/MM/YY') FROM DUAL) AND (SELECT TO_DATE(SYSDATE,'DD/MM/YY') FROM DUAL)");
    }

        //    @" UNION
        //SELECT G.ID,G.INDATETIME CREATEDATE, G.GATEENTRYID TRANSACTIONID,E.EMPLOYEENAME RECEIVERNAME,G.VISTORNAME EMPLOYEENAME FROM DP_GATEENTRY G INNER JOIN EM_EMPLOYEE E ON E.STAFFID=G.STAFFID
        //WHERE G.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "' AND G.GATEPASSED=0 AND (SELECT COUNT(GI.ID) FROM DP_GATEENTRYITEMS GI,DP_GATEENTRY G WHERE GI.GATEENTRYID=G.ID AND ISRETURNABLE=1)>0 " + "ORDER BY TRANSACTIONID DESC

    //public DataTable GateCheckList()
    //{
    //    return dm.ExecuteQuery(@"Select D.ID, D.TRANSACTIONID,SE.EMPLOYEENAME,D.RECEIVERNAME  from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID 
    //    INNER JOIN EM_EMPLOYEE SE ON SE.STAFFID=D.FROMUSER INNER JOIN EM_EMPLOYEE E ON E.STAFFID=D.CREATEDBY 
    //    where DT.ISACTIVE=1 and D.STATUSID IN(2,3,6,7) and D.ISOPEN=0 and D.GATEPASS=0 AND TO_CHAR(D.CREATEDATE, 'DD/MM/YYYY') = TO_CHAR(SYSDATE, 'DD/MM/YYYY') AND E.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "' AND ROWNUM <= 500" +
    //    @" UNION
    //    SELECT G.ID,G.GATEENTRYID TRANSACTIONID,E.EMPLOYEENAME RECEIVERNAME,G.VISTORNAME EMPLOYEENAME FROM DP_GATEENTRY G INNER JOIN EM_EMPLOYEE E ON E.STAFFID=G.STAFFID
    //    WHERE G.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "' AND TO_CHAR(G.INDATETIME, 'DD/MM/YYYY') = TO_CHAR(SYSDATE, 'DD/MM/YYYY') AND G.GATEPASSED=0 AND (SELECT COUNT(ID) FROM DP_GATEENTRYITEMS WHERE GATEENTRYID=G.ID AND ISRETURNABLE=1)>0");
    //}

    public DataTable GateCheck(string TransactionID)
    {
        return dm.ExecuteQuery(@"Select  D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D left JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
        where D.STATUSID>1 AND D.STATUSID<14 and D.ISOPEN=0  and TRANSACTIONID='" + TransactionID + "' AND ROWNUM <= 500 ORDER BY D.ID DESC");
    }

    //public DataTable GateCheck(string TransactionID)
    //{
    //    return dm.ExecuteQuery(@"Select  D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D left JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
    //    INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
    //    where D.STATUSID>1 AND D.STATUSID<14 AND D.GATEPASS=0 AND D.ISOPEN=0  and TRANSACTIONID='" + TransactionID + "' AND ROWNUM <= 500 ORDER BY D.ID DESC");
    //}
    public DataTable GetTransportDetails(long id)
    {
        return dm.ExecuteQuery("SELECT * FROM DP_TRANSPORTDETAILS T INNER JOIN DP_TRANSPORT TR ON TR.ID=T.TRANSPORTID INNER JOIN ASSET_CURRENCY C ON C.ID=T.CURRENCYID where T.DISPATCHID=" + id);
    }

    public string GetGatemen()
    {
        DataTable dt = dm.ExecuteQuery("Select GATENO from DP_ASSIGNGATEMAN where STAFFID='" + SessionManager.SessionName.StaffId + "'");
        string a = "1";
        if (dt.Rows.Count > 0)
        {
            a = dt.Rows[0][0].ToString();
        }
        return a;
    }
    public int InsertGatepass(long id, int GateNo)
    {
        dm.ExecuteNonQuery(@"Update DP_DISPATCH set GATEPASS=1 where ID=" + id);
        DataTable dt = GetEmployeeDetails(SessionManager.SessionName.StaffId);
        string Step = dt.Rows[0]["CENTERCODE"].ToString() + "-" + "Gate#" + GateNo.ToString();
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP,RELEASEDATE,REMARKS,UPDATEDATE,ISACTIVE,UPDATEDBY) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + Step + "',SYSDATE,'Gate Checked',SYSDATE,0,'" + SessionManager.SessionName.StaffId + "')");
        return dm.ExecuteNonQuery(@"Insert into DP_GATECHECKING(ID,STAFFID,DISPATCHID,GATENO)
        VALUES(SQ_DP_GATECHECKING.NEXTVAL,'" + SessionManager.SessionName.StaffId + "','" + id + "','" + GateNo + "')");
    }
    public void InsertReceive(long id, string Step)
    {

        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP,RELEASEDATE,REMARKS,UPDATEDATE,ISACTIVE,UPDATEDBY) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + Step + "',SYSDATE,'Received',SYSDATE,0,'" + SessionManager.SessionName.StaffId + "')");
    }
    public DataTable GetTrackingList()
    {
        return dm.ExecuteQuery(@"Select D.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,DE.DEPARTMENTCODE from DP_DISPATCH D INNER JOIN EM_EMPLOYEE E ON E.STAFFID=D.FROMUSER
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID INNER JOIN BS_DEPARTMENT DE ON DE.ID=E.DEPARTMENTID INNER JOIN DP_ISSUETYPE IT
        ON IT.ID=D.ISSUETYPEID WHERE D.ISOPEN=0 AND D.COMPLETED=0 AND IT.ISPERSONAL=0 AND
        (D.FROMUSER='" + SessionManager.SessionName.StaffId + "' OR D.TOUSER='" + SessionManager.SessionName.StaffId + "')");
    }
    public DataTable GetTrackingDetails(string DispatchId)
    {
        return dm.ExecuteQuery(@"Select D.*,IT.*,E.EMPLOYEENAME,B.CENTERCODE from DP_DISPATCH D INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=D.FROMUSER INNER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID
        WHERE D.ISOPEN=0 AND IT.ISPERSONAL=0 AND D.TRANSACTIONID='" + DispatchId + "'");
    }
    public long GetDispatchIdfromTransactionId(string TransactionId)
    {
        DataTable dt = dm.ExecuteQuery("Select ID from DP_DISPATCH where TRANSACTIONID='" + TransactionId + "'");
        if (dt.Rows.Count > 0)
        {
            return Convert.ToInt64(dt.Rows[0][0]);
        }
        else
            return 0;
    }

    public DataTable TrakckingDetails(string TransactionId)
    {
        return dm.ExecuteQuery(@"Select T.*,D.SENDNAME,B.CENTERCODE,DP.DEPARTMENTCODE from DP_DISPATCH D INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID INNER JOIN BS_BUSINESSCENTER B ON B.ID=D.SENDBUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT DP ON DP.ID=D.SENDDEPARTMENTID INNER JOIN DP_TRANSITS T ON T.DISPATCHID=D.ID WHERE T.DISPATCHID='" + GetDispatchIdfromTransactionId(TransactionId) + "' ORDER BY T.ID ASC");
    }

    public DataTable DispatchItemListBySender(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT D.TRANSACTIONID,D.CREATEDATE,D.FROMUSER,D.TOUSER ,BC.CENTERNAME,DP.DEPARTMENTNAME,DI.ITEMNAME,DI.QUANTITY,DI.UNIT FROM DP_DISPATCH D
        INNER JOIN BS_BUSINESSCENTER BC ON BC.ID = D.SENDBUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT DP ON DP.ID = D.SENDDEPARTMENTID
        INNER JOIN DP_DISPATCHITEMS DI ON DI.DISPATCHID = D.ID
        WHERE D.ID>0 " + critaria);
    }
    public DataTable DispatchItemListByReceiver(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT D.TRANSACTIONID,D.CREATEDATE,D.FROMUSER,D.TOUSER ,BC.CENTERNAME,DP.DEPARTMENTNAME,DI.ITEMNAME,DI.QUANTITY,DI.UNIT FROM DP_DISPATCH D
        INNER JOIN BS_BUSINESSCENTER BC ON BC.ID = D.SENDBUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT DP ON DP.ID = D.SENDDEPARTMENTID
        INNER JOIN DP_DISPATCHITEMS DI ON DI.DISPATCHID = D.ID
        WHERE D.ID>0" + critaria);
    }
    public DataTable DispatchMailDetails(long dispatchid)
    {
        return dm.ExecuteQuery(@"Select D.*,FE.EMPLOYEENAME,FE.OFFICEMAIL,FE.OFFICEMOBILE,FE.OFFICEPHONE,TE.EMPLOYEENAME TOEMPLOYEE,TE.OFFICEMAIL TOMAIL,TE.OFFICEMOBILE TOMOBILE,
        TE.OFFICEPHONE TOPHONE,DI.ITEMNAME,DI.QUANTITY,DI.UNIT,DI.NOTES
        from DP_DISPATCH D INNER JOIN EM_EMPLOYEE FE ON FE.STAFFID=D.FROMUSER
        LEFT OUTER JOIN EM_EMPLOYEE TE ON TE.STAFFID=D.TOUSER
        INNER JOIN DP_DISPATCHITEMS DI ON DI.DISPATCHID=D.ID WHERE D.ID=" + dispatchid);
    }
    public void SendUpdateMail(long dispatchId, string step)
    {
        DataTable dt = DispatchMailDetails(dispatchId);
        if (dt.Rows.Count > 0)
        {
            string fromuser = dt.Rows[0]["OFFICEMAIL"].ToString();
            string touser = dt.Rows[0]["TOMAIL"].ToString();
            string MessageforSender = "<h3>Your dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " is in : " + step + "</h3>";
            MessageforSender += "<b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
            MessageforSender += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
            MessageforSender += "<h4>Item Details : </4>";

            string MessageForReceiver = "<h3>Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " sent to you is in : " + step + "</h3>";
            MessageForReceiver += "<b>Sendar : </b>" + dt.Rows[0]["EMPLOYEENAME"].ToString();
            MessageForReceiver += "<br><b>Mobile : </b>" + dt.Rows[0]["OFFICEMOBILE"].ToString();
            MessageForReceiver += "<h4>Item Details : </4>";

            string dispatchItems = "<table  border='1' cellspacing='0' cellpadding='0' width='400'><tr><th>Item</th><th>Quantity</th><th>Unit</th></tr>";
            foreach (DataRow dr in dt.Rows)
            {
                dispatchItems += "<tr><td>" + dr["ITEMNAME"].ToString() + "</td><td>" + dr["QUANTITY"].ToString() + "</td><td>" + dr["UNIT"].ToString() + "</td><td>" + dr["NOTES"].ToString() + "</td></tr>";
            }
            dispatchItems += "</table>";

            MessageforSender += dispatchItems;
            MessageforSender += "<h3>NB: Please login to <a href='http://hrms.prangroup.com:8283/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://hrms.pranrflgroup.com:8283/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

            MessageForReceiver += dispatchItems;
            MessageForReceiver += "<h3>NB: Please login to <a href='http://hrms.prangroup.com:8283/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://hrms.pranrflgroup.com:8283/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

            if (!string.IsNullOrEmpty(touser))
            {
                SendEmail.SendSingleMail(touser, "Dispatch for you", MessageForReceiver);
            }
            SendEmail.SendSingleMail(fromuser, "Your dispatch status updated", MessageforSender);
        }
    }
    
}