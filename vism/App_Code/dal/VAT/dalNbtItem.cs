using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtItem
/// </summary>
public class dalNbtItem:dalNbtCommon
{
	public dalNbtItem()
	{
	}
    //no change start
    public void Delete(string table, string column, int id)
    {
        dm.ExecuteNonQuery("DELETE FROM " + table + " WHERE " + column + "='" + id + "'");
    }
    public bool CheckDuplicate(string table, string column, string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from " + table + " WHERE " + column + "='" + name + "'");
        return dt.Rows.Count == 0;
    }
    public DataTable GetById(string table, string column, int id)
    {
        return dm.ExecuteQuery("SELECT * FROM " + table + " WHERE " + column + "='" + id + "'");
    }
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    /// no change end
    public int Insert(string code,string name, int categ, int unit, int comp, string remark, int stat )
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_ITEM(ITEM_ID,ITEM_CODE, ITEM_NAME,CATM_ID,UNIT_ID,COMP_ID,ITEM_REMK,ITEM_STAT)
        VALUES(SQ_NBT_ITEM.NEXTVAL,'" + code + "','" + name + "','" + categ + "','" + unit + "','" + comp + "','" + remark + "','" + stat + "')");
    }
    public int Update(int id, string code, string name, int categ, int unit, int comp, string remark, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_ITEM set ITEM_CODE='" + code + "', ITEM_NAME='" + name + "',CATM_ID='" + categ + "',UNIT_ID='" + unit + "',COMP_ID='" + comp + "',ITEM_REMK='" + remark + "',ITEM_STAT='" + stat + "' WHERE ITEM_ID=" + id);
    }

    public DataTable GetItem()
    {
        return dm.ExecuteQuery(@"SELECT IT.ITEM_ID, IT.ITEM_CODE,IT.ITEM_NAME,CA.CATM_NAME,UN.UNIT_NAME,CO.COMP_NAME,IT.ITEM_REMK,IT.ITEM_STAT FROM NBT_ITEM IT
INNER JOIN NBT_COMP CO ON IT.COMP_ID=CO.COMP_ID
INNER JOIN NBT_CATM CA ON IT.CATM_ID=CA.CATM_ID
INNER JOIN NBT_UNIT UN ON IT.UNIT_ID=UN.UNIT_ID");
    }

}