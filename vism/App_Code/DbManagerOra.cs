using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

/// <summary>
/// Summary description for DbManagerOra
/// </summary>
public class DbManagerOra
{
    private static string conStr = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
    private static OracleConnection con = new OracleConnection(conStr);
    private static OracleCommand cmd;
    private static TransactionScope scp;
    

    public int ExecuteNonQuery(string qry)
    {
        try
        {
            int res = 0;
            cmd=new OracleCommand(qry,con);
            cmd.CommandType=CommandType.Text;
            res = cmd.ExecuteNonQuery();
            return res;
        }
        catch (Exception ex)
        {
            scp.Dispose();
            if (con.State != ConnectionState.Closed)
            {
                con.Dispose();
                con.Close();
                OracleConnection.ClearPool(con);
            }
            throw ex;
        }
    }
    public string ExecuteScalar(string qry)
    {
        try
        {
            string res = "";
            cmd.CommandText = qry;
            cmd.CommandType = CommandType.Text;
            object firstColumn = cmd.ExecuteScalar();
            if (firstColumn != null)
            {
                res= firstColumn.ToString();
            }
            return res;
        }
        catch (Exception ex)
        {
            scp.Dispose();
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
                //con.Dispose();
            }
            throw ex;
        }
    }
    public string ExecuteScalar(string qry,string outParameterName)
    {
        try
        {
            string res = "";
            cmd = new OracleCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            OracleParameter outPm = new OracleParameter(outParameterName, OracleType.VarChar, 50) {Direction = ParameterDirection.Output};
            cmd.Parameters.Add(outPm);
            cmd.ExecuteNonQuery();
            res = outPm.Value.ToString();
            return res;
        }
        catch (Exception ex)
        {
            scp.Dispose();
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
            throw ex;
        }
    }
    public string ExecuteScalar(string qry, string outParameterName,bool outOfTransaction)
    {
        try
        {
            string res = "";
            if (outOfTransaction)
            {
                TransactionScope scp2=new TransactionScope(TransactionScopeOption.RequiresNew);
                cmd = new OracleCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                OracleParameter outPm = new OracleParameter(outParameterName, OracleType.VarChar, 50) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outPm);
                cmd.ExecuteNonQuery();
                res = outPm.Value.ToString();
                scp2.Complete();
                scp2.Dispose();
            }
            else
            {
                cmd = new OracleCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                OracleParameter outPm = new OracleParameter(outParameterName, OracleType.VarChar, 50) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outPm);
                cmd.ExecuteNonQuery();
                res = outPm.Value.ToString();
            }
            
            
            return res;
        }
        catch (Exception ex)
        {
            scp.Dispose();
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
            throw ex;
        }
    }
    public DataTable ExecuteQuery(string qry)
    {
        try
        {
            DataTable res = new DataTable();
            cmd.CommandText = qry;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dataReader = cmd.ExecuteReader();
            res.Load(dataReader);
            dataReader.Close();
            dataReader.Dispose();
            return res;
        }
        catch (Exception ex)
        {
            scp.Dispose();
            
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
                //con.Dispose();
            }
            throw ex;
        }
    }
    public void BeginTransaction()
    {
        try
        {
            TransactionOptions top = new TransactionOptions();
            top.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            top.Timeout = TransactionManager.MaximumTimeout;
            scp = new TransactionScope(TransactionScopeOption.Required, top);
            //scp = new TransactionScope();
            con = new OracleConnection(conStr);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
         
    }

    public void Commit()
    {
        try
        {
            scp.Complete();
            scp.Dispose();
            //TransactionScope sc = new TransactionScope(TransactionScopeOption.Suppress);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Dispose();
                con.Close();
                OracleConnection.ClearPool(con);
            }
        }
        
    }
}