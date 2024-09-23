// Decompiled with JetBrains decompiler
// Type: 微店新版.注册.账号注册
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using MyWindowClient.DbHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版.注册
{
    public class 账号注册 : UserControl
    {
        private DataTable DT = new DataTable();
        public static List<wdauth> authlist = new List<wdauth>();
        public static List<wdauth> authlist1 = new List<wdauth>();
        private flurl flurl;
        private string username;
        private string[] telStarts = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
        public List<string> ordlist = new List<string>();
        public List<string> list1 = new List<string>();
        public List<string> list2 = new List<string>();
        private IContainer components = null;
        private Panel panel1;
        private Panel panel2;
        private DataGridView dataGridView1;
        private TextBox textBox7;
        private TextBox textBox9;
        private TextBox textBox10;
        private Label label9;
        private Label label10;
        private Button button5;
        private Button button4;
        private Label label8;
        private Button button6;
        private DataGridViewTextBoxColumn 账号;
        private DataGridViewTextBoxColumn 状态;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button7;
        private Button button8;
        private Button button11;
        private Button button10;
        private Button button9;

        public static int f { get; set; }

        public static bool flog { get; set; }

        public 账号注册()
        {
            InitializeComponent();
            DT.Columns.Add(nameof(账号));
            DT.Columns.Add(nameof(状态));
        }

        private void log(string text)
        {
            if (textBox7.InvokeRequired)
                textBox7.Invoke(new SetTextHandler(log), text);
            else
                textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
        }

        private void UpdateGV(DataTable dt)
        {
            if (dataGridView1.InvokeRequired)
            {
                BeginInvoke(new UpdateDataGridView(UpdateGV), dt);
            }
            else
            {
                try
                {
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }

        public void TABLE(wdauth RW)
        {
            BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
            {
                DataRow row = DT.NewRow();
                row["账号"] = RW.phone;
                row["状态"] = RW.msg;
                DT.Rows.Add(row);
                if (DT == null || DT.Rows.Count <= 0)
                    return;
                UpdateGV(DT);
            }));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(COMM.pt_token))
                log("请在设置中登录接码平台");
            else if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                log("请输入用户名称");
            }
            else
            {
                username = textBox1.Text;
                string text = textBox10.Text;
                f = Convert.ToInt32(textBox9.Text);
                if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(f.ToString()))
                {
                    log("线程数量和阈值是必须输入的");
                }
                else
                {
                    flurl = new flurl();
                    flog = true;
                    for (int index = 0; index < Convert.ToInt32(text); ++index)
                        new Thread(new ParameterizedThreadStart(Start)).Start("");
                }
            }
        }

        public string getRandomTel()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            random.Next(10, 1000);
            return telStarts[random.Next(0, telStarts.Length - 1)] + (random.Next(100, 888) + 10000).ToString().Substring(1) + (random.Next(1, 9100) + 10000).ToString().Substring(1);
        }

        public static string DecodeBase64(Encoding encode, string result)
        {
            byte[] bytes = Convert.FromBase64String(result);
            string str;
            try
            {
                str = encode.GetString(bytes);
            }
            catch
            {
                str = result;
            }
            return str;
        }
        /// <summary>
        /// 开始注册
        /// </summary>
        /// <param name="obj"></param>
        private async void Start(object obj)
        {
            Haozhu haozhu = new Haozhu(flurl);
            (bool success, string data) phone = await haozhu.GetNumber();
            if (!phone.success)
            {
                log($"获取号码失败,{phone.data}");
                return;
            }
            Weidian weidian = new Weidian(flurl);
            var sendSmsResult = await weidian.SendSmsCode(phone.data, true);
            if (!sendSmsResult.success)
            {
                log($"发送短信失败,{sendSmsResult.data}");
                return;
            }
            (bool success, string message, string code) = await haozhu.GetCode(phone.data);
            if (!success)
            {
                log($"获取验证码失败,{message}");
                return;
            }
            var checkRes = await weidian.Check(phone.data, code);
            var resCode = COMM.GetToJsonList(checkRes)["status"]["status_code"].ToString();
            if (resCode != "0")
            {
                log($"验证失败,{COMM.GetToJsonList(checkRes)["status"]["status_reason"]}");
                return;
            }
            var regResult = await weidian.Register(phone.data, "password", COMM.GetToJsonList(checkRes)["result"].ToString());
            if (COMM.GetToJsonList(regResult)["status"]["status_code"].ToString() == "0")
            {
                log($"注册成功,{COMM.GetToJsonList(regResult)["result"]["user_id"]}");
                TABLE(new wdauth() { phone = phone.data, msg = "注册成功" });
            }
            else
            {
                log($"注册失败,{COMM.GetToJsonList(regResult)["status"]["status_reason"]}");
                TABLE(new wdauth() { phone = phone.data, msg = COMM.GetToJsonList(regResult)["status"]["status_reason"].ToString() });
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (authlist == null || authlist.Count == 0)
            {
                log("没有需要导出的数据");
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "文本文件(.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                if (saveFileDialog.ShowDialog() != DialogResult.OK || saveFileDialog.FileName.Length <= 0)
                    return;
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
                try
                {
                    foreach (wdauth wdauth in authlist)
                        streamWriter.WriteLine(wdauth.nickName + "，" + wdauth.original + "，" + wdauth.loginToken + "，" + wdauth.refreshToken + "，" + wdauth.phone + "，" + wdauth.duid + "，" + wdauth.sid + "，" + wdauth.uid);
                    log("数据导出成功,文件保存在：" + saveFileDialog.FileName);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    streamWriter.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            authlist = new List<wdauth>();
            flurl = new flurl();
            int int32_1 = Convert.ToInt32(textBox2.Text);
            int int32_2 = Convert.ToInt32(textBox3.Text);
            foreach (DataRow row in (InternalDataCollectionBase)MYSQL.Query(string.Format("SELECT * FROM `wd_user1_copy1` WHERE datetimes NOT LIKE '%2023-07-22%' ORDER BY datetimes asc   LIMIT {0} , {1}", ((int32_1 - 1) * int32_2), int32_2)).Rows)
            {
                wdauth wdauth = new wdauth()
                {
                    id = row["id"].ToString(),
                    nickName = row["nickname"].ToString(),
                    duid = row["duid"].ToString(),
                    loginToken = row["loginToken"].ToString(),
                    refreshToken = row["refreshToken"].ToString(),
                    original = row["original"].ToString(),
                    sid = row["sid"].ToString(),
                    uid = row["uid"].ToString(),
                    phone = row["phone"].ToString()
                };
                authlist.Add(wdauth);
            }
            log(authlist.Count.ToString());
            int num = authlist.Count / 10;
            foreach (KeyValuePair<string, List<wdauth>> split in COMM.SplitList(authlist, num))
                new Thread(new ParameterizedThreadStart(update)).Start(split.Value);
        }

        private async void update(object obj)
        {
            try
            {
                List<wdauth> list = (List<wdauth>)obj;
                foreach (wdauth item in list)
                {
                    try
                    {
                        string og = item.original;
                        string result = await flurl.API_POST("http://61.136.166.233:8668/api/xcxcode", new
                        {
                            orderId = og,
                            appid = "wx4b74228baa15489a",
                            miniAuthType = 2,
                            user = COMM.pt_account,
                            token = COMM.pt_token
                        });
                        JObject json = COMM.GetToJsonList(result);
                        string code = json["code"].ToString();
                        string nickName = json["Userinfo"][0]["nickName"].ToString();
                        string encryptedData = json["Userinfo"][0]["encryptedData"].ToString();
                        string iv = json["Userinfo"][0]["iv"].ToString();
                        string phone = item.phone;
                        Dictionary<string, string> dic = new Dictionary<string, string>
                        {
                            { "param", "{\"encryptedData\":\"" + encryptedData + "\",\"iv\":\"" + iv + "\",\"code\":\"" + code + "\",\"triggerChangeBind\":true,\"appid\":\"wx4b74228baa15489a\"}" },
                            { "context", "{\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\"}" }
                        };
                        string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
                        JObject so = COMM.GetToJsonList(res);
                        string uid = so["result"]["uid"].ToString();
                        string sid = so["result"]["sid"].ToString();
                        string duid = so["result"]["duid"].ToString();
                        string loginToken = so["result"]["loginToken"].ToString();
                        string refreshToken = so["result"]["refreshToken"].ToString();
                        wdauth auth = new wdauth()
                        {
                            code = code,
                            nickName = nickName,
                            duid = duid,
                            loginToken = loginToken,
                            refreshToken = refreshToken,
                            original = og,
                            sid = sid,
                            uid = uid,
                            phone = phone,
                            msg = "注册成功"
                        };
                        authlist1.Add(auth);
                        TABLE(auth);
                        log("授权成功");
                        log("已注册成功数" + authlist1.Count.ToString());
                        MYSQL.exp("update wd_user1_copy1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private async void start1(object obj)
        {
            log("暂未实现");
        }

        /// <summary>
        /// 白云注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            string text = textBox10.Text;
            f = Convert.ToInt32(textBox9.Text);
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(f.ToString()))
            {
                log("线程数量和阈值是必须输入的");
            }
            else
            {
                flurl = new flurl();
                flog = true;
                for (int index = 0; index < Convert.ToInt32(text); ++index)
                    new Thread(new ParameterizedThreadStart(start1)).Start("");
            }
        }
        /// <summary>
        /// 鲨鱼注册-晒图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                log("请输入用户名称");
            }
            else
            {
                username = textBox1.Text;
                string text = textBox10.Text;
                f = Convert.ToInt32(textBox9.Text);
                if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(f.ToString()))
                {
                    log("线程数量和阈值是必须输入的");
                }
                else
                {
                    flurl = new flurl();
                    flog = true;
                    for (int index = 0; index < Convert.ToInt32(text); ++index)
                        new Thread(new ParameterizedThreadStart(start5)).Start("");
                }
            }
        }

        private async void startH5(object obj)
        {
            // ISSUE: unable to decompile the method.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                log("请输入用户名称");
            }
            else
            {
                username = textBox1.Text;
                string text = textBox10.Text;
                f = Convert.ToInt32(textBox9.Text);
                if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(f.ToString()))
                {
                    log("线程数量和阈值是必须输入的");
                }
                else
                {
                    flurl = new flurl();
                    flog = true;
                    for (int index = 0; index < Convert.ToInt32(text); ++index)
                        new Thread(new ParameterizedThreadStart(start2)).Start("");
                }
            }
        }
        /// <summary>
        /// 派大星注册
        /// </summary>
        /// <param name="obj"></param>
        private async void start2(object obj)
        {
            log("派大星注册暂未实现");
        }
        /// <summary>
        /// 鲨鱼注册-刷单
        /// </summary>
        /// <param name="obj"></param>
        private async void start4(object obj)
        {
            log("鲨鱼注册暂未实现");
        }
        /// <summary>
        /// 鲨鱼注册-晒图
        /// </summary>
        /// <param name="obj"></param>
        private async void start5(object obj)
        {
            log("鲨鱼注册暂未实现");
        }

        public async Task<string> GetResult(string orderid = "")
        {
            string vtoken = "wB7K2XYyolX6uKRmS31wWyVA52kOch";
            string wxurl = WebUtility.UrlEncode("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx7c66568ee7a30417&redirect_uri=https%3A%2F%2Fsso.weidian.com%2Fuser%2Foauth%2Fwechat%2Fcallback&response_type=code&scope=snsapi_userinfo&state=STATE_1_1bcf52d337dc127a700105e49d9bc1ae_xiaoxizhushou&connect_redirect=1&uin=MTgxMjg3MDAzOQ%3D%3D&key=8950eb6d25dd63473ec7b53f1d69626c18c24b6a00711ec1688e91dcf7029a606c75ea73c6b21215df2941301b0fefcd8afe9eb1c8657cc99dd1f3ac890b5816218bf098fd36a36a70fcf7075521be15044454d601d5aae1dcee9fcd0fabf87fa5be69d9117dc41341a3de4501ca4e1d57e29193d929fd2579b5a503be358c6c&version=63070517&exportkey=n_ChQIAhIQLtee6WLV%2Fe0vIBL1lGwHChLMAQIE97dBBAEAAAAAAOwZFFboLuYAAAAOpnltbLcz9gKNyK89dVj0OFqmn1PtGYrvd%2BYjVdoQYGWCxDQEzsTu4WWChAwOe2k9V5vAeBqdg59IV6%2FODQF28Y8SWoKhVYF9ONMUIdyyPAwchwdJLAhF2fzTHRuN7nDvpPKbfIhAhWfxoMHO5gLHv2XOr1D8q1dC2JaHdha3xlt4GC%2Bzr%2B46JdsH8lv%2FIs%2FpjmPW8xeM7jCOwecD5mHfH9nG6lshlnoaLJNQdaHs%2BbFK3Ivo0A%3D%3D&pass_ticket=yxvtA2x3DbI%2FnIMAkTi7iksMYRL6Mi8zSDdBhNHRfdFxJnEmQWvoH93oZFplNfsyTA628tjj9eJ9Wge1ntxW0Q%3D%3D&wx_header=0");
            string result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=" + vtoken + "&appId=72444&qrCode=" + wxurl + "&orderId=" + orderid + "&miniAuthType=");
            if (string.IsNullOrWhiteSpace(result))
                return null;
            JObject json = COMM.GetToJsonList(result);
            return json == null || json["code"] == null || !(json["code"].ToString() == "20000") || json["message"] == null || !(json["message"].ToString() == "授权成功") ? null : result;
        }

        public Dictionary<string, string> GetUserD(string js)
        {
            Dictionary<string, string> userD = new Dictionary<string, string>();
            JObject toJsonList = COMM.GetToJsonList(js);
            userD.Add("iv", toJsonList["iv"].ToString());
            userD.Add("encryptedData", toJsonList["encryptedData"].ToString());
            return userD;
        }

        public Dictionary<string, string> GetPhoneD(string js)
        {
            Dictionary<string, string> phoneD = new Dictionary<string, string>();
            JObject toJsonList = COMM.GetToJsonList(js);
            phoneD.Add("iv", toJsonList["custom_phone_list"][0]["iv"].ToString());
            phoneD.Add("encryptedData", toJsonList["custom_phone_list"][0]["encryptedData"].ToString());
            return phoneD;
        }

        public string GetCode(string js) => COMM.GetToJsonList(js)["code"].ToString();
        /// <summary>
        /// 鲨鱼注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in (InternalDataCollectionBase)MYSQL.Query("select * from wd_user1 where remakr='202312'").Rows)
                ordlist.Add(row["original"].ToString());
            flurl = new flurl();
            flog = true;
            for (int parameter = 1; parameter < 2; ++parameter)
                new Thread(new ParameterizedThreadStart(start3)).Start(parameter);
        }
        /// <summary>
        /// 鲨鱼注册
        /// </summary>
        /// <param name="obj"></param>
        private async void start3(object obj)
        {
            string index = obj.ToString();
            string res111 = await flurl.API_GET("http://202.189.11.23:4399/api/record?account=lbk921486&pwd=346563144a&pageNumber=" + index + "&pageSize=200&appId=wx4b74228baa15489a");
            if (string.IsNullOrWhiteSpace(res111))
            {
                index = null;
                res111 = null;
            }
            else
            {
                JObject js11 = COMM.GetToJsonList(res111);
                JToken lists = js11["data"]["list"];
                foreach (JToken jtoken in lists)
                {
                    JToken itemid = jtoken;
                    string id = itemid["id"].ToString();
                    if (!ordlist.Contains(id))
                    {
                        try
                        {
                            string result = await flurl.API_GET("http://202.189.11.23:4399/api/auth?account=lbk921486&pwd=346563144a&appId=wx4b74228baa15489a&id=" + id + "&channel=1&authType=1&url=");
                            JObject json = COMM.GetToJsonList(result);
                            string info = json["data"]["code"].ToString();
                            string original = id;
                            JObject myjson = COMM.GetToJsonList(info);
                            string code = myjson["code"].ToString();
                            if (string.IsNullOrWhiteSpace(code))
                            {
                                result = await flurl.API_GET("http://202.189.11.23:4399/api/auth?account=lbk921486&pwd=346563144a&appId=wx4b74228baa15489a&id=&channel=1&authType=1&url=");
                                json = COMM.GetToJsonList(result);
                                info = json["data"]["code"].ToString();
                                original = json["data"]["orderId"].ToString();
                                myjson = COMM.GetToJsonList(info);
                                code = myjson["code"].ToString();
                            }
                            JObject js = COMM.GetToJsonList(myjson["userData"].ToString());
                            JObject js1 = COMM.GetToJsonList(myjson["phoneData"].ToString());
                            string nickName = COMM.GetToJsonList(js["data"].ToString())["nickName"].ToString().RemoveControlChars();
                            string encryptedData = js["encryptedData"].ToString();
                            string iv = js["iv"].ToString();
                            string phone = getRandomTel();
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("param", "{\"encryptedData\":\"" + encryptedData + "\",\"iv\":\"" + iv + "\",\"code\":\"" + code + "\",\"triggerChangeBind\":true,\"appid\":\"wx4b74228baa15489a\"}");
                            dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"windows\",\"anonymousId\":\"4683ba5867a1aea764534df3721ca92141cc2100\",\"visitor_id\":\"4683ba5867a1aea764534df3721ca92141cc2100\",\"wxappid\":\"wx4b74228baa15489a\"}");
                            string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
                            JObject so = COMM.GetToJsonList(res);
                            string uid = so["result"]["uid"].ToString();
                            string sid = so["result"]["sid"].ToString();
                            string duid = so["result"]["duid"].ToString();
                            string loginToken = so["result"]["loginToken"].ToString();
                            string refreshToken = so["result"]["refreshToken"].ToString();
                            wdauth auth = new wdauth()
                            {
                                code = code,
                                nickName = nickName,
                                duid = duid,
                                loginToken = loginToken,
                                refreshToken = refreshToken,
                                original = original,
                                sid = sid,
                                uid = uid,
                                phone = phone,
                                msg = "注册成功"
                            };
                            authlist.Add(auth);
                            TABLE(auth);
                            log("授权成功");
                            log("已注册成功数" + authlist.Count.ToString());
                            MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,remakr,status,datetimes) values ('" + nickName + "','" + original + "','" + loginToken + "','" + refreshToken + "','" + phone + "','" + duid + "','" + sid + "','" + uid + "','202312','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                            result = null;
                            json = null;
                            info = null;
                            original = null;
                            myjson = null;
                            code = null;
                            js = null;
                            js1 = null;
                            nickName = null;
                            encryptedData = null;
                            iv = null;
                            phone = null;
                            dic = null;
                            res = null;
                            so = null;
                            uid = null;
                            sid = null;
                            duid = null;
                            loginToken = null;
                            refreshToken = null;
                            auth = null;
                        }
                        catch (Exception ex1)
                        {
                            Exception ex = ex1;
                            log("注册异常");
                        }
                        id = null;
                        itemid = null;
                    }
                }
                js11 = null;
                lists = null;
                index = null;
                res111 = null;
            }
        }
        /// <summary>
        /// 鲨鱼注册-刷单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                log("请输入用户名称");
            }
            else
            {
                username = textBox1.Text;
                string text = textBox10.Text;
                f = Convert.ToInt32(textBox9.Text);
                if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(f.ToString()))
                {
                    log("线程数量和阈值是必须输入的");
                }
                else
                {
                    flurl = new flurl();
                    flog = true;
                    for (int index = 0; index < Convert.ToInt32(text); ++index)
                        new Thread(new ParameterizedThreadStart(start4)).Start("");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文档|*.txt";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string readAllLine in File.ReadAllLines(openFileDialog.FileName.ToString(), Encoding.UTF8))
            {
                if (!string.IsNullOrEmpty(readAllLine))
                    list1.Add(readAllLine);
            }
            log("导入成功，导入" + list1.Count.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文档|*.txt";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            foreach (string readAllLine in File.ReadAllLines(openFileDialog.FileName.ToString(), Encoding.UTF8))
            {
                if (!string.IsNullOrEmpty(readAllLine))
                    list2.Add(readAllLine);
            }
            log("导入成功，导入" + list2.Count.ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<string> stringList = new List<string>();
            foreach (string str in list1)
            {
                if (!list2.Contains(str))
                    stringList.Add(str);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件(.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() != DialogResult.OK || saveFileDialog.FileName.Length <= 0)
                return;
            StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
            try
            {
                foreach (string str in stringList)
                    streamWriter.WriteLine(str);
                log("数据导出成功,文件保存在：" + saveFileDialog.FileName);
            }
            catch
            {
                throw;
            }
            finally
            {
                streamWriter.Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panel1 = new Panel();
            button11 = new Button();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label1 = new Label();
            button6 = new Button();
            label8 = new Label();
            button5 = new Button();
            button4 = new Button();
            textBox9 = new TextBox();
            textBox10 = new TextBox();
            label9 = new Label();
            label10 = new Label();
            panel2 = new Panel();
            textBox7 = new TextBox();
            dataGridView1 = new DataGridView();
            账号 = new DataGridViewTextBoxColumn();
            状态 = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            panel1.Controls.Add(button11);
            panel1.Controls.Add(button10);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(textBox9);
            panel1.Controls.Add(textBox10);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label10);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 89);
            panel1.TabIndex = 0;
            button11.Location = new Point(754, 63);
            button11.Name = "button11";
            button11.Size = new Size(122, 23);
            button11.TabIndex = 26;
            button11.Text = "排除";
            button11.UseVisualStyleBackColor = true;
            button11.Click += new EventHandler(button11_Click);
            button10.Location = new Point(754, 39);
            button10.Name = "button10";
            button10.Size = new Size(122, 23);
            button10.TabIndex = 25;
            button10.Text = "2";
            button10.UseVisualStyleBackColor = true;
            button10.Click += new EventHandler(button10_Click);
            button9.Location = new Point(624, 39);
            button9.Name = "button9";
            button9.Size = new Size(122, 23);
            button9.TabIndex = 24;
            button9.Text = "1";
            button9.UseVisualStyleBackColor = true;
            button9.Click += new EventHandler(button9_Click);
            button8.Location = new Point(624, 63);
            button8.Name = "button8";
            button8.Size = new Size(122, 23);
            button8.TabIndex = 23;
            button8.Text = "鲨鱼注册-刷单";
            button8.UseVisualStyleBackColor = true;
            button8.Click += new EventHandler(button8_Click);
            button7.Location = new Point(108, 60);
            button7.Name = "button7";
            button7.Size = new Size(122, 23);
            button7.TabIndex = 22;
            button7.Text = "派大星注册";
            button7.UseVisualStyleBackColor = true;
            button7.Click += new EventHandler(button7_Click);
            button3.Location = new Point(236, 63);
            button3.Name = "button3";
            button3.Size = new Size(122, 23);
            button3.TabIndex = 21;
            button3.Text = "派大星注册";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            button2.Location = new Point(496, 63);
            button2.Name = "button2";
            button2.Size = new Size(122, 23);
            button2.TabIndex = 20;
            button2.Text = "鲨鱼注册-晒图";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click_1);
            button1.Location = new Point(366, 63);
            button1.Name = "button1";
            button1.Size = new Size(122, 23);
            button1.TabIndex = 19;
            button1.Text = "白云注册";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            textBox3.Location = new Point(496, 39);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(122, 21);
            textBox3.TabIndex = 18;
            textBox2.Location = new Point(366, 39);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(122, 21);
            textBox2.TabIndex = 17;
            textBox1.Location = new Point(236, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(122, 21);
            textBox1.TabIndex = 16;
            textBox1.Text = "0218";
            label1.AutoSize = true;
            label1.Location = new Point(199, 45);
            label1.Name = "label1";
            label1.Size = new Size(35, 12);
            label1.TabIndex = 15;
            label1.Text = " 用户";
            button6.Location = new Point(624, 10);
            button6.Name = "button6";
            button6.Size = new Size(122, 23);
            button6.TabIndex = 14;
            button6.Text = "账号导出";
            button6.UseVisualStyleBackColor = true;
            button6.Click += new EventHandler(button6_Click);
            label8.AutoSize = true;
            label8.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
            label8.ForeColor = Color.Red;
            label8.Location = new Point(752, 15);
            label8.Name = "label8";
            label8.Size = new Size(77, 12);
            label8.TabIndex = 13;
            label8.Text = "注册成功：0";
            button5.Location = new Point(496, 10);
            button5.Name = "button5";
            button5.Size = new Size(122, 23);
            button5.TabIndex = 12;
            button5.Text = "停止注册";
            button5.UseVisualStyleBackColor = true;
            button5.Click += new EventHandler(button5_Click);
            button4.Location = new Point(366, 10);
            button4.Name = "button4";
            button4.Size = new Size(122, 23);
            button4.TabIndex = 11;
            button4.Text = "开始注册";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            textBox9.Location = new Point(236, 11);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(122, 21);
            textBox9.TabIndex = 10;
            textBox9.Text = "999";
            textBox10.Location = new Point(69, 11);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(122, 21);
            textBox10.TabIndex = 9;
            textBox10.Text = "10";
            label9.AutoSize = true;
            label9.Location = new Point(199, 15);
            label9.Name = "label9";
            label9.Size = new Size(29, 12);
            label9.TabIndex = 8;
            label9.Text = "阈值";
            label10.AutoSize = true;
            label10.Location = new Point(32, 15);
            label10.Name = "label10";
            label10.Size = new Size(29, 12);
            label10.TabIndex = 7;
            label10.Text = "线程";
            panel2.Controls.Add(textBox7);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 89);
            panel2.Name = "panel2";
            panel2.Size = new Size(914, 100);
            panel2.TabIndex = 1;
            textBox7.Dock = DockStyle.Fill;
            textBox7.Font = new Font("宋体", 9f);
            textBox7.Location = new Point(0, 0);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.ScrollBars = ScrollBars.Vertical;
            textBox7.Size = new Size(914, 100);
            textBox7.TabIndex = 6;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(账号, 状态);
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 189);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 27;
            dataGridView1.Size = new Size(914, 349);
            dataGridView1.TabIndex = 4;
            账号.DataPropertyName = "账号";
            账号.HeaderText = "账号";
            账号.Name = "账号";
            账号.Width = 150;
            状态.DataPropertyName = "状态";
            状态.HeaderText = "状态";
            状态.Name = "状态";
            状态.Width = 400;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "账号注册";
            Size = new Size(914, 538);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        public delegate void SetTextHandler(string text);

        private delegate void UpdateDataGridView(DataTable dt);
    }
}
