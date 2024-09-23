// Decompiled with JetBrains decompiler
// Type: 微店新版.Tool.一键复制上架
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版.Tool
{
  public class 一键复制上架 : UserControl
  {
    private bool ischeckbox = false;
    private flurl flurl;
    public static ConcurrentDictionary<string, ItemCopy> dic = new ConcurrentDictionary<string, ItemCopy>();
    private IContainer components = null;
    private Panel panel1;
    private Button button2;
    private Button button1;
    private TextBox textBox12;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private Button button3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    private DataGridViewTextBoxColumn SKU;
    private DataGridViewTextBoxColumn 标题;
    private DataGridViewTextBoxColumn 描述;
    private DataGridViewTextBoxColumn 主页数据;
    private DataGridViewTextBoxColumn 详情数据;
    private DataGridViewTextBoxColumn 状态;
    private Button button4;
    private Button button5;

    public 一键复制上架() => InitializeComponent();

    private void log(string text)
    {
      if (textBox12.InvokeRequired)
        textBox12.Invoke(new 一键复制上架.SetTextHandler(log), text);
      else
        textBox12.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
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
        BeginInvoke(new 一键复制上架.UpdateDataGridView(UpdateGV), dt);
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
            viewCheckBoxColumn.Width = 50;
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
            viewCheckBoxColumn.Width = 50;
            checkBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            dataGridView1.Columns.Insert(0, viewCheckBoxColumn);
          }
          dataGridView1.Refresh();
          ischeckbox = true;
        }
      }
    }

    public void UPDATE(ItemCopy users)
    {
      try
      {
        BeginInvoke(new EventHandler(delegate (object  p0 , EventArgs p1)
        {
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells["SKU"].Value.ToString() == users.sku)
            {
              row.Cells["标题"].Value = users.title;
              row.Cells["描述"].Value = users.desc;
              row.Cells["主页数据"].Value = users.zhuyelist.Count + users.zhuyelist1.Count;
              row.Cells["详情数据"].Value = users.desclist.Count;
              row.Cells["状态"].Value = users.status;
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
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "文本文档|*.txt";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
                dic = new ConcurrentDictionary<string, ItemCopy>();
        foreach (string readAllLine in File.ReadAllLines(openFileDialog.FileName.ToString(), Encoding.UTF8))
        {
          if (!string.IsNullOrEmpty(readAllLine))
          {
            string empty = string.Empty;
            param(readAllLine, ref empty);
            if (string.IsNullOrWhiteSpace(empty))
              log(readAllLine + "解析失败");
            else
                            dic.TryAdd(empty, new ItemCopy()
              {
                sku = empty,
                desc = "",
                zhuyelist1 = new List<string>(),
                desclist = new List<string>(),
                status = "",
                title = "",
                zhuyelist = new List<string>()
              });
          }
        }
        log("导入链接" + dic.Count.ToString());
      }
      if (dic == null || dic.Count <= 0)
        return;
      DataTable dt = new DataTable();
      dt.Columns.Add(new DataColumn("SKU", typeof (string)));
      dt.Columns.Add(new DataColumn("标题", typeof (string)));
      dt.Columns.Add(new DataColumn("描述", typeof (string)));
      dt.Columns.Add(new DataColumn("主页数据", typeof (string)));
      dt.Columns.Add(new DataColumn("详情数据", typeof (string)));
      dt.Columns.Add(new DataColumn("状态", typeof (string)));
      foreach (KeyValuePair<string, ItemCopy> keyValuePair in dic)
      {
        DataRow row = dt.NewRow();
        row["SKU"] = keyValuePair.Key;
        dt.Rows.Add(row);
      }
      if (dt != null && dt.Rows.Count > 0)
        UpdateGV(dt);
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

    private void button1_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(COMM.cookie))
      {
        log("请登录主店铺");
      }
      else
      {
        DataTable dataSource = (DataTable) dataGridView1.DataSource;
        if (dataSource == null || dataSource.Rows.Count == 0)
        {
          log("无SKU数据");
        }
        else
        {
          List<string> stringList = new List<string>();
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells[0].Value != null && (bool) row.Cells[0].Value)
              stringList.Add(row.Cells["SKU"].Value.ToString());
          }
          if (stringList == null || stringList.Count == 0)
            log("请勾选需要获取详情的SKU");
          else if (stringList.Count < 10)
          {
            new Thread(new ParameterizedThreadStart(collection)).Start(stringList);
          }
          else
          {
            int num = stringList.Count / 5;
            foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(stringList, num))
              new Thread(new ParameterizedThreadStart(collection)).Start(split.Value);
          }
        }
      }
    }

    public static string StrToEscape(string str)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < str.Length; ++index)
      {
        char ch = str[index];
        switch (ch)
        {
          case '\a':
            stringBuilder.Append("\\a");
            break;
          case '\b':
            stringBuilder.Append("\\b");
            break;
          case '\t':
            stringBuilder.Append("\\t");
            break;
          case '\n':
            stringBuilder.Append("\\n");
            break;
          case '\r':
            stringBuilder.Append("\\r");
            break;
          case '"':
            stringBuilder.Append("\\\"");
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
      }
      return stringBuilder.ToString();
    }

    private async void collection(object obj)
    {
      List<string> skuls = (List<string>) obj;
      foreach (string item in skuls)
      {
        try
        {
          ItemCopy SKU = dic[item];
          string res = await flurl.GET999("https://thor.weidian.com/detail/getDetailDesc/1.0?param=%7B%22vItemId%22%3A%22" + item + "%22%7D&wdtoken=9f16d10a&_=" + COMM.ConvertDateTimeToString(), COMM.cookie);
          if (!string.IsNullOrWhiteSpace(res))
          {
            JObject models = COMM.GetToJsonList(res);
            if (models != null && models["result"] != null && models["result"]["item_detail"] != null && models["result"]["item_detail"]["desc_content"] != null && models["result"]["item_detail"]["desc_content"].Count<JToken>() > 0)
            {
              foreach (JToken s in models["result"]["item_detail"]["desc_content"])
              {
                if (s["type"].ToString() == "1")
                  SKU.desclist.Add(JsonConvert.SerializeObject(new
                  {
                      type = 1,
                      text = s[(object)"text"].ToString()
                  }));
                if (s["type"].ToString() == "2")
                  SKU.desclist.Add(JsonConvert.SerializeObject(new
                  {
                      type = 2,
                      url = s[(object)"url"].ToString()
                  }));
                if (s["type"].ToString() == "11")
                  SKU.desclist.Add(JsonConvert.SerializeObject(new
                  {
                      type = 11,
                      faceurl = s[(object)"face_url"].ToString(),
                      videoId = s[(object)"video_id"].ToString(),
                      auditStatus = 1
                  }));
              }
            }
            else
              log("详情图片获取失败" + item);
            models = null;
          }
          else
            log("详情图片获取失败" + item);
          Dictionary<string, string> dics = new Dictionary<string, string>();
          dics.Add("param", "{\"vitemId\":\"" + item + "\",\"discountId\":\"\",\"from\":\"weiDianPlus\",\"thirdAppVersion\":null,\"wxAppVersionType\":\"release\",\"source\":null,\"scene\":1053,\"isWxVideoScene\":false}");
          dics.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"windows\",\"anonymousId\":\"2e5897f2472f2156e39ea46ed5af686393d2c365\",\"visitor_id\":\"2e5897f2472f2156e39ea46ed5af686393d2c365\",\"wxappid\":\"wx4b74228baa15489a\"}");
          string result = await flurl.POST("https://thor.weidian.com/detail/getAppletItemInfo/1.0", dics);
          if (!string.IsNullOrWhiteSpace(result))
          {
            JObject models = COMM.GetToJsonList(result);
            if (models != null && models["result"] != null && models["result"]["default_model"] != null)
            {
              JToken info = models["result"]["default_model"]["item_info"];
              if (info["imgs"] != null && info["imgs"].Count<JToken>() > 0)
              {
                foreach (JToken im in info["imgs"])
                  SKU.zhuyelist.Add(JsonConvert.SerializeObject(new
                  {
                      url = im.ToString(),
                      title = ""
                  }));
              }
              if (info["head_video_info"] != null && info["head_video_info"].Count<JToken>() > 0)
              {
                foreach (JToken im in info["head_video_info"])
                  SKU.zhuyelist1.Add(JsonConvert.SerializeObject(new
                  {
                      auditStatus = 1,
                      faceUrl = im[(object)"cover_url"].ToString(),
                      type = 1,
                      videoId = im[(object)"video_id"].ToString()
                  }));
              }
              SKU.title = info["item_name"].ToString();
              SKU.desc = info["itemShareDesc"].ToString();
              JToken info1 = models["result"]["default_model"]["sku_properties"];
              if (info1["attr_list"] != null && info1["attr_list"].Count<JToken>() > 0)
              {
                SKU.keyValuePairs = new Dictionary<string, List<Item>>();
                foreach (JToken ins in info1["attr_list"])
                {
                  string title = ins["attr_title"].ToString();
                  List<Item> lists = new List<Item>();
                  foreach (JToken ins_1 in ins["attr_values"])
                  {
                    string img = string.Empty;
                    if (ins_1["img"] != null && !string.IsNullOrWhiteSpace(ins_1["img"].ToString()))
                      img = ins_1["img"].ToString();
                    lists.Add(new Item()
                    {
                      attr_id = ins_1["attr_id"].ToString(),
                      attr_value = ins_1["attr_value"].ToString(),
                      img = img
                    });
                    img = null;
                  }
                  SKU.keyValuePairs.Add(title, lists);
                  title = null;
                  lists = null;
                }
              }
              if (info1["sku"] != null && info1["sku"].Count<JToken>() > 0)
              {
                SKU.SKUs = new Dictionary<string, Root>();
                foreach (JToken ins2 in info1["sku"])
                {
                  JToken ins = ins2.First<JToken>();
                  SKU.SKUs.Add(ins["id"].ToString(), new Root()
                  {
                    attr_ids = ins["attr_ids"].ToString(),
                    id = ins["id"].ToString(),
                    img = ins["img"].ToString(),
                    origin_price = ins["origin_price"].ToString(),
                    price = ins["origin_price"].ToString(),
                    title = ins["title"].ToString()
                  });
                  ins = null;
                }
              }
              info = null;
              info1 = null;
            }
            models = null;
          }
          UPDATE(SKU);
          SKU = null;
          res = null;
          dics = null;
          result = null;
        }
        catch (Exception ex)
        {
          log("详情图片获取异常" + item);
        }
      }
      skuls = null;
    }

    private void 一键复制上架_Load(object sender, EventArgs e)
    {
      flurl = new flurl();
      string path = Environment.CurrentDirectory + "\\视频";
      if (Directory.Exists(path))
        return;
      Directory.CreateDirectory(path);
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(COMM.cookie))
        log("请登录主店铺");
      else if (dic == null || dic.Count == 0)
        log("请采集数据");
      else
        new Thread(new ParameterizedThreadStart(init)).Start("");
    }

    private async void init(object obj)
    {
      foreach (KeyValuePair<string, ItemCopy> keyValuePair1 in dic)
      {
        KeyValuePair<string, ItemCopy> item = keyValuePair1;
        string itemid = item.Key;
        ItemCopy value = item.Value;
        try
        {
          Dictionary<string, List<Item>> itemdic = new Dictionary<string, List<Item>>();
          string res = await flurl.GET999("https://thor.weidian.com/wditem/item.getAllAttrList/1.0?wdtoken=5a286d26&_=1716369897814", COMM.cookie);
          if (res != null && !string.IsNullOrWhiteSpace(res))
          {
            JObject model = COMM.GetToJsonList(res);
            if (model != null && model["result"] != null && model["result"]["attr_list"] != null && model["result"]["attr_list"].Count<JToken>() > 0)
            {
              foreach (JToken ims in model["result"]["attr_list"])
              {
                string title = ims["attr_title"].ToString();
                List<Item> ls = new List<Item>();
                foreach (JToken ins in ims["attr_values"])
                  ls.Add(new Item()
                  {
                    attr_id = ins["attr_id"].ToString(),
                    attr_value = ins["attr_value"].ToString()
                  });
                itemdic.Add(title, ls);
                title = null;
                ls = null;
              }
            }
            model = null;
          }
          if (itemdic != null && itemdic.Count > 0 && value.keyValuePairs != null && value.keyValuePairs.Count > 0)
          {
            List<string> mylist = GetItemValue(itemdic, value.keyValuePairs);
            if (mylist != null && mylist.Count > 0)
            {
              string json = "{\"attr_list\":[" + string.Join(",", mylist) + "]}";
              Dictionary<string, string> ds = new Dictionary<string, string>();
              ds.Add("param", json);
              ds.Add("wdtoken", COMM.tokens(COMM.cookie));
              string d = await flurl.POST999("https://thor.weidian.com/wditem/itemWrite.updateAttrList/1.0", COMM.cookie, ds);
              if (!string.IsNullOrWhiteSpace(d))
              {
                JObject md = COMM.GetToJsonList(d);
                if (md != null && md["result"] != null && md["result"]["data"] != null && md["result"]["data"].Count<JToken>() > 0)
                {
                  StringBuilder json1 = new StringBuilder();
                  if (value.desclist != null && value.desclist.Count > 0)
                    json1.Append(string.Join(",", value.desclist) + ",");
                  string sojson = json1.ToString().Substring(0, json1.ToString().Length - 1);
                  value.json1 = "{\"item_detail\":[" + sojson + "],\"from\":\"weidian_pc\",\"submitWithLimitWords\":false,\"submitWithSensitiveWord\":false,\"sdkVideoSupported\":true}";
                  Dictionary<string, int> dc = new Dictionary<string, int>();
                  int index1 = 1;
                  List<string> arrlistjson = new List<string>();
                  foreach (KeyValuePair<string, List<Item>> keyValuePair2 in value.keyValuePairs)
                  {
                    KeyValuePair<string, List<Item>> jk = keyValuePair2;
                    string k = jk.Key;
                    List<Item> v = jk.Value;
                    StringBuilder asb = new StringBuilder();
                    foreach (Item jk_v in v)
                    {
                      dc.Add(jk_v.attr_value, index1);
                      asb.Append("{\"attrValue\":\"" + jk_v.attr_value + "\",\"attrId\":" + index1.ToString() + ",\"img\":\"" + jk_v.img + "\",\"selected\":true,\"renamePopoverVisible\":false},");
                      ++index1;
                    }
                    string s1 = asb.ToString().Substring(0, asb.ToString().Length - 1);
                    arrlistjson.Add("{\"attrTitle\":\"" + k + "\",\"attrValues\":[" + s1 + "],\"showFilter\":false,\"addSkuPopoverVisible\":false,\"isSystemClassify\":false,\"fillLocal\":false,\"fillLocalPopover\":false,\"canRemove\":true,\"canSaveCommon\":true,\"canSelectFromCommon\":true,\"type\":\"COMMON\",\"skuHeaderExtendName\":\"\",\"skuAttrNameExtendNameTemplate\":\"\",\"commonSkuPopoverVisible\":false,\"showPriceLimitFilter\":false,\"showSaleFloorFilter\":false}");
                    k = null;
                    v = null;
                    asb = null;
                    s1 = null;
                    jk = new KeyValuePair<string, List<Item>>();
                  }
                  value.attrListJson = "[" + string.Join(",", arrlistjson) + "]";
                  if (value.zhuyelist1 != null && value.zhuyelist1.Count > 0)
                  {
                    StringBuilder sp = new StringBuilder();
                    foreach (string v in value.zhuyelist1)
                      sp.Append(v + ",");
                    string s2 = sp.ToString().Substring(0, sp.ToString().Length - 1);
                    value.videosJson = "[" + s2 + "]";
                    sp = null;
                    s2 = null;
                  }
                  else
                    value.videosJson = "[]";
                  if (value.zhuyelist != null && value.zhuyelist.Count > 0)
                  {
                    StringBuilder sp = new StringBuilder();
                    foreach (string v in value.zhuyelist)
                      sp.Append(v + ",");
                    string s2 = sp.ToString().Substring(0, sp.ToString().Length - 1);
                    value.ImageJson = "[" + s2 + "]";
                    sp = null;
                    s2 = null;
                  }
                  List<string> kvlist = new List<string>();
                  foreach (KeyValuePair<string, Root> skU in value.SKUs)
                  {
                    KeyValuePair<string, Root> kv = skU;
                    StringBuilder ids = new StringBuilder();
                    string[] sp = kv.Value.title.Split(';');
                    if (sp.Length == 1)
                    {
                      ids.Append(dc[sp[0]].ToString() + ",");
                    }
                    else
                    {
                      string[] strArray = sp;
                      for (int index2 = 0; index2 < strArray.Length; ++index2)
                      {
                        string s = strArray[index2];
                        ids.Append(dc[s].ToString() + ",");
                        s = null;
                      }
                      strArray = null;
                    }
                    if (!string.IsNullOrWhiteSpace(ids.ToString()))
                    {
                      string newids = ids.ToString().Substring(0, ids.ToString().Length - 1);
                      string js = "{\"identifier\":\"" + COMM.rS(8) + "\",\"price\":\"" + kv.Value.price + "\",\"stock\":\"99999\",\"merchantCode\":\"\",\"attrIds\":[" + string.Join(",", newids.ToString().Split(',').ToList<string>()) + "],\"costPrice\":\"\",\"minPrice\":\"\",\"maxPrice\":\"\",\"stop_sale\":false,\"quantifyItem\":false,\"img\":\"" + kv.Value.img + "\"}";
                      kvlist.Add(js);
                      ids = null;
                      sp = null;
                      newids = null;
                      js = null;
                      kv = new KeyValuePair<string, Root>();
                    }
                  }
                  value.SkuJson = "[" + string.Join(",", kvlist) + "]";
                  log(itemid + "初始化完成");
                  json1 = null;
                  sojson = null;
                  dc = null;
                  arrlistjson = null;
                  kvlist = null;
                }
                else
                  log(itemid + "初始化失败");
                md = null;
              }
              json = null;
              ds = null;
              d = null;
            }
            mylist = null;
          }
          itemdic = null;
          res = null;
        }
        catch (Exception ex)
        {
        }
        itemid = null;
        value = null;
        item = new KeyValuePair<string, ItemCopy>();
      }
      log("所有数据初始化完毕，可以执行一键上架操作");
    }

    public List<string> GetItemValue(
      Dictionary<string, List<Item>> dic1,
      Dictionary<string, List<Item>> dic2)
    {
      List<string> itemValue = new List<string>();
      foreach (KeyValuePair<string, List<Item>> keyValuePair in dic2)
      {
        try
        {
          string key = keyValuePair.Key;
          List<Item> objList = keyValuePair.Value;
          if (dic1.ContainsKey(key))
          {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Item obj in dic1[key])
              stringBuilder.Append("{\"attr_id\":" + obj.attr_id + ",\"attr_value\":\"" + obj.attr_value + "\"},");
            foreach (Item obj in dic2[key])
            {
              Item d2 = obj;
              List<Item> list = dic1[key].Where<Item>(p => p.attr_value == d2.attr_value).ToList<Item>();
              if (list == null || list.Count <= 0)
                stringBuilder.Append("{\"attr_value\":\"" + d2.attr_value + "\"},");
            }
            string str = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
            itemValue.Add("{\"attr_title\":\"" + key + "\",\"attr_values\":[" + str + "]}");
          }
          else
          {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Item obj in dic2[key])
              stringBuilder.Append("{\"attr_value\":\"" + obj.attr_value + "\"},");
            string str = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
            itemValue.Add("{\"attr_title\":\"" + key + "\",\"attr_values\":[" + str + "]}");
          }
        }
        catch (Exception ex)
        {
        }
      }
      return itemValue;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(COMM.cookie))
      {
        log("请登录主店铺");
      }
      else
      {
        DataTable dataSource = (DataTable) dataGridView1.DataSource;
        if (dataSource == null || dataSource.Rows.Count == 0)
        {
          log("无SKU数据");
        }
        else
        {
          List<string> stringList = new List<string>();
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells[0].Value != null && (bool) row.Cells[0].Value)
              stringList.Add(row.Cells["SKU"].Value.ToString());
          }
          if (stringList == null || stringList.Count == 0)
            log("请勾选需要获取上架的SKU");
          else if (stringList.Count < 10)
          {
            new Thread(new ParameterizedThreadStart(start)).Start(stringList);
          }
          else
          {
            int num = stringList.Count / 5;
            foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(stringList, num))
              new Thread(new ParameterizedThreadStart(start)).Start(split.Value);
          }
        }
      }
    }

    private async void start(object obj)
    {
      List<string> list = (List<string>) obj;
      try
      {
        foreach (string item in list)
        {
          try
          {
            ItemCopy SKU = dic[item];
            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("param", SKU.json1);
            dics.Add("wdtoken", COMM.r(8));
            string result = await flurl.POST999("https://thor.weidian.com/wditem/itemWrite.updateItemDetail/1.0", COMM.cookie, dics);
            if (!string.IsNullOrWhiteSpace(result) && result.Contains("OK"))
            {
              JObject od = COMM.GetToJsonList(result);
              string tmpDetailId = od["result"]["tmp_detail_id"].ToString();
              string json = "{\"supportExchange\":null,\"saleFloorLimit\":null,\"floorLimitType\":0,\"lowStockWarningValue\":null,\"itemComment\":\"\",\"videos\":" + SKU.videosJson + ",\"sdkVideoSupported\":true,\"isQuickOrder\":false,\"quickOrderSupportPromotion\":false,\"itemId\":\"\",\"officialLimit\":false,\"itemDeliveryList\":[4],\"itemBizType\":1,\"certificateOption\":1,\"imgs\":" + SKU.ImageJson + ",\"itemName\":\"" + SKU.title + "\",\"price\":\"\",\"vpoint_limit_count\":\"\",\"pointPrice\":{\"price\":\"\",\"point\":\"\"},\"point_price_range\":3,\"merchantCode\":\"\",\"barcode\":\"\",\"hideStock\":false,\"bgCateId\":50012045,\"catePropList\":[],\"tmpDetailId\":\"" + tmpDetailId + "\",\"cardType\":1,\"sellingMethodType\":0,\"cyclePurchaseInfo\":null,\"expressFeeTemplateId\":0,\"freeDelivery\":0,\"remoteFreeDelivery\":0,\"isWeightTemplate\":0,\"cateIds\":\"\",\"addItemToRecommendCate\":null,\"purchase_limit\":false,\"purchase_limit_num\":\"\",\"purchase_cycle\":1,\"purchase_type\":1,\"purchase_dimension_type\":1,\"isNeedIdNo\":\"\",\"isHiddenSetting\":false,\"itemStatus\":1,\"areaTemplateId\":0,\"areaTemplateFlag\":0,\"isFutureSold\":0,\"futureSoldTime\":\"\",\"offShelfTime\":\"\",\"approvedStatus\":\"\",\"submitWithLimitWords\":false,\"submitWithSensitiveWord\":false,\"submitWithSpecificationKeyWord\":false,\"isCheckQualification\":true,\"cardSystemPub\":1,\"validInfo\":{\"validType\":1},\"canNotRefund\":0,\"sellerTop\":null,\"chargeType\":1,\"phoneChargeInfo\":{\"operator\":1,\"faceValue\":10,\"areaCode\":1001},\"itemPresale\":{\"presale\":false,\"deposit\":null,\"depositRate\":null,\"voucher\":null,\"depositType\":1,\"depositStart\":null,\"depositEnd\":null,\"payStart\":null,\"payEnd\":null,\"deliveryType\":2,\"deliveryStart\":null,\"deliveryEnd\":null,\"deliveryOffset\":null},\"fullAmountPreSale\":{\"presale\":false,\"endTime\":null,\"deliveryType\":2,\"deliveryStart\":null,\"deliveryEnd\":null,\"deliveryOffset\":null},\"sellPoint\":null,\"quantifyItem\":false,\"soldCommentRule\":1,\"itemGroupId\":null,\"cardPriceContinueMode\":false,\"hideSold\":false,\"cardItemOrderSc\":1,\"saleChannels\":[],\"isBundleItem\":false,\"onShelfTime\":\"\",\"noStockOffShelf\":false,\"stockOnShelf\":false,\"shopMemberItemLimitModifyDo\":{\"officialLimit\":false,\"cardItemLimitModifyReqList\":[{\"cardType\":\"normal\",\"itemShopMemberLimitInfo\":[]},{\"cardType\":\"pay\",\"itemShopMemberLimitInfo\":[]}]},\"refundAddressId\":-1,\"timelinessId\":0,\"cardItemOverdue\":1,\"attrList\":" + SKU.attrListJson + ",\"sku\":" + SKU.SkuJson + "}";
              dics = new Dictionary<string, string>();
              dics.Add("param", json);
              dics.Add("wdtoken", COMM.r(8));
              result = await flurl.POST999("https://thor.weidian.com/itemsell/item.create.pc/1.0", COMM.cookie, dics);
              if (!string.IsNullOrWhiteSpace(result) && result.Contains("成功"))
              {
                od = COMM.GetToJsonList(result);
                if (od != null && od["result"] != null && od["result"]["skuInfo"] != null)
                {
                  log(item + "上架成功");
                  SKU.status = "上架成功";
                  UPDATE(SKU);
                }
                else
                {
                  log(item + "上架失败");
                  SKU.status = "上架失败";
                  UPDATE(SKU);
                }
              }
              else
              {
                log(item + "上架失败");
                SKU.status = "上架失败";
                UPDATE(SKU);
              }
              od = null;
              tmpDetailId = null;
              json = null;
            }
            else
            {
              log(item + "上架失败");
              SKU.status = "上架失败";
              UPDATE(SKU);
            }
            SKU = null;
            dics = null;
            result = null;
          }
          catch (Exception ex)
          {
          }
        }
        list = null;
      }
      catch (Exception ex)
      {
        list = null;
      }
    }

    private void button5_Click(object sender, EventArgs e)
    {
      DataTable dataSource = (DataTable) dataGridView1.DataSource;
      if (dataSource == null || dataSource.Rows.Count == 0)
      {
        log("无SKU数据");
      }
      else
      {
        List<string> stringList = new List<string>();
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
          if (row.Cells[0].Value != null && (bool) row.Cells[0].Value)
            stringList.Add(row.Cells["SKU"].Value.ToString());
        }
        if (stringList == null || stringList.Count == 0)
          log("请勾选需要获取详情的SKU");
        else if (stringList.Count < 10)
        {
          new Thread(new ParameterizedThreadStart(collection1)).Start(stringList);
        }
        else
        {
          int num = stringList.Count / 5;
          foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(stringList, num))
            new Thread(new ParameterizedThreadStart(collection1)).Start(split.Value);
        }
      }
    }

    private async void collection1(object obj)
    {
      string Path1 = Environment.CurrentDirectory + "\\视频";
      List<string> skuls = (List<string>) obj;
      foreach (string item in skuls)
      {
        try
        {
          ItemCopy SKU = dic[item];
          SKU.status = "准备下载";
          UPDATE(SKU);
          string res = await flurl.GET999("https://thor.weidian.com/detail/getDetailDesc/1.0?param=%7B%22vItemId%22%3A%22" + item + "%22%7D&wdtoken=9f16d10a&_=" + COMM.ConvertDateTimeToString(), COMM.cookie);
          if (!string.IsNullOrWhiteSpace(res))
          {
            JObject models = COMM.GetToJsonList(res);
            if (models != null && models["result"] != null && models["result"]["item_detail"] != null && models["result"]["item_detail"]["desc_content"] != null && models["result"]["item_detail"]["desc_content"].Count<JToken>() > 0)
            {
              foreach (JToken s in models["result"]["item_detail"]["desc_content"])
              {
                if (s["type"].ToString() == "11")
                {
                  string videoId = s["video_id"].ToString();
                  dowvideo(videoId, item);
                  videoId = null;
                }
              }
            }
            else
              log("详情视频获取失败" + item);
            models = null;
          }
          else
            log("详情视频获取失败" + item);
          Dictionary<string, string> dics = new Dictionary<string, string>();
          dics.Add("param", "{\"vitemId\":\"" + item + "\",\"discountId\":\"\",\"from\":\"weiDianPlus\",\"thirdAppVersion\":null,\"wxAppVersionType\":\"release\",\"source\":null,\"scene\":1053,\"isWxVideoScene\":false}");
          dics.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"windows\",\"anonymousId\":\"2e5897f2472f2156e39ea46ed5af686393d2c365\",\"visitor_id\":\"2e5897f2472f2156e39ea46ed5af686393d2c365\",\"wxappid\":\"wx4b74228baa15489a\"}");
          string result = await flurl.POST("https://thor.weidian.com/detail/getAppletItemInfo/1.0", dics);
          if (!string.IsNullOrWhiteSpace(result))
          {
            JObject models = COMM.GetToJsonList(result);
            if (models != null && models["result"] != null && models["result"]["default_model"] != null)
            {
              JToken info = models["result"]["default_model"]["item_info"];
              if (info["head_video_info"] != null && info["head_video_info"].Count<JToken>() > 0)
              {
                foreach (JToken im in info["head_video_info"])
                {
                  string videoId = im["video_id"].ToString();
                  dowvideo(videoId, item);
                  videoId = null;
                }
              }
              info = null;
            }
            models = null;
          }
          SKU.status = "下载完成";
          UPDATE(SKU);
          SKU = null;
          res = null;
          dics = null;
          result = null;
        }
        catch (Exception ex)
        {
          log("详情图片获取异常" + item);
        }
      }
      Path1 = null;
      skuls = null;
    }

    public async void dowvideo(string videoId, string itemid)
    {
      try
      {
        string Path1 = Environment.CurrentDirectory + "\\视频\\" + itemid;
        Dictionary<string, string> dics = new Dictionary<string, string>();
        dics.Add("param", "{\"scope\":\"itemvideo\",\"id\":\"" + videoId + "\"}");
        dics.Add("context", "{\"appid\":\"wxbuyer\",\"platform\":\"windows\",\"anonymousId\":\"2e5897f2472f2156e39ea46ed5af686393d2c365\",\"visitor_id\":\"2e5897f2472f2156e39ea46ed5af686393d2c365\",\"wxappid\":\"wx4b74228baa15489a\"}");
        string results = await flurl.POST("https://thor.weidian.com/vimg/queryVideoPublicUrl/1.0", dics);
        if (!string.IsNullOrWhiteSpace(results) && results.Contains("成功"))
        {
          JObject model = COMM.GetToJsonList(results);
          if (model != null && model["result"] != null && model["result"]["urls"] != null)
          {
            string url = model["result"]["urls"]["orig"].ToString();
            string str = await flurl.IMAGEDOW(url, Path1);
            url = null;
          }
          model = null;
        }
        Path1 = null;
        dics = null;
        results = null;
      }
      catch (Exception ex)
      {
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
      button4 = new Button();
      button3 = new Button();
      button2 = new Button();
      button1 = new Button();
      textBox12 = new TextBox();
      dataGridView1 = new DataGridView();
      SKU = new DataGridViewTextBoxColumn();
      标题 = new DataGridViewTextBoxColumn();
      描述 = new DataGridViewTextBoxColumn();
      主页数据 = new DataGridViewTextBoxColumn();
      详情数据 = new DataGridViewTextBoxColumn();
      状态 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
      button5 = new Button();
      panel1.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      SuspendLayout();
      panel1.Controls.Add(button5);
      panel1.Controls.Add(button4);
      panel1.Controls.Add(button3);
      panel1.Controls.Add(button2);
      panel1.Controls.Add(button1);
      panel1.Dock = DockStyle.Left;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(187, 463);
      panel1.TabIndex = 8;
      button4.Location = new Point(10, 82);
      button4.Name = "button4";
      button4.Size = new Size(168, 31);
      button4.TabIndex = 9;
      button4.Text = "初始化";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      button3.Location = new Point(10, 10);
      button3.Name = "button3";
      button3.Size = new Size(168, 31);
      button3.TabIndex = 8;
      button3.Text = "导入商品链接";
      button3.UseVisualStyleBackColor = true;
      button3.Click += new EventHandler(button3_Click);
      button2.Location = new Point(10, 118);
      button2.Name = "button2";
      button2.Size = new Size(168, 31);
      button2.TabIndex = 7;
      button2.Text = "一键上架";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      button1.Location = new Point(10, 46);
      button1.Name = "button1";
      button1.Size = new Size(168, 31);
      button1.TabIndex = 6;
      button1.Text = "开始采集";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      textBox12.BackColor = Color.Black;
      textBox12.Dock = DockStyle.Bottom;
      textBox12.Font = new Font("宋体", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
      textBox12.ForeColor = Color.Red;
      textBox12.Location = new Point(187, 311);
      textBox12.Multiline = true;
      textBox12.Name = "textBox12";
      textBox12.ReadOnly = true;
      textBox12.ScrollBars = ScrollBars.Vertical;
      textBox12.Size = new Size(606, 152);
      textBox12.TabIndex = 108;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(SKU, 标题, 描述, 主页数据, 详情数据, 状态);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(187, 0);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(606, 311);
      dataGridView1.TabIndex = 109;
      SKU.DataPropertyName = "SKU";
      SKU.HeaderText = "SKU";
      SKU.Name = "SKU";
      SKU.Width = 80;
      标题.DataPropertyName = "标题";
      标题.HeaderText = "标题";
      标题.Name = "标题";
      标题.Width = 200;
      描述.DataPropertyName = "描述";
      描述.HeaderText = "描述";
      描述.Name = "描述";
      主页数据.DataPropertyName = "主页数据";
      主页数据.HeaderText = "主页数据";
      主页数据.Name = "主页数据";
      详情数据.DataPropertyName = "详情数据";
      详情数据.HeaderText = "详情数据";
      详情数据.Name = "详情数据";
      状态.DataPropertyName = "状态";
      状态.HeaderText = "状态";
      状态.Name = "状态";
      dataGridViewTextBoxColumn1.DataPropertyName = "SKU";
      dataGridViewTextBoxColumn1.HeaderText = "SKU";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn1.Width = 80;
      dataGridViewTextBoxColumn2.DataPropertyName = "标题";
      dataGridViewTextBoxColumn2.HeaderText = "标题";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn2.Width = 200;
      dataGridViewTextBoxColumn3.DataPropertyName = "描述";
      dataGridViewTextBoxColumn3.HeaderText = "描述";
      dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      dataGridViewTextBoxColumn4.DataPropertyName = "型号数量";
      dataGridViewTextBoxColumn4.HeaderText = "型号数量";
      dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      dataGridViewTextBoxColumn5.DataPropertyName = "主页图片";
      dataGridViewTextBoxColumn5.HeaderText = "主页图片";
      dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      dataGridViewTextBoxColumn6.DataPropertyName = "主页视频";
      dataGridViewTextBoxColumn6.HeaderText = "主页视频";
      dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      dataGridViewTextBoxColumn7.DataPropertyName = "详情图片";
      dataGridViewTextBoxColumn7.HeaderText = "详情图片";
      dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      dataGridViewTextBoxColumn8.DataPropertyName = "详情视频";
      dataGridViewTextBoxColumn8.HeaderText = "详情视频";
      dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      dataGridViewTextBoxColumn9.DataPropertyName = "状态";
      dataGridViewTextBoxColumn9.HeaderText = "状态";
      dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
      button5.Location = new Point(10, 216);
      button5.Name = "button5";
      button5.Size = new Size(168, 31);
      button5.TabIndex = 10;
      button5.Text = "一键下载视频";
      button5.UseVisualStyleBackColor = true;
      button5.Click += new EventHandler(button5_Click);
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(dataGridView1);
      Controls.Add(textBox12);
      Controls.Add(panel1);
      Name = ("一键复制上架");
      Size = new Size(793, 463);
      Load += new EventHandler(一键复制上架_Load);
      panel1.ResumeLayout(false);
      ((ISupportInitialize) dataGridView1).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    public delegate void SetTextHandler(string text);

    private delegate void UpdateDataGridView(DataTable dt);
  }
}
