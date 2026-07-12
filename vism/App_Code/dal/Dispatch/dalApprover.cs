using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public partial class dalApprover
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalApprover()
    {

    }
    public int Insert(int companyId, string staffId, int priority, int centerid, int departmentid, int IsFinalApprover)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_APPROVER(ID,COMPANYID,APPROVERID,PRIORITY,BUSINESSCENTERID,DEPARTMENTID,ISFINALAPPROVER,CREATEDBY)
        VALUES(SQ_DP_APPROVER.NEXTVAL,'" + companyId + "','" + staffId + "','" + priority + "','" + centerid + "'," + departmentid + "," + IsFinalApprover + ",'" + SessionManager.SessionName.StaffId + "')");
    }
    
    public int Update(int id, int companyId, string staffId, int priority, int centerid, int departmentid, int IsFinalApprover)
    {
        return dm.ExecuteNonQuery("update DP_APPROVER set COMPANYID='" + companyId + "', APPROVERID='" + staffId + "',PRIORITY='" + priority + "',BUSINESSCENTERID='" + centerid + "',DEPARTMENTID=" + departmentid + ", ISFINALAPPROVER=" + IsFinalApprover + ",UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE where ID=" + id);
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DE.DEPARTMENTNAME,DS.DESIGNATIONNAME FROM DP_APPROVER A  INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID INNER JOIN BS_DESIGNATION DS
ON DS.ID=E.DESIGNATIONID where A.ID=" + id);
    }

    public DataTable GetAllApproverList(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.STAFFID, E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DE.DEPARTMENTNAME,
        DE.DEPARTMENTCODE,DS.DESIGNATIONNAME FROM DP_APPROVER A 
         INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
         INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID
         INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID 
         INNER JOIN BS_DESIGNATION DS ON DS.ID=E.DESIGNATIONID where" + critaria);
    }

    public DataTable GetAllApproverList()
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.STAFFID, E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DE.DEPARTMENTNAME,
        DE.DEPARTMENTCODE,DS.DESIGNATIONNAME FROM DP_APPROVER A 
         INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
         INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID
         INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID 
         INNER JOIN BS_DESIGNATION DS ON DS.ID=E.DESIGNATIONID ");
    }
    public DataTable GetAll(int businessCenterId)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,CO.COMPANYNAME,DE.DEPARTMENTNAME,DE.DEPARTMENTCODE,DS.DESIGNATIONNAME FROM DP_APPROVER A  INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
