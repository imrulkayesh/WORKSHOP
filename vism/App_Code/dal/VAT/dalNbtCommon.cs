using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtCommon
/// </summary>
public class dalNbtCommon
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
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
    public DataTable GetDdlData(string table, string text, string value)
    {
        return dm.ExecuteQuery(@"SELECT " + text + "," + value + " FROM " + table + "");
    }

    // no change end


}