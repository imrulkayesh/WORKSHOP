using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtLocation
/// </summary>
public class dalNbtLocation
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name, int stat, string addr, string cont)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_LOCM(LOCM_ID, LOCM_NAME, LOCM_STAT, LOCM_ADDR,LOCM_CONT)
        VALUES(SQ_NBT_LOCM.NEXTVAL,'" + name + "','" + stat + "','" + addr + "','" + cont + "')");
    }
    public int Update(int id, string name, int stat, string addr, string cont)
    {
        return dm.ExecuteNonQuery("update NBT_LOCM set LOCM_NAME='" + name + "',LOCM_STAT='" + stat + "',LOCM_ADDR='" + addr + "',LOCM_CONT='" + cont + "' WHERE LOCM_ID=" + id);
    }
}