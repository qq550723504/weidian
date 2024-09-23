// Decompiled with JetBrains decompiler
// Type: 微店新版.HTTP.MyHttp
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Flurl.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace 微店新版.HTTP
{
  public class MyHttp
  {
    private HttpClient hc;
    private FlurlClient result;

    public MyHttp()
    {
      SetCertificatePolicy();
      hc = new HttpClient(new HttpClientHandler()
      {
          AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate),
          Proxy = new WebProxy(COMM.proxy_ip + ":" + COMM.proxy_port.ToString())
          {
              Credentials = new NetworkCredential(COMM.username, COMM.password)
          },
          UseCookies = false
      });
      hc.DefaultRequestHeaders.ExpectContinue = new bool?(false);
      result = new FlurlClient(hc);
    }

    public void SetCertificatePolicy()
    {
      ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(RemoteCertificateValidate);
    }

    private bool RemoteCertificateValidate(
      object sender,
      X509Certificate cert,
      X509Chain chain,
      SslPolicyErrors error)
    {
      Console.WriteLine("Warning, trust any certificate");
      return true;
    }

    public async Task<string> POST(string url, string cookie, object param)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            origin = "https://weidian.com",
            Host = "thor.weidian.com",
            accept = "application/json, */*",
            content_type = "application/x-www-form-urlencoded; charset=UTF-8",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x6309092b) XWEB/9079 Flue",
            Referer = "https://weidian.com/",
            Cookie = cookie,
            accept_language = "zh-CN,zh;q=0.9"
        }).PostUrlEncodedAsync(param);
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> POST222(string url, string cookie, object param)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            user_agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x6309092b) XWEB/9079 Flue",
            origin = "https://weidian.com",
            referer = "https://weidian.com/",
            accept_encoding = "gzip, deflate, br, zstd",
            accept_language = "zh-CN,zh;q=0.9",
            Cookie = cookie
        }).PostUrlEncodedAsync(param);
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }
  }
}
