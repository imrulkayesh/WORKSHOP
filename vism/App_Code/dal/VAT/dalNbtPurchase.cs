using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtPurchase
/// </summary>
public class dalNbtPurchase:dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public DataTable GetSuppInfo(string p)
    {
        return dm.ExecuteQuery(@"SELECT SUPM_NAME,SUPM_VATR,SUPM_ADDR FROM NBT_SUPM WHERE SUPM_ID='" + p + "'");

    }

    public DataTable GetEmpName(string p)
    {
        return dm.ExecuteQuery(@"SELECT EMPM_NAME FROM NBT_EMPM WHERE EMPM_CODE='" + p + "'");

    }

    public DataTable GetcurrNo()
    {
        return dm.ExecuteQuery(@"SELECT PURM_ID,PURM_SLNO FROM NBT_PURM ORDER BY PURM_ID DESC");
    }

    public bool SaveAll(string slNo, string cateid, string catename, string empId, string empName, string chlNo, string chlDate, string chlTime, string entryDate, string disDate, string disTime,
        string supId, string supName, string supAdd, string supRgNo, string whId, string whName, DataTable dt)
    {
        OracleConnection conn = dm.GetDbConnection();
        conn.Open();
        OracleCommand cmd = conn.CreateCommand();
        OracleTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        cmd.Transaction = trans;
        try
        {
            cmd.CommandText = @"INSERT INTO NBT_PURM (PURM_ID,PURM_SLNO,CATM_ID,CATM_NAME,EMPM_ID,EMPM_NAME,PURM_CHNO,PURM_CHDT,PURM_CHTM,PURM_DATE,PURM_DSDT,PURM_DSTM,
                   SUPM_ID,SUPM_NAME,SUPM_ADDR,SUPM_VTRG,WARM_ID,WARM_NAME) VALUES
                              (SQ_NBT_PURM.NEXTVAL,'" + slNo + "','" + cateid + "','" + catename + "','" + empId + "','" + empName +
                                    "','" + chlNo + "',TO_DATE('" + chlDate + "', 'DD/MM/YYYY'),'" + chlTime
                                     + "',TO_DATE('" + entryDate + "', 'DD/MM/YYYY'),TO_DATE('" + disDate + "', 'DD/MM/YYYY'),'" + disTime + "','" + supId + "','" + supName + "','" + supAdd +
                                    "','" + supRgNo + "','" + whId + "','" + whName + "')";
            cmd.ExecuteNonQuery();
            bool flag = true;
            string compId = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (flag)
                {
                    DataTable dtt = dm.ExecuteQuery(@"select COMP_ID FROM NBT_COMP WHERE COMP_NAME='" + dt.Rows[i][2] + "' ");
                    compId = dtt.Rows[0][0].ToString();
                    flag = false;
                }

                cmd.CommandText =
                    @"INSERT INTO NBT_PURD(PURM_SLNO,ITEM_CODE,ITEM_NAME,COMP_ID,COMP_NAME,UNIT_NAME,ITEM_PRCE,ITEM_QTY,ITEM_NTPR,ITEM_VAT,ITEM_TOVT,ITEM_TOPR)VALUES('" +
                    slNo + "','" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + compId + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][5] +
                    "','" + dt.Rows[i][6] + "','" + dt.Rows[i][7] + "','" + dt.Rows[i][8] + "','" + dt.Rows[i][9] + "')";
                cmd.ExecuteNonQuery();
                cmd.CommandText =
                    @"UPDATE NBT_ITEM SET TOT_REC_QTY=TOT_REC_QTY+'" + dt.Rows[i][5] + "' WHERE ITEM_CODE='" + dt.Rows[i][0] + "' AND COMP_ID='" + compId + "'";
                cmd.ExecuteNonQuery();
            }
            trans.Commit();
            return true;
        }
        catch (Exception e)
        {
            trans.Rollback();
            return false;
        }
        finally
        {
            conn.Close();
        }
    }

    public bool RowCount(int yyyy)
    {

        DataTable dt = new DataTable();
        dt = dm.ExecuteQuery(@"select 1 from nbt_exrm where trunc(EXRM_DATE) = TO_DATE('01-07-" + yyyy + "','dd-MM-yyyy')");
        if (dt.Rows.Count == 0)
        {
            return true;
        }
        return false;
    }
    public DataTable GetDdlDataOnCondition(string cate)
    {
        return dm.ExecuteQuery(@"SELECT ITEM_NAME,ITEM_CODE FROM NBT_ITEM WHERE CATM_ID='" + cate + "' AND ITEM_STAT='" + 1 + "'");
    }

    public DataTable GetItemInfo(string code)
    {
        return dm.ExecuteQuery(@"SELECT I.ITEM_NAME, C.COMP_NAME,U.UNIT_NAME FROM NBT_ITEM I
INNER JOIN NBT_COMP C ON I.COMP_ID=C.COMP_ID
INNER JOIN NBT_UNIT U ON I.UNIT_ID=U.UNIT_ID WHERE I.ITEM_CODE='" + code + "'");
    }

}