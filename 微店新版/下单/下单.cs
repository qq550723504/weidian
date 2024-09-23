// Decompiled with JetBrains decompiler
// Type: 微店新版.下单.下单
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
  public class 下单 : UserControl
  {
    private bool ischeckbox = false;
    public static List<string> list = new List<string>();
    public static Dictionary<string, List<sku>> param_dic = new Dictionary<string, List<sku>>();
    public static List<wdauth> authlist = new List<wdauth>();
    public static List<string> addreslist = new List<string>();
    private flurl flurl;
    private string[] telStarts = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
    public List<string> lsss = new List<string>();
    public static Dictionary<double, double> mjdic = new Dictionary<double, double>();
    public static Dictionary<string, string> xsdic = new Dictionary<string, string>();
    private IContainer components = null;
    private Panel panel1;
    private Panel panel2;
    private TextBox textBox7;
    private ComboBox comboBox1;
    private DataGridView dataGridView1;
    private Button button1;
    private Button button2;
    private Button button3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private TextBox textBox1;
    private Label label1;
    private TextBox textBox10;
    private Label label10;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private Button button4;
    private Button button6;
    private TextBox textBox2;
    private Label label2;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem 免登录打开ToolStripMenuItem;
    private TextBox textBox5;
    private Label label3;
    private DataGridViewTextBoxColumn 编号;
    private DataGridViewTextBoxColumn 账号;
    private DataGridViewTextBoxColumn 昵称;
    private DataGridViewTextBoxColumn 下单类型;
    private DataGridViewTextBoxColumn 状态;
    private DataGridViewTextBoxColumn 支付链接;
    private Button button5;
    private Label label5;
    private Label label4;
    private TextBox textBox3;
    private ComboBox comboBox2;
    private Button button7;
    private TextBox textBox4;
    private Button button8;

    public 下单()
    {
      InitializeComponent();
      comboBox1.SelectedIndex = 0;
      log("备注功能是用于为当前下单的这批账号起一个名字假设：同样的10个账号，第一次刷，备注为莆田鞋，第二次刷，备注为AJ鞋，那么在之后的支付，核销，评论功能中，直接选择备注，就能带出相对应的账号");
      textBox5.Text = Program.users["count"].ToString();
      if (Program.users["open"].ToString() != "Y")
        dataGridView1.CellMouseDown -= new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
      log("请把不需要的备注删除，不然服务器残留的没用数据太多会导致软件出现卡顿！！！！！！！");
      log("请把不需要的备注删除，不然服务器残留的没用数据太多会导致软件出现卡顿！！！！！！！");
      log("请把不需要的备注删除，不然服务器残留的没用数据太多会导致软件出现卡顿！！！！！！！");
      log("服务器会自动清理超过10天的数据，请知悉");
    }

    private void log(string text)
    {
      if (textBox7.InvokeRequired)
        textBox7.Invoke(new 微店新版.下单.下单.SetTextHandler(log), text);
      else
        textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
    }

    private void log(wdauth info)
    {
      if (textBox7.InvokeRequired)
        textBox7.Invoke(new 微店新版.下单.下单.SetTextHandler(log), info.msg);
      else
        textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + info.msg + "\r\n");
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
        BeginInvoke(new 微店新版.下单.下单.UpdateDataGridView(UpdateGV), dt);
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

    public void UPDATE(wdauth users)
    {
      try
      {
        BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
        {
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells["账号"].Value.ToString() == users.phone && row.Cells["昵称"].Value.ToString() == users.nickName)
            {
              row.Cells["状态"].Value = users.msg;
              row.Cells["下单类型"].Value = users.type;
              row.Cells["支付链接"].Value = users.url;
              break;
            }
          }
        }));
      }
      catch (Exception ex)
      {
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "文本文档|*.txt";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
            list = new List<string>();
      foreach (string readAllLine in File.ReadAllLines(openFileDialog.FileName.ToString(), Encoding.UTF8))
      {
        if (!string.IsNullOrEmpty(readAllLine))
                    list.Add(readAllLine);
      }
      log("导入下单链接链接" + list.Count.ToString());
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (list == null || list.Count == 0)
      {
        log("请导入下单链接");
      }
      else
      {
                param_dic = new Dictionary<string, List<sku>>();
        if (list.Count < 10)
        {
          new Thread(new ParameterizedThreadStart(init)).Start(list);
        }
        else
        {
          int num = list.Count / 3;
          foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(list, num))
            new Thread(new ParameterizedThreadStart(init)).Start(split.Value);
        }
      }
    }

    private void param(string url, ref string itemid)
    {
      try
      {
        string str1 = url.Split('?')[1];
        char[] chArray = new char[1]{ '&' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2.Contains("itemID="))
            itemid = str2.Replace("itemID=", "");
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void TCparam(string url, ref string activityId, ref string shopId)
    {
      try
      {
        string str1 = url.Split('?')[1];
        char[] chArray = new char[1]{ '&' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2.Contains("activityId="))
            activityId = str2.Replace("activityId=", "");
          if (str2.Contains("shopId="))
            shopId = str2.Replace("shopId=", "");
        }
      }
      catch (Exception ex)
      {
      }
    }

    public static string MyshopId { get; set; }

    public static string MyactivityId { get; set; }

    public static string totalOriginPrice { get; set; }

    private async void init(object obj)
    {
      List<string> newlist = (List<string>) obj;
      foreach (string item in newlist)
      {
        string url = item;
        try
        {
          string itemid = string.Empty;
          param(url, ref itemid);
          if (string.IsNullOrWhiteSpace(itemid))
          {
            log(url + "解析失败");
            continue;
          }
          HttpRequestEntity res = await MYHTTP.Result("GET", "https://thor.weidian.com/detail/getItemSkuInfo/1.0?param=%7B%22itemId%22%3A%22" + itemid + "%22%7D&wdtoken=54c26548&_=" + COMM.ConvertDateTimeToString(), 3, null, null);
          if (res != null && !string.IsNullOrWhiteSpace(res.ResponseContent) && res.ResponseContent.Contains("OK"))
          {
            JObject json = COMM.GetToJsonList(res.ResponseContent);
            if (json != null && json["result"] != null)
            {
              JToken data = json["result"]["skuInfos"];
              if (data != null)
              {
                List<JToken> data0 = data.Where<JToken>(p => Convert.ToInt32(p["skuInfo"]["stock"].ToString()) > 0).ToList<JToken>();
                List<sku> skulist = new List<sku>();
                foreach (JToken data1 in data0)
                {
                  string disprice = string.Empty;
                  double discount_fee = 0.0;
                  bool 限时折扣 = false;
                  string priceKey = string.Empty;
                  string priceDesc = string.Empty;
                  string sku_name = data1["skuInfo"]["title"].ToString();
                  string id = data1["skuInfo"]["id"].ToString();
                  int stock = Convert.ToInt32(data1["skuInfo"]["stock"].ToString());
                  string originalPrice = data1["skuInfo"]["originalPrice"].ToString();
                  int length = originalPrice.Length;
                  int new_length = length - 2;
                  string new_price = originalPrice.Substring(0, new_length) + "." + originalPrice.Substring(originalPrice.Length - 2);
                  if (res.ResponseContent.Contains("priceTagList"))
                  {
                    限时折扣 = true;
                    string discountPrice = data1["skuInfo"]["discountPrice"].ToString();
                    int dislength = discountPrice.Length;
                    int disnew_length = dislength - 2;
                    string disnew_price = discountPrice.Substring(0, disnew_length) + "." + discountPrice.Substring(discountPrice.Length - 2);
                    discount_fee = Convert.ToDouble(new_price) - Convert.ToDouble(disnew_price);
                    disprice = disnew_price;
                    priceKey = data1["skuInfo"]["priceTagList"][0]["priceKey"].ToString();
                    priceDesc = data1["skuInfo"]["priceTagList"][0]["priceDesc"].ToString();
                    discountPrice = null;
                    disnew_price = null;
                  }
                  skulist.Add(new sku()
                  {
                    sku_name = sku_name,
                    stock = stock,
                    id = id,
                    price = new_price,
                    限时折扣 = 限时折扣,
                    discount_fee = discount_fee.ToString("f2"),
                    disprice = disprice,
                    priceKey = priceKey,
                    priceDesc = priceDesc
                  });
                  disprice = null;
                  priceKey = null;
                  priceDesc = null;
                  sku_name = null;
                  id = null;
                  originalPrice = null;
                  new_price = null;
                }
                log(string.Format("【{0}】获取可下单数据【{1}条，下单时，将随机获取型号以及随机数量，请确保库存充足】", itemid, skulist.Count));
                                param_dic.Add(itemid, skulist);
                data0 = null;
                skulist = null;
              }
              else
              {
                string disprice = string.Empty;
                double discount_fee = 0.0;
                bool 限时折扣 = false;
                string priceKey = string.Empty;
                string priceDesc = string.Empty;
                List<sku> skulist = new List<sku>();
                string sku_name = json["result"]["itemTitle"].ToString();
                string id = "0";
                string originalPrice = json["result"]["itemOriginalHighPrice"].ToString();
                int stock = Convert.ToInt32(json["result"]["itemStock"].ToString());
                int length = originalPrice.Length;
                int new_length = length - 2;
                string new_price = originalPrice.Substring(0, new_length) + "." + originalPrice.Substring(originalPrice.Length - 2);
                if (res.ResponseContent.Contains("priceTagList"))
                {
                  限时折扣 = true;
                  string discountPrice = json["result"]["itemDiscountHighPrice"].ToString();
                  int dislength = discountPrice.Length;
                  int disnew_length = dislength - 2;
                  string disnew_price = discountPrice.Substring(0, disnew_length) + "." + discountPrice.Substring(discountPrice.Length - 2);
                  discount_fee = Convert.ToDouble(new_price) - Convert.ToDouble(disnew_price);
                  disprice = disnew_price;
                  priceKey = json["result"]["priceTagList"][0]["priceKey"].ToString();
                  priceDesc = json["result"]["priceTagList"][0]["priceDesc"].ToString();
                  discountPrice = null;
                  disnew_price = null;
                }
                skulist.Add(new sku()
                {
                  sku_name = sku_name,
                  stock = stock,
                  id = id,
                  price = new_price,
                  限时折扣 = 限时折扣,
                  discount_fee = discount_fee.ToString("f2"),
                  disprice = disprice,
                  priceKey = priceKey,
                  priceDesc = priceDesc
                });
                log(string.Format("【{0}】获取可下单数据【{1}条，下单时，将随机获取型号以及随机数量，请确保库存充足】", itemid, skulist.Count));
                                param_dic.Add(itemid, skulist);
                disprice = null;
                priceKey = null;
                priceDesc = null;
                skulist = null;
                sku_name = null;
                id = null;
                originalPrice = null;
                new_price = null;
              }
              data = null;
            }
            else
              log("【" + itemid + "】数据解析失败，该商品需设置正常的商品类型，否则将导入无法解析");
            json = null;
          }
          else
            log("【" + itemid + "】数据解析失败");
          itemid = null;
          res = null;
        }
        catch (Exception ex)
        {
          log(url + "采集异常");
        }
        url = null;
      }
      newlist = null;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(textBox4.Text))
        log("请输入账号区间值");
      else if (string.IsNullOrWhiteSpace(textBox5.Text))
      {
        log("请输入账号区间值");
      }
      else
      {
        if (Convert.ToInt32(textBox5.Text) > Convert.ToInt32(Program.users["count"].ToString()))
        {
          Convert.ToInt32(Program.users["count"].ToString());
          textBox5.Text = Program.users["count"].ToString();
        }
        string sql = "SELECT * FROM `wd_user1` where status='Y' and remakr='" + COMM.TOKENS + "' ";
        if (Program.users["role"].ToString() == "Y")
          sql = "SELECT * FROM `wd_user1` where status='Y'";
        DataTable dataTable = MYSQL.Query(sql);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          log("暂无数据");
        }
        else
        {
          int int32_1 = Convert.ToInt32(textBox4.Text);
          int int32_2 = Convert.ToInt32(textBox5.Text);
                    authlist = new List<wdauth>();
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("编号", typeof (string)));
          dt.Columns.Add(new DataColumn("账号", typeof (string)));
          dt.Columns.Add(new DataColumn("昵称", typeof (string)));
          dt.Columns.Add(new DataColumn("下单类型", typeof (string)));
          dt.Columns.Add(new DataColumn("状态", typeof (string)));
          dt.Columns.Add(new DataColumn("支付链接", typeof (string)));
          for (int index = int32_1; index < int32_2; ++index)
          {
            DataRow row = dt.NewRow();
            row["编号"] = dataTable.Rows[index]["original"].ToString();
            row["账号"] = dataTable.Rows[index]["phone"].ToString();
            row["昵称"] = dataTable.Rows[index]["nickname"].ToString();
            row["下单类型"] = "";
            row["状态"] = "";
            row["支付链接"] = "";
            dt.Rows.Add(row);
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
              phone = dataTable.Rows[index]["phone"].ToString()
            };
                        authlist.Add(wdauth);
          }
          if (dt != null && dt.Rows.Count > 0)
            UpdateGV(dt);
          log("账号获取成功，获取账号" + authlist.Count.ToString());
        }
      }
    }

    public int X { get; set; }

    public int F { get; set; }

    public bool flog { get; set; }

    public string type { get; set; }

    public string remark { get; set; }

    private void button4_Click(object sender, EventArgs e)
    {
      if (authlist == null || authlist.Count == 0)
        log("请导入账号");
      else if (param_dic == null || param_dic.Count == 0)
        log("请导入商品链接");
      else if (string.IsNullOrWhiteSpace(comboBox1.Text))
        log("请选择下单方式");
      else if (string.IsNullOrWhiteSpace(COMM.cookie))
        log("请登录主店铺");
      else if (string.IsNullOrWhiteSpace(COMM.shop_param))
        log("请选择店铺下单数据");
      else if (COMM.isgj && string.IsNullOrWhiteSpace(COMM.price))
        log("请输入改价价格");
      else if (COMM.isgz && string.IsNullOrWhiteSpace(COMM.shopid))
        log("请输入店铺ID");
      else if (string.IsNullOrWhiteSpace(textBox10.Text))
        log("请输入线程数量");
      else if (string.IsNullOrWhiteSpace(textBox1.Text))
        log("请输入阈值");
      else if (string.IsNullOrWhiteSpace(textBox2.Text))
      {
        log("请输入备注");
      }
      else
      {
        remark = textBox2.Text;
        type = comboBox1.Text;
        if (type == "快递" && (addreslist == null || addreslist.Count == 0))
        {
          log("请导入地址");
        }
        else
        {
          DataTable dataSource = (DataTable) dataGridView1.DataSource;
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
              if (item.Cells[0].Value != null && (bool) item.Cells[0].Value)
              {
                List<wdauth> list = authlist.Where<wdauth>(p => p.original == item.Cells["编号"].Value.ToString()).ToList<wdauth>();
                if (list == null || list.Count == 0)
                  log(item.Cells["编号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
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
              flurl = new flurl();
              X = Convert.ToInt32(textBox10.Text);
              F = Convert.ToInt32(textBox1.Text);
              if (wdauthList.Count < 10)
              {
                new Thread(new ParameterizedThreadStart(start)).Start(wdauthList);
              }
              else
              {
                int num = wdauthList.Count / X;
                foreach (KeyValuePair<string, List<wdauth>> split in COMM.SplitList(wdauthList, num))
                  new Thread(new ParameterizedThreadStart(start)).Start(split.Value);
              }
            }
          }
        }
      }
    }

    private async void start(object obj)
    {
      List<wdauth> list = (List<wdauth>) obj;
      foreach (wdauth wdauth in list)
      {
        wdauth ims = wdauth;
        wdauth info = ims;
        if (string.IsNullOrWhiteSpace(info.url))
        {
          try
          {
            if (authlist.Where<wdauth>(p => p.msg == "下单成功").ToList<wdauth>().Count >= F)
            {
              log("符合阈值条件，已自动停止下单");
              break;
            }
            string addresid = string.Empty;
            if (type == "快递")
            {
              for (int i = 0; i < 5; ++i)
              {
                addresid = await addres(info);
                if (!string.IsNullOrWhiteSpace(addresid))
                  break;
              }
              if (string.IsNullOrWhiteSpace(addresid) || addresid == "1")
              {
                info.msg = "地址增加失败";
                log(info);
                UPDATE(info);
                continue;
              }
            }
            string res = string.Empty;
            string orderAmount = string.Empty;
            string items_str = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (type == "快递")
            {
              Model.微店_od1 od = new Model.微店_od1();
              List<Model.微店_odItem_list> kd = SKU();
              StringBuilder stringBuilder = new StringBuilder();
              int num;
              for (int j = 0; j < kd.Count; num = j++)
              {
                items_str = items_str + kd[j].item_id + ",";
                StringBuilder stringBuilder1 = stringBuilder;
                string[] strArray1 = new string[7]
                {
                  "{\"item_id\":\"",
                  kd[j].item_id,
                  "\",\"quantity\":\"",
                  null,
                  null,
                  null,
                  null
                };
                num = kd[j].quantity;
                strArray1[3] = num.ToString();
                strArray1[4] = "\",\"price_type\":\"1\",\"item_sku_id\":\"";
                strArray1[5] = kd[j].item_sku_id;
                strArray1[6] = "\"},";
                string str = string.Concat(strArray1);
                stringBuilder1.Append(str);
              }
              string js1 = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
              string js = "{\"channel\":\"xiaochengxu\",\"item_list\":[" + js1 + "],\"source_id\":\"5774937da7853cd3860c022c7fb13f84\",\"wfr\":\"\",\"self_address_id\":\"\",\"address_id\":\"\",\"buyer\":{\"eat_in_table_name\":\"\"}}";
              dic.Add("param", js);
              dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
              res = await flurl.POST("https://thor.weidian.com/vbuy/ConfirmOrder/1.0", dic);
              if (!string.IsNullOrWhiteSpace(res))
              {
                JObject myjsons = COMM.GetToJsonList(res);
                JToken shop = myjsons["result"]["shop_list"][0];
                JToken itemls = myjsons["result"]["shop_list"][0]["item_list"];
                List<Model.微店_odItem_list> itemlist = new List<Model.微店_odItem_list>();
                od = new Model.微店_od1()
                {
                  channel = "xiaochengxu",
                  source_id = myjsons["result"]["source_id"].ToString(),
                  q_pv_id = "1535000001883e9d61680a2064b00275",
                  biz_type = 1,
                  buyer = new Model.微店_odBuyer1()
                  {
                    buyer_id = myjsons["result"]["buyer"]["buyer_id"].ToString(),
                    eat_in_table_name = "",
                    address_id = Convert.ToInt32(addresid)
                  },
                  deliver_type = 0,
                  is_no_ship_addr = 0,
                  total_vjifen = "",
                  wfr = "",
                  appid = "",
                  pay_type = 0,
                  discount_list = new List<object>(),
                  invalid_shop_list = new List<object>()
                };
                foreach (JToken item in itemls)
                {
                  List<object> discount_list = new List<object>();
                  JToken discountls = item["discount_list"];
                  foreach (JToken dis in discountls)
                    discount_list.Add(new
                    {
                        discount_fee = dis[(object)"discount_fee"].ToString(),
                        extend = dis[(object)"extend"].ToString(),
                        id = dis[(object)"id"].ToString(),
                        title = dis[(object)"title"].ToString()
                    });
                  itemlist.Add(new Model.微店_odItem_list()
                  {
                    item_id = item["item_id"].ToString(),
                    quantity = Convert.ToInt32(item["quantity"].ToString()),
                    item_sku_id = item["item_sku_id"].ToString(),
                    ori_price = item["ori_price"].ToString(),
                    price = item["price"].ToString(),
                    price_type = 1,
                    extend = new Model.微店_odExtend(),
                    discount_list = discount_list,
                    item_convey_info = new Model.微店_odItem_convey_info()
                  });
                  discount_list = null;
                  discountls = null;
                }
                List<Model.微店_odShop_list1> shoplist = new List<Model.微店_odShop_list1>();
                List<Model.vv> shopdislist = new List<Model.vv>();
                foreach (JToken shopdis in shop["discount_list"])
                {
                  JToken option_list = shopdis["option_list"];
                  foreach (JToken opt in option_list)
                    shopdislist.Add(new Model.vv()
                    {
                      id = opt["id"].ToString()
                    });
                  option_list = null;
                }
                shoplist.Add(new Model.微店_odShop_list1()
                {
                  shop_id = shop["shop"]["shop_id"].ToString(),
                  f_shop_id = shop["shop"]["f_shop_id"] == null ? "" : shop["shop"]["f_shop_id"].ToString(),
                  sup_id = "",
                  item_list = itemlist,
                  order_type = 3,
                  ori_price = shop["ori_total_price"].ToString(),
                  price = myjsons["result"]["total_pay_price"].ToString(),
                  express_fee = "0.00",
                  express_type = 4,
                  discount_list = shopdislist,
                  invalid_item_list = new List<Model.Invalid_item_list>()
                });
                od.shop_list = shoplist;
                od.total_pay_price = myjsons["result"]["total_pay_price"].ToString();
                orderAmount = od.total_pay_price;
                info.type = orderAmount;
                string myjson = COMM.ToJSON(od);
                dic = new Dictionary<string, string>();
                dic.Add("param", myjson);
                string wdtoken = COMM.r(8);
                dic.Add("context", "{\"shopping_center\":\"" + shop["shop"]["shop_id"].ToString() + "\",\"subChannel\":\"browser\",\"thirdSubchannel\":\"chrome\"}");
                dic.Add("udc", "H4sIAAAAAAAAA91UiZEcIQxMSehF4fBGccG7NbvnrbtyBIZiEOhviaHoizAmvnxBKD0jOv0a47ZTAtTo4TWzJGjtNel1HYOWaByVHNZDYDTPmcPH5uYW57dJ6nCj8nzfdOutwxT51e03hqcvzMZXjI8QE3cL3B5X8COESY8pkzRwISGT6SUhLlqn6EVxQrIFBcGeQcKcdXqGlm0RCWiLeHnQqd1Uty5eXPLp0w36pSWg72NNvAf79Q3ehX8WeWL8XmTizd0bpPKnFwKbARxvooyC10hV4d0c3izgKRioJnBIl%2FITAl%2BOeEkN2UdF9G8tuCbAA0LKdsdMGsissbKx80E0BHRCydjEuk0%2FXXv20Xe25JT07Lly58k7aLQhw0aMNWP21VcukGutvdv2HbvvsddpJ05SAJkbVlk%2F8XQFBqhYWGWAnVI7kFzh4Gvl540M1WiehjMsHMimb5MwP%2BSv06OV36c3z0qjaoCOKwSq8ihEYfqjQvSqEE1U8WJG9QGwrF5C3Tz%2Bsw7regpddA1kEJMB1R9vCgiZffCyj2%2Fq8IQWxf2T9dNF1Dba7SH66TINjztBRT9X7ygqG1rHXtTWpqDO4Uwk8dGsG6NdztFRg0dB%2FprAzqlFYE1qcohrIVK2TfK9GulMj9BLyhozM04MubZENkXpYyy67YuqD2AVaiz4sXQnR0PaXqR7A5P5eizgLPx4dARcfPYnYFjThJyuvzt9vRNZLucPRQwfeEMFAAA%3D");
                dic.Add("wdtoken", wdtoken);
                string cookie = "__spider__visitorid=" + COMM.r(16) + ";login_token=" + info.loginToken + ";is_login=true;login_type=LOGIN_USER_TYPE_MASTER;login_source=LOGIN_USER_SOURCE_MASTER;uid=" + info.uid + ";duid=" + info.duid + ";sid=" + info.sid + ";wdtoken=" + wdtoken + ";__spider__sessionid=" + COMM.r(16) + ";v-components/clean-up-advert@private_domain=" + shop["shop"]["shop_id"].ToString() + ";v-components/clean-up-advert@wx_app=" + shop["shop"]["shop_id"].ToString();
                res = await flurl.POST999("https://thor.weidian.com/vbuy/CreateOrder/1.0", cookie, dic);
                if (res == null || string.IsNullOrWhiteSpace(res) || !res.Contains("order_id"))
                {
                  for (int i = 0; i < 5; ++i)
                  {
                    string str = await flurl.POST999("https://thor.weidian.com/vbuy/CreateOrder/1.0", cookie, dic);
                    res = res = str;
                    str = null;
                    if (res != null && !string.IsNullOrWhiteSpace(res) && res.Contains("order_id"))
                      break;
                  }
                }
                myjsons = null;
                shop = null;
                itemls = null;
                itemlist = null;
                shoplist = null;
                shopdislist = null;
                myjson = null;
                wdtoken = null;
                cookie = null;
              }
              else
                log("下单失败，获取下单数据失败");
              od = null;
              kd = null;
              stringBuilder = null;
              js1 = null;
              js = null;
            }
            if (type == "商城版自提")
            {
              Model.微店_od od = new Model.微店_od();
              List<Model.微店_odItem_list> kd = SKU();
              StringBuilder stringBuilder = new StringBuilder();
              int num;
              for (int j = 0; j < kd.Count; num = j++)
              {
                items_str = items_str + kd[j].item_id + ",";
                StringBuilder stringBuilder2 = stringBuilder;
                string[] strArray2 = new string[7]
                {
                  "{\"item_id\":\"",
                  kd[j].item_id,
                  "\",\"quantity\":\"",
                  null,
                  null,
                  null,
                  null
                };
                num = kd[j].quantity;
                strArray2[3] = num.ToString();
                strArray2[4] = "\",\"price_type\":\"1\",\"item_sku_id\":\"";
                strArray2[5] = kd[j].item_sku_id;
                strArray2[6] = "\"},";
                string str = string.Concat(strArray2);
                stringBuilder2.Append(str);
              }
              string js1 = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
              string js = "{\"channel\":\"xiaochengxu\",\"item_list\":[" + js1 + "],\"source_id\":\"5774937da7853cd3860c022c7fb13f84\",\"wfr\":\"\",\"self_address_id\":\"\",\"address_id\":\"\",\"buyer\":{\"eat_in_table_name\":\"\"}}";
              dic.Add("param", js);
              dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
              res = await flurl.POST("https://thor.weidian.com/vbuy/ConfirmOrder/1.0", dic);
              if (!string.IsNullOrWhiteSpace(res))
              {
                JObject myjsons = COMM.GetToJsonList(res);
                JToken shop = myjsons["result"]["shop_list"][0];
                JToken itemls = myjsons["result"]["shop_list"][0]["item_list"];
                List<Model.微店_odItem_list> itemlist = new List<Model.微店_odItem_list>();
                od = new Model.微店_od()
                {
                  channel = "bjh5",
                  source_id = myjsons["result"]["source_id"].ToString(),
                  q_pv_id = "0e470000018e8ee2ad710a8104fd2cf6",
                  biz_type = 1,
                  buyer = new Model.微店_odBuyer()
                  {
                    buyer_id = myjsons["result"]["buyer"]["buyer_id"].ToString(),
                    eat_in_table_name = "",
                    self_address_id = Convert.ToInt32(COMM.shop_param.Split('-')[1]),
                    buyer_telephone = info.phone,
                    buyer_name = new GetChineseNames().GetTestData()
                  },
                  deliver_type = 1,
                  is_no_ship_addr = 0,
                  total_vjifen = "",
                  wfr = "",
                  appid = "",
                  pay_type = 0,
                  discount_list = new List<object>(),
                  invalid_shop_list = new List<object>()
                };
                foreach (JToken item in itemls)
                {
                  List<object> discount_list = new List<object>();
                  JToken discountls = item["discount_list"];
                  foreach (JToken dis in discountls)
                    discount_list.Add(new
                    {
                        discount_fee = dis[(object)"discount_fee"].ToString(),
                        extend = dis[(object)"extend"].ToString(),
                        id = dis[(object)"id"].ToString(),
                        title = dis[(object)"title"].ToString()
                    });
                  itemlist.Add(new Model.微店_odItem_list()
                  {
                    item_id = item["item_id"].ToString(),
                    quantity = Convert.ToInt32(item["quantity"].ToString()),
                    item_sku_id = item["item_sku_id"].ToString(),
                    ori_price = item["ori_price"].ToString(),
                    price = item["price"].ToString(),
                    price_type = 1,
                    extend = new Model.微店_odExtend(),
                    discount_list = discount_list,
                    item_convey_info = new Model.微店_odItem_convey_info()
                  });
                  discount_list = null;
                  discountls = null;
                }
                List<Model.微店_odShop_list> shoplist = new List<Model.微店_odShop_list>();
                List<Model.vv> shopdislist = new List<Model.vv>();
                foreach (JToken shopdis in shop["discount_list"])
                {
                  JToken option_list = shopdis["option_list"];
                  foreach (JToken jtoken in option_list)
                  {
                    JToken opt = jtoken;
                    if (!(opt["id"].ToString() == "0_shopCoupon"))
                    {
                      shopdislist.Add(new Model.vv()
                      {
                        id = opt["id"].ToString()
                      });
                      opt = null;
                    }
                  }
                  option_list = null;
                }
                shoplist.Add(new Model.微店_odShop_list()
                {
                  shop_id = shop["shop"]["shop_id"].ToString(),
                  f_shop_id = shop["shop"]["f_shop_id"] == null ? "" : shop["shop"]["f_shop_id"].ToString(),
                  sup_id = "",
                  item_list = itemlist,
                  order_type = 3,
                  ori_price = shop["ori_total_price"].ToString(),
                  price = shop["total_price"].ToString(),
                  express_fee = "0.00",
                  express_type = "",
                  discount_list = shopdislist,
                  invalid_item_list = new List<Model.Invalid_item_list>()
                });
                od.shop_list = shoplist;
                od.total_pay_price = shop["total_price"].ToString();
                orderAmount = od.total_pay_price;
                info.type = orderAmount;
                string myjson = COMM.ToJSON(od);
                dic = new Dictionary<string, string>();
                dic.Add("param", myjson);
                string wdtoken = COMM.r(8);
                dic.Add("context", "{\"shopping_center\":\"" + shop["shop"]["shop_id"].ToString() + "\",\"subChannel\":\"browser\",\"thirdSubchannel\":\"chrome\"}");
                dic.Add("udc", "H4sIAAAAAAAAA91UiZEcIQxMSehF4fBGccG7NbvnrbtyBIZiEOhviaHoizAmvnxBKD0jOv0a47ZTAtTo4TWzJGjtNel1HYOWaByVHNZDYDTPmcPH5uYW57dJ6nCj8nzfdOutwxT51e03hqcvzMZXjI8QE3cL3B5X8COESY8pkzRwISGT6SUhLlqn6EVxQrIFBcGeQcKcdXqGlm0RCWiLeHnQqd1Uty5eXPLp0w36pSWg72NNvAf79Q3ehX8WeWL8XmTizd0bpPKnFwKbARxvooyC10hV4d0c3izgKRioJnBIl%2FITAl%2BOeEkN2UdF9G8tuCbAA0LKdsdMGsissbKx80E0BHRCydjEuk0%2FXXv20Xe25JT07Lly58k7aLQhw0aMNWP21VcukGutvdv2HbvvsddpJ05SAJkbVlk%2F8XQFBqhYWGWAnVI7kFzh4Gvl540M1WiehjMsHMimb5MwP%2BSv06OV36c3z0qjaoCOKwSq8ihEYfqjQvSqEE1U8WJG9QGwrF5C3Tz%2Bsw7regpddA1kEJMB1R9vCgiZffCyj2%2Fq8IQWxf2T9dNF1Dba7SH66TINjztBRT9X7ygqG1rHXtTWpqDO4Uwk8dGsG6NdztFRg0dB%2FprAzqlFYE1qcohrIVK2TfK9GulMj9BLyhozM04MubZENkXpYyy67YuqD2AVaiz4sXQnR0PaXqR7A5P5eizgLPx4dARcfPYnYFjThJyuvzt9vRNZLucPRQwfeEMFAAA%3D");
                dic.Add("wdtoken", wdtoken);
                string cookie = "__spider__visitorid=" + COMM.r(16) + ";login_token=" + info.loginToken + ";is_login=true;login_type=LOGIN_USER_TYPE_MASTER;login_source=LOGIN_USER_SOURCE_MASTER;uid=" + info.uid + ";duid=" + info.duid + ";sid=" + info.sid + ";wdtoken=" + wdtoken + ";__spider__sessionid=" + COMM.r(16) + ";v-components/clean-up-advert@private_domain=" + shop["shop"]["shop_id"].ToString() + ";v-components/clean-up-advert@wx_app=" + shop["shop"]["shop_id"].ToString();
                res = await flurl.POST999("https://thor.weidian.com/vbuy/CreateOrder/1.0", cookie, dic);
                if (res == null || string.IsNullOrWhiteSpace(res) || !res.Contains("order_id"))
                {
                  for (int i = 0; i < 5; ++i)
                  {
                    res = await flurl.POST999("https://thor.weidian.com/vbuy/CreateOrder/1.0", cookie, dic);
                    if (res != null && !string.IsNullOrWhiteSpace(res) && res.Contains("order_id"))
                      break;
                  }
                }
                myjsons = null;
                shop = null;
                itemls = null;
                itemlist = null;
                shoplist = null;
                shopdislist = null;
                myjson = null;
                wdtoken = null;
                cookie = null;
              }
              else
                log("下单失败，获取下单数据失败");
              od = null;
              kd = null;
              stringBuilder = null;
              js1 = null;
              js = null;
            }
            if (type == "非商城版自提")
            {
              Model.微店_od od = new Model.微店_od();
              List<Model.微店_odItem_list> kd = SKU();
              StringBuilder stringBuilder = new StringBuilder();
              int num;
              for (int j = 0; j < kd.Count; num = j++)
              {
                items_str = items_str + kd[j].item_id + ",";
                StringBuilder stringBuilder3 = stringBuilder;
                string[] strArray3 = new string[7]
                {
                  "{\"item_id\":\"",
                  kd[j].item_id,
                  "\",\"quantity\":\"",
                  null,
                  null,
                  null,
                  null
                };
                num = kd[j].quantity;
                strArray3[3] = num.ToString();
                strArray3[4] = "\",\"price_type\":\"1\",\"item_sku_id\":\"";
                strArray3[5] = kd[j].item_sku_id;
                strArray3[6] = "\"},";
                string str = string.Concat(strArray3);
                stringBuilder3.Append(str);
              }
              string js1 = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
              string js = "{\"channel\":\"xiaochengxu\",\"item_list\":[" + js1 + "],\"source_id\":\"5774937da7853cd3860c022c7fb13f84\",\"wfr\":\"\",\"self_address_id\":\"\",\"address_id\":\"\",\"buyer\":{\"eat_in_table_name\":\"\"}}";
              dic.Add("param", js);
              dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
              res = await flurl.POST("https://thor.weidian.com/vbuy/ConfirmOrder/1.0", dic);
              if (!string.IsNullOrWhiteSpace(res))
              {
                JObject myjsons = COMM.GetToJsonList(res);
                JToken shop = myjsons["result"]["shop_list"][0];
                JToken itemls = myjsons["result"]["shop_list"][0]["item_list"];
                List<Model.微店_odItem_list> itemlist = new List<Model.微店_odItem_list>();
                od = new Model.微店_od()
                {
                  channel = "xiaochengxu",
                  source_id = myjsons["result"]["source_id"].ToString(),
                  q_pv_id = "1535000001883e9d61680a2064b00275",
                  biz_type = 1,
                  buyer = new Model.微店_odBuyer()
                  {
                    buyer_id = myjsons["result"]["buyer"]["buyer_id"].ToString(),
                    eat_in_table_name = "",
                    self_address_id = Convert.ToInt32(COMM.shop_param.Split('-')[1]),
                    buyer_telephone = info.phone,
                    buyer_name = new GetChineseNames().GetTestData()
                  },
                  deliver_type = 1,
                  is_no_ship_addr = 0,
                  total_vjifen = "",
                  wfr = "",
                  appid = "",
                  pay_type = 0,
                  discount_list = new List<object>(),
                  invalid_shop_list = new List<object>()
                };
                foreach (JToken item in itemls)
                {
                  List<object> discount_list = new List<object>();
                  JToken discountls = item["discount_list"];
                  foreach (JToken dis in discountls)
                    discount_list.Add(new
                    {
                        discount_fee = dis[(object)"discount_fee"].ToString(),
                        extend = dis[(object)"extend"].ToString(),
                        id = dis[(object)"id"].ToString(),
                        title = dis[(object)"title"].ToString()
                    });
                  itemlist.Add(new Model.微店_odItem_list()
                  {
                    item_id = item["item_id"].ToString(),
                    quantity = Convert.ToInt32(item["quantity"].ToString()),
                    item_sku_id = item["item_sku_id"].ToString(),
                    ori_price = item["ori_price"].ToString(),
                    price = item["price"].ToString(),
                    price_type = 1,
                    extend = new Model.微店_odExtend(),
                    discount_list = discount_list,
                    item_convey_info = new Model.微店_odItem_convey_info()
                  });
                  discount_list = null;
                  discountls = null;
                }
                List<Model.微店_odShop_list> shoplist = new List<Model.微店_odShop_list>();
                List<Model.vv> shopdislist = new List<Model.vv>();
                foreach (JToken shopdis in shop["discount_list"])
                {
                  JToken option_list = shopdis["option_list"];
                  foreach (JToken jtoken in option_list)
                  {
                    JToken opt = jtoken;
                    if (!(opt["id"].ToString() == "0_shopCoupon"))
                    {
                      shopdislist.Add(new Model.vv()
                      {
                        id = opt["id"].ToString()
                      });
                      opt = null;
                    }
                  }
                  option_list = null;
                }
                shoplist.Add(new Model.微店_odShop_list()
                {
                  shop_id = shop["shop"]["shop_id"].ToString(),
                  f_shop_id = shop["shop"]["f_shop_id"] == null ? "" : shop["shop"]["f_shop_id"].ToString(),
                  sup_id = "",
                  item_list = itemlist,
                  order_type = 3,
                  ori_price = shop["ori_total_price"].ToString(),
                  price = shop["total_price"].ToString(),
                  express_fee = "0.00",
                  express_type = "",
                  discount_list = shopdislist,
                  invalid_item_list = new List<Model.Invalid_item_list>()
                });
                od.shop_list = shoplist;
                od.total_pay_price = shop["total_price"].ToString();
                orderAmount = od.total_pay_price;
                info.type = orderAmount;
                string myjson = COMM.ToJSON(od);
                dic = new Dictionary<string, string>();
                dic.Add("param", myjson);
                string wdtoken = COMM.r(8);
                dic.Add("context", "{\"shopping_center\":\"" + shop["shop"]["shop_id"].ToString() + "\",\"subChannel\":\"browser\",\"thirdSubchannel\":\"chrome\"}");
                dic.Add("udc", "H4sIAAAAAAAAA91UiZEcIQxMSehF4fBGccG7NbvnrbtyBIZiEOhviaHoizAmvnxBKD0jOv0a47ZTAtTo4TWzJGjtNel1HYOWaByVHNZDYDTPmcPH5uYW57dJ6nCj8nzfdOutwxT51e03hqcvzMZXjI8QE3cL3B5X8COESY8pkzRwISGT6SUhLlqn6EVxQrIFBcGeQcKcdXqGlm0RCWiLeHnQqd1Uty5eXPLp0w36pSWg72NNvAf79Q3ehX8WeWL8XmTizd0bpPKnFwKbARxvooyC10hV4d0c3izgKRioJnBIl%2FITAl%2BOeEkN2UdF9G8tuCbAA0LKdsdMGsissbKx80E0BHRCydjEuk0%2FXXv20Xe25JT07Lly58k7aLQhw0aMNWP21VcukGutvdv2HbvvsddpJ05SAJkbVlk%2F8XQFBqhYWGWAnVI7kFzh4Gvl540M1WiehjMsHMimb5MwP%2BSv06OV36c3z0qjaoCOKwSq8ihEYfqjQvSqEE1U8WJG9QGwrF5C3Tz%2Bsw7regpddA1kEJMB1R9vCgiZffCyj2%2Fq8IQWxf2T9dNF1Dba7SH66TINjztBRT9X7ygqG1rHXtTWpqDO4Uwk8dGsG6NdztFRg0dB%2FprAzqlFYE1qcohrIVK2TfK9GulMj9BLyhozM04MubZENkXpYyy67YuqD2AVaiz4sXQnR0PaXqR7A5P5eizgLPx4dARcfPYnYFjThJyuvzt9vRNZLucPRQwfeEMFAAA%3D");
                dic.Add("wdtoken", wdtoken);
                string cookie = "__spider__visitorid=" + COMM.r(16) + ";login_token=" + info.loginToken + ";is_login=true;login_type=LOGIN_USER_TYPE_MASTER;login_source=LOGIN_USER_SOURCE_MASTER;uid=" + info.uid + ";duid=" + info.duid + ";sid=" + info.sid + ";wdtoken=" + wdtoken + ";__spider__sessionid=" + COMM.r(16) + ";v-components/clean-up-advert@private_domain=" + shop["shop"]["shop_id"].ToString() + ";v-components/clean-up-advert@wx_app=" + shop["shop"]["shop_id"].ToString();
                res = await flurl.POST999("https://thor.weidian.com/vbuy/CreateOrder/1.0", cookie, dic);
                if (res == null || string.IsNullOrWhiteSpace(res) || !res.Contains("order_id"))
                {
                  for (int i = 0; i < 5; ++i)
                  {
                    res = await flurl.POST("https://thor.weidian.com/vbuy/CreateOrder/1.0", dic);
                    if (res != null && !string.IsNullOrWhiteSpace(res) && res.Contains("order_id"))
                      break;
                  }
                }
                myjsons = null;
                shop = null;
                itemls = null;
                itemlist = null;
                shoplist = null;
                shopdislist = null;
                myjson = null;
                wdtoken = null;
                cookie = null;
              }
              else
                log("下单失败，获取下单数据失败");
              od = null;
              kd = null;
              stringBuilder = null;
              js1 = null;
              js = null;
            }
            if (res == null || string.IsNullOrWhiteSpace(res) || !res.Contains("order_id"))
            {
              info.msg = "订单创建失败";
              log(res);
              log(info);
              UPDATE(info);
              continue;
            }
            info.msg = "订单创建成功";
            log(info);
            UPDATE(info);
            Model.微店_PAY paymodel = JsonConvert.DeserializeObject<Model.微店_PAY>(res);
            string payurl = paymodel.result.url;
            string orderid = paymodel.result.order_list[0].order_id;
            string[] sp = payurl.Split('?')[1].Split('&');
            string ct = string.Empty;
            string tk = string.Empty;
            string[] strArray = sp;
            for (int index = 0; index < strArray.Length; ++index)
            {
              string tm = strArray[index];
              if (tm.StartsWith("ct"))
                ct = tm.Split('=')[1];
              if (tm.StartsWith("token"))
                tk = tm.Split('=')[1];
              tm = null;
            }
            strArray = null;
            if (COMM.isgj)
            {
              double f = await gj(orderid);
              if (f > 0.0)
              {
                orderAmount = f.ToString("f2");
              }
              else
              {
                info.msg = "改价失败";
                log(info);
                UPDATE(info);
                continue;
              }
            }
            info.type = info.type + "，" + orderAmount;
            items_str = items_str.TrimEnd(',');
            string cts = "{\"ct\":\"" + ct + "\",\"token\":\"" + tk + "\",\"from\":\"H5\",\"platForm\":\"browser\",\"environment\":\"DEFAULT\",\"appVersion\":\"\",\"orderAmount\":\"" + Convert.ToDecimal(orderAmount).ToString("f2") + "\",\"isMall\":1,\"entrance\":\"common_cashier\",\"instCode\":\"ALIPAY\",\"dbcr\":\"GC\",\"payMode\":\"ONLINE_BANK\",\"concession\":true,\"clientExt\":{\"deviceType\":\"H5\",\"payTypeFrom\":\"payCashier\",\"userAgent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36\",\"shopType\":\"normal\",\"isMobile\":true,\"entrance\":\"common_cashier\",\"itemIds\":\"" + items_str + "\"}}";
            dic = new Dictionary<string, string>();
            dic.Add("param", cts);
            dic.Add("v", "1.0");
            dic.Add("timestamp", COMM.ConvertDateTimeToString());
            string cookies = "__spider__sessionid=" + COMM.r(16) + "; __spider__visitorid=" + COMM.r(16) + "; duid=" + info.duid + "; is_login=true; login_source=LOGIN_USER_SOURCE_MASTER; login_token=" + info.loginToken + "; login_type=LOGIN_USER_TYPE_MASTER; sid=" + info.sid + "; uid=" + info.uid + "; v-components/tencent-live-plugin@wfr=c_wxh5; wdtoken=8f2d1224";
            HttpRequestEntity resu = await MYHTTP.Result("POST", "https://thor.weidian.com/cashier/pay.pre/1.0", 4, dic, cookies);
            if (resu == null || string.IsNullOrWhiteSpace(resu.ResponseContent) || resu.ResponseContent.Contains("qr.alipay.com"))
            {
              for (int i = 0; i < 5; ++i)
              {
                dic["timestamp"] = COMM.ConvertDateTimeToString();
                resu = await MYHTTP.Result("POST", "https://thor.weidian.com/cashier/pay.pre/1.0", 4, dic, cookies);
                if (resu != null && !string.IsNullOrWhiteSpace(resu.ResponseContent) && !resu.ResponseContent.Contains("qr.alipay.com"))
                  break;
              }
            }
            if (resu == null || string.IsNullOrWhiteSpace(resu.ResponseContent) || !resu.ResponseContent.Contains("payUrl"))
            {
              info.msg = "支付链接提取失败";
              log(info);
              log(resu.ResponseContent);
              UPDATE(info);
              continue;
            }
            Model.微店_per permodel = JsonConvert.DeserializeObject<Model.微店_per>(resu.ResponseContent);
            string per_url = permodel.result.aliPay.payUrl;
            info.url = per_url;
            info.msg = "创建订单成功，链接提取成功";
            UPDATE(info);
            log(info);
            INSERT(info, orderid, items_str, per_url);
            if (COMM.isgz)
              guanzhu(info);
            addresid = null;
            res = null;
            orderAmount = null;
            items_str = null;
            dic = null;
            paymodel = null;
            payurl = null;
            orderid = null;
            sp = null;
            ct = null;
            tk = null;
            cts = null;
            cookies = null;
            resu = null;
            permodel = null;
            per_url = null;
          }
          catch (Exception ex1)
          {
            Exception ex = ex1;
            info.msg = "下单异常";
            log(info);
            UPDATE(info);
          }
          info = null;
          ims = null;
        }
      }
      list = null;
    }

    public void INSERT(wdauth info, string orderid, string items_str, string per_url)
    {
      try
      {
        MYSQL.exp("insert into wd (phone,shopid,orderid,itemid,cookie,payurl,status,remark,type,times,token) values ('" + info.phone + "','" + COMM.shopid + "','" + orderid + "','" + items_str + "','" + info.nickName + "，" + info.original + "，" + info.loginToken + "，" + info.refreshToken + "，" + info.phone + "，" + info.duid + "，" + info.sid + "，" + info.uid + "','" + per_url + "','N','" + remark + "','K','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + COMM.TOKENS + "')");
      }
      catch (Exception ex)
      {
      }
    }

    public double GetRandomNumber(double minimum, double maximum, int Len)
    {
      return Math.Round(new Random().NextDouble() * (maximum - minimum) + minimum, Len);
    }

    public async Task<double> gj(string orderid)
    {
      double monery = GetRandomNumber(Convert.ToDouble(COMM.price.Split('-')[0]), Convert.ToDouble(COMM.price.Split('-')[1]), 2);
      try
      {
        var objs = new
        {
          orderId = long.Parse(orderid),
          totalItemsPrice = monery.ToString("f2"),
          expressFee = "0.00"
        };
        string myjson1 = COMM.ToJSON(objs);
        HttpRequestEntity od = await MYHTTP.Result("POST", "https://thor.weidian.com/tradeview/seller.modifyOrder/1.0", 5, new Dictionary<string, string>()
        {
          {
            "param",
            myjson1
          },
          {
            "wdtoken",
            COMM.tokens(COMM.cookie)
          }
        }, COMM.cookies(COMM.cookie));
        if (od == null || string.IsNullOrWhiteSpace(od.ResponseContent))
          return 0.0;
        JObject json = COMM.GetToJsonList(od.ResponseContent);
        return !(json["status"]["code"].ToString() == "0") || !(json["status"]["message"].ToString() == "OK") ? 0.0 : monery;
      }
      catch (Exception ex)
      {
        return 0.0;
      }
    }

    private async void guanzhu(wdauth info)
    {
      try
      {
        try
        {
          Dictionary<string, string> dic = new Dictionary<string, string>();
          dic.Add("param", "{\"shopId\":\"" + COMM.shopid + "\"}");
          dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
          string str = await flurl.POST("https://thor.weidian.com/detail/addShopCollect/1.0", dic);
          dic = null;
        }
        catch (Exception ex)
        {
        }
      }
      catch (Exception ex)
      {
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

    private async Task<string> addres(wdauth item)
    {
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
          return null;
        }
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
          strArray[5] = Convert.ToInt32(json["result"]["province"].ToString()).ToString();
          strArray[6] = ",\"city\":";
          int int32 = Convert.ToInt32(json["result"]["city"].ToString());
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
            return null;
          string addresid = COMM.GetToJsonList(res)["result"]["id"].ToString();
          item.addres = addresid;
          UPDATE(item);
          return addresid;
        }
        log(str);
        return null;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<string> GetAddres(wdauth item)
    {
      string id = string.Empty;
      try
      {
        string od = await flurl.POST("https://thor.weidian.com/address/buyerGetAddressList/1.0", new Dictionary<string, string>()
        {
          {
            "param",
            "{\"page_num\":0,\"page_size\":50,\"keyword\":\"\",\"bizType\":0}"
          },
          {
            "context",
            "{\"appid\":\"wxbuyer\",\"platform\":\"ios\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"token\":\"" + item.loginToken + "\",\"duid\":\"" + item.duid + "\",\"uss\":\"" + item.loginToken + "\",\"userID\":\"" + item.duid + "\",\"wduserID\":\"" + item.sid + "\",\"wxappid\":\"wx4b74228baa15489a\"}"
          }
        });
        if (string.IsNullOrWhiteSpace(od))
          return null;
        JObject json = COMM.GetToJsonList(od);
        if (!(json["status"]["code"].ToString() == "0") || !(json["status"]["message"].ToString() == "OK"))
          return null;
        if (json["result"] != null)
        {
          JToken list = json["result"];
          if (list.Count<JToken>() > 4)
          {
            id = list[new Random(Guid.NewGuid().GetHashCode()).Next(list.Count<JToken>())]["id"].ToString();
            return id;
          }
          id = "1";
          return id;
        }
        id = "1";
        return id;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public List<Model.微店_odItem_list> SKU()
    {
      int int32_1 = Convert.ToInt32(COMM.order_count.Split('-')[0]);
      int int32_2 = Convert.ToInt32(COMM.order_count.Split('-')[1]);
      List<Model.微店_odItem_list> 微店OdItemListList = new List<Model.微店_odItem_list>();
      foreach (KeyValuePair<string, List<sku>> keyValuePair in param_dic)
      {
        List<sku> skuList = keyValuePair.Value;
        sku sku = skuList[new Random(Guid.NewGuid().GetHashCode()).Next(skuList.Count)];
        int num = new Random(Guid.NewGuid().GetHashCode()).Next(int32_1, int32_2);
        微店OdItemListList.Add(new Model.微店_odItem_list()
        {
          item_id = keyValuePair.Key,
          quantity = num,
          item_sku_id = sku.id
        });
      }
      return 微店OdItemListList;
    }

    public Model.微店_od1 快递下单参数组合(wdauth info, string duid, int addresid)
    {
      Model.微店_od1 微店Od1 = new Model.微店_od1()
      {
        channel = "xiaochengxu",
        source_id = COMM.shop_param.Split('-')[1],
        q_pv_id = COMM.shop_param.Split('-')[2],
        biz_type = 1,
        buyer = new Model.微店_odBuyer1()
        {
          buyer_id = duid,
          eat_in_table_name = "",
          address_id = addresid
        },
        deliver_type = 0,
        is_no_ship_addr = 0,
        total_vjifen = "",
        wfr = "",
        appid = "",
        pay_type = 0,
        discount_list = new List<object>(),
        invalid_shop_list = new List<object>()
      };
      List<Model.微店_odItem_list> source = new List<Model.微店_odItem_list>();
      foreach (KeyValuePair<string, List<sku>> keyValuePair in param_dic)
      {
        List<sku> skuList = keyValuePair.Value;
        sku sku = skuList[new Random(Guid.NewGuid().GetHashCode()).Next(skuList.Count)];
        int num1 = new Random(Guid.NewGuid().GetHashCode()).Next(1, 3);
        double num2 = Convert.ToDouble(sku.price);
        double num3 = 0.0;
        List<object> objectList = new List<object>();
        if (sku.限时折扣)
        {
          if (sku.priceDesc == "会员价")
            objectList.Add(new
            {
                discount_fee = Convert.ToDouble(sku.discount_fee),
                extend = (sku.priceKey + "-1_" + keyValuePair.Key),
                id = (sku.priceKey + "-1_1"),
                title = sku.priceDesc
            });
          else if (sku.priceDesc == "限时折扣")
          {
            if (xsdic.ContainsKey(keyValuePair.Key))
            {
              num3 = Convert.ToDouble(sku.disprice);
              string str = xsdic[keyValuePair.Key];
              objectList.Add(new
              {
                  discount_fee = Convert.ToDouble(sku.discount_fee),
                  extend = (sku.priceKey + "-" + str.Split(',')[0] + "_" + keyValuePair.Key),
                  id = (sku.priceKey + "-" + str.Split(',')[0] + "_" + str.Split(',')[1]),
                  title = sku.priceDesc
              });
            }
            else
              continue;
          }
          else
            log("当前" + sku.priceDesc + "优惠未添加，请联系开发者添加该优惠，或关闭此优惠刷单");
        }
        source.Add(new Model.微店_odItem_list()
        {
          item_id = keyValuePair.Key,
          quantity = num1,
          item_sku_id = sku.id,
          ori_price = num2.ToString("f2"),
          price = sku.限时折扣 ? num3.ToString("f2") : num2.ToString("f2"),
          price_type = 1,
          extend = new Model.微店_odExtend(),
          discount_list = objectList,
          item_convey_info = new Model.微店_odItem_convey_info()
        });
      }
      List<Model.微店_odShop_list1> 微店OdShopList1List1 = new List<Model.微店_odShop_list1>();
      List<Model.vv> vvList = new List<Model.vv>();
      double num = source.Sum<Model.微店_odItem_list>(p => Convert.ToDouble(p.price) * p.quantity);
      string str1 = num.ToString("f2");
      string empty = string.Empty;
      if (mjdic != null && mjdic.Count > 0)
      {
        vvList.Add(new Model.vv() { id = COMM.满减id });
        foreach (KeyValuePair<double, double> keyValuePair in mjdic)
        {
          if (Convert.ToDouble(str1) > keyValuePair.Key)
          {
            num = keyValuePair.Value;
            empty = num.ToString("f2");
          }
        }
        if (!string.IsNullOrWhiteSpace(empty))
        {
          num = Convert.ToDouble(str1) - Convert.ToDouble(empty);
          str1 = num.ToString("f2");
        }
      }
      List<Model.微店_odShop_list1> 微店OdShopList1List2 = 微店OdShopList1List1;
      Model.微店_odShop_list1 微店OdShopList1 = new Model.微店_odShop_list1();
      微店OdShopList1.shop_id = COMM.sid(COMM.cookie);
      微店OdShopList1.f_shop_id = "";
      微店OdShopList1.sup_id = "";
      微店OdShopList1.item_list = source;
      微店OdShopList1.order_type = 3;
      num = source.Sum<Model.微店_odItem_list>(p => Convert.ToDouble(p.ori_price) * p.quantity);
      微店OdShopList1.ori_price = num.ToString("f2");
      微店OdShopList1.price = str1;
      微店OdShopList1.express_fee = "0.00";
      微店OdShopList1.express_type = 4;
      微店OdShopList1.discount_list = new List<Model.vv>();
      微店OdShopList1.invalid_item_list = new List<Model.Invalid_item_list>();
      微店OdShopList1List2.Add(微店OdShopList1);
      微店Od1.shop_list = 微店OdShopList1List1;
      微店Od1.total_pay_price = str1;
      return 微店Od1;
    }

    public Model.微店_od 自提下单参数组合(wdauth info, string duid, string phone)
    {
      Model.微店_od 微店Od = new Model.微店_od()
      {
        channel = "xiaochengxu",
        source_id = COMM.shop_param.Split('-')[1],
        q_pv_id = COMM.shop_param.Split('-')[2],
        biz_type = 1,
        buyer = new Model.微店_odBuyer()
        {
          buyer_id = duid,
          eat_in_table_name = "",
          self_address_id = Convert.ToInt32(COMM.shop_param.Split('-')[3]),
          buyer_telephone = phone,
          buyer_name = new GetChineseNames().GetTestData()
        },
        deliver_type = 1,
        is_no_ship_addr = 0,
        total_vjifen = "",
        wfr = "",
        appid = "",
        pay_type = 0,
        discount_list = new List<object>(),
        invalid_shop_list = new List<object>()
      };
      List<Model.微店_odItem_list> source = new List<Model.微店_odItem_list>();
      foreach (KeyValuePair<string, List<sku>> keyValuePair in param_dic)
      {
        List<sku> skuList = keyValuePair.Value;
        sku sku = skuList[new Random(Guid.NewGuid().GetHashCode()).Next(skuList.Count)];
        int num1 = new Random(Guid.NewGuid().GetHashCode()).Next(1, 1);
        double num2 = Convert.ToDouble(sku.price);
        double num3 = 0.0;
        List<object> objectList = new List<object>();
        if (sku.限时折扣)
        {
          if (sku.priceDesc == "会员价")
            objectList.Add(new
            {
                discount_fee = Convert.ToDouble(sku.discount_fee),
                extend = (sku.priceKey + "-1_" + keyValuePair.Key),
                id = (sku.priceKey + "-1_1"),
                title = sku.priceDesc
            });
          else if (sku.priceDesc == "限时折扣")
          {
            if (xsdic.ContainsKey(keyValuePair.Key))
            {
              num3 = Convert.ToDouble(sku.disprice);
              string str = xsdic[keyValuePair.Key];
              objectList.Add(new
              {
                  discount_fee = Convert.ToDouble(sku.discount_fee),
                  extend = (sku.priceKey + "-" + str.Split(',')[0] + "_" + keyValuePair.Key),
                  id = (sku.priceKey + "-" + str.Split(',')[0] + "_" + str.Split(',')[1]),
                  title = sku.priceDesc
              });
            }
            else
              continue;
          }
          else
            log("当前" + sku.priceDesc + "优惠未添加，请联系开发者添加该优惠，或关闭此优惠刷单");
        }
        source.Add(new Model.微店_odItem_list()
        {
          item_id = keyValuePair.Key,
          quantity = num1,
          item_sku_id = sku.id,
          ori_price = num2.ToString("f2"),
          price = sku.限时折扣 ? num3.ToString("f2") : num2.ToString("f2"),
          price_type = 1,
          extend = new Model.微店_odExtend(),
          discount_list = objectList,
          item_convey_info = new Model.微店_odItem_convey_info()
        });
      }
      List<Model.微店_odShop_list> 微店OdShopListList1 = new List<Model.微店_odShop_list>();
      List<Model.vv> vvList = new List<Model.vv>();
      double num = source.Sum<Model.微店_odItem_list>(p => Convert.ToDouble(p.price) * p.quantity);
      string str1 = num.ToString("f2");
      string empty = string.Empty;
      if (mjdic != null && mjdic.Count > 0)
      {
        vvList.Add(new Model.vv() { id = COMM.满减id });
        foreach (KeyValuePair<double, double> keyValuePair in mjdic)
        {
          if (Convert.ToDouble(str1) > keyValuePair.Key)
          {
            num = keyValuePair.Value;
            empty = num.ToString("f2");
          }
        }
        if (!string.IsNullOrWhiteSpace(empty))
        {
          num = Convert.ToDouble(str1) - Convert.ToDouble(empty);
          str1 = num.ToString("f2");
        }
      }
      List<Model.微店_odShop_list> 微店OdShopListList2 = 微店OdShopListList1;
      Model.微店_odShop_list 微店OdShopList = new Model.微店_odShop_list();
      微店OdShopList.shop_id = COMM.sid(COMM.cookie);
      微店OdShopList.f_shop_id = "";
      微店OdShopList.sup_id = "";
      微店OdShopList.item_list = source;
      微店OdShopList.order_type = 3;
      num = source.Sum<Model.微店_odItem_list>(p => Convert.ToDouble(p.ori_price) * p.quantity);
      微店OdShopList.ori_price = num.ToString("f2");
      微店OdShopList.price = str1;
      微店OdShopList.express_fee = "0.00";
      微店OdShopList.express_type = "";
      微店OdShopList.discount_list = vvList;
      微店OdShopList.invalid_item_list = new List<Model.Invalid_item_list>();
      微店OdShopListList2.Add(微店OdShopList);
      微店Od.shop_list = 微店OdShopListList1;
      微店Od.total_pay_price = str1;
      return 微店Od;
    }

    public async Task<bool> delete(wdauth info)
    {
      bool flog = false;
      try
      {
        StringBuilder ids1 = new StringBuilder();
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("param", "{\"source\":\"h5\",\"v_seller_id\":\"\",\"tabKey\":\"all\"}");
        dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
        string res = await flurl.POST("https://thor.weidian.com/vcart/getListCart/3.0", dic);
        if (!string.IsNullOrWhiteSpace(res))
        {
          JObject md = COMM.GetToJsonList(res);
          if (string.IsNullOrWhiteSpace(md["result"]["shops"].ToString()))
          {
            flog = true;
          }
          else
          {
            foreach (JToken mds in md["result"]["shops"][0]["partitions"][0]["itemList"])
            {
              string id = mds["id"].ToString();
              ids1.Append(id + ",");
              id = null;
            }
            string ids = ids1.ToString().Substring(0, ids1.ToString().Length - 1);
            dic = new Dictionary<string, string>();
            dic.Add("param", "{\"source\":\"h5\",\"promotionIdList\":[],\"idList\":[" + ids + "]}");
            dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
            res = await flurl.POST("https://thor.weidian.com/vcart/deleteCart/2.0", dic);
            if (!string.IsNullOrWhiteSpace(res) && res.Contains("OK") && res.Contains("result"))
              flog = true;
            ids = null;
          }
          md = null;
        }
        ids1 = null;
        dic = null;
        res = null;
      }
      catch (Exception ex1)
      {
        Exception ex = ex1;
      }
      return flog;
    }

    public async Task<bool> juan(wdauth info, string str)
    {
      bool flog = false;
      try
      {
        StringBuilder ids1 = new StringBuilder();
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("param", "{\"querySource\":\"confirmOrder\",\"itemIdList\":[" + str + "],\"queryChannel\":\"wxProgram\",\"expressType\":\"EXPRESS\"}");
        dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
        string res = await flurl.POST("https://thor.weidian.com/vmpcoupon/buyerShopCoupon.queryTheoreticallyAvailableCoupon/1.0", dic);
        if (!string.IsNullOrWhiteSpace(res))
        {
          JObject md = COMM.GetToJsonList(res);
          if (string.IsNullOrWhiteSpace(md["result"]["alreadyFetchedBuyerCouponList"].ToString()))
          {
            flog = true;
          }
          else
          {
            foreach (JToken mds in md["result"]["unFetchedBuyerCouponList"])
            {
              string id = mds["couponId"].ToString();
              dic = new Dictionary<string, string>();
              dic.Add("param", "{\"shop_id\":" + COMM.sid(COMM.cookie) + ",\"coupon_id\":" + Convert.ToInt32(id).ToString() + ",\"type\":\"h5\"}");
              dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
              res = await flurl.POST("https://thor.weidian.com/vmpcoupon/fetchShopCoupon/1.0", dic);
              id = null;
            }
            flog = true;
          }
          md = null;
        }
        ids1 = null;
        dic = null;
        res = null;
      }
      catch (Exception ex1)
      {
        Exception ex = ex1;
      }
      return flog;
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

    private void 免登录打开ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (dataGridView1 == null || dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
          return;
        int a = dataGridView1.CurrentRow.Index;
        List<wdauth> auth = authlist.Where<wdauth>(p => p.phone == dataGridView1.Rows[a].Cells["账号"].Value.ToString()).ToList<wdauth>();
        if (auth == null)
        {
          log("账号不存在");
        }
        else
        {
          string loginToken = auth[0].loginToken;
          string duid = auth[0].duid;
          string sid = auth[0].sid;
          string uid = auth[0].uid;
          string cookie = "wdtoken=" + Guid.NewGuid().ToString().ToLower().Substring(8) + ";__spider__visitorid=" + COMM.r(16) + ";__spider__sessionid=" + COMM.r(16) + ";v-components/cpn-coupon-dialog@iwdDefault=1;login_token=" + loginToken + ";uid=" + uid + ";is_login=true;login_type=LOGIN_USER_TYPE_WECHAT;login_source=LOGIN_USER_SOURCE_WECHAT;duid=" + duid + ";sid=" + sid;
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
                    log(auth[0].phone + "免登录打开成功");
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
      DataTable dataSource = (DataTable) dataGridView1.DataSource;
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
          if (item.Cells[0].Value != null && (bool) item.Cells[0].Value)
          {
            List<wdauth> list = authlist.Where<wdauth>(p => p.phone == item.Cells["账号"].Value.ToString()).ToList<wdauth>();
            if (list == null || list.Count == 0)
              log(item.Cells["账号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
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
          X = Convert.ToInt32(textBox10.Text);
          F = Convert.ToInt32(textBox1.Text);
          flurl = new flurl();
          if (wdauthList.Count < 10)
          {
            new Thread(new ParameterizedThreadStart(updates)).Start(wdauthList);
          }
          else
          {
            int num = wdauthList.Count / X;
            foreach (KeyValuePair<string, List<wdauth>> split in COMM.SplitList(wdauthList, num))
              new Thread(new ParameterizedThreadStart(updates)).Start(split.Value);
          }
        }
      }
    }

    private async void updates(object obj)
    {
      List<wdauth> list = (List<wdauth>) obj;
      foreach (wdauth item in list)
      {
        try
        {
          Dictionary<string, string> dic = new Dictionary<string, string>();
          dic.Add("param", "{}");
          dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"ios\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"token\":\"" + item.loginToken + "\",\"duid\":\"" + item.duid + "\",\"uss\":\"" + item.loginToken + "\",\"userID\":\"" + item.duid + "\",\"wduserID\":\"" + item.sid + "\",\"wxappid\":\"wx4b74228baa15489a\"}");
          string od = await flurl.POST("https://thor.weidian.com/ark/my.info/1.1", dic);
          if (!string.IsNullOrWhiteSpace(od) && od.Contains("成功"))
          {
            string uname = COMM.GetToJsonList(od)["result"]["userInfo"]["userNickName"].ToString();
            MYSQL.exp("update wd_user1 set nickname='" + uname + "' where id=" + item.id);
            log(item.phone + uname + "更新成功");
            uname = null;
          }
          dic = null;
          od = null;
        }
        catch (Exception ex)
        {
        }
      }
      list = null;
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

    private void button5_Click_1(object sender, EventArgs e)
    {
      if (flurl == null)
        flurl = new flurl();
      if (authlist == null || authlist.Count == 0)
        log("请导入账号");
      else if (string.IsNullOrWhiteSpace(textBox10.Text))
      {
        log("请输入线程数量");
      }
      else
      {
        DataTable dataSource = (DataTable) dataGridView1.DataSource;
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
            if (item.Cells[0].Value != null && (bool) item.Cells[0].Value)
            {
              List<wdauth> list = authlist.Where(p => p.original == item.Cells["编号"].Value.ToString()).ToList();
              if (list == null || list.Count == 0)
                log(item.Cells["编号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
              else
                wdauthList.Add(list.First());
            }
          }
          if (wdauthList == null || wdauthList.Count == 0)
          {
            log("请勾选需要复活的账号");
          }
          else
          {
            X = Convert.ToInt32(textBox10.Text);
            if (wdauthList.Count < 10)
            {
              new Thread(new ParameterizedThreadStart(fuhuo1)).Start(wdauthList);
            }
            else
            {
              int num = wdauthList.Count / X;
              foreach (KeyValuePair<string, List<wdauth>> split in COMM.SplitList(wdauthList, num))
                new Thread(new ParameterizedThreadStart(fuhuo1)).Start(split.Value);
            }
          }
        }
      }
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

    private async void fuhuo(object obj)
    {
      List<wdauth> list = (List<wdauth>) obj;
      foreach (wdauth ims in list)
      {
        wdauth infos = ims;
        try
        {
          try
          {
            string og = infos.original;
            string result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=LoCNXKAoij7lLQvT8a04uqlcclrTdC&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
            JObject json = COMM.GetToJsonList(result);
            if (json != null && json["code"] != null && json["code"].ToString() == "20000" && json["message"] != null && json["message"].ToString() == "授权成功")
            {
              string original = json["data"]["orderId"].ToString();
              string info = json["data"]["callback"].ToString().Replace("授权成功：", "");
              JObject myjson = COMM.GetToJsonList(info);
              string code = myjson["code"].ToString();
              if (string.IsNullOrWhiteSpace(code))
              {
                result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=LoCNXKAoij7lLQvT8a04uqlcclrTdC&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
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
              dic.Add("context", "{\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\"}");
              string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
              JObject so = COMM.GetToJsonList(res);
              string uid = so["result"]["uid"].ToString();
              string sid = so["result"]["sid"].ToString();
              string duid = so["result"]["duid"].ToString();
              string loginToken = so["result"]["loginToken"].ToString();
              string refreshToken = so["result"]["refreshToken"].ToString();
              MYSQL.exp("update wd_user1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
              infos.msg = "复活成功";
              log(infos);
              UPDATE(infos);
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
            else if (json != null && result.Contains("订单过期"))
            {
              Thread.Sleep(5000);
              result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=LoCNXKAoij7lLQvT8a04uqlcclrTdC&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
              json = COMM.GetToJsonList(result);
              if (json != null && json["code"] != null && json["code"].ToString() == "20000" && json["message"] != null && json["message"].ToString() == "授权成功")
              {
                string original = json["data"]["orderId"].ToString();
                string info = json["data"]["callback"].ToString().Replace("授权成功：", "");
                JObject myjson = COMM.GetToJsonList(info);
                string code = myjson["code"].ToString();
                if (string.IsNullOrWhiteSpace(code))
                {
                  result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=LoCNXKAoij7lLQvT8a04uqlcclrTdC&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
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
                dic.Add("context", "{\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\"}");
                string res = await flurl.POST("https://thor.weidian.com/passport/login.wechatphone/1.0", dic);
                JObject so = COMM.GetToJsonList(res);
                string uid = so["result"]["uid"].ToString();
                string sid = so["result"]["sid"].ToString();
                string duid = so["result"]["duid"].ToString();
                string loginToken = so["result"]["loginToken"].ToString();
                string refreshToken = so["result"]["refreshToken"].ToString();
                MYSQL.exp("update wd_user1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
                infos.msg = "复活成功";
                log(infos);
                UPDATE(infos);
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
            }
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
        infos = null;
      }
      list = null;
    }

    private async void 派大星复活(object obj)
    {
      List<wdauth> list = (List<wdauth>) obj;
      foreach (wdauth ims in list)
      {
        wdauth infos = ims;
        try
        {
          try
          {
            string og = infos.original;
            string result = await flurl.API_GET("http://202.189.11.23:4399/api/auth?account=lbk921486&pwd=346563144a&appId=wx4b74228baa15489a&id=" + og + "&channel=1&authType=1&url=");
            JObject json = COMM.GetToJsonList(result);
            string info = json["data"]["code"].ToString();
            string original = og;
            JObject myjson = COMM.GetToJsonList(info);
            string code = myjson["code"].ToString();
            if (string.IsNullOrWhiteSpace(code))
            {
              result = await flurl.API_GET("http://202.189.11.23:4399/api/auth?account=lbk921486&pwd=346563144a&appId=wx4b74228baa15489a&id=" + og + "&channel=1&authType=1&url=");
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
            MYSQL.exp("update wd_user1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
            infos.msg = "复活成功";
            log(infos);
            UPDATE(infos);
            og = null;
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
        catch (Exception ex)
        {
        }
        infos = null;
      }
      list = null;
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

    private async void fuhuo1(object obj)
    {
      List<wdauth> list = (List<wdauth>) obj;
      foreach (wdauth ims in list)
      {
        wdauth infos = ims;
        try
        {
          try
          {
            string token = "wB7K2XYyolX6uKRmS31wWyVA52kOch";
            string og = infos.original;
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
              MYSQL.exp("update wd_user1 set loginToken='" + loginToken + "',refreshToken='" + refreshToken + "',sid='" + sid + "',uid='" + uid + "',datetimes='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where original='" + og + "'");
              infos.msg = "复活成功";
              log(infos);
              UPDATE(infos);
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
        infos = null;
      }
      list = null;
    }

    private async void fuhuo3(object obj)
    {
      List<string> list = (List<string>) obj;
      foreach (string ims in list)
      {
        string infos = ims;
        try
        {
          try
          {
            string token = "wB7K2XYyolX6uKRmS31wWyVA52kOch";
            string og = infos;
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
              string phone = getRandomTel();
              string duid = so["result"]["duid"].ToString();
              string loginToken = so["result"]["loginToken"].ToString();
              string refreshToken = so["result"]["refreshToken"].ToString();
              MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,remakr,status,datetimes) values ('" + nickName + "','" + original + "','" + loginToken + "','" + refreshToken + "','" + phone + "','" + duid + "','" + sid + "','" + uid + "','v01','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
              log("插入成功");
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
              phone = null;
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
        infos = null;
      }
      list = null;
    }

    public string getRandomTel()
    {
      Random random = new Random((int) DateTime.Now.Ticks);
      random.Next(10, 1000);
      return telStarts[random.Next(0, telStarts.Length - 1)] + (random.Next(100, 888) + 10000).ToString().Substring(1) + (random.Next(1, 9100) + 10000).ToString().Substring(1);
    }

    private async void fuhuo2(object obj)
    {
      List<string> list = (List<string>) obj;
      foreach (string ims in list)
      {
        string infos = ims;
        try
        {
          try
          {
            string og = infos;
            string result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=LoCNXKAoij7lLQvT8a04uqlcclrTdC&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
            JObject json = COMM.GetToJsonList(result);
            if (json != null && json["code"] != null && json["code"].ToString() == "20000" && json["message"] != null && json["message"].ToString() == "授权成功")
            {
              string original = json["data"]["orderId"].ToString();
              string info = json["data"]["callback"].ToString().Replace("授权成功：", "");
              JObject myjson = COMM.GetToJsonList(info);
              string code = myjson["code"].ToString();
              if (string.IsNullOrWhiteSpace(code))
              {
                result = await flurl.API_GET("http://sha.mrlj.cn:8881/createTaskApiSync?token=LoCNXKAoij7lLQvT8a04uqlcclrTdC&appId=71953&qrCode=1&orderId=" + og + "&miniAuthType=2");
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
              MYSQL.exp("insert into wd_user1 (nickname,original,loginToken,refreshToken,phone,duid,sid,uid,remakr,status,datetimes) values ('" + nickName + "','" + original + "','" + loginToken + "','" + refreshToken + "','" + phone + "','" + duid + "','" + sid + "','" + uid + "','20240113','Y','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
              log("复活成功");
              original = null;
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
        catch (Exception ex)
        {
        }
        infos = null;
      }
      list = null;
    }

    public static bool yh { get; set; }

    private async void button7_Click_1(object sender, EventArgs e)
    {
      HttpRequestEntity resu;
      if (string.IsNullOrWhiteSpace(COMM.cookie))
      {
        log("请登录主店铺");
        resu = null;
      }
      else
      {
                mjdic = new Dictionary<double, double>();
                xsdic = new Dictionary<string, string>();
        resu = await MYHTTP.Result("GET", "https://thor.weidian.com/promotion/market.fullCut.getFullCutPresentList/1.0?param=%7B%22searchType%22%3A2%2C%22status%22%3A1%2C%22activityName%22%3A%22%22%2C%22pageNum%22%3A1%2C%22pageSize%22%3A10%7D&wdtoken=04f9cf5d&_=1688234387850", 4, null, COMM.cookie);
        if (resu != null && !string.IsNullOrWhiteSpace(resu.ResponseContent) && resu.ResponseContent.Contains("promotionDesc"))
        {
          JObject js = COMM.GetToJsonList(resu.ResponseContent);
          JToken list = js["result"]["fullCutActivityList"][0]["fullCutActivity"]["promotionDesc"];
          JToken activityId = js["result"]["fullCutActivityList"][0]["fullCutActivity"]["activityId"];
          JToken detailId = js["result"]["fullCutActivityList"][0]["fullCutActivity"]["detailId"];
          COMM.满减id = string.Format("shopMj-{0}_{1}", activityId, detailId);
          foreach (JToken item in list)
          {
            string[] sp = item.ToString().Split('，');
                        mjdic.Add(Convert.ToDouble(sp[0].Replace("满", "").Replace("元", "")), Convert.ToDouble(sp[1].Replace("满", "").Replace("减", "").Replace("元", "")));
            log("获取满减优惠【" + item.ToString() + "】");
            sp = null;
          }
          js = null;
          list = null;
          activityId = null;
          detailId = null;
        }
        resu = await MYHTTP.Result("GET", "https://thor.weidian.com/promotion/limitdiscount.seller.pc.queryLimitDiscountActivityList/1.0?param=%7B%22pageNum%22%3A1%2C%22pageSize%22%3A10%2C%22searchType%22%3A2%2C%22searchKey%22%3A%22%22%2C%22status%22%3A2%7D&wdtoken=04f9cf5d&_=1688234420252", 4, null, COMM.cookie);
        if (resu != null && !string.IsNullOrWhiteSpace(resu.ResponseContent) && resu.ResponseContent.Contains("activityId"))
        {
          JObject js = COMM.GetToJsonList(resu.ResponseContent);
          string activityId = js["result"]["activityList"][0]["activityId"].ToString();
          for (int i = 0; i < 100; ++i)
          {
            resu = await MYHTTP.Result("GET", "https://thor.weidian.com/promotion/limitdiscount.seller.queryLimitDiscountActivityDetail/1.0?param=%7B%22pageNum%22%3A" + i.ToString() + "%2C%22pageSize%22%3A200%2C%22activityId%22%3A" + activityId + "%7D&wdtoken=04f9cf5d&_=1688233597921", 4, null, COMM.cookie);
            if (resu != null && !string.IsNullOrWhiteSpace(resu.ResponseContent) && resu.ResponseContent.Contains("activityId"))
            {
              JObject json = COMM.GetToJsonList(resu.ResponseContent);
              JToken list = json["result"]["items"];
              if (list != null && list.Count<JToken>() != 0 && !(list.ToString() == "[]"))
              {
                foreach (JToken item in list)
                {
                                    xsdic.Add(item["itemId"].ToString(), activityId + "," + item["discountId"].ToString());
                  log("获取限时优惠【" + item["discountId"].ToString() + "】");
                }
                json = null;
                list = null;
              }
              else
                break;
            }
          }
          js = null;
          activityId = null;
        }
                yh = true;
        log("优惠获取完毕");
        resu = null;
      }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
    {
    }

    private void button8_Click(object sender, EventArgs e)
    {
      if (authlist == null || authlist.Count == 0)
        log("请导入账号");
      else if (string.IsNullOrWhiteSpace(COMM.shopid))
        log("请输入店铺ID");
      else if (string.IsNullOrWhiteSpace(textBox10.Text))
      {
        log("请输入线程数量");
      }
      else
      {
        DataTable dataSource = (DataTable) dataGridView1.DataSource;
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
            if (item.Cells[0].Value != null && (bool) item.Cells[0].Value)
            {
              List<wdauth> list = authlist.Where<wdauth>(p => p.original == item.Cells["编号"].Value.ToString()).ToList<wdauth>();
              if (list == null || list.Count == 0)
                log(item.Cells["编号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
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
            flurl = new flurl();
            X = Convert.ToInt32(textBox10.Text);
            F = Convert.ToInt32(textBox1.Text);
            if (wdauthList.Count < 10)
            {
              new Thread(new ParameterizedThreadStart(gz)).Start(wdauthList);
            }
            else
            {
              int num = wdauthList.Count / X;
              foreach (KeyValuePair<string, List<wdauth>> split in COMM.SplitList(wdauthList, num))
                new Thread(new ParameterizedThreadStart(gz)).Start(split.Value);
            }
          }
        }
      }
    }

    private async void gz(object obj)
    {
      List<wdauth> list = (List<wdauth>) obj;
      foreach (wdauth ims in list)
      {
        wdauth info = ims;
        try
        {
          Dictionary<string, string> dic = new Dictionary<string, string>();
          dic.Add("param", "{\"shopId\":\"" + COMM.shopid + "\"}");
          dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + info.loginToken + "\",\"duid\":\"" + info.duid + "\",\"uss\":\"" + info.loginToken + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + info.duid + "\",\"wduserID\":\"" + info.sid + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
          string res = await flurl.POST("https://thor.weidian.com/detail/addShopCollect/1.0", dic);
          info.msg = res;
          log(info);
          UPDATE(info);
          dic = null;
          res = null;
        }
        catch (Exception ex1)
        {
          Exception ex = ex1;
          info.msg = "关注异常";
          log(info);
          UPDATE(info);
        }
        info = null;
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
      components = new System.ComponentModel.Container();
      panel1 = new Panel();
      textBox7 = new TextBox();
      panel2 = new Panel();
      button8 = new Button();
      textBox4 = new TextBox();
      button7 = new Button();
      button5 = new Button();
      label5 = new Label();
      label4 = new Label();
      textBox3 = new TextBox();
      comboBox2 = new ComboBox();
      label3 = new Label();
      textBox5 = new TextBox();
      textBox2 = new TextBox();
      label2 = new Label();
      button6 = new Button();
      button4 = new Button();
      textBox1 = new TextBox();
      label1 = new Label();
      textBox10 = new TextBox();
      label10 = new Label();
      button3 = new Button();
      button2 = new Button();
      button1 = new Button();
      comboBox1 = new ComboBox();
      dataGridView1 = new DataGridView();
      编号 = new DataGridViewTextBoxColumn();
      账号 = new DataGridViewTextBoxColumn();
      昵称 = new DataGridViewTextBoxColumn();
      下单类型 = new DataGridViewTextBoxColumn();
      状态 = new DataGridViewTextBoxColumn();
      支付链接 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      contextMenuStrip1 = new ContextMenuStrip(components);
      免登录打开ToolStripMenuItem = new ToolStripMenuItem();
      panel1.SuspendLayout();
      panel2.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      contextMenuStrip1.SuspendLayout();
      SuspendLayout();
      panel1.Controls.Add(textBox7);
      panel1.Dock = DockStyle.Bottom;
      panel1.Location = new Point(0, 413);
      panel1.Name = "panel1";
      panel1.Size = new Size(914, 125);
      panel1.TabIndex = 0;
      textBox7.BackColor = SystemColors.ActiveCaptionText;
      textBox7.Dock = DockStyle.Fill;
      textBox7.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
      textBox7.ForeColor = Color.YellowGreen;
      textBox7.Location = new Point(0, 0);
      textBox7.Multiline = true;
      textBox7.Name = "textBox7";
      textBox7.ReadOnly = true;
      textBox7.ScrollBars = ScrollBars.Vertical;
      textBox7.Size = new Size(914, 125);
      textBox7.TabIndex = 7;
      panel2.Controls.Add(button8);
      panel2.Controls.Add(textBox4);
      panel2.Controls.Add(button7);
      panel2.Controls.Add(button5);
      panel2.Controls.Add(label5);
      panel2.Controls.Add(label4);
      panel2.Controls.Add(textBox3);
      panel2.Controls.Add(comboBox2);
      panel2.Controls.Add(label3);
      panel2.Controls.Add(textBox5);
      panel2.Controls.Add(textBox2);
      panel2.Controls.Add(label2);
      panel2.Controls.Add(button6);
      panel2.Controls.Add(button4);
      panel2.Controls.Add(textBox1);
      panel2.Controls.Add(label1);
      panel2.Controls.Add(textBox10);
      panel2.Controls.Add(label10);
      panel2.Controls.Add(button3);
      panel2.Controls.Add(button2);
      panel2.Controls.Add(button1);
      panel2.Controls.Add(comboBox1);
      panel2.Dock = DockStyle.Left;
      panel2.Location = new Point(0, 0);
      panel2.Name = "panel2";
      panel2.Size = new Size(200, 413);
      panel2.TabIndex = 1;
      button8.Location = new Point(13, 377);
      button8.Name = "button8";
      button8.Size = new Size(170, 25);
      button8.TabIndex = 35;
      button8.Text = "不下单只关注";
      button8.UseVisualStyleBackColor = true;
      button8.Click += new EventHandler(button8_Click);
      textBox4.Location = new Point(13, 136);
      textBox4.Name = "textBox4";
      textBox4.Size = new Size(59, 21);
      textBox4.TabIndex = 0;
      textBox4.Text = "0";
      button7.Location = new Point(13, 42);
      button7.Name = "button7";
      button7.Size = new Size(170, 25);
      button7.TabIndex = 33;
      button7.Text = "获取优惠";
      button7.UseVisualStyleBackColor = true;
      button7.Visible = false;
      button7.Click += new EventHandler(button7_Click_1);
      button5.Location = new Point(13, 347);
      button5.Name = "button5";
      button5.Size = new Size(170, 25);
      button5.TabIndex = 31;
      button5.Text = "复活";
      button5.UseVisualStyleBackColor = true;
      button5.Click += new EventHandler(button5_Click_1);
      label5.AutoSize = true;
      label5.Location = new Point(85, 324);
      label5.Name = "label5";
      label5.Size = new Size(29, 12);
      label5.TabIndex = 30;
      label5.Text = "个号";
      label5.Visible = false;
      label4.AutoSize = true;
      label4.Location = new Point(11, 324);
      label4.Name = "label4";
      label4.Size = new Size(17, 12);
      label4.TabIndex = 29;
      label4.Text = "前";
      label4.Visible = false;
      textBox3.Location = new Point(34, 320);
      textBox3.Name = "textBox3";
      textBox3.Size = new Size(45, 21);
      textBox3.TabIndex = 28;
      textBox3.Text = "50";
      textBox3.Visible = false;
      comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox2.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 134);
      comboBox2.FormattingEnabled = true;
      comboBox2.Items.AddRange(new object[2]
      {
         "隐藏",
         "可见"
      });
      comboBox2.Location = new Point(120, 319);
      comboBox2.Name = "comboBox2";
      comboBox2.Size = new Size(63, 22);
      comboBox2.TabIndex = 32;
      comboBox2.Visible = false;
      label3.AutoSize = true;
      label3.Location = new Point(92, 140);
      label3.Name = "label3";
      label3.Size = new Size(11, 12);
      label3.TabIndex = 21;
      label3.Text = "~";
      textBox5.Location = new Point(124, 136);
      textBox5.Name = "textBox5";
      textBox5.Size = new Size(59, 21);
      textBox5.TabIndex = 1;
      textBox5.Text = "50";
      textBox2.Location = new Point(48, 283);
      textBox2.Name = "textBox2";
      textBox2.Size = new Size(135, 21);
      textBox2.TabIndex = 18;
      label2.AutoSize = true;
      label2.Location = new Point(11, 287);
      label2.Name = "label2";
      label2.Size = new Size(29, 12);
      label2.TabIndex = 17;
      label2.Text = "备注";
      button6.Location = new Point(13, 222);
      button6.Name = "button6";
      button6.Size = new Size(170, 25);
      button6.TabIndex = 16;
      button6.Text = "导入地址";
      button6.UseVisualStyleBackColor = true;
      button6.Click += new EventHandler(button6_Click);
      button4.Location = new Point(13, 252);
      button4.Name = "button4";
      button4.Size = new Size(170, 25);
      button4.TabIndex = 14;
      button4.Text = "开始下单";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      textBox1.Location = new Point(140, 194);
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(43, 21);
      textBox1.TabIndex = 13;
      label1.AutoSize = true;
      label1.Location = new Point(104, 198);
      label1.Name = "label1";
      label1.Size = new Size(29, 12);
      label1.TabIndex = 12;
      label1.Text = "阈值";
      textBox10.Location = new Point(48, 194);
      textBox10.Name = "textBox10";
      textBox10.Size = new Size(43, 21);
      textBox10.TabIndex = 3;
      label10.AutoSize = true;
      label10.Location = new Point(11, 198);
      label10.Name = "label10";
      label10.Size = new Size(29, 12);
      label10.TabIndex = 10;
      label10.Text = "线程";
      button3.Location = new Point(13, 163);
      button3.Name = "button3";
      button3.Size = new Size(170, 25);
      button3.TabIndex = 2;
      button3.Text = "获取账号";
      button3.UseVisualStyleBackColor = true;
      button3.Click += new EventHandler(button3_Click);
      button2.Location = new Point(13, 106);
      button2.Name = "button2";
      button2.Size = new Size(170, 25);
      button2.TabIndex = 3;
      button2.Text = "初始化下单链接";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      button1.Location = new Point(13, 74);
      button1.Name = "button1";
      button1.Size = new Size(170, 25);
      button1.TabIndex = 2;
      button1.Text = "导入下单链接";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox1.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 134);
      comboBox1.FormattingEnabled = true;
      comboBox1.Items.AddRange(new object[4]
      {
         "选择下单方式",
         "快递",
         "商城版自提",
         "非商城版自提"
      });
      comboBox1.Location = new Point(13, 14);
      comboBox1.Name = "comboBox1";
      comboBox1.Size = new Size(170, 22);
      comboBox1.TabIndex = 0;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(编号, 账号, 昵称, 下单类型, 状态, 支付链接);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(200, 0);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(714, 413);
      dataGridView1.TabIndex = 6;
      dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
      编号.DataPropertyName = "编号";
      编号.HeaderText = "编号";
      编号.Name = "编号";
      账号.DataPropertyName = "账号";
      账号.HeaderText = "账号";
      账号.Name = "账号";
      昵称.DataPropertyName = "昵称";
      昵称.HeaderText = "昵称";
      昵称.Name = "昵称";
      下单类型.DataPropertyName = "下单类型";
      下单类型.HeaderText = "下单类型";
      下单类型.Name = "下单类型";
      下单类型.Width = 200;
      状态.DataPropertyName = "状态";
      状态.HeaderText = "状态";
      状态.Name = "状态";
      状态.Width = 150;
      支付链接.DataPropertyName = "支付链接";
      支付链接.HeaderText = "支付链接";
      支付链接.Name = "支付链接";
      支付链接.Width = 300;
      dataGridViewTextBoxColumn1.DataPropertyName = "账号";
      dataGridViewTextBoxColumn1.HeaderText = "账号";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn2.DataPropertyName = "密码";
      dataGridViewTextBoxColumn2.HeaderText = "密码";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn3.DataPropertyName = "COOKIE";
      dataGridViewTextBoxColumn3.HeaderText = "COOKIE";
      dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      dataGridViewTextBoxColumn3.Width = 200;
      dataGridViewTextBoxColumn4.DataPropertyName = "状态";
      dataGridViewTextBoxColumn4.HeaderText = "状态";
      dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      dataGridViewTextBoxColumn4.Width = 150;
      dataGridViewTextBoxColumn5.DataPropertyName = "支付链接";
      dataGridViewTextBoxColumn5.HeaderText = "支付链接";
      dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      dataGridViewTextBoxColumn5.Width = 300;
      dataGridViewTextBoxColumn6.DataPropertyName = "支付链接";
      dataGridViewTextBoxColumn6.HeaderText = "支付链接";
      dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      dataGridViewTextBoxColumn6.Width = 300;
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
      Controls.Add(dataGridView1);
      Controls.Add(panel2);
      Controls.Add(panel1);
      Name ="下单";
      Size = new Size(914, 538);
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      panel2.ResumeLayout(false);
      panel2.PerformLayout();
      ((ISupportInitialize) dataGridView1).EndInit();
      contextMenuStrip1.ResumeLayout(false);
      ResumeLayout(false);
    }

    public delegate void SetTextHandler(string text);

    private delegate void UpdateDataGridView(DataTable dt);
  }
}
