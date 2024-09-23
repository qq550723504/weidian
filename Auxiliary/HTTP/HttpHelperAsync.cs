// Decompiled with JetBrains decompiler
// Type: Auxiliary.HTTP.HttpHelperAsync
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace Auxiliary.HTTP
{
  public class HttpHelperAsync
  {
    private bool _disposable = false;

    public static Task<WebResponse> HttpRequestAsync(
      string getOrPost,
      string url,
      Dictionary<string, string> headers,
      Dictionary<string, string> parameters,
      Encoding dataEncoding,
      string contentType,
      string cookie)
    {
      HttpWebRequest httpWebRequest = null;
      GC.Collect();
      HttpWebRequest request = CreateRequest(getOrPost, url, headers, parameters, dataEncoding, contentType, cookie);
      if (getOrPost == "POST" && parameters != null && parameters.Count != 0)
      {
        byte[] buffer = FormatPostParameters(parameters, dataEncoding, contentType);
        Stream requestStream;
        try
        {
          requestStream = request.GetRequestStream();
        }
        catch (Exception ex)
        {
          request.Abort();
          httpWebRequest = null;
          throw;
        }
        if (requestStream != null)
        {
          requestStream.Write(buffer, 0, buffer.Length);
          requestStream.Close();
        }
      }
      return request.GetResponseAsync();
    }

    private static HttpWebRequest CreateRequest(
      string getOrPost,
      string url,
      Dictionary<string, string> headers,
      Dictionary<string, string> parameters,
      Encoding paraEncoding,
      string contentType,
      string cookie)
    {
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      if (parameters != null && parameters.Count > 0 && paraEncoding == null)
        throw new ArgumentNullException("requestEncoding");
      if (getOrPost == "GET" && parameters != null && parameters.Count > 0)
        url = FormatGetParametersToUrl(url, parameters, paraEncoding);
      HttpWebRequest request;
      if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
      {
        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        request = WebRequest.Create(url) as HttpWebRequest;
        request.ProtocolVersion = HttpVersion.Version11;
      }
      else
        request = WebRequest.Create(url) as HttpWebRequest;
      if (getOrPost == "GET")
        request.Method = "GET";
      else
        request.Method = "POST";
      if (contentType == null)
        request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
      else
        request.ContentType = contentType;
      ServicePointManager.DefaultConnectionLimit = 65500;
      request.ServicePoint.Expect100Continue = false;
      request.ServicePoint.ConnectionLimit = int.MaxValue;
      request.ServicePoint.UseNagleAlgorithm = false;
      request.AllowWriteStreamBuffering = true;
      request.KeepAlive = true;
      request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      if (headers != null)
      {
                FormatRequestHeaders(headers, request);
        if (!string.IsNullOrWhiteSpace(cookie))
          request.CookieContainer = Cookiestr2CookieContainer(cookie, ".weidian.com");
      }
      return request;
    }

    private static void FormatRequestHeaders(
      Dictionary<string, string> headers,
      HttpWebRequest request)
    {
      foreach (KeyValuePair<string, string> header in headers)
      {
        switch (header.Key.ToLower())
        {
          case "Accept":
            request.Accept = header.Value;
            break;
          case "Accept-Encoding":
            request.Headers.Add("Accept-Encoding", header.Value);
            break;
          case "Accept-Language":
            request.Headers.Add("Accept-Encoding", header.Value);
            break;
          case "Content-Type":
            request.ContentType = header.Value;
            break;
          case "Host":
            request.Host = header.Value;
            break;
          case "Referer":
            request.Referer = header.Value;
            break;
          case "User-Agent":
            request.UserAgent = header.Value;
            break;
          case "accept":
            request.Accept = header.Value;
            break;
          case "accept-encoding":
            request.Headers.Add("accept-encoding", header.Value);
            break;
          case "accept-language":
            request.Headers.Add("accept-language", header.Value);
            break;
          case "connection":
            request.KeepAlive = false;
            break;
          case "origin":
            request.Headers.Add("origin", header.Value);
            break;
          case "proxy":
            request.Proxy = new WebProxy(header.Value)
            {
                Credentials = new NetworkCredential(COMM.username, COMM.password)
            };
            break;
          case "referer":
            request.Referer = header.Value;
            break;
          case "referrer":
            request.Headers.Add("referrer", header.Value);
            break;
          case "x-encrypt":
            request.Headers.Add("x-encrypt", header.Value);
            break;
          case "x-origin":
            request.Headers.Add("x-origin", header.Value);
            break;
          case "x-schema":
            request.Headers.Add("x-schema", header.Value);
            break;
        }
      }
    }

    public static CookieContainer Cookiestr2CookieContainer(string cooikes, string domain)
    {
      CookieCollection cookies = new CookieCollection();
      foreach (Match match in new Regex("(\\S*?)=(.*?)(?:;|$)", RegexOptions.Compiled).Matches(cooikes))
        cookies.Add(new Cookie(match.Groups[1].Value, match.Groups[2].Value, "", domain));
      CookieContainer cookieContainer = new CookieContainer();
      cookieContainer.Add(cookies);
      return cookieContainer;
    }

    private static string FormatGetParametersToUrl(
      string url,
      Dictionary<string, string> parameters,
      Encoding paraEncoding)
    {
      if (url.IndexOf("?") < 0)
        url += "?";
      int num = 0;
      string str = "";
      foreach (KeyValuePair<string, string> parameter in parameters)
      {
        if (num > 0)
          str += "&";
        str = str + HttpUtility.UrlEncode(parameter.Key, paraEncoding) + "=" + HttpUtility.UrlEncode(parameter.Value, paraEncoding);
        ++num;
      }
      url += str;
      return url;
    }

    private static byte[] FormatPostParameters(
      Dictionary<string, string> parameters,
      Encoding dataEncoding,
      string contentType)
    {
      string s = "";
      int num = 0;
      if (!string.IsNullOrEmpty(contentType) && contentType.ToLower().Trim().Contains("application/json"))
        s = "{";
      foreach (KeyValuePair<string, string> parameter in parameters)
      {
        s = string.IsNullOrEmpty(contentType) || !contentType.ToLower().Trim().Contains("application/json") ? (num <= 0 ? string.Format("{0}={1}", parameter.Key, HttpUtility.UrlEncode(parameter.Value, dataEncoding)) : s + string.Format("&{0}={1}", parameter.Key, HttpUtility.UrlEncode(parameter.Value, dataEncoding))) : (num <= 0 ? (!parameter.Value.StartsWith("{") ? s + string.Format("\"{0}\":\"{1}\"", parameter.Key, parameter.Value) : s + string.Format("\"{0}\":{1}", parameter.Key, parameter.Value)) : (!parameter.Value.StartsWith("{") ? s + string.Format(",\"{0}\":\"{1}\"", parameter.Key, parameter.Value) : s + string.Format(",\"{0}\":{1}", parameter.Key, parameter.Value)));
        ++num;
      }
      if (!string.IsNullOrEmpty(contentType) && contentType.ToLower().Trim().Contains("application/json"))
        s += "}";
      return dataEncoding.GetBytes(s);
    }

    private static bool CheckValidationResult(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors errors)
    {
      return true;
    }
  }
}
