// Decompiled with JetBrains decompiler
// Type: Auxiliary.Comm.COMM
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using MyWindowClient.DbHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using 微店新版;
using 微店新版.下单;


namespace Auxiliary.Comm
{
  public static class COMM
  {
    public static List<string> agentlist = new List<string>();

    public static string getagent()
    {
      try
      {
        int index = new Random((int) DateTime.Now.Ticks).Next(agentlist.Count);
        return agentlist[index];
      }
      catch (Exception ex)
      {
        return agentlist[0];
      }
    }

    public static string 满减id { get; set; }

    public static string 新客id1 { get; set; }

    public static string 新客id2 { get; set; }

    public static void CompanyDate(string dateStr1, string dateStr2, ref string msg)
    {
      int num = DateTime.Compare(Convert.ToDateTime(dateStr1), Convert.ToDateTime(dateStr2));
      if (num > 0)
        msg = "t1:(" + dateStr1 + ")大于t2(" + dateStr2 + ")";
      if (num == 0)
        msg = "t1:(" + dateStr1 + ")等于t2(" + dateStr2 + ")";
      if (num >= 0)
        return;
      msg = "t1:(" + dateStr1 + ")小于t2(" + dateStr2 + ")";
    }

    public static string TOKENS { get; set; }

    public static bool isgj { get; set; }

    public static string price { get; set; }

    public static bool isgz { get; set; }

    public static string thr { get; set; }

    public static string bieming { get; set; }

    public static string ts { get; set; }

    public static int isnm { get; set; }

    public static string shopid { get; set; }

    public static string pcid { get; set; }

    public static string pt_account { get; set; }

    public static string pt_pass { get; set; }

    public static string pt_token { get; set; }

    public static string proxy_ip { get; set; }

    public static int proxy_port { get; set; }

    public static string username { get; set; }

    public static string password { get; set; }

    public static string xmid { get; set; }

    public static string order_count { get; set; }

    public static string sp { get; set; }

    public static string paypass { get; set; }

    public static string card { get; set; }

    public static bool hdfk { get; set; }

    public static List<param> plist { get; set; }

    public static string shop_param { get; set; }

    public static string cookie { get; set; }

    public static long ConvertDateTimeToInt(DateTime time)
    {
      DateTime localTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
      return (time.Ticks - localTime.Ticks) / 10000L;
    }

    public static string r(int i)
    {
      char[] charArray = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
      string source = "";
      for (int index = 0; index < i; ++index)
      {
        char ch = charArray[(int) (new Random().NextDouble() * 36.0)];
        if (source.Contains<char>(ch))
          --index;
        else
          source += ch.ToString();
      }
      return source.ToLower();
    }

    public static string rS(int i)
    {
      List<string> values = new List<string>();
      while (values.Count < i)
      {
        foreach (char ch in Regex.Replace(Guid.NewGuid().ToString("N"), "[^0-9]+", ""))
        {
          if (values.Count < i)
            values.Add(ch.ToString());
          else
            break;
        }
      }
      return string.Join("", values);
    }

    public static string GetCookie(wdauth auth)
    {
      string loginToken = auth.loginToken;
      string duid = auth.duid;
      string sid = auth.sid;
      string uid = auth.uid;
      return "wdtoken=" + Guid.NewGuid().ToString().ToLower().Substring(8) + ";__spider__visitorid=" + r(16) + ";__spider__sessionid=" + r(16) + ";v-components/cpn-coupon-dialog@iwdDefault=1;login_token=" + loginToken + ";uid=" + uid + ";is_login=true;login_type=LOGIN_USER_TYPE_WECHAT;login_source=LOGIN_USER_SOURCE_WECHAT;duid=" + duid + ";sid=" + sid;
    }

    public static Cookie ToCookie(string cookie)
    {
      string[] strArray = cookie.Split('=');
      return new Cookie(strArray[0], strArray[1], ".weidian.com", "/", new DateTime?());
    }

    public static Dictionary<string, List<wdauth>> SplitList(List<wdauth> list, int num)
    {
      int count = list.Count;
      Dictionary<string, List<wdauth>> dictionary = new Dictionary<string, List<wdauth>>();
      List<wdauth> wdauthList = new List<wdauth>();
      for (int index = 0; index < count; ++index)
      {
        wdauthList.Add(list[index]);
        if ((index + 1) % num == 0 || index + 1 == count)
        {
          dictionary.Add("ContractItem" + index.ToString(), wdauthList);
          wdauthList = new List<wdauth>();
        }
      }
      return dictionary;
    }

