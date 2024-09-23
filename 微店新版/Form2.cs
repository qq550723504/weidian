// Decompiled with JetBrains decompiler
// Type: 微店新版.Form2
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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版
{
    public class Form2 : Form
    {
        private flurl flurl;
        public static List<wdauth> authlist = new List<wdauth>();
        private string[] telStarts = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
        private IContainer components = null;
        private Button button1;
        private TextBox textBox7;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button2;
        private TextBox textBox3;
        private TextBox textBox4;

        public Form2() => InitializeComponent();

        private void log(string text)
        {
            if (textBox7.InvokeRequired)
                textBox7.Invoke(new SetTextHandler(log), text);
            else
                textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
        }

        public static bool flog { get; set; }

        public static int f { get; set; }

        /// <summary>
        /// 加号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            flurl = new flurl();
            flog = true;
            f = Convert.ToInt32(textBox1.Text);
            int int32 = Convert.ToInt32(textBox2.Text);
            for (int index = 0; index < int32; ++index)
                new Thread(new ParameterizedThreadStart(start)).Start("");
        }

        private async void start(object obj)
        {
            while (flog)
            {
                if (authlist.Count >= f)
                {
                    log("注册已达阈值，注册已终止");
                    flog = false;
                    break;
                }
                try
                {
                    string vtoken = "wB7K2XYyolX6uKRmS31wWyVA52kOch";
                    string result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=" + vtoken + "&appId=71953&qrCode=1&orderId=&miniAuthType=4");
                    if (!result.Contains("余额不足"))
                    {
                        JObject json = COMM.GetToJsonList(result);
                        if (json != null && json["code"] != null && json["code"].ToString() == "20000" && json["message"] != null && json["message"].ToString() == "授权成功")
                        {
                            string original = json["data"]["orderId"].ToString();
                            string info = json["data"]["callback"].ToString().Replace("授权成功：", "");
                            JObject myjson = COMM.GetToJsonList(info);
                            string code = myjson["code"].ToString();
                            if (string.IsNullOrWhiteSpace(code))
                            {
                                result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=" + vtoken + "&appId=71953&qrCode=1&orderId=&miniAuthType=4");
                                json = COMM.GetToJsonList(result);
                                info = json["data"]["callback"].ToString().Replace("授权成功：", "");
                                myjson = COMM.GetToJsonList(info);
                                code = myjson["code"].ToString();
                                original = json["data"]["orderId"].ToString();
                            }
                            JObject js = COMM.GetToJsonList(myjson["userData"].ToString());
                            JObject js1 = COMM.GetToJsonList(myjson["phoneData"].ToString());
                            string nickName = COMM.GetToJsonList(js["data"].ToString())["nickName"].ToString().RemoveControlChars();
                            string encryptedData = js["encryptedData"].ToString();
                            string iv = js["iv"].ToString();
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("param", "{\"encryptedData\":\"" + encryptedData + "\",\"iv\":\"" + iv + "\",\"code\":\"" + code + "\",\"triggerChangeBind\":true,\"appid\":\"wx4b74228baa15489a\"}");
                            dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"windows\",\"anonymousId\":\"89a6cb88a9ac3271431575b945e31661e42b77fb\",\"visitor_id\":\"89a6cb88a9ac3271431575b945e31661e42b77fb\",\"wxappid\":\"wx4b74228baa15489a\"}");
                            string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
                            JObject so = COMM.GetToJsonList(res);
                            string uid = so["result"]["uid"].ToString();
                            string sid = so["result"]["sid"].ToString();
                            string duid = so["result"]["duid"].ToString();
                            string loginToken = so["result"]["loginToken"].ToString();
                            string phone = getRandomTel();
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
                            log("已注册成功数" + authlist.Count.ToString());
                            MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,remakr,status,datetimes) values ('" + nickName + "','" + original + "','" + loginToken + "','" + refreshToken + "','" + phone + "','" + duid + "','" + sid + "','" + uid + "','v01','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                            MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,remakr,status,datetimes) values ('" + nickName + "','" + original + "','" + loginToken + "','" + refreshToken + "','" + phone + "','" + duid + "','" + sid + "','" + uid + "','wd1101','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                            original = null;
                            info = null;
                            myjson = null;
                            code = null;
                            js = null;
                            js1 = null;
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
                            phone = null;
                            refreshToken = null;
                            auth = null;
                        }
                        else
                            log(result);
                        vtoken = null;
                        result = null;
                        json = null;
                    }
                    else
                        break;
                }
                catch (Exception ex1)
                {
                    Exception ex = ex1;
                    log("注册异常");
                }
            }
            DataTable dt = MYSQL.Query("SELECT count,username FROM `wd_admin` where username in ('v01','wd1101')");
            if (dt == null || dt.Rows.Count <= 0)
            {
                dt = null;
            }
            else
            {
                foreach (DataRow item in (InternalDataCollectionBase)dt.Rows)
                {
                    int num = MYSQL.exp(string.Format("update wd_admin set count='{0}' where username='{1}'", Convert.ToInt32(item["count"].ToString()) + authlist.Count, item["username"].ToString()));
                    if (num > 0)
                        log(item["username"].ToString() + string.Format("加号成功，加号前{0},加号后{1}", item["count"].ToString(), Convert.ToInt32(item["count"].ToString()) + authlist.Count));
                }
                dt = null;
            }
        }

        public string getRandomTel()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            random.Next(10, 1000);
            return telStarts[random.Next(0, telStarts.Length - 1)] + (random.Next(100, 888) + 10000).ToString().Substring(1) + (random.Next(1, 9100) + 10000).ToString().Substring(1);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MYSQL.Init();
            IniFiles.inipath = Application.StartupPath + "\\Config.ini";
            COMM.proxy_ip = IniFiles.IniReadValue("代理", "host");
            COMM.proxy_port = int.Parse(IniFiles.IniReadValue("代理", "proxy"));
            COMM.username = IniFiles.IniReadValue("代理", "username");
            COMM.password = IniFiles.IniReadValue("代理", "password");
            string path = Environment.CurrentDirectory + "\\UA.txt";
            if (!File.Exists(path))
                return;
            foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (!string.IsNullOrEmpty(readAllLine))
                    COMM.agentlist.Add(readAllLine);
            }
        }
        /// <summary>
        /// 复活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            flurl = new flurl();
            string text1 = textBox3.Text;
            string text2 = textBox4.Text;
            DataTable dataTable = MYSQL.Query("select original from wd_s_user where remakr='" + text1 + "' and datetimes >= DATE_SUB(CURDATE(), INTERVAL " + text2 + " DAY);");
            if (dataTable == null || dataTable.Rows.Count <= 0)
            { 
                log("未找到相关数据");
                return; 
            }
            log(string.Format("找到{0}，{1}天内的数据共计{2}条", text1, text2, dataTable.Rows.Count));
            List<string> stringList = new List<string>();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                stringList.Add(row["original"].ToString());
            if (stringList.Count < 10)
            {
                new Thread(new ParameterizedThreadStart(start1)).Start(stringList);
            }
            else
            {
                int num = stringList.Count / 10;
                foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(stringList, num))
                    new Thread(new ParameterizedThreadStart(start1)).Start(split.Value);
            }
        }

        private async void start1(object obj)
        {
            List<string> list = (List<string>)obj;
            foreach (string item in list)
            {
                try
                {
                    try
                    {
                        string token = "wB7K2XYyolX6uKRmS31wWyVA52kOch";
                        string og = item;
                        string result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=" + token + "&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
                        JObject json = COMM.GetToJsonList(result);
                        if (json != null && json["code"] != null && json["code"].ToString() == "20000" && json["message"] != null && json["message"].ToString() == "授权成功")
                        {
                            string original = json["data"]["orderId"].ToString();
                            string info = json["data"]["callback"].ToString().Replace("授权成功：", "");
                            JObject myjson = COMM.GetToJsonList(info);
                            string code = myjson["code"].ToString();
                            if (string.IsNullOrWhiteSpace(code))
                            {
                                result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=" + token + "&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=1");
                                json = COMM.GetToJsonList(result);
                                info = json["data"]["callback"].ToString().Replace("授权成功：", "");
                                myjson = COMM.GetToJsonList(info);
                                code = myjson["code"].ToString();
                                original = json["data"]["orderId"].ToString();
                            }
                            JObject js = COMM.GetToJsonList(myjson["userData"].ToString());
                            JObject js1 = COMM.GetToJsonList(myjson["phoneData"].ToString());
                            string nickName = COMM.GetToJsonList(js["data"].ToString())["nickName"].ToString().RemoveControlChars();
                            string encryptedData = js["encryptedData"].ToString();
                            string iv = js["iv"].ToString();
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("param", "{\"encryptedData\":\"" + encryptedData + "\",\"iv\":\"" + iv + "\",\"code\":\"" + code + "\",\"triggerChangeBind\":true,\"appid\":\"wx4b74228baa15489a\"}");
                            dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"anonymousId\":\"5d63b27a403e9e25cfd4c51c6ad674d83af60acc\",\"visitor_id\":\"5d63b27a403e9e25cfd4c51c6ad674d83af60acc\",\"wxappid\":\"wx4b74228baa15489a\"}");
                            string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
                            JObject so = COMM.GetToJsonList(res);
                            string uid = so["result"]["uid"].ToString();
                            string sid = so["result"]["sid"].ToString();
                            string duid = so["result"]["duid"].ToString();
                            string loginToken = so["result"]["loginToken"].ToString();
                            string refreshToken = so["result"]["refreshToken"].ToString();
                            MYSQL.exp("update wd_s_user set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "' where original='" + og + "'");
                            log(item + "复活成功");
                            original = null;
                            info = null;
                            myjson = null;
                            code = null;
                            js = null;
                            js1 = null;
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
                        else
                            log(result);
                        token = null;
                        og = null;
                        result = null;
                        json = null;
                    }
                    catch (Exception ex1)
                    {
                        Exception ex = ex1;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            list = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            button1 = new Button();
            textBox7 = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button2 = new Button();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            SuspendLayout();
            button1.Location = new Point(11, 9);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "加号";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            textBox7.BackColor = SystemColors.ActiveCaptionText;
            textBox7.Dock = DockStyle.Bottom;
            textBox7.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox7.ForeColor = Color.YellowGreen;
            textBox7.Location = new Point(0, 70);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.ScrollBars = ScrollBars.Vertical;
            textBox7.Size = new Size(366, 239);
            textBox7.TabIndex = 9;
            textBox1.Location = new Point(92, 10);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(77, 21);
            textBox1.TabIndex = 10;
            textBox1.Text = "200";
            textBox2.Location = new Point(175, 9);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(38, 21);
            textBox2.TabIndex = 11;
            textBox2.Text = "3";
            button2.Location = new Point(11, 43);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 12;
            button2.Text = "复活";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            textBox3.Location = new Point(92, 44);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(77, 21);
            textBox3.TabIndex = 13;
            textBox3.Text = "晒图昵称";
            textBox4.Location = new Point(175, 44);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(38, 21);
            textBox4.TabIndex = 14;
            textBox4.Text = "7";
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(366, 309);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(button2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(textBox7);
            Controls.Add(button1);
            MaximizeBox = false;
            MaximumSize = new Size(382, 348);
            MinimumSize = new Size(382, 348);
            Name = "自助";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "自助";
            Load += new EventHandler(Form2_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        public delegate void SetTextHandler(string text);
    }
}
