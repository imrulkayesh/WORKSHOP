using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNbtFormulaReport
/// </summary>
public class dalNbtFormulaReport
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

   // public DsFormula LoadCrytalReport()
   // {


        //string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        //OdbcConnection conn = new OdbcConnection(con);
        //string query = @"select * from VW_NBT_FORMULA";
        //conn.Open();
        //OdbcDataAdapter da = new OdbcDataAdapter(query, conn);
        //DsFormula ds = new DsFormula();
        //da.Fill(ds, "DsFormula");
        //conn.Close();
       // return null;

        // Close connection
        //if (conn.State == ConnectionState.Open)
        //    conn.Close();


        //static OracleConnection _con = null;
        //string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        //OracleConnection connection = new OracleConnection(con);
        //string query = @"select * from VW_NBT_FORMULA";
        //connection.Open();
        //OracleDataReader adapter = new OracleDataReader(query);
        //DsFormula product = new DsFormula();
        //adapter.Fill(product, "DsFormula");
        //connection.Close();
        //return product;




   // }

    //public DataTable GetData()
    //{
    //    return dm.ExecuteQuery(@"select * from VW_NBT_FORMULA");

    //}
}