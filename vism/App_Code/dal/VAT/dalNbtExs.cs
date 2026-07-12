using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtExs
/// </summary>
public class dalNbtExs:dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public DataTable GetRptData()
    {
        return dm.ExecuteQuery(@"SELECT EX.EXSM_ID, EX.EXSM_CODE,EX.EXSM_NAME,EX.EXSM_DESC,EX.EXSM_STAT  FROM NBT_EXSM EX");
    }



    public void Insert(string code, string name, string desc, int stat)
    {
        dm.ExecuteNonQuery(@"INSERT INTO NBT_EXSM(EXSM_ID,EXSM_CODE,EXSM_NAME,EXSM_DESC,EXSM_STAT) VALUES
                        (SQ_NBT_EXSM.NEXTVAL,'" + code + "','" + name + "','" + desc + "','" +
                           stat + "')");
    }

    public void Update(int id, string code, string name, string desc, int stat)
    {
        dm.ExecuteNonQuery(@"UPDATE NBT_EXSM SET EXSM_CODE='" + code + "',EXSM_NAME='" + name + "',EXSM_DESC='" + desc + "',EXSM_STAT='" +
                           stat + "' WHERE EXSM_ID='" + id + "'");
    }
}