    public static Dictionary<string, List<accounts>> SplitList(List<accounts> list, int num)
    {
      int count = list.Count;
      Dictionary<string, List<accounts>> dictionary = new Dictionary<string, List<accounts>>();
      List<accounts> accountsList = new List<accounts>();
      for (int index = 0; index < count; ++index)
      {
        accountsList.Add(list[index]);
        if ((index + 1) % num == 0 || index + 1 == count)
        {
          dictionary.Add("ContractItem" + index.ToString(), accountsList);
          accountsList = new List<accounts>();
        }
      }
      return dictionary;
    }

    public static Dictionary<string, List<pay>> SplitList(List<pay> list, int num)
    {
      int count = list.Count;
      Dictionary<string, List<pay>> dictionary = new Dictionary<string, List<pay>>();
      List<pay> payList = new List<pay>();
      for (int index = 0; index < count; ++index)
      {
        payList.Add(list[index]);
        if ((index + 1) % num == 0 || index + 1 == count)
        {
          dictionary.Add("ContractItem" + index.ToString(), payList);
          payList = new List<pay>();
        }
      }
      return dictionary;
    }

    public static Dictionary<string, List<Model.wdinfo>> SplitList(List<Model.wdinfo> list, int num)
    {
      int count = list.Count;
      Dictionary<string, List<Model.wdinfo>> dictionary = new Dictionary<string, List<Model.wdinfo>>();
      List<Model.wdinfo> wdinfoList = new List<Model.wdinfo>();
      for (int index = 0; index < count; ++index)
      {
        wdinfoList.Add(list[index]);
        if ((index + 1) % num == 0 || index + 1 == count)
        {
          dictionary.Add("ContractItem" + index.ToString(), wdinfoList);
          wdinfoList = new List<Model.wdinfo>();
        }
      }
      return dictionary;
    }

    public static Dictionary<string, List<info>> SplitList(List<info> list, int num)
    {
      int count = list.Count;
      Dictionary<string, List<info>> dictionary = new Dictionary<string, List<info>>();
      List<info> infoList = new List<info>();
      for (int index = 0; index < count; ++index)
      {
        infoList.Add(list[index]);
        if ((index + 1) % num == 0 || index + 1 == count)
        {
          dictionary.Add("ContractItem" + index.ToString(), infoList);
          infoList = new List<info>();
        }
      }
      return dictionary;
    }

    public static Dictionary<string, List<string>> SplitList(List<string> list, int num)
    {
      int count = list.Count;
      Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
      List<string> stringList = new List<string>();
      for (int index = 0; index < count; ++index)
      {
        stringList.Add(list[index]);
        if ((index + 1) % num == 0 || index + 1 == count)
        {
          dictionary.Add("ContractItem" + index.ToString(), stringList);
          stringList = new List<string>();
        }
      }
      return dictionary;
    }

    public static string cookies(string ck)
    {
      string[] strArray = ck.Split(';');
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str in strArray)
      {
        if (!str.Contains("Hm_l"))
          stringBuilder.Append(str.Trim() + ";");
      }
      return stringBuilder.ToString();
    }

    public static string ToJSON(object o)
    {
      return o == null ? null : JsonConvert.SerializeObject(o);
    }

    public static T FromJSON<T>(string input)
    {
      try
      {
        return JsonConvert.DeserializeObject<T>(input);
      }
      catch (Exception ex)
      {
        return default (T);
      }
    }

    public static JArray GetToJsonList1(string json)
    {
      return (JArray) JsonConvert.DeserializeObject(json);
    }

    public static JObject GetToJsonList(string json)
    {
      return (JObject) JsonConvert.DeserializeObject(json);
    }

