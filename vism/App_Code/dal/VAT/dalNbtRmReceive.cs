using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtRmReceive
/// </summary>
public class dalNbtRmReceive:dalNbtCommon
{
    
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public DataTable GetData(string p)
    {
        return dm.ExecuteQuery(@"SELECT RMMR_CODE, RMMR_NAME,RMMR_PRCE FROM NBT_RMMR WHERE RMMR_CODE='" + p + "'");

    }
    
    public DataTable GetEmpName(string p)
    {
        return dm.ExecuteQuery(@"SELECT EMPM_NAME FROM NBT_EMPM WHERE EMPM_CODE='" + p + "'");
    }

    public DataTable GetSuppInfo(string p)
    {
        return dm.ExecuteQuery(@"SELECT SUPM_NAME,SUPM_VATR,SUPM_ADDR FROM NBT_SUPM WHERE SUPM_ID='" + p + "'");
    }

    public DataTable GetcurrNo()
    {
        return dm.ExecuteQuery(@"SELECT RMRM_ID,RMRM_SLNO,RMRM_INNO FROM NBT_RMRM  ORDER BY RMRM_ID DESC");
    }

    public bool SaveAll(string slNo, string inNo, string date, string time, string emp, string chNo, string chDate, string chTime, string dispDate, string dispTime,
        string comp, string supp, string supAdd, string supVatReg, string whId, DataTable dt)
    {
//        try
//        {
//            dm.ExecuteNonQuery(@"INSERT INTO NBT_RMRM (RMRM_ID,RMRM_SLNO,RMRM_INNO,RMRM_DATE,RMRM_TIME,RMRM_EMPL,RMRM_CHNO,RMRM_CHDT,RMRM_CHTM,RMRM_DSDT,RMRM_DSTM,
//                   RMRM_COMP,RMRM_SUPL,RMRM_SPAD,RMRM_SPVR,RMRM_WHID) VALUES(SQ_NBT_RMRM.NEXTVAL,'" + slNo + "','" + inNo + "',TO_DATE('" + date + "', 'DD/MM/YYYY'),'" + time + "','" + emp + "','" + chNo + "',TO_DATE('" + chDate + "', 'DD/MM/YYYY'),'" + chTime + "',TO_DATE('" + dispDate + "', 'DD/MM/YYYY'),'" + dispTime + "','"+comp+"','"+supp+"','"+supAdd+"','"+supVatReg+"','"+whId+"')");
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                dm.ExecuteNonQuery(@"Insert into NBT_RMRD(RMRM_SLNO,RMMR_CODE,RMRD_LINE,RMRD_QTNY,RMRD_PRWV,RMRD_VAT,RMRD_TOPR)
//        VALUES('" + slNo + "','" + dt.Rows[i][0] + "','" + (i + 1) + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][7] + "')");
//            }
//            return true;
//        }
//        catch (Exception)
//        {

//            return false;
//        }
        string myConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        OracleConnection myConnection = new OracleConnection(myConnString);
        myConnection.Open();
        OracleCommand myCommand = myConnection.CreateCommand();
        OracleTransaction myTrans = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        myCommand.Transaction = myTrans;
        try
        {
            dm.ExecuteNonQuery(@"INSERT INTO NBT_RMRM (RMRM_ID,RMRM_SLNO,RMRM_INNO,RMRM_DATE,RMRM_TIME,RMRM_EMPL,RMRM_CHNO,RMRM_CHDT,RMRM_CHTM,RMRM_DSDT,RMRM_DSTM,
                   RMRM_COMP,RMRM_SUPL,RMRM_SPAD,RMRM_SPVR,RMRM_WHID) VALUES(SQ_NBT_RMRM.NEXTVAL,'" + slNo + "','" + inNo + "',TO_DATE('" + date + "', 'DD/MM/YYYY'),'" + time + "','" + emp + "','" + chNo + "',TO_DATE('" + chDate + "', 'DD/MM/YYYY'),'" + chTime + "',TO_DATE('" + dispDate + "', 'DD/MM/YYYY'),'" + dispTime + "','" + comp + "','" + supp + "','" + supAdd + "','" + supVatReg + "','" + whId + "')");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dm.ExecuteNonQuery(@"Insert into NBT_RMRD(RMRM_SLNO,RMMR_CODE,RMRD_LINE,RMRD_QTNY,RMRD_PRWV,RMRD_VAT,RMRD_TOPR)
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