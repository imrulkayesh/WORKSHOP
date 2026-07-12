using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLegalPlace
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalLegalPlace()
    {
    }
    public int Insert(string place, int country, int division, int district, int thana, string remarks)
    {
        return dm.ExecuteNonQuery(@"Insert into LEG_PLACE(ID,PLACENAME,COUNTRYID,DIVISION,DISTRICT,THANA,REMARKS,CREATEDBY)
        VALUES(SQ_LEG_PLACE.NEXTVAL,'" + place + "','" + country + "','" + division + "','" + district + "','" + thana + "','" + remarks + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string place, int country, int division, int district, int thana, string remarks)
    {
        return dm.ExecuteNonQuery("update LEG_PLACE set PLACENAME='" + place + "', COUNTRYID='" + country + "', DIVISION='" + division + "', DISTRICT='" + district + "', THANA='" + thana + "', REMARKS='" + remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from LEG_PLACE WHERE ID=" + id);
    }
    public DataTable GetAllPlace(int country)
    {
        return dm.ExecuteQuery(@"Select L.*, C.COUNTRYCODE, C.COUNTRYNAME, D.DIVISIONNAME, DI.DISTRICTNAME, T.THANANAME from LEG_PLACE L
        INNER JOIN BS_COUNTRY C ON C.ID = L.COUNTRYID
        INNER JOIN BS_DIVISION D ON D.ID = L.DIVISION
        INNER JOIN BS_DISTRICT DI ON DI.ID = L.DISTRICT
        INNER JOIN BS_THANA T ON T.ID = L.THANA where L.COUNTRYID='" + country + "'");
    }
    public bool CheckDuplicate(string place)
    {
        DataTable dt = dm.ExecuteQuery("Select * from LEG_PLACE WHERE PLACENAME='" + place + "'");
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