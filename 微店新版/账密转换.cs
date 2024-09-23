// Decompiled with JetBrains decompiler
// Type: 微店新版.账密转换
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Auxiliary.HTTP;
using MyWindowClient.DbHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版
{
    public class 账密转换 : Form
    {
        private DataTable DT = new DataTable();
        public static List<wdauth> authlist = new List<wdauth>();
        private flurl flurl;
        public static List<accounts> ulist = new List<accounts>();
        public static List<idcard> idlist = new List<idcard>();
        private IContainer components = null;
        private Panel panel1;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox4;
        private Label label3;
        private TextBox textBox5;
        private Button button3;
        private Panel panel2;
        private DataGridView dataGridView1;
        private TextBox textBox7;
        private DataGridViewTextBoxColumn 账号;
        private DataGridViewTextBoxColumn 密码;
        private DataGridViewTextBoxColumn 状态;
        private Button button2;
        private Button button1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Button button4;
        private TextBox textBox2;
        private Label label2;
        private Button button5;
        private Button button6;
        private Button button7;

        public 账密转换() => InitializeComponent();

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

        public void TABLE(accounts RW)
        {
            BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
            {
                DataRow row = DT.NewRow();
                row["账号"] = RW.account;
                row["密码"] = RW.password;
                row["状态"] = RW.status;
                DT.Rows.Add(row);
                if (DT == null || DT.Rows.Count <= 0)
                    return;
                UpdateGV(DT);
            }));
        }

        public void UPDATE(accounts user)
        {
            try
            {
                BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["账号"].Value.ToString() == user.account)
                        {
                            row.Cells["密码"].Value = user.password;
                            row.Cells["状态"].Value = user.status;
                            break;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                log("请输入用户名");
            else if (string.IsNullOrWhiteSpace(textBox4.Text))
                log("请输入账号区间值");
            else if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                log("请输入账号区间值");
            }
            else
            {
                if (Convert.ToInt32(textBox5.Text) > Convert.ToInt32(loginrows["count"].ToString()))
                {
                    Convert.ToInt32(loginrows["count"].ToString());
                    textBox5.Text = loginrows["count"].ToString();
                }
                DataTable dataTable = MYSQL.Query("select * from wd_piao_order where original='a67e5870b4b5371dc853bc7bef7429eb'");
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    log("暂无数据");
                }
                else
                {
                    int int32_1 = Convert.ToInt32(textBox4.Text);
                    int int32_2 = Convert.ToInt32(textBox5.Text);
                    authlist = new List<wdauth>();
                    for (int index = int32_1; index < int32_2; ++index)
                    {
                        wdauth wdauth = new wdauth()
                        {
                            id = dataTable.Rows[index]["id"].ToString(),
                            code = "",
                            nickName = dataTable.Rows[index]["nickname"].ToString(),
                            duid = dataTable.Rows[index]["duid"].ToString(),
                            loginToken = dataTable.Rows[index]["loginToken"].ToString(),
                            refreshToken = dataTable.Rows[index]["refreshToken"].ToString(),
                            original = dataTable.Rows[index]["original"].ToString(),
                            sid = dataTable.Rows[index]["sid"].ToString(),
                            uid = dataTable.Rows[index]["uid"].ToString(),
                            phone = dataTable.Rows[index]["phone"].ToString(),
                            flog = false
                        };
                        authlist.Add(wdauth);
                    }
                    log("获取账号" + authlist.Count.ToString());
                }
            }
        }

        private void 账密转换_Load(object sender, EventArgs e)
        {
            IniFiles.inipath = Application.StartupPath + "\\Config.ini";
            COMM.proxy_ip = IniFiles.IniReadValue("代理", "host");
            COMM.proxy_port = int.Parse(IniFiles.IniReadValue("代理", "proxy"));
            COMM.username = IniFiles.IniReadValue("代理", "username");
            COMM.password = IniFiles.IniReadValue("代理", "password");
            MYSQL.Init();
            DT.Columns.Add("账号");
            DT.Columns.Add("密码");
            DT.Columns.Add("状态");
            flurl = new flurl();
            IniFiles.inipath = Application.StartupPath + "\\Config.ini";
            if (!IniFiles.ExistINIFile())
                return;
            textBox1.Text = IniFiles.IniReadValue("用户", "账号");
            textBox2.Text = IniFiles.IniReadValue("用户", "密码");
        }

        public static bool flog { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox2.Text))
                log("请输入接码");
            else if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                log("请选择卡类型");
            }
            else
            {
                jiema = comboBox2.Text;
                getkatype(comboBox1.Text);
                flog = true;
                ulist = new List<accounts>();
                if (authlist.Count < 10)
                {
                    new Thread(new ParameterizedThreadStart(start)).Start(authlist);
                }
                else
                {
                    int num = authlist.Count / 10;
                    foreach (KeyValuePair<string, List<wdauth>> split in COMM.SplitList(authlist, num))
                        new Thread(new ParameterizedThreadStart(start)).Start(split.Value);
                }
            }
        }

        private async void start(object obj)
        {
            // ISSUE: unable to decompile the method.
        }

        public async Task<Model.腾讯滑块> txhk(wdauth info)
        {
            try
            {
                flurl flurl1 = flurl;
                string proxyIp1 = COMM.proxy_ip;
                int proxyPort = COMM.proxy_port;
                string str1 = proxyPort.ToString();
                string url1 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp1 + ":" + str1;
                string txjson = await flurl1.get(url1);
                while (txjson == null || string.IsNullOrWhiteSpace(txjson))
                {
                    flurl flurl2 = flurl;
                    string proxyIp2 = COMM.proxy_ip;
                    proxyPort = COMM.proxy_port;
                    string str2 = proxyPort.ToString();
                    string url2 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp2 + ":" + str2;
                    txjson = await flurl2.get(url2);
                    log(info.nickName + "滑块重试中");
                }
                Model.腾讯滑块 tx = JsonConvert.DeserializeObject<Model.腾讯滑块>(txjson);
                while (tx == null || string.IsNullOrWhiteSpace(tx.ticket) || string.IsNullOrWhiteSpace(tx.randstr))
                {
                    flurl flurl3 = flurl;
                    string proxyIp3 = COMM.proxy_ip;
                    proxyPort = COMM.proxy_port;
                    string str3 = proxyPort.ToString();
                    string url3 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp3 + ":" + str3;
                    txjson = await flurl3.get(url3);
                    while (txjson == null || string.IsNullOrWhiteSpace(txjson))
                    {
                        flurl flurl4 = flurl;
                        string proxyIp4 = COMM.proxy_ip;
                        proxyPort = COMM.proxy_port;
                        string str4 = proxyPort.ToString();
                        string url4 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp4 + ":" + str4;
                        txjson = await flurl4.get(url4);
                        log(info.nickName + "滑块重试中");
                    }
                    tx = JsonConvert.DeserializeObject<Model.腾讯滑块>(txjson);
                    log(info.nickName + "滑块重试中");
                }
                return tx;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Model.腾讯滑块> txhk(accounts info)
        {
            try
            {
                flurl flurl1 = flurl;
                string proxyIp1 = COMM.proxy_ip;
                int proxyPort = COMM.proxy_port;
                string str1 = proxyPort.ToString();
                string url1 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp1 + ":" + str1;
                string txjson = await flurl1.get(url1);
                while (txjson == null || string.IsNullOrWhiteSpace(txjson))
                {
                    flurl flurl2 = flurl;
                    string proxyIp2 = COMM.proxy_ip;
                    proxyPort = COMM.proxy_port;
                    string str2 = proxyPort.ToString();
                    string url2 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp2 + ":" + str2;
                    txjson = await flurl2.get(url2);
                    log(info.account + "滑块重试中");
                }
                Model.腾讯滑块 tx = JsonConvert.DeserializeObject<Model.腾讯滑块>(txjson);
                while (tx == null || string.IsNullOrWhiteSpace(tx.ticket) || string.IsNullOrWhiteSpace(tx.randstr))
                {
                    flurl flurl3 = flurl;
                    string proxyIp3 = COMM.proxy_ip;
                    proxyPort = COMM.proxy_port;
                    string str3 = proxyPort.ToString();
                    string url3 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp3 + ":" + str3;
                    txjson = await flurl3.get(url3);
                    while (txjson == null || string.IsNullOrWhiteSpace(txjson))
                    {
                        flurl flurl4 = flurl;
                        string proxyIp4 = COMM.proxy_ip;
                        proxyPort = COMM.proxy_port;
                        string str4 = proxyPort.ToString();
                        string url4 = "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + proxyIp4 + ":" + str4;
                        txjson = await flurl4.get(url4);
                        log(info.account + "滑块重试中");
                    }
                    tx = JsonConvert.DeserializeObject<Model.腾讯滑块>(txjson);
                    log(info.account + "滑块重试中");
                }
                return tx;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> phoneSuccess(wdauth info, accounts item, string cookies)
        {
            try
            {
                HttpRequestEntity resu = await MYHTTP.Result("POST", "https://sso.weidian.com/user/bindcheck", 1, new Dictionary<string, string>()
        {
          {
            "phone",
            item.account
          },
          {
            "countryCode",
            "86"
          },
          {
            "ua",
            "H4sIAAAAAAAAA+1V2YEkJwxNSUIXCoczig3eT1XTM56NwB+GLhDoPlBT9EVEzIm1fX0Y3ej3YFpt38Iz9efCLEmI9ppfBGrbb1tTp/C1vbnNufHjFRnL8tDfo0cnFaqVX5g7d4iitRTCYnj6wuR2xdoRqG/dArfHFfgIaaTHtJEwsKCQ2eilEBetU/SCWoKSg4Igz0Bh3nR6hpZsEQlwi3hp0KndVLeutlrRp0838BeXAL6PNPEeza9v4G7pf2TQ90cmzu7OoMq/tehxCzVFbKj4cUh1Lbkb+qQkA7KQEGg9Dt3RIOWjoz0rA92FW5fXz4AVFQsVuFxW/o6WvR502KTfPAKOxGeY/PD0x7TS1qGnrCZ1X2H+r7qQjSVRDUkDUeOmzZq3Ay6C/lCyBpHdpp+uPfvoOzlbSnr2XLnz5B00eMiwEWPNmH31lQvgWgsltH3H7nvsdfjEybNuXDDdfS8FMnDDKroVG+qwkStSiPutnVI7/F3hFeuKqjMZss6ehjMkHNCmbxP4dpCI5/Rw5ef0hbPi8Ccj5CfG7nlijnkR4rfeROr1jF91QW9d0ETtXMyo6kM0KyfSztL/6/o/Utf9tRxWwbuyHdXxqwcht2Y/mbafqFFJK/tEPvmuVrreTkncUy+Yrq0Hikg+DzT3QFoKWvug7ABdzjn6A+n1MR9oqJ4vaAxXXj9ycfP0YXsGG7L7mQehR23ipaFHo1IxmU4kniDe3rQ2+PDcZTwe5jjzrClPK28cf0gGOjRxVeJwEjw4p03cGg1EVVsg10I8Lrx82KYB0uoKk7933OeckHPrbcj3jvs2c+FfAzi48Nnpz+sWLG77H6xJknmfBgAA"
          }
        }, cookies);
                if (resu == null || string.IsNullOrWhiteSpace(resu.ResponseContent))
                    return false;
                JObject json = COMM.GetToJsonList(resu.ResponseContent);
                if (json["result"]["registed"].ToString() == "0" || json["result"]["password"].ToString() == "0")
                    return true;
                item.status = "手机已被注册";
                log(item.status);
                getblack(item);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string katype { get; set; }

        public static string jiema { get; set; }

        public void getkatype(string str)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            if (str == "实卡")
            {
                switch (str2)
                {
                    case "米云":
                        katype = "4";
                        break;
                    case "他信":
                        katype = "2";
                        break;
                    case "蓝鸟":
                        katype = "2";
                        break;
                    case "七星码":
                        katype = "2";
                        break;
                    case "EJM":
                        katype = "实卡";
                        break;
                    default:
                        katype = "实卡";
                        break;
                }
            }
            if (!(str == "默认"))
                return;
            switch (str2)
            {
                case "米云":
                    katype = "0";
                    break;
                case "他信":
                    katype = "0";
                    break;
                case "蓝鸟":
                    katype = "0";
                    break;
                case "七星码":
                    katype = "0";
                    break;
                case "EJM":
                    katype = "全部";
                    break;
                default:
                    katype = "全部";
                    break;
            }
        }

        public string getphones(accounts item = null)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            switch (str2)
            {
                case "流星":
                    return 流星(item);
                case "椰子云":
                    return yezi(item);
                case "豪猪":
                    return 豪猪(item);
                default:
                    return TAXIN(item);
            }
        }

        public string getcodes(accounts items)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            switch (str2)
            {
                case "流星":
                    return 流星_CODE(items);
                case "椰子云":
                    return yezi_CODE(items);
                case "豪猪":
                    return 豪猪_CODE(items);
                default:
                    return taxin_CODE(items);
            }
        }

        public void getblack(accounts item)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            switch (str2)
            {
                case "椰子云":
                    RestHttp.GET("http://api.sqhyw.net:90/api/free_mobile?token=" + str1 + "&project_id=" + str3 + "&phone_num=" + item.account);
                    break;
                case "流星":
                    RestHttp.GET("http://api.lxy8.net/sms/api/addBlacklist?token=" + str1 + "&sid=" + str3 + "&phone=" + item.account);
                    break;
                case "豪猪":
                    RestHttp.GET("http://api.haozhuma.com/sms/?api=cancelRecv&token=" + str1 + "&sid=" + str3 + "&phone=" + item.account);
                    break;
                default:
                    RestHttp.GET("http://api.my531.com/Cancel/?token=" + str1 + "&id=" + str3 + "&phone=" + item.account);
                    break;
            }
        }

        public string TAXIN(accounts item = null)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            string empty1 = string.Empty;
            try
            {
                string empty2 = string.Empty;
                string str4;
                if (item == null)
                    str4 = RestHttp.GET("http://api.my531.com/GetPhone/?token=" + str1 + "&id=" + str3);
                else
                    str4 = RestHttp.GET("http://api.my531.com/GetPhone/?token=" + str1 + "&id=" + str3 + "&phone=" + item.account);
                if (!string.IsNullOrWhiteSpace(str4))
                    empty1 = str4.Split('|')[1];
            }
            catch (Exception ex)
            {
            }
            return empty1;
        }

        public string yezi(accounts item = null)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            string empty1 = string.Empty;
            try
            {
                string empty2 = string.Empty;
                string json;
                if (item == null)
                    json = RestHttp.GET("http://api.sqhyw.net:90/api/get_mobile?token=" + str1 + "&project_id=" + str3);
                else
                    json = RestHttp.GET("http://api.sqhyw.net:90/api/get_mobile?token=" + str1 + "&project_id=" + str3 + "&phone_num=" + item.account);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    JObject toJsonList = COMM.GetToJsonList(json);
                    if (toJsonList != null && toJsonList["message"].ToString() == "ok")
                        empty1 = toJsonList["mobile"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return empty1;
        }

        public string 流星(accounts item = null)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            string empty1 = string.Empty;
            try
            {
                string empty2 = string.Empty;
                string json;
                if (item == null)
                    json = RestHttp.GET("http://api.lxy8.net/sms/api/getPhone?token=" + str1 + "&sid=" + str3);
                else
                    json = RestHttp.GET("http://api.lxy8.net/sms/api/getPhone?token=" + str1 + "&sid=" + str3 + "&phone=" + item.account);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    JObject toJsonList = GetToJsonList(json);
                    if (toJsonList["code"].ToString() == "0" || toJsonList["msg"].ToString() == "success")
                        empty1 = toJsonList["phone"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return empty1;
        }

        public string 豪猪(accounts item = null)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            string empty1 = string.Empty;
            try
            {
                string empty2 = string.Empty;
                string json;
                if (item == null)
                    json = RestHttp.GET("http://api.haozhuma.com/sms/?api=getPhone&token=" + str1 + "&sid=" + str3 + "&exclude=166|165|167|170|192&author=lbk003");
                else
                    json = RestHttp.GET("http://api.haozhuma.com/sms/?api=getPhone&token=" + str1 + "&sid=" + str3 + "&phone=" + item.account + "&exclude=166|165|167|170|192&author=lbk003");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    JObject toJsonList = GetToJsonList(json);
                    if (toJsonList["code"].ToString() == "200" || toJsonList["msg"].ToString() == "成功")
                        empty1 = toJsonList["phone"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return empty1;
        }

        public string 流星_CODE(accounts item)
        {
            try
            {
                string str1 = jiema.Split('，')[0];
                string str2 = jiema.Split('，')[1];
                string str3 = jiema.Split('，')[2];
                string str4 = string.Empty;
                try
                {
                    string url = "http://api.lxy8.net/sms/api/getMessage?token=" + str1 + "&sid=" + str3 + "&phone=" + item.account;
                    string json = RestHttp.GET(url) ?? RestHttp.GET(url);
                    int num = 0;
                    while (!json.Contains("微店") && num < 11)
                    {
                        json = RestHttp.GET(url);
                        Thread.Sleep(5000);
                        ++num;
                        item.status = "第" + num.ToString() + "次获取验证码";
                        UPDATE(item);
                    }
                    if (json.Contains("微店"))
                    {
                        item.status = "短信获取成功";
                        UPDATE(item);
                        str4 = GetToJsonList(json)["sms"].ToString().Replace("【微店】", "").Replace("(验证码)微店客服绝不会索取此验证码，请勿将此验证码告知他人", "");
                    }
                    else
                    {
                        item.status = "未能从接码平台获取到短信";
                        UPDATE(item);
                    }
                }
                catch (Exception ex)
                {
                }
                RestHttp.GET("http://api.lxy8.net/sms/api/addBlacklist?token=" + str1 + "&sid=" + str3 + "&phone=" + item.account);
                return str4;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string 豪猪_CODE(accounts item)
        {
            try
            {
                string str1 = jiema.Split('，')[0];
                string str2 = jiema.Split('，')[1];
                string str3 = jiema.Split('，')[2];
                string str4 = string.Empty;
                try
                {
                    string url = "http://api.haozhuma.com/sms/?api=getMessage&token=" + str1 + "&sid=" + str3 + "&phone=" + item.account;
                    string json = RestHttp.GET(url) ?? RestHttp.GET(url);
                    int num = 0;
                    while (!json.Contains("微店") && num < 11)
                    {
                        json = RestHttp.GET(url);
                        Thread.Sleep(5000);
                        ++num;
                        item.status = "第" + num.ToString() + "次获取验证码";
                        UPDATE(item);
                    }
                    if (json.Contains("微店"))
                    {
                        item.status = "短信获取成功";
                        UPDATE(item);
                        str4 = GetToJsonList(json)["sms"].ToString().Replace("【微店】", "").Replace("(验证码)微店客服绝不会索取此验证码，请勿将此验证码告知他人", "");
                    }
                    else
                    {
                        item.status = "未能从接码平台获取到短信";
                        UPDATE(item);
                    }
                }
                catch (Exception ex)
                {
                }
                RestHttp.GET("http://api.haozhuma.com/sms/?api=cancelRecv&token=" + str1 + "&sid=" + str3 + "&phone=" + item.account);
                return str4;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string taxin_CODE(accounts item)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            string str4 = string.Empty;
            try
            {
                string url = "http://api.my531.com/GetMsg/?token=" + str1 + "&id=" + str3 + "&phone=" + item.account;
                string str5 = RestHttp.GET(url) ?? RestHttp.GET(url);
                int num = 0;
                while (!str5.Contains("微店") && num < 11)
                {
                    str5 = RestHttp.GET(url);
                    Thread.Sleep(5000);
                    ++num;
                    item.status = "第" + num.ToString() + "次获取验证码";
                    UPDATE(item);
                }
                RestHttp.GET("http://api.my531.com/Cancel/?token=" + str1 + "&id=" + str3 + "&phone=" + item.account);
                if (str5.Contains("微店"))
                {
                    string[] strArray = str5.Split('|');
                    if (strArray[0] == "1")
                    {
                        str4 = strArray[1].Replace("【微店】", "").Replace("(验证码)微店客服绝不会索取此验证码，请勿将此验证码告知他人", "");
                        item.status = "短信获取成功";
                        UPDATE(item);
                    }
                    else
                    {
                        item.status = "未能从接码平台获取到短信";
                        UPDATE(item);
                    }
                }
                else
                {
                    item.status = "未能从接码平台获取到短信";
                    UPDATE(item);
                }
            }
            catch (Exception ex)
            {
            }
            return str4;
        }

        public string yezi_CODE(accounts item)
        {
            string str1 = jiema.Split('，')[0];
            string str2 = jiema.Split('，')[1];
            string str3 = jiema.Split('，')[2];
            string empty = string.Empty;
            try
            {
                string url = "http://api.sqhyw.net:90/api/get_message?token=" + str1 + "&project_id=" + str3 + "&phone_num=" + item.account;
                string json = RestHttp.GET(url) ?? RestHttp.GET(url);
                int num = 0;
                while (!json.Contains("微店") && num < 11)
                {
                    json = RestHttp.GET(url);
                    Thread.Sleep(5000);
                    ++num;
                    item.status = "第" + num.ToString() + "次获取验证码";
                    UPDATE(item);
                }
                RestHttp.GET("http://api.sqhyw.net:90/api/free_mobile?token=" + str1 + "&project_id=" + str3 + "&phone_num=" + item.account);
                if (json.Contains("微店"))
                {
                    empty = COMM.GetToJsonList(json)["code"].ToString();
                    item.status = "短信获取成功";
                    UPDATE(item);
                }
                else
                {
                    item.status = "未能从接码平台获取到短信";
                    UPDATE(item);
                }
            }
            catch (Exception ex)
            {
            }
            return empty;
        }

        public static JObject GetToJsonList(string json)
        {
            return (JObject)JsonConvert.DeserializeObject(json);
        }

        public static JArray GetToJsonListArry(string json)
        {
            return (JArray)JsonConvert.DeserializeObject(json);
        }

        public static bool flog1 { get; set; }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox2.Text))
                log("请输入接码");
            else if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                log("请选择卡类型");
            }
            else
            {
                jiema = comboBox2.Text;
                getkatype(comboBox1.Text);
                flog1 = true;
                if (ulist.Count < 10)
                {
                    new Thread(new ParameterizedThreadStart(start1)).Start(ulist);
                }
                else
                {
                    int num = ulist.Count / 10;
                    foreach (KeyValuePair<string, List<accounts>> split in COMM.SplitList(ulist, num))
                        new Thread(new ParameterizedThreadStart(start1)).Start(split.Value);
                }
            }
        }

        private async void start1(object obj)
        {
            // ISSUE: unable to decompile the method.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件(.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() != DialogResult.OK || saveFileDialog.FileName.Length <= 0)
                return;
            StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
            try
            {
                foreach (accounts accounts in ulist)
                {
                    if (accounts.flog)
                        streamWriter.WriteLine(accounts.account + "，" + accounts.password);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                streamWriter.Close();
            }
            int num = (int)MessageBox.Show("导出成功");
        }

        public static DataRow loginrows { get; set; }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                log("请输入账号或密码");
            }
            else
            {
                string text1 = textBox1.Text;
                string text2 = textBox2.Text;
                button5.Enabled = false;
                try
                {
                    DataTable dataTable = MYSQL.Query("select * from wd_admin where username='" + text1 + "' and password='" + text2 + "'");
                    if (dataTable == null || dataTable.Rows.Count == 0)
                    {
                        log("账号或密码不正确");
                        button5.Enabled = true;
                    }
                    else
                    {
                        string str = dataTable.Rows[0]["type"].ToString();
                        string time = dataTable.Rows[0]["expire"].ToString();
                        if (str == "N")
                        {
                            log("账户已被禁用");
                            button5.Enabled = true;
                        }
                        else if (dataTable.Rows[0]["role"].ToString() != "Y" && COMM.IsProcessTimeOut(time))
                        {
                            log("账号于" + time + "过期，请续费");
                            button5.Enabled = true;
                        }
                        else
                        {
                            IniFiles.IniWriteValue("用户", "账号", text1);
                            IniFiles.IniWriteValue("用户", "密码", text2);
                            loginrows = dataTable.Rows[0];
                            log("登录成功");
                            textBox5.Text = loginrows["count"].ToString();
                            COMM.TOKENS = loginrows["username"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log(ex.Message);
                    button5.Enabled = true;
                }
            }
        }

        public static bool flog2 { get; set; }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox2.Text))
                log("请输入接码");
            else if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                log("请选择卡类型");
            }
            else
            {
                jiema = comboBox2.Text;
                getkatype(comboBox1.Text);
                flog2 = true;
                if (ulist.Count < 10)
                {
                    new Thread(new ParameterizedThreadStart(start2)).Start(ulist);
                }
                else
                {
                    int num = ulist.Count / 10;
                    foreach (KeyValuePair<string, List<accounts>> split in COMM.SplitList(ulist, num))
                        new Thread(new ParameterizedThreadStart(start2)).Start(split.Value);
                }
            }
        }

        private async void start2(object obj)
        {
            // ISSUE: unable to decompile the method.
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文档|*.txt";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog.FileName.ToString();
            idlist = new List<idcard>();
            foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (!string.IsNullOrEmpty(readAllLine))
                {
                    string[] strArray = readAllLine.Split('-');
                    string str1 = strArray[0].Trim();
                    string str2 = strArray[strArray.Length - 1].Trim();
                    idlist.Add(new idcard()
                    {
                        card = str2,
                        username = str1
                    });
                }
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
            SuspendLayout();
            // 
            // 账密转换
            // 
            ClientSize = new Size(284, 261);
            Name = "账密转换";
            ResumeLayout(false);

        }

        public delegate void SetTextHandler(string text);

        private delegate void UpdateDataGridView(DataTable dt);
    }
}