// Decompiled with JetBrains decompiler
// Type: 微店新版.RestHttp
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System;
using System.IO;
using System.Net;


namespace 微店新版
{
  public static class RestHttp
  {
    public static string GET(string url)
    {
      try
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";
        httpWebRequest.UserAgent = "Mozilla/5.0 (Linux; Android 5.1.1; VOG-AL10 Build/HUAWEIVOG-AL10; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/74.0.3729.136 Mobile Safari/537.36 MMWEBID/2343 MicroMessenger/7.0.15.1680(0x27000F34) Process/tools WeChat/arm32 NetType/WIFI Language/zh_CN ABI/arm32";
        httpWebRequest.Method = nameof (GET);
        httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        if (httpWebRequest == null || httpWebRequest.GetResponse() == null)
          return string.Empty;
        HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
        if (response == null)
          return string.Empty;
        using (Stream responseStream = response.GetResponseStream())
        {
          using (StreamReader streamReader = new StreamReader(responseStream))
            return streamReader.ReadToEnd();
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public static string GET(string url, string token)
    {
      try
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";
        httpWebRequest.UserAgent = "Mozilla/5.0 (Linux; Android 5.1.1; VOG-AL10 Build/HUAWEIVOG-AL10; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/74.0.3729.136 Mobile Safari/537.36 MMWEBID/2343 MicroMessenger/7.0.15.1680(0x27000F34) Process/tools WeChat/arm32 NetType/WIFI Language/zh_CN ABI/arm32";
        httpWebRequest.Method = nameof (GET);
        httpWebRequest.Headers.Add("Authorization", token);
        httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        if (httpWebRequest == null || httpWebRequest.GetResponse() == null)
          return string.Empty;
        HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
        if (response == null)
          return string.Empty;
        using (Stream responseStream = response.GetResponseStream())
        {
          using (StreamReader streamReader = new StreamReader(responseStream))
            return streamReader.ReadToEnd();
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }
  }
}
