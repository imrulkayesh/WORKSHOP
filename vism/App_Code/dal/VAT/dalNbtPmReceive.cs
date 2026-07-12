using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtPmReceive
/// </summary>
public class dalNbtPmReceive:dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public DataTable GetEmpName(string p)
    {
        return dm.ExecuteQuery(@"SELECT EMPM_NAME FROM NBT_EMPM WHERE EMPM_CODE='" + p + "'");

    }

    public DataTable GetSuppInfo(string p)
    {
        return dm.ExecuteQuery(@"SELECT SUPM_NAME,SUPM_VATR,SUPM_ADDR FROM NBT_SUPM WHERE SUPM_ID='" + p + "'");

    }

    public DataTable GetData(string p)
    {
        return dm.ExecuteQuery(@"SELECT PMMR_CODE, PMMR_NAME,PMMR_PRCE FROM NBT_PMMR WHERE PMMR_CODE='" + p + "'");

    }

    public DataTable GetcurrNo()
    {
        return dm.ExecuteQuery(@"SELECT PMRM_ID,PMRM_SLNO,PMRM_INNO FROM NBT_PMRM  ORDER BY PMRM_ID DESC");

    }

    public bool SaveAll(string slNo, string inNo, string date, string time, string emp, string chNo, string chDate, string chTime, string dispDate, string dispTime,
        string comp, string supp, string supAdd, string supVatReg, string whId, DataTable dt)
    {
        OracleConnection myConnection = dm.GetDbConnection();
        myConnection.Open();
        OracleCommand myCommand = myConnection.CreateCommand();
        OracleTransaction myTrans = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        myCommand.Transaction = myTrans;
        try
        {
            dm.ExecuteNonQuery(@"INSERT INTO NBT_PMRM (PMRM_ID,PMRM_SLNO,PMRM_INNO,PMRM_DATE,PMRM_TIME,PMRM_EMPL,PMRM_CHNO,PMRM_CHDT,PMRM_CHTM,PMRM_DSDT,PMRM_DSTM,
                   PMRM_COMP,PMRM_SUPL,PMRM_SPAD,PMRM_SPVR,PMRM_WHID) VALUES(SQ_NBT_PMRM.NEXTVAL,'" + slNo + "','" + inNo + "',TO_DATE('" + date + "', 'DD/MM/YYYY'),'" + time + "','" + emp + "','" + chNo + "',TO_DATE('" + chDate + "', 'DD/MM/YYYY'),'" + chTime + "',TO_DATE('" + dispDate + "', 'DD/MM/YYYY'),'" + dispTime + "','" + comp + "','" + supp + "','" + supAdd + "','" + supVatReg + "','" + whId + "')");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dm.ExecuteNonQuery(@"Insert into NBT_PMRD(PMRM_SLNO,PMMR_CODE,PMRD_LINE,PMRD_QTNY,PMRD_PRWV,PMRD_VAT,PMRD_TOPR)
                    VALUES('" + slNo + "','" + dt.Rows[i][0] + "','" + (i + 1) + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][7] + "')");
            }
            myTrans.Commit();
            return true;
        }
        catch (Exception e)
        {
            myTrans.Rollback();
            return false;
        }
        finally
        {
            myConnection.Close();
        }
    }
}