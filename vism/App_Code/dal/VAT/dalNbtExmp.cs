using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtExmp
/// </summary>
public class dalNbtExmp
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string code, string name, decimal price, int stat)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_EXPM(EXPM_ID, EXPM_CODE,EXPM_NAME,EXPM_PRCE,EXPM_STAT)
        VALUES(SQ_NBT_EXPM.NEXTVAL,'" + code + "','" + name + "','" + price + "','" + stat + "')");
    }
    public int Update(int id, string code, string name, decimal price, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_EXPM set EXPM_CODE='" + code + "',EXPM_NAME='" + name + "',EXPM_PRCE='" + price + "',EXPM_STAT='" + stat + "' WHERE EXPM_ID=" + id);
    }

}