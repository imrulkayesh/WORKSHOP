using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalProduction
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalProduction()
    {
    }
    public int Insert(int locationId, int businesscenter, int departmentId, int productionLineid, int shiftId, string productionDate, int fHour, int tHour, int inchargeId, int operatorId, int machineId, int mouldId, int itemId, int sizeId, int colorId, int quality, decimal quantity, decimal avegWeight, decimal proWeight, decimal recycle, decimal bQuentity, decimal cQuentity, int dquantity, int rejReason, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_PRODUCTION(ID,LOCATIONID,BUSINESSUNITID,DEPARTMENTID,LINEID,SHIFTID,PRODTIONDATE,FHOURID,THOURID,INCHARGEID,OPERATORID,MACHINEID,MOULDID,ITEMID,SIZEID,COLORID,QUALITYID,QUANTITY,AVGWEIGHT,PROWEIGHT,RECYCLE,BQUANTITY,CQUANTITY,DQUANTITY,REASONID,REMARKS,CREATEDBY)
        VALUES(SQ_PR_PRODUCTION.NEXTVAL,'" + locationId + "','" + businesscenter + "','" + departmentId + "','" + productionLineid + "','" + shiftId + "',TO_DATE('" + productionDate + "','DD/MM/YYYY'),'" + fHour + "','" + tHour + "','" + inchargeId + "','" + operatorId + "','" + machineId + "','" + mouldId + "','" + itemId + "','" + sizeId + "','" + colorId + "','" + quality + "','" + quantity + "','" + avegWeight + "','" + proWeight + "','" + recycle + "','" + bQuentity + "','" + cQuentity + "','" + dquantity + "','" + rejReason + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public int Update(int id, int locationId, int businesscenter, int departmentId, int productionLineid, int shiftId, string productionDate, int fHour, int tHour, int inchargeId, int operatorId, int machineId, int mouldId, int itemId, int sizeId, int colorId, int quality, decimal quantity, decimal avegWeight, decimal proWeight, decimal recycle, decimal bQuentity, decimal cQuentity, int dquantity, int rejReason, string remarks)
    {
        return dm.ExecuteNonQuery("update PR_PRODUCTION set LOCATIONID='" + locationId + "',BUSINESSUNITID='" + businesscenter + "',DEPARTMENTID='" + departmentId + "', LINEID='" + productionLineid + "',SHIFTID='" + shiftId + "',PRODTIONDATE=TO_DATE('" + productionDate + "','DD/MM/YYYY'),FHOURID='" + fHour + "',THOURID='" + tHour + "',INCHARGEID='" + inchargeId + "',OPERATORID='" + operatorId + "',MACHINEID='" + machineId + "',MOULDID='" + mouldId + "',ITEMID='" + itemId + "',SIZEID='" + sizeId + "',COLORID='" + colorId + "',QUALITYID='" + quality + "',QUANTITY='" + quantity + "',AVGWEIGHT='" + avegWeight + "',PROWEIGHT='" + proWeight + "',RECYCLE='" + recycle + "',BQUANTITY='" + bQuentity + "',CQUANTITY='" + cQuentity + "',DQUANTITY='" + dquantity + "',REASONID='" + rejReason + "',REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_PRODUCTION WHERE ID=" + id);
    }
    public DataTable GetALL(int lineId)
    {
        //string fromDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
        //string toDate = DateTime.Now.ToString("dd/MM/yyyy");

        string fromDate = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
        string toDate = DateTime.Now.ToString("dd/MM/yyyy");

        return dm.ExecuteQuery(@"SELECT P.*, B.CENTERNAME, MM.MACHINENAME, MI.ITEMNAME, MD.MOULDNAME FROM PR_PRODUCTION P
                INNER JOIN BS_BUSINESSCENTER B ON B.ID = P.LOCATIONID
                INNER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
                INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
                INNER JOIN PR_ITEM I ON I.ID = P.ITEMID
                INNER JOIN PR_MITEM MI ON MI.ID = I.ITEMID
                INNER JOIN PR_MMOULD MD ON MD.ID = MI.MOLDID
                WHERE P.ID>0 AND P.LINEID ='" + lineId + "' AND TRUNC(P.CREATEDATE) BETWEEN TRUNC (TO_DATE('" + fromDate + "','DD/MM/YYYY')) AND TRUNC (TO_DATE('" + toDate + "','DD/MM/YYYY'))");
    }

    public DataTable GetMoldWiseItem(int moldId)
    {
        return dm.ExecuteQuery(@"SELECT MI.ID, MI.ITEMNAME, MM.MOULDNAME FROM PR_MITEM MI 
        INNER JOIN PR_MMOULD MM ON MM.ID = MI.MOLDID
        WHERE MI.MOLDID=" + moldId);
    }
    public int GetLineId(int lineId)
    {
        DataTable dt = dm.ExecuteQuery("Select ML.* from PR_MLINE  ML WHERE ML.ID=" + lineId);
        if (dt.Rows.Count > 0)
        {
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        else
        {
            return 0;
        }
    }
    public DataTable LineWiseTextBoxPermission(int lineId)
    {
        return dm.ExecuteQuery(@"SELECT LP.*, ML.LINENAME,TB.TEXTBOXNAME, TB.LABELNAME FROM PR_LINEWISETBP LP 
        INNER JOIN PR_TEXTBOX TB ON TB.ID = LP.TEXTBOXID
        INNER JOIN PR_MLINE ML ON ML.ID = LP.LINEID
        WHERE LP.LINEID=" + lineId);
    }
    public DataTable GetAllProduction()
    {
        return dm.ExecuteQuery(@"SELECT P.*, B.CENTERNAME, M.MACHINENAME, I.ITEMNAME FROM PR_PRODUCTION P
        INNER JOIN BS_BUSINESSCENTER B ON B.ID = P.LOCATIONID
        INNER JOIN PR_MMACHINE M ON M.ID = P.MACHINEID
        INNER JOIN PR_MITEM I ON I.ID = P.ITEMID");
    }

    public DataTable GetUserWiseLine(string staffid)
    {
        return dm.ExecuteQuery(@"SELECT ML.* FROM PR_MLINE ML 
        INNER JOIN PR_USERWISELP UL ON UL.LINEID = ML.ID
        WHERE UL.STAFFID =" + SessionManager.SessionName.StaffId);
    }
    public DataTable GetInchargeLW(int id)
    {
        return dm.ExecuteQuery(@"SELECT I.*, C.COMPANYNAME FROM PR_MINCHARGE  I 
        INNER JOIN BS_COMPANY C ON C.ID =I.BUSINESSUNITID
        WHERE I.BUSINESSUNITID=" + id);
    }

    public DataTable GetOperatorLW(int id)
    {
        return dm.ExecuteQuery(@" SELECT O.*, C.COMPANYNAME FROM PR_MOPERATOR O 
        INNER JOIN BS_COMPANY C ON C.ID =O.BUSINESSUNITID
              WHERE O.BUSINESSUNITID=" + id);
    }

    public DataTable GetMachineLW(int id)
    {
        return dm.ExecuteQuery(@"SELECT M.*, MM.MACHINENAME FROM PR_MACHINE M
        INNER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
        WHERE M.LINEID=" + id);
    }

    public DataTable GetMouldLW(int id)
    {
        return dm.ExecuteQuery(@"SELECT M.*, MM.MOULDNAME FROM PR_MOULD M
        INNER JOIN PR_MMOULD MM ON MM.ID = M.MOULDID
        INNER JOIN BS_COMPANY C ON C.ID = M.BUSINESSUNITID
        WHERE M.BUSINESSUNITID =" + id);
    }

    public DataTable GetItemLW(int id)
    {
        return dm.ExecuteQuery(@"SELECT I.*, MI.ITEMNAME||'-'||MI.ITEMCODE ITEMNAME FROM PR_ITEM I
        INNER JOIN PR_MITEM MI ON MI.ID = I.ITEMID 
        INNER JOIN BS_COMPANY C ON C.ID = I.BUSINESSUNITID
        WHERE I.BUSINESSUNITID=" + id);
    }

    public DataTable GetShiftLW(int id)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PR_SHIFT S
        INNER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        WHERE S.LINEID =" + id);
    }

    public DataTable GetColorLW(int id)
    {
        return dm.ExecuteQuery(@"SELECT C.*, MC.COLORNAME FROM PR_COLOR C 
        INNER JOIN PR_MCOLOR MC ON MC.ID = C.COLORID
        WHERE  C.LINEID=" + id);
    }
    public DataTable GetRejReasonLW(int id)
    {
        return dm.ExecuteQuery(@"SELECT RJ.*, MR.REASONNAME FROM PR_REJECTREASON RJ
        INNER JOIN PR_MREJECTREASON MR ON MR.ID = RJ.REASONID
        WHERE RJ.LINEID=" + id);
    }

    public DataTable ProductionSummery(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT P.ID, P.PRODTIONDATE, B.CENTERNAME, C.COMPANYNAME, D.DEPARTMENTNAME, L.LINENAME, MS.SHIFTNAME, 
        I.INCHARGENAME, O.OPERATORNAME, MA.MACHINENAME, MM.MOULDNAME, MI.ITEMNAME FROM PR_PRODUCTION P
        LEFT OUTER JOIN BS_BUSINESSCENTER B ON B.ID = P.LOCATIONID
        LEFT OUTER JOIN BS_COMPANY C ON C.ID = P.BUSINESSUNITID
        LEFT OUTER JOIN BS_DEPARTMENT D ON D.ID = P.DEPARTMENTID
        LEFT OUTER JOIN PR_MLINE L ON L.ID = P.LINEID
        LEFT OUTER JOIN PR_SHIFT S ON S.ID = P.SHIFTID
        LEFT OUTER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        LEFT OUTER JOIN PR_MINCHARGE I ON I.ID = P.INCHARGEID
        LEFT OUTER JOIN PR_MOPERATOR O ON O.ID = P.OPERATORID
        LEFT OUTER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
        LEFT OUTER JOIN PR_MMACHINE MA ON MA.ID = M.MACHINEID
        LEFT OUTER JOIN PR_MOULD MO ON MO.ID= P.MOULDID
        LEFT OUTER JOIN PR_MMOULD MM ON MM.ID = MO.MOULDID
        LEFT OUTER JOIN PR_MITEM MI ON MI.ID = P.ITEMID
        WHERE P.ID >0 " + critaria + " ORDER BY P.PRODTIONDATE ASC");
    }
    public DataTable MachineWisePreport(string critaria)
    {
        return dm.ExecuteQuery(@"SELECT P.ID, P.PRODTIONDATE, B.CENTERNAME, C.COMPANYNAME, D.DEPARTMENTNAME, L.LINENAME, MS.SHIFTNAME, 
        I.INCHARGENAME, O.OPERATORNAME, MA.MACHINENAME, MM.MOULDNAME, MI.ITEMNAME FROM PR_PRODUCTION P
        LEFT OUTER JOIN BS_BUSINESSCENTER B ON B.ID = P.LOCATIONID
        LEFT OUTER JOIN BS_COMPANY C ON C.ID = P.BUSINESSUNITID
        LEFT OUTER JOIN BS_DEPARTMENT D ON D.ID = P.DEPARTMENTID
        LEFT OUTER JOIN PR_MLINE L ON L.ID = P.LINEID
        LEFT OUTER JOIN PR_SHIFT S ON S.ID = P.SHIFTID
        LEFT OUTER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        LEFT OUTER JOIN PR_MINCHARGE I ON I.ID = P.INCHARGEID
        LEFT OUTER JOIN PR_MOPERATOR O ON O.ID = P.OPERATORID
        LEFT OUTER JOIN PR_MACHINE M ON M.ID = P.MACHINEID
        LEFT OUTER JOIN PR_MMACHINE MA ON MA.ID = M.MACHINEID
        LEFT OUTER JOIN PR_MOULD MO ON MO.ID= P.MOULDID
        LEFT OUTER JOIN PR_MMOULD MM ON MM.ID = MO.MOULDID
        LEFT OUTER JOIN PR_MITEM MI ON MI.ID = P.ITEMID
        WHERE P.ID >0 " + critaria + " ORDER BY P.PRODTIONDATE ASC");
    }
    public DataTable ProductionDetails(string critaria)
    {
        return
            dm.ExecuteQuery(
                @"SELECT PD.PROLINEID, P.ID, P.LOCATIONID, P.BUSINESSUNITID, L.LINENAME,P.MACHINEID,FH.HOURNAME FROMHOUR, TH.HOURNAME TOHOUR,
         P.DEPARTMENTID,P.ITEMID,MS.SHIFTNAME, MM.MACHINENAME, TO_CHAR(PD.PRODATE, 'DD-MM-YYYY') PRODATE, PD.AQUANTITY, PD.BQUANTITY, PD.CQUANTITY,
        (SELECT B.CENTERNAME FROM BS_BUSINESSCENTER B WHERE B.ID= P.LOCATIONID) BULOCATION,
        (SELECT C.COMPANYNAME FROM BS_COMPANY C WHERE C.ID= P.BUSINESSUNITID) COMPANYNAME,
        (SELECT D.DEPARTMENTNAME FROM BS_DEPARTMENT D WHERE D.ID= P.DEPARTMENTID ) DEPARTMENTNAME,
        (SELECT IT.ITEMNAME FROM PR_MITEM IT WHERE IT.ID= P.ITEMID ) ITEMNAME,
        (SELECT MI.INCHARGENAME FROM PR_MINCHARGE MI WHERE MI.ID = P.INCHARGEID) INCHARGENAME,
        (SELECT MO.OPERATORNAME FROM PR_MOPERATOR MO WHERE MO.ID = P.OPERATORID ) OPERATORNAME,
        (SELECT MLD.MOULDNAME FROM PR_MOULD MD INNER JOIN PR_MMOULD MLD ON MLD.ID = MD.MOULDID WHERE MD.MOULDID = P.MOULDID ) MOULDNAME
        FROM PR_PRODETAILS PD 
        LEFT OUTER JOIN PR_PRODUCTION P ON P.ID= PD.PROLINEID
        LEFT OUTER JOIN PR_MLINE L ON L.ID = PD.LINEID
        LEFT OUTER JOIN PR_MACHINE M ON M.ID = PD.MACHINEID
        LEFT OUTER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
        LEFT OUTER JOIN PR_SHIFT S ON S.ID = PD.SHIFTID
        LEFT OUTER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
        LEFT OUTER JOIN PR_MHOUR FH ON FH.ID = PD.FHOURID
        LEFT OUTER JOIN PR_MHOUR TH ON TH.ID = PD.TOHOURID
        WHERE PD.ID > 0 " + critaria + "");
    }

    //public DataTable MachineWisePreport(string critaria)
    //{
    //    return dm.ExecuteQuery(@"SELECT TBL.*, TBL.TAQUANTITY + TBL.TBQUANTITY + TBL.TCQUANTITY TOTALAQUANTITY FROM 
    //    (SELECT PD.PROLINEID, P.ID, P.LOCATIONID, P.BUSINESSUNITID, L.LINENAME,P.MACHINEID,
    //     P.DEPARTMENTID,P.ITEMID,MS.SHIFTNAME, TO_CHAR(PD.PRODATE, 'YYYY-MM-DD') PRODATE, PD.AQUANTITY, PD.BQUANTITY, PD.CQUANTITY,
    //    (SELECT SUM(PD.AQUANTITY) FROM PR_PRODETAILS PD WHERE PD.PROLINEID= P.ID ) TAQUANTITY,
    //    (SELECT SUM(PD.BQUANTITY) FROM PR_PRODETAILS PD WHERE PD.PROLINEID= P.ID ) TBQUANTITY,
    //    (SELECT SUM(PD.CQUANTITY) FROM PR_PRODETAILS PD WHERE PD.PROLINEID= P.ID ) TCQUANTITY,
    //    (SELECT B.CENTERNAME FROM BS_BUSINESSCENTER B WHERE B.ID= P.LOCATIONID) BULOCATION,
    //    (SELECT C.COMPANYNAME FROM BS_COMPANY C WHERE C.ID= P.BUSINESSUNITID) COMPANYNAME,
    //    (SELECT D.DEPARTMENTNAME FROM BS_DEPARTMENT D WHERE D.ID= P.DEPARTMENTID ) DEPARTMENTNAME,
    //    (SELECT IT.ITEMNAME FROM PR_MITEM IT WHERE IT.ID= P.ITEMID ) ITEMNAME,
    //    (SELECT MCA.MACHINENAME FROM PR_MACHINE MC INNER JOIN PR_MMACHINE MCA ON MCA.ID = MC.MACHINEID WHERE MC.MACHINEID = P.MACHINEID ) MACHINENAME
    //    FROM PR_PRODETAILS PD 
    //    LEFT OUTER JOIN PR_PRODUCTION P ON P.ID= PD.PROLINEID
    //    LEFT OUTER JOIN PR_MLINE L ON L.ID = PD.LINEID
    //    LEFT OUTER JOIN PR_MACHINE M ON M.ID = PD.MACHINEID
    //    LEFT OUTER JOIN PR_MMACHINE MM ON MM.ID = M.MACHINEID
    //    LEFT OUTER JOIN PR_SHIFT S ON S.ID = PD.SHIFTID
    //    LEFT OUTER JOIN PR_MSHIFT MS ON MS.ID = S.SHIFTID
    //    WHERE PD.ID > 0 " + critaria + "" +
    //                           " GROUP BY P.ID, PD.PROLINEID, P.LOCATIONID,P.BUSINESSUNITID, " +
    //    "P.DEPARTMENTID, PD.LINEID,L.LINENAME,P.ITEMID,MS.SHIFTNAME,PD.PRODATE,P.MACHINEID, PD.AQUANTITY, PD.BQUANTITY, PD.CQUANTITY) TBL");

    //    //WHERE PD.ID > 0 " + critaria + "" +
    //}
    public bool CheckDuplicate(int lineId)
    {
        DataTable dt = dm.ExecuteQuery("@'" + lineId + "' ");
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