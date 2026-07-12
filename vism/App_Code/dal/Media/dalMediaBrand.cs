using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMediaBrand
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMediaBrand()
    {
    }
    public int Insert(int gcid, int compid, string brand, string resperson)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_BRAND(ID,GCID,COMPANYID,BRANDNAME,RESPERSON,CREATEDBY)
        VALUES(SQ_MD_BRAND.NEXTVAL,'" + gcid + "','" + compid + "','" + brand + "','" + resperson + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int gcid, int compid, string brand, string resperson)
    {
        return dm.ExecuteNonQuery("update MD_BRAND set GCID='" + gcid + "',COMPANYID='" + compid + "',BRANDNAME='" + brand + "',RESPERSON='" + resperson + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllBrand(int gcid, int compid)
    {
        return dm.ExecuteQuery(@"Select B.*, GC.COMPANYTITLE, C.COMPANYNAME from MD_BRAND B
        INNER JOIN BS_GROUPOFCOMPANY GC ON GC.ID = B.GCID
        INNER JOIN BS_COMPANY C ON C.ID = B.COMPANYID WHERE B.GCID='" + gcid + "' AND B.COMPANYID='" + compid + "'");
    }
    public DataTable GetAll(int gcid)
    {
        return dm.ExecuteQuery(@"Select B.*, GC.COMPANYTITLE, C.COMPANYNAME from MD_BRAND B
        INNER JOIN BS_GROUPOFCOMPANY GC ON GC.ID = B.GCID
        INNER JOIN BS_COMPANY C ON C.ID = B.COMPANYID WHERE B.GCID='" + gcid + "'");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_BRAND WHERE ID=" + id);
    }
    public bool CheckDuplicate(string brand)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_BRAND WHERE BRANDNAME='" + brand + "'");
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
