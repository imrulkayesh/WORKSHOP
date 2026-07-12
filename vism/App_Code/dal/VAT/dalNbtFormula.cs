using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for dalNbtFormula
/// </summary>
public class dalNbtFormula:dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public int Insert(decimal slno, string item, string comp, int country, int type, string unit, string hscd, decimal vacp, DateTime entryDate, decimal vapp, decimal vtsd, decimal vwsp, decimal sacp,
        decimal vtrp, decimal sapp, decimal vtbp, decimal sdpp, decimal sdtp, int stat)
    {
        try
        {
            return
            dm.ExecuteNonQuery(@"INSERT INTO NBT_MSFM(OID,MSFM_SLNO,MSFM_ITEM,MSFM_COMP,MSFM_CNTR,MSFM_TYPE,MSFM_UNIT,MSFM_HSCD,MSFM_VACP,
                                                            MSFM_DATE,MSFM_VAPP,MSFM_VTSD,MSFM_VWSP,MSFM_SACP,MSFM_VTRP,MSFM_SAPP,MSFM_VTBP,MSFM_SDPP,
                                                            MSFM_SDTP,MSFM_STAT) VALUES (SQ_NBT_MSFM.NEXTVAL,'" + slno +
                               "','" + item + "','" + comp + "','" + country + "','" + type + "','" + unit + "','" +
                               hscd + "','" + vacp + "', TO_DATE('" + entryDate + "','MM/dd/yyyy HH:MI:SSAM'), '" + vapp + "','" + vtsd + "','" + vwsp + "','" + sacp + "','" +
                               vtrp + "','" + sapp + "','" + vtbp + "','" + sdpp + "','" + sdtp + "','" + stat + "')");
        }
        catch (Exception ex)
        {
            return -1;

        }
        
    }

    public void InsertDetail(int masterId, int code, string name, int type, decimal unitPrice, decimal totalQty, decimal wastQty, decimal actQty, decimal price, string expcode, decimal expPrice)
    {
        dm.ExecuteNonQuery(@"INSERT INTO NBT_MSFD(OID,MSFD_MSFM,MSFD_CODE,MSFD_ITEM,MSFD_TYPE,MSFD_UNPR,MSFD_TOTQ,MSFD_WSQT,MSFD_ACQT,MSFD_PRCE,MSFD_EXPC,MSFD_EXPP)
VALUES (SQ_NBT_MSFM.NEXTVAL,'"+masterId+"','"+code+"','"+name+"','"+type+"','"+unitPrice+"','"+totalQty+"','"+wastQty+"','"+actQty+"','"+price+"','"+expcode+"','"+expPrice+"')");
    }

    public DataTable GetMatInfo(string comp,string type, string code)
    {
        return dm.ExecuteQuery(@"SELECT IT.ITEM_NAME,CO.COMP_NAME,UN.UNIT_NAME FROM NBT_ITEM IT
INNER JOIN NBT_COMP CO ON IT.COMP_ID=CO.COMP_ID
INNER JOIN NBT_UNIT UN ON IT.UNIT_ID =UN.UNIT_ID WHERE IT.COMP_ID='" + comp + "' AND IT.CATM_ID='" + type +
                               "' AND IT.ITEM_CODE='" + code + "'");
    }

    public DataTable GetExpmInfo(string code)
    {
        return dm.ExecuteQuery(@"SELECT EXPM_NAME,EXPM_PRCE FROM NBT_EXPM WHERE EXPM_CODE ='" + code + "'");
    }

    public DataTable GetcurrNo()
    {
        return dm.ExecuteQuery(@"SELECT FORM_ID,FORM_SLNO FROM NBT_FORM ORDER BY FORM_ID DESC");
    }

    public bool SaveAll(string slNo, string hscode, string itemCode, string itemName, string itemUnit, string itemComp, string date, decimal vacp,
        decimal sd, decimal vapp, decimal sacp, decimal wholSalPr, decimal sapp, decimal retPr, decimal sosa, decimal totBuyPr, int stat, DataTable dt)
    {
        OracleConnection conn = dm.GetDbConnection(); 
        conn.Open();
        OracleCommand cmd = conn.CreateCommand();
        OracleTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        cmd.Transaction = trans;
        try
        {
            cmd.CommandText = @"INSERT INTO NBT_FORM (FORM_ID,FORM_SLNO,FORM_HSCD,ITEM_CODE,ITEM_NAME,ITEM_UNIT,ITEM_COMP,FORM_DATE,FORM_VACP,
                                 FORM_SD,FORM_VAPP,FORM_SACP,FORM_WHSP,FORM_SAPP,FORM_REPR,FORM_SOSA,FORM_TOBP,FORM_STAT) VALUES
                              (SQ_NBT_FORM.NEXTVAL,'" + slNo + "','" + hscode + "','" + itemCode + "','" + itemName +
                                    "','" + itemUnit + "','" + itemComp + "',TO_DATE('" + date + "', 'DD/MM/YYYY'),'" +
                                    vacp + "','" + sd + "','" + vapp + "','" + sacp + "','" + wholSalPr + "','" + sapp +
                                    "','" + retPr + "','" + sosa + "','" + totBuyPr + "','" + stat + "')";
            cmd.ExecuteNonQuery();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd.CommandText = @"INSERT INTO NBT_FORD(FORD_ID,FORM_SLNO,FORD_TYPE,FORD_CODE,FORD_NAME,FORD_UNPR,FORD_TOQT,FORD_WSQT,FORD_ACQT,FORD_PRCE,FORD_EXCD,FORD_EXNM,FORD_EXPR,FORD_LINE)
                   VALUES(SQ_NBT_FORD.NEXTVAL,'" + slNo + "','" + dt.Rows[i][0] + "','" + dt.Rows[i][1] + "','" +
                                        dt.Rows[i][2] + "','" + dt.Rows[i][3] + "','" + dt.Rows[i][4] + "','" +
                                        dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][7] + "','" +
                                        dt.Rows[i][8] + "','" + dt.Rows[i][9] + "','" + dt.Rows[i][10] + "','" + (i + 1) +
                                        "')";
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

    public DataTable GetItemInfo(string code)
    {
        return dm.ExecuteQuery(@"SELECT IT.ITEM_NAME,UN.UNIT_NAME FROM NBT_ITEM IT
INNER JOIN NBT_UNIT UN ON IT.UNIT_ID=UN.UNIT_ID
WHERE IT.ITEM_CODE='" + code + "'");
    }

    public DataTable GetComp()
    {
        return dm.ExecuteQuery(@"SELECT * FROM NBT_COMP WHERE COMP_STAT=1");
    }

    public DataTable GetFpInfo(string comp)
    {
        return dm.ExecuteQuery(@"select * from NBT_ITEM WHERE CATM_ID=6 AND ITEM_STAT=1 AND COMP_ID='" + comp + "'");
    }

    public DataTable GetMatType()
    {
        return dm.ExecuteQuery(@"select * from NBT_CATM WHERE CATM_STAT=1");
        
    }

    public DataTable GetMatCode(string type,string comp)
    {
        return dm.ExecuteQuery(@"select * from NBT_ITEM WHERE CATM_ID='"+type+"' AND ITEM_STAT=1 AND COMP_ID='" + comp + "'");
    }
}