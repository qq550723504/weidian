// Decompiled with JetBrains decompiler
// Type: 微店新版.SqLiteHelper
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;


namespace 微店新版
{
  public class SqLiteHelper
  {
    private SQLiteConnection _sqLiteConn = null;
    private SQLiteTransaction _sqLiteTrans = null;
    private bool _isRunTrans = false;
    private string _sqLiteConnString = null;
    private bool _autoCommit = false;

    public SqLiteHelper()
    {
      _sqLiteConnString = "Data Source=IG.db;Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
    }

    public void NewTable(string cmdText)
    {
      SQLiteConnection sqLiteConnection = new SQLiteConnection(_sqLiteConnString);
      if (sqLiteConnection.State != ConnectionState.Open)
      {
        sqLiteConnection.Open();
        SQLiteCommand sqLiteCommand = new SQLiteCommand();
        sqLiteCommand.Connection = sqLiteConnection;
        sqLiteCommand.CommandText = cmdText;
        sqLiteCommand.ExecuteNonQuery();
      }
      sqLiteConnection.Close();
    }

    public bool CheckTableExist(string tbName)
    {
      DataTable dataTable = ExecuteDataTable("select * from sqlite_master where type = 'table' and name = '" + tbName + "'", null);
      return dataTable != null && dataTable.Rows.Count > 0;
    }

    public DataSet ExecuteDataset(string cmdText, Dictionary<string, object> data)
    {
      DataSet dataSet = new DataSet();
      using (SQLiteConnection conn = new SQLiteConnection(_sqLiteConnString))
      {
        SQLiteCommand cmd = new SQLiteCommand();
        PrepareCommand(cmd, conn, cmdText, data);
        new SQLiteDataAdapter(cmd).Fill(dataSet);
      }
      return dataSet;
    }

    public DataTable ExecuteDataTable(string cmdText, Dictionary<string, object> data)
    {
      DataTable dataTable = new DataTable();
      using (SQLiteConnection conn = new SQLiteConnection(_sqLiteConnString))
      {
        SQLiteCommand cmd = new SQLiteCommand();
        PrepareCommand(cmd, conn, cmdText, data);
        SQLiteDataReader reader = cmd.ExecuteReader();
        dataTable.Load(reader);
      }
      return dataTable;
    }

    public static DataTable DeleteSameRow(DataTable dt, string Field)
    {
      ArrayList indexList = new ArrayList();
      for (int index1 = 0; index1 < dt.Rows.Count - 1; ++index1)
      {
        if (!IsContain(indexList, index1))
        {
          for (int index2 = index1 + 1; index2 < dt.Rows.Count; ++index2)
          {
            if (dt.Rows[index1][Field].ToString() == dt.Rows[index2][Field].ToString())
              indexList.Add(index2);
          }
        }
      }
      indexList.Sort();
      for (int index = indexList.Count - 1; index >= 0; --index)
      {
        int int32 = Convert.ToInt32(indexList[index]);
        dt.Rows.RemoveAt(int32);
      }
      return dt;
    }

    public static bool IsContain(ArrayList indexList, int index)
    {
      for (int index1 = 0; index1 < indexList.Count; ++index1)
      {
        if (Convert.ToInt32(indexList[index1]) == index)
          return true;
      }
      return false;
    }

    public DataRow ExecuteDataRow(string cmdText, Dictionary<string, object> data)
    {
      DataSet dataSet = ExecuteDataset(cmdText, data);
      return dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 ? dataSet.Tables[0].Rows[0] : null;
    }

    public int ExecuteNonQuery(string cmdText, Dictionary<string, object> data)
    {
      using (SQLiteConnection conn = new SQLiteConnection(_sqLiteConnString))
      {
        SQLiteCommand cmd = new SQLiteCommand();
        PrepareCommand(cmd, conn, cmdText, data);
        return cmd.ExecuteNonQuery();
      }
    }

