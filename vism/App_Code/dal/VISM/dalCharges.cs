using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalCharges
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalCharges()
    {
    }
    public int Insert(int catgid, int workshopid, int chargeid, decimal amount, string remark, int locid, int currencyid)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_CHARGES(ID,CATEGORYID,WORKSHOPID,CHARGEID,AMOUNT,REMARKS,BUSINESSCENTERID,CURRENCYID,CREATEDBY)
        VALUES(SQ_RVS_CHARGES.NEXTVAL,'" + catgid + "','" + workshopid + "','" + chargeid + "','" + amount + "','" + remark + "','" + locid + "','" + currencyid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int catgid, int workshopid, int chargeid, decimal amount, string remark, int locid, int currencyid)
    {
        return dm.ExecuteNonQuery("update RVS_CHARGES set CATEGORYID='" + catgid + "',WORKSHOPID='" + workshopid + "',CHARGEID='" + chargeid + "',AMOUNT='" + amount + "',REMARKS='" + remark + "',BUSINESSCENTERID='" + locid + "',CURRENCYID='" + currencyid + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_CHARGES WHERE ID=" + id);
    }
    public DataTable GetAll(int workshopid, int locid, int catid)
    {
        return dm.ExecuteQuery(@"Select C.*, W.WORKSHOP, SC.CATNAME,B.CENTERCODE, B.CENTERNAME,CN.CHARGENAME,CURRENCYCODE from RVS_CHARGES C 
        INNER JOIN RVS_WORKSHOP W ON W.ID= C.WORKSHOPID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID= C.BUSINESSCENTERID
        INNER JOIN RVS_CHARGENAME CN ON CN.ID=C.CHARGEID
        INNER JOIN ASSET_CURRENCY CR ON CR.ID=C.CURRENCYID
        INNER JOIN RVS_SERVICECATEGORY SC ON SC.ID= C.CATEGORYID WHERE C.WORKSHOPID='" + workshopid + "' AND C.BUSINESSCENTERID='" + locid + "' AND C.CATEGORYID=" + catid);
    }

    public bool CheckDuplicate(int catgid, int workshopid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_CHARGES WHERE TEAMID='" + catgid + "' AND WORKSHOPID='" + workshopid + "'");
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