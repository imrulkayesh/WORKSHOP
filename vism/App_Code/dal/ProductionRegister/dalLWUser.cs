using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalLWUser
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalLWUser()
	{
	}
    public int Insert(int locationId, int lineId, string staffId, string name, string mobile, string email)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_USERWISELP(ID,LOCATIONID,LINEID,STAFFID,EMPLOYEENAME,OFFICEMOBILE,OFFICEMAIL,CREATEDBY)
        VALUES(SQ_PR_USERWISELP.NEXTVAL,'" + locationId + "','" + lineId + "','" + staffId + "','" + name + "'," + mobile + ",'" + email + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public int Update(int id, int locationId, int lineId, string staffId, string name, string mobile, string email)
    {
        return dm.ExecuteNonQuery(@"update PR_USERWISELP set LOCATIONID='" + locationId + "', LINEID='" + lineId + "',STAFFID='" + staffId + "',EMPLOYEENAME='" + name + "',OFFICEMOBILE='" + mobile + "',OFFICEMAIL='" + email + "' ,UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,UL.LOCATIONID, UL.LINEID, UL.STAFFID, B.CENTERNAME FROM PR_USERWISELP UL
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=UL.LOCATIONID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=UL.STAFFID
        INNER JOIN PR_MLINE L ON L.ID = UL.LINEID
        WHERE UL.ID=" + id);
    }
    public DataTable GetAll(int id)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,UP.ID, UP.STAFFID,UP.LOCATIONID ,B.CENTERNAME,ML.LINENAME FROM PR_USERWISELP UP  
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=UP.LOCATIONID
        INNER JOIN PR_MLINE ML ON ML.ID = UP.LINEID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=UP.STAFFID
        WHERE UP.LOCATIONID=" + id);
    }
    public bool CheckDuplicate(int lineid, string staffId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_USERWISELP WHERE LINEID='" + lineid + "' AND STAFFID='" + staffId + "'");
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