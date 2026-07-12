using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalCurrency
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalCurrency()
	{
    }
    public int Insert(string name,string code)
    {
        return dm.ExecuteNonQuery(@"Insert into ASSET_CURRENCY(ID,CURRENCYNAME,CURRENCYCODE)
        VALUES(SQ_ASSET_CURRENCY.NEXTVAL,'" + name + "','" + code + "')");
    }
    public int Update(int id, string name, string code)
    {
        return dm.ExecuteNonQuery("update ASSET_CURRENCY set CURRENCYNAME='" + name + "',CURRENCYCODE='" + code + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from ASSET_CURRENCY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from ASSET_CURRENCY WHERE CURRENCYNAME='" + name + "'");
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