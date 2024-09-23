// Decompiled with JetBrains decompiler
// Type: Auxiliary.HTTP.MYHTTP
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Auxiliary.HTTP
{
  public static class MYHTTP
  {
    public static string getagent()
    {
      try
      {
        int index = new Random((int) DateTime.Now.Ticks).Next(COMM.agentlist.Count);
        return COMM.agentlist[index];
      }
      catch (Exception ex)
      {
        return COMM.agentlist[0];
      }
    }

    public static Dictionary<string, string> headers(int type = 0)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (type == 99)
      {
        dictionary.Add("Host", "thor.weidian.com");
        dictionary.Add("Accept", "*/*");
        dictionary.Add("referrer", "https://ios.weidian.com");
        dictionary.Add("x-origin", "thor");
        dictionary.Add("Accept-Encoding", "GLZip");
        dictionary.Add("Accept-Language", "zh-CN,zh-Hans;q=0.9");
        dictionary.Add("origin", "ios.weidian.com");
        dictionary.Add("x-schema", "https");
        dictionary.Add("x-encrypt", "1");
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("Referer", "https://ios.weidian.com");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 22)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "application/json, */*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("origin", "https://weidian.com");
        dictionary.Add("referer", "https://weidian.com/weidian-h5/user/index.html?&vc_wfr=dahao_caidan");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 88)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "application/json, */*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("origin", "https://weidian.com");
        dictionary.Add("referer", "https://weidian.com/");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 0)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
      }
      if (type == 1)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "*/*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("Content-Type", "application/x-www-form-urlencoded");
        dictionary.Add("origin", "https://sso.weidian.com");
        dictionary.Add("referer", "https://sso.weidian.com/login/index.php?redirect=https%3A%2F%2Fweidian.com%2Fweidian-h5%2Fuser%2Findex.html%3F%26vc_wfr%3Ddahao_caidan");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 2)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "application/json, */*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("origin", "https://weidian.com");
        dictionary.Add("referer", "https://weidian.com/weidian-h5/user/index.html?&vc_wfr=dahao_caidan");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 3)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "application/json, */*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 4)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "application/json, */*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("Content-Type", "application/x-www-form-urlencoded");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("origin", "https://weidian.com");
        dictionary.Add("referer", "https://weidian.com/");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 5)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "application/json, */*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("Content-Type", "application/x-www-form-urlencoded");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("origin", "https://d.weidian.com");
        dictionary.Add("referer", "https://d.weidian.com/");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      if (type == 6)
      {
        dictionary.Add("User-Agent", getagent());
        dictionary.Add("accept", "application/json, */*");
        dictionary.Add("accept-encoding", "gzip, deflate, br");
        dictionary.Add("Content-Type", "application/x-www-form-urlencoded");
        dictionary.Add("accept-language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        dictionary.Add("origin", "https://weidian.com");
        dictionary.Add("referer", "https://weidian.com/weidian-h5/user/index.html?&vc_wfr=dahao_caidan");
        dictionary.Add("proxy", COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
      }
      return dictionary;
    }

    public static void SetCertificatePolicy()
    {
      ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(RemoteCertificateValidate);
    }

    private static bool RemoteCertificateValidate(
      object sender,
      X509Certificate cert,
      X509Chain chain,
      SslPolicyErrors error)
    {
      Console.WriteLine("Warning, trust any certificate");
      return true;
    }

    public static async Task<HttpRequestEntity> Result(
      string Method,
      string url,
      int type,
      Dictionary<string, string> param,
      string cookie,
      string ContentType = "application/x-www-form-urlencoded")
    {
            SetCertificatePolicy();
      Dictionary<string, string> head = headers(type);
      HttpRequestEntity result = new HttpRequestEntity()
      {
        IsSuccess = 0
      };
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      HttpStatusCode statusCode = HttpStatusCode.OK;
      Task<WebResponse> reqPOST = null;
      WebResponse Res = null;
      try
      {
        reqPOST = HttpHelperAsync.HttpRequestAsync(Method, url, head, param, Encoding.UTF8, ContentType, cookie);
        WebResponse webResponse = await reqPOST;
        Res = reqPOST.Result;
        statusCode = ((HttpWebResponse) Res).StatusCode;
        result.ResponseLength = Res.ContentLength;
        result.ResponseEncodingName = ((HttpWebResponse) Res).ContentEncoding;
        stopwatch.Stop();
        result.IsSuccess = 0;
        StreamReader readerPOST = new StreamReader(Res.GetResponseStream(), Encoding.UTF8);
        string retJson = readerPOST.ReadToEnd();
        readerPOST.Close();
        Res.Close();
        result.ts = stopwatch;
        result.ResponseContent = retJson;
        if (type == 22)
        {
          result.cookie = Res.Headers["Set-Cookie"].ToString().Split(';')[0].Split('=')[1];
        }
        else
        {
          StringBuilder sb = new StringBuilder();
          foreach (Cookie item in ((HttpWebResponse) Res).Cookies)
            sb.Append(item.Name + "=" + item.Value + ";");
          result.cookie = sb.ToString();
          sb = null;
        }
        readerPOST = null;
        retJson = null;
      }
      catch (WebException ex)
      {
        Res = ex.Response;
        if (Res == null)
        {
          result.IsSuccess = 1;
          result.ResponseContent = ex.Message;
          result.ts = stopwatch;
        }
      }
      catch (Exception ex)
      {
        result.IsSuccess = 1;
        result.ResponseContent = ex.Message;
        result.ts = stopwatch;
      }
      HttpRequestEntity httpRequestEntity = result;
      head = null;
      result = null;
      stopwatch = null;
      reqPOST = null;
      Res = null;
      return httpRequestEntity;
    }
  }
}
