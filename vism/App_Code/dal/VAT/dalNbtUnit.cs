using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtUnit
/// </summary>
public class dalNbtUnit
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name, int stat)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_UNIT(UNIT_ID, UNIT_NAME, UNIT_STAT)
        VALUES(SQ_NBT_UNIT.NEXTVAL,'" + name + "','" + stat + "')");
    }
    public int Update(int id, string name,int stat)
    {
        return dm.ExecuteNonQuery("update NBT_UNIT set UNIT_NAME='" + name + "',UNIT_STAT='" + stat + "' WHERE UNIT_ID=" + id);
    }
}