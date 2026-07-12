using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtWhdm
/// </summary>
public class dalNbtWhdm
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string name, int loc, int comp, int wh,int stat)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_WHDM(WHDM_ID, WHDM_NAME, WHDM_LOCM, WHDM_COMP, WHDM_MWHM, WHDM_STAT)
        VALUES(SQ_NBT_ITEM.NEXTVAL,'" + name + "','" + loc + "','" + comp + "','" + wh + "','"+stat+"')");
    }
    public int Update(int id, string name, int loc, int comp, int wh, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_WHDM set WHDM_NAME='" + name + "',WHDM_LOCM='" + loc + "',WHDM_COMP='" + comp + "',WHDM_MWHM='" + wh + "',WHDM_STAT='"+stat+"' WHERE WHDM_ID=" + id);
    }

    public DataTable GetData()
    {
        return dm.ExecuteQuery(@"SELECT DM.WHDM_STAT, LM.LOCM_NAME,WM.MWHM_NAME,CP.COMP_NAME,DM.WHDM_ID,DM.WHDM_NAME FROM NBT_WHDM DM
INNER JOIN NBT_LOCM LM ON DM.WHDM_LOCM=LM.LOCM_ID
INNER JOIN NBT_MWHM WM ON DM.WHDM_MWHM=WM.MWHM_ID
INNER JOIN NBT_COMP CP ON DM.WHDM_COMP=CP.COMP_ID");
    }
}