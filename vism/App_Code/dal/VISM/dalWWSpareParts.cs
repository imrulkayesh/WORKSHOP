using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalWWSpareParts
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalWWSpareParts()
    {
    }
    public int Insert(int countryId, int workshop, int sparePartsId, int active)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_WWSPAREPARTS(COUNTRYID,WORKSHOPID,SPAREPARTSID,ACTIVE,CREATEDBY)
        VALUES('" + countryId + "','" + workshop + "','" + sparePartsId + "','" + active + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int countryId, int workshop, int sparePartsId, int active)
    {
        return dm.ExecuteNonQuery("update RVS_WWSPAREPARTS set COUNTRYID='" + countryId + "',WORKSHOPID='" + workshop + "',SPAREPARTSID='" + sparePartsId + "',ACTIVE='" + active + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public int Delete(int workshopid)
    {
        return dm.ExecuteNonQuery(@"DELETE FROM RVS_WWSPAREPARTS WHERE WORKSHOPID=" + workshopid);
    }

    public DataTable GetAll(int workshopId)
    {
        return dm.ExecuteQuery(@"SELECT WS.*, MS.SPARTSNAME FROM RVS_WWSPAREPARTS WS 
        INNER JOIN RVS_MASTERSPAREPARTS MS ON MS.ID = WS.SPAREPARTSID
        WHERE WS.WORKSHOPID=" + workshopId);
    }
    public DataTable loadSparePartslist(int workshopId)
    {
        return dm.ExecuteQuery(@"SELECT WS.*, MS.SPARTSNAME FROM RVS_WWSPAREPARTS WS 
        INNER JOIN RVS_MASTERSPAREPARTS MS ON MS.ID = WS.SPAREPARTSID
        WHERE WS.WORKSHOPID= " + workshopId);
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_WWSPAREPARTS WHERE ID=" + id);
    }

    public bool CheckDuplicate(int countryId, int workshop, int sparePartsId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_WWSPAREPARTS WHERE COUNTRYID='" + countryId + "' AND WORKSHOPID='" + workshop + "' AND SPAREPARTSID='" + sparePartsId + "'");
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