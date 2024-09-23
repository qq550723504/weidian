using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using 微店新版.HTTP;

namespace Auxiliary.Comm
{
    public class Weidian
    {
        private flurl flurl;
        public Weidian(flurl flurl)
        {
            this.flurl = flurl;
        }
        private string user_agent => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36 Edg/128.0.0.0";
        private string GetUa()
        {

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "*/*"},
                {"Accept-Language", "zh-CN,zh;q=0.9"},
                {"Cache-Control", "no-cache"},
                {"Connection", "keep-alive"},
                {"Pragma", "no-cache"},
                {"Referer", "https://sso.weidian.com/login/index.php"},
                {"Sec-Fetch-Dest", "script"},
                {"Sec-Fetch-Mode", "no-cors"},
                {"Sec-Fetch-Site", "same-site"},
                {"User-Agent", user_agent}
            };
            byte[] userAgentBytes = Encoding.UTF8.GetBytes(headers["User-Agent"]);
            return Convert.ToBase64String(userAgentBytes);
        }
        public async Task<string> GetSlideTicket()
        {
            return await flurl.API_GET("http://124.223.8.244:5000/get_ticket");
        }
        public async Task<(bool success, string data)> SendSmsCode(string phone, bool forceGraph)
        {
            try
            {
                var result = await GetSlideTicket();
                var slideTicket = COMM.GetToJsonList(result)["data"].ToObject<SlideTicket>();
                if (slideTicket != null && !string.IsNullOrEmpty(slideTicket.ticket))
                {
                    result = await flurl.API_POST("https://thor.weidian.com/passport/get.vcode/2.0", new GetVcode
                    {
                        phone = phone,
                        forceGraph = forceGraph,
                        slideTicket = slideTicket.ticket,
                        slideRandStr = slideTicket.randStr
                    });
                    return (true, result);
                }
                else
                {
                    return (false, "验证码识别失败");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<string> Check(string phone, string vcode)
        {
            try
            {
                return await flurl.API_POST("https://sso.weidian.com/user/vcode/check", new CheckResult
                {
                    phone = phone,
                    vcode = vcode,
                    ua = GetUa()
                });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> Register(string phone, string password, string session)
        {
            return await flurl.API_POST("https://sso.weidian.com/user/regist", new RegisterResult
            {
                phone = phone,
                password = password,
                session = session,
                ua = GetUa()
            });
        }
        public async Task<string> Login(string phone, string vcode)
        {
            return await flurl.API_POST("https://sso.weidian.com/user/loginbyvcode", new LoginResult
            {
                phone = phone,
                vcode = vcode,
                ua = GetUa()
            });
        }
    }
    public class LoginResult
    {
        public int countryCode => 86;
        public string phone;
        public string vcode;
        public string ua;
    }
    public class RegisterResult
    {
        public int countryCode => 86;
        public int version => 1;
        public string phone;
        public string password;
        public string session;
        public string ua;
    }
    public class CheckResult
    {
        public string phone;
        public string ua;
        public string vcode;
        public string countryCode => "86";
        public string action => "//sso.weidian.com/user/regist";
    }
    public class GetVcode
    {
        public string countryCode => "86";
        public string action => "weidian";
        public string scene => "H5Login";
        public bool forceGraph;
        public string slideImageAppId => "2003473469";
        public string phone;
        public string slideTicket;
        public string slideRandStr;
    }
    public class SlideTicket
    {
        public string errMessage;
        public string errorCode;
        public string ticket;
        public string randStr;
        public string sess;
        public string traceId;
    }
}