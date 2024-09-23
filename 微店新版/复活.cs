// Decompiled with JetBrains decompiler
// Type: 微店新版.复活
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版
{
  public class 复活 : Form
  {
    private flurl flurl;
    private IContainer components =  null;
    private ComboBox comboBox2;
    private Button button1;
    private TextBox textBox1;
    private TextBox textBox2;
    private Button button2;

    public 复活() => InitializeComponent();

    private void log(string text)
    {
      if (textBox1.InvokeRequired)
        textBox1.Invoke(new SetTextHandler(log),  text);
      else
        textBox1.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
    }

    private void 复活_Load(object sender, EventArgs e)
    {
      MYSQL.Init();
      IniFiles.inipath = Application.StartupPath + "\\Config.ini";
      if (IniFiles.ExistINIFile())
      {
        COMM.proxy_ip = IniFiles.IniReadValue("代理", "host");
        COMM.proxy_port = int.Parse(IniFiles.IniReadValue("代理", "proxy"));
        COMM.username = IniFiles.IniReadValue("代理", "username");
        COMM.password = IniFiles.IniReadValue("代理", "password");
        flurl = new flurl();
      }
      DataTable dataTable = MYSQL.Query("select username from wd_admin");
      if (dataTable == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string text = row["username"].ToString();
        comboBox2.Items.Add(text);
        log(text);
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(textBox2.Text);
      string text = comboBox2.Text;
      DataTable dataTable1 = new DataTable();
      List<string> stringList = new List<string>();
      DataTable dataTable2 = !string.IsNullOrWhiteSpace(text) ? MYSQL.Query("select DISTINCT original from wd_user1 where remakr='" + text + "' and  status='Y'") : MYSQL.Query("select DISTINCT original from wd_user1 where status='Y'");
      if (dataTable2 != null && dataTable2.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          stringList.Add(row["original"].ToString());
        if (stringList.Count < 10)
        {
          new Thread(new ParameterizedThreadStart(fuhuo1)).Start( stringList);
        }
        else
        {
          int num = stringList.Count / int32;
          foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(stringList, num))
            new Thread(new ParameterizedThreadStart(fuhuo1)).Start( split.Value);
        }
      }
      else
        log("没有数据");
    }

    private async void fuhuo1(object obj)
    {
      // ISSUE: reference to a compiler-generated field
      //int num = this.<>__state;
      while (true)
      {
        List<string> list = (List<string>) obj;
        foreach (string ims in list)
        {
          try
          {
            string og = ims;
            string result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=lQnVkowPBlWQ8GmR9F8giOFrYFHkDz&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
            JObject json = COMM.GetToJsonList(result);
            if (json != null && json["code"] != null && json["code"].ToString() == "20000" && json["message"] != null && json["message"].ToString() == "授权成功")
            {
              string original = json["data"]["orderId"].ToString();
              string info = json["data"]["callback"].ToString().Replace("授权成功：", "");
              JObject myjson = COMM.GetToJsonList(info);
              string code = myjson["code"].ToString();
              if (string.IsNullOrWhiteSpace(code))
              {
                result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=lQnVkowPBlWQ8GmR9F8giOFrYFHkDz&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=1");
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
              dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"windows\",\"anonymousId\":\"5d63b27a403e9e25cfd4c51c6ad674d83af60acc\",\"visitor_id\":\"5d63b27a403e9e25cfd4c51c6ad674d83af60acc\",\"wxappid\":\"wx4b74228baa15489a\"}");
              string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
              JObject so = COMM.GetToJsonList(res);
              string uid = so["result"]["uid"].ToString();
              string sid = so["result"]["sid"].ToString();
              string duid = so["result"]["duid"].ToString();
              string loginToken = so["result"]["loginToken"].ToString();
              string refreshToken = so["result"]["refreshToken"].ToString();
              MYSQL.exp("update wd_user1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
              log(og + "复活成功");
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
            og = null;
            result = null;
            json = null;
          }
          catch (Exception ex1)
          {
            Exception ex = ex1;
          }
        }
        log("复活完毕,休眠中");
        Thread.Sleep(21600);
        list = null;
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new Thread(new ParameterizedThreadStart(fuhuo2)).Start("");
    }

    public async Task<bool> xufei(string orderid, string token)
    {
      try
      {
        string result = await flurl.API_POST("http://shapp.mrlj.cn:8881/addtime2?token=" + token + "&orderId=" + orderid);
        return result != null && result.Contains("续费成功");
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private async void fuhuo2(object obj)
    {
      // ISSUE: reference to a compiler-generated field
      //int num = this.\u003C\u003E1__state;
      while (true)
      {
        List<string> list = new List<string>();
        DataTable dt = MYSQL.Query("SELECT * FROM wd_s_user WHERE datetimes >= DATE_SUB(CURDATE(), INTERVAL 1 WEEK) AND datetimes <= CURDATE();");
        if (dt != null && dt.Rows.Count > 0)
        {
          foreach (DataRow item in (InternalDataCollectionBase) dt.Rows)
            list.Add(item["original"].ToString());
        }
        foreach (string ims in list)
        {
          try
          {
            string og = ims;
            string result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=wB7K2XYyolX6uKRmS31wWyVA52kOch&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
            JObject json = COMM.GetToJsonList(result);
            if (json != null && json["code"] != null && json["code"].ToString() == "20000" && json["message"] != null && json["message"].ToString() == "授权成功")
            {
              string original = json["data"]["orderId"].ToString();
              string info = json["data"]["callback"].ToString().Replace("授权成功：", "");
              JObject myjson = COMM.GetToJsonList(info);
              string code = myjson["code"].ToString();
              if (string.IsNullOrWhiteSpace(code))
              {
                result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=wB7K2XYyolX6uKRmS31wWyVA52kOch&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=1");
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
              dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"windows\",\"anonymousId\":\"5d63b27a403e9e25cfd4c51c6ad674d83af60acc\",\"visitor_id\":\"5d63b27a403e9e25cfd4c51c6ad674d83af60acc\",\"wxappid\":\"wx4b74228baa15489a\"}");
              string res = await flurl.POSTS1("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
              JObject so = COMM.GetToJsonList(res);
              string uid = so["result"]["uid"].ToString();
              string sid = so["result"]["sid"].ToString();
              string duid = so["result"]["duid"].ToString();
              string loginToken = so["result"]["loginToken"].ToString();
              string refreshToken = so["result"]["refreshToken"].ToString();
              MYSQL.exp("update wd_s_user set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "' where original='" + og + "'");
              log(og + "复活成功");
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
            og = null;
            result = null;
            json = null;
          }
          catch (Exception ex1)
          {
            Exception ex = ex1;
            log("复活报错了");
          }
          Thread.Sleep(3000);
        }
        log("复活完毕,休眠中");
        Thread.Sleep(21600);
        list = null;
        dt = null;
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
      comboBox2 = new ComboBox();
      button1 = new Button();
      textBox1 = new TextBox();
      textBox2 = new TextBox();
      button2 = new Button();
      SuspendLayout();
      comboBox2.FormattingEnabled = true;
      comboBox2.Location = new Point(13, 8);
      comboBox2.Name = "comboBox2";
      comboBox2.Size = new Size(155, 20);
      comboBox2.TabIndex = 1;
      button1.Location = new Point(280, 7);
      button1.Name = "button1";
      button1.Size = new Size(112, 23);
      button1.TabIndex = 2;
      button1.Text = "微店复活";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      textBox1.Location = new Point(13, 36);
      textBox1.Multiline = true;
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(497, 358);
      textBox1.TabIndex = 3;
      textBox2.Location = new Point(174, 8);
      textBox2.Name = "textBox2";
      textBox2.Size = new Size(100, 21);
      textBox2.TabIndex = 4;
      button2.Location = new Point(398, 7);
      button2.Name = "button2";
      button2.Size = new Size(112, 23);
      button2.TabIndex = 5;
      button2.Text = "晒图复活";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(519, 406);
      Controls.Add(button2);
      Controls.Add(textBox2);
      Controls.Add(textBox1);
      Controls.Add(button1);
      Controls.Add(comboBox2);
      MaximizeBox = false;
      MaximumSize = new Size(535, 445);
      MinimumSize = new Size(535, 445);
      Name = "复活";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "复活";
      Load += new EventHandler(复活_Load);
      ResumeLayout(false);
      PerformLayout();
    }

    public delegate void SetTextHandler(string text);
  }
}
