using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtComp
/// </summary>
public class dalNbtComp
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalNbtComp()
	{
	}
    public int Insert(string name, int stat, string desc, string vrnb)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_COMP(COMP_ID,COMP_NAME,COMP_STAT,COMP_DESC,COMP_VRNB)
        VALUES(SQ_NBT_COMP.NEXTVAL,'" + name + "','" + stat + "','" + desc + "','" + vrnb + "')");
    }
    public int Update(int id, string name, int stat, string desc, string vrnb)
    {
        return dm.ExecuteNonQuery("update NBT_COMP set COMP_NAME='" + name + "',COMP_STAT='" + stat + "',COMP_DESC='" + desc + "',COMP_VRNB='" + vrnb + "' WHERE COMP_ID=" + id);
    }
    public int Delete(string table, int id)
    {
        return dm.ExecuteNonQuery("DELETE FROM " + table + " WHERE COMP_ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery(@"Select * from NBT_COMP WHERE COMP_ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from NBT_COMP WHERE COMP_NAME='" + name + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    
}