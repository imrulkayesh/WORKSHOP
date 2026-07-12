using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtCustomer
/// </summary>
public class dalNbtCustomer:dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string code, string name, string vatr, string add1, string add2, string add3,int country, string cont,string email,string dest,string remark, int stat)
    {
        return dm.ExecuteNonQuery(@"INSERT INTO NBT_CUSM(CUSM_ID,CUSM_CODE, CUSM_NAME,CUSM_VATR,CUSM_ADD1,CUSM_ADD2,CUSM_ADD3,CUSM_CNTY,CUSM_CONT,CUSM_EMAL,CUSM_DESN,CUSM_REMK,CUSM_STAT)
        VALUES(SQ_NBT_CUSM.NEXTVAL,'" + code + "','" + name + "','" + vatr + "','" + add1 + "','" + add2 + "','" + add3 + "','" + country + "','" + cont + "','" + email + "','" + dest + "','" + remark + "','" + stat + "')");
    }
    public int Update(int id, string code, string name, string vatr, string add1, string add2, string add3, int country, string cont, string email, string dest, string remark, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_CUSM set CUSM_CODE='" + code + "',CUSM_NAME='" + name + "',CUSM_VATR='" + vatr + "',CUSM_ADD1='" + add1 + "',CUSM_ADD2='" + add2 + "',CUSM_ADD3='" + add3 + "',CUSM_CNTY='" + country + "',CUSM_CONT='" + cont + "',CUSM_EMAL='" + email + "',CUSM_DESN='" + dest + "',CUSM_ADD1='" + add1 + "',CUSM_REMK='" + remark + "' WHERE CUSM_ID=" + id);
    }

    public DataTable GetData()
    {
        return dm.ExecuteQuery(@"SELECT CU.*,CN.CNTM_NAME FROM NBT_CUSM CU
INNER JOIN NBT_CNTM CN ON CU.CUSM_CNTY=CN.CNTM_ID");
    }

}