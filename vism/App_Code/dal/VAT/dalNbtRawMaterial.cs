using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtRawMaterial
/// </summary>
public class dalNbtRawMaterial:dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public DataTable GetRptData()
    {
        return dm.ExecuteQuery(@"SELECT RM.RMMR_ID, RM.RMMR_CODE,RM.RMMR_NAME,UN.UNIT_NAME,RM.RMMR_DESC,RM.RMMR_STAT  FROM NBT_RMMR RM
INNER JOIN NBT_UNIT UN ON RM.RMMR_UNIT=UN.UNIT_ID");
    }



    public void Insert(string code, string name, int unit, string desc, int stat)
    {
        dm.ExecuteNonQuery(@"INSERT INTO NBT_RMMR(RMMR_ID,RMMR_CODE,RMMR_NAME,RMMR_UNIT,RMMR_DESC,RMMR_STAT) VALUES
                        (SQ_NBT_RMMR.NEXTVAL,'" + code + "','" + name + "','" + unit + "','" + desc + "','" +
                           stat + "')");
    }

    public void Update(int id, string code, string name, int unit, string desc, int stat)
    {
        dm.ExecuteNonQuery(@"UPDATE NBT_RMMR SET RMMR_CODE='" + code + "',RMMR_NAME='" + name + "',RMMR_UNIT='" + unit +
                           "',RMMR_DESC='" + desc + "',RMMR_STAT='" +
                           stat + "' WHERE RMMR_ID='" + id + "'");
    }
}