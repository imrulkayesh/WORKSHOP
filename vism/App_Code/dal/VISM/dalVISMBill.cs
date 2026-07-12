using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalVISMBill
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalVISMBill()
    {
    }
    public void Update(int jobID, int IsOpen, decimal totalPaid, int currencyId, int paytypeid, decimal discount, decimal netamount, decimal custPaid, decimal companyPaid, int companyId, decimal empPaid, string empStaffId, string Remarks)
    {
        dm.ExecuteNonQuery(@"UPDATE RVS_BILL SET ISOPEN='" + IsOpen + "', PAIDAMOUNT='" + totalPaid + "',CURRENCYID='" + currencyId + "',PAYTYPEID='" + paytypeid + "',DISCOUNT='" + discount + "',NETAMOUNT='" + netamount + "',CUSTOMERPAID='" + custPaid + "',COMPANYPAID='" + companyPaid + "',PAIDCOMPANY='" + companyId + "',EMPPAID='" + empPaid + "',PAIDBYEMP='" + empStaffId + "',REMARKS='" + Remarks + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE JOBID='" + jobID + "'");
    }

    public DataTable GetById(int ID)
    {
        return dm.ExecuteQuery(@"SELECT * FROM RVS_BILL B
        INNER JOIN RVS_JOBCARD J ON J.ID=B.JOBID
        INNER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID
        WHERE W.BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "' AND B.ISOPEN=1 AND B.JOBID=" + ID);
    }
    public DataTable GetByJobID(int id)
    {
        return dm.ExecuteQuery(@"SELECT B.*,P.CORDER, P.PAYTYPE, C.CURRENCYCODE, C.CURRENCYNAME, CO.COMPANYNAME, W.WORKSHOP, W.ADDRESS FROM RVS_BILL B
        LEFT OUTER JOIN RVS_JOBCARD J ON J.ID=B.JOBID
        LEFT OUTER JOIN RVS_PAYTYPE P ON P.ID = B.PAYTYPEID
        LEFT OUTER JOIN ASSET_CURRENCY C ON P.ID = B.CURRENCYID
        LEFT OUTER JOIN BS_COMPANY CO ON CO.ID = B.COMPANYPAID
        LEFT OUTER JOIN RVS_WORKSHOP W ON W.ID=J.WORKSHOPID WHERE B.JOBID=" + id);
    }

    public DataTable GetAllBILL()
    {
        return dm.ExecuteQuery(@"SELECT * FROM VW_BILLS  WHERE ISOPEN=1 AND BUSINESSCENTERID='" + SessionManager.SessionName.BusinessCentetid + "'");
    }
}