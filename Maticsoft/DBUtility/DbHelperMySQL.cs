// Decompiled with JetBrains decompiler
// Type: Maticsoft.DBUtility.DbHelperMySQL
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Maticsoft.DBUtility
{
  public abstract class DbHelperMySQL
  {
    public static string connectionString = string.Empty;

    public static int GetMaxID(string FieldName, string TableName)
    {
      object single = GetSingle("select max(" + FieldName + ")+1 from " + TableName);
      return single == null ? 1 : int.Parse(single.ToString());
    }

    public static bool Exists(string strSql)
    {
      object single = GetSingle(strSql);
      return (!Equals(single, null) && !Equals(single, DBNull.Value) ? int.Parse(single.ToString()) : 0) != 0;
    }

    public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
    {
      object single = GetSingle(strSql, cmdParms);
      return (!Equals(single, null) && !Equals(single, DBNull.Value) ? int.Parse(single.ToString()) : 0) != 0;
    }

    public static int ExecuteSql(string SQLString)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, connection))
        {
          try
          {
            connection.Open();
            return mySqlCommand.ExecuteNonQuery();
          }
          catch (MySqlException ex)
          {
            connection.Close();
            throw ex;
          }
        }
      }
    }

    public static int ExecuteSql1(string SQLString)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, connection))
        {
          try
          {
            connection.Open();
            return (int) mySqlCommand.Parameters["id"].Value;
          }
          catch (MySqlException ex)
          {
            connection.Close();
            throw ex;
          }
        }
      }
    }

    public static int InsertDataAndGetId(string query, params MySqlParameter[] parameters)
    {
      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();
          using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
          {
            mySqlCommand.Parameters.AddRange(parameters);
            MySqlParameterCollection parameters1 = mySqlCommand.Parameters;
            MySqlParameter mySqlParameter = new MySqlParameter("?id", MySqlDbType.Int32);
            mySqlParameter.Direction = ParameterDirection.Output;
            parameters1.Add(mySqlParameter);
            mySqlCommand.ExecuteNonQuery();
            return (int) mySqlCommand.Parameters["?id"].Value;
          }
        }
      }
      catch (Exception ex)
      {
        return 0;
      }
    }

    public static int ExecuteSqlByTime(string SQLString, int Times)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, connection))
        {
          try
          {
            connection.Open();
            mySqlCommand.CommandTimeout = Times;
            return mySqlCommand.ExecuteNonQuery();
          }
          catch (MySqlException ex)
          {
            connection.Close();
            throw ex;
          }
        }
      }
    }

    public static int ExecuteSqlTran(List<string> SQLStringList)
    {
      using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
      {
        mySqlConnection.Open();
        MySqlCommand mySqlCommand = new MySqlCommand();
        mySqlCommand.Connection = mySqlConnection;
        MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
        mySqlCommand.Transaction = mySqlTransaction;
        try
        {
          int num = 0;
          for (int index = 0; index < SQLStringList.Count; ++index)
          {
            string sqlString = SQLStringList[index];
            if (sqlString.Trim().Length > 1)
            {
              mySqlCommand.CommandText = sqlString;
              num += mySqlCommand.ExecuteNonQuery();
            }
          }
          mySqlTransaction.Commit();
          return num;
        }
        catch
        {
          mySqlTransaction.Rollback();
          return 0;
        }
      }
    }

    public static int ExecuteSql(string SQLString, string content)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        MySqlCommand mySqlCommand = new MySqlCommand(SQLString, connection);
        MySqlParameter mySqlParameter = new MySqlParameter("@content", SqlDbType.NText);
        mySqlParameter.Value = content;
        mySqlCommand.Parameters.Add(mySqlParameter);
        try
        {
          connection.Open();
          return mySqlCommand.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
          throw ex;
        }
        finally
        {
          mySqlCommand.Dispose();
          connection.Close();
        }
      }
    }

    public static object ExecuteSqlGet(string SQLString, string content)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        MySqlCommand mySqlCommand = new MySqlCommand(SQLString, connection);
        MySqlParameter mySqlParameter = new MySqlParameter("@content", SqlDbType.NText);
        mySqlParameter.Value = content;
        mySqlCommand.Parameters.Add(mySqlParameter);
        try
        {
          connection.Open();
          object objA = mySqlCommand.ExecuteScalar();
          return Equals(objA, null) || Equals(objA, DBNull.Value) ? null : objA;
        }
        catch (MySqlException ex)
        {
          throw ex;
        }
        finally
        {
          mySqlCommand.Dispose();
          connection.Close();
        }
      }
    }

    public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        MySqlCommand mySqlCommand = new MySqlCommand(strSQL, connection);
        MySqlParameter mySqlParameter = new MySqlParameter("@fs", SqlDbType.Image);
        mySqlParameter.Value = fs;
        mySqlCommand.Parameters.Add(mySqlParameter);
        try
        {
          connection.Open();
          return mySqlCommand.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
          throw ex;
        }
        finally
        {
          mySqlCommand.Dispose();
          connection.Close();
        }
      }
    }

    public static object GetSingle(string SQLString)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, connection))
        {
          try
          {
            connection.Open();
            object objA = mySqlCommand.ExecuteScalar();
            return Equals(objA, null) || Equals(objA, DBNull.Value) ? null : objA;
          }
          catch (MySqlException ex)
          {
            connection.Close();
            throw ex;
          }
        }
      }
    }

    public static object GetSingle(string SQLString, int Times)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        using (MySqlCommand mySqlCommand = new MySqlCommand(SQLString, connection))
        {
          try
          {
            connection.Open();
            mySqlCommand.CommandTimeout = Times;
            object objA = mySqlCommand.ExecuteScalar();
            return Equals(objA, null) || Equals(objA, DBNull.Value) ? null : objA;
          }
          catch (MySqlException ex)
          {
            connection.Close();
            throw ex;
          }
        }
      }
    }

    public static MySqlDataReader ExecuteReader(string strSQL)
    {
      MySqlConnection connection = new MySqlConnection(connectionString);
      MySqlCommand mySqlCommand = new MySqlCommand(strSQL, connection);
      try
      {
        connection.Open();
        return mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
      }
      catch (MySqlException ex)
      {
        throw ex;
      }
    }

    public static DataSet Query(string SQLString)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        DataSet dataSet = new DataSet();
        try
        {
          connection.Open();

          new MySqlDataAdapter(SQLString, connection).Fill(dataSet, "ds");
        }
        catch (MySqlException ex)
        {
          throw new Exception(ex.Message);
        }
        return dataSet;
      }
    }

    public static DataSet Query(string SQLString, int Times)
    {
      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        DataSet dataSet = new DataSet();
        try
        {
          connection.Open();
          MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(SQLString, connection);
          mySqlDataAdapter.SelectCommand.CommandTimeout = Times;
          mySqlDataAdapter.Fill(dataSet, "ds");
        }
        catch (MySqlException ex)
        {
          throw new Exception(ex.Message);
        }
        return dataSet;
      }
    }

    public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
    {
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        using (MySqlCommand cmd = new MySqlCommand())
        {
          try
          {
                        PrepareCommand(cmd, conn, null, SQLString, cmdParms);
            int num = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return num;
          }
          catch (MySqlException ex)
          {
            throw ex;
          }
        }
      }
    }

    public static void ExecuteSqlTran(Hashtable SQLStringList)
    {
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        using (MySqlTransaction trans = conn.BeginTransaction())
        {
          MySqlCommand cmd = new MySqlCommand();
          try
          {
            foreach (DictionaryEntry sqlString in SQLStringList)
            {
              string cmdText = sqlString.Key.ToString();
              MySqlParameter[] cmdParms = (MySqlParameter[]) sqlString.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
              cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();
            }
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            throw;
          }
        }
      }
    }

    public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
    {
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        using (MySqlTransaction trans = conn.BeginTransaction())
        {
          MySqlCommand cmd = new MySqlCommand();
          try
          {
            int num = 0;
            foreach (DictionaryEntry sqlString in SQLStringList)
            {
              string cmdText = sqlString.Key.ToString();
              MySqlParameter[] cmdParms = (MySqlParameter[]) sqlString.Value;
              foreach (MySqlParameter mySqlParameter in cmdParms)
              {
                if (mySqlParameter.Direction == ParameterDirection.InputOutput)
                  mySqlParameter.Value = num;
              }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
              cmd.ExecuteNonQuery();
              foreach (MySqlParameter mySqlParameter in cmdParms)
              {
                if (mySqlParameter.Direction == ParameterDirection.Output)
                  num = Convert.ToInt32(mySqlParameter.Value);
              }
              cmd.Parameters.Clear();
            }
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            throw;
          }
        }
      }
    }

    public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
    {
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        using (MySqlCommand cmd = new MySqlCommand())
        {
          try
          {
                        PrepareCommand(cmd, conn, null, SQLString, cmdParms);
            object objA = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return Equals(objA, null) || Equals(objA, DBNull.Value) ? null : objA;
          }
          catch (MySqlException ex)
          {
            throw ex;
          }
        }
      }
    }

    public static MySqlDataReader ExecuteReader(string SQLString, params MySqlParameter[] cmdParms)
    {
      MySqlConnection conn = new MySqlConnection(connectionString);
      MySqlCommand cmd = new MySqlCommand();
      try
      {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
        MySqlDataReader mySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        cmd.Parameters.Clear();
        return mySqlDataReader;
      }
      catch (MySqlException ex)
      {
        throw ex;
      }
    }

    public static DataSet Query(string SQLString, params MySqlParameter[] cmdParms)
    {
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        MySqlCommand mySqlCommand = new MySqlCommand();
                PrepareCommand(mySqlCommand, conn, null, SQLString, cmdParms);
        using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand))
        {
          DataSet dataSet = new DataSet();
          try
          {
            mySqlDataAdapter.Fill(dataSet, "ds");
            mySqlCommand.Parameters.Clear();
          }
          catch (MySqlException ex)
          {
            throw new Exception(ex.Message);
          }
          return dataSet;
        }
      }
    }

    private static void PrepareCommand(
      MySqlCommand cmd,
      MySqlConnection conn,
      MySqlTransaction trans,
      string cmdText,
      MySqlParameter[] cmdParms)
    {
      if (conn.State != ConnectionState.Open)
        conn.Open();
      cmd.Connection = conn;
      cmd.CommandText = cmdText;
      if (trans != null)
        cmd.Transaction = trans;
      cmd.CommandType = CommandType.Text;
      if (cmdParms == null)
        return;
      foreach (MySqlParameter cmdParm in cmdParms)
      {
        if ((cmdParm.Direction == ParameterDirection.InputOutput || cmdParm.Direction == ParameterDirection.Input) && cmdParm.Value == null)
          cmdParm.Value = DBNull.Value;
        cmd.Parameters.Add(cmdParm);
      }
    }
  }
}
