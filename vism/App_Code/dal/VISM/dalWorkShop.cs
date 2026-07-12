using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalWorkShop
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalWorkShop()
    {
    }
    public int Insert(string wshop, int buscenter, string inchstaffid, string address, string website, string email, string mobile, string phone, string fax)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_WORKSHOP(ID,WORKSHOP,BUSINESSCENTERID,INCHARGESTAFFID,ADDRESS,WEBSITE,EMAIL,MOBILE,PHONE,FAX,CREATEDBY)
        VALUES(SQ_RVS_WORKSHOP.NEXTVAL,'" + wshop + "','" + buscenter + "','" + inchstaffid + "','" + address + "','" + website + "','" + email + "','" + mobile + "','" + phone + "','" + fax + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string wshop, int buscenter, string inchstaffid, string address, string website, string email, string mobile, string phone, string fax)
    {
        return dm.ExecuteNonQuery("update RVS_WORKSHOP set WORKSHOP='" + wshop + "',BUSINESSCENTERID='" + buscenter + "',INCHARGESTAFFID='" + inchstaffid + "',ADDRESS='" + address + "',WEBSITE='" + website + "',EMAIL='" + email + "',PHONE='" + phone + "',FAX='" + fax + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_WORKSHOP WHERE ID=" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery("Select WS.*, BC.CENTERNAME from RVS_WORKSHOP WS INNER JOIN BS_BUSINESSCENTER BC ON BC.ID=WS.BUSINESSCENTERID WHERE WS.BUSINESSCENTERID=" + id);
    }
    public bool CheckDuplicate(string name, int buscenter)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_WORKSHOP WHERE WORKSHOP='" + name + "' AND BUSINESSCENTERID=" + buscenter);
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