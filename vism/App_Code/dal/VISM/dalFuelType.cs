using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalFuelType
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalFuelType()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_FUELTYPE(ID,FUELTYPENAME)
        VALUES(SQ_RVS_FUELTYPE.NEXTVAL,'" + name + "')");
    }

    public int InsertFDMND(string VID,string FId, string PumpId,decimal qty,decimal rate, int sendDI,int sendBI,string rmk)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_FUELDMD(ID,VEHICLEID,FUELID,PUMPID,QTY,CREATEDBY,STATUSID,TRANSACTIONID,RATE,SENDDEPARTMENTID,SENDBUSINESSCENTERID,REMARKS)
        VALUES(SQ_RVS_FUELDMD.NEXTVAL,'" + VID + "','" + FId + "','" + PumpId + "','" + qty + "','" + SessionManager.SessionName.UserId + "','1',SQ_RVS_FUELDMD.NEXTVAL,'"+rate+"','"+ sendDI + "','"+ sendBI + "','"+rmk+"' )");
    }

    public void UpdateFd(string iD, string qty)
    {
        dm.ExecuteNonQuery("UPDATE RVS_FUELDMD SET AQTY ='" + qty + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',STATUSID='2',UPDATEDATE=SYSDATE WHERE ID=" + iD);
    }

    public void UpdateFG(string iD, string qty)
    {
        dm.ExecuteNonQuery("UPDATE RVS_FUELDMD SET GQTY ='" + qty + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',STATUSID='3',UPDATEDATE=SYSDATE,CODATE=TO_DATE(SYSDATE, 'DD/MM/YY') WHERE ID=" + iD);
    }

    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_FUELTYPE set FUELTYPENAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_FUELTYPE WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_FUELTYPE WHERE FUELTYPENAME='" + name + "'");
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