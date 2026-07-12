using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalGatein
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalGatein()
    {
    }
    public int Insert(string chassisno, string regno, string ownername, string ownermobile, string driverName, string driverMobile, int servCatid, int WorkshopId)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_GATEIN(ID,CHASSISNO,REGNO,OWNERNAME,OWNERMOBILE,DRIVERNAME,DRIVERMOBILE,SERVICECATID,WORKSHOPID,BUSINESSCENTERID,CREATEDBY)
        VALUES(SQ_RVS_SERVICETYPE.NEXTVAL,'" + chassisno + "','" + regno + "','" + ownername + "','" + ownermobile + "','" + driverName + "','" + driverMobile + "','" + servCatid + "','" + WorkshopId + "','" + SessionManager.SessionName.BusinessCentetid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string chassisno, string regno, string ownername, string ownermobile, string driverName, string driverMobile, int servCatid, int WorkshopId)
    {
        return dm.ExecuteNonQuery("update RVS_GATEIN set CHASSISNO='" + chassisno + "',REGNO='" + regno + "',OWNERNAME='" + ownername + "',OWNERMOBILE='" + ownermobile + "',DRIVERNAME='" + driverName + "',DRIVERMOBILE='" + driverMobile + "',SERVICECATID='" + servCatid + "',WORKSHOPID='" + WorkshopId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public int VehicleExit(int id)
    {
        return dm.ExecuteNonQuery("update RVS_GATEIN set ISOUT=1,OUTDATE=SYSDATE,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select * from RVS_GATEIN G
        INNER JOIN RVS_WORKSHOP W ON W.ID = G.WORKSHOPID
        INNER JOIN RVS_SERVICETYPE S ON S.ID = G.SERVICECATID
        WHERE G.ID = " + id);
    }
    public DataTable VehicleReciveHistoryAll()
    {
        return dm.ExecuteQuery(@"Select G.*, W.WORKSHOP, S.SERVICETYPE from RVS_GATEIN G
        INNER JOIN RVS_WORKSHOP W ON W.ID = G.WORKSHOPID
        INNER JOIN RVS_SERVICETYPE S ON S.ID = G.SERVICECATID");
    }

    public DataTable VehicleReciveHistory(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT * FROM RVS_GATEIN G WHERE G.ID>0 " + critaria);
    }
    public DataTable GetWorkHistoryAll()
    {
        return dm.ExecuteQuery(@"Select G.*, S.CATNAME from RVS_GATEIN G
        INNER JOIN RVS_SERVICECATEGORY S ON S.ID = G.SERVICECATID");
    }
    public DataTable GetWorkHistory(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT   E.STAFFID,TE.TEAMNAME,E.EMPLOYEENAME,
         J.ID,J.CREATEDATE,J.UPDATEDATE,J.ISOPEN,
         C.CUSTOMERNAME,V.REGNO,V.CHASSISNO,
         M.MODELNAME,J.PIID,S.SERVICETYPE,W.WORKSHOP,
         J.REMARKS,AWI.ITEMNAME,
         AWW.QUANTITY,
         AWW.LABORRATE,
         AWW.LABORQTY,
         AWW.WORKINGHOUR,
         (AWW.QUANTITY * AWW.LABORRATE * AWW.LABORQTY * AWW.WORKINGHOUR)    AMOUNT,
         AWW.REMARKS ""TREMARKS""
         FROM
         RVS_JOBCARDTECHS T
         INNER JOIN RVS_JOBCARD J
         ON J.ID = T.JOBID
         LEFT OUTER JOIN EM_EMPLOYEE E
         ON E.STAFFID = T.STAFFID
         INNER JOIN RVS_TEAMMEMBER TM
         ON TM.STAFFID = E.STAFFID
         INNER JOIN RVS_TEAM TE
         ON TE.ID = TM.TEAMID AND TE.WORKSHOPID = J.WORKSHOPID
         INNER JOIN  RVS_VEHICLE V
         ON V.ID = J.VEHICLEID
         INNER JOIN RVS_CUSTOMER C
         ON C.ID = V.CUSTOMERID
         INNER JOIN RVS_MODEL M
         ON M.ID = V.MODELID
         INNER JOIN RVS_SERVICETYPE S
         ON S.ID = J.SERVICETYPEID
         INNER JOIN RVS_WORKSHOP W
         ON W.ID = J.WORKSHOPID
         INNER JOIN RVS_PIDETAILS AWW
         ON AWW.PIID = J.PIID
         INNER JOIN RVS_INSPECTIONITEMS AWI
         ON AWI.ID = AWW.INSPECTIONITEMID
        WHERE J.ID>0 " + critaria + " ORDER BY J.CREATEDATE DESC ");
    }
    public DataTable GetWorkHistorywithoutproblem(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT   E.STAFFID,TE.TEAMNAME,E.EMPLOYEENAME,
         J.ID,J.CREATEDATE,J.UPDATEDATE,J.ISOPEN,
         C.CUSTOMERNAME,V.REGNO,V.CHASSISNO,
         M.MODELNAME,J.PIID,S.SERVICETYPE,W.WORKSHOP,
         J.REMARKS
         FROM
         RVS_JOBCARDTECHS T
         INNER JOIN RVS_JOBCARD J
         ON J.ID = T.JOBID
         LEFT OUTER JOIN EM_EMPLOYEE E
         ON E.STAFFID = T.STAFFID
         INNER JOIN RVS_TEAMMEMBER TM
         ON TM.STAFFID = E.STAFFID
         INNER JOIN RVS_TEAM TE
         ON TE.ID = TM.TEAMID AND TE.WORKSHOPID = J.WORKSHOPID
         INNER JOIN  RVS_VEHICLE V
         ON V.ID = J.VEHICLEID
         INNER JOIN RVS_CUSTOMER C
         ON C.ID = V.CUSTOMERID
         INNER JOIN RVS_MODEL M
         ON M.ID = V.MODELID
         INNER JOIN RVS_SERVICETYPE S
         ON S.ID = J.SERVICETYPEID
         INNER JOIN RVS_WORKSHOP W
         ON W.ID = J.WORKSHOPID
         INNER JOIN RVS_PIDETAILS AWW
         ON AWW.PIID = J.PIID
         INNER JOIN RVS_INSPECTIONITEMS AWI
         ON AWI.ID = AWW.INSPECTIONITEMID
        WHERE J.ID>0 " + critaria + " group by E.STAFFID,TE.TEAMNAME,E.EMPLOYEENAME, J.ID, J.CREATEDATE, J.UPDATEDATE, J.ISOPEN, C.CUSTOMERNAME, V.REGNO, V.CHASSISNO, M.MODELNAME, J.PIID, S.SERVICETYPE, W.WORKSHOP, J.REMARKS ORDER BY J.CREATEDATE DESC ");
    }
    public DataTable ReceivingDetails(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT W.WORKSHOP, P.ID PIID,P.TECSTAFFID,P.CREATEDATE,
        CASE WHEN P.ISOPEN=1 AND J.ID>0 AND J.ISOPEN=1 THEN 1 WHEN P.ISOPEN=1 AND J.ID is null THEN 2 WHEN P.ISOPEN=0 AND J.ID>0 
        AND J.ISOPEN=0 THEN 0 ELSE 3 END STATUS ,
        J.ID JOBID,C.CUSTOMERNAME,V.REGNO,V.CHASSISNO,M.MODELNAME, S.SERVICETYPE,J.UPDATEDATE, J.REMARKS FROM RVS_PREINSPECTION P 
        LEFT OUTER JOIN RVS_JOBCARD J ON J.PIID =P.ID
        LEFT OUTER JOIN RVS_VEHICLE V ON V.ID=P.VEHICLEID
        LEFT OUTER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE S ON S.ID=P.SERVICETYPEID
        LEFT OUTER JOIN RVS_WORKSHOP W ON W.ID=P.WORKSHOPID
        LEFT OUTER JOIN RVS_MODEL M ON M.ID = V.MODELID
        WHERE P.ID>0 " + critaria + "  ORDER BY P.CREATEDATE DESC ");
    }
    public DataTable ReceiveVehicle(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT J.ID JOBID,J.CREATEDATE, J.EFFECTIVEDATE,PI.DRIVERNAME, PI.DRIVERMOBILE,ST.SERVICETYPE,
        V.REGNO, V.CHASSISNO, M.MODELNAME, 
         C.COMPANYNAME,J.REMARKS,W.WORKSHOP,CO.CUSTOMERNAME, V.ID VEHICLEID FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        INNER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN V_COMPANY C ON C.COMPANYID= V.COMPANYID
        INNER JOIN RVS_SERVICETYPE ST ON ST.ID = J.SERVICETYPEID
        INNER JOIN RVS_CUSTOMER CO ON CO.ID = V.CUSTOMERID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID WHERE J.ISACTIVE=1 AND PI.ISACTIVE=1 " + critaria + " ORDER BY J.CREATEDATE DESC");
    }
    public DataTable StayingVehicle(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT J.ID JOBID,J.CREATEDATE, J.EFFECTIVEDATE,PI.DRIVERNAME, PI.DRIVERMOBILE,ST.SERVICETYPE,
        V.REGNO, V.CHASSISNO, M.MODELNAME, SP.PARTSNAME, JI.QUANTITY, U.UNITNAME,
        JI.AMOUNT, C.COMPANYNAME,J.REMARKS,W.WORKSHOP,CO.CUSTOMERNAME, V.ID VEHICLEID FROM RVS_JOBCARD J
        LEFT OUTER JOIN RVS_JOBCARDITEMS JI ON JI.JOBID = J.ID
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        LEFT OUTER JOIN RVS_SPAREPARTS SP ON SP.ID = JI.PARTSID
        LEFT OUTER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN V_COMPANY C ON C.COMPANYID= V.COMPANYID
        INNER JOIN RVS_SERVICETYPE ST ON ST.ID = J.SERVICETYPEID
        INNER JOIN RVS_CUSTOMER CO ON CO.ID = V.CUSTOMERID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        LEFT OUTER JOIN ASSET_UNIT U ON U.ID = SP.UNITID 
        WHERE J.ID>0 AND J.ISOPEN=1 AND J.ISACTIVE=1 AND PI.ISACTIVE=1 " + critaria + " ORDER BY J.CREATEDATE DESC");
    }
    public DataTable DelivaryVehicle(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT J.ID JOBID,J.CREATEDATE,J.UPDATEDATE, J.EFFECTIVEDATE,PI.DRIVERNAME, PI.DRIVERMOBILE,ST.SERVICETYPE,
        V.REGNO, V.CHASSISNO, M.MODELNAME, 
         C.COMPANYNAME,J.REMARKS,W.WORKSHOP,CO.CUSTOMERNAME, V.ID VEHICLEID FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        INNER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN V_COMPANY C ON C.COMPANYID= V.COMPANYID
        INNER JOIN RVS_SERVICETYPE ST ON ST.ID = J.SERVICETYPEID
        INNER JOIN RVS_CUSTOMER CO ON CO.ID = V.CUSTOMERID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        WHERE J.ID>0 AND J.ISOPEN=0 AND" + critaria + "ORDER BY J.CREATEDATE DESC");
    }
    public DataTable JobcardOpen(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT W.WORKSHOP, P.ID PIID,P.TECSTAFFID,P.CREATEDATE, M.MODELNAME,
        CASE WHEN P.ISOPEN=1 AND J.ID>0 AND J.ISOPEN=1 THEN 1 WHEN P.ISOPEN=1 AND J.ID is null THEN 2 WHEN P.ISOPEN=0 AND J.ID>0 AND J.ISOPEN=0 THEN 0 ELSE 3 END STATUS ,
        J.ID,C.CUSTOMERNAME,V.REGNO,V.CHASSISNO,S.SERVICETYPE,J.UPDATEDATE FROM RVS_PREINSPECTION P 
        LEFT OUTER JOIN RVS_JOBCARD J ON J.PIID =P.ID
        INNER JOIN RVS_VEHICLE V ON V.ID=P.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        INNER JOIN RVS_SERVICETYPE S ON S.ID=P.SERVICETYPEID
        INNER JOIN RVS_WORKSHOP W ON W.ID=P.WORKSHOPID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        WHERE P.ID>0 AND J.ISOPEN=1 " + critaria + "  ORDER BY P.CREATEDATE DESC ");
    }

    public DataTable BillSummary(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT J.ID JOBID, V.ID VEHICLE, V.CHASSISNO, PI.CREATEDATE, J.UPDATEDATE ,ST.SERVICETYPE, V.REGNO,
        B.TOTALBILL, PT.PAYTYPE, W.WORKSHOP, M.MODELNAME,  J.REMARKS,B.BILLNO FROM RVS_JOBCARD J 
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID=J.PIID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID        
        INNER JOIN RVS_BILL B ON B.JOBID = J.ID
        INNER JOIN RVS_SERVICETYPE ST ON ST.ID = J.SERVICETYPEID
        LEFT OUTER JOIN RVS_PAYTYPE PT ON PT.ID = B.PAYTYPEID
        INNER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        WHERE J.ID > 0 " + critaria + " ORDER BY PI.CREATEDATE DESC");
    }
    public DataTable VReceivingHistoryDetails(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT PI.ID PIID, PI.CREATEDATE, J.ID JOBID, V.REGNO, V.CHASSISNO, W.WORKSHOP,
        C.CUSTOMERNAME, ST.SERVICETYPE, M.MODELNAME,J.UPDATEDATE, C.COMPANYNAME FROM 
        RVS_PREINSPECTION PI,
        RVS_JOBCARD J,
        RVS_VEHICLE V,
        RVS_WORKSHOP W ,
        RVS_CUSTOMER C,
        RVS_SERVICETYPE ST,
        RVS_MODEL M,
        V_COMPANY C
        WHERE J.PIID = PI.ID
        AND V.ID= J.VEHICLEID
        AND W.ID = J.WORKSHOPID
        AND C.ID = V.CUSTOMERID
        AND ST.ID = J.SERVICETYPEID
        AND M.ID = V.MODELID
        AND C.COMPANYID = V.COMPANYID 
        AND PI.ID>0 " + critaria + " ORDER BY PI.CREATEDATE DESC ");
    }

    public DataTable VehicleStayWorkshop(string critaria)
    {
        return dm.ExecuteQuery("" + critaria);
    }
    public DataTable GetPDINo(string JobID)
    {
        return dm.ExecuteQuery(@"SELECT * FROM RVS_PDI WHERE JOBID=" + JobID);
    }

    public DataTable GetAll(int catid, int WorkshopId)
    {
        return dm.ExecuteQuery("Select G.*,SC.SERVICETYPE from RVS_GATEIN G INNER JOIN RVS_SERVICETYPE SC ON SC.ID= G.SERVICECATID WHERE WORKSHOPID='" + WorkshopId + "' AND ISOUT=0 AND G.SERVICECATID='" + catid + "'");
    }
    public DataTable GetAllByWorkshop(int WorkShopId)
    {
        return dm.ExecuteQuery("Select G.*,SC.SERVICETYPE from RVS_GATEIN G INNER JOIN RVS_SERVICETYPE SC ON SC.ID= G.SERVICECATID WHERE G.SERVICECATID='" + WorkShopId + "'");
    }
    public bool CheckDuplicate(string chassisno)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_GATEIN WHERE ISOUT=0 and CHASSISNO='" + chassisno + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckOpenBill(string chassisno)
    {
        DataTable dt = dm.ExecuteQuery(@"SELECT V.ID FROM RVS_VEHICLE V
        INNER JOIN RVS_JOBCARD J ON J.VEHICLEID = V.ID
        INNER JOIN RVS_BILL B ON B.JOBID = J.ID
        WHERE V.CHASSISNO= '" + chassisno + "'");
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return true;
        }
    }

}