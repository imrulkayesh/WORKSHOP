using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalMLine
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMLine()
    {
    }
    public int Insert(int locationId, int businesscenter, int departmentId, string name, string discription, string building, string floor, string mobile)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MLINE(ID,LOCATIONID,BUSINESSUNITID,DEPARTMENTID,LINENAME,DISCRIPTION,BUILDINGNO,FLOORNAME,MOBILE,CREATEDBY)
        VALUES(SQ_PR_MLINE.NEXTVAL,'" + locationId + "','" + businesscenter + "','" + departmentId + "','" + name + "','" + discription + "','" + building + "','" + floor + "','" + mobile + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int locationId, int businesscenter, int departmentId, string name, string discription, string building, string floor, string mobile)
    {
        return dm.ExecuteNonQuery("update PR_MLINE set LOCATIONID='" + locationId + "',BUSINESSUNITID='" + businesscenter + "',DEPARTMENTID='" + departmentId + "',LINENAME='" + name + "',DISCRIPTION='" + discription + "',BUILDINGNO='" + building + "',FLOORNAME='" + floor + "',MOBILE='" + mobile + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MLINE WHERE ID=" + id);
    }

    public DataTable GetAllLine(int id)
    {
        return dm.ExecuteQuery(@"SELECT L.*, B.CENTERNAME, C.COMPANYNAME, D.DEPARTMENTNAME FROM PR_MLINE L
        INNER JOIN BS_BUSINESSCENTER B ON B.ID = L.LOCATIONID
        INNER JOIN BS_COMPANY C ON C.ID = L.BUSINESSUNITID
        INNER JOIN BS_DEPARTMENT D ON D.ID = L.DEPARTMENTID WHERE L.LOCATIONID=" + id);
    }
    public bool CheckDuplicate(int locationId, string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MLINE WHERE LOCATIONID='" + locationId + "' AND LINENAME='" + name + "'");
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