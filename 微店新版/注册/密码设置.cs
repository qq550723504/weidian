// Decompiled with JetBrains decompiler
// Type: 微店新版.注册.密码设置
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Auxiliary.HTTP;
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


namespace 微店新版.注册
{
  public class 密码设置 : UserControl
  {
    private DataTable DT = new DataTable();
    public static List<info> passlist = new List<info>();
    public static List<info> passlist1 = new List<info>();
    private IContainer components = null;
    private Panel panel1;
    private Button button5;
    private Button button4;
    private TextBox textBox9;
    private TextBox textBox10;
    private Label label9;
    private Label label10;
    private Panel panel2;
    private TextBox textBox7;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn 账号;
    private DataGridViewTextBoxColumn 密码;
    private DataGridViewTextBoxColumn COOKIE;
    private DataGridViewTextBoxColumn 状态;
    private Button button1;
    private Button button2;

    public static int f { get; set; }

    public 密码设置()
    {
      InitializeComponent();
      DT.Columns.Add(nameof (账号));
      DT.Columns.Add(nameof (密码));
      DT.Columns.Add(nameof (COOKIE));
      DT.Columns.Add(nameof (状态));
    }

    private void log(string text)
    {
      if (textBox7.InvokeRequired)
        textBox7.Invoke(new 密码设置.SetTextHandler(log), text);
      else
        textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
    }

    private void UpdateGV(DataTable dt)
    {
      if (dataGridView1.InvokeRequired)
      {
        BeginInvoke(new 密码设置.UpdateDataGridView(UpdateGV), dt);
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

    public void TABLE(info RW)
    {
      BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
      {
        DataRow row = DT.NewRow();
        row["账号"] = RW.phone;
        row["COOKIE"] = RW.cookie;
        row["状态"] = RW.status;
        DT.Rows.Add(row);
        if (DT == null || DT.Rows.Count <= 0)
          return;
        UpdateGV(DT);
      }));
    }

