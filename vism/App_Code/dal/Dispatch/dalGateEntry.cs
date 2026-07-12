using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalGateEntry
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalGateEntry()
    {
    }
    public int Update(long id, string Staffid, int BusinessCenterId, int DepartmentId, string ScheduleTime, string VisitorName, string visitorAddress, string visitorMobile, int GateNo, string VisitorCardNo, string visitorStaffId, string Attachment)
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
        //if (SessionManager.SessionName.BusinessCentetid <= 100)
        //{
        //    office = "00" + SessionManager.SessionName.BusinessCentetid.ToString();
        //}
        //else if (SessionManager.SessionName.BusinessCentetid <= 10)
        //{
        //    office = "0" + SessionManager.SessionName.BusinessCentetid.ToString();
        //}
        //else
        //{
        //    office = SessionManager.SessionName.BusinessCentetid.ToString();
        //}

        string TransID = "R" + office + DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("d2") + DateTime.Now.Day.ToString("00");
        string TransIdQuery = "(Select '" + TransID + "'||TRIM(TO_CHAR((NVL(MAX(TO_NUMBER(SUBSTR(GATEENTRYID,11,6))),0)+1),'000000')) ID from DP_GATEENTRY where TRIM(TO_CHAR(NVL(SUBSTR(GATEENTRYID,2,3),0),'000'))='" + office + "' and TRIM(TO_CHAR(NVL(SUBSTR(GATEENTRYID,5,2),0),'00'))='" + DateTime.Now.ToString("yy") + "'  and TRIM(TO_CHAR(NVL(SUBSTR(GATEENTRYID,7,2),0),'00'))='" + DateTime.Now.Month.ToString("d2") + "' AND TRIM(TO_CHAR(NVL(SUBSTR(GATEENTRYID,9,2),0),'00'))='" + DateTime.Now.Day.ToString("00") + "')";
        return dm.ExecuteNonQuery(@"update DP_GATEENTRY set STAFFID='" + Staffid + "',BUSINESSCENTERID='" + BusinessCenterId + "',DEPARTMENTID='" + DepartmentId + "',SCHEDULETIME='" + ScheduleTime + "',VISITORSTAFFID='" + visitorStaffId + "',VISTORNAME='" + VisitorName + "',VISITORADDRESS='" + visitorAddress + "',VISTORMOBILE='" + visitorMobile + "',GATENO='" + GateNo + "',VISTORCARDNO='" + VisitorCardNo + "', ISOPEN=0,UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE,INDATETIME=SYSDATE,ATTACHMENTS='" + Attachment + "', GATEENTRYID=" + TransIdQuery + " where ID=" + id);

    }
    public DataTable GetEmployeeDetails(string staffId)
    {
        return dm.ExecuteQuery(@"Select E.*,B.CENTERNAME,B.CENTERCODE,D.DEPARTMENTCODE,DE.DESIGNATIONNAME,L.NAME,C.COUNTRYNAME,L.COUNTRYID from EM_EMPLOYEE E inner join BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID INNER JOIN BS_LOCATIONS L ON L.ID=B.LOCATIONID
        INNER JOIN BS_COUNTRY C ON  C.ID=L.COUNTRYID INNER JOIN BS_DEPARTMENT D on D.ID=E.DEPARTMENTID Inner join BS_DESIGNATION DE on DE.ID=E.DESIGNATIONID where E.STAFFID='" + staffId + "'");
    }
    public long CheckGateEntry()
    {
        DataTable dt = dm.ExecuteQuery("Select * from DP_GATEENTRY where ISOPEN=1 AND CREATEDBY='" + SessionManager.SessionName.StaffId + "'");
        if (dt.Rows.Count == 0)
        {
            dm.ExecuteNonQuery(@"Insert into DP_GATEENTRY(ID,ISOPEN,CREATEDBY) VALUES(SQ_DP_GATEENTRY.NEXTVAL,1,'" + SessionManager.SessionName.StaffId + "')");
            dt = dm.ExecuteQuery("Select * from DP_GATEENTRY where ISOPEN=1 AND CREATEDBY='" + SessionManager.SessionName.StaffId + "'");
        }
        return Convert.ToInt64(dt.Rows[0]["ID"]);
    }
    public DataTable GetGateEntryItems(long GateEntryId)
    {
        return dm.ExecuteQuery("Select GI.*,CASE WHEN GI.ISPERSONAL=1 THEN 'YES' ELSE 'NO' END AS PERSONAL,CASE WHEN GI.ISRETURNABLE=1 THEN 'YES' ELSE 'NO' END AS RETURNABLE FROM DP_GATEENTRYITEMS GI WHERE GI.GATEENTRYID=" + GateEntryId);
    }
    public int InsertGateEntryItems(long GateEntryId, string ItemName, string Attachment, decimal quantity, string unit, string Remarks, int IsPersonal, int IsReturnable)
    {
        return dm.ExecuteNonQuery("INSERT INTO DP_GATEENTRYITEMS(ID,QUANTITY,UNIT,ITEMNAME,GATEENTRYID,ATTACHMENTS,REMARKS,ISRETURNABLE,ISPERSONAL) Values(SQ_DP_GATEENTRYITEMS.NEXTVAL," + quantity + ",'" + unit + "','" + ItemName + "'," + GateEntryId + ",'" + Attachment + "','" + Remarks + "','" + IsReturnable + "','" + IsPersonal + "')");
    }
    public long GetGateEntryIdfromTransactionId(string TransactionId)
    {
        DataTable dt = dm.ExecuteQuery("Select ID from DP_GATEENTRY where GATEENTRYID='" + TransactionId + "'");
        return Convert.ToInt64(dt.Rows[0][0]);
    }

   
    public void GatePass(string TransactionId, int Gateno)
    {
        dm.ExecuteNonQuery(@"UPDATE DP_GATEENTRY set GATEPASSED=1,GATENO='" + Gateno + "',OUTDATETIME=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE where GATEENTRYID='" + TransactionId + "'");
    }
    public DataTable GetEntryDetails(long Id)
    {
        return dm.ExecuteQuery(@"Select G.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,D.DEPARTMENTCODE,D.DEPARTMENTNAME,B.CENTERCODE,B.CENTERNAME from DP_GATEENTRY G LEFT OUTER JOIN EM_EMPLOYEE E ON E.STAFFID=G.STAFFID
        LEFT OUTER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID LEFT OUTER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID WHERE GATEPASSED=0 AND G.ID=" + Id);
    }
    public DataTable GateCheck(string GateEntryId)
    {
        return dm.ExecuteQuery(@"Select G.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,D.DEPARTMENTCODE,D.DEPARTMENTNAME,B.CENTERCODE,B.CENTERNAME from DP_GATEENTRY G LEFT OUTER JOIN EM_EMPLOYEE E ON E.STAFFID=G.STAFFID
        LEFT OUTER JOIN BS_BUSINESSCENTER B ON B.ID=E.BUSINESSCENTERID LEFT OUTER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID WHERE GATEPASSED=0 AND G.GATEENTRYID='" + GateEntryId + "'");
    }
    public DataTable GetGateEntryList()
    {
        return dm.ExecuteQuery(@"Select * from DP_GATEENTRY WHERE STAFFID='" + SessionManager.SessionName.StaffId + "' AND GATEPASSED=0");
    }
    public DataTable GetSampleReturnable()
    {
        return dm.ExecuteQuery(@"SELECT * FROM(
        Select D.*,(SELECT COUNT(ID) FROM DP_GATEENTRYITEMS G WHERE G.ISRETURNABLE=1 AND G.ISPERSONAL=1 AND G.GATEENTRYID=D.ID ) CNT from DP_GATEENTRY D) T WHERE T.CNT>0 AND T.STAFFID='" + SessionManager.SessionName.StaffId + "' AND T.ISRETURNED=0");
    }
    public void SampleReturn(long id)
    {
        dm.ExecuteNonQuery(@"UPDATE DP_GATEENTRY SET ISRETURNED=1, UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetReturnListForMail()
    {
        return dm.ExecuteQuery("SELECT * FROM VW_SAMPLERETURNABLE V WHERE V.DAYS>7");
    }
    public DataTable GetReturnItems(long GateEntryId)
    {
        return dm.ExecuteQuery("Select GI.* FROM DP_GATEENTRYITEMS GI WHERE GI.ISRETURNABLE=1 AND GI.ISPERSONAL=1 AND GI.GATEENTRYID=" + GateEntryId);

    }
    public DataTable GetEntryItemsDetails(long getEntryId)
    {
        return dm.ExecuteQuery("Select * from DP_GATEENTRYITEMS WHERE GATEENTRYID=" + getEntryId);
    }

    public DataTable GetByGeteEntryDetails(long getEntryId)
    {
        return dm.ExecuteQuery(@"SELECT GE.*, EM.EMPLOYEENAME,EM.STAFFID,EM.OFFICEMOBILE ,BC.CENTERNAME, BC.CENTERCODE,D.DEPARTMENTNAME,D.DEPARTMENTCODE
        FROM DP_GATEENTRY GE 
        INNER JOIN EM_EMPLOYEE EM ON EM.STAFFID = GE.CREATEDBY
        INNER JOIN BS_BUSINESSCENTER BC ON BC.ID = GE.BUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT D ON D.ID = GE.DEPARTMENTID  
        WHERE GE.ID = " + getEntryId);
    }
}