    public void ExecuteSqlTran(ArrayList SQLStringList)
    {
      using (SQLiteConnection sqLiteConnection = new SQLiteConnection(_sqLiteConnString))
      {
        sqLiteConnection.Open();
        SQLiteCommand sqLiteCommand = new SQLiteCommand();
        sqLiteCommand.Connection = sqLiteConnection;
        SQLiteTransaction sqLiteTransaction = sqLiteConnection.BeginTransaction();
        sqLiteCommand.Transaction = sqLiteTransaction;
        try
        {
          for (int index = 0; index < SQLStringList.Count; ++index)
          {
            string str = SQLStringList[index].ToString();
            if (str.Trim().Length > 1)
            {
              sqLiteCommand.CommandText = str;
              sqLiteCommand.ExecuteNonQuery();
            }
          }
          sqLiteTransaction.Commit();
        }
        catch (SQLiteException ex)
        {
          sqLiteTransaction.Rollback();
          throw new Exception(ex.Message);
        }
      }
    }

    public SQLiteDataReader ExecuteReader(string cmdText, Dictionary<string, object> data)
    {
      SQLiteCommand cmd = new SQLiteCommand();
      SQLiteConnection conn = new SQLiteConnection(_sqLiteConnString);
      try
      {
        PrepareCommand(cmd, conn, cmdText, data);
        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
      }
      catch
      {
        conn.Close();
        cmd.Dispose();
        throw;
      }
    }

    public object ExecuteScalar(string cmdText, Dictionary<string, object> data)
    {
      using (SQLiteConnection conn = new SQLiteConnection(_sqLiteConnString))
      {
        SQLiteCommand cmd = new SQLiteCommand();
        PrepareCommand(cmd, conn, cmdText, data);
        return cmd.ExecuteScalar();
      }
    }

    public DataSet ExecutePager(
      ref int recordCount,
      int pageIndex,
      int pageSize,
      string cmdText,
      string countText,
      Dictionary<string, object> data)
    {
      if (recordCount < 0)
        recordCount = int.Parse(ExecuteScalar(countText, data).ToString());
      DataSet dataSet = new DataSet();
      using (SQLiteConnection conn = new SQLiteConnection(_sqLiteConnString))
      {
        SQLiteCommand cmd = new SQLiteCommand();
        PrepareCommand(cmd, conn, cmdText, data);
        new SQLiteDataAdapter(cmd).Fill(dataSet, (pageIndex - 1) * pageSize, pageSize, "result");
      }
      return dataSet;
    }

    private void PrepareCommand(
      SQLiteCommand cmd,
      SQLiteConnection conn,
      string cmdText,
      Dictionary<string, object> data)
    {
      if (conn.State != ConnectionState.Open)
        conn.Open();
      cmd.Parameters.Clear();
      cmd.Connection = conn;
      cmd.CommandText = cmdText;
      cmd.CommandType = CommandType.Text;
      cmd.CommandTimeout = 30;
      if (data == null || data.Count < 1)
        return;
      foreach (KeyValuePair<string, object> keyValuePair in data)
        cmd.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
    }

    public void ResetDataBass()
    {
      using (SQLiteConnection sqLiteConnection = new SQLiteConnection(_sqLiteConnString))
      {
        SQLiteCommand sqLiteCommand = new SQLiteCommand();
        if (sqLiteConnection.State != ConnectionState.Open)
          sqLiteConnection.Open();
        sqLiteCommand.Parameters.Clear();
        sqLiteCommand.Connection = sqLiteConnection;
        sqLiteCommand.CommandText = "vacuum";
        sqLiteCommand.CommandType = CommandType.Text;
        sqLiteCommand.CommandTimeout = 30;
        sqLiteCommand.ExecuteNonQuery();
      }
    }

    public bool OpenDb()
    {
      try
      {
        _sqLiteConn = new SQLiteConnection(_sqLiteConnString);
        _sqLiteConn.Open();
        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("打开数据库连接失败：" + ex.Message);
      }
    }

    public void CloseDb()
    {
      if (_sqLiteConn == null || _sqLiteConn.State == 0)
        return;
      if (_isRunTrans && _autoCommit)
        Commit();
      _sqLiteConn.Close();
      _sqLiteConn = null;
    }

    public void BeginTransaction()
    {
      _sqLiteConn.BeginTransaction();
      _isRunTrans = true;
    }

    public void BeginTransaction(IsolationLevel isoLevel)
    {
      _sqLiteConn.BeginTransaction(isoLevel);
      _isRunTrans = true;
    }

    public void Commit()
    {
      if (!_isRunTrans)
        return;
      _sqLiteTrans.Commit();
      _isRunTrans = false;
    }
  }
}
