using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalGateman
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalGateman()
    {
    }
    public int Insert(string staffId, int gateno, int centerid)
    {
        return dm.ExecuteNonQuery(@"Insert into DP_ASSIGNGATEMAN(ID,STAFFID,GATENO,BUSINESSCENTERID,CREATEDBY)
        VALUES(SQ_DP_ASSIGNGATEMAN.NEXTVAL,'" + staffId + "','" + gateno + "','" + centerid + "','" + SessionManager.SessionName.UserId + "')");
    }
    public int Update(int id, string staffId, int gateno, int centerid)
    {
        return dm.ExecuteNonQuery("update DP_ASSIGNGATEMAN set STAFFID='" + staffId + "',GATENO='" + gateno + "',BUSINESSCENTERID='" + centerid + "',UPDATEDBY='" + SessionManager.SessionName.UserId + "', UPDATEDATE= SYSDATE where ID=" + id);
    }
    public int Delete(int id)
    {
        return dm.ExecuteNonQuery("update DP_ASSIGNGATEMAN set ISACTIVE=0,ENDDATE=SYSDATE, UPDATEDATE= SYSDATE where ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DS.DESIGNATIONNAME FROM DP_ASSIGNGATEMAN A  INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
        INNER JOIN EM_EMPLOYEE E ON E.USERID=A.STAFFID INNER JOIN BS_DESIGNATION DS
        ON DS.ID=E.DESIGNATIONID where  ISACTIVE=1 and A.ID=" + id);
    }
    public DataTable GetAll(int businessCenterId)
    {
        return dm.ExecuteQuery(@"SELECT E.EMPLOYEENAME,E.OFFICEMOBILE,E.OFFICEMAIL,A.*,B.CENTERNAME,DS.DESIGNATIONNAME FROM DP_ASSIGNGATEMAN A  INNER JOIN BS_BUSINESSCENTER B ON B.ID=A.BUSINESSCENTERID
INNER JOIN EM_EMPLOYEE E ON E.USERID=A.STAFFID INNER JOIN BS_DESIGNATION DS
ON DS.ID=E.DESIGNATIONID where  A.ISACTIVE=1 and A.BUSINESSCENTERID=" + businessCenterId);
    }

}