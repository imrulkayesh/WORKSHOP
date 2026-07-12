using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMMould
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMMould()
    {
    }
    public int Insert(string machine, string mcode, int noOfCavity, double scycleTime, int shiftLength, int targetPerShift, decimal sinputgm, decimal soutputgm)
    {
        return dm.ExecuteNonQuery(@"Insert into PR_MMOULD(ID,MOULDNAME,MCODE,NOOFCAVITY,SCYCLETIME,SHIFTLENGTHM,TARGETPERSHIFT,SINPUTGM,SOUTPUTGM,CREATEDBY)
        VALUES(SQ_PR_MMOULD.NEXTVAL,'" + machine + "','" + mcode + "','" + noOfCavity + "','" + scycleTime + "','" + shiftLength + "','" + targetPerShift + "','" + sinputgm + "','" + soutputgm + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public int Update(int id, string machine, string mcode, int noOfCavity, int scycleTime, int shiftLength, int targetPerShift, decimal sinputgm, decimal soutputgm)
    {
        return dm.ExecuteNonQuery("update PR_MMOULD set MOULDNAME='" + machine + "',MCODE='" + mcode + "',NOOFCAVITY='" + noOfCavity + "',SCYCLETIME='" + scycleTime + "',SHIFTLENGTHM='" + shiftLength + "',TARGETPERSHIFT='" + targetPerShift + "',SINPUTGM='" + sinputgm + "',SOUTPUTGM='" + soutputgm + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from PR_MMOULD WHERE ID=" + id);
    }
    public bool CheckDuplicate(string machine)
    {
        DataTable dt = dm.ExecuteQuery("Select * from PR_MMOULD WHERE MOULDNAME='" + machine + "'");
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