// Decompiled with JetBrains decompiler
// Type: 微店新版.Tool.采集评论
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版.Tool
{
  public class 采集评论 : UserControl
  {
    private bool ischeckbox = false;
    private flurl flurl;
    public static List<SkucommentList> list = new List<SkucommentList>();
    private IContainer components = null;
    private Panel panel1;
    private Button button1;
    private TextBox textBox8;
    private TextBox textBox12;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private Button button2;
    private DataGridViewTextBoxColumn SKU;
    private DataGridViewTextBoxColumn 型号;
    private DataGridViewTextBoxColumn 评论时间;
    private DataGridViewTextBoxColumn 用户昵称;
    private DataGridViewTextBoxColumn 评论内容;
    private DataGridViewTextBoxColumn 是否带图;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

    public 采集评论() => InitializeComponent();

    private void log(string text)
    {
      if (textBox12.InvokeRequired)
        textBox12.Invoke(new 采集评论.SetTextHandler(log), text);
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
        BeginInvoke(new 采集评论.UpdateDataGridView(UpdateGV), dt);
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

    private void button1_Click(object sender, EventArgs e)
    {
      string text = textBox8.Text;
      if (string.IsNullOrWhiteSpace(text))
      {
        log("请输入SKU");
      }
      else
      {
                list = new List<SkucommentList>();
        new Thread(new ParameterizedThreadStart(start)).Start(text);
      }
    }

    private async void start(object obj)
    {
      int index = 0;
      log("开始采集");
      while (true)
      {
        try
        {
          string res = await flurl.GET999("https://thor.weidian.com/wdcomment/queryCommentList/1.0?param=%7B%22vItemId%22%3A%22" + obj.ToString() + "%22%2C%22pageNum%22%3A" + index.ToString() + "%2C%22pageSize%22%3A1000%2C%22querySource%22%3A%22%22%2C%22topCommentId%22%3A%22%22%2C%22excludeIdList%22%3A%5B%5D%2C%22scoreType%22%3A0%7D&wdtoken=9f16d10a&_=" + COMM.ConvertDateTimeToString(), COMM.cookie);
          if (res != null && !string.IsNullOrWhiteSpace(res))
          {
            JObject model = COMM.GetToJsonList(res);
            if (model != null && model["result"] != null && model["result"]["commentList"] != null && model["result"]["commentList"].Count<JToken>() > 0)
            {
              foreach (JToken item in model["result"]["commentList"])
              {
                try
                {
                                    list.Add(new SkucommentList()
                  {
                    time = item["addTime"].ToString(),
                    nickname = item["buyerVO"]["buyerName"].ToString(),
                    content = item["comment"].ToString(),
                    imageslist = imglist(item["imageList"]),
                    sku = obj.ToString(),
                    type = skulist(item["skuList"])
                  });
                  log("采集内容" + item["comment"].ToString());
                }
                catch (Exception ex)
                {
                }
              }
              ++index;
              model = null;
            }
            else
              break;
          }
          res = null;
        }
        catch (Exception ex)
        {
        }
      }
      if (list != null && list.Count > 0)
      {
        DataTable newdt = new DataTable();
        newdt.Columns.Add(new DataColumn("SKU", typeof (string)));
        newdt.Columns.Add(new DataColumn("型号", typeof (string)));
        newdt.Columns.Add(new DataColumn("评论时间", typeof (string)));
        newdt.Columns.Add(new DataColumn("用户昵称", typeof (string)));
        newdt.Columns.Add(new DataColumn("评论内容", typeof (string)));
        newdt.Columns.Add(new DataColumn("是否带图", typeof (string)));
        foreach (SkucommentList item in list)
        {
          DataRow dr = newdt.NewRow();
          dr["SKU"] = item.sku;
          dr["型号"] = item.type;
          dr["评论时间"] = item.time;
          dr["用户昵称"] = item.nickname;
          dr["评论内容"] = item.content;
          dr["是否带图"] = item.imageslist.Count;
          newdt.Rows.Add(dr);
          dr = null;
        }
        if (newdt != null && newdt.Rows.Count > 0)
          UpdateGV(newdt);
        log("数据采集完毕，本次采集" + list.Count.ToString());
        newdt = null;
      }
      else
        log("没有评论数据");
    }

    public List<string> imglist(JToken o)
    {
      List<string> stringList = new List<string>();
      try
      {
        if (o != null && o.Count<JToken>() > 0)
        {
          foreach (JToken jtoken in o)
            stringList.Add(jtoken.ToString());
        }
      }
      catch (Exception ex)
      {
      }
      return stringList;
    }

    public string skulist(JToken o)
    {
      List<string> values = new List<string>();
      try
      {
        if (o != null && o.Count<JToken>() > 0)
        {
          foreach (JToken jtoken in o)
            values.Add(jtoken["itemSkuId"].ToString() + ">" + jtoken["skuTitle"].ToString());
        }
      }
      catch (Exception ex)
      {
      }
      return values.Count == 0 ? "" : string.Join("，", values);
    }

    private void 采集评论_Load(object sender, EventArgs e) => flurl = new flurl();

    private void button2_Click(object sender, EventArgs e)
    {
      if (list == null || list.Count == 0)
      {
        log("没有需要导出的数据");
      }
      else
      {
        string selectedFolderPath = string.Empty;
        using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
        {
          folderBrowserDialog.Description = "请选择导出存储位置";
          if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            selectedFolderPath = folderBrowserDialog.SelectedPath;
        }
        if (!string.IsNullOrEmpty(selectedFolderPath))
        {
          StreamWriter streamWriter = new StreamWriter(selectedFolderPath + "\\" + list.First<SkucommentList>().sku + ".txt", false);
          try
          {
            foreach (SkucommentList skucommentList in list)
            {
              streamWriter.WriteLine(skucommentList.content);
              if (skucommentList.imageslist != null && skucommentList.imageslist.Count > 0)
              {
                foreach (string str in skucommentList.imageslist)
                {
                  string i = str;
                  Task.Run(async () =>
                  {
                      string ims = await flurl.IMAGEDOW(i, selectedFolderPath);
                      ims = null;
                  });
                }
              }
            }
          }
          catch
          {
          }
          finally
          {
            streamWriter.Close();
          }
        }
        else
          log("存储位置为空");
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
      button2 = new Button();
      button1 = new Button();
      textBox8 = new TextBox();
      textBox12 = new TextBox();
      dataGridView1 = new DataGridView();
      SKU = new DataGridViewTextBoxColumn();
      型号 = new DataGridViewTextBoxColumn();
      评论时间 = new DataGridViewTextBoxColumn();
      用户昵称 = new DataGridViewTextBoxColumn();
      评论内容 = new DataGridViewTextBoxColumn();
      是否带图 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      panel1.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      SuspendLayout();
      panel1.Controls.Add(button2);
      panel1.Controls.Add(button1);
      panel1.Controls.Add(textBox8);
      panel1.Dock = DockStyle.Left;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(187, 463);
      panel1.TabIndex = 7;
      button2.Location = new Point(10, 84);
      button2.Name = "button2";
      button2.Size = new Size(168, 31);
      button2.TabIndex = 7;
      button2.Text = "开始导出";
      button2.UseVisualStyleBackColor = true;
      button2.Click += new EventHandler(button2_Click);
      button1.Location = new Point(10, 47);
      button1.Name = "button1";
      button1.Size = new Size(168, 31);
      button1.TabIndex = 6;
      button1.Text = "开始采集";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      textBox8.Font = new Font("宋体", 12f);
      textBox8.Location = new Point(10, 15);
      textBox8.Name = "textBox8";
      textBox8.Size = new Size(168, 26);
      textBox8.TabIndex = 4;
      textBox8.Text = "商品SKU";
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
      textBox12.TabIndex = 107;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(SKU, 型号, 评论时间, 用户昵称, 评论内容, 是否带图);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(187, 0);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(606, 311);
      dataGridView1.TabIndex = 108;
      SKU.DataPropertyName = "SKU";
      SKU.HeaderText = "SKU";
      SKU.Name = "SKU";
      SKU.Width = 80;
      型号.DataPropertyName = "型号";
      型号.HeaderText = "型号";
      型号.Name = "型号";
      型号.Width = 120;
      评论时间.DataPropertyName = "评论时间";
      评论时间.HeaderText = "评论时间";
      评论时间.Name = "评论时间";
      用户昵称.DataPropertyName = "用户昵称";
      用户昵称.HeaderText = "用户昵称";
      用户昵称.Name = "用户昵称";
      评论内容.DataPropertyName = "评论内容";
      评论内容.HeaderText = "评论内容";
      评论内容.Name = "评论内容";
      评论内容.Width = 450;
      是否带图.DataPropertyName = "是否带图";
      是否带图.HeaderText = "是否带图";
      是否带图.Name = "是否带图";
      dataGridViewTextBoxColumn1.DataPropertyName = "SKU";
      dataGridViewTextBoxColumn1.HeaderText = "SKU";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn1.Width = 80;
      dataGridViewTextBoxColumn2.DataPropertyName = "型号";
      dataGridViewTextBoxColumn2.HeaderText = "型号";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn2.Width = 120;
      dataGridViewTextBoxColumn3.DataPropertyName = "用户昵称";
      dataGridViewTextBoxColumn3.HeaderText = "用户昵称";
      dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      dataGridViewTextBoxColumn4.DataPropertyName = "评论内容";
      dataGridViewTextBoxColumn4.HeaderText = "评论内容";
      dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      dataGridViewTextBoxColumn4.Width = 400;
      dataGridViewTextBoxColumn5.DataPropertyName = "是否带图";
      dataGridViewTextBoxColumn5.HeaderText = "是否带图";
      dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      dataGridViewTextBoxColumn5.Width = 450;
      dataGridViewTextBoxColumn6.DataPropertyName = "是否带图";
      dataGridViewTextBoxColumn6.HeaderText = "是否带图";
      dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(dataGridView1);
      Controls.Add(textBox12);
      Controls.Add(panel1);
      Name =  ("采集评论");
      Size = new Size(793, 463);
      Load += new EventHandler(采集评论_Load);
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      ((ISupportInitialize) dataGridView1).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    public delegate void SetTextHandler(string text);

    private delegate void UpdateDataGridView(DataTable dt);
  }
}
