using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtSubContr
/// </summary>
public class dalNbtSubContr
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name, string vat, string addr, string cont, int stat)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_SUBM(SUBM_ID, SUBM_NAME, SUBM_VATR, SUBM_ADDR, SUBM_CONT, SUBM_STAT)
        VALUES(SQ_NBT_SUBM.NEXTVAL,'" + name + "','" + vat + "','" + addr + "','" + cont + "','" + stat + "')");
    }
    public int Update(int id, string name, string vat, string addr, string cont, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_SUBM set SUBM_NAME='" + name + "',SUBM_VATR='" + vat + "',SUBM_ADDR='" + addr + "',SUBM_CONT='" + cont + "',SUBM_STAT='" + stat + "' WHERE SUBM_ID=" + id);
    }

    public DataTable GetData()
    {
        return dm.ExecuteQuery(@"SELECT * FROM NBT_SUBM");
    }
}