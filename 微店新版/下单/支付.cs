// Decompiled with JetBrains decompiler
// Type: 微店新版.下单.支付
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using MyWindowClient.DbHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace 微店新版.下单
{
  public class 支付 : UserControl
  {
    public static ChromeDriver webs;
    private bool ischeckbox = false;
    public static List<pay> paylist = new List<pay>();
    private IContainer components = null;
    private Panel panel1;
    private Button button2;
    private TextBox textBox1;
    private Button button4;
    private Panel panel2;
    private DataGridView dataGridView1;
    private TextBox textBox7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private ComboBox comboBox1;
    private LinkLabel linkLabel1;
    private Button button3;
    private DataGridViewTextBoxColumn 账号;
    private DataGridViewTextBoxColumn 订单号;
    private DataGridViewTextBoxColumn SKU;
    private DataGridViewTextBoxColumn 支付链接;
    private DataGridViewTextBoxColumn 支付状态;
    private DataGridViewTextBoxColumn 创建时间;
    private LinkLabel linkLabel2;

    public 支付() => InitializeComponent();

    public void data()
    {
      comboBox1.Items.Clear();
      DataTable remark = COMM.GETRemark();
      if (remark == null || remark.Rows.Count == 0)
      {
        log("没有备注数据，请手动刷新");
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) remark.Rows)
          comboBox1.Items.Add(row["remark"].ToString());
        log("数据刷新成功");
      }
    }

    private void log(string text)
    {
      if (textBox7.InvokeRequired)
        textBox7.Invoke(new 支付.SetTextHandler(log), text);
      else
        textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
    }

    public bool ok { get; set; }

    private void button2_Click(object sender, EventArgs e)
    {
      string cookies = textBox1.Text;
      Task.Run(() =>
      {
          ChromeDriverService defaultService = ChromeDriverService.CreateDefaultService();
          defaultService.HideCommandPromptWindow = true;
          ChromeMobileEmulationDeviceSettings deviceSettings = new ChromeMobileEmulationDeviceSettings();
          deviceSettings.Width = 320L;
          deviceSettings.Height = 800L;
          deviceSettings.PixelRatio = 1.0;
          deviceSettings.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25";
          ChromeOptions options = new ChromeOptions();
          options.AddExcludedArgument("enable-automation");
          options.AddArgument("--mute-audio");
          options.AddAdditionalCapability("useAutomationExtension", false);
          options.AddArguments("lang=zh_CN.UTF-8");
          options.AddArgument("--disable-gpu");
          options.EnableMobileEmulation(deviceSettings);
          webs = new ChromeDriver(defaultService, options);
          webs.Navigate().GoToUrl("https://mclient.alipay.com/h5pay/unifiedLogin/index.html?server_param=emlkPTI0O25kcHQ9ZDU0OTtjYz15&contextId=RZ41no9pRy6avPYgcGFPagOLO9XNFJmobileclientgw24RZ41&pageToken=&refreshNoAuth=Y");
          webs.Manage().Window.Maximize();
          webs.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000.0);
          ICookieJar cookies1 = webs.Manage().Cookies;
          string str1 = cookies;
          char[] chArray = new char[1] { ';' };
          foreach (string str2 in str1.Split(chArray))
          {
              try
              {
                  cookies1.AddCookie(new Cookie(str2.Split('=')[0].Trim(), str2.Split('=')[1].Trim()));
              }
              catch (Exception ex)
              {
              }
          }
          webs.Navigate().GoToUrl("https://openapi.alipay.com/gateway.do?charset=utf-8&method=alipay.trade.wap.pay&sign=MI3RPQmKb9Ux%2FfN47HJNWZMF9wS7z2us5NQZM%2B0hslr3Sw064iB4mIcNII%2F%2FmoHhcuVPa46iQYcAQJc%2BCXPyV8KMqmuSAI%2F%2BcJpeDEGBLitRXxAQAqoB4PPYHsatbZ%2F9tMpGG5PHYRVU1b5mKmwRmb86JpCLZcrzT2dUkkH%2FEVOF7khBSsCWsatW6td165ranZaF3ttrVZG5o2LGq2qd6q5G%2Bj8hBGracL2Ivni9Nul0fPsUnqHBWkN1ymA38vfVST2mcacvyUmWjSF3OBCIMEfiNXWS32FTalv7FyBJlxfKHYP1IEDbsPAlmCHPbtn1%2Fmrh0UW9CDp0sjBGZ2%2FvHg%3D%3D&return_url=https%3A%2F%2Ffcw-pay.weidian.com%2Fpage%2FALIPAY10107-VS.htm&notify_url=https%3A%2F%2Ffcw-pay.weidian.com%2Fserver%2FALIPAY10107-VS.htm&version=1.0&app_id=2015123101057705&sign_type=RSA2&timestamp=2023-01-31+14%3A50%3A22&alipay_sdk=alipay-sdk-java-dynamicVersionNo&format=json&biz_content=%7B%22out_trade_no%22%3A%22AL1012023013175099966729%22%2C%22total_amount%22%3A%220.86%22%2C%22subject%22%3A%22NewBalance%E6%96%B0%E7%99%BE%E4%BC%A6NB%E7%BE%8E%E4%BA%A7%E5%9B%BD...%22%2C%22business_params%22%3A%22%7B%5C%22mcCreateTradeIp%5C%22%3A%5C%2214.106.243.0%5C%22%2C%5C%22outTradeRiskInfo%5C%22%3A%5C%22%7B%5C%5C%5C%22extraMerchantId%5C%5C%5C%22%3A%5C%5C%5C%22100077222080%5C%5C%5C%22%2C%5C%5C%5C%22mcCreateTradeTime%5C%5C%5C%22%3A%5C%5C%5C%222023-01-31+14%3A50%3A22%5C%5C%5C%22%7D%5C%22%7D%22%2C%22disable_pay_channels%22%3A%22pcredit%2CpcreditpayInstallment%22%2C%22body%22%3A%22%E5%BE%AE%E5%BA%97%E8%AE%A2%E5%8D%95%EF%BC%8C%E6%89%8B%E6%9C%BA%E5%BC%80%E5%BA%97%E5%BE%88%E6%96%B9%E4%BE%BF%22%2C%22seller_id%22%3A%222088011385003005%22%7D&=%E7%AB%8B%E5%8D%B3%E6%94%AF%E4%BB%98");
          webs.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000.0);
          ok = true;
      });
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      data();
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
        BeginInvoke(new 支付.UpdateDataGridView(UpdateGV), dt);
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

    public void UPDATE(pay users)
    {
      try
      {
        BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
        {
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells["订单号"].Value.ToString() == users.orderid)
            {
              row.Cells["支付状态"].Value = users.status;
              break;
            }
          }
        }));
      }
      catch (Exception ex)
      {
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(comboBox1.Text))
      {
        log("请选择备注");
      }
      else
      {
        DataTable dataTable = MYSQL.Query("select * from wd where token='" + COMM.TOKENS + "' and remark='" + comboBox1.Text + "' and status!='Y'");
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          log("选择的备注没有待支付数据");
        }
        else
        {
                    paylist = new List<pay>();
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("账号", typeof (string)));
          dt.Columns.Add(new DataColumn("订单号", typeof (string)));
          dt.Columns.Add(new DataColumn("SKU", typeof (string)));
          dt.Columns.Add(new DataColumn("支付链接", typeof (string)));
          dt.Columns.Add(new DataColumn("支付状态", typeof (string)));
          dt.Columns.Add(new DataColumn("创建时间", typeof (string)));
          foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
          {
            DataRow row2 = dt.NewRow();
            row2["账号"] = row1["phone"].ToString();
            row2["订单号"] = row1["orderid"].ToString();
            row2["SKU"] = row1["itemid"].ToString();
            row2["支付链接"] = row1["payurl"].ToString();
            row2["支付状态"] = row1["status"].ToString() == "N" ? "待支付" : (object) "二次支付";
            row2["创建时间"] = row1["times"].ToString();
            dt.Rows.Add(row2);
                        paylist.Add(new pay()
            {
              id = row1["id"].ToString(),
              phone = row1["phone"].ToString(),
              cookie = row1["cookie"].ToString(),
              orderid = row1["orderid"].ToString(),
              payurl = row1["payurl"].ToString(),
              itemid = row1["itemid"].ToString(),
              status = row1["status"].ToString(),
              times = row1["times"].ToString()
            });
          }
          if (dt == null || dt.Rows.Count <= 0)
            return;
          log("支付数据获取成功");
          UpdateGV(dt);
        }
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (paylist == null || paylist.Count == 0)
        log("请先查询数据");
      else if (!ok)
        log("请输入支付宝cookie后点击打开网页");
      else if (string.IsNullOrWhiteSpace(COMM.paypass))
        log("请输入支付密码");
      else if (string.IsNullOrWhiteSpace(COMM.card))
      {
        log("请输入身份证后6位");
      }
      else
      {
        DataTable dataSource = (DataTable) dataGridView1.DataSource;
        if (dataSource == null || dataSource.Rows.Count == 0)
        {
          log("无数据");
        }
        else
        {
          List<pay> parameter = new List<pay>();
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            DataGridViewRow item = row;
            if (item.Cells[0].Value != null && (bool) item.Cells[0].Value && !dictionary.ContainsKey(item.Cells["订单号"].Value.ToString()))
            {
              List<pay> list = paylist.Where<pay>(p => p.orderid == item.Cells["订单号"].Value.ToString()).ToList<pay>();
              if (list == null || list.Count == 0)
              {
                log(item.Cells["订单号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
              }
              else
              {
                dictionary.Add(list.First<pay>().id, list.First<pay>().payurl);
                parameter.Add(list.First<pay>());
              }
            }
          }
          if (parameter == null || parameter.Count == 0)
            log("请勾选需要支付的数据");
          else
            new Thread(new ParameterizedThreadStart(start)).Start(parameter);
        }
      }
    }

    private void start(object obj)
    {
      List<pay> payList = (List<pay>) obj;
      try
      {
        foreach (pay users in payList)
        {
          string id = users.id;
          string payurl = users.payurl;
          try
          {
            if (payurl.Contains("https://qr.alipay.com"))
            {
              users.status = "支付链接错误，请复制链接手动支付";
              log("支付链接错误，请复制链接手动支付");
              UPDATE(users);
              MYSQL.exp("delete from wd where id=" + id);
            }
            else
            {
                            webs.Navigate().GoToUrl(payurl);
                            webs.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000.0);
              Thread.Sleep(600);
                            webs.FindElement(By.ClassName("h5RouteAppSenior__h5pay")).Click();
              Thread.Sleep(600);
              if (webs.PageSource.Contains("订单已付款成功，请勿重复提交"))
              {
                users.status = "订单已支付";
                log("订单已支付");
                UPDATE(users);
                MYSQL.exp("update wd set status='Y' where id=" + id);
              }
              else if (webs.PageSource.Contains("抱歉，订单已关闭"))
              {
                users.status = "订单已关闭";
                log("订单已关闭");
                UPDATE(users);
                MYSQL.exp("delete from wd where id=" + id);
              }
              else if (webs.PageSource.Contains("和支付宝当前登录账号不一致"))
              {
                users.status = "和支付宝当前登录账号不一致";
                log("和支付宝当前登录账号不一致");
                UPDATE(users);
                MYSQL.exp("delete from wd where id=" + id);
              }
              else
              {
                                webs.FindElement(By.ClassName("cashierPreConfirm__btn")).Click();
                Thread.Sleep(600);
                                webs.FindElement(By.ClassName("length-6")).SendKeys(COMM.paypass);
                int num = 0;
                bool flag = false;
                while (!webs.PageSource.Contains("支付成功"))
                {
                  try
                  {
                    Thread.Sleep(800);
                    if (!flag && webs.PageSource.Contains("为确保是你本人操作，请回答安全保护问题"))
                    {
                      flag = true;
                                            webs.FindElement(By.ClassName("adm-input-element")).SendKeys(COMM.card);
                      Thread.Sleep(600);
                                            webs.FindElement(By.ClassName("adm-button-shape-default")).Click();
                      Thread.Sleep(600);
                    }
                    ++num;
                    log(string.Format("第{0}次检测支付状态", num));
                    users.status = string.Format("第{0}次检测支付状态", num);
                    UPDATE(users);
                    if (num > 20)
                    {
                      log("检测支付状态超时，将由二次支付程序继续发起支付");
                      users.status = "支付超时，可以在本轮支付完成后点击获取数据继续支付";
                      UPDATE(users);
                      break;
                    }
                  }
                  catch (Exception ex)
                  {
                    ++num;
                  }
                }
                if (webs.PageSource.Contains("支付成功"))
                {
                  log("支付成功");
                  users.status = "支付成功";
                  UPDATE(users);
                  MYSQL.exp("update wd set status='Y' where id=" + id);
                }
                else
                {
                  log("支付失败");
                  users.status = "支付失败";
                  UPDATE(users);
                  MYSQL.exp("update wd set status='S' where id=" + id);
                }
              }
            }
          }
          catch (Exception ex)
          {
            log("支付异常");
            users.status = "支付异常";
            UPDATE(users);
            MYSQL.exp("update wd set status='S' where id=" + id);
          }
        }
        log("选择的数据已全部支付完毕");
      }
      catch (Exception ex)
      {
        log("程序异常");
        Thread.Sleep(600);
      }
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string text = comboBox1.Text;
      if (string.IsNullOrWhiteSpace(text))
      {
        log("请选择备注");
      }
      else
      {
        MYSQL.exp("delete from wd where token='" + COMM.TOKENS + "' and remark='" + text + "'");
        data();
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
      button3 = new Button();
      linkLabel1 = new LinkLabel();
      comboBox1 = new ComboBox();
      button2 = new Button();
      textBox1 = new TextBox();
      button4 = new Button();
      panel2 = new Panel();
      textBox7 = new TextBox();
      dataGridView1 = new DataGridView();
      账号 = new DataGridViewTextBoxColumn();
      订单号 = new DataGridViewTextBoxColumn();
      SKU = new DataGridViewTextBoxColumn();
      支付链接 = new DataGridViewTextBoxColumn();
      支付状态 = new DataGridViewTextBoxColumn();
      创建时间 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      linkLabel2 = new LinkLabel();
      panel1.SuspendLayout();
      panel2.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      SuspendLayout();
      panel1.Controls.Add(linkLabel2);
      panel1.Controls.Add(button3);
      panel1.Controls.Add(linkLabel1);
      panel1.Controls.Add(comboBox1);
      panel1.Controls.Add(button2);
      panel1.Controls.Add(textBox1);
      panel1.Controls.Add(button4);
      panel1.Dock = DockStyle.Top;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(914, 48);
      panel1.TabIndex = 0;
      button3.Location = new Point(613, 12);
      button3.Name = "button3";
      button3.Size = new Size(75, 23);
      button3.TabIndex = 28;
      button3.Text = "开始支付";
      button3.UseVisualStyleBackColor = true;
      button3.Click += new EventHandler(button3_Click);
      linkLabel1.AutoSize = true;
      linkLabel1.Location = new Point(694, 17);
      linkLabel1.Name = "linkLabel1";
      linkLabel1.Size = new Size(53, 12);
      linkLabel1.TabIndex = 27;
      linkLabel1.TabStop = true;
      linkLabel1.Text = "刷新备注";
      linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
      comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox1.FormattingEnabled = true;
      comboBox1.Location = new Point(390, 13);
      comboBox1.Name = "comboBox1";
      comboBox1.Size = new Size(138, 20);
      comboBox1.TabIndex = 26;
      button2.Location = new Point(13, 12);
      button2.Name = "button2";
      button2.Size = new Size(123, 23);
      button2.TabIndex = 22;
      button2.Text = "打开网页";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      textBox1.Location = new Point(141, 13);
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(244, 21);
      textBox1.TabIndex = 23;
      textBox1.Text = "支付宝Cookie";
      button4.Location = new Point(533, 12);
      button4.Name = "button4";
      button4.Size = new Size(75, 23);
      button4.TabIndex = 24;
      button4.Text = "查询数据";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      panel2.Controls.Add(textBox7);
      panel2.Dock = DockStyle.Top;
      panel2.Location = new Point(0, 48);
      panel2.Name = "panel2";
      panel2.Size = new Size(914, 142);
      panel2.TabIndex = 1;
      textBox7.BackColor = SystemColors.ActiveCaptionText;
      textBox7.Dock = DockStyle.Fill;
      textBox7.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
      textBox7.ForeColor = Color.Maroon;
      textBox7.Location = new Point(0, 0);
      textBox7.Multiline = true;
      textBox7.Name = "textBox7";
      textBox7.ReadOnly = true;
      textBox7.ScrollBars = ScrollBars.Vertical;
      textBox7.Size = new Size(914, 142);
      textBox7.TabIndex = 8;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(账号, 订单号, SKU, 支付链接, 支付状态, 创建时间);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(0, 190);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(914, 348);
      dataGridView1.TabIndex = 7;
      账号.DataPropertyName = "账号";
      账号.HeaderText = "账号";
      账号.Name = "账号";
      订单号.DataPropertyName = "订单号";
      订单号.HeaderText = "订单号";
      订单号.Name = "订单号";
      SKU.DataPropertyName = "SKU";
      SKU.HeaderText = "SKU";
      SKU.Name = "SKU";
      SKU.Width = 150;
      支付链接.DataPropertyName = "支付链接";
      支付链接.HeaderText = "支付链接";
      支付链接.Name = "支付链接";
      支付链接.Width = 200;
      支付状态.DataPropertyName = "支付状态";
      支付状态.HeaderText = "支付状态";
      支付状态.Name = "支付状态";
      支付状态.Width = 180;
      创建时间.DataPropertyName = "创建时间";
      创建时间.HeaderText = "创建时间";
      创建时间.Name = "创建时间";
      dataGridViewTextBoxColumn1.DataPropertyName = "账号";
      dataGridViewTextBoxColumn1.HeaderText = "账号";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn2.DataPropertyName = "COOKIE";
      dataGridViewTextBoxColumn2.HeaderText = "COOKIE";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn3.DataPropertyName = "订单号";
      dataGridViewTextBoxColumn3.HeaderText = "订单号";
      dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      dataGridViewTextBoxColumn4.DataPropertyName = "SKU";
      dataGridViewTextBoxColumn4.HeaderText = "SKU";
      dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      dataGridViewTextBoxColumn4.Width = 150;
      dataGridViewTextBoxColumn5.DataPropertyName = "支付链接";
      dataGridViewTextBoxColumn5.HeaderText = "支付链接";
      dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      dataGridViewTextBoxColumn5.Width = 200;
      dataGridViewTextBoxColumn6.DataPropertyName = "支付状态";
      dataGridViewTextBoxColumn6.HeaderText = "支付状态";
      dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      dataGridViewTextBoxColumn6.Width = 180;
      dataGridViewTextBoxColumn7.DataPropertyName = "支付时间";
      dataGridViewTextBoxColumn7.HeaderText = "支付时间";
      dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      linkLabel2.AutoSize = true;
      linkLabel2.Location = new Point(753, 17);
      linkLabel2.Name = "linkLabel2";
      linkLabel2.Size = new Size(53, 12);
      linkLabel2.TabIndex = 29;
      linkLabel2.TabStop = true;
      linkLabel2.Text = "删除备注";
      linkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(dataGridView1);
      Controls.Add(panel2);
      Controls.Add(panel1);
      Name = ("支付");
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
