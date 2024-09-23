// Decompiled with JetBrains decompiler
// Type: 微店新版.Tool.水印
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版.Tool
{
  public class 水印 : UserControl
  {
    private bool ischeckbox = false;
    public static List<string> imglist = new List<string>();
    private Color selectedColor;
    public static Dictionary<string, itemsKu> skudic = new Dictionary<string, itemsKu>();
    private flurl flurl;
    public static int cop = 10;
    private IContainer components = null;
    private Button button5;
    private TextBox textBox8;
    private Button button4;
    private Panel panel1;
    private Button button1;
    private NumericUpDown numericUpDown1;
    private GroupBox groupBox2;
    private Button button2;
    private GroupBox groupBox1;
    private TextBox textBox12;
    private DataGridView dataGridView1;
    private Button button3;
    private Button button6;
    private DataGridViewTextBoxColumn SKU;
    private DataGridViewTextBoxColumn 商品名称;
    private DataGridViewTextBoxColumn 图片数量;
    private DataGridViewTextBoxColumn 完成状态;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

    public 水印() => InitializeComponent();

    private void log(string text)
    {
      if (textBox12.InvokeRequired)
        textBox12.Invoke(new 水印.SetTextHandler(log), text);
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
        BeginInvoke(new 水印.UpdateDataGridView(UpdateGV), dt);
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

    public void UPDATE(itemsKu users)
    {
      try
      {
        BeginInvoke(new EventHandler(delegate (object p0, EventArgs p1)
        {
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            if (row.Cells["SKU"].Value.ToString() == users.sku)
            {
              row.Cells["完成状态"].Value = users.msg;
              row.Cells["图片数量"].Value = users.descDic.Count;
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
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        folderBrowserDialog.Description = "选择文件夹";
        if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
          return;
                imglist = new List<string>();
                imglist = FindImages(folderBrowserDialog.SelectedPath);
        log("获取图片总数" + imglist.Count.ToString());
      }
    }

    private List<string> FindImages(string directoryPath)
    {
      List<string> images1 = new List<string>();
      foreach (string file in Directory.GetFiles(directoryPath))
      {
        if (IsImage(file))
          images1.Add(file);
      }
      foreach (string directory in Directory.GetDirectories(directoryPath))
      {
        List<string> images2 = FindImages(directory);
        images1.AddRange(images2);
      }
      return images1;
    }

    private bool IsImage(string filePath)
    {
      string lowerInvariant = Path.GetExtension(filePath).ToLowerInvariant();
      return lowerInvariant == ".jpg" || lowerInvariant == ".jpeg" || lowerInvariant == ".png" || lowerInvariant == ".gif" || lowerInvariant == ".bmp";
    }

    private void button1_Click(object sender, EventArgs e)
    {
      ColorDialog colorDialog = new ColorDialog();
      if (colorDialog.ShowDialog() != DialogResult.OK)
        return;
      selectedColor = colorDialog.Color;
      button1.BackColor = selectedColor;
    }

    private void button5_Click(object sender, EventArgs e)
    {
            txt = textBox8.Text;
      if (string.IsNullOrWhiteSpace(txt))
      {
        log("请输入水印文字");
      }
      else
      {
        Color selectedColor = this.selectedColor;
        if (false)
          log("请选择水印颜色");
        else if (imglist == null || imglist.Count == 0)
        {
          log("请导入图片");
        }
        else
        {
                    cop = Convert.ToInt32(numericUpDown1.Value);
          foreach (string str in imglist)
          {
            string fileName = Path.GetFileName(str);
            Point point = new Point();
            point.X = -10;
            point.Y = -100;
            Font font = new Font("微软雅黑", 19f, FontStyle.Bold);
            Color color = Color.FromArgb(Convert.ToInt32(cop), this.selectedColor.R, this.selectedColor.G, this.selectedColor.B);
            float rotate = 30f;
            int textWidth = 100;
            int textHeight = 30;
            int repeatD = 100;
            AddWaterText(fileName, str, txt, str, point, font, color, rotate, textWidth, textHeight, repeatD);
          }
          log("图片水印增加完成，保存在" + (Environment.CurrentDirectory + "\\水印"));
        }
      }
    }

    public void AddWaterText(
      string imgname,
      string oldpath,
      string text,
      string newpath,
      object point,
      Font font,
      Color color,
      float rotate = 0.0f,
      int textWidth = 1,
      int textHeight = 1,
      int repeatD = 0)
    {
      try
      {
        FileStream input = new FileStream(oldpath, FileMode.Open);
        BinaryReader binaryReader = new BinaryReader(input);
        byte[] buffer = binaryReader.ReadBytes((int) input.Length);
        binaryReader.Close();
        input.Close();
        Image image = Image.FromStream(new MemoryStream(buffer));
        int width = image.Width;
        int height = image.Height;
        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        bitmap.SetResolution(72f, 72f);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.FromName("white"));
        graphics.InterpolationMode = InterpolationMode.High;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
        SizeF sizeF1 = new SizeF();
        SizeF sizeF2 = graphics.MeasureString(text, font);
        float y = height - sizeF2.Height;
        float x = width - sizeF2.Width;
        new StringFormat().Alignment = StringAlignment.Center;
        if (point != null)
        {
          Point point1 = (Point) point;
          x = point1.X;
          y = point1.Y;
        }
        SolidBrush solidBrush = new SolidBrush(color);
        Color.FromArgb(1, 1, 1, 1);
        graphics.RotateTransform(rotate);
        if (repeatD == 0)
        {
          graphics.DrawString(text, font, solidBrush, x, y);
        }
        else
        {
          int num1 = width / textWidth + 3;
          int num2 = height / textHeight + 3;
          float num3 = x;
          for (int index1 = 0; index1 < num2; ++index1)
          {
            for (int index2 = 0; index2 < num1; ++index2)
            {
              for (int index3 = 0; index3 < num1; ++index3)
                graphics.DrawString(text, font, solidBrush, x, y);
              x += textWidth + repeatD;
            }
            x = num3;
            y += textHeight + repeatD;
          }
        }
        string path = Environment.CurrentDirectory + "\\水印";
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        bitmap.Save(path + "\\" + imgname);
        graphics.Dispose();
        image.Dispose();
        bitmap.Dispose();
      }
      catch
      {
      }
    }

    public void SKUAddWaterText(
      string imgname,
      string oldpath,
      string text,
      string newpath,
      object point,
      Font font,
      Color color,
      ref string local,
      float rotate = 0.0f,
      int textWidth = 1,
      int textHeight = 1,
      int repeatD = 0)
    {
      try
      {
        PictureBox pictureBox = new PictureBox();
        pictureBox.Load(oldpath);
        Image image = pictureBox.Image;
        int width = image.Width;
        int height = image.Height;
        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        bitmap.SetResolution(72f, 72f);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.FromName("white"));
        graphics.InterpolationMode = InterpolationMode.High;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
        SizeF sizeF1 = new SizeF();
        SizeF sizeF2 = graphics.MeasureString(text, font);
        float y = height - sizeF2.Height;
        float x = width - sizeF2.Width;
        new StringFormat().Alignment = StringAlignment.Center;
        if (point != null)
        {
          Point point1 = (Point) point;
          x = point1.X;
          y = point1.Y;
        }
        SolidBrush solidBrush = new SolidBrush(color);
        Color.FromArgb(1, 1, 1, 1);
        graphics.RotateTransform(rotate);
        if (repeatD == 0)
        {
          graphics.DrawString(text, font, solidBrush, x, y);
        }
        else
        {
          int num1 = width / textWidth + 3;
          int num2 = height / textHeight + 3;
          float num3 = x;
          for (int index1 = 0; index1 < num2; ++index1)
          {
            for (int index2 = 0; index2 < num1; ++index2)
            {
              for (int index3 = 0; index3 < num1; ++index3)
                graphics.DrawString(text, font, solidBrush, x, y);
              x += textWidth + repeatD;
            }
            x = num3;
            y += textHeight + repeatD;
          }
        }
        string path = Environment.CurrentDirectory + "\\水印";
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        bitmap.Save(path + "\\" + imgname);
        graphics.Dispose();
        image.Dispose();
        bitmap.Dispose();
        local = path + "\\" + imgname;
      }
      catch
      {
      }
    }

    private void 水印_Load(object sender, EventArgs e)
    {
      log("水印数字最多255，数字越大，透明度越低，数字越小，透明度越高");
      flurl = new flurl();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(COMM.cookie))
      {
        log("请登录主店铺");
      }
      else
      {
                skudic = new Dictionary<string, itemsKu>();
        new Thread(new ParameterizedThreadStart(start)).Start();
      }
    }

    private async void start(object obj)
    {
      int index = 0;
      while (true)
      {
        try
        {
          string res = await flurl.GET999("https://thor.weidian.com/wditem/itemList.pcListItems/1.0?param=%7B%22pageSize%22%3A100%2C%22pageNum%22%3A" + index.ToString() + "%2C%22listStatus%22%3A%222%22%2C%22sorts%22%3A%5B%7B%22field%22%3A%22add_time%22%2C%22mode%22%3A%22desc%22%7D%5D%2C%22shopId%22%3A%22%22%2C%22searchCondition%22%3A%7B%22cateId%22%3A%22%22%2C%22itemProperty%22%3A%5B%5D%2C%22itemId%22%3A%22%22%2C%22type%22%3A%22%22%7D%2C%22isSearch%22%3Atrue%7D&wdtoken=9f16d10a&_=1715849541988", COMM.cookie);
          if (res != null && !string.IsNullOrWhiteSpace(res))
          {
            JObject model = COMM.GetToJsonList(res);
            if (model != null && model["result"] != null && model["result"]["dataList"] != null)
            {
              foreach (JToken item in model["result"]["dataList"])
                                skudic.Add(item["itemId"].ToString(), new itemsKu()
                {
                  descDicT = new List<string>(),
                  descDicV = new List<string>(),
                  sku = item["itemId"].ToString(),
                  mylist = new List<string>(),
                  skuname = item["itemName"].ToString(),
                  msg = "",
                  descDic = new Dictionary<string, string>()
                });
              if (skudic.Count >= Convert.ToInt32(model["result"]["totalNum"].ToString()))
              {
                log("获取商品" + skudic.Count.ToString());
                break;
              }
              ++index;
              log("获取商品" + skudic.Count.ToString());
            }
            model = null;
          }
          res = null;
        }
        catch (Exception ex)
        {
        }
      }
      if (skudic == null || skudic.Count <= 0)
        return;
      DataTable newdt = new DataTable();
      newdt.Columns.Add(new DataColumn("SKU", typeof (string)));
      newdt.Columns.Add(new DataColumn("商品名称", typeof (string)));
      newdt.Columns.Add(new DataColumn("图片数量", typeof (string)));
      newdt.Columns.Add(new DataColumn("完成状态", typeof (string)));
      foreach (KeyValuePair<string, itemsKu> keyValuePair in skudic)
      {
        KeyValuePair<string, itemsKu> item = keyValuePair;
        DataRow dr = newdt.NewRow();
        dr["SKU"] = item.Key;
        dr["商品名称"] = item.Value.skuname;
        dr["图片数量"] = "";
        dr["完成状态"] = "";
        newdt.Rows.Add(dr);
        dr = null;
        item = new KeyValuePair<string, itemsKu>();
      }
      if (newdt != null && newdt.Rows.Count > 0)
        UpdateGV(newdt);
      log("商品数据加载完毕");
      newdt = null;
    }

    private async void Descstart(object obj)
    {
      List<string> skuls = (List<string>) obj;
      foreach (string item in skuls)
      {
        try
        {
          string res = await flurl.GET999("https://thor.weidian.com/detail/getDetailDesc/1.0?param=%7B%22vItemId%22%3A%22" + item + "%22%7D&wdtoken=9f16d10a&_=" + COMM.ConvertDateTimeToString(), COMM.cookie);
          if (!string.IsNullOrWhiteSpace(res))
          {
            JObject models = COMM.GetToJsonList(res);
            if (models != null && models["result"] != null && models["result"]["item_detail"] != null && models["result"]["item_detail"]["desc_content"] != null && models["result"]["item_detail"]["desc_content"].Count<JToken>() > 0)
            {
              itemsKu SKU = skudic[item];
              foreach (JToken s in models["result"]["item_detail"]["desc_content"])
              {
                if (s["type"].ToString() == "2")
                {
                  if (!SKU.descDic.ContainsKey(s["url"].ToString()))
                    SKU.descDic.Add(s["url"].ToString(), "");
                  if (!SKU.mylist.Contains(s["url"].ToString()))
                    SKU.mylist.Add(s["url"].ToString());
                }
                if (s["type"].ToString() == "1")
                  SKU.descDic.Add("文字" + Guid.NewGuid().ToString("N"), JsonConvert.SerializeObject(new
                  {
                      type = 1,
                      text = s[(object)"text"].ToString()
                  }));
                if (s["type"].ToString() == "11")
                  SKU.descDic.Add("视频" + Guid.NewGuid().ToString("N"), JsonConvert.SerializeObject(new
                  {
                      type = 11,
                      faceurl = s[(object)"face_url"].ToString(),
                      videoId = s[(object)"video_id"].ToString(),
                      auditStatus = 1
                  }));
              }
              UPDATE(SKU);
              log("详情图片获取成功" + item + "，获取数量：" + SKU.descDic.Count.ToString());
              SKU = null;
            }
            else
              log("详情图片获取失败" + item);
            models = null;
          }
          else
            log("详情图片获取失败" + item);
          res = null;
        }
        catch (Exception ex)
        {
          log("详情图片获取异常" + item);
        }
      }
      skuls = null;
    }

    public static string txt { get; set; }

    private void button3_Click_1(object sender, EventArgs e)
    {
            txt = textBox8.Text;
      if (string.IsNullOrWhiteSpace(txt))
      {
        log("请输入水印文字");
      }
      else
      {
        Color selectedColor = this.selectedColor;
        if (false)
        {
          log("请选择水印颜色");
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
                        cop = Convert.ToInt32(numericUpDown1.Value);
            List<string> stringList = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
              if (row.Cells[0].Value != null && (bool) row.Cells[0].Value)
                stringList.Add(row.Cells["SKU"].Value.ToString());
            }
            if (stringList == null || stringList.Count == 0)
              log("请勾选需要增加水印的SKU");
            else if (stringList.Count < 10)
            {
              new Thread(new ParameterizedThreadStart(SKUstart)).Start(stringList);
            }
            else
            {
              int num = stringList.Count / 5;
              foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(stringList, num))
                new Thread(new ParameterizedThreadStart(SKUstart)).Start(split.Value);
            }
          }
        }
      }
    }

    private async void SKUstart(object obj)
    {
      List<string> skuls = (List<string>) obj;
      foreach (string str1 in skuls)
      {
        string item = str1;
        try
        {
          log("开始采集" + item);
          itemsKu SKU = skudic[item];
          SKU.mylist.AsParallel<string>().ForAll<string>(y =>
          {
              log("开始生成水印" + item);
              string str2 = y;
              string fileName = Path.GetFileName(str2);
              Point point = new Point();
              point.X = -10;
              point.Y = -100;
              Font font = new Font("微软雅黑", 19f, FontStyle.Bold);
              Color color = Color.FromArgb(Convert.ToInt32(cop), selectedColor.R, selectedColor.G, selectedColor.B);
              float rotate = 30f;
              int textWidth = 100;
              int textHeight = 30;
              int repeatD = 100;
              string empty = string.Empty;
              SKUAddWaterText(fileName, str2, txt, str2, point, font, color, ref empty, rotate, textWidth, textHeight, repeatD);
              if (string.IsNullOrWhiteSpace(empty))
                  return;
              log("水印生成完毕" + item);
              string json = flurl.UploadImage("https://vimg.weidian.com/upload/v3/direct?scope=pcitem&fileType=image", empty, COMM.cookie);
              if (!string.IsNullOrWhiteSpace(json))
              {
                  JObject toJsonList = COMM.GetToJsonList(json);
                  if (toJsonList != null && toJsonList["result"] != null)
                  {
                      SKU.descDic[y] = toJsonList["result"]["url"].ToString();
                      log("水印保存成功" + item);
                  }
                  else
                      log("水印保存失败" + item);
              }
              else
                  log("水印保存失败" + item);
          });
        }
        catch (Exception ex1)
        {
          Exception ex = ex1;
          log(item + "出现异常");
        }
      }
      log("水印上传完成，开始进行替换");
      foreach (string item in skuls)
      {
        itemsKu SKU = skudic[item];
        try
        {
          log("开始替换" + item);
          Dictionary<string, string> dic = new Dictionary<string, string>();
          dic.Add("param", "{\"itemID\":\"" + item + "\",\"with_sku_attr\":1,\"include_hide_item\":1,\"userID\":\"\"}");
          string result2 = await flurl.POST999("https://thor.weidian.com/wditem/item.getInfo/1.0", COMM.cookie, dic);
          if (!string.IsNullOrWhiteSpace(result2))
          {
            JObject so = COMM.GetToJsonList(result2);
            JToken my = so["result"];
            List<int> itemDeliveryList = new List<int>();
            foreach (JToken items in my["itemDeliveryDTOs"])
              itemDeliveryList.Add(Convert.ToInt32(items["deliveryId"].ToString()));
            string n = string.Join<int>(",", itemDeliveryList);
            List<string> imgs = new List<string>();
            foreach (JToken items in my["Imgs"])
              imgs.Add("{\"url\":\"" + items.ToString() + "\",\"title\":\"\"}");
            List<string> jsonlist = new List<string>();
            foreach (KeyValuePair<string, string> keyValuePair in SKU.descDic)
            {
              KeyValuePair<string, string> m = keyValuePair;
              if (m.Key.Contains("文字") || m.Key.Contains("视频"))
                jsonlist.Add(m.Value);
              else
                jsonlist.Add("{\"text\":\"\",\"type\":2,\"url\":\"" + m.Value + "\"}");
              m = new KeyValuePair<string, string>();
            }
            string json = "{\"item_detail\":[" + string.Join(",", jsonlist) + "],\"from\":\"weidian_pc\",\"submitWithLimitWords\":false,\"submitWithSensitiveWord\":false,\"sdkVideoSupported\":true}";
            dic = new Dictionary<string, string>();
            dic.Add("param", json);
            dic.Add("wdtoken", COMM.r(8));
            string result = await flurl.POST999("https://thor.weidian.com/wditem/itemWrite.updateItemDetail/1.0", COMM.cookie, dic);
            if (!string.IsNullOrWhiteSpace(result) && result.Contains("OK"))
            {
              JObject od = COMM.GetToJsonList(result);
              string tmpDetailId = od["result"]["tmp_detail_id"].ToString();
              string headVideos = "[]";
              if (my["videoInfo"] != null && my["videoInfo"]["headVideos"] != null)
              {
                StringBuilder sb = new StringBuilder();
                foreach (JToken ims in my["videoInfo"]["headVideos"])
                  sb.Append("{\"auditStatus\":1,\"faceUrl\":\"" + ims["faceUrl"].ToString() + "\",\"type\":" + ims["type"].ToString() + ",\"videoId\":\"" + ims["videoId"].ToString() + "\"},");
                string sb1 = sb.ToString().Substring(0, sb.ToString().Length - 1);
                headVideos = "[" + sb1 + "]";
                sb = null;
                sb1 = null;
              }
              string isQuickOrder = GetValueString(my["isQuickOrder"]);
              string quickOrderSupportPromotion = GetValueString(my["quickOrderSupportPromotion"]);
              string itemID = GetValueString(my["itemID"]);
              string officialLimit = GetValueString(my["officialLimit"]);
              string itemBizType = "1";
              string itemName = GetValueString(my["itemName"]).Replace("\"", "");
              string merchant_code = GetValueString(my["merchant_code"]);
              string bgCateId = GetValueString(my["bg_cate"]["id"]);
              string cardType = "1";
              string sellingMethodType = GetValueString(my["sellingMethodType"]);
              string expressFeeTemplateId = GetValueString(my["remote_template_info"]["template_id"]);
              string freeDelivery = GetValueString(my["free_delivery"]);
              string remoteFreeDelivery = GetValueString(my["remote_free_delivery"]);
              string isWeightTemplate = GetValueString(my["is_weight_template"]);
              string cateIds = GetValueString(my["cates"] == null || my["cates"].Count<JToken>() == 0 ? (JToken) "" : my["cates"].First<JToken>()["cate_id"]);
              string addItemToRecommendCate = "null";
              string purchase_limit = "false";
              int purchase_cycle = 1;
              int purchase_type = 1;
              int purchase_dimension_type = 1;
              int isNeedIdNo = 0;
              string isHiddenSetting = "false";
              int itemStatus = 1;
              int areaTemplateId = 0;
              int areaTemplateFlag = 0;
              int isFutureSold = 0;
              string submitWithLimitWords = "false";
              string submitWithSensitiveWord = "false";
              string submitWithSpecificationKeyWord = "false";
              string isCheckQualification = "false";
              int cardSystemPub = 1;
              string validInfo = "{\"validType\":1}";
              string buy_template = GetValueString(my["buy_template"]);
              int chargeType = 1;
              string phoneChargeInfo = "{\"operator\":1,\"faceValue\":10,\"areaCode\":1001}";
              string itemPresale = "{\"presale\":false,\"deposit\":null,\"depositRate\":null,\"voucher\":null,\"depositType\":1,\"depositStart\":null,\"depositEnd\":null,\"payStart\":null,\"payEnd\":null,\"deliveryType\":2,\"deliveryStart\":null,\"deliveryEnd\":null,\"deliveryOffset\":null}";
              string fullAmountPreSale = "{\"presale\":false,\"endTime\":null,\"deliveryType\":2,\"deliveryStart\":null,\"deliveryEnd\":null,\"deliveryOffset\":null}";
              string quantifyItem = GetValueString(my["quantifyItem"]);
              string soldCommentRule = GetValueString(my["soldCommentRule"]);
              string itemGroupId = "null";
              string cardPriceContinueMode = GetValueString(my["cardPriceContinueMode"]);
              string hideSold = GetValueString(my["hideSold"]);
              int cardItemOrderSc = 0;
              string saleChannels = "[]";
              string noStockOffShelf = GetValueString(my["noStockOffShelf"]);
              string stockOnShelf = GetValueString(my["stockOnShelf"]);
              string shopMemberItemLimitModifyDo = "{\"officialLimit\":false,\"cardItemLimitModifyReqList\":[{\"cardType\":\"normal\",\"itemShopMemberLimitInfo\":[]},{\"cardType\":\"pay\",\"itemShopMemberLimitInfo\":[]}]}";
              int refundAddressId = -1;
              string timelinessId = GetValueString(my["timelines"]["timelinessId"]);
              string cardItemOverdue = GetValueString(my["cardItemOverdue"]) == "" ? "1" : GetValueString(my["cardItemOverdue"]);
              string attrList = GetValueString(my["attr_list"]) == "" ? "[]" : GetValueString(my["attr_list"]);
              string sku = GetValueJson(my["sku"]);
              string objjson = "{\"supportExchange\":null,\"saleFloorLimit\":null,\"floorLimitType\":0,\"lowStockWarningValue\":null,\"itemComment\":\"\",\"videos\":" + headVideos + ",\"sdkVideoSupported\":true,\"isQuickOrder\":" + isQuickOrder + ",\"quickOrderSupportPromotion\":" + quickOrderSupportPromotion + ",\"itemId\":\"" + itemID + "\",\"officialLimit\":" + officialLimit + ",\"itemDeliveryList\":[" + n + "],\"itemBizType\":" + itemBizType + ",\"certificateOption\":1,\"imgs\":[" + string.Join(",", imgs) + "],\"itemName\":\"" + itemName + "\",\"price\":\"\",\"pointPrice\":{\"price\":\"\",\"point\":\"\"},\"point_price_range\":3,\"merchantCode\":\"" + merchant_code + "\",\"hideStock\":false,\"bgCateId\":" + bgCateId + ",\"catePropList\":[],\"tmpDetailId\":\"" + tmpDetailId + "\",\"cardType\":" + cardType + ",\"sellingMethodType\":" + sellingMethodType + ",\"cyclePurchaseInfo\":null,\"expressFeeTemplateId\":" + expressFeeTemplateId + ",\"freeDelivery\":" + freeDelivery + ",\"remoteFreeDelivery\":" + remoteFreeDelivery + ",\"isWeightTemplate\":" + isWeightTemplate + ",\"cateIds\":\"" + cateIds + "\",\"addItemToRecommendCate\":" + addItemToRecommendCate + ",\"purchase_limit\":" + purchase_limit + ",\"purchase_cycle\":" + purchase_cycle.ToString() + ",\"purchase_type\":" + purchase_type.ToString() + ",\"purchase_dimension_type\":" + purchase_dimension_type.ToString() + ",\"isNeedIdNo\":" + isNeedIdNo.ToString() + ",\"isHiddenSetting\":" + isHiddenSetting + ",\"itemStatus\":" + itemStatus.ToString() + ",\"areaTemplateId\":" + areaTemplateId.ToString() + ",\"areaTemplateFlag\":" + areaTemplateFlag.ToString() + ",\"isFutureSold\":" + isFutureSold.ToString() + ",\"futureSoldTime\":\"\",\"offShelfTime\":\"\",\"submitWithLimitWords\":" + submitWithLimitWords + ",\"submitWithSensitiveWord\":" + submitWithSensitiveWord + ",\"submitWithSpecificationKeyWord\":" + submitWithSpecificationKeyWord + ",\"isCheckQualification\":" + isCheckQualification + ",\"cardSystemPub\":" + cardSystemPub.ToString() + ",\"validInfo\":" + validInfo + ",\"canNotRefund\":0,\"sellerTop\":null,\"buy_template\":" + buy_template + ",\"chargeType\":" + chargeType.ToString() + ",\"phoneChargeInfo\":" + phoneChargeInfo + ",\"itemPresale\":" + itemPresale + ",\"fullAmountPreSale\":" + fullAmountPreSale + ",\"quantifyItem\":" + quantifyItem + ",\"soldCommentRule\":" + soldCommentRule + ",\"itemGroupId\":" + itemGroupId + ",\"cardPriceContinueMode\":" + cardPriceContinueMode + ",\"hideSold\":" + hideSold + ",\"cardItemOrderSc\":" + cardItemOrderSc.ToString() + ",\"saleChannels\":" + saleChannels + ",\"onShelfTime\":\"\",\"noStockOffShelf\":" + noStockOffShelf + ",\"stockOnShelf\":" + stockOnShelf + ",\"shopMemberItemLimitModifyDo\":" + shopMemberItemLimitModifyDo + ",\"refundAddressId\":" + refundAddressId.ToString() + ",\"timelinessId\":" + timelinessId + ",\"cardItemOverdue\":" + cardItemOverdue + ",\"attrList\":" + attrList + ",\"showFilter\":false,\"addSkuPopoverVisible\":false,\"isSystemClassify\":false,\"fillLocal\":false,\"fillLocalPopover\":false,\"canRemove\":true,\"canSaveCommon\":true,\"canSelectFromCommon\":true,\"type\":\"COMMON\",\"skuHeaderExtendName\":\"\",\"skuAttrNameExtendNameTemplate\":\"\",\"showSaleFloorFilter\":false,\"showPriceLimitFilter\":false,\"commonSkuPopoverVisible\":false,\"sku\":" + sku + "}";
              dic = new Dictionary<string, string>();
              dic.Add("param", objjson);
              dic.Add("wdtoken", COMM.r(8));
              result = await flurl.POST999("https://thor.weidian.com/itemsell/item.update.pc/1.0", COMM.cookie, dic);
              if (!string.IsNullOrWhiteSpace(result) && result.Contains("成功"))
              {
                SKU.msg = "替换成功";
                UPDATE(SKU);
                log(item + "替换成功");
              }
              else
              {
                SKU.msg = "替换失败1";
                UPDATE(SKU);
                log(item + "替换失败1");
              }
              od = null;
              tmpDetailId = null;
              headVideos = null;
              isQuickOrder = null;
              quickOrderSupportPromotion = null;
              itemID = null;
              officialLimit = null;
              itemBizType = null;
              itemName = null;
              merchant_code = null;
              bgCateId = null;
              cardType = null;
              sellingMethodType = null;
              expressFeeTemplateId = null;
              freeDelivery = null;
              remoteFreeDelivery = null;
              isWeightTemplate = null;
              cateIds = null;
              addItemToRecommendCate = null;
              purchase_limit = null;
              isHiddenSetting = null;
              submitWithLimitWords = null;
              submitWithSensitiveWord = null;
              submitWithSpecificationKeyWord = null;
              isCheckQualification = null;
              validInfo = null;
              buy_template = null;
              phoneChargeInfo = null;
              itemPresale = null;
              fullAmountPreSale = null;
              quantifyItem = null;
              soldCommentRule = null;
              itemGroupId = null;
              cardPriceContinueMode = null;
              hideSold = null;
              saleChannels = null;
              noStockOffShelf = null;
              stockOnShelf = null;
              shopMemberItemLimitModifyDo = null;
              timelinessId = null;
              cardItemOverdue = null;
              attrList = null;
              sku = null;
              objjson = null;
            }
            else
            {
              SKU.msg = "替换失败2";
              UPDATE(SKU);
              log(item + "替换失败2");
            }
            so = null;
            my = null;
            itemDeliveryList = null;
            n = null;
            imgs = null;
            jsonlist = null;
            json = null;
            result = null;
          }
          else
          {
            SKU.msg = "获取详情失败";
            UPDATE(SKU);
            log(item + "获取详情失败");
          }
          dic = null;
          result2 = null;
        }
        catch (Exception ex2)
        {
          Exception ex = ex2;
          SKU.msg = "异常";
          UPDATE(SKU);
          log(item + "异常");
        }
        SKU = null;
      }
      skuls = null;
    }

    public string GetValueString(JToken o)
    {
      return o == null || string.IsNullOrWhiteSpace(o.ToString()) ? "" : o.ToString().ToLower();
    }

    public string GetValueJson(JToken o)
    {
      List<object> values = new List<object>();
      foreach (JToken jtoken in o)
      {
        string empty = string.Empty;
        if (jtoken["skuId"] != null && !string.IsNullOrWhiteSpace(jtoken["skuId"].ToString()))
          empty = jtoken["skuId"].ToString();
        if (jtoken["id"] != null && !string.IsNullOrWhiteSpace(jtoken["id"].ToString()))
          empty = jtoken["id"].ToString();
        values.Add("{\"hasInvalidBundleItem\":" + jtoken["hasInvalidBundleItem"].ToString().ToLower() + ",\"img\":\"" + jtoken["img"].ToString() + "\",\"price\":\"" + jtoken["price"].ToString() + "\",\"quantifyItem\":" + jtoken["quantifyItem"].ToString().ToLower() + ",\"stock\":\"" + jtoken["stock"].ToString() + "\",\"title\":\"" + jtoken["title"].ToString() + "\",\"weight\":\"" + jtoken["weight"].ToString() + "\",\"stop_sale\":false,\"skuId\":\"" + empty + "\",\"attrIds\":[" + string.Join(",", jtoken["attr_ids"].ToString().Split('-').ToList<string>()) + "],\"merchantCode\":\"" + jtoken["sku_merchant_code"].ToString() + "\",\"updateStockFlag\":0}");
      }
      return "[" + string.Join<object>(",", values) + "]";
    }

    private void button6_Click(object sender, EventArgs e)
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
          new Thread(new ParameterizedThreadStart(Descstart)).Start(stringList);
        }
        else
        {
          int num = stringList.Count / 5;
          foreach (KeyValuePair<string, List<string>> split in COMM.SplitList(stringList, num))
            new Thread(new ParameterizedThreadStart(Descstart)).Start(split.Value);
        }
      }
    }

    private void 水印_LocationChanged(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      button5 = new Button();
      textBox8 = new TextBox();
      button4 = new Button();
      panel1 = new Panel();
      button3 = new Button();
      groupBox2 = new GroupBox();
      button6 = new Button();
      button2 = new Button();
      groupBox1 = new GroupBox();
      numericUpDown1 = new NumericUpDown();
      button1 = new Button();
      textBox12 = new TextBox();
      dataGridView1 = new DataGridView();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      SKU = new DataGridViewTextBoxColumn();
      商品名称 = new DataGridViewTextBoxColumn();
      图片数量 = new DataGridViewTextBoxColumn();
      完成状态 = new DataGridViewTextBoxColumn();
      panel1.SuspendLayout();
      groupBox2.SuspendLayout();
      groupBox1.SuspendLayout();
      numericUpDown1.BeginInit();
      ((ISupportInitialize) dataGridView1).BeginInit();
      SuspendLayout();
      button5.Location = new Point(8, 260);
      button5.Name = "button5";
      button5.Size = new Size(168, 31);
      button5.TabIndex = 5;
      button5.Text = "开始增加水印（本地）";
      button5.UseVisualStyleBackColor = true;
      button5.Click += new EventHandler(button5_Click);
      textBox8.Font = new Font("宋体", 12f);
      textBox8.Location = new Point(8, 164);
      textBox8.Name = "textBox8";
      textBox8.Size = new Size(168, 26);
      textBox8.TabIndex = 4;
      textBox8.Text = "输入水印文字";
      button4.Location = new Point(8, 20);
      button4.Name = "button4";
      button4.Size = new Size(168, 31);
      button4.TabIndex = 3;
      button4.Text = "选择图片文件夹";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      panel1.Controls.Add(button3);
      panel1.Controls.Add(groupBox2);
      panel1.Controls.Add(groupBox1);
      panel1.Controls.Add(numericUpDown1);
      panel1.Controls.Add(button1);
      panel1.Controls.Add(button5);
      panel1.Controls.Add(textBox8);
      panel1.Dock = DockStyle.Left;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(187, 463);
      panel1.TabIndex = 6;
      button3.Location = new Point(8, 297);
      button3.Name = "button3";
      button3.Size = new Size(168, 31);
      button3.TabIndex = 10;
      button3.Text = "开始增加水印（商品）";
      button3.UseVisualStyleBackColor = true;
      button3.Click += new EventHandler(button3_Click_1);
      groupBox2.Controls.Add(button6);
      groupBox2.Controls.Add(button2);
      groupBox2.Dock = DockStyle.Top;
      groupBox2.Location = new Point(0, 62);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(187, 96);
      groupBox2.TabIndex = 9;
      groupBox2.TabStop = false;
      groupBox2.Text = "商品";
      button6.Location = new Point(8, 57);
      button6.Name = "button6";
      button6.Size = new Size(168, 31);
      button6.TabIndex = 4;
      button6.Text = "获取商品详情";
      button6.UseVisualStyleBackColor = true;
      button6.Click += new EventHandler(button6_Click);
      button2.Location = new Point(8, 20);
      button2.Name = "button2";
      button2.Size = new Size(168, 31);
      button2.TabIndex = 3;
      button2.Text = "获取全部商品";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      groupBox1.Controls.Add(button4);
      groupBox1.Dock = DockStyle.Top;
      groupBox1.Location = new Point(0, 0);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(187, 62);
      groupBox1.TabIndex = 8;
      groupBox1.TabStop = false;
      groupBox1.Text = "本地";
      numericUpDown1.Location = new Point(8, 233);
      numericUpDown1.Maximum = new Decimal(new int[4]
      {
         byte.MaxValue,
        0,
        0,
        0
      });
      numericUpDown1.Name = "numericUpDown1";
      numericUpDown1.Size = new Size(168, 21);
      numericUpDown1.TabIndex = 7;
      numericUpDown1.Value = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      button1.Location = new Point(8, 196);
      button1.Name = "button1";
      button1.Size = new Size(168, 31);
      button1.TabIndex = 6;
      button1.Text = "选择水印颜色";
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
      textBox12.TabIndex = 106;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(SKU, 商品名称, 图片数量, 完成状态);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(187, 0);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(606, 311);
      dataGridView1.TabIndex = 107;
      dataGridViewTextBoxColumn1.DataPropertyName = "SKU";
      dataGridViewTextBoxColumn1.HeaderText = "SKU";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn1.Width = 80;
      dataGridViewTextBoxColumn2.DataPropertyName = "商品名称";
      dataGridViewTextBoxColumn2.HeaderText = "商品名称";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn2.Width = 650;
      dataGridViewTextBoxColumn3.DataPropertyName = "图片数量";
      dataGridViewTextBoxColumn3.HeaderText = "图片数量";
      dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      dataGridViewTextBoxColumn4.DataPropertyName = "完成状态";
      dataGridViewTextBoxColumn4.HeaderText = "完成状态";
      dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      SKU.DataPropertyName = "SKU";
      SKU.HeaderText = "SKU";
      SKU.Name = "SKU";
      SKU.Width = 80;
      商品名称.DataPropertyName = "商品名称";
      商品名称.HeaderText = "商品名称";
      商品名称.Name = "商品名称";
      商品名称.Width = 650;
      图片数量.DataPropertyName = "图片数量";
      图片数量.HeaderText = "图片数量";
      图片数量.Name = "图片数量";
      完成状态.DataPropertyName = "完成状态";
      完成状态.HeaderText = "完成状态";
      完成状态.Name = "完成状态";
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(dataGridView1);
      Controls.Add(textBox12);
      Controls.Add(panel1);
      Name = ("水印");
      Size = new Size(793, 463);
      Load += new EventHandler(水印_Load);
      LocationChanged += new EventHandler(水印_LocationChanged);
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      groupBox2.ResumeLayout(false);
      groupBox1.ResumeLayout(false);
      numericUpDown1.EndInit();
      ((ISupportInitialize) dataGridView1).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    public delegate void SetTextHandler(string text);

    private delegate void UpdateDataGridView(DataTable dt);
  }
}
