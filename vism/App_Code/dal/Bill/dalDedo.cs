using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalDedo
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalDedo()
    {
    }
    public int Insert(int shipmentid, decimal frtAmount, int bankId, decimal realAmount, string bankSubDate)
    {
        int dedoId = 0;
        DataTable dt = dm.ExecuteQuery(@"SELECT * FROM BIL_DEDO D INNER JOIN BIL_SHIPMENT S ON S.ID = D.SHIPMENTID WHERE D.SHIPMENTID=" + shipmentid);
        if (dt.Rows.Count > 0)
        {
            dedoId = Convert.ToInt32(dt.Rows[0][0].ToString());
        }
        else
        {
            return dm.ExecuteNonQuery(@"Insert into BIL_DEDO(ID,SHIPMENTID,FRTINVVALUE,BANKID,TOTALREALVALUE,BANKSUBDATE,CREATEDBY)
            VALUES(SQ_BIL_DEDO.NEXTVAL,'" + shipmentid + "','" + frtAmount + "','" + bankId + "','" + realAmount + "',TO_DATE('" + bankSubDate + "','DD/MM/YYYY'),'" + SessionManager.SessionName.StaffId + "')");
        }
        return dedoId;
    }

    //    public int Inserts(int shipmentid, decimal frtAmount, int bankId, decimal realAmount, string bankSubDate)
    //    {
    //        return dm.ExecuteNonQuery(@"Insert into BIL_DEDO(ID,SHIPMENTID,FRTINVVALUE,BANKID,TOTALREALVALUE,BANKSUBDATE,CREATEDBY)
    //        VALUES(SQ_BIL_DEDO.NEXTVAL,'" + shipmentid + "','" + frtAmount + "','" + bankId + "','" + realAmount + "',TO_DATE('" + bankSubDate + "','DD/MM/YYYY'),'" + SessionManager.SessionName.StaffId + "')");
    //    }
    public int InsertDedo(int shipmentid, decimal realAmount, string bankSubDate, int CollectionMode, string attachment)
    {
        return dm.ExecuteNonQuery(@"Insert into BIL_DEDODETAILS(ID,SHIPMENTID,REALVALUE,REALDATE,COLLMODEID,ATTACHMENT,CREATEDBY)
        VALUES(SQ_BIL_DEDODETAILS.NEXTVAL,'" + shipmentid + "','" + realAmount + "',TO_DATE('" + bankSubDate + "','DD/MM/YYYY'),'" + CollectionMode + "','" + attachment + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int shipmentid, decimal frtAmount, int bankId, decimal realAmount, string bankSubDate)
    {
        return dm.ExecuteNonQuery("update BIL_DEDO set SHIPMENTID='" + shipmentid + "',FRTINVVALUE='" + frtAmount + "',BANKID='" + bankId + "',TOTALREALVALUE='" + realAmount + "',BANKSUBDATE=TO_DATE('" + bankSubDate + "','DD/MM/YYYY'),UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAll()
    {
        return dm.ExecuteQuery(@"SELECT D.*,S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT,B.BANKNAME FROM BIL_DEDO D
        INNER JOIN BIL_BANK B ON B.ID=D.BANKID
        INNER JOIN BIL_SHIPMENT S ON S.ID=D.SHIPMENTID where D.ISOPEN=1");
    }
    public DataTable GetAllDedo(int shipmentid)
    {
        return dm.ExecuteQuery(@"SELECT * FROM BIL_DEDODETAILS D
        INNER JOIN BIL_COLLECTIONMODE C ON C.ID=D.COLLMODEID WHERE D.SHIPMENTID=" + shipmentid);
    }
    public DataTable GetShipment()
    {
        return dm.ExecuteQuery("Select S.*,S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE STATUS=7 AND ISOPEN=1");
    }
    public DataTable GetShipmentCachIn()
    {
        return dm.ExecuteQuery("Select S.ID, S.ID ||'_'||S.PINO||'-'||S.CINO||'-'||S.EXPNO SHIPMENT from BIL_SHIPMENT S WHERE ISOPEN=1 ORDER BY S.ID DESC");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from BIL_DEDO WHERE ID=" + id);
    }

    public DataTable GetShipmentId(int shipmentId)
    {
        return dm.ExecuteQuery(@"SELECT D.*, S.ID SHIPMENT, B.BANKNAME FROM BIL_DEDO D
        INNER JOIN BIL_SHIPMENT S ON S.ID = D.SHIPMENTID
        INNER JOIN BIL_BANK B ON B.ID = D.BANKID
        WHERE D.SHIPMENTID=" + shipmentId);
    }
}