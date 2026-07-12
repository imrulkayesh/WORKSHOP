using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMedia
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMedia()
    {
    }
    public int Insert(int paycomid,int catid, string media, string address, string contractperson, string mobile, string phone, string fax, string email, string web)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_MEDIA(ID,PAYMENTCOMID,CATID,MEDIANAME,ADDRESS,CONTACTPERSON,MOBILE,PHONE,FAX,EMAIL,WEBSITE,CREATEDBY)
        VALUES(SQ_MD_MEDIA.NEXTVAL,'" + paycomid + "','"+catid+"','" + media + "','" + address + "','" + contractperson + "','" + mobile + "','" + phone + "','" + fax + "','" + email + "','" + web + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int paycomid, int catid, string media, string address, string contractperson, string mobile, string phone, string fax, string email, string web)
    {
        return dm.ExecuteNonQuery("update MD_MEDIA set PAYMENTCOMID='" + paycomid + "',CATID='" + catid + "',MEDIANAME='" + media + "',ADDRESS='" + address + "',CONTACTPERSON='" + contractperson + "',MOBILE='" + mobile + "',PHONE='" + phone + "',FAX='" + fax + "',EMAIL='" + email + "',WEBSITE='" + web + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllMedia(int paycomid,int catid)
    {
        return dm.ExecuteQuery(@"Select M.*, MC.CATNAME, PC.COMPANYNAME from MD_MEDIA M 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = M.CATID
        INNER JOIN MD_PAYMENTCOMPANY PC ON PC.ID = M.PAYMENTCOMID WHERE M.CATID='"+paycomid+"' AND M.PAYMENTCOMID='"+catid+"' ");
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_MEDIA WHERE ID=" + id);
    }
    public bool CheckDuplicate(string media)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_MEDIA WHERE MEDIANAME='" + media + "'");
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
