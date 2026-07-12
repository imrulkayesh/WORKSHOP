using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DbManagerSms
/// </summary>
public class DbManagerSms:IDisposable
{

    private String conString;
    private OracleConnection _cn;
    private DataTable _dt;
    private OracleCommand _cmd;
    private OracleDataReader _reader;
    private Dictionary<string, object> ParamList;

    private static OracleConnection _con;
    private static string connectionString;
    public DbManagerSms()
    {
        conString =
            "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.9.154)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=PRAN)));USER ID=RFL;PASSWORD=9552312;Unicode=True;";
        connectionString = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.9.154)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=PRAN)));USER ID=RFL;PASSWORD=9552312;Unicode=True;";
        ParamList = new Dictionary<string, object>();
    }
    public static OracleConnection OpenCon
    {
        get
        {
            if (_con == null)
            {
                _con = new OracleConnection(connectionString);
                _con.Open();
                return _con;
            }
            else if (_con.State != ConnectionState.Open)
            {
                _con.Open();
                return _con;
            }
            else
            {
                return _con;
            }
        }

    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_cn != null)
            {
                _cn.Dispose();
                _cn = null;
            }
            if (_dt != null)
            {
                _dt.Dispose();
                _dt = null;
            }
            if (_cmd != null)
            {
                _cmd.Dispose();
                _cmd = null;
            }
            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }
        }
    }

    ~DbManagerSms()
    {
        Dispose(false);
    }

    private static readonly Lazy<DatabaseManagerORCL> VLazy = new Lazy<DatabaseManagerORCL>(() => new DatabaseManagerORCL());

    public static DatabaseManagerORCL Instance
    {
        get { return VLazy.Value; }
    }

    public int ClearParameters()
    {
        if (ParamList != null)
        {
            if (ParamList.Count > 0)
                ParamList.Clear();
        }
        if (_cmd != null)
        {
            if (_cmd.Parameters.Count > 0)
                _cmd.Parameters.Clear();
        }
        return 1;
    }

    public void AddParameteres(string param, object value)
    {
        ParamList.Add(param, value);
    }

    //public int ExecuteNonQuery(string strSql)
    //{
    //    try
    //    {
    //        int intResult;
    //        _cmd = new OracleCommand(strSql, OpenCon);

    //        _cmd.CommandType = CommandType.Text;

    //        intResult = _cmd.ExecuteNonQuery();

    //        return intResult;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (new Exception(ex.Message));

    //    }
    //    finally
    //    {

    //    }
    //}
    public int ExecuteNonQuery(string strSql)
    {
        try
        {
            int intResult;
            using (_cn = new OracleConnection(conString))
            {
                _cn.Open();
                using (_cmd = new OracleCommand(strSql, _cn))
                {
                    _cmd.CommandType = CommandType.Text;
                    //foreach (KeyValuePair<string, object> dc in ParamList)
                    //{
                    //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                    //}

                    intResult = _cmd.ExecuteNonQuery();
                }
            }
            return intResult;
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message));

        }
        finally
        {
            _cn.Dispose();
            _cn.Close();
            OracleConnection.ClearPool(_cn);
        }
    }
    public int ExecuteNonQuery(string strSql, bool isProcedure)
    {
        try
        {
            int intResult;
            using (_cn = new OracleConnection(conString))
            {
                _cn.Open();
                using (_cmd = new OracleCommand(strSql, _cn))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    //foreach (KeyValuePair<string, object> dc in ParamList)
                    //{
                    //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                    //}

                    intResult = _cmd.ExecuteNonQuery();
                }
            }
            return intResult;
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message));

        }
        finally
        {
            _cn.Dispose();
            _cn.Close();
            OracleConnection.ClearPool(_cn);
        }
    }
    //public DataTable ExecuteQuery(string strSql)
    //{
    //    try
    //    {

    //        using (_cmd = new OracleCommand(strSql, OpenCon))
    //        {
    //            _cmd.CommandType = CommandType.Text;

    //            using (_reader = _cmd.ExecuteReader())
    //            {
    //                _dt = new DataTable();
    //                try
    //                {
    //                    _dt.Load(_reader);
    //                }
    //                catch { }
    //            }
    //        }

    //        return _dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //    finally
    //    {

    //    }
    //}
    public DataTable ExecuteQuery(string strSql)
    {
        try
        {
            using (_cn = new OracleConnection(conString))
            {
                _cn.Open();


                using (_cmd = new OracleCommand(strSql, _cn))
                {
                    _cmd.CommandType = CommandType.Text;
                    //foreach (KeyValuePair<string, object> dc in ParamList)
                    //{
                    //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                    //}

                    using (_reader = _cmd.ExecuteReader())
                    {
                        _dt = new DataTable();
                        try
                        {
                            _dt.Load(_reader);
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                        }
                    }

                }
            }

            return _dt;
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message));
        }
        finally
        {
            _cn.Dispose();
            _cn.Close();
            OracleConnection.ClearPool(_cn);
        }
    }

    public string ExecuteScalar(string strSql)
    {
        string res = "";
        try
        {
            using (_cn = new OracleConnection(conString))
            {
                _cn.Open();


                using (_cmd = new OracleCommand(strSql, _cn))
                {
                    _cmd.CommandType = CommandType.Text;
                    var firstColumn = _cmd.ExecuteScalar();
                    if (firstColumn != null)
                    {
                        res = firstColumn.ToString();
                    }
                }
            }

            return res;
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message));
        }
        finally
        {
            _cn.Dispose();
            _cn.Close();
            OracleConnection.ClearPool(_cn);
        }
    }
    public string ExecuteScalar(string strSql, string outParameterName)
    {
        string res = "";
        try
        {
            using (_cn = new OracleConnection(conString))
            {
                _cn.Open();

                using (_cmd = new OracleCommand(strSql, _cn))
                {
                    _cmd.CommandType = CommandType.Text;
                    OracleParameter outPm = new OracleParameter(outParameterName, OracleType.VarChar, 50) { Direction = ParameterDirection.Output };
                    _cmd.Parameters.Add(outPm);
                    _cmd.ExecuteNonQuery();
                    res = outPm.Value.ToString();
                }
            }

            return res;
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message));
        }
        finally
        {
            _cn.Dispose();
            _cn.Close();
            OracleConnection.ClearPool(_cn);
        }

    }



    internal OracleConnection GetDbConnection()
    {
        OracleConnection myConnection = new OracleConnection(connectionString);
        return myConnection;
    }

    public static DataTable StaticDT(string strSql)
    {
        try
        {
            DataTable dt = new DataTable();
            using (_con = new OracleConnection(connectionString))
            {
                _con.Open();


                using (OracleCommand cmds = new OracleCommand(strSql, _con))
                {
                    cmds.CommandType = CommandType.Text;
                    //foreach (KeyValuePair<string, object> dc in ParamList)
                    //{
                    //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                    //}
                    using (OracleDataReader reader = cmds.ExecuteReader())
                    {
                        dt = new DataTable();
                        try
                        {
                            dt.Load(reader);
                        }
                        catch { }
                    }
                }
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message));
        }
        finally
        {
            _con.Dispose();
            _con.Close();
            OracleConnection.ClearPool(_con);
        }
    }
    //public static DataTable StaticDT(string Query)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        using (OracleCommand cmd = new OracleCommand(Query, OpenCon))
    //        {
    //            cmd.CommandType = CommandType.Text;

    //            using (OracleDataReader _reader = cmd.ExecuteReader())
    //            {

    //                try
    //                {
    //                    dt.Load(_reader);
    //                }
    //                catch { }
    //            }
    //        }

    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //    finally
    //    {
    //        //_con.Dispose();
    //        //_con.Close();
    //        //OracleConnection.ClearPool(_con);
    //    }

    //}
}