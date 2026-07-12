using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtSupm
/// </summary>
public class dalNbtSupm
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name, string addr, int type, string cont, int stat, string vatr)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_SUPM(SUPM_ID, SUPM_NAME, SUPM_ADDR, SUPM_TYPE, SUPM_CONT,SUPM_STAT,SUPM_VATR)
        VALUES(SQ_NBT_SUPM.NEXTVAL,'" + name + "','" + addr + "','" + type + "','" + cont + "','" + stat + "','" + vatr + "')");
    }
    public int Update(int id, string name, string addr, int type, string cont, int stat, string vatr)
    {
        return dm.ExecuteNonQuery("update NBT_SUPM set SUPM_NAME='" + name + "',SUPM_ADDR='" + addr + "',SUPM_TYPE='" + type + "',SUPM_CONT='" + cont + "',SUPM_STAT='" + stat + "',SUPM_VATR='" + vatr + "' WHERE SUPM_ID=" + id);
    }
}