    public void UPDATE(info user)
    {
      try
      {
        BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
        {
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells["账号"].Value.ToString() == user.phone)
            {
              row.Cells["状态"].Value = user.status;
              row.Cells["密码"].Value = user.password;
              row.Cells["COOKIE"].Value = user.cookie;
              break;
            }
          }
        }));
      }
      catch (Exception ex)
      {
      }
    }

    private async void start(object obj)
    {
      List<info> list = (List<info>) obj;
      try
      {
        foreach (info info1 in list)
        {
          info wd = info1;
          if (!(wd.status == "密码设置成功"))
          {
            try
            {
              string randstr = "";
              string securityticket = "";
              HttpRequestEntity txjson = await MYHTTP.Result("GET", "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + COMM.proxy_ip + ":" + COMM.proxy_port.ToString(), 0, null, null);
              while (txjson == null || string.IsNullOrWhiteSpace(txjson.ResponseContent))
              {
                randstr = "";
                securityticket = "";
                txjson = await MYHTTP.Result("GET", "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + COMM.proxy_ip + ":" + COMM.proxy_port.ToString(), 0, null, null);
              }
              Model.腾讯滑块 tx;
              for (tx = JsonConvert.DeserializeObject<Model.腾讯滑块>(txjson.ResponseContent); tx == null || string.IsNullOrWhiteSpace(tx.ticket) || string.IsNullOrWhiteSpace(tx.randstr); tx = JsonConvert.DeserializeObject<Model.腾讯滑块>(txjson.ResponseContent))
              {
                randstr = "";
                securityticket = "";
                txjson = await MYHTTP.Result("GET", "http://127.0.0.1:13366/ocr.html?aid=2003473469&ip=" + COMM.proxy_ip + ":" + COMM.proxy_port.ToString(), 0, null, null);
              }
              randstr = tx.randstr;
              securityticket = tx.ticket;
              string phones = await GetPhone(wd);
              if (phones == null || string.IsNullOrWhiteSpace(phones))
              {
                wd.status = "指定号码失败或号码已下线";
                log("指定号码失败或号码已下线");
                UPDATE(wd);
                continue;
              }
              Dictionary<string, string> param = new Dictionary<string, string>();
              param.Add("param", "{\"countryCode\":86,\"phone\":\"" + wd.phone + "\",\"scene\":\"PCModifyPhone\",\"slideImageAppId\":\"2003473469\",\"slideTicket\":\"" + securityticket + "\",\"slideRandStr\":\"" + randstr + "\"}");
              HttpRequestEntity vcode = await MYHTTP.Result("POST", "https://thor.weidian.com/passport/get.vcode/2.0", 1, param, wd.cookie);
              for (int i = 0; i < 3 && (vcode == null || string.IsNullOrWhiteSpace(vcode.ResponseContent) || !vcode.ResponseContent.Contains("成功")); ++i)
                vcode = await MYHTTP.Result("POST", "https://thor.weidian.com/passport/get.vcode/2.0", 1, param, wd.cookie);
              if (vcode != null && !string.IsNullOrWhiteSpace(vcode.ResponseContent) && vcode.ResponseContent.Contains("成功"))
              {
                wd.status = "验证码发送成功";
                UPDATE(wd);
                log(wd.status);
                info info = wd;
                string str = await GetCode(wd);
                info.code = str;
                info = null;
                str = null;
                if (string.IsNullOrWhiteSpace(wd.code))
                {
                  wd.status = "验证码获取失败";
                  UPDATE(wd);
                  log(wd.status);
                  continue;
                }
                param = new Dictionary<string, string>();
                param.Add("phone", wd.phone);
                param.Add("countryCode", "86");
                param.Add("vcode", wd.code);
                param.Add("action", "https://sso.weidian.com/user/passwd/recover");
                param.Add("wdtoken", COMM.tokens(wd.cookie));
                HttpRequestEntity resp = await MYHTTP.Result("POST", "https://sso.weidian.com/user/vcode/verify", 1, param, wd.cookie);
                for (int i = 0; i < 3 && (resp == null || string.IsNullOrWhiteSpace(resp.ResponseContent) || !resp.ResponseContent.Contains("session")); ++i)
                  resp = await MYHTTP.Result("POST", "https://sso.weidian.com/user/vcode/verify", 1, param, wd.cookie);
                if (resp != null && !string.IsNullOrWhiteSpace(resp.ResponseContent) && resp.ResponseContent.Contains("session"))
                {
                  JObject so = COMM.GetToJsonList(resp.ResponseContent);
                  string session = so["result"]["session"].ToString();
                  resp = await MYHTTP.Result("GET", "https://sso.weidian.com/user/passwd/recover?password=pass" + wd.phone.Substring(5) + "&version=1&session=" + session + "&clientInfo=%7B%22clientType%22%3A1%7D&wdtoken=" + COMM.tokens(wd.cookie) + "&_=" + COMM.ConvertDateTimeToString(), 1, null, wd.cookie);
                  if (resp != null && !string.IsNullOrWhiteSpace(resp.ResponseContent) && resp.ResponseContent.Contains("cookie"))
                  {
                    wd.status = "密码设置成功";
                    wd.password = "pass" + wd.phone.Substring(5);
                    UPDATE(wd);
                    log(wd.status);
                                        passlist1.Add(wd);
                  }
                  else
                  {
                    wd.status = "密码设置失败";
                    UPDATE(wd);
                    log(wd.status);
                  }
                  so = null;
                  session = null;
                }
                else
                {
                  wd.status = "滑块验证码校验失败";
                  UPDATE(wd);
                  log(wd.status);
                }
                resp = null;
              }
              else
              {
                wd.status = "验证码发送失败";
                UPDATE(wd);
                log(wd.status);
              }
              randstr = null;
              securityticket = null;
              txjson = null;
              tx = null;
              phones = null;
              param = null;
              vcode = null;
            }
            catch (Exception ex)
            {
              wd.status = "密码设置异常";
              log(wd.status);
              UPDATE(wd);
            }
            wd = null;
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      list = null;
    }

    public async Task<string> GetPhone(info wd)
    {
      string phone = string.Empty;
      try
      {
        HttpRequestEntity json = await MYHTTP.Result("GET", "http://api.lxy8.net/sms/api/getPhone?token=" + COMM.pt_token + "&sid=" + COMM.xmid + "&phone=" + wd.phone, 0, null, null);
        Model.流星接码实体 data;
        for (data = JsonConvert.DeserializeObject<Model.流星接码实体>(json.ResponseContent); data.code != 0; data = JsonConvert.DeserializeObject<Model.流星接码实体>(json.ResponseContent))
        {
          if (data.msg.Contains("下线"))
          {
            log(data.msg);
            break;
          }
          Thread.Sleep(5000);
          json = await MYHTTP.Result("GET", "http://api.lxy8.net/sms/api/getPhone?token=" + COMM.pt_token + "&sid=" + COMM.xmid + "&phone=" + wd.phone, 0, null,  null);
        }
        if (data.code == 0)
        {
          phone = data.phone;
        }
        else
        {
          log("接码获取手机号码失败");
          log(json.ResponseContent);
        }
        json = null;
        data = null;
      }
      catch (Exception ex1)
      {
        Exception ex = ex1;
        log("接码发生异常");
      }
      string phone1 = phone;
      phone = null;
      return phone1;
    }

    public async Task<string> GetPhone()
    {
      string phone = string.Empty;
      try
      {
        HttpRequestEntity json = await MYHTTP.Result("GET", "http://api.lxy8.net/sms/api/getPhone?token=" + COMM.pt_token + "&sid=" + COMM.xmid, 0, null, null);
        Model.流星接码实体 data;
        for (data = JsonConvert.DeserializeObject<Model.流星接码实体>(json.ResponseContent); data.code != 0; data = JsonConvert.DeserializeObject<Model.流星接码实体>(json.ResponseContent))
        {
          Thread.Sleep(5000);
          json = await MYHTTP.Result("GET", "http://api.lxy8.net/sms/api/getPhone?token=" + COMM.pt_token + "&sid=" + COMM.xmid, 0, null, null);
        }
        if (data.code == 0)
        {
          phone = data.phone;
        }
        else
        {
          log("接码获取手机号码失败");
          log(json.ResponseContent);
        }
        json = null;
        data = null;
      }
      catch (Exception ex1)
      {
        Exception ex = ex1;
        log("接码发生异常");
      }
      string phone1 = phone;
      phone = null;
      return phone1;
    }

    public async Task<string> GetCode(info wd)
    {
      string code = string.Empty;
      try
      {
        string url = "http://api.lxy8.net/sms/api/getMessage?token=" + COMM.pt_token + "&sid=" + COMM.xmid + "&phone=" + wd.phone;
        HttpRequestEntity json = await MYHTTP.Result("GET", url, 0, null, null);
        int index = 0;
        while ((json == null || string.IsNullOrWhiteSpace(json.ResponseContent) || !json.ResponseContent.Contains("微店")) && index < 20)
        {
          HttpRequestEntity httpRequestEntity = await MYHTTP.Result("GET", url, 0, null, null);
          json = json = httpRequestEntity;
          httpRequestEntity = null;
          Thread.Sleep(5000);
          ++index;
          log(wd.phone + "第" + index.ToString() + "次获取验证码");
          wd.status = wd.phone + "第" + index.ToString() + "次获取验证码";
          UPDATE(wd);
        }
        HttpRequestEntity httpRequestEntity1 = await MYHTTP.Result("GET", "http://api.lxy8.net/sms/api/addBlacklist?token=" + COMM.pt_token + "&sid=" + COMM.xmid + "&phone=" + wd.phone, 0, null, null);
        if (json.ResponseContent.Contains("微店"))
        {
          Model.流星接码实体 data = JsonConvert.DeserializeObject<Model.流星接码实体>(json.ResponseContent);
          if (data.code == 0)
          {
            log(json.ResponseContent);
            code = data.sms.Replace("【微店】", "").Replace("(验证码)微店客服绝不会索取此验证码，请勿将此验证码告知他人", "");
            log(wd.phone + "接码成功。" + wd.phone + "，" + code);
            wd.status = "验证码获取成功";
            UPDATE(wd);
          }
          else
          {
            log(json.ResponseContent);
            log(wd.phone + "获取验证码失败");
          }
          data = null;
        }
        url = null;
        json = null;
      }
      catch (Exception ex1)
      {
        Exception ex = ex1;
        log("接码发生异常");
      }
      string code1 = code;
      code = null;
      return code1;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (passlist == null || passlist.Count == 0)
        log("请导入需要设置的小号");
      else if (string.IsNullOrWhiteSpace(COMM.pt_token))
      {
        log("请在设置中登录接码平台");
      }
      else
      {
        string text = textBox10.Text;
                f = Convert.ToInt32(textBox9.Text);
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(f.ToString()))
          log("线程数量和阈值是必须输入的");
        else if (passlist.Count < 5)
        {
          foreach (object parameter in passlist)
            new Thread(new ParameterizedThreadStart(start)).Start(parameter);
        }
        else
        {
          int num = passlist.Count / Convert.ToInt32(text);
          foreach (KeyValuePair<string, List<info>> split in COMM.SplitList(passlist, num))
            new Thread(new ParameterizedThreadStart(start)).Start(split.Value);
        }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "文本文档|*.txt";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      string path = openFileDialog.FileName.ToString();
            passlist = new List<info>();
      foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
      {
        if (!string.IsNullOrEmpty(readAllLine))
        {
          string[] strArray = readAllLine.Split('，');
          string str1 = strArray[0].Trim();
          string str2 = strArray[1].Trim();
          info RW = new info()
          {
            phone = str1,
            cookie = str2,
            status = ""
          };
                    passlist.Add(RW);
          TABLE(RW);
        }
      }
      log("导入账号成功，导入账号" + passlist.Count.ToString());
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (passlist == null || passlist.Count == 0)
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
          foreach (info info in passlist)
            streamWriter.WriteLine(info.phone + "，" + info.password);
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      panel1 = new Panel();
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
      密码 = new DataGridViewTextBoxColumn();
      COOKIE = new DataGridViewTextBoxColumn();
      状态 = new DataGridViewTextBoxColumn();
      button1 = new Button();
      button2 = new Button();
      panel1.SuspendLayout();
      panel2.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      SuspendLayout();
      panel1.Controls.Add(button2);
      panel1.Controls.Add(button1);
      panel1.Controls.Add(button5);
      panel1.Controls.Add(button4);
      panel1.Controls.Add(textBox9);
      panel1.Controls.Add(textBox10);
      panel1.Controls.Add(label9);
      panel1.Controls.Add(label10);
      panel1.Dock = DockStyle.Top;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(914, 41);
      panel1.TabIndex = 1;
      button5.Location = new Point(625, 11);
      button5.Name = "button5";
      button5.Size = new Size(122, 23);
      button5.TabIndex = 12;
      button5.Text = "停止";
      button5.UseVisualStyleBackColor = true;
      button4.Location = new Point(495, 11);
      button4.Name = "button4";
      button4.Size = new Size(122, 23);
      button4.TabIndex = 11;
      button4.Text = "设置密码";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      textBox9.Location = new Point(215, 12);
      textBox9.Name = "textBox9";
      textBox9.Size = new Size(113, 21);
      textBox9.TabIndex = 10;
      textBox10.Location = new Point(69, 12);
      textBox10.Name = "textBox10";
      textBox10.Size = new Size(101, 21);
      textBox10.TabIndex = 9;
      label9.AutoSize = true;
      label9.Location = new Point(178, 16);
      label9.Name = "label9";
      label9.Size = new Size(29, 12);
      label9.TabIndex = 8;
      label9.Text = "阈值";
      label10.AutoSize = true;
      label10.Location = new Point(32, 16);
      label10.Name = "label10";
      label10.Size = new Size(29, 12);
      label10.TabIndex = 7;
      label10.Text = "线程";
      panel2.Controls.Add(textBox7);
      panel2.Dock = DockStyle.Top;
      panel2.Location = new Point(0, 41);
      panel2.Name = "panel2";
      panel2.Size = new Size(914, 100);
      panel2.TabIndex = 2;
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
      dataGridView1.Columns.AddRange(账号, 密码, COOKIE, 状态);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(0, 141);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(914, 397);
      dataGridView1.TabIndex = 5;
      账号.DataPropertyName = "账号";
      账号.HeaderText = "账号";
      账号.Name = "账号";
      账号.Width = 150;
      密码.DataPropertyName = "密码";
      密码.HeaderText = "密码";
      密码.Name = "密码";
      密码.Width = 150;
      COOKIE.DataPropertyName = "COOKIE";
      COOKIE.HeaderText = "COOKIE";
      COOKIE.Name = "COOKIE";
      COOKIE.Width = 400;
      状态.DataPropertyName = "状态";
      状态.HeaderText = "状态";
      状态.Name = "状态";
      状态.Width = 300;
      button1.Location = new Point(365, 11);
      button1.Name = "button1";
      button1.Size = new Size(122, 23);
      button1.TabIndex = 14;
      button1.Text = "导入账号";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      button2.Location = new Point(753, 11);
      button2.Name = "button2";
      button2.Size = new Size(122, 23);
      button2.TabIndex = 17;
      button2.Text = "导出";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(dataGridView1);
      Controls.Add(panel2);
      Controls.Add(panel1);
      Name = ("密码设置");
      Size = new Size(914, 538);
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      panel2.ResumeLayout(false);
      panel2.PerformLayout();
      ((ISupportInitialize) dataGridView1).EndInit();
      ResumeLayout(false);
    }

    public delegate void SetTextHandler(string text);

    private delegate void UpdateDataGridView(DataTable dt);
  }
}
