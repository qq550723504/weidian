// Decompiled with JetBrains decompiler
// Type: 微店新版.下单.评论
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


namespace 微店新版.下单
{
  public class 评论 : UserControl
  {
    private bool ischeckbox = false;
    public static List<pay> paylist = new List<pay>();
    public static Dictionary<string, pay> paydic = new Dictionary<string, pay>();
    public static List<string> plist = new List<string>();
    private MyHttp myhttp = new MyHttp();
    public static ConcurrentDictionary<string, List<string>> imgdic = new ConcurrentDictionary<string, List<string>>();
    private IContainer components = null;
    private Panel panel1;
    private Panel panel2;
    private TextBox textBox7;
    private LinkLabel linkLabel1;
    private ComboBox comboBox1;
    private Button button4;
    private LinkLabel linkLabel2;
    private CheckBox checkBox1;
    private DataGridView dataGridView1;
    private Button button3;
    private Button button2;
    private Button button1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private TextBox textBox10;
    private Label label10;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem 免登录打开ToolStripMenuItem;
    private Button button5;
    private DataGridViewTextBoxColumn 编号;
    private DataGridViewTextBoxColumn 账号;
    private DataGridViewTextBoxColumn COOKIE;
    private DataGridViewTextBoxColumn 订单号;
    private DataGridViewTextBoxColumn SKU;
    private DataGridViewTextBoxColumn 商品名称;
    private DataGridViewTextBoxColumn 商品型号;
    private DataGridViewTextBoxColumn 账号状态;
    private DataGridViewTextBoxColumn 评论状态;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    private Button button6;
    private DateTimePicker dateTimePicker1;
    private Button button7;
    private TextBox textBox4;
    private Label label3;
    private TextBox textBox5;
    private Button button8;

    public 评论()
    {
      InitializeComponent();
      if (Program.users["open"].ToString() != "Y")
        dataGridView1.CellMouseDown -= new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
      COMM.isnm = 1;
    }

