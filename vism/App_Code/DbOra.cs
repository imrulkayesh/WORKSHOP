using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Transactions;
using System.Web;
using IsolationLevel = System.Data.IsolationLevel;

/// <summary>
/// Summary description for DbOra
/// </summary>
public class DbOra
{
    private static string conStr = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
    private static OracleConnection con = new OracleConnection(conStr);
    private static OracleCommand cmd;
    private static OracleTransaction transaction;

    public DbOra()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void BeginTransaction()
    {
        try
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
            con.Open();
            cmd = con.CreateCommand();
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            cmd.Connection = con;
            cmd.Transaction = transaction;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int ExecuteNonQuery(string qry)
    {
        try
        {
            cmd.CommandText = qry;
            cmd.CommandType = CommandType.Text;
            int res = cmd.ExecuteNonQuery();
            return res;
        }
        catch (Exception ex)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
            finally
            {
                con.Close();
            }
            throw ex;
        }
    }

    public void Commit()
    {
        try
        {
            transaction.Commit();
        }
        catch (Exception ex)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
            finally
            {
                con.Close();
            }
            throw ex;
        }
        finally
        {
           con.Close(); 
        }
    }

    public void Rollback()
    {
       
            try
            {
                if (con.State != ConnectionState.Closed)
                {
                    transaction.Rollback();
                }
            }
            catch
            {
                //
            }
            finally
            {
                if (con.State!=ConnectionState.Closed)
                {
                con.Close();
                }
                
            }
   }
    
}