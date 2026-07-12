using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtFpReceive
/// </summary>
public class dalNbtFpReceive : dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public DataTable GetData(string p)
    {
        return dm.ExecuteQuery(@"SELECT ITEM_CODE, ITEM_NAME,ITEM_PRCE FROM NBT_ITEM WHERE ITEM_CODE='" + p + "'");
    }

    public DataTable GetcurrNo()
    {
        return dm.ExecuteQuery(@"SELECT FPRM_ID,FPRM_SLNO,FPRM_INNO FROM NBT_FPRM  ORDER BY FPRM_ID DESC");
    }

    public DataTable GetEmpName(string p)
    {
        return dm.ExecuteQuery(@"SELECT EMPM_NAME FROM NBT_EMPM WHERE EMPM_CODE='"+p+"'");
    }

    public bool SaveAll(string fpRecSlno, string internalo, string recDate, string recTime, string empId, string recForm, string comp, string whId, DataTable dt)
    {
        try
        {
            dm.ExecuteNonQuery(@"INSERT INTO NBT_FPRM (FPRM_ID,FPRM_SLNO,FPRM_INNO,FPRM_DATE,FPRM_TIME,FPRM_EMPL,FPRM_FROM,FPRM_COMP,FPRM_WHID) VALUES(SQ_NBT_FPRM.NEXTVAL,'" + fpRecSlno + "','" + internalo + "',TO_DATE('" + recDate + "', 'DD/MM/YYYY'),'" + recTime + "','" + empId + "','" + recForm + "','" + comp + "','" + whId + "')");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dm.ExecuteNonQuery(@"Insert into NBT_FPRD(FPRD_SLNO, FPRD_CODE,FPRD_NAME,FPRD_UNPR,FPRD_QNTY,FPRD_TPRC,FPRD_LINE)
        VALUES('" + fpRecSlno + "','" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][4] + "','" + (i + 1) + "')");
            }
            return true;
        }
        catch (Exception)
        {

            return false;
        }
        
    }
}