INNER JOIN BS_COMPANY CO ON A.COMPANYID=CO.ID INNER JOIN BS_DEPARTMENT DE ON DE.ID=A.DEPARTMENTID INNER JOIN EM_EMPLOYEE E ON E.USERID=A.APPROVERID INNER JOIN BS_DESIGNATION DS
ON DS.ID=E.DESIGNATIONID where A.BUSINESSCENTERID=" + businessCenterId);
    }
    public List<string> GetByCreatedby(string vid)
    {
        List<string> list = new List<string>();
        DataTable dt = dm.ExecuteQuery(@"SELECT CS.EMAIL,CS.CC1,CS.CC2 FROM RVS_PREINSPECTION PR
INNER JOIN RVS_WORKSHOP WS ON PR.WORKSHOPID=WS.ID
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN RVS_CUSTOMER CS ON VH.CUSTOMERID=CS.ID
WHERE VH.ID='" + vid + "'");
        foreach (DataRow dr in dt.Rows)
        {
            string mail1 = dt.Rows[0]["EMAIL"].ToString();
            string mail2 = dt.Rows[0]["CC1"].ToString();
            string mail3 = dt.Rows[0]["CC2"].ToString();
            if (mail1 != "")
            {
                list.Add(dr["EMAIL"].ToString());
            }
            if (mail2 != "")
            {
                list.Add(dr["CC1"].ToString());
            }
            if (mail3 != "")
            {
                list.Add(dr["CC2"].ToString());
            }

        }
        return list;
    }

    public List<string> GetByCreatedbyJC(string vid, int ID)
    {
        List<string> list = new List<string>();
        DataTable dt = dm.ExecuteQuery(@"SELECT CS.EMAIL,CS.CC1,CS.CC2 FROM RVS_PREINSPECTION PR
INNER JOIN RVS_WORKSHOP WS ON PR.WORKSHOPID=WS.ID
INNER JOIN RVS_VEHICLE VH ON PR.VEHICLEID=VH.ID
INNER JOIN RVS_CUSTOMER CS ON VH.CUSTOMERID=CS.ID
WHERE VH.ID='" + vid + "' and PR.ID=(SELECT PIID FROM RVS_JOBCARD WHERE ID='" + ID + "')");
        foreach (DataRow dr in dt.Rows)
        {
            string mail1 = dt.Rows[0]["EMAIL"].ToString();
            string mail2 = dt.Rows[0]["CC1"].ToString();
            string mail3 = dt.Rows[0]["CC2"].ToString();
            if (mail1 != "")
            {
                list.Add(dr["EMAIL"].ToString());
            }
            if (mail2 != "")
            {
                list.Add(dr["CC1"].ToString());
            }
            if (mail3 != "")
            {
                list.Add(dr["CC2"].ToString());
            }

        }
        return list;
    }

    public string DispatchStatus(long DispatchId)
    {
        string Status = "";
        try
        {
            DataTable dt = dm.ExecuteQuery(@"Select * from DP_DISPATCH D INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID INNER JOIN BS_BUSINESSCENTER B ON B.ID=D.SENDBUSINESSCENTERID
INNER JOIN BS_DEPARTMENT DP ON DP.ID=D.SENDDEPARTMENTID WHERE D.ID=" + DispatchId);
            int Statusid = Convert.ToInt32(dt.Rows[0]["STATUSID"]);
            int DirectSend = Convert.ToInt32(dt.Rows[0]["DIRECTSEND"]);
            int DirectReturn = Convert.ToInt32(dt.Rows[0]["DIRECTRETURN"]);
            int isPersonal = Convert.ToInt32(dt.Rows[0]["ISPERSONAL"]);
            int isExternal = Convert.ToInt32(dt.Rows[0]["ISEXTERNAL"]);
            int isReturnable = Convert.ToInt32(dt.Rows[0]["RETURNABLE"]);
            if (Statusid == 0)
            {
                Status = "Approved";
            }
            else if (Statusid == 1)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                if (isExternal == 1)
                {
                    if (DirectSend == 1)
                    {
                        Status = "External Direct Sent";
                    }
                    else
                    {
                        Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + "Dispatch";
                    }
                }
                else
                {
                    if (DirectSend == 1)
                    {
                        Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERDEPARTMENT"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
                    }
                    else
                    {
                        Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + "Dispatch";
                    }

                }
            }
            else if (Statusid == 2)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                else
                {
                    if (isReturnable == 1)
                    {
                        if (DirectSend == 1)
                        {
                            Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERDEPARTMENT"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString() + " Received";
                        }
                        else
                        {
                            Status = "Transport";
                        }
                    }
                    else
                    {
                        if (DirectSend == 1)
                        {
                            Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERDEPARTMENT"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString() + " Received";
                        }
                        else
                        {
                            Status = "Transport";
                        }
                    }
                }

            }
            else if (Statusid == 3)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                if (isExternal == 1)
                {
                    Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + "Dispatch Received";
                }
                else
                {
                    if (DirectSend == 1)
                    {
                        Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + dt.Rows[0]["DEPARTMENTCODE"].ToString() + "-" + dt.Rows[0]["SENDNAME"].ToString();
                    }
                    else
                    {
                        Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + "Dispatch";
                    }

                }
            }
            else if (Statusid == 4)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                if (isExternal == 1)
                {
                    Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + dt.Rows[0]["DEPARTMENTCODE"].ToString() + "-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                else
                {
                    if (DirectSend == 1)
                    {
                        Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + dt.Rows[0]["DEPARTMENTCODE"].ToString() + "-" + dt.Rows[0]["SENDNAME"].ToString() + "-Returned";
                    }
                    else
                    {
                        Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERDEPARTMENT"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
                    }

                }
            }
            else if (Statusid == 5)
            {
                Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-Returning Dispatch";
            }
            else if (Statusid == 6)
            {
                Status = "Returning Transport";
            }
            else if (Statusid == 7)
            {
                Status = dt.Rows[0]["CENTERCODE"].ToString() + "- Returned to Dispatch";
            }
            else if (Statusid == 8)
            {
                Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + dt.Rows[0]["DEPARTMENTCODE"].ToString() + "-" + dt.Rows[0]["SENDNAME"].ToString();
            }
            else
            {
                Status = "Approved";
            }
            new dalDispatch().SendUpdateMail(DispatchId, Status);
        }
        catch (Exception)
        {

            Status = "";
        }

        return Status;
    }
    public string DirectSendStatus(long DispatchId, int StatusId)
    {
        string Status = "";
        try
        {
            DataTable dt = dm.ExecuteQuery(@"Select * from DP_DISPATCH D INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID INNER JOIN BS_BUSINESSCENTER B ON B.ID=D.SENDBUSINESSCENTERID
            INNER JOIN BS_DEPARTMENT DP ON DP.ID=D.SENDDEPARTMENTID WHERE D.ID=" + DispatchId);
            int Statusid = StatusId;
            int DirectSend = Convert.ToInt32(dt.Rows[0]["DIRECTSEND"]);
            int isPersonal = Convert.ToInt32(dt.Rows[0]["ISPERSONAL"]);
            int isExternal = Convert.ToInt32(dt.Rows[0]["ISEXTERNAL"]);
            int isReturnable = Convert.ToInt32(dt.Rows[0]["RETURNABLE"]);
            if (Statusid == 0)
            {
                Status = "Approved";
            }
            else if (Statusid == 1)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                if (isExternal == 1)
                {
                    if (DirectSend == 1)
                    {
                        Status = "External Direct Sent";
                    }
                    else
                    {
                        Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + "Dispatch";
                    }
                }
                else
                {
                    if (DirectSend == 1)
                    {
                        Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERDEPARTMENT"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
                    }
                    else
                    {
                        Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + "Dispatch";
                    }

                }
            }
            else if (Statusid == 2)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                else
                {
                    if (isReturnable == 1)
                    {
                        if (DirectSend == 1)
                        {
                            Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERDEPARTMENT"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString() + "Received";
                        }
                        else
                        {
                            Status = "Transport";
                        }
                    }
                }

            }
            else if (Statusid == 3)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                if (isExternal == 1)
                {
                    Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + "Dispatch Received";
                }
                else
                {
                    if (DirectSend == 1)
                    {
                        Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + dt.Rows[0]["DEPARTMENTCODE"].ToString() + "-" + dt.Rows[0]["SENDNAME"].ToString();
                    }
                    else
                    {
                        Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + "Dispatch";
                    }

                }
            }
            else if (Statusid == 4)
            {
                if (isPersonal == 1)
                {
                    Status = "Personal-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                if (isExternal == 1)
                {
                    Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + dt.Rows[0]["DEPARTMENTCODE"].ToString() + "-" + dt.Rows[0]["SENDNAME"].ToString();
                }
                else
                {
                    Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERDEPARTMENT"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
                }
            }
            else if (Statusid == 5)
            {
                Status = dt.Rows[0]["BUSINESSCENTER"].ToString() + "-Returning Dispatch";
            }
            else if (Statusid == 6)
            {
                Status = "Returning Transport";
            }
            else if (Statusid == 7)
            {
                Status = dt.Rows[0]["CENTERCODE"].ToString() + "- Returned to Dispatch";
            }
            else if (Statusid == 8)
            {
                Status = dt.Rows[0]["CENTERCODE"].ToString() + "-" + dt.Rows[0]["DEPARTMENTCODE"].ToString() + "-" + dt.Rows[0]["SENDNAME"].ToString();
            }
            else
            {
                Status = "Approved";
            }
            new dalDispatch().SendUpdateMail(DispatchId, Status);

        }
        catch (Exception)
        {

            Status = "";
        } return Status;

    }

    public DataTable GetApprovedCount()
    {
        return dm.ExecuteQuery( @"Select COUNT (A.APPROVERID) NOTAPPROVE, A.APPROVERID from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY INNER JOIN DP_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
        Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID
        WHERE DT.ISACTIVE=1 and D.STATUSID=A.PRIORITY AND E.DEPARTMENTID=A.DEPARTMENTID AND A.COMPANYID=E.COMPANYID AND A.APPROVERID='"+SessionManager.SessionName.StaffId+"' GROUP BY A.APPROVERID");
    }
    public DataTable GetApprovalList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.*,IT.ISSUETYPENAME from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY INNER JOIN DP_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
        Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID
        where DT.ISACTIVE=1 and D.STATUSID=A.PRIORITY AND E.DEPARTMENTID=A.DEPARTMENTID AND A.COMPANYID=E.COMPANYID AND A.APPROVERID='" + SessionManager.SessionName.StaffId + "' ORDER BY D.UPDATEDATE DESC");
    }

    public DataTable GetApprovedList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.*,IT.ISSUETYPENAME from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY INNER JOIN DP_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
        Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER  INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID
        where D.STATUSID=2 AND E.DEPARTMENTID=A.DEPARTMENTID AND A.COMPANYID=E.COMPANYID AND A.APPROVERID='" + SessionManager.SessionName.StaffId + "' and dt.UPDATEDBY='" + SessionManager.SessionName.StaffId + "' AND TO_DATE(D.CREATEDATE,'DD/MM/YY') BETWEEN (SELECT TO_DATE(SYSDATE-3,'DD/MM/YY') FROM DUAL) AND (SELECT TO_DATE(SYSDATE,'DD/MM/YY') FROM DUAL) order by d.TRANSACTIONID desc");
    }

    public DataTable GetRejectedList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.*,IT.ISSUETYPENAME from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY INNER JOIN DP_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
        Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER  INNER JOIN DP_ISSUETYPE IT ON IT.ID=D.ISSUETYPEID
        where DT.ISACTIVE=1 and D.STATUSID=A.PRIORITY+98 AND E.DEPARTMENTID=A.DEPARTMENTID AND A.COMPANYID=E.COMPANYID AND A.APPROVERID='" + SessionManager.SessionName.StaffId + "' AND TO_DATE(D.CREATEDATE,'DD/MM/YY') BETWEEN (SELECT TO_DATE(SYSDATE-180,'DD/MM/YY') FROM DUAL) AND (SELECT TO_DATE(SYSDATE,'DD/MM/YY') FROM DUAL) order by d.TRANSACTIONID desc");
    }
    public int UpdateTransit(long id, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE= SYSDATE,REMARKS='" + Remarks + "',ISACTIVE=0,UPDATEDATE= SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + DispatchStatus(id) + "')");
        DataTable dt = dm.ExecuteQuery("Select STATUSID from DP_DISPATCH where ID=" + id);
        string GatePass = "";
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0][0]) == 5)
            {
                GatePass = ",GATEPASS=0";
            }
        }
        return dm.ExecuteNonQuery("update DP_DISPATCH set STATUSID=STATUSID+1 " + GatePass + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE=SYSDATE where ID=" + id);

    }
    public void ApproveDispatch(long id, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE= SYSDATE,REMARKS='" + Remarks + "',UPDATEDATE=SYSDATE,ISACTIVE=0,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + DispatchStatus(id) + "')");
        dm.ExecuteNonQuery("update DP_DISPATCH set STATUSID=2,UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= SYSDATE where ID=" + id);
        SendApprovedMail(id);

    }
    public string ApproveDispatchMail(long id, string StaffID)
    {
        string msg = "";
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE= SYSDATE,REMARKS='Approved From Mail',UPDATEDATE=SYSDATE,ISACTIVE=0,UPDATEDBY='" + StaffID + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + DispatchStatus(id) + "')");
        dm.ExecuteNonQuery("update DP_DISPATCH set STATUSID=2,UPDATEDBY='" + StaffID + "', UPDATEDATE= SYSDATE where ID=" + id);
        SendApprovedMail(id);

        return msg;
    }
    public long GetDispatchID(string tnsId, string staffid)
    {
        long id = 0;
        DataTable dt = dm.ExecuteQuery(@"Select D.ID from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY INNER JOIN DP_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
where DT.ISACTIVE=1  and D.STATUSID=1 AND E.DEPARTMENTID=A.DEPARTMENTID AND A.COMPANYID=E.COMPANYID  AND D.TRANSACTIONID='" + tnsId + "'  AND A.APPROVERID='" + staffid + "'");
        if (dt.Rows.Count == 1)
        {
            id = Convert.ToInt64(dt.Rows[0][0]);
        }
        return id;
    }
    public void SendApprovedMail(long dispatchId)
    {
        DataTable dt = new dalDispatch().DispatchMailDetails(dispatchId);
        string fromuser = dt.Rows[0]["OFFICEMAIL"].ToString();
        List<string> Approver = new dalApprover().GetByCreatedby(dt.Rows[0]["CREATEDBY"].ToString());
        string MessageforSender = "<h3>Your dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " approved by " + SessionManager.SessionName.StaffId + "</h3>";
        MessageforSender += "<b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageforSender += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageforSender += "<h4>Item Details : </4>";

        string MessageApprover = "<h3>Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " Approved  by " + SessionManager.SessionName.StaffId + "</h3>";
        MessageApprover += "<b>Sendar : </b>" + dt.Rows[0]["EMPLOYEENAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["OFFICEMOBILE"].ToString();
        MessageApprover += "<hr><b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageApprover += "<h4>Item Details : </4>";

        string dispatchItems = "<table border='1' cellspacing='0' cellpadding='0' width='400'><tr><th>Item</th><th>Quantity</th><th>Unit</th></tr>";
        foreach (DataRow dr in dt.Rows)
        {
            dispatchItems += "<tr><td>" + dr["ITEMNAME"].ToString() + "</td><td>" + dr["QUANTITY"].ToString() + "</td><td>" + dr["UNIT"].ToString() + "</td></tr>";
        }
        dispatchItems += "</table>";

        MessageforSender += dispatchItems;
        MessageforSender += "<h3>NB: Please login to <a href='http://apps.prangroup.com/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://apps.pranrflgroup.com/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

        MessageApprover += dispatchItems;
        MessageApprover += "<h3>NB: Please login to <a href='http://apps.prangroup.com/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://apps.pranrflgroup.com/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

        if (Approver.Count > 0)
        {
            SendEmail.SendMultipleMail(Approver, "Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " Approved", MessageApprover);
        }

        SendEmail.SendSingleMail(fromuser, "Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " Approved", MessageforSender);

    }
    public int NormalReturn(long id, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE=SYSDATE,REMARKS='" + Remarks + "',UPDATEDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set ISACTIVE=0 where DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + DirectSendStatus(id, 5) + "')");
        string GatePass = ",GATEPASS=0";
        return dm.ExecuteNonQuery("update DP_DISPATCH set DIRECTSEND=0, STATUSID=6 " + GatePass + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE=SYSDATE where ID=" + id);

    }
    public int DirectReturn(long id, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE=SYSDATE,REMARKS='" + Remarks + "',UPDATEDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set ISACTIVE=0 where DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + DirectSendStatus(id, 3) + "')");
        string GatePass = ",GATEPASS=0";
        return dm.ExecuteNonQuery("update DP_DISPATCH set DIRECTSEND=1, STATUSID=4 " + GatePass + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE=SYSDATE where ID=" + id);

    }
    public void SendRejectMail(long dispatchId)
    {
        DataTable dt = new dalDispatch().DispatchMailDetails(dispatchId);
        string fromuser = dt.Rows[0]["OFFICEMAIL"].ToString();
        List<string> Approver = new dalApprover().GetByCreatedby(dt.Rows[0]["CREATEDBY"].ToString());
        string MessageforSender = "<h3>Your dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " rejected by " + SessionManager.SessionName.StaffId + "</h3>";
        MessageforSender += "<b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageforSender += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageforSender += "<h4>Item Details : </4>";

        string MessageApprover = "<h3>Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " rejected  by " + SessionManager.SessionName.StaffId + "</h3>";
        MessageApprover += "<b>Sendar : </b>" + dt.Rows[0]["EMPLOYEENAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["OFFICEMOBILE"].ToString();
        MessageApprover += "<hr><b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageApprover += "<h4>Item Details : </4>";

        string dispatchItems = "<table border='1' cellspacing='0' cellpadding='0' width='400'><tr><th>Item</th><th>Quantity</th></tr>";
        foreach (DataRow dr in dt.Rows)
        {
            dispatchItems += "<tr><td>" + dr["ITEMNAME"].ToString() + "</td><td>" + dr["QUANTITY"].ToString() + "</td></tr>";
        }
        dispatchItems += "</table>";

        MessageforSender += dispatchItems;
        MessageforSender += "<h3>NB: Please login to <a href='http://apps.prangroup.com/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://apps.pranrflgroup.com/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

        MessageApprover += dispatchItems;
        MessageApprover += "<h3>NB: Please login to <a href='http://apps.prangroup.com/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://apps.pranrflgroup.com/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";


        if (Approver.Count > 0)
        {
            SendEmail.SendMultipleMail(Approver, "Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " rejected", MessageApprover);
        }
        SendEmail.SendSingleMail(fromuser, "Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " rejected", MessageforSender);

    }

    public void SendRejectGateMail(long dispatchId)
    {
        DataTable dt = new dalDispatch().DispatchMailDetails(dispatchId);
        string fromuser = dt.Rows[0]["OFFICEMAIL"].ToString();
        List<string> Approver = new dalApprover().GetByCreatedby(dt.Rows[0]["CREATEDBY"].ToString());
        string MessageforSender = "<h3>Your dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " rejected by " + SessionManager.SessionName.StaffId + "</h3>";
        MessageforSender += "<b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageforSender += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageforSender += "<h4>Item Details : </4>";

        string MessageApprover = "<h3>Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + "Gate rejected  by " + SessionManager.SessionName.StaffId + "</h3>";
        MessageApprover += "<b>Sendar : </b>" + dt.Rows[0]["EMPLOYEENAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["OFFICEMOBILE"].ToString();
        MessageApprover += "<hr><b>Receiver : </b>" + dt.Rows[0]["BUSINESSCENTER"].ToString() + "-" + dt.Rows[0]["RECEIVERNAME"].ToString();
        MessageApprover += "<br><b>Mobile : </b>" + dt.Rows[0]["RECEIVERMOBILE"].ToString();
        MessageApprover += "<h4>Item Details : </4>";

        string dispatchItems = "<table border='1' cellspacing='0' cellpadding='0' width='400'><tr><th>Item</th><th>Quantity</th></tr>";
        foreach (DataRow dr in dt.Rows)
        {
            dispatchItems += "<tr><td>" + dr["ITEMNAME"].ToString() + "</td><td>" + dr["QUANTITY"].ToString() + "</td></tr>";
        }
        dispatchItems += "</table>";

        MessageforSender += dispatchItems;
        MessageforSender += "<h3>NB: Please login to <a href='http://apps.prangroup.com/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://apps.pranrflgroup.com/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

        MessageApprover += dispatchItems;
        MessageApprover += "<h3>NB: Please login to <a href='http://apps.prangroup.com/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://apps.pranrflgroup.com/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";


        if (Approver.Count > 0)
        {
            SendEmail.SendMultipleMail(Approver, "Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " rejected", MessageApprover);
        }
        SendEmail.SendSingleMail(fromuser, "Dispatch-" + dt.Rows[0]["TRANSACTIONID"].ToString() + " rejected", MessageforSender);

    }
    public int RejectDispatch(long id, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE=SYSDATE,REMARKS='" + Remarks + "',UPDATEDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set ISACTIVE=0 where DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'Rejected')");

        dm.ExecuteNonQuery("update DP_DISPATCH set STATUSID=STATUSID+98,UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE=SYSDATE where ID=" + id);
        SendRejectMail(id);
        return 1;
    }

    public int RejectDispatchGate(long id, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE=SYSDATE,REMARKS='" + Remarks + "',UPDATEDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set ISACTIVE=0 where DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'Gate Rejected')");

        dm.ExecuteNonQuery("update DP_DISPATCH set STATUSID=STATUSID+98,UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE=SYSDATE where ID=" + id);
        //SendRejectGateMail(id);
        return 1;
    }
    public int ApproveRejected(long id, string Remarks)
    {
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set RELEASEDATE= SYSDATE,REMARKS='" + Remarks + "',UPDATEDATE= SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "' where ISACTIVE=1 and DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Update DP_TRANSITS set ISACTIVE=0 where DISPATCHID=" + id);
        dm.ExecuteNonQuery(@"Insert into DP_TRANSITS(ID,DISPATCHID,STEP) VALUES(SQ_DP_TRANSITS.NEXTVAL," + id + ",'" + DispatchStatus(id) + "')");
        dm.ExecuteNonQuery("update DP_DISPATCH set STATUSID=STATUSID-97,UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= SYSDATE where ID=" + id);
        SendApprovedMail(id);
        return 1;
    }
    public DataTable GetDispatchSendingList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
where DT.ISACTIVE=1 and D.STATUSID IN(2,6) AND I.ISPERSONAL=0 and D.DIRECTSEND=0 and E.BUSINESSCENTERID=" + SessionManager.SessionName.BusinessCentetid);
    }
    public DataTable GetDispatchReceiveList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.TOUSER Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER 
where DT.ISACTIVE=1 and D.STATUSID IN (3,7) and D.DIRECTSEND=0 and D.TOUSER>0 and E.BUSINESSCENTERID=" + SessionManager.SessionName.BusinessCentetid + "" +
@"union
Select D.*, DT.*, E.EMPLOYEENAME CREATEDUSER, SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID = D.ID
left outer JOIN EM_EMPLOYEE E ON E.USERID = D.TOUSER Inner join EM_EMPLOYEE SE ON SE.USERID = D.FROMUSER
where DT.ISACTIVE = 1 and D.STATUSID IN(3, 7) and D.DIRECTSEND = 0 and D.TOUSER = 0 and SE.BUSINESSCENTERID = " + SessionManager.SessionName.BusinessCentetid);
    }
    public DataTable GetGetPassList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
where DT.ISACTIVE=1 and D.STATUSID=2 AND I.ISPERSONAL=1 and E.BUSINESSCENTERID=" + SessionManager.SessionName.BusinessCentetid);
    }
    public DataTable GetReceiveList()
    {
        //        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
        //INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID" +
        //@" where (DT.ISACTIVE=1 and D.STATUSID IN(4,8) AND D.DIRECTSEND=0 AND I.ISPERSONAL=0 AND D.TOUSER='" + SessionManager.SessionName.StaffId + "')" +
        //" OR (DT.ISACTIVE=1 and D.STATUSID IN(2,4) AND I.ISPERSONAL=0 AND D.DIRECTSEND=1 AND D.TOUSER='" + SessionManager.SessionName.StaffId + "') " +
        //" OR (DT.ISACTIVE=1 and D.STATUSID=4 AND I.ISEXTERNAL =1 AND D.DIRECTSEND=0 AND D.FROMUSER ='" + SessionManager.SessionName.StaffId + "')" +
        //" OR (DT.ISACTIVE=1 and D.STATUSID=2 AND I.ISEXTERNAL =1 AND D.DIRECTSEND=1 AND D.FROMUSER ='" + SessionManager.SessionName.StaffId + "')");
        string qry = @"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D
 INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID = D.ID
INNER JOIN EM_EMPLOYEE E ON E.USERID = D.CREATEDBY
 Inner join EM_EMPLOYEE SE ON SE.USERID = D.FROMUSER
  INNER JOIN DP_ISSUETYPE I ON I.ID = D.ISSUETYPEID
  where DT.ISACTIVE = 1 and D.STATUSID IN(2,3, 4, 8)
  AND I.ISPERSONAL = 0 AND D.TOUSER = '" + SessionManager.SessionName.StaffId + "'";
        return dm.ExecuteQuery(qry);
//        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D
// INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID = D.ID
//INNER JOIN EM_EMPLOYEE E ON E.USERID = D.CREATEDBY
// Inner join EM_EMPLOYEE SE ON SE.USERID = D.FROMUSER
//  INNER JOIN DP_ISSUETYPE I ON I.ID = D.ISSUETYPEID
//  where DT.ISACTIVE = 1 and D.STATUSID IN(2, 4, 8)
//  AND I.ISPERSONAL = 0 AND D.TOUSER = '"+ SessionManager.SessionName.StaffId + "'");
    }
    public DataTable GetRejectList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
where DT.ISACTIVE=1 and D.STATUSID IN(99,100) AND D.TOUSER='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable WaitingApprovalList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
where DT.ISACTIVE=1 and D.STATUSID<2 AND D.TOUSER='" + SessionManager.SessionName.StaffId + "'");
    }

    public DataTable NewWaitingApprovalList()
    {
        return dm.ExecuteQuery(@"Select D.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D 
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
where D.STATUSID=1 AND D.GATEPASS>=0 AND (D.CREATEDBY='" + SessionManager.SessionName.StaffId + "' OR D.FROMUSER='" + SessionManager.SessionName.StaffId + "') ORDER BY D.CREATEDATE DESC ");
    }

    public DataTable ApprovedList()
    {
        return dm.ExecuteQuery(@"Select D.*,E.EMPLOYEENAME CREATEDUSER,SE.EMPLOYEENAME from DP_DISPATCH D 
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
where D.STATUSID>1 AND D.GATEPASS>=0 AND (D.CREATEDBY='" + SessionManager.SessionName.StaffId + "' OR D.FROMUSER='" + SessionManager.SessionName.StaffId + "') ORDER BY D.UPDATEDATE DESC ");
    }
    public DataTable GetReturnList()
    {
        return dm.ExecuteQuery(@"Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
            INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
            where DT.ISACTIVE=1 and D.STATUSID=5 AND D.DIRECTSEND=0 AND I.ISPERSONAL=0 AND I.RETURNABLE=1 AND D.TOUSER='" + SessionManager.SessionName.StaffId + "'" +
            @" UNION
            Select D.*,DT.*,E.EMPLOYEENAME CREATEDUSER,SE.* from DP_DISPATCH D INNER JOIN DP_TRANSITS DT ON DT.DISPATCHID=D.ID
            INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY Inner join EM_EMPLOYEE SE ON SE.USERID=D.FROMUSER INNER JOIN DP_ISSUETYPE I ON I.ID=D.ISSUETYPEID
            where DT.ISACTIVE=1 and D.STATUSID=3 AND I.ISPERSONAL=0 AND I.RETURNABLE=1 AND D.DIRECTSEND=1 AND D.TOUSER='" + SessionManager.SessionName.StaffId + "'");
    }
    public int InsertTransportDetails(long id, decimal cost, int transportid, int currencyid, string driver, string mobile, string deliveryman)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_TRANSPORTDETAILS(ID,DISPATCHID,TRANSPORTID,DRIVER,DELIVERYMAN,MOBILE,CURRENCYID,COST) VALUES(SQ_DP_TRANSPORTDETAILS.NEXTVAL," + id + ",'" + transportid + "','" + driver + "','" + deliveryman + "','" + mobile + "','" + currencyid + "'," + cost + ")");
    }
}