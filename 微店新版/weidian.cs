// Decompiled with JetBrains decompiler
// Type: 微店新版.weidian
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Auxiliary.HTTP;
using MyWindowClient.DbHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版
{
    public class weidian : Form
    {
        private bool ischeckbox = false;
        private flurl flurl;
        private DataTable DT = new DataTable();
        public static List<string> phlist = new List<string>();
        public static List<wdauth> authlist = new List<wdauth>();
        public static List<微店新版.piao> piao = new List<微店新版.piao>();
        private string[] telStarts = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
        public static List<string> addreslist = new List<string>();
        public static ConcurrentQueue<card> cardlist = new ConcurrentQueue<card>();
        public static List<string> listcard = new List<string>();
        public static List<List<string>> cdlist = new List<List<string>>();
        private IContainer components = null;
        private Panel panel1;
        private Button button2;
        private Panel panel2;
        private TextBox textBox7;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private Button button3;
        private Button button1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private Button button6;
        private TextBox textBox2;
        private Button button4;
        private TextBox textBox3;
        private Button button5;
        private Button button7;
        private TextBox textBox5;
        private TextBox textBox4;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 免登录打开ToolStripMenuItem;
        private Button button8;
        private DataGridViewTextBoxColumn nickName;
        private DataGridViewTextBoxColumn original;
        private DataGridViewTextBoxColumn loginToken;
        private DataGridViewTextBoxColumn refreshToken;
        private DataGridViewTextBoxColumn phone;
        private DataGridViewTextBoxColumn duid;
        private DataGridViewTextBoxColumn sid;
        private DataGridViewTextBoxColumn uid;
        private DataGridViewTextBoxColumn addresid;
        private DataGridViewTextBoxColumn msg;
        private DataGridViewTextBoxColumn url;
        private DataGridViewTextBoxColumn url1;
        private Button button10;
        private Button button9;
        private Button button11;
        private Button button12;
        private Button button13;
        private Button button14;

        public weidian() => InitializeComponent();

        private void log(string text)
        {
            if (textBox7.InvokeRequired)
                textBox7.Invoke(new weidian.SetTextHandler(log), text);
            else
                textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
        }

        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            dataGridView1.EndEdit();
            dataGridView1.Rows.OfType<DataGridViewRow>().ToList<DataGridViewRow>().ForEach(t => t.Cells[0].Value = state);
        }

        private void UpdateGV(DataTable dt)
        {
            if (dataGridView1.InvokeRequired)
            {
                BeginInvoke(new weidian.UpdateDataGridView(UpdateGV), dt);
            }
            else
            {
                try
                {
                    dataGridView1.DataSource = dt;
                    if (!ischeckbox)
                    {
                        DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
                        DatagridViewCheckBoxHeaderCell checkBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
                        viewCheckBoxColumn.HeaderCell = checkBoxHeaderCell;
                        viewCheckBoxColumn.HeaderText = "";
                        checkBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
                        dataGridView1.Columns.Insert(0, viewCheckBoxColumn);
                    }
                    dataGridView1.Refresh();
                    ischeckbox = true;
                }
                catch (Exception ex)
                {
                    dataGridView1.DataSource = dt;
                    if (!ischeckbox)
                    {
                        DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
                        DatagridViewCheckBoxHeaderCell checkBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
                        viewCheckBoxColumn.HeaderCell = checkBoxHeaderCell;
                        viewCheckBoxColumn.HeaderText = "";
                        checkBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
                        dataGridView1.Columns.Insert(0, viewCheckBoxColumn);
                    }
                    dataGridView1.Refresh();
                    ischeckbox = true;
                }
            }
        }

        public void TABLE(wdauth RW)
        {
            BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
            {
                DataRow row = DT.NewRow();
                row["nickName"] = RW.nickName;
                row["original"] = RW.original;
                row["loginToken"] = RW.loginToken;
                row["refreshToken"] = RW.refreshToken;
                row["phone"] = RW.phone;
                row["duid"] = RW.duid;
                row["sid"] = RW.sid;
                row["uid"] = RW.uid;
                row["msg"] = RW.msg;
                DT.Rows.Add(row);
                if (DT == null || DT.Rows.Count <= 0)
                    return;
                UpdateGV(DT);
            }));
        }

        public void UPDATE(wdauth user)
        {
            try
            {
                BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["loginToken"].Value.ToString() == user.loginToken)
                        {
                            row.Cells["addresid"].Value = user.addres;
                            row.Cells["msg"].Value = user.msg;
                            row.Cells["url"].Value = user.url;
                            row.Cells["url1"].Value = user.url1;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
            }
        }

        private void weidian_Load(object sender, EventArgs e)
        {
            IniFiles.inipath = Application.StartupPath + "\\Config.ini";
            if (IniFiles.ExistINIFile())
            {
                COMM.proxy_ip = IniFiles.IniReadValue("代理", "host");
                COMM.proxy_port = int.Parse(IniFiles.IniReadValue("代理", "proxy"));
                COMM.username = IniFiles.IniReadValue("代理", "username");
                COMM.password = IniFiles.IniReadValue("代理", "password");
                flurl = new flurl();
                DT.Columns.Add("nickName");
                DT.Columns.Add("original");
                DT.Columns.Add("loginToken");
                DT.Columns.Add("refreshToken");
                DT.Columns.Add("phone");
                DT.Columns.Add("duid");
                DT.Columns.Add("sid");
                DT.Columns.Add("uid");
                DT.Columns.Add("addresid");
                DT.Columns.Add("msg");
                DT.Columns.Add("url");
                DT.Columns.Add("url1");
                MYSQL.Init();
            }
            phlist.Add("18796782005");
            phlist.Add("17768105161");
            phlist.Add("19825970577");
        }

        public static string token { get; set; }

        public static int max { get; set; }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    string result = await flurl.API_POST("http://61.136.166.233:8668/api/login", new
                    {
                        user = nameof(weidian),
                        pwd = nameof(weidian)
                    });
                    token = COMM.GetToJsonList(result)["token"].ToString();
                    result = null;
                }
                string _max = textBox1.Text;
                string str = textBox2.Text;
                if (string.IsNullOrWhiteSpace(_max))
                    log("请输入需要授权的个数");
                else if (string.IsNullOrWhiteSpace(str))
                {
                    log("请输入要抢的票数据");
                }
                else
                {
                    max = Convert.ToInt32(_max);
                    for (int i = 0; i < 1; ++i)
                    {
                        Thread thread_ = new Thread(new ParameterizedThreadStart(start));
                        thread_.Start("");
                        thread_ = null;
                    }
                    _max = null;
                    str = null;
                }
            }
            catch (Exception ex)
            {
                log("按钮点击事件错误" + ex.StackTrace);
            }
        }

        public string getRandomTel()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            random.Next(10, 1000);
            return telStarts[random.Next(0, telStarts.Length - 1)] + (random.Next(100, 888) + 10000).ToString().Substring(1) + (random.Next(1, 9100) + 10000).ToString().Substring(1);
        }

        public static string ConvertDateTimeToString()
        {
            return ((DateTime.Now.Ticks - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).Ticks) / 10000L).ToString();
        }

        private async void start(object obj)
        {
            while (authlist.Count < max)
            {
                try
                {
                    string result = await flurl.API_POST("http://61.136.166.233:8668/api/xcxcode", new
                    {
                        orderId = "a5d1c277f46b5501ab016371076fc36c",
                        appid = "wx4b74228baa15489a",
                        miniAuthType = 2,
                        user = nameof(weidian),
                        token = token
                    });
                    JObject json = COMM.GetToJsonList(result);
                    string code = json["code"].ToString();
                    string original = json["original"].ToString();
                    string nickName = json["Userinfo"][0]["nickName"].ToString();
                    string encryptedData = json["Userinfo"][0]["encryptedData"].ToString();
                    string iv = json["Userinfo"][0]["iv"].ToString();
                    string phone = getRandomTel();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("param", "{\"encryptedData\":\"" + encryptedData + "\",\"iv\":\"" + iv + "\",\"code\":\"" + code + "\",\"triggerChangeBind\":true,\"appid\":\"wx4b74228baa15489a\"}");
                    dic.Add("context", "{\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\"}");
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
                    MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,status,datetimes) values ('" + nickName + "','" + original + "','" + loginToken + "','" + refreshToken + "','" + phone + "','" + duid + "','" + sid + "','" + uid + "','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    result = null;
                    json = null;
                    code = null;
                    original = null;
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
                catch (Exception ex)
                {
                    log("授权过程中出现异常错误");
                }
            }
        }

        public async void WHXH()
        {
            try
            {
                string state = string.Empty;
                string dd = await flurl.GETXH(" https://api.asclepius.whxh.com.cn/houseKeeperAuth/authentication/officialWechat/oauth2WechatMp?callBackUrl=https%3A%2F%2Fali.whxh.com.cn%2Fviews%2Fp187%2Findex.html%23%2Fnavigation%2Findex%3F");
                string[] sp = dd.Split('&');
                if (sp.Length != 0)
                {
                    string[] strArray = sp;
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        string item = strArray[index];
                        if (item.StartsWith("state"))
                        {
                            state = item.Split('#')[0].Replace("state=", "");
                            break;
                        }
                        item = null;
                    }
                    strArray = null;
                }
                string result = await flurl.API_POST("http://61.136.166.233:8668/api/login", new
                {
                    user = nameof(weidian),
                    pwd = nameof(weidian)
                });
                string TOKEN = COMM.GetToJsonList(result)["token"].ToString();
                string appid = "wx398f5367219cc454";
                string authUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx398f5367219cc454&scope=snsapi_userinfo&response_type=code&redirect_uri=https%3A%2F%2Fali.whxh.com.cn%2Fasclepius-proxy%2FhouseKeeperAuth%2Fauthentication%2FofficialWechat%2Foauth2WechatMp&state=a61b857c5b6d4a789021c609ed6525af#wechat_redirect";
                string res = await flurl.API_POST("http://61.136.166.233:8668/api/webcode", new
                {
                    appid = appid,
                    type = 1,
                    user = nameof(weidian),
                    token = TOKEN,
                    url = authUrl
                });
                JObject json = COMM.GetToJsonList(res);
                string original = json["original"].ToString();
                string url = json["code"].ToString();
                string code = url.Split('?')[1].Split('&')[0].Split('=')[1];
                string auth = "https://ali.whxh.com.cn/asclepius-proxy/houseKeeperAuth/authentication/officialWechat/oauth2WechatMp?code=" + code + "&state=" + state;
                string cookie = await flurl.GETXHAUTH(auth);
                log(cookie);
                code = json["code"].ToString();
                string encryptedData = json["Userinfo"][0]["encryptedData"].ToString();
                string iv = json["Userinfo"][0]["iv"].ToString();
                state = null;
                dd = null;
                sp = null;
                result = null;
                TOKEN = null;
                appid = null;
                authUrl = null;
                res = null;
                json = null;
                original = null;
                url = null;
                code = null;
                auth = null;
                cookie = null;
                encryptedData = null;
                iv = null;
            }
            catch (Exception ex)
            {
            }
        }

        public async void WHXH(object obj)
        {
            string orderid = "41bf7aff9403e9421098bf31d3b24531";
            string result = await flurl.API_POST("http://61.136.166.233:8668/api/login", new
            {
                user = "test123",
                pwd = "798798"
            });
            string TOKEN = COMM.GetToJsonList(result)["token"].ToString();
            for (int i = 0; i < 100; ++i)
            {
                try
                {
                    result = await flurl.API_POST("http://61.136.166.233:8668/api/webcode", new
                    {
                        orderId = orderid,
                        appid = "wx14716a90a1175907",
                        type = 1,
                        user = "test123",
                        token = TOKEN,
                        url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx14716a90a1175907&redirect_uri=http%3A%2F%2Fs.appykt.com%2Fzld%2Fuc%2Fjump%3Ftarget%3Duserpay%26gid%3D0&response_type=code&scope=snsapi_base&state=123#wechat_redirect"
                    });
                    log(result);
                }
                catch (Exception ex)
                {
                }
            }
            orderid = null;
            result = null;
            TOKEN = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (authlist == null || authlist.Count == 0)
            {
                log("请获取账号");
            }
            else
            {
                foreach (wdauth parameter in authlist)
                {
                    if (string.IsNullOrWhiteSpace(parameter.addres))
                        new Thread(new ParameterizedThreadStart(addres)).Start(parameter);
                }
            }
        }

        public async Task<string> shibie(wdauth item)
        {
            try
            {
                string addresname = addreslist[new Random(Guid.NewGuid().GetHashCode()).Next(addreslist.Count)];
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("param", "{\"address\":\"" + addresname + "\"}");
                dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"ios\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"token\":\"" + item.loginToken + "\",\"duid\":\"" + item.duid + "\",\"uss\":\"" + item.loginToken + "\",\"userID\":\"" + item.duid + "\",\"wduserID\":\"" + item.sid + "\",\"wxappid\":\"wx4b74228baa15489a\"}");
                string res = await flurl.POST("https://thor.weidian.com/address/analysisAddress/1.0", dic);
                if (string.IsNullOrWhiteSpace(res))
                    res = await flurl.POST("https://thor.weidian.com/address/analysisAddress/1.0", dic);
                return !string.IsNullOrWhiteSpace(res) ? res : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async void addres(object obj)
        {
            wdauth item = (wdauth)obj;
            try
            {
                string str = await shibie(item);
                if (string.IsNullOrWhiteSpace(str))
                    str = await shibie(item);
                if (string.IsNullOrWhiteSpace(str))
                {
                    log("地址识别失败");
                    item.msg = "地址识别失败";
                    UPDATE(item);
                    item = null;
                }
                else
                {
                    JObject json = COMM.GetToJsonList(str);
                    if (json["status"]["code"].ToString() == "0" && json["status"]["message"].ToString() == "OK" && json["result"] != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        Dictionary<string, string> dictionary = dic;
                        string[] strArray = new string[15];
                        strArray[0] = "{\"buyerName\":\"";
                        strArray[1] = json["result"]["name"].ToString();
                        strArray[2] = "\",\"buyerPhone\":\"";
                        strArray[3] = json["result"]["phone"].ToString();
                        strArray[4] = "\",\"isDefault\":1,\"province\":";
                        int int32 = Convert.ToInt32(json["result"]["province"].ToString());
                        strArray[5] = int32.ToString();
                        strArray[6] = ",\"city\":";
                        int32 = Convert.ToInt32(json["result"]["city"].ToString());
                        strArray[7] = int32.ToString();
                        strArray[8] = ",\"area\":";
                        int32 = Convert.ToInt32(json["result"]["area"].ToString());
                        strArray[9] = int32.ToString();
                        strArray[10] = ",\"town\":";
                        int32 = Convert.ToInt32(json["result"]["town"].ToString());
                        strArray[11] = int32.ToString();
                        strArray[12] = ",\"street\":\"";
                        strArray[13] = json["result"]["detailAddress"].ToString();
                        strArray[14] = "\",\"longitude\":\"\",\"latitude\":\"\",\"countryCode\":\"86\",\"bizType\":0,\"houseNum\":\"\"}";
                        string str1 = string.Concat(strArray);
                        dictionary.Add("param", str1);
                        dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"ios\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"token\":\"" + item.loginToken + "\",\"duid\":\"" + item.duid + "\",\"uss\":\"" + item.loginToken + "\",\"userID\":\"" + item.duid + "\",\"wduserID\":\"" + item.sid + "\",\"wxappid\":\"wx4b74228baa15489a\"}");
                        string res = await flurl.POST("https://thor.weidian.com/address/buyerAddAddress/1.0", dic);
                        if (string.IsNullOrWhiteSpace(res))
                            res = await flurl.POST("https://thor.weidian.com/address/buyerAddAddress/1.0", dic);
                        log(res);
                        if (string.IsNullOrWhiteSpace(res))
                        {
                            item = null;
                            return;
                        }
                        string addresid = COMM.GetToJsonList(res)["result"]["id"].ToString();
                        item.addres = addresid;
                        UPDATE(item);
                        dic = null;
                        res = null;
                        addresid = null;
                    }
                    else
                        log(str);
                    str = null;
                    json = null;
                    item = null;
                }
            }
            catch (Exception ex)
            {
                item = null;
            }
        }

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

        public static bool flog { get; set; }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (authlist == null || authlist.Count == 0)
                log("请获取账号");
            else if (piao == null || piao.Count == 0)
            {
                log("票数据不正确");
            }
            else
            {
                string dateStr1 = Convert.ToDateTime(textBox3.Text).ToString("yyyy-MM-dd HH:mm:ss.fff");
                log("提交在：" + dateStr1 + "，时启动");
                string msg = "";
                while (true)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    if (!msg.Contains("等于") && !msg.Contains("小于"))
                        CompanyDate(dateStr1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), ref msg);
                    else
                        break;
                }
                flog = true;
                foreach (wdauth parameter in authlist)
                {
                    if (!string.IsNullOrWhiteSpace(parameter.addres))
                        new Thread(new ParameterizedThreadStart(order)).Start(parameter);
                }
            }
        }

        private async void order(object obj)
        {
            // ISSUE: unable to decompile the method.
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文档|*.txt";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog.FileName.ToString();
            addreslist = new List<string>();
            foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (!string.IsNullOrEmpty(readAllLine))
                    addreslist.Add(readAllLine);
            }
            log("导入地址成功，导入" + addreslist.Count.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文档|*.txt";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog.FileName.ToString();
            cardlist = new ConcurrentQueue<card>();
            foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (!string.IsNullOrEmpty(readAllLine))
                {
                    string str1 = readAllLine.Split('-')[0];
                    string str2 = readAllLine.Split('-')[readAllLine.Split('-').Length - 1];
                    cardlist.Enqueue(new card()
                    {
                        name = str1,
                        cardno = str2
                    });
                }
            }
            log("导入身份信息成功，导入" + cardlist.Count.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flog = false;
            log("已停止");
            GetToken();
        }

        public async void GetToken()
        {
            try
            {
                string result = await flurl.API_POST("http://111.173.119.135:8088/login", new
                {
                    name = "wd1113",
                    pwd = "wd1113"
                });
                string integral = COMM.GetToJsonList(result)["user_info"]["balance"].ToString();
                if (Convert.ToDouble(integral) <= 0.0)
                {
                    log("余额不足");
                }
                else
                {
                    COMM.pt_token = COMM.GetToJsonList(result)["token"].ToString();
                    log("登录成功");
                    button1.Enabled = false;
                }
                result = null;
                integral = null;
            }
            catch (Exception ex1)
            {
                Exception ex = ex1;
                log("登录平台发生异常");
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                string result = await flurl.API_POST("http://61.136.166.233:8668/api/login", new
                {
                    user = nameof(weidian),
                    pwd = nameof(weidian)
                });
                token = COMM.GetToJsonList(result)["token"].ToString();
                result = null;
            }
            string str = textBox2.Text;
            string sql;
            DataTable dt;
            if (string.IsNullOrWhiteSpace(str))
            {
                log("请输入要抢的票数据");
                str = null;
                sql = null;
                dt = null;
            }
            else if (cardlist == null || cardlist.Count == 0)
            {
                log("请导入身份信息");
                str = null;
                sql = null;
                dt = null;
            }
            else if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                log("请输入页码");
                str = null;
                sql = null;
                dt = null;
            }
            else if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                log("请输入条数");
                str = null;
                sql = null;
                dt = null;
            }
            else
            {
                int Page = Convert.ToInt32(textBox4.Text);
                int PageSize = Convert.ToInt32(textBox5.Text);
                if (cardlist.Count <= PageSize * 2)
                {
                    log("身份信息应该是账号数量的2倍，请重新导入");
                    str = null;
                    sql = null;
                    dt = null;
                }
                else
                {
                    piao = new List<微店新版.piao>();
                    authlist = new List<wdauth>();
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        string[] sp = str.Split('，');
                        string[] strArray = sp;
                        for (int index = 0; index < strArray.Length; ++index)
                        {
                            string item = strArray[index];
                            string id = item.Split('-')[0];
                            double price = Convert.ToDouble(item.Split('-')[1]);
                            piao.Add(new 微店新版.piao()
                            {
                                id = id,
                                price = price
                            });
                            id = null;
                            item = null;
                        }
                        strArray = null;
                        sp = null;
                    }
                    sql = string.Format("SELECT * FROM `wd_user1` LIMIT {0} , {1}", (Page - 1) * PageSize, PageSize);
                    dt = MYSQL.Query(sql);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        log("暂无数据");
                        str = null;
                        sql = null;
                        dt = null;
                    }
                    else
                    {
                        foreach (DataRow item in (InternalDataCollectionBase)dt.Rows)
                        {
                            微店新版.piao _piao = piao[new Random(Guid.NewGuid().GetHashCode()).Next(piao.Count)];
                            int index = new Random((int)DateTime.Now.Ticks).Next(1, 3);
                            card card1 = null;
                            card card2 = null;
                            card card3 = null;
                            card card4 = null;
                            string names = string.Empty;
                            if (index == 1)
                            {
                                cardlist.TryDequeue(out card1);
                                names = card1.name;
                            }
                            if (index == 2)
                            {
                                cardlist.TryDequeue(out card1);
                                cardlist.TryDequeue(out card2);
                                names = card1.name + "," + card2.name;
                            }
                            if (index == 3)
                            {
                                cardlist.TryDequeue(out card1);
                                cardlist.TryDequeue(out card2);
                                cardlist.TryDequeue(out card3);
                                names = card1.name + "," + card2.name + "," + card3.name;
                            }
                            if (index == 4)
                            {
                                cardlist.TryDequeue(out card1);
                                cardlist.TryDequeue(out card2);
                                cardlist.TryDequeue(out card3);
                                cardlist.TryDequeue(out card4);
                                names = card1.name + "," + card2.name + "," + card3.name + "," + card4.name;
                            }
                            wdauth auth = new wdauth()
                            {
                                code = "",
                                nickName = item["nickname"].ToString(),
                                duid = item["duid"].ToString(),
                                loginToken = item["loginToken"].ToString(),
                                refreshToken = item["refreshToken"].ToString(),
                                original = item["original"].ToString(),
                                sid = item["sid"].ToString(),
                                uid = item["uid"].ToString(),
                                phone = item["phone"].ToString(),
                                id = _piao.id,
                                price = _piao.price,
                                count = index,
                                card1 = card1,
                                card2 = card2,
                                card3 = card3,
                                card4 = card4,
                                msg = _piao.price.ToString() + "," + names + "，" + index.ToString()
                            };
                            authlist.Add(auth);
                            TABLE(auth);
                            log("授权成功");
                            _piao = null;
                            card1 = null;
                            card2 = null;
                            card3 = null;
                            card4 = null;
                            names = null;
                            auth = null;
                        }
                        str = null;
                        sql = null;
                        dt = null;
                    }
                }
            }
        }

        private void 免登录打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1 == null || dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
                    return;
                int index = dataGridView1.CurrentRow.Index;
                object obj1 = dataGridView1.Rows[index].Cells["loginToken"].Value;
                object obj2 = dataGridView1.Rows[index].Cells["duid"].Value;
                object obj3 = dataGridView1.Rows[index].Cells["sid"].Value;
                object obj4 = dataGridView1.Rows[index].Cells["uid"].Value;
                string cookie = "wdtoken=" + Guid.NewGuid().ToString().ToLower().Substring(8) + ";__spider__visitorid=" + COMM.r(16) + ";__spider__sessionid=" + COMM.r(16) + ";v-components/cpn-coupon-dialog@iwdDefault=1;login_token=" + obj1?.ToString() + ";uid=" + obj4?.ToString() + ";is_login=true;login_type=LOGIN_USER_TYPE_WECHAT;login_source=LOGIN_USER_SOURCE_WECHAT;duid=" + obj2?.ToString() + ";sid=" + obj3?.ToString();
                if (cookie == null || string.IsNullOrWhiteSpace(cookie.ToString()))
                    log("当前选中账号没有COOKIE");
                else
                    Task.Run(() =>
                    {
                        ChromeDriverService defaultService = ChromeDriverService.CreateDefaultService();
                        defaultService.HideCommandPromptWindow = false;
                        ChromeOptions options = new ChromeOptions();
                        options.AddExcludedArgument("enable-automation");
                        options.AddArgument("--mute-audio");
                        options.AddArgument("--auto-open-devtools-for-tabs");
                        options.AddAdditionalCapability("useAutomationExtension", false);
                        options.AddArguments("lang=zh_CN.UTF-8");
                        options.AddArgument("--disable-gpu");
                        ChromeDriver chromeDriver = new ChromeDriver(defaultService, options);
                        try
                        {
                            chromeDriver.Navigate().GoToUrl("https://sso.weidian.com/login/index.php");
                            chromeDriver.Manage().Window.Maximize();
                            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000.0);
                            string[] strArray = cookie.ToString().Split(';');
                            ICookieJar cookies = chromeDriver.Manage().Cookies;
                            foreach (string cookie1 in strArray)
                            {
                                try
                                {
                                    cookies.AddCookie(COMM.ToCookie(cookie1));
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            chromeDriver.Navigate().GoToUrl("https://weidian.com/user/order-new/list.php?type=0&spider_token=225b");
                            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000.0);
                            log(phone?.ToString() + "免登录打开成功");
                        }
                        catch (Exception ex)
                        {
                        }
                    });
            }
            catch (Exception ex)
            {
                log("免登录打开错误");
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0)
                return;
            if (!dataGridView1.Rows[e.RowIndex].Selected)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            if (dataGridView1.SelectedRows.Count == 1)
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataTable dataSource = (DataTable)dataGridView1.DataSource;
            if (dataSource == null || dataSource.Rows.Count == 0)
            {
                log("无账号数据");
            }
            else
            {
                List<wdauth> wdauthList = new List<wdauth>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewRow item = row;
                    if (item.Cells[0].Value != null && (bool)item.Cells[0].Value)
                    {
                        List<wdauth> list = authlist.Where<wdauth>(p => p.original == item.Cells["original"].Value.ToString()).ToList<wdauth>();
                        if (list == null || list.Count == 0)
                            log(item.Cells["original"].Value.ToString() + "数据已被外部修改，检索不到对于值");
                        else
                            wdauthList.Add(list.First<wdauth>());
                    }
                }
                if (wdauthList == null || wdauthList.Count == 0)
                {
                    log("请勾选需要下单的账号");
                }
                else
                {
                    foreach (object parameter in wdauthList)
                        new Thread(new ParameterizedThreadStart(start1)).Start(parameter);
                }
            }
        }

        private async void start1(object obj)
        {
            wdauth item = (wdauth)obj;
            try
            {
                string payurl = item.url1;
                string orderid = item.orderid;
                string[] sp = payurl.Split('?')[1].Split('&');
                string ct = string.Empty;
                string tk = string.Empty;
                ct = sp[0].Split('=')[1];
                tk = sp[4].Split('=')[1];
                string items_str = item.itemid;
                string orderAmount = (item.price * item.count + item.express_fee).ToString("f2");
                string cts = "{\"ct\":\"" + ct + "\",\"token\":\"" + tk + "\",\"from\":\"H5\",\"platForm\":\"browser\",\"environment\":\"DEFAULT\",\"appVersion\":\"\",\"orderAmount\":\"" + orderAmount + "\",\"isMall\":0,\"entrance\":\"common_cashier\",\"instCode\":\"ALIPAY\",\"dbcr\":\"GC\",\"payMode\":\"ONLINE_BANK\",\"concession\":true,\"clientExt\":{\"deviceType\":\"H5\",\"payTypeFrom\":\"payCashier\",\"userAgent\":\"Mozilla/5.0 (Linux; Android 8.0.0; SM-G955U Build/R16NW) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Mobile Safari/537.36\",\"shopType\":\"normal\",\"isMobile\":true,\"entrance\":\"common_cashier\",\"itemIds\":\"" + items_str + "\"}}";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("param", cts);
                dic.Add("v", "1.0");
                dic.Add("timestamp", COMM.ConvertDateTimeToString());
                string cookies = "__spider__sessionid=" + COMM.r(16) + "; __spider__visitorid=" + COMM.r(16) + "; duid=" + item.duid + "; is_login=true; login_source=LOGIN_USER_SOURCE_MASTER; login_token=" + item.loginToken + "; login_type=LOGIN_USER_TYPE_MASTER; sid=" + item.sid + "; uid=" + item.uid + "; v-components/tencent-live-plugin@wfr=c_wxh5; wdtoken=8f2d1224";
                HttpRequestEntity resu = await MYHTTP.Result("POST", "https://thor.weidian.com/cashier/pay.pre/1.0", 4, dic, cookies);
                Model.微店_per permodel = JsonConvert.DeserializeObject<Model.微店_per>(resu.ResponseContent);
                string per_url = permodel.result.aliPay.payUrl;
                item.url = per_url;
                item.msg = "创建订单成功，链接提取成功";
                UPDATE(item);
                MYSQL.exp("insert into wd_ods (original,url,price,datetime) values ('" + item.original + "','" + per_url + "','" + orderAmount + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')");
                payurl = null;
                orderid = null;
                sp = null;
                ct = null;
                tk = null;
                items_str = null;
                orderAmount = null;
                cts = null;
                dic = null;
                cookies = null;
                resu = null;
                permodel = null;
                per_url = null;
                item = null;
            }
            catch (Exception ex)
            {
                log("链接提取异常");
                item = null;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int int32_1 = Convert.ToInt32(textBox4.Text);
            int int32_2 = Convert.ToInt32(textBox5.Text);
            authlist = new List<wdauth>();
            string.Format("SELECT * FROM `wd_user1` LIMIT {0} , {1}", (int32_1 - 1) * int32_2, int32_2);
            DataTable dataTable = MYSQL.Query("select * from wd_user1 where datetimes LIKE '%2023-08-30%'");
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                log("暂无数据");
            }
            else
            {
                foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                {
                    wdauth RW = new wdauth()
                    {
                        code = "",
                        nickName = row["nickname"].ToString(),
                        duid = row["duid"].ToString(),
                        loginToken = row["loginToken"].ToString(),
                        refreshToken = row["refreshToken"].ToString(),
                        original = row["original"].ToString(),
                        sid = row["sid"].ToString(),
                        uid = row["uid"].ToString(),
                        phone = row["phone"].ToString()
                    };
                    authlist.Add(RW);
                    TABLE(RW);
                    log("授权成功");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string sql = "select * from wd_user1 where remakr='0616img'  LIMIT 500";
            List<string> stringList = new List<string>();
            foreach (DataRow row in (InternalDataCollectionBase)MYSQL.Query(sql).Rows)
            {
                if (!stringList.Contains(row["original"].ToString()))
                {
                    MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,remakr,status,datetimes) values ('" + row["nickname"].ToString() + "','" + row["original"].ToString() + "','" + row["loginToken"].ToString() + "','" + row["refreshToken"].ToString() + "','" + row["phone"].ToString() + "','" + row["duid"].ToString() + "','" + row["sid"].ToString() + "','" + row["uid"].ToString() + "','cmz8803','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    stringList.Add(row["original"].ToString());
                }
            }
        }

        private void test(object obj)
        {
            foreach (wdauth wdauth in (List<wdauth>)obj)
            {
                try
                {
                    GetAddres(wdauth);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public async void GetAddres(wdauth item)
        {
            List<string> list = new List<string>();
            list.Add("17768105161");
            list.Add("16562138888");
            list.Add("18796782005");
            list.Add("19825970577");
            list.Add("13852675576");
            list.Add("18852163682");
            list.Add("18796782005");
            list.Add("17768105161");
            list.Add("18852163682");
            list.Add("19825970577");
            list.Add("16562138888");
            list.Add("13164805926");
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("param", "{\"page_num\":0,\"page_size\":50,\"keyword\":\"\",\"bizType\":0}");
                dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"ios\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"token\":\"" + item.loginToken + "\",\"duid\":\"" + item.duid + "\",\"uss\":\"" + item.loginToken + "\",\"userID\":\"" + item.duid + "\",\"wduserID\":\"" + item.sid + "\",\"wxappid\":\"wx4b74228baa15489a\"}");
                string od = await flurl.POST("https://thor.weidian.com/address/buyerGetAddressList/1.0", dic);
                if (!string.IsNullOrWhiteSpace(od))
                {
                    JObject json = COMM.GetToJsonList(od);
                    if (json["status"]["code"].ToString() == "0" && json["status"]["message"].ToString() == "OK" && json["result"] != null)
                    {
                        JToken list1 = json["result"];
                        foreach (JToken ids in list1)
                        {
                            try
                            {
                                if (list.Contains(ids["phone"].ToString()))
                                {
                                    dic = new Dictionary<string, string>();
                                    dic.Add("param", "{\"buyer_address_id\":\"" + ids["id"].ToString() + "\",\"bizType\":0}");
                                    dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"ios\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"token\":\"" + item.loginToken + "\",\"duid\":\"" + item.duid + "\",\"uss\":\"" + item.loginToken + "\",\"userID\":\"" + item.duid + "\",\"wduserID\":\"" + item.sid + "\",\"wxappid\":\"wx4b74228baa15489a\"}");
                                    string del = await flurl.POST("https://thor.weidian.com/address/buyerDeleteAddress/1.0", dic);
                                    log(del);
                                    del = null;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        list1 = null;
                    }
                    json = null;
                }
                dic = null;
                od = null;
                list = null;
            }
            catch (Exception ex)
            {
                list = null;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文档|*.txt";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            listcard = new List<string>();
            foreach (string readAllLine in File.ReadAllLines(openFileDialog.FileName.ToString(), Encoding.Default))
            {
                if (!string.IsNullOrEmpty(readAllLine))
                {
                    string str1 = readAllLine.Split('-')[0];
                    string str2 = readAllLine.Split('-')[readAllLine.Split('-').Length - 1];
                    if (str1.Length <= 3 && str2.Length == 18)
                        listcard.Add(readAllLine);
                }
            }
            log("导入数据" + listcard.Count.ToString());
            List<string> stringList = new List<string>();
            foreach (string str in listcard)
            {
                if (stringList.Count > 1000)
                {
                    stringList = new List<string>();
                    cdlist.Add(stringList);
                }
                else
                    stringList.Add(str);
            }
            log("生成数据" + cdlist.Count.ToString());
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            string sqls = "SELECT * FROM `wd_user1` where remakr='11004' and `status`='Y'";
            List<string> olist = new List<string>();
            DataTable dts = MYSQL.Query(sqls);
            foreach (DataRow row in (InternalDataCollectionBase)dts.Rows)
            {
                DataRow item = row;
                if (!olist.Contains(item["original"].ToString()))
                {
                    MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,remakr,status,datetimes) values ('" + item["nickname"].ToString() + "','" + item["original"].ToString() + "','" + item["loginToken"].ToString() + "','" + item["refreshToken"].ToString() + "','" + item["phone"].ToString() + "','" + item["duid"].ToString() + "','" + item["sid"].ToString() + "','" + item["uid"].ToString() + "','UA121390','Y','" + item["datetimes"].ToString() + "')");
                    olist.Add(item["original"].ToString());
                    item = null;
                }
            }
            sqls = null;
            olist = null;
            dts = null;
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

        private async void button13_Click(object sender, EventArgs e)
        {
            int Page = Convert.ToInt32(textBox4.Text);
            int PageSize = Convert.ToInt32(textBox5.Text);
            string sql = string.Format("SELECT * FROM `wd_user1` where remakr='gesst' and id >106678 LIMIT {0} , {1}", (Page - 1) * PageSize, PageSize);
            DataTable dt = MYSQL.Query(sql);
            foreach (DataRow item in (InternalDataCollectionBase)dt.Rows)
            {
                try
                {
                    string og = item["original"].ToString();
                    string result = await flurl.API_POST1("http://111.173.119.135:8088/auth", new
                    {
                        pid = 138,
                        order = og,
                        url = ""
                    }, COMM.pt_token);
                    JObject json = COMM.GetToJsonList(result);
                    string code = json["code"][0].ToString();
                    string info = DecodeBase64(Encoding.UTF8, json["userinfo"].ToString());
                    JObject myjson = COMM.GetToJsonList(info);
                    JObject str1 = COMM.GetToJsonList(myjson["Data"]["data"].ToString());
                    string nickName = COMM.GetToJsonList(str1["data"].ToString())["nickName"].ToString();
                    string encryptedData = str1["encryptedData"].ToString();
                    string iv = str1["iv"].ToString();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("param", "{\"encryptedData\":\"" + encryptedData + "\",\"iv\":\"" + iv + "\",\"code\":\"" + code + "\",\"triggerChangeBind\":true,\"appid\":\"wx4b74228baa15489a\"}");
                    dic.Add("context", "{\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\"}");
                    string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
                    JObject so = COMM.GetToJsonList(res);
                    string uid = so["result"]["uid"].ToString();
                    string sid = so["result"]["sid"].ToString();
                    string duid = so["result"]["duid"].ToString();
                    string loginToken = so["result"]["loginToken"].ToString();
                    string refreshToken = so["result"]["refreshToken"].ToString();
                    log("授权成功");
                    MYSQL.exp("update wd_user1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
                    og = null;
                    result = null;
                    json = null;
                    code = null;
                    info = null;
                    myjson = null;
                    str1 = null;
                    nickName = null;
                    encryptedData = null;
                    iv = null;
                    dic = null;
                    res = null;
                    so = null;
                    uid = null;
                    sid = null;
                    duid = null;
                    loginToken = null;
                    refreshToken = null;
                }
                catch (Exception ex1)
                {
                    Exception ex = ex1;
                }
            }
            sql = null;
            dt = null;
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            int Page = Convert.ToInt32(textBox4.Text);
            int PageSize = Convert.ToInt32(textBox5.Text);
            string sql = string.Format("SELECT * FROM `wd_user1`  where remakr='1107' and `status`='Y' LIMIT {0} , {1}", (Page - 1) * PageSize, PageSize);
            sql = "select  * from wd_user1  where remakr='ceshi'";
            DataTable dt = MYSQL.Query(sql);
            foreach (DataRow item in (InternalDataCollectionBase)dt.Rows)
            {
                try
                {
                    string og = item["original"].ToString();
                    string result = await flurl.API_GET("http://202.189.5.105:7899/api/auth?account=9900&pwd=vb4544&appId=wx4b74228baa15489a&id=" + og + "&channel=1&authType=1&url=");
                    if (string.IsNullOrWhiteSpace(result) || !result.Contains("失败"))
                    {
                        JObject json = COMM.GetToJsonList(result);
                        string info = json["data"]["code"].ToString();
                        JObject myjson = COMM.GetToJsonList(info);
                        string code = myjson["code"].ToString();
                        if (string.IsNullOrWhiteSpace(code))
                        {
                            result = await flurl.API_GET("http://202.189.5.105:7899/api/auth?account=9900&pwd=vb4544&appId=wx4b74228baa15489a&id=" + og + "&channel=1&authType=1&url=");
                            json = COMM.GetToJsonList(result);
                            info = json["data"]["code"].ToString();
                            myjson = COMM.GetToJsonList(info);
                            code = myjson["code"].ToString();
                        }
                        JObject js = COMM.GetToJsonList(myjson["userData"].ToString());
                        JObject js1 = COMM.GetToJsonList(myjson["phoneData"].ToString());
                        string nickName = COMM.GetToJsonList(js["data"].ToString())["nickName"].ToString();
                        string encryptedData = js["encryptedData"].ToString();
                        string iv = js["iv"].ToString();
                        string phone = getRandomTel();
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("param", "{\"encryptedData\":\"" + encryptedData + "\",\"iv\":\"" + iv + "\",\"code\":\"" + code + "\",\"triggerChangeBind\":true,\"appid\":\"wx4b74228baa15489a\"}");
                        dic.Add("context", "{\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\"}");
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
                        log("授权成功");
                        MYSQL.exp("update wd_user1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
                        og = null;
                        result = null;
                        json = null;
                        info = null;
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
                }
                catch (Exception ex1)
                {
                    Exception ex = ex1;
                }
            }
            sql = null;
            dt = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            button14 = new Button();
            button13 = new Button();
            button11 = new Button();
            button12 = new Button();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            button5 = new Button();
            textBox3 = new TextBox();
            button4 = new Button();
            textBox2 = new TextBox();
            button6 = new Button();
            button1 = new Button();
            button3 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            panel2 = new Panel();
            textBox7 = new TextBox();
            dataGridView1 = new DataGridView();
            nickName = new DataGridViewTextBoxColumn();
            original = new DataGridViewTextBoxColumn();
            loginToken = new DataGridViewTextBoxColumn();
            refreshToken = new DataGridViewTextBoxColumn();
            phone = new DataGridViewTextBoxColumn();
            duid = new DataGridViewTextBoxColumn();
            sid = new DataGridViewTextBoxColumn();
            uid = new DataGridViewTextBoxColumn();
            addresid = new DataGridViewTextBoxColumn();
            msg = new DataGridViewTextBoxColumn();
            url = new DataGridViewTextBoxColumn();
            url1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            免登录打开ToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            panel1.Controls.Add(button14);
            panel1.Controls.Add(button13);
            panel1.Controls.Add(button11);
            panel1.Controls.Add(button12);
            panel1.Controls.Add(button10);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(textBox5);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1210, 129);
            panel1.TabIndex = 1;
            button14.Location = new Point(589, 78);
            button14.Name = "button14";
            button14.Size = new Size(111, 31);
            button14.TabIndex = 21;
            button14.Text = "白云复扫";
            button14.UseVisualStyleBackColor = true;
            button14.Click += new EventHandler(button14_Click);
            button13.Location = new Point(704, 79);
            button13.Name = "button13";
            button13.Size = new Size(111, 31);
            button13.TabIndex = 20;
            button13.Text = "复扫";
            button13.UseVisualStyleBackColor = true;
            button13.Click += new EventHandler(button13_Click);
            button11.Location = new Point(1056, 42);
            button11.Name = "button11";
            button11.Size = new Size(111, 31);
            button11.TabIndex = 19;
            button11.Text = "筛选";
            button11.UseVisualStyleBackColor = true;
            button11.Click += new EventHandler(button11_Click);
            button12.Location = new Point(939, 42);
            button12.Name = "button12";
            button12.Size = new Size(111, 31);
            button12.TabIndex = 18;
            button12.Text = "导入";
            button12.UseVisualStyleBackColor = true;
            button12.Click += new EventHandler(button12_Click);
            button10.Location = new Point(821, 42);
            button10.Name = "button10";
            button10.Size = new Size(111, 31);
            button10.TabIndex = 17;
            button10.Text = "同步";
            button10.UseVisualStyleBackColor = true;
            button10.Click += new EventHandler(button10_Click);
            button9.Location = new Point(704, 42);
            button9.Name = "button9";
            button9.Size = new Size(111, 31);
            button9.TabIndex = 16;
            button9.Text = "取全部号";
            button9.UseVisualStyleBackColor = true;
            button9.Click += new EventHandler(button9_Click);
            button8.Location = new Point(589, 41);
            button8.Name = "button8";
            button8.Size = new Size(111, 31);
            button8.TabIndex = 15;
            button8.Text = "提取链接";
            button8.UseVisualStyleBackColor = true;
            button8.Click += new EventHandler(button8_Click);
            button7.Location = new Point(119, 41);
            button7.Name = "button7";
            button7.Size = new Size(111, 31);
            button7.TabIndex = 14;
            button7.Text = "开始获取账号";
            button7.UseVisualStyleBackColor = true;
            button7.Click += new EventHandler(button7_Click);
            textBox5.Location = new Point(73, 46);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(40, 21);
            textBox5.TabIndex = 13;
            textBox5.Text = "1000";
            textBox4.Location = new Point(13, 46);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(40, 21);
            textBox4.TabIndex = 12;
            textBox4.Text = "1";
            button5.Location = new Point(355, 41);
            button5.Name = "button5";
            button5.Size = new Size(111, 31);
            button5.TabIndex = 11;
            button5.Text = "停止";
            button5.UseVisualStyleBackColor = true;
            button5.Click += new EventHandler(button5_Click);
            textBox3.Location = new Point(472, 46);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(111, 21);
            textBox3.TabIndex = 10;
            textBox3.Text = "17:59:58.000";
            button4.Location = new Point(236, 41);
            button4.Name = "button4";
            button4.Size = new Size(111, 31);
            button4.TabIndex = 9;
            button4.Text = "导入身份信息";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            textBox2.Location = new Point(589, 15);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(609, 21);
            textBox2.TabIndex = 8;
            textBox2.Text = "105294427035-488.00";
            button6.Location = new Point(236, 10);
            button6.Name = "button6";
            button6.Size = new Size(111, 31);
            button6.TabIndex = 7;
            button6.Text = "导入地址";
            button6.UseVisualStyleBackColor = true;
            button6.Click += new EventHandler(button6_Click);
            button1.Location = new Point(472, 10);
            button1.Name = "button1";
            button1.Size = new Size(111, 31);
            button1.TabIndex = 4;
            button1.Text = "开始下单";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click_1);
            button3.Location = new Point(355, 10);
            button3.Name = "button3";
            button3.Size = new Size(111, 31);
            button3.TabIndex = 3;
            button3.Text = "新增地址";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            textBox1.Location = new Point(13, 15);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 21);
            textBox1.TabIndex = 2;
            button2.Location = new Point(119, 10);
            button2.Name = "button2";
            button2.Size = new Size(111, 31);
            button2.TabIndex = 1;
            button2.Text = "开始获取账号";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            panel2.Controls.Add(textBox7);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 129);
            panel2.Name = "panel2";
            panel2.Size = new Size(1210, 180);
            panel2.TabIndex = 2;
            textBox7.BackColor = SystemColors.ActiveCaptionText;
            textBox7.Dock = DockStyle.Fill;
            textBox7.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox7.ForeColor = Color.YellowGreen;
            textBox7.Location = new Point(0, 0);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.ScrollBars = ScrollBars.Vertical;
            textBox7.Size = new Size(1210, 180);
            textBox7.TabIndex = 8;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(nickName, original, loginToken, refreshToken, phone, duid, sid, uid, addresid, msg, url, url1);
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 309);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 27;
            dataGridView1.Size = new Size(1210, 354);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
            nickName.DataPropertyName = "nickName";
            nickName.HeaderText = "nickName";
            nickName.Name = "nickName";
            original.DataPropertyName = "original";
            original.HeaderText = "original";
            original.Name = "original";
            loginToken.DataPropertyName = "loginToken";
            loginToken.HeaderText = "loginToken";
            loginToken.Name = "loginToken";
            refreshToken.DataPropertyName = "refreshToken";
            refreshToken.HeaderText = "refreshToken";
            refreshToken.Name = "refreshToken";
            phone.DataPropertyName = "phone";
            phone.HeaderText = "phone";
            phone.Name = "phone";
            duid.DataPropertyName = "duid";
            duid.HeaderText = "duid";
            duid.Name = "duid";
            duid.Width = 80;
            sid.DataPropertyName = "sid";
            sid.HeaderText = "sid";
            sid.Name = "sid";
            sid.Width = 80;
            uid.DataPropertyName = "uid";
            uid.HeaderText = "uid";
            uid.Name = "uid";
            uid.Width = 80;
            addresid.DataPropertyName = "addresid";
            addresid.HeaderText = "addresid";
            addresid.Name = "addresid";
            addresid.Width = 80;
            msg.DataPropertyName = "msg";
            msg.HeaderText = "msg";
            msg.Name = "msg";
            msg.Width = 200;
            url.DataPropertyName = "url";
            url.HeaderText = "url";
            url.Name = "url";
            url.Width = 200;
            url1.DataPropertyName = "url1";
            url1.HeaderText = "url1";
            url1.Name = "url1";
            dataGridViewTextBoxColumn1.DataPropertyName = "nickName";
            dataGridViewTextBoxColumn1.HeaderText = "nickName";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn2.DataPropertyName = "code";
            dataGridViewTextBoxColumn2.HeaderText = "code";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn3.DataPropertyName = "original";
            dataGridViewTextBoxColumn3.HeaderText = "original";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn4.DataPropertyName = "loginToken";
            dataGridViewTextBoxColumn4.HeaderText = "loginToken";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn5.DataPropertyName = "refreshToken";
            dataGridViewTextBoxColumn5.HeaderText = "refreshToken";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn6.DataPropertyName = "phone";
            dataGridViewTextBoxColumn6.HeaderText = "phone";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.Width = 80;
            dataGridViewTextBoxColumn7.DataPropertyName = "duid";
            dataGridViewTextBoxColumn7.HeaderText = "duid";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.Width = 80;
            dataGridViewTextBoxColumn8.DataPropertyName = "sid";
            dataGridViewTextBoxColumn8.HeaderText = "sid";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.Width = 80;
            dataGridViewTextBoxColumn9.DataPropertyName = "uid";
            dataGridViewTextBoxColumn9.HeaderText = "uid";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.Width = 80;
            dataGridViewTextBoxColumn10.DataPropertyName = "addresid";
            dataGridViewTextBoxColumn10.HeaderText = "addresid";
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.Width = 200;
            dataGridViewTextBoxColumn11.DataPropertyName = "msg";
            dataGridViewTextBoxColumn11.HeaderText = "msg";
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.Width = 200;
            dataGridViewTextBoxColumn12.DataPropertyName = "url";
            dataGridViewTextBoxColumn12.HeaderText = "url";
            dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
            {
                免登录打开ToolStripMenuItem
            });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(137, 26);
            免登录打开ToolStripMenuItem.Name = "免登录打开ToolStripMenuItem";
            免登录打开ToolStripMenuItem.Size = new Size(136, 22);
            免登录打开ToolStripMenuItem.Text = "免登录打开";
            免登录打开ToolStripMenuItem.Click += new EventHandler(免登录打开ToolStripMenuItem_Click);
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1210, 663);
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "weidian";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "weidian";
            Load += new EventHandler(weidian_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        public delegate void SetTextHandler(string text);

        private delegate void UpdateDataGridView(DataTable dt);
    }
}
