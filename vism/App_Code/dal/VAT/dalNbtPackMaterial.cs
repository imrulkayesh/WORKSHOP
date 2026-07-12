using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtPackMaterial
/// </summary>
public class dalNbtPackMaterial:dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public DataTable GetRptData()
    {
        return dm.ExecuteQuery(@"SELECT PM.PMMR_ID, PM.PMMR_CODE,PM.PMMR_NAME,UN.UNIT_NAME,PM.PMMR_DESC,PM.PMMR_STAT  FROM NBT_PMMR PM
INNER JOIN NBT_UNIT UN ON PM.PMMR_UNIT=UN.UNIT_ID");
    }



    public void Insert(string code, string name, int unit, string desc, int stat)
    {
        dm.ExecuteNonQuery(@"INSERT INTO NBT_PMMR(PMMR_ID,PMMR_CODE,PMMR_NAME,PMMR_UNIT,PMMR_DESC,PMMR_STAT) VALUES
                        (SQ_NBT_PMMR.NEXTVAL,'" + code + "','" + name + "','" + unit + "','" + desc + "','" +
                           stat + "')");
    }

    public void Update(int id, string code, string name, int unit, string desc, int stat)
    {
        dm.ExecuteNonQuery(@"UPDATE NBT_PMMR SET PMMR_CODE='" + code + "',PMMR_NAME='" + name + "',PMMR_UNIT='" + unit +
                           "',PMMR_DESC='" + desc + "',PMMR_STAT='" +
                           stat + "' WHERE PMMR_ID='" + id + "'");
    }
}