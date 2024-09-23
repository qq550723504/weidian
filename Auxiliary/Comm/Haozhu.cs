using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using 微店新版.HTTP;

namespace Auxiliary.Comm
{
    public class Haozhu
    {
        private flurl flurl;
        public Haozhu(flurl flurl)
        {
            this.flurl = flurl;
        }
        public async Task<string> GetToken()
        {
            try
            {
                string result = await flurl.API_GET($"https://api.6333600.com/sms/?api=login&user={COMM.pt_account}&pass={COMM.pt_pass}");
                COMM.pt_token = COMM.GetToJsonList(result)["token"].ToString();
                result = await flurl.API_GET($"https://api.6333600.com/sms/?api=getSummary&token={COMM.pt_token}");
                var money = COMM.GetToJsonList(result)["money"].ToString();
                var num = COMM.GetToJsonList(result)["num"].ToString();
                if (decimal.Parse(num) <= 0)
                {
                    return "余额不足^@^^@^^@^请及时充值";
                }
                return $"登录成功^v^^v^^v^平台余额：{money},最大区号数量:{num}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(bool success, string data)> GetNumber()
        {
            try
            {
                string result = await flurl.API_GET($"https://api.6333600.com/sms/?api=getPhone&token={COMM.pt_token}&sid=22433");
                JObject json = COMM.GetToJsonList(result);
                if (json["code"].ToString() != "0")
                    return (false, json["msg"].ToString());
                return (true, json["phone"].ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool success, string message, string code)> GetCode(string phone)
        {
            try
            {
                string result = await flurl.API_GET($"https://api.6333600.com/sms/?api=getMessage&token={COMM.pt_token}&sid=22433&phone={phone}");
                JObject json = COMM.GetToJsonList(result);
                if (json["code"].ToString() != "0")
                    return (false, json["msg"].ToString(), null);
                return (true, json["sms"].ToString(), json["yzm"].ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
    }
}
