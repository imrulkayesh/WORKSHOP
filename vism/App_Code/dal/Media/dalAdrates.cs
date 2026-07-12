using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalAdrates
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalAdrates()
    {
    }
    public int Insert(int catid, int adpositionid, int mediaid, int colorid, decimal amount, int currency, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_ADRATES(ID,CATID,ADPOSITIONID,MEDIAID,COLORID,AMOUNT,CURRENCYID,REMARKS,CREATEDBY)
        VALUES(SQ_MD_ADRATES.NEXTVAL,'" + catid + "','" + adpositionid + "','" + mediaid + "','" + colorid + "','" + amount + "','" + currency + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int catid, int adpositionid, int mediaid, int colorid, decimal amount, int currency, string remarks)
    {
        return dm.ExecuteNonQuery("update MD_ADRATES set CATID='" + catid + "',ADPOSITIONID='" + adpositionid + "',MEDIAID='" + mediaid + "',COLORID='" + colorid + "',AMOUNT='" + amount + "',CURRENCYID='" + currency + "',REMARKS='" + remarks + "', UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_ADRATES WHERE ID=" + id);
    }
    public DataTable GetAllPosition(int catid, int adpositionid)
    {
        return dm.ExecuteQuery(@"Select A.*,MC.CATNAME, AP.ADPOSITION, M.MEDIANAME, C.COLORNAME, AC.CURRENCYCODE, AC.CURRENCYNAME from MD_ADRATES A 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = A.CATID
        INNER JOIN MD_ADPOSITIONS AP ON AP.ID = A.ADPOSITIONID
        INNER JOIN MD_MEDIA M ON M.ID = A.MEDIAID
        INNER JOIN MD_COLOR C ON C.ID = A.COLORID
        INNER JOIN ASSET_CURRENCY AC ON AC.ID = A.CURRENCYID where A.CATID='"+catid+ "' AND A.ADPOSITIONID='"+adpositionid+"'");
    }
    public bool CheckDuplicate(int catid , int adpositionid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_ADRATES WHERE CATID='" + catid + "' AND adpositionid=" + adpositionid);
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