using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtMwhm
/// </summary>
public class dalNbtMwhm
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name, int loc,int type, int stat)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_MWHM(MWHM_ID, MWHM_NAME, MWHM_LOCM, MWHM_TYPE, MWHM_STAT)
        VALUES(SQ_NBT_MWHM.NEXTVAL,'" + name + "','" + loc + "','" + type + "','" + stat + "')");
    }
    public int Update(int id, string name, int loc, int type, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_MWHM set MWHM_NAME='" + name + "',MWHM_LOCM='" + loc + "',MWHM_TYPE='" + type + "',MWHM_STAT='" + stat + "' WHERE MWHM_ID=" + id);
    }

    public DataTable GetWh()
    {
        return dm.ExecuteQuery(@"SELECT WH.MWHM_ID, WH.MWHM_NAME, LO.LOCM_NAME, TY.WHTY_NAME, WH.MWHM_STAT FROM NBT_MWHM WH 
INNER JOIN NBT_LOCM LO ON WH.MWHM_LOCM=LO.LOCM_ID
INNER JOIN NBT_WHTY TY ON WH.MWHM_TYPE=TY.WHTY_ID");
    }
}