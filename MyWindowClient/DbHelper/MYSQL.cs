// Decompiled with JetBrains decompiler
// Type: MyWindowClient.DbHelper.MYSQL
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Maticsoft.DBUtility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace MyWindowClient.DbHelper
{
  public static class MYSQL
  {
    public static void Init()
    {
      DbHelperMySQL.connectionString = "server=47.106.178.15;port=3306;database=soft_wd;user=soft_wd;password=4Lm8NnXNcbAWzLLH;SslMode=none;Charset=utf8mb4;allowPublicKeyRetrieval=true";
    }

    public static void Initwd()
    {
      DbHelperMySQL.connectionString = "server=115.239.214.41;port=3306;database=soft_wd;user=root;password=zhang.5201;SslMode=none;Charset=utf8";
    }

    public static void Init(string conn) => DbHelperMySQL.connectionString = conn;

    public static DataTable Query(string sql)
    {
      if (string.IsNullOrWhiteSpace(sql))
        return new DataTable();
      DataSet dataSet = DbHelperMySQL.Query(sql,10000);
      return dataSet == null || dataSet.Tables.Count == 0 ? new DataTable() : dataSet.Tables[0];
    }

    public static DataTable Existence(string sql, ref bool flog)
    {
      DataTable dataTable = Query(sql);
      if (dataTable != null && dataTable.Rows.Count > 0)
        flog = true;
      return dataTable;
    }

    public static int exp(string sql)
    {
      return string.IsNullOrWhiteSpace(sql) ? 0 : DbHelperMySQL.ExecuteSql(sql);
    }

    public static int exp1(string sql, params MySqlParameter[] parameters)
    {
      return string.IsNullOrWhiteSpace(sql) ? 0 : DbHelperMySQL.InsertDataAndGetId(sql, parameters);
    }

    public static int exp(List<string> sqlls)
    {
      return sqlls == null || sqlls.Count == 0 ? 0 : DbHelperMySQL.ExecuteSqlTran(sqlls);
    }

    public static Decimal UserMoney(string sql)
    {
      if (string.IsNullOrWhiteSpace(sql))
        return 0M;
      Decimal num = 0M;
      object single = DbHelperMySQL.GetSingle(sql);
      if (single != null)
        num = Convert.ToDecimal(single);
      return num;
    }
  }
}
