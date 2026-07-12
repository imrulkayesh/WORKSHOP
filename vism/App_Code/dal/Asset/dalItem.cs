using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalItem
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalItem()
    {
    }
    public int Insert(string name, int categoryId, string itemcode, int vendorId, decimal totalquantity, decimal availablequantiy, int iscommon, decimal unitprice, int unitid, int currencyid, DateTime purchasedate, string Brand, string ItemNumber, string ExtraFields, string extraValues)
    {
        return dm.ExecuteNonQuery(@"Insert into ASSET_ITEMS(ID,ITEMNAME,CATEGORYID,ITEMCODE,VENDORID,TOTALQUANTITY,AVAILABLEQUANTITY,ISCOMMON,UNITPRICE,UNITID,CURRENCYID,PURCHASEDATE,BRAND,ITEMNUMBER,CREATEDBY" + ExtraFields + ")"
       + @" VALUES(SQ_ASSET_ITEMS.NEXTVAL,'" + name + "','" + categoryId + "','" + itemcode + "','" + vendorId + "','" + totalquantity + "','" + availablequantiy + "','" + iscommon + "','" + unitprice + "'," + unitid + "," + currencyid + ", TO_DATE('" + purchasedate + "','MM/dd/yyyy HH:MI:SSAM'),'" + Brand + "','" + ItemNumber + "','" + SessionManager.SessionName.StaffId + "'" + extraValues + ")");
    }
    public int Update(int id, string name, int categoryId, string itemcode, int vendorId, decimal totalquantity, decimal availablequantiy, int iscommon, decimal unitprice, int unitid, int currencyid, DateTime purchasedate, string Brand, string ItemNumber, string ExtraFields)
    {
        return dm.ExecuteNonQuery("update ASSET_ITEMS set ITEMNAME='" + name + "',CATEGORYID=" + categoryId + ",TOTALQUANTITY='" + totalquantity + "',AVAILABLEQUANTITY='" + availablequantiy + "',ISCOMMON='" + iscommon + "',UNITPRICE='" + unitprice + "',UNITID='" + unitid + "',CURRENCYID='" + currencyid + "',PURCHASEDATE=TO_DATE('" + purchasedate + "','MM/dd/yyyy HH:MI:SSAM'),BRAND='" + Brand + "',ITEMNUMBER='" + ItemNumber + "'" + ExtraFields + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "', UPDATEDATE= TO_DATE('" + DateTime.Now + "','MM/dd/yyyy HH:MI:SSAM') where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from ASSET_ITEMS I inner join ASSET_CATEGORY C ON C.ID=I.CATEGORYID where I.ID=" + id);
    }

    public DataTable GetConsumptionDetails(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT J.ID JOBID,J.CREATEDATE, J.EFFECTIVEDATE,PI.DRIVERNAME, PI.DRIVERMOBILE,ST.SERVICETYPE,
        V.REGNO, V.CHASSISNO, M.MODELNAME, SP.PARTSNAME, JI.QUANTITY, U.UNITNAME,
        JI.AMOUNT, C.COMPANYNAME,J.REMARKS,W.WORKSHOP,V.ID VEHICLEID FROM RVS_JOBCARD J
        LEFT OUTER JOIN RVS_JOBCARDITEMS JI ON JI.JOBID = J.ID
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        LEFT OUTER JOIN RVS_SPAREPARTS SP ON SP.ID = JI.PARTSID
        LEFT OUTER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN V_COMPANY C ON C.COMPANYID= V.COMPANYID
        INNER JOIN RVS_SERVICETYPE ST ON ST.ID = J.SERVICETYPEID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        LEFT OUTER JOIN ASSET_UNIT U ON U.ID = SP.UNITID WHERE " + critaria + " ORDER BY J.CREATEDATE DESC ");
    }

    public DataTable JobInformation(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT J.ID JOBID,J.CREATEDATE,J.UPDATEDATE, v.REGNO,M.MODELNAME,PI.MILLAGE,ST.SERVICETYPE, SC.CATNAME,II.ITEMNAME, 
             PD.QUANTITY,
             PD.LABORRATE,
             PD.LABORQTY,
             PD.WORKINGHOUR,
             (PD.QUANTITY * PD.LABORRATE * PD.LABORQTY * PD.WORKINGHOUR)    AMOUNT,
            PD.REMARKS,CO.CUSTOMERNAME,C.COMPANYNAME,J.CREATEDBY,J.UPDATEDBY,W.WORKSHOP
            FROM RVS_INSPECTIONITEMS II
            INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID=II.SERVICECATID
            INNER JOIN RVS_PIDETAILS PD ON PD.INSPECTIONITEMID = II.ID 
            INNER JOIN RVS_PREINSPECTION PI ON PI.ID = PD.PIID
            INNER JOIN RVS_JOBCARD J ON J.PIID  = PI.ID
            INNER JOIN RVS_VEHICLE V ON V.ID = PI.VEHICLEID
            INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
            INNER JOIN RVS_SERVICETYPE ST ON ST.ID = PI.SERVICETYPEID
            INNER JOIN RVS_WORKSHOP W ON W.ID = PI.WORKSHOPID
            INNER JOIN V_COMPANY C ON C.COMPANYID = V.COMPANYID
            INNER JOIN RVS_CUSTOMER CO ON CO.ID = V.CUSTOMERID
            WHERE PI.ID >0 AND II.INSPECTIONTYPE=0 AND PD.STATUSID=2" + critaria + " ORDER BY j.id, J.CREATEDATE,v.REGNO");
    }
    public DataTable SingleJobDetails(string jobId)
    {
        return dm.ExecuteQuery(@"SELECT J.ID JOBID,PI.ID PIID, J.UPDATEDATE, J.EFFECTIVEDATE, PI.CREATEDATE as CDS,ST.SERVICETYPE,M.MODELNAME,V.CHASSISNO,V.REGNO,V.ENGINENO,V.MANUFACTURINGYEAR ,
        C.CUSTOMERNAME,C.MOBILE,C.PHONE ,C.ADDRESS ,O.*,W.WORKSHOP,W.ADDRESS WADDRESS,B.*,PI.DRIVERNAME,PI.DRIVERMOBILE,PI.MILLAGE, CO.COMPANYNAME FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        LEFT OUTER JOIN RVS_MODEL M ON M.ID= V.MODELID
        LEFT OUTER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        LEFT OUTER JOIN RVS_BILL B ON B.JOBID=J.ID
        INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
        WHERE J.ID =" + jobId);
    }

    public DataTable GetConsumptionWithJob(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT J.ID JOBID,J.CREATEDATE, J.EFFECTIVEDATE,PI.DRIVERNAME, PI.DRIVERMOBILE,ST.SERVICETYPE,
        V.REGNO, V.CHASSISNO, M.MODELNAME, SP.PARTSNAME, JI.QUANTITY, U.UNITNAME,
        JI.AMOUNT, C.COMPANYNAME,J.REMARKS,W.WORKSHOP,V.ID VEHICLEID FROM RVS_JOBCARD J
        LEFT OUTER JOIN RVS_JOBCARDITEMS JI ON JI.JOBID = J.ID
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        LEFT OUTER JOIN RVS_SPAREPARTS SP ON SP.ID = JI.PARTSID
        LEFT OUTER JOIN RVS_WORKSHOP W ON W.ID = J.WORKSHOPID
        INNER JOIN RVS_VEHICLE V ON V.ID = J.VEHICLEID
        INNER JOIN V_COMPANY C ON C.COMPANYID= V.COMPANYID
        INNER JOIN RVS_SERVICETYPE ST ON ST.ID = J.SERVICETYPEID
        INNER JOIN RVS_MODEL M ON M.ID = V.MODELID
        LEFT OUTER JOIN ASSET_UNIT U ON U.ID = SP.UNITID WHERE " + critaria + " ");
    }

    public DataTable MonthlyReportWwise(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT DISTINCT J.ID JOBID, ST.SERVICETYPE, V.ID ,V.REGNO, V.CHASSISNO , M.MODELNAME,C.COMPANYNAME,
        J.WORKSHOPID,W.WORKSHOP,J.EFFECTIVEDATE,J.CREATEDATE, CU.CUSTOMERNAME  FROM 
        RVS_JOBCARD J,
        RVS_VEHICLE V,
        V_COMPANY C ,
        RVS_WORKSHOP W,
        RVS_MODEL M,
        RVS_SERVICETYPE ST,
        RVS_CUSTOMER CU
        WHERE
        J.VEHICLEID = V.ID
        AND C.COMPANYID= V.COMPANYID
        AND W.ID = J.WORKSHOPID
        AND M.ID = V.MODELID
        AND ST.ID = J.SERVICETYPEID
        AND CU.ID = V.CUSTOMERID
        AND W.ID = J.WORKSHOPID
        AND M.ID = V.MODELID
        AND ST.ID = J.SERVICETYPEID
        " + critaria + " ORDER BY C.COMPANYNAME ASC");
    }
    public DataTable GetVehicleAll()
    {
        return dm.ExecuteQuery("SELECT V.ID ||'-'||V.REGNO||'-'||V.CHASSISNO VEHICLE FROM RVS_VEHICLE V WHERE V.ID >0");
    }
    public DataTable GetConsumptionJobcard()
    {
        return dm.ExecuteQuery(@"Select I.*, U.UNITCODE, U.UNITNAME from ASSET_ITEMS I 
        INNER JOIN ASSET_UNIT U ON U.ID = I.UNITID ");
    }

    public bool CheckDuplicate(string name, string ItemNumber)
    {
        DataTable dt = dm.ExecuteQuery("Select * from ASSET_ITEMS WHERE ITEMNAME='" + name + "' and ITEMNUMBER='" + ItemNumber + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public DataTable WorkOrderInformation(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT WM.WORKORDERNO,
                                 WOITEM.ITEMNAME,
                                 WD.REASON,
                                 WM.WORKORDERDATE,
                                 WM.VEHICLEID,
                                 WM.DRIVERSTAFFID,
                                 WM.WORKSHOPID,
                                 W.WORKSHOP,
                                 WM.RUNNINGMILLAGE,
                                 WM.PREINSPECTIONID,
                                 V.REGNO,
                                 CASE 
                                 WHEN WM.WORKORDERSTATUS = 0 THEN 'completed' 
                                 ELSE 'Pending' 
                               END AS WORKORDERSTATUS
                            FROM RVS_WORKORDERMASTER  WM
                                LEFT JOIN RVS_WORKORDERDETAILS WD ON WD.WORKORDERNO = WM.WORKORDERNO
                                LEFT JOIN RVS_WORKORDERITEMS WOITEM ON WD.WORKORDERITEMID = WOITEM.ID
                                LEFT JOIN RVS_VEHICLE V ON WM.VEHICLEIDNEW = V.ID
                                LEFT JOIN RVS_WORKSHOP W ON WM.WORKSHOPID = W.ID
                            WHERE     WM.ISACTIVE = 1

                        " + critaria + " ORDER BY WM.WORKORDERNO ");
    }
    public DataTable GetJobDurationData(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT 
                              JDT.WORKSHOP,
                              JDT.REGNO,
                              JDT.WORKORDERNO,
                              JDT.WORKORDERDATE,
                              JDT.GATEIN,
                              JDT.PREINSPECTION_ID,
                              JDT.PREINSPECTIONDATE,
                              JDT.JOB_ID,
                              JDT.JOBDATE,
                              JDT.JOBCLOSEDATE,
                              JDT.WORKINGHOURS,
                              JDT.ACTUALWORKINGHOURS,
                              JDT.DELAYTIME,
                              JDT.JOBDURATION,
                              JDT.GATEOUT,
                              JDT.NPT_STATUS,
                               CASE 
                                WHEN (JDT.WORKINGHOURS - JDT.ACTUALWORKINGHOURS) < 0 
                                THEN (JDT.WORKINGHOURS - JDT.ACTUALWORKINGHOURS)
                                ELSE 0
                              END AS DELAYTIME_NEW
                            FROM 
                              V_RVS_JOB_DURATION_TRACKING JDT
                              WHERE 
                          JDT.ISACTIVE = 1
                        " + critaria + " ORDER BY JDT.WORKORDERDATE DESC ");
    }

    public DataTable GetJobDurationTrackingDetailsData(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT 
                                  JDT.WORKSHOP,
                                  JDT.REGNO,
                                  JDT.WORKORDERNO,
                                  JDT.WORKORDERDATE,
                                  JDT.WORK_ORDER_CREATOR,
                                  JDT.GATEIN,
                                  ROUND((JDT.GATEIN - JDT.WORKORDERDATE) * 24, 2) AS DIFF_WORKORDER_TO_GATEIN,
                                  JDT.PREINSPECTION_ID,
                                  JDT.PREINSPECTIONDATE,
                                  ROUND((JDT.PREINSPECTIONDATE - JDT.GATEIN) * 24, 2) AS DIFF_GATEIN_TO_PREINSPECTION,
                                  JDT.JOB_ID,
                                  JDT.JOBDATE,
                                  ROUND((JDT.JOBDATE - JDT.PREINSPECTIONDATE) * 24, 2) AS DIFF_PREINSPECTION_TO_JOBDATE,
                                  JDT.JOBCLOSEDATE,
                                  ROUND((JDT.JOBCLOSEDATE - JDT.JOBDATE) * 24, 2) AS DIFF_JOB_TO_CLOSE,
                                  JDT.GATEOUT,
                                  ROUND((JDT.GATEOUT - JDT.JOBCLOSEDATE) * 24, 2) AS DIFF_CLOSE_TO_GATEOUT,
                                  JDT.WORKINGHOURS,
                                  JDT.ACTUALWORKINGHOURS,
                                  CASE 
                                    WHEN (JDT.WORKINGHOURS - JDT.ACTUALWORKINGHOURS) < 0 
                                    THEN (JDT.WORKINGHOURS - JDT.ACTUALWORKINGHOURS)
                                    ELSE 0
                                  END AS DELAYTIME,
                                  JDT.SERVICETYPE,
                                  JDT.TECHNICIAN_GROUP,
                                  JDT.CUSTOMERNAME,
                                  JDT.COMPANYNAME

                                FROM 
                                  V_RVS_JOB_DURATION_TRACKING JDT
                                WHERE JDT.ISACTIVE = 1
                        " + critaria + " ORDER BY JDT.WORKORDERDATE DESC ");
    }

    public DataTable GetNPTData(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT W.WORKSHOP,V.REGNO,M.MODELNAME,ST.SERVICETYPE,NPT.JOBID,D.DELAYCAUSESNAME,RD.RESPONSIBLEDEPARTMENTNAME,NPT.TIME,NPT.REMARKS,J.CREATEDATE
        FROM RVS_NPT NPT
        LEFT JOIN RVS_RESPONSIBLEDEPARTMENT RD ON NPT.RESPONSIBLEDEPARTMENTID = RD.ID
        LEFT JOIN RVS_DELAYCAUSES D ON NPT.DELAYCAUSESID = D.ID
        LEFT JOIN RVS_WORKSHOP W ON W.ID = NPT.WORKSHOPID
        LEFT JOIN RVS_VEHICLE V ON V.ID = NPT.VEHICLEID
        LEFT JOIN RVS_MODEL M ON V.MODELID = M.ID
        LEFT JOIN RVS_JOBCARD J ON J.ID = NPT.JOBID
        LEFT JOIN RVS_SERVICETYPE ST ON J.SERVICETYPEID = ST.ID
        WHERE NPT.ISACTIVE = 1 
        " + critaria + " ORDER BY W.WORKSHOP ASC ");
    }
    //public DataTable StockReconciliationReport(string critaria)
    //{
    //    return dm.ExecuteQuery(@"SELECT A.VISMWHNAME,
    //     SUM (OR_T)       AS OR_T,
    //     SUM (TI)         AS TI,
    //     SUM (CR)         AS CR,
    //     SUM (AI)         AS AI,

    //     SUM (A.CO)       AS CO,
    //     SUM(A.FA)        AS FA,
    //     SUM (A.TO_T)     AS TO_T,
    //     SUM (A.AO)       AS AO,

    //     ((SUM (OR_T)+ SUM (CR)+SUM (TI)+SUM (AI))-(SUM (A.CO)+ SUM (A.FA) +SUM (A.TO_T)+SUM (A.AO))) AS CLOSING_AMT
    //FROM (  SELECT WH.VISMWHNAME,
    //               SUM (CASE WHEN SendAll.TRID LIKE '%CO%' THEN SendAll.ISAMT ELSE 0 END)    AS CO,
    //               SUM (CASE WHEN SendAll.TRID LIKE '%TO%' THEN SendAll.ISAMT ELSE 0 END)    AS TO_T,
    //               SUM (CASE WHEN SendAll.TRID LIKE '%AO%' THEN SendAll.ISAMT ELSE 0 END)    AS AO,
    //               0           FA,
    //               0           OR_T,
    //               0           CR,
    //               0           TI,
    //               0           AI
    //          FROM V_RVS_IS_ALL SendAll
    //               LEFT JOIN RVS_VISMWH WH ON SendAll.WHCODE = WH.ID
    //         WHERE     SendAll.WHCODE IN (SELECT ID FROM RVS_VISMWH)
    //               AND TRUNC (SendAll.TRDATE) BETWEEN  " + critaria + @" GROUP BY WH.VISMWHNAME

    //      UNION ALL
    //        SELECT WH.VISMWHNAME,
    //               0           CO,
    //               0           TO_T,
    //               0           AO,
    //               0           FA,
    //               SUM (CASE WHEN RecvdAll.TRID LIKE '%OR%' THEN RecvdAll.REAMT ELSE 0 END)    AS OR_T,
    //               SUM (CASE WHEN RecvdAll.TRID LIKE '%CR%' THEN RecvdAll.REAMT ELSE 0 END)    AS CR,
    //               SUM (CASE WHEN RecvdAll.TRID LIKE '%TI%' THEN RecvdAll.REAMT ELSE 0 END)    AS TI,
    //               SUM (CASE WHEN RecvdAll.TRID LIKE '%AI%' THEN RecvdAll.REAMT ELSE 0 END)    AS AI
    //          FROM V_RVS_RE_ALL RecvdAll
    //               LEFT JOIN RVS_VISMWH WH ON RecvdAll.WHCODE = WH.ID
    //         WHERE     RecvdAll.WHCODE IN (SELECT ID FROM RVS_VISMWH)
    //               AND TRUNC (RecvdAll.TRDATE) BETWEEN  " + critaria + @" GROUP BY WH.VISMWHNAME

    //       UNION ALL
    //      SELECT SendAll.FROMWH,
    //               0 CO,
    //               0 TO_T,
    //               0 AO,
    //               SUM (CASE WHEN SendAll.FANO LIKE '%FA%' THEN SendAll.FAAMT ELSE 0 END)    AS FA,

    //               0           OR_T,
    //               0           CR,
    //               0           TI,
    //               0           AI
    //          FROM V_RVS_FA_MONTH_CLOSE_AMT SendAll
    //         LEFT JOIN RVS_VISMWH WH ON SendAll.FROMWH = WH.ID
    //         WHERE     
    //         SendAll.FROMWHCODE IN (SELECT ID FROM RVS_VISMWH)  AND   
    //         TRUNC (SendAll.fadate) BETWEEN " + critaria + @" GROUP BY SendAll.FROMWH

    //             ) A

    //                             GROUP BY A.VISMWHNAME");
    //}

    public DataTable StockReconciliationReport(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT A.VISMWHNAME,
         SUM (OPENING_AMOUNT) AS OPENING_AMOUNT,
         SUM (OR_T)       AS OR_T,
         SUM (TI)         AS TI,
         SUM (CR)         AS CR,
         SUM (AI)         AS AI,
         
         SUM (A.CO)       AS CO,
         SUM(A.FA)        AS FA,
         SUM (A.TO_T)     AS TO_T,
         SUM (A.AO)       AS AO,
         
         ((SUM (OPENING_AMOUNT) + SUM (OR_T)+ SUM (CR)+SUM (TI)+SUM (AI))-(SUM (A.CO)+ SUM (A.FA) +SUM (A.TO_T)+SUM (A.AO))) AS CLOSING_AMT
    FROM (  SELECT WH.VISMWHNAME,
                   0 OPENING_AMOUNT,
                   SUM (CASE WHEN SendAll.TRID LIKE '%CO%' THEN SendAll.ISAMT ELSE 0 END)    AS CO,
                   SUM (CASE WHEN SendAll.TRID LIKE '%TO%' THEN SendAll.ISAMT ELSE 0 END)    AS TO_T,
                   SUM (CASE WHEN SendAll.TRID LIKE '%AO%' THEN SendAll.ISAMT ELSE 0 END)    AS AO,
                   0           FA,
                   0           OR_T,
                   0           CR,
                   0           TI,
                   0           AI
              FROM V_RVS_IS_ALL SendAll
                   LEFT JOIN RVS_VISMWH WH ON SendAll.WHCODE = WH.ID
             WHERE     SendAll.WHCODE IN (SELECT ID FROM RVS_VISMWH)
                   AND TRUNC (SendAll.TRDATE) BETWEEN  " + critaria + @" GROUP BY WH.VISMWHNAME
 
          UNION ALL
            SELECT WH.VISMWHNAME,
                   0 OPENING_AMOUNT,
                   0           CO,
                   0           TO_T,
                   0           AO,
                   0           FA,
                   SUM (CASE WHEN RecvdAll.TRID LIKE '%OR%' THEN RecvdAll.REAMT ELSE 0 END)    AS OR_T,
                   SUM (CASE WHEN RecvdAll.TRID LIKE '%CR%' THEN RecvdAll.REAMT ELSE 0 END)    AS CR,
                   SUM (CASE WHEN RecvdAll.TRID LIKE '%TI%' THEN RecvdAll.REAMT ELSE 0 END)    AS TI,
                   SUM (CASE WHEN RecvdAll.TRID LIKE '%AI%' THEN RecvdAll.REAMT ELSE 0 END)    AS AI
              FROM V_RVS_RE_ALL RecvdAll
                   LEFT JOIN RVS_VISMWH WH ON RecvdAll.WHCODE = WH.ID
             WHERE     RecvdAll.WHCODE IN (SELECT ID FROM RVS_VISMWH)
                   AND TRUNC (RecvdAll.TRDATE) BETWEEN  " + critaria + @" GROUP BY WH.VISMWHNAME

           UNION ALL
          SELECT SendAll.FROMWH,
                   0 OPENING_AMOUNT,
                   0 CO,
                   0 TO_T,
                   0 AO,
                   SUM (CASE WHEN SendAll.FANO LIKE '%FA%' THEN SendAll.FAAMT ELSE 0 END)    AS FA,
                   
                   0           OR_T,
                   0           CR,
                   0           TI,
                   0           AI
              FROM V_RVS_FA_MONTH_CLOSE_AMT SendAll
             LEFT JOIN RVS_VISMWH WH ON SendAll.FROMWH = WH.ID
             WHERE     
             SendAll.FROMWHCODE IN (SELECT ID FROM RVS_VISMWH)  AND   
             TRUNC (SendAll.fadate) BETWEEN " + critaria + @" GROUP BY SendAll.FROMWH

             UNION ALL
             SELECT  OP.WHNAME, 
                      SUM (OP.OPAMT) AS OPENING_AMOUNT,
                     
                       0 CO,
                       0 TO_T,
                       0 AO,
                       0 FA,
                       
                       0           OR_T,
                       0           CR,
                       0           TI,
                       0           AI
                     
                FROM V_RVS_OP_ALL OP
               WHERE 
               OP.WHCODE IN (SELECT ID FROM RVS_VISMWH)  AND 
               TRUNC (OP.TRDATE) BETWEEN  " + critaria + @"
               GROUP BY OP.WHNAME

                 ) A
                          
                                 GROUP BY A.VISMWHNAME");
    }


}