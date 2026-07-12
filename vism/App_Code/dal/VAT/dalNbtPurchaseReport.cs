using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtPurchaseReport
/// </summary>
public class dalNbtPurchaseReport:dalNbtCommon
{
    DatabaseManagerORCL dm=new DatabaseManagerORCL();
    public DataTable GetDdlComp()
    {
        return dm.ExecuteQuery(@"SELECT COMP_ID,COMP_NAME FROM NBT_COMP WHERE COMP_STAT=1");
    }

    public DataTable GetDdlCate()
    {
        return dm.ExecuteQuery(@"SELECT CATM_ID,CATM_NAME FROM NBT_CATM WHERE CATM_STAT=1");
    }

    public DataTable GetDdlMatCode(string cate)
    {
        return dm.ExecuteQuery(@"SELECT ITEM_NAME,ITEM_CODE FROM NBT_ITEM WHERE CATM_ID='" + cate + "' AND ITEM_STAT='" + 1 + "'");

    }

    public DataTable GetItemInfo(string code)
    {
        return dm.ExecuteQuery(@"SELECT ITEM_NAME FROM NBT_ITEM WHERE ITEM_CODE='" + code + "'");
    }
}