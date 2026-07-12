using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalAdpositions
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalAdpositions()
    {
    }
    public int Insert(int catid, string adposition, string remark)
    {
        return dm.ExecuteNonQuery(@"Insert into MD_ADPOSITIONS(ID,CATID,ADPOSITION,REMARKS,CREATEDBY)
        VALUES(SQ_MD_ADPOSITIONS.NEXTVAL,'" + catid + "','" + adposition + "','" + remark + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, int catid, string adposition, string remark)
    {
        return dm.ExecuteNonQuery("update MD_ADPOSITIONS set CATID='" + catid + "',ADPOSITION='"+adposition+"',REMARKS='" + remark + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetAllCAT( int catid)
    {
        return dm.ExecuteQuery(@"Select AP.*, MC.CATNAME from MD_ADPOSITIONS AP 
        INNER JOIN MD_MEDIACATEGORY MC ON MC.ID = AP.CATID WHERE CATID="+catid);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MD_ADPOSITIONS WHERE ID=" + id);
    }
    public bool CheckDuplicate(int catid, string adposition)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MD_ADPOSITIONS WHERE CATID='"+catid+ "' AND ADPOSITION='" + adposition + "'");
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