    public static string ConvertDateTimeToString()
    {
      return ((DateTime.Now.Ticks - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).Ticks) / 10000L).ToString();
    }

    public static string urlencode(string value)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~*";
      foreach (char ch in value)
      {
        if (str.IndexOf(ch) != -1)
          stringBuilder.Append(ch);
        else
          stringBuilder.Append("%" + string.Format("{0:X2}", (int)ch));
      }
      return stringBuilder.ToString();
    }

    public static string getname()
    {
      List<string> list = bieming.Split('，').ToList<string>();
      return list[new Random().Next(list.Count)];
    }

    public static string tokens(string cookie)
    {
      string str1 = "";
      string str2 = cookie;
      char[] chArray = new char[1]{ ';' };
      foreach (string str3 in str2.Split(chArray))
      {
        if (str3.Contains("wdtoken"))
        {
          str1 = str3.Split('=')[1];
          break;
        }
      }
      return str1;
    }

    public static Dictionary<string, List<Model.plinfo>> SplitList(List<Model.plinfo> list, int num)
    {
      int count = list.Count;
      Dictionary<string, List<Model.plinfo>> dictionary = new Dictionary<string, List<Model.plinfo>>();
      List<Model.plinfo> plinfoList = new List<Model.plinfo>();
      for (int index = 0; index < count; ++index)
      {
        plinfoList.Add(list[index]);
        if ((index + 1) % num == 0 || index + 1 == count)
        {
          dictionary.Add("ContractItem" + index.ToString(), plinfoList);
          plinfoList = new List<Model.plinfo>();
        }
      }
      return dictionary;
    }

    public static string sid(string cookie)
    {
      string str1 = "";
      string str2 = cookie;
      char[] chArray = new char[1]{ ';' };
      foreach (string str3 in str2.Split(chArray))
      {
        if (str3.Contains("sid="))
        {
          str1 = str3.Split('=')[1];
          break;
        }
      }
      return str1;
    }

    public static string uid(string cookie)
    {
      string str1 = "";
      string str2 = cookie;
      char[] chArray = new char[1]{ ';' };
      foreach (string str3 in str2.Split(chArray))
      {
        if (str3.Contains("uid="))
        {
          str1 = str3.Split('=')[1];
          break;
        }
      }
      return str1;
    }

    public static DataTable GETRemark()
    {
      try
      {
        DataTable dataTable = MYSQL.Query("select DISTINCT remark from wd where token='" + TOKENS + "'");
        return dataTable == null || dataTable.Rows.Count == 0 ? null : dataTable;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public static DataTable orders()
    {
      try
      {
        DataTable dataTable = MYSQL.Query("select DISTINCT remark from orders where token='" + TOKENS + "'");
        return dataTable == null || dataTable.Rows.Count == 0 ? null : dataTable;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public static DataRow getauth(string id)
    {
      try
      {
        id = id.Split('，')[1];
        DataTable dataTable = MYSQL.Query("select * from wd_user1 where original='" + id + "' and remakr='" + TOKENS + "'");
        return dataTable == null || dataTable.Rows.Count == 0 ? null : dataTable.Rows[0];
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public static string Mycookies(string ck)
    {
      string[] strArray = ck.Split('，');
      string str1 = strArray[2];
      string str2 = strArray[5];
      string str3 = strArray[6];
      string str4 = strArray[7];
      return "login_token=" + str1 + ";is_login=true;login_type=LOGIN_USER_TYPE_MASTER;login_source=LOGIN_USER_SOURCE_MASTER;uid=" + str4 + ";duid=" + str2 + ";sid=" + str3 + ";wdtoken=" + r(8) + ";__spider__visitorid=" + r(16) + ";__spider__sessionid=" + r(16) + ";";
    }

    public static bool IsProcessTimeOut(string time)
    {
      return DateTime.Now > DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
    }

    public static string getCpu()
    {
      string cpu = null;
      using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementClass("win32_Processor").GetInstances().GetEnumerator())
      {
        if (enumerator.MoveNext())
          cpu = enumerator.Current.Properties["Processorid"].Value.ToString();
      }
      return cpu;
    }

    public static string CreateCode()
    {
      try
      {
        string str = getCpu() + GetDiskVolumeSerialNumber();
        string[] strArray = new string[24];
        for (int startIndex = 0; startIndex < 24; ++startIndex)
          strArray[startIndex] = str.Substring(startIndex, 1);
        string text = "";
        for (int index = 0; index < 24; ++index)
          text += strArray[index + 3 >= 24 ? 0 : index + 3];
        return GetMd5(text);
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public static string GetDiskVolumeSerialNumber()
    {
      ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
      ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
      managementObject.Get();
      return managementObject.GetPropertyValue("VolumeSerialNumber").ToString();
    }

    public static string GetMd5(object text)
    {
      text.ToString();
      return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(text.ToString()))).Replace("-", "");
    }
  }
}
