using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtChallan
/// </summary>
public class dalNbtChallan : dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();



    public DataTable GetData(string code)
    {
        return dm.ExecuteQuery(@"SELECT IT.ITEM_ID,IT.ITEM_NAME,UN.UNIT_NAME,IT.ITEM_PRCE,IT.ITEM_VAT FROM NBT_ITEM IT
INNER JOIN NBT_UNIT UN ON IT.ITEM_UNIT=UN.UNIT_ID WHERE ITEM_CODE='" + code + "'");
    }

    public DataTable GetCusCode(string table)
    {
        return dm.ExecuteQuery("Select  FROM " + table);
    }

    public DataTable GetDistrInfo(string p)
    {
        return
            dm.ExecuteQuery(
                @"SELECT C.CUSM_NAME,C.CUSM_ADD1,C.CUSM_ADD2,C.CUSM_ADD3,C.CUSM_DESN,C.CUSM_VATR FROM NBT_CUSM C WHERE CUSM_CODE='" +
                p + "'");
    }

    public DataTable GetIntVtChNo()
    {
        return dm.ExecuteQuery(@"SELECT CHLM_ID,CHLM_IVCN,CHLM_EVCN FROM NBT_CHLM  ORDER BY CHLM_ID DESC");

    }

    public bool SaveAll(string newInVtChNo, string newVtChNo, string chDate, string chTime, string dispDate,
        string dispTime, string distCode,
        string distName, string add1, string add2, string add3, string destn, string cusVatId, string trukNo,
        string whId, string compId, string chType,DataTable dt)
    {
        try
        {
            dm.ExecuteNonQuery(@"Insert into NBT_CHLM(CHLM_ID, CHLM_IVCN,CHLM_EVCN,CHLM_CHDT, CHLM_CHTM, CHLM_DIDT, CHLM_DITM,CHLM_DSCD,
                    CHLM_DSNM,CHLM_ADD1,CHLM_ADD2,CHLM_ADD3,CHLM_DESN,CHLM_CVIN,CHLM_TRNO,CHLM_WHID,CHLM_COMP,CHLM_TYPE)
        VALUES(SQ_NBT_CHLM.NEXTVAL,'" + newInVtChNo + "','" + newVtChNo + "', TO_DATE('" + chDate + "','dd/MM/yyyy'),'" + chTime + "', TO_DATE('" + dispDate + "','dd/MM/yyyy'),'" + dispTime + "','" + distCode + "','" + distName + "','" + add1 + "','" + add2 + "','" + add3 + "','" + destn + "','" +
                               cusVatId + "','" + trukNo + "','" + whId + "','" + compId + "','" + chType + "')");

        for (int i = 0; i < dt.Rows.Count; i++)
                {

            dm.ExecuteNonQuery(@"Insert into NBT_CHLD(CHLD_IVCN, CHLD_PRCD,CHLD_NAME,CHLD_UNIT, CHLD_QNTY, CHLD_UNPR,CHLD_LINE,CHLD_TOVT,CHLD_TOPR)
        VALUES('" + newInVtChNo + "','" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" + dt.Rows[i][2] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][4] + "','" + (i + 1) + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][7] + "')");
                }
        return true;
        }
        catch (Exception ex)
        {

            return false;
        }
        
            
        
    }

    public int Posting(string newInVtChNo)
    {
        return dm.ExecuteNonQuery(@"UPDATE NBT_CHLM SET CHLM_POST='Y' WHERE CHLM_IVCN='" + newInVtChNo + "'");
        
    }
}