    public void data()
    {
      comboBox1.Items.Clear();
      DataTable dataTable = COMM.orders();
      if (dataTable == null || dataTable.Rows.Count == 0)
      {
        log("没有备注数据，请手动刷新");
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          comboBox1.Items.Add(row["remark"].ToString());
        log("数据刷新成功");
      }
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
        BeginInvoke(new 评论.UpdateDataGridView(UpdateGV), dt);
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
            if (row.Cells["编号"].Value.ToString() == users.id)
            {
              row.Cells["账号状态"].Value = users.status;
              row.Cells["评论状态"].Value = users.status1;
              row.Cells["商品名称"].Value = users.pdname;
              row.Cells["商品型号"].Value = users.pdtype;
              break;
            }
          }
        }));
      }
      catch (Exception ex)
      {
      }
    }

    private void log(string text)
    {
      if (textBox7.InvokeRequired)
        textBox7.Invoke(new 评论.SetTextHandler(log), text);
      else
        textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      data();
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
        MYSQL.exp("delete from orders where token='" + COMM.TOKENS + "' and remark='" + text + "'");
        data();
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
        DataTable dataTable = MYSQL.Query("select * from orders where token='" + COMM.TOKENS + "' and remark='" + comboBox1.Text + "' and status='Y'");
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          log("选择的备注没有需要评论的数据");
        }
        else
        {
                    paylist = new List<pay>();
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("编号", typeof (string)));
          dt.Columns.Add(new DataColumn("账号", typeof (string)));
          dt.Columns.Add(new DataColumn("COOKIE", typeof (string)));
          dt.Columns.Add(new DataColumn("订单号", typeof (string)));
          dt.Columns.Add(new DataColumn("SKU", typeof (string)));
          dt.Columns.Add(new DataColumn("账号状态", typeof (string)));
          dt.Columns.Add(new DataColumn("评论状态", typeof (string)));
          foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
          {
            foreach (string str in row1["itemid"].ToString().Split(',').ToList<string>())
            {
              string key = Guid.NewGuid().ToString("N");
              DataRow row2 = dt.NewRow();
              row2["编号"] = key;
              row2["账号"] = row1["phone"].ToString();
              row2["COOKIE"] = row1["cookie"].ToString();
              row2["订单号"] = row1["orderid"].ToString();
              row2["SKU"] = str;
              row2["账号状态"] = "";
              row2["评论状态"] = "";
              dt.Rows.Add(row2);
              pay pay = new pay()
              {
                id = key,
                phone = row1["phone"].ToString(),
                cookie = row1["cookie"].ToString(),
                orderid = row1["orderid"].ToString(),
                itemid = str
              };
                            paylist.Add(pay);
                            paydic.Add(key, pay);
            }
          }
          if (dt == null || dt.Rows.Count <= 0)
            return;
          log("评论数据获取成功");
          UpdateGV(dt);
        }
      }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      COMM.isnm = checkBox1.Checked ? 0 : 1;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "文本文档|*.txt";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      string path = openFileDialog.FileName.ToString();
            plist = new List<string>();
      foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
      {
        if (!string.IsNullOrEmpty(readAllLine))
                    plist.Add(readAllLine);
      }
      log("导入评论" + plist.Count.ToString() + "条");
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (paylist == null || paylist.Count == 0)
        log("请先获取账号");
      else if (string.IsNullOrWhiteSpace(textBox10.Text))
      {
        log("请输入线程数量");
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
          List<pay> payList = new List<pay>();
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells[0].Value != null && (bool) row.Cells[0].Value)
            {
              pay pay = paydic[row.Cells["编号"].Value.ToString()];
              payList.Add(pay);
            }
          }
          if (payList == null || payList.Count == 0)
          {
            log("请勾选需要评论的数据");
          }
          else
          {
            COMM.isnm = checkBox1.Checked ? 0 : 1;
            int int32 = Convert.ToInt32(textBox10.Text);
            if (payList.Count < 10)
            {
              new Thread(new ParameterizedThreadStart(start)).Start(payList);
            }
            else
            {
              int num = payList.Count / int32;
              foreach (KeyValuePair<string, List<pay>> split in COMM.SplitList(payList, num))
                new Thread(new ParameterizedThreadStart(start)).Start(split.Value);
            }
          }
        }
      }
    }

    private async void start(object obj)
    {
      List<pay> list = (List<pay>) obj;
      foreach (pay ims in list)
      {
        pay item = ims;
        DataRow rows = COMM.getauth(item.cookie);
        try
        {
          if (item != null)
          {
            if (!string.IsNullOrWhiteSpace(item.cookie) && !string.IsNullOrWhiteSpace(item.orderid) && !string.IsNullOrWhiteSpace(item.itemid) && !string.IsNullOrWhiteSpace(item.phone))
            {
              string wdtoken = Guid.NewGuid().ToString().ToLower().Substring(8);
              string cookie = "__spider__visitorid=4d5547d79a944a2a;wdtoken=a1af204d;__spider__sessionid=9031d5a03d294b51;Hm_lvt_f3b91484e26c0d850ada494bff4b469b=1711979537;Hm_lpvt_f3b91484e26c0d850ada494bff4b469b=1711979537;login_token=" + rows["loginToken"].ToString() + ";is_login=true;login_type=LOGIN_USER_TYPE_MASTER;login_source=LOGIN_USER_SOURCE_MASTER;uid=" + rows["uid"].ToString() + ";duid=" + rows["duid"].ToString() + ";sid=" + rows["sid"].ToString();
              item.status = "开始评论";
              string str = string.Empty;
              if (plist != null && plist.Count > 0)
                str = plist[new Random().Next(plist.Count)];
              string imgurl = string.Empty;
              if (imgdic != null && imgdic.Count > 0 && imgdic.ContainsKey(item.id))
              {
                List<string> lists = new List<string>();
                                imgdic.TryRemove(item.id, out lists);
                foreach (string url in lists)
                {
                  string img = path(url, cookie);
                  imgurl = imgurl + "\"" + img + "\",";
                  img = null;
                }
                lists = null;
              }
              if (!string.IsNullOrWhiteSpace(imgurl))
                imgurl = imgurl.Substring(0, imgurl.Length - 1);
              Dictionary<string, string> dic = new Dictionary<string, string>();
              if (!string.IsNullOrWhiteSpace(imgurl))
                dic.Add("param", "{\"orderID\":\"" + item.orderid + "\",\"itemId\":" + item.itemid + ",\"imgList\":[" + imgurl + "],\"score\":5,\"comment\":\"" + str + "\",\"isShow\":" + COMM.isnm.ToString() + ",\"tagIds\":[]}");
              else
                dic.Add("param", "{\"orderID\":\"" + item.orderid + "\",\"itemId\":" + item.itemid + ",\"imgList\":[],\"score\":5,\"comment\":\"" + str + "\",\"isShow\":" + COMM.isnm.ToString() + ",\"tagIds\":[]}");
              dic.Add("wdtoken", wdtoken);
              string res = await myhttp.POST222("https://thor.weidian.com/wdcomment/addComment/1.0", cookie, dic);
              if (!string.IsNullOrWhiteSpace(res) && res.Contains("成功"))
              {
                item.status1 = "评论成功";
                log(item.phone + "，商品ID：" + item.itemid + "，评论成功，" + str);
                UPDATE(item);
              }
              else
              {
                item.status1 = res;
                log(item.phone + "，商品ID：" + item.itemid + "，评论失败，" + res);
                UPDATE(item);
              }
              wdtoken = null;
              cookie = null;
              str = null;
              imgurl = null;
              dic = null;
              res = null;
            }
            else
              continue;
          }
          else
            continue;
        }
        catch (Exception ex1)
        {
          Exception ex = ex1;
          item.status1 = "评论异常";
          log(item.phone + "，商品ID：" + item.itemid + "，评论异常");
          UPDATE(item);
        }
        item = null;
        rows = null;
      }
      list = null;
    }

    public static string path(string path, string cookie)
    {
      string empty = string.Empty;
      try
      {
        string json = flurl.DoPostFile("https://vimg.weidian.com/upload/v3/direct?scope=h5user&fileType=image", path, cookie);
        if (!string.IsNullOrWhiteSpace(json))
        {
          if (json.Contains("SUCCESS"))
            empty = COMM.GetToJsonList(json)["result"]["url"].ToString();
        }
      }
      catch (Exception ex)
      {
      }
      return empty;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (paylist == null || paylist.Count == 0)
        log("请先获取账号");
      else if (plist == null || plist.Count == 0)
        log("请导入评论");
      else if (string.IsNullOrWhiteSpace(COMM.cookie))
        log("请登录主店铺");
      else if (string.IsNullOrWhiteSpace(textBox10.Text))
      {
        log("请输入线程数量");
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
          List<pay> payList = new List<pay>();
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            DataGridViewRow item = row;
            if (item.Cells[0].Value != null && (bool) item.Cells[0].Value && !dictionary.ContainsKey(item.Cells["编号"].Value.ToString()))
            {
              List<pay> list = paylist.Where<pay>(p => p.id == item.Cells["编号"].Value.ToString()).ToList<pay>();
              if (list == null || list.Count == 0)
              {
                log(item.Cells["编号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
              }
              else
              {
                dictionary.Add(list.First<pay>().id, list.First<pay>().payurl);
                payList.Add(list.First<pay>());
              }
            }
          }
          if (payList == null || payList.Count == 0)
          {
            log("请勾选需要评论的数据");
          }
          else
          {
            COMM.isnm = checkBox1.Checked ? 0 : 1;
            int int32 = Convert.ToInt32(textBox10.Text);
            if (payList.Count < 10)
            {
              new Thread(new ParameterizedThreadStart(start_huitou)).Start(payList);
            }
            else
            {
              int num = payList.Count / int32;
              foreach (KeyValuePair<string, List<pay>> split in COMM.SplitList(payList, num))
                new Thread(new ParameterizedThreadStart(start_huitou)).Start(split.Value);
            }
          }
        }
      }
    }

    private async void start_huitou(object obj)
    {
      List<pay> list = (List<pay>) obj;
      foreach (pay ims in list)
      {
        pay item = ims;
        try
        {
          if (item == null)
          {
            list = null;
            return;
          }
          if (!string.IsNullOrWhiteSpace(item.cookie) && !string.IsNullOrWhiteSpace(item.phone))
          {
            DataRow rows = COMM.getauth(item.cookie);
            string cookie = "__spider__sessionid=" + COMM.r(16) + "; __spider__visitorid=" + COMM.r(16) + "; duid=" + rows["duid"].ToString() + "; is_login=true; login_source=LOGIN_USER_SOURCE_MASTER; login_token=" + rows["loginToken"].ToString() + "; login_type=LOGIN_USER_TYPE_MASTER; sid=" + rows["sid"].ToString() + "; uid=" + rows["uid"].ToString() + "; v-components/tencent-live-plugin@wfr=c_wxh5; wdtoken=8f2d1224";
            string imgurl = string.Empty;
            if (imgdic != null && imgdic.Count > 0 && imgdic.ContainsKey(item.id))
            {
              List<string> lists = new List<string>();
                            imgdic.TryRemove(item.id, out lists);
              foreach (string url in lists)
              {
                string img = path(url, cookie);
                imgurl = imgurl + "\"" + img + "\",";
                img = null;
              }
              lists = null;
            }
            item.status = "开始筛选账号";
            HttpRequestEntity pl = await MYHTTP.Result("GET", "https://thor.weidian.com/ares/huitouke.listBuyItems/1.0?param=%7B%22shopId%22%3A%22" + COMM.sid(COMM.cookie) + "%22%7D&wdtoken=" + COMM.tokens(cookie) + "&v=1.0&timestamp=" + COMM.ConvertDateTimeToInt(DateTime.Now).ToString(), 6, null, cookie);
            if (pl != null && !string.IsNullOrWhiteSpace(pl.ResponseContent) && pl.ResponseContent.Contains("成功"))
            {
              Model.MD model = JsonConvert.DeserializeObject<Model.MD>(pl.ResponseContent);
              if (model != null && model.status != null && model.status.message == "OK" && model.result != null && model.result.shopInfo != null)
              {
                if (model.result.shopInfo.shopDesc == null)
                {
                  item.status = "不是回头客";
                  log(item.phone + item.status);
                  UPDATE(item);
                  continue;
                }
                string num = model.result.shopInfo.shopDesc.Replace("你在这家店买过", "").Replace("次啦", "");
                if (Convert.ToInt32(num) >= 2)
                {
                  item.status = "回头客";
                  log(item.phone + item.status);
                  UPDATE(item);
                  string str = plist[new Random().Next(plist.Count)];
                  Dictionary<string, string> dic = new Dictionary<string, string>();
                  if (!string.IsNullOrWhiteSpace(imgurl))
                    dic.Add("param", "{\"shopId\":\"" + COMM.sid(COMM.cookie) + "\",\"text\":\"" + str + "\",\"imgList\":[" + imgurl + "],\"itemIdList\":[" + item.itemid + "]}");
                  else
                    dic.Add("param", "{\"shopId\":\"" + COMM.sid(COMM.cookie) + "\",\"text\":\"" + str + "\",\"imgList\":[],\"itemIdList\":[" + item.itemid + "]}");
                  dic.Add("wdtoken", COMM.tokens(cookie));
                  dic.Add("v", "1.0");
                  dic.Add("timestamp", COMM.ConvertDateTimeToInt(DateTime.Now).ToString());
                  string res = await myhttp.POST("https://thor.weidian.com/ares/huitouke.addSaying/1.1", cookie, dic);
                  if (!string.IsNullOrWhiteSpace(res) && res.Contains("成功"))
                  {
                    item.status1 = "回头评论成功";
                    log(item.phone + item.status);
                    UPDATE(item);
                    try
                    {
                      string ID = COMM.GetToJsonList(res)["result"]["sayingId"].ToString();
                      HttpRequestEntity httpRequestEntity = await MYHTTP.Result("GET", "https://thor.weidian.com/ares/familiar.recShop/1.0?param=%7B%22sayingId%22%3A" + ID + "%2C%22recAction%22%3A1%7D&wdtoken=" + COMM.tokens(cookie) + "&v=1.0&timestamp=" + COMM.ConvertDateTimeToInt(DateTime.Now).ToString(), 6, null, cookie);
                      ID = null;
                    }
                    catch (Exception ex)
                    {
                    }
                  }
                  else
                  {
                    item.status1 = res;
                    log(item.phone + item.status);
                    UPDATE(item);
                  }
                  str = null;
                  dic = null;
                  res = null;
                }
                else
                {
                  item.status = "不是回头客";
                  log(item.phone + item.status);
                  UPDATE(item);
                }
                num = null;
              }
              model = null;
            }
            else
            {
              if (pl != null && !string.IsNullOrWhiteSpace(pl.ResponseContent) && pl.ResponseContent.Contains("需要您登录微店账号"))
              {
                item.status = "账号已封禁";
                item.status1 = "账号已封禁";
              }
              else if (pl != null && !string.IsNullOrWhiteSpace(pl.ResponseContent))
              {
                item.status = pl.ResponseContent;
                item.status1 = pl.ResponseContent;
              }
              if (pl == null || string.IsNullOrWhiteSpace(pl.ResponseContent))
              {
                item.status = "筛选失败";
                item.status1 = "筛选失败";
              }
              log(item.phone + item.status);
              UPDATE(item);
            }
            rows = null;
            cookie = null;
            imgurl = null;
            pl = null;
          }
          else
            continue;
        }
        catch (Exception ex1)
        {
          Exception ex = ex1;
          item.status = "异常";
          item.status1 = "异常";
          log(item.phone + item.status);
          UPDATE(item);
        }
        item = null;
      }
      list = null;
    }

    private void 免登录打开ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (dataGridView1 == null || dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
          return;
        int index = dataGridView1.CurrentRow.Index;
        pay auth = paylist[index];
        if (auth == null)
        {
          log("账号不存在");
        }
        else
        {
          DataRow dataRow = COMM.getauth(auth.cookie);
          string cookie = "wdtoken=" + Guid.NewGuid().ToString().ToLower().Substring(8) + ";__spider__visitorid=" + COMM.r(16) + ";__spider__sessionid=" + COMM.r(16) + ";v-components/cpn-coupon-dialog@iwdDefault=1;login_token=" + dataRow["loginToken"].ToString() + ";uid=" + dataRow["uid"].ToString() + ";is_login=true;login_type=LOGIN_USER_TYPE_WECHAT;login_source=LOGIN_USER_SOURCE_WECHAT;duid=" + dataRow["duid"].ToString() + ";sid=" + dataRow["sid"].ToString();
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
                options.AddArgument("user-agent=" + COMM.getagent());
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
                    log(auth.phone + "免登录打开成功");
                }
                catch (Exception ex)
                {
                }
            });
        }
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

    private void button5_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      folderBrowserDialog.Description = "请选择图片文件路径";
      folderBrowserDialog.ShowNewFolderButton = false;
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
            imgdic = new ConcurrentDictionary<string, List<string>>();
      foreach (FileInfo file in new DirectoryInfo(folderBrowserDialog.SelectedPath).GetFiles())
      {
        string lower = Path.GetExtension(file.Name).ToLower();
        if (lower == ".jpg" || lower == ".jpeg" || lower == ".png" || lower == ".bmp" || lower == ".gif")
        {
          try
          {
            string fullName = file.FullName;
            string name = file.Name;
            if (name.Length == 32)
                            imgdic.TryAdd(name, new List<string>()
              {
                fullName
              });
            else if (name.Length > 32)
            {
              string key = name.Substring(0, 32);
              if (imgdic.ContainsKey(key))
                                imgdic[key].Add(fullName);
              else
                                imgdic.TryAdd(key, new List<string>()
                {
                  fullName
                });
            }
          }
          catch (IOException ex)
          {
          }
        }
      }
      log(string.Format("导入图片{0}条", imgdic.Count));
    }

    private void button6_Click(object sender, EventArgs e)
    {
      if (paylist == null || paylist.Count == 0)
      {
        log("请先获取账号");
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
            if (item.Cells[0].Value != null && (bool) item.Cells[0].Value && !dictionary.ContainsKey(item.Cells["编号"].Value.ToString()))
            {
              List<pay> list = paylist.Where<pay>(p => p.id == item.Cells["编号"].Value.ToString()).ToList<pay>();
              if (list == null || list.Count == 0)
              {
                log(item.Cells["编号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
              }
              else
              {
                dictionary.Add(list.First<pay>().id, list.First<pay>().payurl);
                parameter.Add(list.First<pay>());
              }
            }
          }
          if (parameter == null || parameter.Count == 0)
            log("请勾选需要评论的数据");
          else
            new Thread(new ParameterizedThreadStart(query)).Start(parameter);
        }
      }
    }

    private async void query(object obj)
    {
      List<pay> list = (List<pay>) obj;
      Dictionary<string, string> orderlist = new Dictionary<string, string>();
      foreach (pay item in list)
      {
        if (!orderlist.ContainsKey(item.orderid))
          orderlist.Add(item.orderid, item.cookie);
      }
      foreach (KeyValuePair<string, string> keyValuePair in orderlist)
      {
        KeyValuePair<string, string> item = keyValuePair;
        DataRow rows = COMM.getauth(item.Value);
        string cookie = "__spider__sessionid=" + COMM.r(16) + "; __spider__visitorid=" + COMM.r(16) + "; duid=" + rows["duid"].ToString() + "; is_login=true; login_source=LOGIN_USER_SOURCE_MASTER; login_token=" + rows["loginToken"].ToString() + "; login_type=LOGIN_USER_TYPE_MASTER; sid=" + rows["sid"].ToString() + "; uid=" + rows["uid"].ToString() + "; v-components/tencent-live-plugin@wfr=c_wxh5; wdtoken=8f2d1224";
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("param", "{\"orderId\":\"" + item.Key + "\",\"refundType\":101,\"roleType\":1,\"vSellerId\":\"\"}");
        HttpRequestEntity pl = await MYHTTP.Result("POST", "https://thor.weidian.com/refundplatform/couldRefundListByOrderId/1.0", 88, dic, cookie);
        if (pl != null && !string.IsNullOrWhiteSpace(pl.ResponseContent))
        {
          JObject model = COMM.GetToJsonList(pl.ResponseContent);
          foreach (JToken tokens in model["result"]["couldRefundSubOrderInfos"])
          {
            string itemid = tokens["itemId"].ToString();
            string itemName = tokens["itemName"].ToString();
            string skuName = tokens["skuName"].ToString();
            List<pay> paylist = list.Where<pay>(p => p.orderid == item.Key && p.itemid == itemid).ToList<pay>();
            if (paylist != null && paylist.Count > 0)
            {
              foreach (pay sku in paylist)
              {
                sku.pdname = itemName;
                sku.pdtype = skuName;
                UPDATE(sku);
              }
            }
            itemName = null;
            skuName = null;
            paylist = null;
          }
          model = null;
        }
        else
          log("查看商品型号错误");
        rows = null;
        cookie = null;
        dic = null;
        pl = null;
      }
      list = null;
      orderlist = null;
    }

    private void button7_Click(object sender, EventArgs e)
    {
      if (paylist == null || paylist.Count == 0)
        log("请先获取账号");
      else if (string.IsNullOrWhiteSpace(textBox10.Text))
      {
        log("请输入线程数量");
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
          List<pay> payList = new List<pay>();
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            DataGridViewRow item = row;
            if (item.Cells[0].Value != null && (bool) item.Cells[0].Value && !dictionary.ContainsKey(item.Cells["编号"].Value.ToString()))
            {
              List<pay> list = paylist.Where<pay>(p => p.id == item.Cells["编号"].Value.ToString()).ToList<pay>();
              if (list == null || list.Count == 0)
              {
                log(item.Cells["编号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
              }
              else
              {
                dictionary.Add(list.First<pay>().id, list.First<pay>().payurl);
                payList.Add(list.First<pay>());
              }
            }
          }
          if (payList == null || payList.Count == 0)
          {
            log("请勾选需要评论的数据");
          }
          else
          {
            DateTime dateTime = dateTimePicker1.Value;
            string str1 = dateTime.ToString();
            dateTime = Convert.ToDateTime(str1);
            string str2 = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (MessageBox.Show("定时评论在" + str1 + "时自动执行，请确认！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
              return;
            string str3 = "";
            while (true)
            {
              Thread.Sleep(1);
              Application.DoEvents();
              if (!str3.Contains("等于") && !str3.Contains("小于"))
              {
                string dateStr1 = str2;
                dateTime = DateTime.Now;
                string dateStr2 = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                ref string local = ref str3;
                COMM.CompanyDate(dateStr1, dateStr2, ref local);
              }
              else
                break;
            }
            int int32 = Convert.ToInt32(textBox10.Text);
            if (payList.Count < 10)
            {
              new Thread(new ParameterizedThreadStart(start)).Start(payList);
            }
            else
            {
              int num = payList.Count / int32;
              foreach (KeyValuePair<string, List<pay>> split in COMM.SplitList(payList, num))
                new Thread(new ParameterizedThreadStart(start)).Start(split.Value);
            }
          }
        }
      }
    }

    private void button8_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(textBox4.Text))
        log("请输入账号区间值");
      else if (string.IsNullOrWhiteSpace(textBox5.Text))
      {
        log("请输入账号区间值");
      }
      else
      {
        DataTable dataTable = MYSQL.Query("select * from orders where token='" + COMM.TOKENS + "' and remark='" + comboBox1.Text + "' and status='Y'");
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          log("选择的备注没有需要评论的数据");
        }
        else
        {
                    paylist = new List<pay>();
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("编号", typeof (string)));
          dt.Columns.Add(new DataColumn("账号", typeof (string)));
          dt.Columns.Add(new DataColumn("COOKIE", typeof (string)));
          dt.Columns.Add(new DataColumn("订单号", typeof (string)));
          dt.Columns.Add(new DataColumn("SKU", typeof (string)));
          dt.Columns.Add(new DataColumn("账号状态", typeof (string)));
          dt.Columns.Add(new DataColumn("评论状态", typeof (string)));
          int int32_1 = Convert.ToInt32(textBox4.Text);
          int int32_2 = Convert.ToInt32(textBox5.Text);
          for (int index = int32_1; index < int32_2; ++index)
          {
            foreach (string str in dataTable.Rows[index]["itemid"].ToString().Split(',').ToList<string>())
            {
              string key = Guid.NewGuid().ToString("N");
              DataRow row = dt.NewRow();
              row["编号"] = key;
              row["账号"] = dataTable.Rows[index]["phone"].ToString();
              row["COOKIE"] = dataTable.Rows[index]["cookie"].ToString();
              row["订单号"] = dataTable.Rows[index]["orderid"].ToString();
              row["SKU"] = str;
              row["账号状态"] = "";
              row["评论状态"] = "";
              dt.Rows.Add(row);
              pay pay = new pay()
              {
                id = key,
                phone = dataTable.Rows[index]["phone"].ToString(),
                cookie = dataTable.Rows[index]["cookie"].ToString(),
                orderid = dataTable.Rows[index]["orderid"].ToString(),
                itemid = str
              };
                            paylist.Add(pay);
                            paydic.Add(key, pay);
            }
          }
          if (dt == null || dt.Rows.Count <= 0)
            return;
          log("评论数据获取成功");
          UpdateGV(dt);
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
      components = new System.ComponentModel.Container();
      panel1 = new Panel();
      button8 = new Button();
      textBox4 = new TextBox();
      label3 = new Label();
      textBox5 = new TextBox();
      button7 = new Button();
      dateTimePicker1 = new DateTimePicker();
      button6 = new Button();
      button5 = new Button();
      textBox10 = new TextBox();
      label10 = new Label();
      button1 = new Button();
      button3 = new Button();
      button2 = new Button();
      checkBox1 = new CheckBox();
      linkLabel2 = new LinkLabel();
      linkLabel1 = new LinkLabel();
      comboBox1 = new ComboBox();
      button4 = new Button();
      panel2 = new Panel();
      textBox7 = new TextBox();
      dataGridView1 = new DataGridView();
      编号 = new DataGridViewTextBoxColumn();
      账号 = new DataGridViewTextBoxColumn();
      COOKIE = new DataGridViewTextBoxColumn();
      订单号 = new DataGridViewTextBoxColumn();
      SKU = new DataGridViewTextBoxColumn();
      商品名称 = new DataGridViewTextBoxColumn();
      商品型号 = new DataGridViewTextBoxColumn();
      账号状态 = new DataGridViewTextBoxColumn();
      评论状态 = new DataGridViewTextBoxColumn();
      contextMenuStrip1 = new ContextMenuStrip(components);
      免登录打开ToolStripMenuItem = new ToolStripMenuItem();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
      panel1.SuspendLayout();
      panel2.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      contextMenuStrip1.SuspendLayout();
      SuspendLayout();
      panel1.Controls.Add(button8);
      panel1.Controls.Add(textBox4);
      panel1.Controls.Add(label3);
      panel1.Controls.Add(textBox5);
      panel1.Controls.Add(button7);
      panel1.Controls.Add(dateTimePicker1);
      panel1.Controls.Add(button6);
      panel1.Controls.Add(button5);
      panel1.Controls.Add(textBox10);
      panel1.Controls.Add(label10);
      panel1.Controls.Add(button1);
      panel1.Controls.Add(button3);
      panel1.Controls.Add(button2);
      panel1.Controls.Add(checkBox1);
      panel1.Controls.Add(linkLabel2);
      panel1.Controls.Add(linkLabel1);
      panel1.Controls.Add(comboBox1);
      panel1.Controls.Add(button4);
      panel1.Dock = DockStyle.Left;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(162, 538);
      panel1.TabIndex = 0;
      button8.Location = new Point(11, 417);
      button8.Name = "button8";
      button8.Size = new Size(135, 23);
      button8.TabIndex = 70;
      button8.Text = "查询数据";
      button8.UseVisualStyleBackColor = true;
      button8.Click += new EventHandler(button8_Click);
      textBox4.Location = new Point(13, 390);
      textBox4.Name = "textBox4";
      textBox4.Size = new Size(51, 21);
      textBox4.TabIndex = 69;
      textBox4.Text = "0";
      label3.AutoSize = true;
      label3.Location = new Point(70, 394);
      label3.Name = "label3";
      label3.Size = new Size(11, 12);
      label3.TabIndex = 68;
      label3.Text = "~";
      textBox5.Location = new Point(87, 390);
      textBox5.Name = "textBox5";
      textBox5.Size = new Size(59, 21);
      textBox5.TabIndex = 67;
      textBox5.Text = "50";
      button7.Location = new Point(11, 361);
      button7.Name = "button7";
      button7.Size = new Size(135, 23);
      button7.TabIndex = 66;
      button7.Text = "定时评论";
      button7.UseVisualStyleBackColor = true;
      button7.Click += new EventHandler(button7_Click);
      dateTimePicker1.CustomFormat = "MM-dd HH:mm:ss";
      dateTimePicker1.Format = DateTimePickerFormat.Custom;
      dateTimePicker1.Location = new Point(13, 334);
      dateTimePicker1.Name = "dateTimePicker1";
      dateTimePicker1.Size = new Size(133, 21);
      dateTimePicker1.TabIndex = 65;
      button6.Location = new Point(11, 304);
      button6.Name = "button6";
      button6.Size = new Size(135, 23);
      button6.TabIndex = 64;
      button6.Text = "查看商品型号";
      button6.UseVisualStyleBackColor = true;
      button6.Click += new EventHandler(button6_Click);
      button5.Location = new Point(11, 275);
      button5.Name = "button5";
      button5.Size = new Size(135, 23);
      button5.TabIndex = 63;
      button5.Text = "选择图片文件夹";
      button5.UseVisualStyleBackColor = true;
      button5.Click += new EventHandler(button5_Click);
      textBox10.Location = new Point(55, 219);
      textBox10.Name = "textBox10";
      textBox10.Size = new Size(91, 21);
      textBox10.TabIndex = 62;
      label10.AutoSize = true;
      label10.Location = new Point(11, 223);
      label10.Name = "label10";
      label10.Size = new Size(29, 12);
      label10.TabIndex = 61;
      label10.Text = "线程";
      button1.Location = new Point(11, 246);
      button1.Name = "button1";
      button1.Size = new Size(135, 23);
      button1.TabIndex = 58;
      button1.Text = "回头客评论";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      button3.Location = new Point(11, 190);
      button3.Name = "button3";
      button3.Size = new Size(135, 23);
      button3.TabIndex = 57;
      button3.Text = "开始评论（普通评论）";
      button3.UseVisualStyleBackColor = true;
      button3.Click += new EventHandler(button3_Click);
      button2.Location = new Point(11, 161);
      button2.Name = "button2";
      button2.Size = new Size(135, 23);
      button2.TabIndex = 56;
      button2.Text = "导入评论";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      checkBox1.AutoSize = true;
      checkBox1.Location = new Point(11, 139);
      checkBox1.Name = "checkBox1";
      checkBox1.Size = new Size(72, 16);
      checkBox1.TabIndex = 54;
      checkBox1.Text = "匿名评论";
      checkBox1.UseVisualStyleBackColor = true;
      checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
      linkLabel2.AutoSize = true;
      linkLabel2.Location = new Point(96, 86);
      linkLabel2.Name = "linkLabel2";
      linkLabel2.Size = new Size(53, 12);
      linkLabel2.TabIndex = 53;
      linkLabel2.TabStop = true;
      linkLabel2.Text = "删除备注";
      linkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
      linkLabel1.AutoSize = true;
      linkLabel1.Location = new Point(11, 86);
      linkLabel1.Name = "linkLabel1";
      linkLabel1.Size = new Size(53, 12);
      linkLabel1.TabIndex = 52;
      linkLabel1.TabStop = true;
      linkLabel1.Text = "刷新备注";
      linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
      comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox1.FormattingEnabled = true;
      comboBox1.Location = new Point(11, 55);
      comboBox1.Name = "comboBox1";
      comboBox1.Size = new Size(135, 20);
      comboBox1.TabIndex = 51;
      button4.Location = new Point(11, 110);
      button4.Name = "button4";
      button4.Size = new Size(135, 23);
      button4.TabIndex = 50;
      button4.Text = "查询数据";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      panel2.Controls.Add(textBox7);
      panel2.Dock = DockStyle.Top;
      panel2.Location = new Point(162, 0);
      panel2.Name = "panel2";
      panel2.Size = new Size(752, 154);
      panel2.TabIndex = 1;
      textBox7.BackColor = SystemColors.ActiveCaptionText;
      textBox7.Dock = DockStyle.Fill;
      textBox7.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
      textBox7.ForeColor = Color.FromArgb(byte.MaxValue, 128, 0);
      textBox7.Location = new Point(0, 0);
      textBox7.Multiline = true;
      textBox7.Name = "textBox7";
      textBox7.ReadOnly = true;
      textBox7.ScrollBars = ScrollBars.Vertical;
      textBox7.Size = new Size(752, 154);
      textBox7.TabIndex = 10;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(编号, 账号, COOKIE, 订单号, SKU, 商品名称, 商品型号, 账号状态, 评论状态);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(162, 154);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(752, 384);
      dataGridView1.TabIndex = 10;
      dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
      编号.DataPropertyName = "编号";
      编号.HeaderText = "编号";
      编号.Name = "编号";
      编号.Width = 60;
      账号.DataPropertyName = "账号";
      账号.HeaderText = "账号";
      账号.Name = "账号";
      账号.Width = 80;
      COOKIE.DataPropertyName = "COOKIE";
      COOKIE.HeaderText = "COOKIE";
      COOKIE.Name = "COOKIE";
      COOKIE.Visible = false;
      COOKIE.Width = 150;
      订单号.DataPropertyName = "订单号";
      订单号.HeaderText = "订单号";
      订单号.Name = "订单号";
      SKU.DataPropertyName = "SKU";
      SKU.HeaderText = "SKU";
      SKU.Name = "SKU";
      商品名称.DataPropertyName = "商品名称";
      商品名称.HeaderText = "商品名称";
      商品名称.Name = "商品名称";
      商品名称.Width = 200;
      商品型号.DataPropertyName = "商品型号";
      商品型号.HeaderText = "商品型号";
      商品型号.Name = "商品型号";
      商品型号.Width = 150;
      账号状态.DataPropertyName = "账号状态";
      账号状态.HeaderText = "账号状态";
      账号状态.Name = "账号状态";
      评论状态.DataPropertyName = "评论状态";
      评论状态.HeaderText = "评论状态";
      评论状态.Name = "评论状态";
      评论状态.Width = 200;
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
      dataGridViewTextBoxColumn1.DataPropertyName = "账号";
      dataGridViewTextBoxColumn1.HeaderText = "账号";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn1.Width = 60;
      dataGridViewTextBoxColumn2.DataPropertyName = "COOKIE";
      dataGridViewTextBoxColumn2.HeaderText = "COOKIE";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn2.Width = 150;
      dataGridViewTextBoxColumn3.DataPropertyName = "订单号";
      dataGridViewTextBoxColumn3.HeaderText = "订单号";
      dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      dataGridViewTextBoxColumn3.Visible = false;
      dataGridViewTextBoxColumn3.Width = 150;
      dataGridViewTextBoxColumn4.DataPropertyName = "SKU";
      dataGridViewTextBoxColumn4.HeaderText = "SKU";
      dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      dataGridViewTextBoxColumn4.Width = 150;
      dataGridViewTextBoxColumn5.DataPropertyName = "账号状态";
      dataGridViewTextBoxColumn5.HeaderText = "账号状态";
      dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      dataGridViewTextBoxColumn5.Width = 150;
      dataGridViewTextBoxColumn6.DataPropertyName = "评论状态";
      dataGridViewTextBoxColumn6.HeaderText = "评论状态";
      dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      dataGridViewTextBoxColumn6.Width = 200;
      dataGridViewTextBoxColumn7.DataPropertyName = "评论状态";
      dataGridViewTextBoxColumn7.HeaderText = "评论状态";
      dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      dataGridViewTextBoxColumn7.Width = 200;
      dataGridViewTextBoxColumn8.DataPropertyName = "账号状态";
      dataGridViewTextBoxColumn8.HeaderText = "账号状态";
      dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      dataGridViewTextBoxColumn9.DataPropertyName = "评论状态";
      dataGridViewTextBoxColumn9.HeaderText = "评论状态";
      dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
      dataGridViewTextBoxColumn9.Width = 200;
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(dataGridView1);
      Controls.Add(panel2);
      Controls.Add(panel1);
      Name = "评论";
      Size = new Size(914, 538);
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      panel2.ResumeLayout(false);
      panel2.PerformLayout();
      ((ISupportInitialize) dataGridView1).EndInit();
      contextMenuStrip1.ResumeLayout(false);
      ResumeLayout(false);
    }

    private delegate void UpdateDataGridView(DataTable dt);

    public delegate void SetTextHandler(string text);
  }
}
