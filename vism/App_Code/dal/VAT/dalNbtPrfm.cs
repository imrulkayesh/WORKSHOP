using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtPrfm
/// </summary>
public class dalNbtPrfm
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name, int loc, int comp, int stat)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_PRFM(PRFM_ID, PRFM_NAME, PRFM_LOCM, PRFM_COMP, PRFM_STAT)
        VALUES(SQ_NBT_PRFM.NEXTVAL,'" + name + "','" + loc + "','" + comp + "','" + stat + "')");
    }
    public int Update(int id, string name, int loc, int comp, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_PRFM set PRFM_NAME='" + name + "',PRFM_LOCM='" + loc + "',PRFM_COMP='" + comp + "',PRFM_STAT='" + stat + "' WHERE PRFM_ID=" + id);
    }

    public DataTable GetData()
    {
        return dm.ExecuteQuery(@"SELECT PR.PRFM_ID,PR.PRFM_NAME,LO.LOCM_NAME,CO.COMP_NAME,PR.PRFM_STAT  FROM NBT_PRFM PR 
INNER JOIN NBT_LOCM LO ON PR.PRFM_LOCM=LO.LOCM_ID
INNER JOIN NBT_COMP CO ON PR.PRFM_COMP=CO.COMP_ID");
    }
}