// Decompiled with JetBrains decompiler
// Type: 微店新版.HTTP.flurl
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Auxiliary.HTTP;
using Flurl.Http;
using Flurl.Http.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace 微店新版.HTTP
{
  public class flurl
  {
    private HttpClient hc;
    private FlurlClient result;

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

    public flurl()
    {
      SetCertificatePolicy();
      hc = new HttpClient(new HttpClientHandler()
      {
        AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate),
        MaxConnectionsPerServer = 2000,
        Proxy = new WebProxy(COMM.proxy_ip + ":" + COMM.proxy_port.ToString())
        {
          Credentials = new NetworkCredential(COMM.username, COMM.password)
        },
        AllowAutoRedirect = false,
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

    public async Task<string> GET(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            Host = "thor.weidian.com",
            content_type = "application/x-www-form-urlencoded",
            x_origin = "wechat.weidian.com",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent(),
            Referer = "https://servicewechat.com/wx4b74228baa15489a/495/page-frame.html"
        }).GetAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> GET2(string url)
    {
      try
      {
        IFlurlResponse t = await url.GetAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> POST(string url, object param)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            Host = "thor.weidian.com",
            content_type = "application/x-www-form-urlencoded",
            x_origin = "wechat.weidian.com",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent(),
            Referer = "https://servicewechat.com/wx9794a0135ea909fc/123/page-frame.html"
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

    public async Task<string> POST888(string url, object param)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            Host = "thor.weidian.com",
            content_type = "application/x-www-form-urlencoded",
            x_origin = "wechat.weidian.com",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent(),
            Referer = "https://servicewechat.com/wx9794a0135ea909fc/123/page-frame.html"
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

    public async Task<string> POST999(string url, string cookie, object param)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            sec_ch_ua = "\"Google Chrome\";v=\"123\", \"Not:A-Brand\";v=\"8\", \"Chromium\";v=\"123\"",
            accept = "application/json, */*",
            content_type = "application/x-www-form-urlencoded",
            sec_ch_ua_platform = "\"Windows\"",
            origin = "https://shop1625218842.v.weidian.com",
            Host = "thor.weidian.com",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent(),
            Referer = "https://shop1625218842.v.weidian.com/",
            cookie = cookie
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

    public async Task<string> POSTFILE(string url, string cookie, string imgurl)
    {
      try
      {
        IFlurlResponse response = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            cookie = cookie
        }).PostMultipartAsync(content => content.AddFile("file", imgurl));
        string data = await response.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> GET999(string url, string cookie)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            sec_ch_ua = "\"Google Chrome\";v=\"123\", \"Not:A-Brand\";v=\"8\", \"Chromium\";v=\"123\"",
            accept = "application/json, */*",
            content_type = "application/x-www-form-urlencoded",
            sec_ch_ua_platform = "\"Windows\"",
            origin = "https://shop1625218842.v.weidian.com",
            Host = "thor.weidian.com",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent(),
            Referer = "https://shop1625218842.v.weidian.com/",
            cookie = cookie
        }).GetAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> POSTS1(string url, object param)
    {
      try
      {
        IFlurlResponse t = await url.WithHeaders(new
        {
            Host = "thor.weidian.com",
            content_type = "application/x-www-form-urlencoded",
            x_origin = "wechat.weidian.com",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent(),
            Referer = "https://servicewechat.com/wx9794a0135ea909fc/123/page-frame.html"
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

    public string UploadImage(
      string uploadUrl,
      string imgPath,
      string cookie,
      string fileparameter = "file")
    {
      HttpWebRequest httpWebRequest = WebRequest.Create(uploadUrl) as HttpWebRequest;
      httpWebRequest.AllowAutoRedirect = true;
      httpWebRequest.Method = "POST";
      httpWebRequest.CookieContainer = HttpHelperAsync.Cookiestr2CookieContainer(cookie, ".weidian.com");
      string str1 = DateTime.Now.Ticks.ToString("X");
      httpWebRequest.ContentType = "multipart/form-data;charset=utf-8;boundary=" + str1;
      byte[] bytes1 = Encoding.UTF8.GetBytes("\r\n--" + str1 + "\r\n");
      byte[] bytes2 = Encoding.UTF8.GetBytes("\r\n--" + str1 + "--\r\n");
      int num = imgPath.LastIndexOf("/");
      string str2 = imgPath.Substring(num + 1);
      byte[] bytes3 = Encoding.UTF8.GetBytes(new StringBuilder(string.Format("Content-Disposition:form-data;name=\"" + fileparameter + "\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", str2)).ToString());
      FileStream fileStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
      byte[] buffer = new byte[fileStream.Length];
      fileStream.Read(buffer, 0, buffer.Length);
      fileStream.Close();
      Stream requestStream = httpWebRequest.GetRequestStream();
      requestStream.Write(bytes1, 0, bytes1.Length);
      requestStream.Write(bytes3, 0, bytes3.Length);
      requestStream.Write(buffer, 0, buffer.Length);
      requestStream.Write(bytes2, 0, bytes2.Length);
      requestStream.Close();
      return new StreamReader((httpWebRequest.GetResponse() as HttpWebResponse).GetResponseStream(), Encoding.UTF8).ReadToEnd();
    }

    public static string DoPostFile(string url, string filePath, string cookie)
    {
      try
      {
        string fileName = Path.GetFileName(filePath);
        string str = "---------------" + DateTime.Now.Ticks.ToString("x");
        byte[] bytes1 = Encoding.ASCII.GetBytes("--" + str + "\r\n");
        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        byte[] bytes2 = Encoding.ASCII.GetBytes("--" + str + "--\r\n");
        byte[] bytes3 = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", "file", fileName));
        MemoryStream memoryStream = new MemoryStream();
        memoryStream.Write(bytes1, 0, bytes1.Length);
        memoryStream.Write(bytes3, 0, bytes3.Length);
        byte[] buffer1 = new byte[1024];
        int count;
        while ((count = fileStream.Read(buffer1, 0, buffer1.Length)) != 0)
          memoryStream.Write(buffer1, 0, count);
        fileStream.Close();
        string format = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("len", "500");
        dictionary.Add("wid", "300");
        foreach (string key in dictionary.Keys)
        {
          byte[] bytes4 = Encoding.UTF8.GetBytes(string.Format(format, key, dictionary[key]));
          memoryStream.Write(bytes1, 0, bytes1.Length);
          memoryStream.Write(bytes4, 0, bytes4.Length);
        }
        memoryStream.Write(bytes2, 0, bytes2.Length);
        memoryStream.Position = 0L;
        byte[] buffer2 = new byte[memoryStream.Length];
        memoryStream.Read(buffer2, 0, buffer2.Length);
        memoryStream.Close();
        WebProxy webProxy = new WebProxy(COMM.proxy_ip + ":" + COMM.proxy_port.ToString());
        webProxy.Credentials = new NetworkCredential(COMM.username, COMM.password);
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Proxy = webProxy;
        httpWebRequest.CookieContainer = HttpHelperAsync.Cookiestr2CookieContainer(cookie, ".weidian.com");
        httpWebRequest.Method = "POST";
        httpWebRequest.Timeout = 100000;
        httpWebRequest.ContentType = "multipart/form-data; boundary=" + str;
        httpWebRequest.ContentLength = buffer2.Length;
        httpWebRequest.KeepAlive = false;
        httpWebRequest.ProtocolVersion = HttpVersion.Version10;
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(buffer2, 0, buffer2.Length);
        requestStream.Close();
        HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
        string end;
        using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
          end = streamReader.ReadToEnd();
        response.Close();
        httpWebRequest.Abort();
        return end;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public static Stream BytesToStream(byte[] bytes) => new MemoryStream(bytes);

    public async Task<string> POSTH5(string url, object param)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders<IFlurlRequest>(new
        {
            Host = "thor.weidian.com",
            content_type = "application/x-www-form-urlencoded",
            origin = "https://weidian.com",
            referer = "https://weidian.com/",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent()
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

    public async Task<string> GET3(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders(new
        {
            Host = "thor.weidian.com",
            content_type = "application/x-www-form-urlencoded",
            accept = "application/json, */*",
            Accept_Encoding = "gzip, deflate, br",
            accept_language = "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7",
            User_Agent = getagent()
        }).GetAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> GETXH(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithHeaders(new
        {
            Host = "api.asclepius.whxh.com.cn",
            accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
            Accept_Encoding = "gzip,compress,br,deflate",
            User_Agent = getagent(),
            Referer = "https://ali.whxh.com.cn/"
        }).WithAutoRedirect(false).GetAsync();
        string data = await t.GetStringAsync();
        foreach (var item in t.Headers)
        {
          if (item.Item1.ToLower() == "location")
          {
            data = item.Item2;
            break;
          }
        }
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> GETXHAUTH(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithHeaders(new
        {
          Host = "ali.whxh.com.cn",
          accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
          Accept_Encoding = "gzip,compress,br,deflate",
          User_Agent = getagent(),
          Referer = "https://ali.whxh.com.cn/"
        }).WithAutoRedirect(false).GetAsync();
        string data = await t.GetStringAsync();
        foreach (var item in t.Headers)
        {
          if (item.Item1.ToLower() == "set-cookie")
          {
            data = item.Item2.Split(';')[0].Replace("token=", "");
            break;
          }
        }
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> OPENID(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders(new
        {
            Host = "api-c-prod.etcp.cn",
            content_type = "application/json",
            ayaya_v = "2.8.87",
            versionname = "5.5.0",
            version = "2.8.87",
            biztime = weidian.ConvertDateTimeToString(),
            ayaya_u = "168779092954515709",
            User_Agent = getagent(),
            Referer = "https://servicewechat.com/wxc07f9d67923d676d/370/page-frame.html"
        }).GetAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> API_POST(string url, object param)
    {
      try
      {
        IFlurlResponse t = await url.PostJsonAsync(param);
        Stream data = await t.GetStreamAsync();
        string result = string.Empty;
        using (StreamReader reader = new StreamReader(data, Encoding.GetEncoding("gbk")))
          result = reader.ReadToEnd();
        return result;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> API_POST(string url)
    {
      try
      {
        IFlurlResponse t = await url.PostAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> API_POSTURL(string url, object param)
    {
      try
      {
        IFlurlResponse t = await url.PostUrlEncodedAsync(param);
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> API_GET(string url)
    {
      try
      {
        IFlurlResponse t = await url.GetAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> IMAGEDOW(string url, string path)
    {
      try
      {
        string t = await url.DownloadFileAsync(path);
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> API_POST1(string url, object param, string Authorization = "")
    {
      try
      {
        IFlurlResponse t = await url.WithHeaders(new
        {
            Content_Type = "application/json-patch+json",
            Authorization = Authorization
        }).PostJsonAsync(param);
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> get(string url)
    {
      try
      {
        IFlurlResponse result = await url.GetAsync();
        string data = await result.GetStringAsync();
        return data;
      }
      catch (Exception ex1)
      {
        Exception ex = ex1;
        return null;
      }
    }

    public static string API_POST1(string url, string postData)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json";
        byte[] bytes = Encoding.UTF8.GetBytes(postData);
        httpWebRequest.ContentLength = bytes.Length;
        using (Stream requestStream = httpWebRequest.GetRequestStream())
        {
          requestStream.Write(bytes, 0, bytes.Length);
          requestStream.Close();
        }
        using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("gbk")))
          str = streamReader.ReadToEnd();
        return str;
      }
      catch (Exception ex)
      {
      }
      return "";
    }

    public async Task<wdauth> GETWDH5(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithHeaders(new
        {
            Host = "api.asclepius.whxh.com.cn",
            accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
            Accept_Encoding = "gzip, deflate, br",
            User_Agent = getagent(),
            Referer = "https://open.weixin.qq.com/"
        }).WithAutoRedirect(false).GetAsync();
        string data = await t.GetStringAsync();
        wdauth wdauth = new wdauth();
        foreach (var item in (IEnumerable<(string, string)>) t.Headers)
        {
          if (item.Item1.ToLower() == "set-cookie")
          {
            if (item.Item2.StartsWith("login_token="))
            {
              string sp = item.Item2.Split(';')[0].Replace("login_token=", "");
              wdauth.loginToken = sp;
              sp = null;
            }
            if (item.Item2.StartsWith("uid="))
            {
              string sp = item.Item2.Split(';')[0].Replace("uid=", "");
              wdauth.uid = sp;
              sp = null;
            }
            if (item.Item2.StartsWith("duid="))
            {
              string sp = item.Item2.Split(';')[0].Replace("duid=", "");
              wdauth.duid = sp;
              sp = null;
            }
            if (item.Item2.StartsWith("sid="))
            {
              string sp = item.Item2.Split(';')[0].Replace("sid=", "");
              wdauth.sid = sp;
              sp = null;
            }
          }
        }
        return wdauth;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<string> GETWDH51(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithHeaders(new
        {
            Host = "api.asclepius.whxh.com.cn",
            accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
            Accept_Encoding = "gzip, deflate, br",
            User_Agent = getagent(),
            Referer = "https://sso.weidian.com/login/index.php?redirect=https%3A%2F%2Fweidian.com%2Fweidian-h5%2Fuser%2Findex.html%3F%26vc_wfr%3Ddahao_caidan"
        }).WithAutoRedirect<IFlurlRequest>(false).GetAsync();
        string data = await t.GetStringAsync();
        return data;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }

    public async Task<Dictionary<string, string>> WXAUTH(string url)
    {
      try
      {
        IFlurlResponse t = await url.WithClient(result).WithHeaders( new
        {
          origin = "https://open.weixin.qq.com",
          accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
          accept_language = "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7",
          accept_encoding = "gzip, deflate",
          User_Agent = getagent(),
          Referer = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx7c66568ee7a30417&redirect_uri=https%3A%2F%2Fsso.weidian.com%2Fuser%2Foauth%2Fwechat%2Fcallback&response_type=code&scope=snsapi_userinfo&state=STATE_1_1bcf52d337dc127a700105e49d9bc1ae_xiaoxizhushou&connect_redirect=1&uin=MTgxMjg3MDAzOQ%3D%3D&key=8950eb6d25dd63473ec7b53f1d69626c18c24b6a00711ec1688e91dcf7029a606c75ea73c6b21215df2941301b0fefcd8afe9eb1c8657cc99dd1f3ac890b5816218bf098fd36a36a70fcf7075521be15044454d601d5aae1dcee9fcd0fabf87fa5be69d9117dc41341a3de4501ca4e1d57e29193d929fd2579b5a503be358c6c&version=63070517&exportkey=n_ChQIAhIQLtee6WLV%2Fe0vIBL1lGwHChLMAQIE97dBBAEAAAAAAOwZFFboLuYAAAAOpnltbLcz9gKNyK89dVj0OFqmn1PtGYrvd%2BYjVdoQYGWCxDQEzsTu4WWChAwOe2k9V5vAeBqdg59IV6%2FODQF28Y8SWoKhVYF9ONMUIdyyPAwchwdJLAhF2fzTHRuN7nDvpPKbfIhAhWfxoMHO5gLHv2XOr1D8q1dC2JaHdha3xlt4GC%2Bzr%2B46JdsH8lv%2FIs%2FpjmPW8xeM7jCOwecD5mHfH9nG6lshlnoaLJNQdaHs%2BbFK3Ivo0A%3D%3D&pass_ticket=yxvtA2x3DbI%2FnIMAkTi7iksMYRL6Mi8zSDdBhNHRfdFxJnEmQWvoH93oZFplNfsyTA628tjj9eJ9Wge1ntxW0Q%3D%3D&wx_header=0",
          Cookie = ("wdtoken=" + COMM.r(8) + ";__spider__visitorid=" + COMM.r(16) + ";__spider__sessionid=" + COMM.r(16) + ";")
        }).GetAsync();
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach (var item in (IEnumerable<(string, string)>) t.Headers)
        {
          if (item.Item1 == "set-cookie")
          {
            string[] sp = item.Item2.Split('=');
            dic.Add(sp[0], sp[1].Split(';')[0]);
            sp = null;
          }
        }
        return dic;
      }
      catch (FlurlHttpException ex1)
      {
        FlurlHttpException ex = ex1;
        return null;
      }
    }
  }
}
