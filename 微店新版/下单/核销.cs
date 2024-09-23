// Decompiled with JetBrains decompiler
// Type: 微店新版.下单.核销
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版.下单
{
  public class 核销 : UserControl
  {
    private bool ischeckbox = false;
    public static List<pay> paylist = new List<pay>();
    private flurl https;
    private flurl flurl;
    public static List<kuaidi> kdlist = new List<kuaidi>();
    public static DataTable dt;
    private IContainer components = null;
    private Panel panel1;
    private Button button8;
    private Button button2;
    private ComboBox comboBox1;
    private Button button4;
    private LinkLabel linkLabel1;
    private ComboBox comboBox2;
    private Panel panel2;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn 账号;
    private DataGridViewTextBoxColumn 订单号;
    private DataGridViewTextBoxColumn 核销状态;
    private TextBox textBox7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private CheckBox checkBox1;
    private LinkLabel linkLabel2;
    private Button button1;

    public 核销() => InitializeComponent();

    private void cbHeader_OnCheckBoxClicked(bool state)
    {
      dataGridView1.EndEdit();
      dataGridView1.Rows.OfType<DataGridViewRow>().ToList<DataGridViewRow>().ForEach(t => t.Cells[0].Value = state);
    }

    private void UpdateGV(DataTable dt)
    {
      if (dataGridView1.InvokeRequired)
      {
        BeginInvoke(new 核销.UpdateDataGridView(UpdateGV), dt);
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
              row.Cells["核销状态"].Value = users.status;
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
        textBox7.Invoke(new 核销.SetTextHandler(log), text);
      else
        textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
    }

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

    private void button4_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(comboBox1.Text))
      {
        log("请选择备注");
      }
      else
      {
        DataTable dataTable = MYSQL.Query("select * from wd where token='" + COMM.TOKENS + "' and remark='" + comboBox1.Text + "' and status='Y'");
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          log("选择的备注没有需要核销的数据");
        }
        else
        {
                    paylist = new List<pay>();
          DataTable dt = new DataTable();
          dt.Columns.Add(new DataColumn("账号", typeof (string)));
          dt.Columns.Add(new DataColumn("订单号", typeof (string)));
          dt.Columns.Add(new DataColumn("核销状态", typeof (string)));
          foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
          {
            DataRow row2 = dt.NewRow();
            row2["账号"] = row1["phone"].ToString();
            row2["订单号"] = row1["orderid"].ToString();
            row2["核销状态"] = "";
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
          log("核销数据获取成功");
          UpdateGV(dt);
        }
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      data();
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBox2.Text == "商城版自提")
        button8.Enabled = false;
      if (comboBox2.Text == "非商城版自提")
        button8.Enabled = false;
      if (!(comboBox2.Text == "快递"))
        return;
      button8.Enabled = true;
    }

    public static string remark { get; set; }

    public static string type { get; set; }

    public static bool shouhuo { get; set; }

    private void button2_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(comboBox2.Text))
        log("请选择核销方式");
      else if (string.IsNullOrWhiteSpace(COMM.ts))
        log("请设置核销间隔");
      else if (string.IsNullOrWhiteSpace(COMM.cookie))
      {
        log("请登录主店铺");
      }
      else
      {
                type = comboBox2.Text;
        if (type == "快递" && (kdlist == null || kdlist.Count == 0))
        {
          log("选择快递发货需要导入快递单号");
        }
        else
        {
                    shouhuo = checkBox1.Checked;
                    remark = comboBox1.Text;
          DataTable dataSource = (DataTable) dataGridView1.DataSource;
          if (dataSource == null || dataSource.Rows.Count == 0)
          {
            log("无数据");
          }
          else
          {
            https = new flurl();
            List<pay> payList = new List<pay>();
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
                  payList.Add(list.First<pay>());
                }
              }
            }
            if (payList == null || payList.Count == 0)
            {
              log("请勾选需要核销的数据");
            }
            else
            {
              flurl = new flurl();
              if (payList.Count < 10)
              {
                new Thread(new ParameterizedThreadStart(hexiao)).Start(payList);
              }
              else
              {
                int num = payList.Count / 3;
                foreach (KeyValuePair<string, List<pay>> split in COMM.SplitList(payList, num))
                  new Thread(new ParameterizedThreadStart(hexiao)).Start(split.Value);
              }
            }
          }
        }
      }
    }

    private async void hexiao(object obj)
    {
      List<pay> list = (List<pay>) obj;
      try
      {
        foreach (pay item in list)
        {
          try
          {
            DataRow rows = COMM.getauth(item.cookie);
            switch (type)
            {
              case "快递":
                kuaidi kuaidi = kdlist[new Random().Next(kdlist.Count)];
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("param", "{\"from\":\"pc\",\"orderId\":\"" + item.orderid + "\",\"expressNo\":\"" + kuaidi.orderid + "\",\"expressType\":" + kuaidi.id.ToString() + ",\"expressCustom\":\"" + kuaidi.name + "\",\"fullDeliver\":true}");
                dic.Add("wdtoken", COMM.cookies(COMM.cookie));
                HttpRequestEntity res = await MYHTTP.Result("POST", "https://thor.weidian.com/tradeview/seller.deliverOrder/1.0", 5, dic, COMM.cookies(COMM.cookie));
                if (res != null && !string.IsNullOrWhiteSpace(res.ResponseContent) && res.ResponseContent.Contains("OK") && res.ResponseContent.Contains("true"))
                {
                  log("账号" + item.phone + "，订单号" + item.orderid + "发货成功，快递" + kuaidi.name + "快递单号" + kuaidi.orderid);
                  if (!shouhuo)
                  {
                    dic = new Dictionary<string, string>();
                    dic.Add("param", "{\"order_id\":\"" + item.orderid + "\",\"order_source\":0,\"from\":\"h5\"}");
                    dic.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"wx4b74228baa15489a\",\"token\":\"" + item.cookie.Split('，')[2] + "\",\"duid\":\"" + item.cookie.Split('，')[5] + "\",\"uss\":\"" + item.cookie.Split('，')[2] + "\",\"anonymousId\":\"7316e333e3febf1c7d957a0dd00698ed46430f4a\",\"userID\":\"" + item.cookie.Split('，')[5] + "\",\"wduserID\":\"" + item.cookie.Split('，')[6] + "\",\"version\":\"1.0.1\",\"miniProgramScene\":1089,\"payEnv\":{\"environment\":\"WXAPP\",\"platform\":\"wx4b74228baa15489a\",\"from\":\"WXAPP\"},\"subChannel\":\"wdPlus\",\"wxEnvVersion\":\"release\",\"wxappid\":\"wx4b74228baa15489a\"}");
                    string resu = await flurl.POST("https://thor.weidian.com/tradeview/buyer.confirm.order/1.0", dic);
                    if (resu != null && !string.IsNullOrWhiteSpace(resu) && resu.Contains("OK"))
                    {
                      item.status = "发货成功";
                      UPDATE(item);
                      MYSQL.exp("insert into orders (phone,cookie,orderid,itemid,status,remark,token,times) values ('" + item.phone + "','" + item.cookie + "','" + item.orderid + "','" + item.itemid + "','Y','" + remark + "','" + COMM.TOKENS + "','" + DateTime.Now.ToString() + "')");
                      MYSQL.exp("delete from wd where id='" + item.id + "'");
                      log("账号" + item.phone + "，订单号" + item.orderid + "收货成功");
                    }
                    else
                      log("账号" + item.phone + "，订单号" + item.orderid + "收货失败" + res.ResponseContent);
                    resu = null;
                  }
                }
                kuaidi = null;
                dic = null;
                res = null;
                break;
              case "商城版自提":
                string rs1 = await https.GET999("https://thor.weidian.com/tradeview/verifySelfDeliveryOrder/1.0?param=%7B%22orderIds%22%3A%5B" + item.orderid + "%5D%2C%22from%22%3A%22pc%22%7D&wdtoken=" + COMM.tokens(COMM.cookie) + "&_=" + COMM.ConvertDateTimeToString(), COMM.cookies(COMM.cookie));
                if (!string.IsNullOrWhiteSpace(rs1) && rs1.Contains("orderIds") && rs1.Contains("OK"))
                {
                  item.status = "核销成功";
                  UPDATE(item);
                  MYSQL.exp("insert into orders (phone,cookie,orderid,itemid,status,remark,token,times) values ('" + item.phone + "','" + item.cookie + "','" + item.orderid + "','" + item.itemid + "','Y','" + remark + "','" + COMM.TOKENS + "','" + DateTime.Now.ToString() + "')");
                  MYSQL.exp("delete from wd where id='" + item.id + "'");
                  log("账号" + item.phone + "，订单号" + item.orderid + "核销成功");
                }
                rs1 = null;
                break;
              default:
                string cookie = "__spider__sessionid=" + COMM.r(16) + "; __spider__visitorid=" + COMM.r(16) + "; duid=" + rows["duid"].ToString() + "; is_login=true; login_source=LOGIN_USER_SOURCE_MASTER; login_token=" + rows["loginToken"].ToString() + "; login_type=LOGIN_USER_TYPE_MASTER; sid=" + rows["sid"].ToString() + "; uid=" + rows["uid"].ToString() + "; v-components/tencent-live-plugin@wfr=c_wxh5; wdtoken=8f2d1224";
                HttpRequestEntity rs2 = await MYHTTP.Result("GET", "https://thor.weidian.com/tradeview/buyer.query.selfdelivery.code/1.0?appKey=84092576&param=%7B%22from%22%3A%22h5%22%2C%22order_id%22%3A%22" + item.orderid + "%22%7D&_=" + COMM.ConvertDateTimeToString() + "&callback=jsonp1", 5, null, cookie);
                if (rs2 != null && !string.IsNullOrWhiteSpace(rs2.ResponseContent) && rs2.ResponseContent.Contains("status_desc") && rs2.ResponseContent.Contains("OK"))
                {
                  JObject JSON = COMM.GetToJsonList(rs2.ResponseContent.Replace("jsonp1(", "").Replace(")", ""));
                  string CODE = JSON["result"]["code"].ToString();
                  rs2 = await MYHTTP.Result("GET", "https://thor.weidian.com/tradeview/verifySelfDelivery/1.0?param=%7B%22pickingCode%22%3A%22" + CODE + "%22%7D&wdtoken=ecdadb3b&_=" + COMM.ConvertDateTimeToString(), 5, null, COMM.cookies(COMM.cookie));
                  if (rs2 != null && !string.IsNullOrWhiteSpace(rs2.ResponseContent) && rs2.ResponseContent.Contains("OK"))
                  {
                    item.status = "核销成功";
                    UPDATE(item);
                    MYSQL.exp("insert into orders (phone,cookie,orderid,itemid,status,remark,token,times) values ('" + item.phone + "','" + item.cookie + "','" + item.orderid + "','" + item.itemid + "','Y','" + remark + "','" + COMM.TOKENS + "','" + DateTime.Now.ToString() + "')");
                    MYSQL.exp("delete from wd where id='" + item.id + "'");
                    log("账号" + item.phone + "，订单号" + item.orderid + "核销成功");
                  }
                  JSON = null;
                  CODE = null;
                }
                cookie = null;
                rs2 = null;
                break;
            }
            rows = null;
          }
          catch (Exception ex1)
          {
            Exception ex = ex1;
            log(item.orderid + "核销出现异常");
          }
        }
        list = null;
      }
      catch (Exception ex2)
      {
        Exception ex = ex2;
        log("核销出现异常");
        list = null;
      }
    }

    private void button8_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "文本文档|*.txt";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      string path = openFileDialog.FileName.ToString();
            kdlist = new List<kuaidi>();
      foreach (string readAllLine in File.ReadAllLines(path, Encoding.UTF8))
      {
        if (!string.IsNullOrEmpty(readAllLine))
        {
          string[] strArray = readAllLine.Split('-');
          if (strArray != null && strArray.Length == 3)
                        kdlist.Add(new kuaidi()
            {
              orderid = strArray[0].Trim(),
              name = strArray[1].Trim(),
              id = Convert.ToInt32(strArray[2].Trim())
            });
        }
      }
      log("导入快递" + kdlist.Count.ToString() + "条");
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

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
    }

    private async void test(object obj)
    {
      List<string> list = (List<string>) obj;
      foreach (string item in list)
      {
        try
        {
          Dictionary<string, string> dic = new Dictionary<string, string>();
          dic.Add("param", "{\"listType\":0,\"pageNum\":" + item + ",\"pageSize\":50,\"statusList\":[\"paid\"],\"refundStatusList\":[],\"channel\":\"pc\",\"topOrderType\":0,\"shipRole\":0,\"orderIdList\":\"\",\"itemTitle\":\"\",\"buyerName\":\"\",\"timeSearch\":{},\"orderBizType\":\"\",\"promotionType\":\"\",\"shipType\":\"1\",\"newGhSearchSellerRole\":\"7\",\"memberLevel\":\"\",\"repayStatus\":\"2\",\"bSellerId\":\"\",\"itemSource\":\"\",\"shipper\":\"\",\"nSellerName\":\"\",\"partnerName\":\"\",\"noteSearchCondition\":{\"buyerNote\":\"\"},\"specialOrderSearchCondition\":{\"notShowGroupUnsuccess\":0,\"notShowFxOrder\":0,\"notShowUnRepayOrder\":0,\"notShowBuyerRepayOrder\":0,\"showAllPeriodOrder\":1,\"notShowTencentShopOrder\":0,\"notShowWithoutTimelinessOrder\":0},\"orderType\":4}");
          dic.Add("wdtoken", "87355c41");
          HttpRequestEntity res = await MYHTTP.Result("POST", "https://thor.weidian.com/tradeview/seller.getOrderListForPC/1.0", 5, dic, COMM.cookies(COMM.cookie));
          Model.hexiaoroot model = JsonConvert.DeserializeObject<Model.hexiaoroot>(res.ResponseContent);
          if (model != null && model.result != null && model.result.orderList != null && model.result.orderList.Count > 0)
          {
            List<Model.hexiaorootOrderList> mylist = model.result.orderList;
            foreach (Model.hexiaorootOrderList items in mylist)
            {
              try
              {
                if (items.totalPrice == "0.01")
                {
                  string phone = items.receiver.buyerTelephone;
                  string orderid = items.orderId;
                  DataRow[] arrrows = dt.Select("phone='" + phone + "'");
                  if (arrrows != null && arrrows.Length != 0)
                  {
                    DataRow rows = arrrows[0];
                    string cookie = "__spider__sessionid=" + COMM.r(16) + "; __spider__visitorid=" + COMM.r(16) + "; duid=" + rows["duid"].ToString() + "; is_login=true; login_source=LOGIN_USER_SOURCE_MASTER; login_token=" + rows["loginToken"].ToString() + "; login_type=LOGIN_USER_TYPE_MASTER; sid=" + rows["sid"].ToString() + "; uid=" + rows["uid"].ToString() + "; v-components/tencent-live-plugin@wfr=c_wxh5; wdtoken=8f2d1224";
                    HttpRequestEntity rs = await MYHTTP.Result("GET", "https://thor.weidian.com/tradeview/buyer.query.selfdelivery.code/1.0?appKey=84092576&param=%7B%22from%22%3A%22h5%22%2C%22order_id%22%3A%22" + orderid + "%22%7D&_=" + COMM.ConvertDateTimeToString() + "&callback=jsonp1", 5, null, cookie);
                    if (rs != null && !string.IsNullOrWhiteSpace(rs.ResponseContent) && rs.ResponseContent.Contains("status_desc") && rs.ResponseContent.Contains("OK"))
                    {
                      JObject JSON = COMM.GetToJsonList(rs.ResponseContent.Replace("jsonp1(", "").Replace(")", ""));
                      string CODE = JSON["result"]["code"].ToString();
                      rs = await MYHTTP.Result("GET", "https://thor.weidian.com/tradeview/verifySelfDelivery/1.0?param=%7B%22pickingCode%22%3A%22" + CODE + "%22%7D&wdtoken=ecdadb3b&_=" + COMM.ConvertDateTimeToString(), 5, null, COMM.cookies(COMM.cookie));
                      if (rs != null && !string.IsNullOrWhiteSpace(rs.ResponseContent) && rs.ResponseContent.Contains("OK"))
                        log("账号" + phone + "，订单号" + orderid + "核销成功");
                      JSON = null;
                      CODE = null;
                    }
                    else if (rs.ResponseContent.Contains("LOGIN"))
                    {
                      log(phone + "失效");
                      continue;
                    }
                    rows = null;
                    cookie = null;
                    rs = null;
                  }
                  phone = null;
                  orderid = null;
                  arrrows = null;
                }
              }
              catch (Exception ex)
              {
              }
            }
            mylist = null;
          }
          dic = null;
          res = null;
          model = null;
        }
        catch (Exception ex)
        {
        }
      }
      list = null;
    }

    private void button1_Click(object sender, EventArgs e)
    {
            dt = MYSQL.Query("select * from wd_user1 where remakr='" + COMM.TOKENS + "' and status='Y'");
      List<string> list = new List<string>();
      for (int index = 0; index < 60; ++index)
        list.Add(index.ToString());
      int num = list.Count / 60;
      foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(list, num))
        new Thread(new ParameterizedThreadStart(test)).Start(split.Value);
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
      button1 = new Button();
      linkLabel2 = new LinkLabel();
      checkBox1 = new CheckBox();
      comboBox2 = new ComboBox();
      linkLabel1 = new LinkLabel();
      comboBox1 = new ComboBox();
      button4 = new Button();
      button2 = new Button();
      button8 = new Button();
      panel2 = new Panel();
      textBox7 = new TextBox();
      dataGridView1 = new DataGridView();
      账号 = new DataGridViewTextBoxColumn();
      订单号 = new DataGridViewTextBoxColumn();
      核销状态 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      panel1.SuspendLayout();
      panel2.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      SuspendLayout();
      panel1.Controls.Add(button1);
      panel1.Controls.Add(linkLabel2);
      panel1.Controls.Add(checkBox1);
      panel1.Controls.Add(comboBox2);
      panel1.Controls.Add(linkLabel1);
      panel1.Controls.Add(comboBox1);
      panel1.Controls.Add(button4);
      panel1.Controls.Add(button2);
      panel1.Controls.Add(button8);
      panel1.Dock = DockStyle.Left;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(200, 538);
      panel1.TabIndex = 0;
      panel1.Paint += new PaintEventHandler(panel1_Paint);
      button1.Location = new Point(23, 362);
      button1.Name = "button1";
      button1.Size = new Size(135, 23);
      button1.TabIndex = 55;
      button1.Text = "无核销码核销";
      button1.UseVisualStyleBackColor = true;
      button1.Visible = false;
      button1.Click += new EventHandler(button1_Click);
      linkLabel2.AutoSize = true;
      linkLabel2.Location = new Point(105, 65);
      linkLabel2.Name = "linkLabel2";
      linkLabel2.Size = new Size(53, 12);
      linkLabel2.TabIndex = 54;
      linkLabel2.TabStop = true;
      linkLabel2.Text = "删除备注";
      linkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
      checkBox1.AutoSize = true;
      checkBox1.Location = new Point(23, 229);
      checkBox1.Name = "checkBox1";
      checkBox1.Size = new Size(84, 16);
      checkBox1.TabIndex = 51;
      checkBox1.Text = "不自动收货";
      checkBox1.UseVisualStyleBackColor = true;
      comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox2.FormattingEnabled = true;
      comboBox2.Items.AddRange(new object[3]
      {
         "快递",
         "商城版自提",
         "非商城版自提"
      });
      comboBox2.Location = new Point(23, 143);
      comboBox2.Name = "comboBox2";
      comboBox2.Size = new Size(135, 20);
      comboBox2.TabIndex = 50;
      comboBox2.SelectedIndexChanged += new EventHandler(comboBox2_SelectedIndexChanged);
      linkLabel1.AutoSize = true;
      linkLabel1.Location = new Point(23, 65);
      linkLabel1.Name = "linkLabel1";
      linkLabel1.Size = new Size(53, 12);
      linkLabel1.TabIndex = 49;
      linkLabel1.TabStop = true;
      linkLabel1.Text = "刷新备注";
      linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
      comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox1.FormattingEnabled = true;
      comboBox1.Location = new Point(23, 86);
      comboBox1.Name = "comboBox1";
      comboBox1.Size = new Size(135, 20);
      comboBox1.TabIndex = 48;
      button4.Location = new Point(23, 113);
      button4.Name = "button4";
      button4.Size = new Size(135, 23);
      button4.TabIndex = 47;
      button4.Text = "查询数据";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      button2.Location = new Point(23, 200);
      button2.Name = "button2";
      button2.Size = new Size(135, 23);
      button2.TabIndex = 45;
      button2.Text = "开始核销";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      button8.Location = new Point(23, 170);
      button8.Name = "button8";
      button8.Size = new Size(135, 23);
      button8.TabIndex = 44;
      button8.Text = "导入快递单号";
      button8.UseVisualStyleBackColor = true;
      button8.Click += new EventHandler(button8_Click);
      panel2.Controls.Add(textBox7);
      panel2.Dock = DockStyle.Top;
      panel2.Location = new Point(200, 0);
      panel2.Name = "panel2";
      panel2.Size = new Size(714, 138);
      panel2.TabIndex = 1;
      textBox7.BackColor = SystemColors.ActiveCaptionText;
      textBox7.Dock = DockStyle.Fill;
      textBox7.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
      textBox7.ForeColor = Color.DarkGray;
      textBox7.Location = new Point(0, 0);
      textBox7.Multiline = true;
      textBox7.Name = "textBox7";
      textBox7.ReadOnly = true;
      textBox7.ScrollBars = ScrollBars.Vertical;
      textBox7.Size = new Size(714, 138);
      textBox7.TabIndex = 9;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(账号, 订单号, 核销状态);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(200, 138);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(714, 400);
      dataGridView1.TabIndex = 8;
      账号.DataPropertyName = "账号";
      账号.HeaderText = "账号";
      账号.Name = "账号";
      账号.Width = 150;
      订单号.DataPropertyName = "订单号";
      订单号.HeaderText = "订单号";
      订单号.Name = "订单号";
      订单号.Width = 400;
      核销状态.DataPropertyName = "核销状态";
      核销状态.HeaderText = "核销状态";
      核销状态.Name = "核销状态";
      核销状态.Width = 200;
      dataGridViewTextBoxColumn1.DataPropertyName = "账号";
      dataGridViewTextBoxColumn1.HeaderText = "账号";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn1.Width = 150;
      dataGridViewTextBoxColumn2.DataPropertyName = "订单号";
      dataGridViewTextBoxColumn2.HeaderText = "订单号";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn2.Width = 400;
      dataGridViewTextBoxColumn3.DataPropertyName = "核销状态";
      dataGridViewTextBoxColumn3.HeaderText = "核销状态";
      dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      dataGridViewTextBoxColumn3.Width = 200;
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(dataGridView1);
      Controls.Add(panel2);
      Controls.Add(panel1);
      Name = "核销";
      Size = new Size(914, 538);
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      panel2.ResumeLayout(false);
      panel2.PerformLayout();
      ((ISupportInitialize) dataGridView1).EndInit();
      ResumeLayout(false);
    }

    private delegate void UpdateDataGridView(DataTable dt);

    public delegate void SetTextHandler(string text);
  }
}
