using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalJobCard
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalJobCard()
    {
    }
    public int Insert(int vehicleid, string effDate, int workshopId, int PiID, int sercategoryid, int ServiceTypeId, string driver, string drivermobile, string remark,int groupId)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_JOBCARD(ID,VEHICLEID,EFFECTIVEDATE,WORKSHOPID,PIID,SERVICECATID,SERVICETYPEID,DRIVERNAME,DRIVERMOBILE,REMARKS,CREATEDBY,GROUPID)
        VALUES(SQ_RVS_JOBCARD.NEXTVAL,'" + vehicleid + "',TO_DATE('" + effDate + "','DD/MM/YYYY'),'" + workshopId + "','" + PiID + "','" + sercategoryid + "','" + ServiceTypeId + "','" + driver + "','" + drivermobile + "','" + remark + "','" + SessionManager.SessionName.StaffId + "','" + groupId + "')");
        int JobID = 0;
        DataTable dt = dm.ExecuteQuery("SELECT ID FROM RVS_JOBCARD WHERE VEHICLEID='" + vehicleid + "' AND PIID='" + PiID + "' and  SERVICECATID='" + sercategoryid + "' AND  WORKSHOPID='" + workshopId + "' AND CREATEDBY='" + SessionManager.SessionName.StaffId + "' ORDER BY ID DESC");
        if (dt.Rows.Count > 0)
        {
            JobID = Convert.ToInt32(dt.Rows[0][0]);
        }
        return JobID;
    }
    public void InsertTechs(int JobID, string staffId, string remark)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_JOBCARDTECHS(JOBID,STAFFID,REMARKS)
        VALUES('" + JobID + "','" + staffId + "','" + remark + "')");
    }
    public void DeleteTechs(int JobID)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_JOBCARDTECHS Where JOBID='" + JobID + "'");
    }
    public int Update(int id, string EffDate, int ServiceCat, int ServiceType, string remark, int GrpupId )
    {
        return dm.ExecuteNonQuery("update RVS_JOBCARD set EFFECTIVEDATE=TO_DATE('" + EffDate + "','DD/MM/YYYY'),SERVICECATID='" + ServiceCat + "',SERVICETYPEID='" + ServiceType + "',REMARKS='" + remark + "',GROUPID='" + GrpupId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public void UpdateStoreParts(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            try
            {
                string query = @"BEGIN ";
                foreach (DataRow dr in dt.Rows)
                {
                    query += "update RVS_JOBCARDITEMS set STOREQTY='" + dr["STOREQTY"] + "' WHERE JOBID='" + dr["JOBID"] + "' AND PARTSID='" + dr["PARTSID"] + "';";
                }
                query += " END;";

                dm.ExecuteNonQuery(query);
            }
            catch
            { }
        }
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select jc.*,jc.VEHICLEID||' - ' ||vh.REGNO Veh,vh.BUSINESSCENTERID,vh.COMPANYID,MO.MODELNAME from RVS_JOBCARD jc
inner join rvs_vehicle vh on jc.VEHICLEID=vh.ID
INNER JOIN RVS_MODEL MO ON VH.MODELID =MO.ID WHERE jc.ID=" + id);
    }
    public DataTable GetByVehicleId(int VehicleId)
    {
        return dm.ExecuteQuery("Select J.*,W.WORKSHOP from RVS_JOBCARD J INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID WHERE J.ISOPEN=1 AND J.VEHICLEID=" + VehicleId);
    }
    public DataTable GetAll(int workshopid)
    {
        return dm.ExecuteQuery(@"SELECT J.*, ST.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE FROM RVS_JOBCARD J
        INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        WHERE J.ISOPEN=1 AND  J.WORKSHOPID='" + workshopid + "'");
    }
    public DataTable JobCardForStore()
    {
        return dm.ExecuteQuery(@"SELECT J.*, ST.SERVICETYPE,V.CHASSISNO,V.REGNO,C.CUSTOMERNAME,C.MOBILE FROM RVS_JOBCARD J
        INNER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        INNER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        WHERE J.ISOPEN=1 AND  W.BUSINESSCENTERID ='" + SessionManager.SessionName.BusinessCentetid + "'");
    }
    public DataTable GetPartsStore(int JobID)
    {
        string query = @" SELECT P.ID, P.PARTSNAME,IT.AMOUNT, IT.QUANTITY, IT.JOBID ,IT.REMARKS,IT.STOREQTY
            FROM RVS_JOBCARDITEMS IT
            INNER JOIN RVS_SPAREPARTS P  ON P.ID=IT.PARTSID
            WHERE IT.JOBID='" + JobID + "'";
        return dm.ExecuteQuery(query);
    }
    public DataTable GetJobCardPrint(int JOBID)
    {
        //return dm.ExecuteQuery(@"SELECT J.ID JOBID,PI.ID PIID,PI.CREATEDATE, J.UPDATEDATE, J.CREATEDBY, J.EFFECTIVEDATE,J.REMARKS, ST.SERVICETYPE,M.MODELNAME,V.CHASSISNO,V.REGNO,V.ENGINENO,V.MANUFACTURINGYEAR ,
        //C.CUSTOMERNAME,C.MOBILE,C.PHONE ,C.ADDRESS ,O.*,W.WORKSHOP,W.ADDRESS WADDRESS,B.*,PI.DRIVERNAME,PI.DRIVERMOBILE,PI.MILLAGE, CO.COMPANYNAME,G.GROUPNAME,V.DRIVERID FROM RVS_JOBCARD J
        //INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        //LEFT OUTER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        //LEFT OUTER JOIN RVS_MODEL M ON M.ID= V.MODELID
        //LEFT OUTER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        //LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        //LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        //INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        //LEFT OUTER JOIN RVS_BILL B ON B.JOBID=J.ID
        //INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
        //INNER JOIN RVS_GROUP G ON G.ID=J.GROUPID
        //WHERE J.ID=" + JOBID);

        //return dm.ExecuteQuery(@"SELECT J.ID JOBID,PI.ID PIID,PI.CREATEDATE, J.UPDATEDATE, J.CREATEDBY, J.EFFECTIVEDATE,J.REMARKS, ST.SERVICETYPE,M.MODELNAME,V.CHASSISNO,V.REGNO,V.ENGINENO,V.MANUFACTURINGYEAR ,
        //C.CUSTOMERNAME,C.MOBILE,C.PHONE ,C.ADDRESS ,O.*,W.WORKSHOP,W.ADDRESS WADDRESS,B.*,PI.DRIVERNAME,PI.DRIVERMOBILE,PI.MILLAGE, CO.COMPANYNAME,G.GROUPNAME,V.DRIVERID,
        //COJT.AMOUNT SPARE_PARTS_COST, B.ACTUALBILL LABOR_COST, B.POWERCOST AS POWER_COST,
        //(NVL(COJT.AMOUNT, 0) + B.ACTUALBILL + B.POWERCOST) AS VARIABLE_COST, 
        //ROUND(( ((NVL(COJT.AMOUNT, 0) + B.ACTUALBILL + B.POWERCOST)/0.92 ) - (NVL(COJT.AMOUNT, 0) + B.ACTUALBILL + B.POWERCOST) ),2) AS CM,
        //ROUND ((( NVL(COJT.AMOUNT, 0) + B.ACTUALBILL + B.POWERCOST)/0.92 ),2) AS GRAND_TOTAL          
        //FROM RVS_JOBCARD J
        //INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        //LEFT OUTER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        //LEFT OUTER JOIN RVS_MODEL M ON M.ID= V.MODELID
        //LEFT OUTER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        //LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        //LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        //INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        //LEFT OUTER JOIN RVS_BILL B ON B.JOBID=J.ID
        //INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
        //INNER JOIN RVS_GROUP G ON G.ID=J.GROUPID
        //LEFT JOIN V_RVS_CO_JOB_TOTAL_AMT COJT ON COJT.JOBID = J.ID
        //WHERE J.ID=" + JOBID);

        return dm.ExecuteQuery(@"SELECT J.ID JOBID,PI.ID PIID,PI.CREATEDATE, J.UPDATEDATE, J.CREATEDBY, J.EFFECTIVEDATE,J.REMARKS, ST.SERVICETYPE,M.MODELNAME,V.CHASSISNO,V.REGNO,V.ENGINENO,V.MANUFACTURINGYEAR ,
        C.CUSTOMERNAME,C.MOBILE,C.PHONE ,C.ADDRESS ,O.*,W.WORKSHOP,W.ADDRESS WADDRESS,B.*,PI.DRIVERNAME,PI.DRIVERMOBILE,PI.MILLAGE, CO.COMPANYNAME,G.GROUPNAME,V.DRIVERID,
        COJT.AMOUNT SPARE_PARTS_COST, L.ACTUALBILL LABOR_COST, B.POWERCOST AS POWER_COST,
        (NVL(COJT.AMOUNT, 0) + L.ACTUALBILL + B.POWERCOST) AS VARIABLE_COST, 
        ROUND(( ((NVL(COJT.AMOUNT, 0) + L.ACTUALBILL + B.POWERCOST)/0.92 ) - (NVL(COJT.AMOUNT, 0) + L.ACTUALBILL + B.POWERCOST) ),2) AS CM,
        ROUND ((( NVL(COJT.AMOUNT, 0) + L.ACTUALBILL + B.POWERCOST)/0.92 ),2) AS GRAND_TOTAL          
        FROM RVS_JOBCARD J
        INNER JOIN RVS_PREINSPECTION PI ON PI.ID = J.PIID
        LEFT OUTER JOIN RVS_VEHICLE V ON V.ID=J.VEHICLEID
        LEFT OUTER JOIN RVS_MODEL M ON M.ID= V.MODELID
        LEFT OUTER JOIN RVS_CUSTOMER C ON C.ID=V.CUSTOMERID
        LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID=J.SERVICETYPEID
        LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID=V.ID
        INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        LEFT OUTER JOIN RVS_BILL B ON B.JOBID=J.ID
        INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
        INNER JOIN RVS_GROUP G ON G.ID=J.GROUPID
        LEFT JOIN V_RVS_CO_JOB_TOTAL_AMT COJT ON COJT.JOBID = J.ID
        LEFT JOIN V_LABOUR_CHARGE_JOB_TOTAL L ON J.ID = L.JOBID
        WHERE J.ID=" + JOBID);
    }

    public DataTable GetPreInspectionCardPrint(int PreInspectionID)
    {

        return dm.ExecuteQuery(@"SELECT 
       PI.ID
           PIID,
       PI.CREATEDATE,
       PI.UPDATEDATE,
       PI.CREATEDBY,
       PI.EFCTDATE,
       PI.REMARKS,
       ST.SERVICETYPE,
       M.MODELNAME,
       V.CHASSISNO,
       V.REGNO,
       V.ENGINENO,
       V.MANUFACTURINGYEAR,
       C.CUSTOMERNAME,
       C.MOBILE,
       C.PHONE,
       C.ADDRESS,
       O.*,
       W.WORKSHOP,
       W.ADDRESS
           WADDRESS,
       PI.DRIVERNAME,
       PI.DRIVERMOBILE,
       PI.MILLAGE,
       CO.COMPANYNAME,
       G.GROUPNAME,
       V.DRIVERID
    FROM RVS_PREINSPECTION PI
       LEFT OUTER JOIN RVS_VEHICLE V ON V.ID = PI.VEHICLEID
       LEFT OUTER JOIN RVS_MODEL M ON M.ID = V.MODELID
       LEFT OUTER JOIN RVS_CUSTOMER C ON C.ID = V.CUSTOMERID
       LEFT OUTER JOIN RVS_SERVICETYPE ST ON ST.ID = PI.SERVICETYPEID
       LEFT OUTER JOIN RVS_OWNERS O ON O.VEHICLEID = V.ID
       INNER JOIN RVS_WORKSHOP W ON W.ID = PI.WORKSHOPID

       INNER JOIN V_COMPANY CO ON CO.COMPANYID = V.COMPANYID
       INNER JOIN RVS_GROUP G ON G.ID = PI.GROUPID
    WHERE PI.ID =" + PreInspectionID);
    }

    public DataTable GetCDdetailsTrn(int TrnNo)
    {
        return dm.ExecuteQuery(@"SELECT 'CD No: '|| RM.ID CDNO,'CD Date: '||to_char(RM.IDATE,'DD/MM/YYYY HH:MI AM') CDDATE,LO.CENTERNAME FROMLOC,WH.VISMWHNAME FROMWH,WS.WORKSHOP FROMCOM,RM.IUSER||' - '||EM.EMPLOYEENAME CREATEDBY,
        LOT.CENTERNAME TOLOC, VH.REGNO TOWH,CO.COMPANYNAME TOCOM,RM.RECVREMARKS SENDREMARKS,
        IM.ITEMCODE,IM.ITEMNAME,UT.UNITNAME,RD.QTY,RD.REMARKS,'Consumption Demand (CD)' RTITLE,'*'||RM.ID||'*' REQUESTNUMBERXXX,MO.MODELNAME,CU.CUSTOMERNAME,CC.CATAGORYNAME CATAGORY FROM RVS_REQDETAIL RD
        INNER JOIN RVS_REQMASTER RM ON RD.REQMASTERID=RM.ID
        INNER JOIN RVS_ITEMMASTER IM ON RD.ITEMCODE=IM.ITEMCODE
        INNER JOIN RVS_SCMUNIT UT ON IM.SCMUNITCODE =UT.UNITCODE
        INNER JOIN RVS_JOBCARD JC ON RM.JOBID=JC.ID
        INNER JOIN BS_BUSINESSCENTER LO ON RM.SENDLOCATION=LO.ID
        INNER JOIN RVS_VISMWH WH ON RM.SENDWH=WH.ID
        INNER JOIN RVS_WORKSHOP WS ON RM.SENDCOMPANY=WS.ID
        INNER JOIN RVS_VEHICLE VH ON RM.RECVWH=VH.ID
        INNER JOIN BS_BUSINESSCENTER LOT ON VH.BUSINESSCENTERID=LOT.ID
        INNER JOIN BS_COMPANY CO ON VH.COMPANYID=CO.ID
        INNER JOIN EM_EMPLOYEE EM ON RM.IUSER=EM.STAFFID
        INNER JOIN rvs_model MO on VH.MODELID=MO.ID
        INNER JOIN RVS_CUSTOMER CU on VH.CUSTOMERID=CU.ID
        LEFT OUTER JOIN RVS_COCATAGORY CC on RM.CATEGORYCODE=CC.ID
        WHERE RM.ID='" + TrnNo + @"'
        ORDER BY RD.LINE");
    }
    public DataTable GetTechnician(int WorkshopId, int JobID)
    {
        string query = @"SELECT TM.STAFFID,T.TEAMNAME ||'-'||E.EMPLOYEENAME TECHNAME,'' REMARKS,0 JOBID FROM RVS_TEAMMEMBER TM
        INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
        WHERE TM.ISACTIVE=1 AND TM.WORKSHOPID=" + WorkshopId;
        if (JobID > 0)
        {
            query = @"SELECT TM.STAFFID,T.TEAMNAME ||'-'||E.EMPLOYEENAME TECHNAME,JT.REMARKS,JT.JOBID FROM RVS_TEAMMEMBER TM
            INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
            INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
            INNER JOIN RVS_JOBCARDTECHS JT ON JT.STAFFID=TM.STAFFID
            WHERE TM.ISACTIVE=1 AND JT.JOBID='" + JobID + "' AND TM.WORKSHOPID=" + WorkshopId;
        }
        return dm.ExecuteQuery(query);
    }
    public DataTable GetTechnicianByWorkShop()
    {
        string query = @"SELECT TM.STAFFID,TM.STAFFID||'_'||T.TEAMNAME ||'_'||E.EMPLOYEENAME TECHNAME,'' REMARKS FROM RVS_TEAMMEMBER TM
        INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
        INNER JOIN EM_EMPLOYEE ES ON ES.BUSINESSCENTERID=E.BUSINESSCENTERID AND ES.STAFFID='" + SessionManager.SessionName.StaffId + "'   WHERE TM.ISACTIVE=1";
        return dm.ExecuteQuery(query);
    }
    public DataTable GetTechBYJOBID(int JobID)
    {
        string query = @"SELECT TM.STAFFID,T.TEAMNAME ||'-'||E.EMPLOYEENAME TECHNAME,JT.REMARKS,JT.JOBID FROM RVS_TEAMMEMBER TM
            INNER JOIN RVS_TEAM T ON T.ID=TM.TEAMID
            INNER JOIN EM_EMPLOYEE E ON E.STAFFID=TM.STAFFID
            INNER JOIN RVS_JOBCARDTECHS JT ON JT.STAFFID=TM.STAFFID
            WHERE TM.ISACTIVE=1 AND JT.JOBID='" + JobID + "'";

        return dm.ExecuteQuery(query);
    }

    public DataTable GetCharges(int WorkshopId, int JobID)
    {
        string query = @"SELECT CN.CHARGENAME,C.AMOUNT,1 QUANTITY, '' REMARKS,0 JOBID,CN.ID FROM RVS_CHARGENAME CN
        INNER JOIN RVS_CHARGES C ON C.CHARGEID=CN.ID
        WHERE C.WORKSHOPID=" + WorkshopId;
        if (JobID > 0)
        {
            query = @"SELECT CN.CHARGENAME,JC.AMOUNT,JC.QUANTITY,JC.REMARKS,JC.JOBID,CN.ID FROM RVS_CHARGENAME CN
            INNER JOIN RVS_CHARGES C ON C.CHARGEID=CN.ID
            INNER JOIN RVS_JOBCARDSCCHARGE JC ON JC.CHARGEID=C.CHARGEID
            WHERE JC.JOBID='" + JobID + "' AND C.WORKSHOPID=" + WorkshopId;
        }
        return dm.ExecuteQuery(query);
    }
    public DataTable GetChargesByJobID(int JobID)
    {
        string query = @"SELECT CN.CHARGENAME,JC.AMOUNT,JC.QUANTITY,JC.REMARKS,JC.JOBID,CN.ID FROM RVS_CHARGENAME CN
            INNER JOIN RVS_CHARGES C ON C.CHARGEID=CN.ID
            INNER JOIN RVS_JOBCARDSCCHARGE JC ON JC.CHARGEID=C.CHARGEID
            INNER JOIN RVS_JOBCARD J ON J.ID=JC.JOBID
            WHERE J.WORKSHOPID=C.WORKSHOPID AND JC.JOBID='" + JobID + "'";

        return dm.ExecuteQuery(query);
    }
    public DataTable GetParts(int WorkshopId, int JobID)
    {
        string query = @"SELECT  P.ID, P.PARTSNAME,P.SELLINGPRICE AMOUNT, 1 QUANTITY, 0 JOBID,'' REMARKS  FROM RVS_SPAREPARTS P 
            WHERE P.WORKSHOPID=" + WorkshopId;
        if (JobID > 0)
        {
            query = @" SELECT P.ID, P.PARTSNAME,IT.AMOUNT, IT.QUANTITY, IT.JOBID ,IT.REMARKS
            FROM RVS_SPAREPARTS P 
            INNER JOIN RVS_JOBCARDITEMS IT ON IT.PARTSID=P.ID 
            WHERE IT.JOBID='" + JobID + "' AND P.WORKSHOPID=" + WorkshopId;
        }
        return dm.ExecuteQuery(query);
    }


    public DataTable GetPartsByJobID(int JobID)
    {
        //string query = @"SELECT P.ID, P.PARTSNAME,IT.AMOUNT, IT.QUANTITY, IT.JOBID ,IT.REMARKS,U.UNITCODE
        //    FROM RVS_SPAREPARTS P 
        //    INNER JOIN RVS_JOBCARDITEMS IT ON IT.PARTSID=P.ID 
        //    INNER JOIN RVS_JOBCARD J ON J.ID=IT.JOBID
        //    INNER JOIN ASSET_UNIT U ON U.ID=P.UNITID
        //    WHERE J.WORKSHOPID=P.WORKSHOPID AND IT.JOBID='" + JobID + "'";
        string query = @"SELECT P.ID, P.PARTSNAME,IT.AMOUNT, IT.QUANTITY, IT.JOBID ,IT.REMARKS,U.UNITCODE
            FROM RVS_SPAREPARTS P 
            INNER JOIN RVS_JOBCARDITEMS IT ON IT.PARTSID=P.ID 
            INNER JOIN RVS_JOBCARD J ON J.ID=IT.JOBID
            INNER JOIN ASSET_UNIT U ON U.ID=P.UNITID
            WHERE IT.JOBID='" + JobID + "'";
        return dm.ExecuteQuery(query);
    }
    public DataTable VehicleJobCardHistory(String critaria)
    {
        return dm.ExecuteQuery(@"SELECT J.VEHICLEID, J.ID JOBID,J.PIID,J.CREATEDATE, V.CHASSISNO,V.REGNO,C.COMPANYNAME, M.MODELNAME ,S.SERVICETYPE,P.DRIVERNAME, P.DRIVERMOBILE,P.MILLAGE
        FROM 
        RVS_JOBCARD J,
        RVS_PREINSPECTION P,
        RVS_VEHICLE V,
        RVS_MODEL M,
        RVS_SERVICETYPE S,
        BS_COMPANY C
        WHERE J.VEHICLEID =V.ID AND
        P.ID = J.PIID AND
        C.ID = V.COMPANYID AND
        M.ID=V.MODELID AND
        S.ID =J.SERVICECATID AND
        J.ISOPEN=1 AND
        J.ID>0 " + critaria);
    }
    public void DeleteCharges(int jobID)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_JOBCARDSCCHARGE Where JOBID='" + jobID + "'");
    }
    public void InsertCharges(int jobID, int ChargeId, decimal qtys, decimal amount, string remarks)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_JOBCARDSCCHARGE(JOBID,CHARGEID,AMOUNT,QUANTITY,REMARKS)
        VALUES('" + jobID + "','" + ChargeId + "','" + amount + "','" + qtys + "','" + remarks + "')");
    }
    public void DeleteParts(int jobID)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_JOBCARDITEMS Where JOBID='" + jobID + "'");
    }
    public void InsertParts(int jobID, int Parts, decimal qtys, decimal amount, string remarks)
    {
        dm.ExecuteNonQuery(@"Insert into RVS_JOBCARDITEMS(JOBID,PARTSID,QUANTITY,AMOUNT,REMARKS)
        VALUES('" + jobID + "','" + Parts + "','" + qtys + "','" + amount + "','" + remarks + "')");
    }
    public void InsertBill(int jobID, decimal amount)
    {
        DeleteBill(jobID);
        dm.ExecuteNonQuery(@"Insert into RVS_BILL(JOBID,TOTALAMOUNT,NETAMOUNT,CREATEDBY)
        VALUES('" + jobID + "','" + amount + "','" + amount + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public void DeleteBill(int jobID)
    {
        dm.ExecuteNonQuery(@"Delete From RVS_BILL Where JOBID='" + jobID + "'");
    }

    public void CloseJobCard(int iD)
    {
        string query = "BEGIN ";
        query += " UPDATE RVS_PREINSPECTION SET ISOPEN=0,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=(SELECT PIID FROM RVS_JOBCARD WHERE ID=" + iD + ");";
        query += " update RVS_JOBCARD set ISOPEN=0,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + iD + ";";
        query += "COMMIT; END;";
        dm.ExecuteNonQuery(query);
    }
    public void OpenJobCard(int iD)
    {
        string query = "BEGIN ";
        query += " UPDATE RVS_PREINSPECTION SET ISOPEN=1,PIOPENBY='" + SessionManager.SessionName.StaffId + "',PIOPENDATE=SYSDATE WHERE ID=(SELECT PIID FROM RVS_JOBCARD WHERE ID=" + iD + ");";
        query += " update RVS_JOBCARD set ISOPEN=1,JOBOPENBY='" + SessionManager.SessionName.StaffId + "',JOBOPENDATE=SYSDATE WHERE ID=" + iD + ";";
        query += "COMMIT; END;";
        dm.ExecuteNonQuery(query);
    }
}