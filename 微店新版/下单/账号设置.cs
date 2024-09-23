// Decompiled with JetBrains decompiler
// Type: 微店新版.下单.账号设置
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using Auxiliary.HTTP;
using MyWindowClient.DbHelper;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版.下单
{
  public class 账号设置 : UserControl
  {
    private bool ischeckbox = false;
    public static List<wdauth> authlist = new List<wdauth>();
    public static ConcurrentQueue<imgs> imgqueue = new ConcurrentQueue<imgs>();
    public static ConcurrentQueue<string> namequeue = new ConcurrentQueue<string>();
    private IContainer components = null;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private TextBox textBox7;
    private Panel panel1;
    private Button button3;
    private Button button4;
    private Button button6;
    private Panel panel2;
    private Button button1;
    private Panel panel3;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn 编号;
    private DataGridViewTextBoxColumn 账号;
    private DataGridViewTextBoxColumn 昵称;
    private DataGridViewTextBoxColumn 头像;
    private DataGridViewTextBoxColumn 状态;

    public 账号设置() => InitializeComponent();

    private void log(string text)
    {
      if (textBox7.InvokeRequired)
        textBox7.Invoke(new 账号设置.SetTextHandler(log), text);
      else
        textBox7.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
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
        BeginInvoke(new 账号设置.UpdateDataGridView(UpdateGV), dt);
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

    private void button3_Click(object sender, EventArgs e)
    {
      DataTable dataTable = MYSQL.Query("SELECT * FROM `wd_user1` where status='Y'");
      if (dataTable == null || dataTable.Rows.Count == 0)
      {
        log("暂无数据");
      }
      else
      {
                authlist = new List<wdauth>();
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("编号", typeof (string)));
        dt.Columns.Add(new DataColumn("账号", typeof (string)));
        dt.Columns.Add(new DataColumn("昵称", typeof (string)));
        dt.Columns.Add(new DataColumn("头像", typeof (string)));
        dt.Columns.Add(new DataColumn("状态", typeof (string)));
        foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
        {
          try
          {
            DataRow row2 = dt.NewRow();
            row2["编号"] = row1["original"].ToString();
            row2["账号"] = row1["phone"].ToString();
            row2["昵称"] = row1["nickname"].ToString();
            row2["头像"] = "";
            row2["状态"] = "";
            dt.Rows.Add(row2);
            wdauth wdauth = new wdauth()
            {
              code = "",
              nickName = row1["nickname"].ToString(),
              duid = row1["duid"].ToString(),
              loginToken = row1["loginToken"].ToString(),
              refreshToken = row1["refreshToken"].ToString(),
              original = row1["original"].ToString(),
              sid = row1["sid"].ToString(),
              uid = row1["uid"].ToString(),
              phone = row1["phone"].ToString()
            };
                        authlist.Add(wdauth);
          }
          catch (Exception ex)
          {
            log(ex.Message);
          }
        }
        if (dt != null && dt.Rows.Count > 0)
          UpdateGV(dt);
        log("账号获取成功，获取账号" + authlist.Count.ToString());
      }
    }

    private void button6_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      folderBrowserDialog.Description = "请选择图片文件路径";
      folderBrowserDialog.ShowNewFolderButton = false;
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      foreach (FileInfo file in new DirectoryInfo(folderBrowserDialog.SelectedPath).GetFiles())
      {
        string lower = Path.GetExtension(file.Name).ToLower();
        if (lower == ".jpg" || lower == ".jpeg" || lower == ".png" || lower == ".bmp" || lower == ".gif")
        {
          try
          {
            string fullName = file.FullName;
            string name = file.Name;
            byte[] pictureData = GetPictureData(fullName);
                        imgqueue.Enqueue(new imgs()
            {
              hz = lower,
              path = fullName,
              filename = name,
              imgby = pictureData
            });
          }
          catch (IOException ex)
          {
          }
        }
      }
      log(string.Format("导入图片{0}条", imgqueue.Count));
    }

    public byte[] GetPictureData(string imagePath)
    {
      FileStream fileStream = new FileStream(imagePath, FileMode.Open);
      byte[] buffer = new byte[fileStream.Length];
      fileStream.Read(buffer, 0, buffer.Length);
      fileStream.Close();
      return buffer;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "文本文档|*.txt";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
            namequeue = new ConcurrentQueue<string>();
      foreach (string readAllLine in File.ReadAllLines(openFileDialog.FileName.ToString(), Encoding.UTF8))
      {
        if (!string.IsNullOrEmpty(readAllLine))
                    namequeue.Enqueue(readAllLine);
      }
      log("导入昵称" + namequeue.Count.ToString());
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (imgqueue == null || imgqueue.Count == 0)
        log("请导入头像");
      else if (namequeue == null || namequeue.Count == 0)
      {
        log("请导入昵称");
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
          List<wdauth> parameter = new List<wdauth>();
          foreach (DataGridViewRow row in dataGridView1.Rows)
          {
            DataGridViewRow item = row;
            if (item.Cells[0].Value != null && (bool) item.Cells[0].Value)
            {
              List<wdauth> list = authlist.Where<wdauth>(p => p.original == item.Cells["编号"].Value.ToString()).ToList<wdauth>();
              if (list == null || list.Count == 0)
                log(item.Cells["账号"].Value.ToString() + "数据已被外部修改，检索不到对于值");
              else
                parameter.Add(list.First<wdauth>());
            }
          }
          if (parameter == null || parameter.Count == 0)
          {
            log("请勾选需要下单的账号");
          }
          else
          {
            wdauth wdauth = parameter[0];
            if (imgqueue.Count < parameter.Count)
              log("头像数量不能少于勾选的账号数量");
            else if (namequeue.Count < parameter.Count)
              log("昵称数量不能少于勾选的账号数量");
            else
              new Thread(new ParameterizedThreadStart(start)).Start(parameter);
          }
        }
      }
    }

    private async void start(object obj)
    {
      List<wdauth> list = (List<wdauth>) obj;
      foreach (wdauth item in list)
      {
        string cookie = COMM.GetCookie(item);
        imgs imgs;
                imgqueue.TryDequeue(out imgs);
        string name;
                namequeue.TryDequeue(out name);
        WebClient myWebClient = new WebClient();
        string result = flurl.DoPostFile("https://vimg.weidian.com/upload/v3/direct?scope=h5user&fileType=image", imgs.path, cookie);
        if (!string.IsNullOrWhiteSpace(result))
        {
          if (result.Contains("SUCCESS"))
          {
            string url = COMM.GetToJsonList(result)["result"]["url"].ToString();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("param", "{\"headurl\":\"" + url + "\"}");
            dic.Add("wdtoken", COMM.tokens(cookie));
            dic.Add("_", COMM.ConvertDateTimeToString());
            HttpRequestEntity od = await MYHTTP.Result("POST", "https://thor.weidian.com/passport/modify.info/1.0?param=%7B%22headurl%22%3A%22https%3A%2F%2Fsi.geilicdn.com%2Fh5user1686623992-4bc700000188af7bc82e0a23132e-unadjust_219_86.png%3Fw%3D160%26h%3D160%26cp%3D1%22%7D&wdtoken=48653869&_=1686571305128", 4, dic, cookie);
            if (od == null || !string.IsNullOrWhiteSpace(od.ResponseContent))
              ;
            url = null;
            dic = null;
            od = null;
          }
          else
            item.msg = result;
        }
        cookie = null;
        imgs = null;
        name = null;
        myWebClient = null;
        result = null;
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
      textBox7 = new TextBox();
      panel1 = new Panel();
      button3 = new Button();
      button4 = new Button();
      button6 = new Button();
      panel2 = new Panel();
      button1 = new Button();
      panel3 = new Panel();
      dataGridView1 = new DataGridView();
      编号 = new DataGridViewTextBoxColumn();
      账号 = new DataGridViewTextBoxColumn();
      昵称 = new DataGridViewTextBoxColumn();
      头像 = new DataGridViewTextBoxColumn();
      状态 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      panel1.SuspendLayout();
      panel2.SuspendLayout();
      panel3.SuspendLayout();
      ((ISupportInitialize) dataGridView1).BeginInit();
      SuspendLayout();
      textBox7.BackColor = SystemColors.ActiveCaptionText;
      textBox7.Dock = DockStyle.Fill;
      textBox7.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
      textBox7.ForeColor = Color.YellowGreen;
      textBox7.Location = new Point(0, 0);
      textBox7.Multiline = true;
      textBox7.Name = "textBox7";
      textBox7.ReadOnly = true;
      textBox7.ScrollBars = ScrollBars.Vertical;
      textBox7.Size = new Size(914, 151);
      textBox7.TabIndex = 7;
      panel1.Controls.Add(textBox7);
      panel1.Dock = DockStyle.Bottom;
      panel1.Location = new Point(0, 387);
      panel1.Name = "panel1";
      panel1.Size = new Size(914, 151);
      panel1.TabIndex = 7;
      button3.Location = new Point(16, 56);
      button3.Name = "button3";
      button3.Size = new Size(170, 25);
      button3.TabIndex = 4;
      button3.Text = "获取账号";
      button3.UseVisualStyleBackColor = true;
      button3.Click += new EventHandler(button3_Click);
      button4.Location = new Point(16, 118);
      button4.Name = "button4";
      button4.Size = new Size(170, 25);
      button4.TabIndex = 14;
      button4.Text = "导入昵称";
      button4.UseVisualStyleBackColor = true;
      button4.Click += new EventHandler(button4_Click);
      button6.Location = new Point(16, 87);
      button6.Name = "button6";
      button6.Size = new Size(170, 25);
      button6.TabIndex = 16;
      button6.Text = "导入头像";
      button6.UseVisualStyleBackColor = true;
      button6.Click += new EventHandler(button6_Click);
      panel2.Controls.Add(button1);
      panel2.Controls.Add(button6);
      panel2.Controls.Add(button4);
      panel2.Controls.Add(button3);
      panel2.Dock = DockStyle.Left;
      panel2.Location = new Point(0, 0);
      panel2.Name = "panel2";
      panel2.Size = new Size(200, 387);
      panel2.TabIndex = 8;
      button1.Location = new Point(16, 149);
      button1.Name = "button1";
      button1.Size = new Size(170, 25);
      button1.TabIndex = 17;
      button1.Text = "开始设置";
      button1.UseVisualStyleBackColor = true;
      button1.Click += new EventHandler(button1_Click);
      panel3.Controls.Add(dataGridView1);
      panel3.Dock = DockStyle.Fill;
      panel3.Location = new Point(200, 0);
      panel3.Name = "panel3";
      panel3.Size = new Size(714, 387);
      panel3.TabIndex = 9;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AllowUserToResizeRows = false;
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.Columns.AddRange(编号, 账号, 昵称, 头像, 状态);
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.Location = new Point(0, 0);
      dataGridView1.Margin = new Padding(2);
      dataGridView1.Name = "dataGridView1";
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.RowTemplate.Height = 27;
      dataGridView1.Size = new Size(714, 387);
      dataGridView1.TabIndex = 10;
      编号.DataPropertyName = "编号";
      编号.HeaderText = "编号";
      编号.Name = "编号";
      账号.DataPropertyName = "账号";
      账号.HeaderText = "账号";
      账号.Name = "账号";
      昵称.DataPropertyName = "昵称";
      昵称.HeaderText = "昵称";
      昵称.Name = "昵称";
      昵称.Width = 200;
      头像.DataPropertyName = "头像";
      头像.HeaderText = "头像";
      头像.Name = "头像";
      头像.Width = 300;
      状态.DataPropertyName = "状态";
      状态.HeaderText = "状态";
      状态.Name = "状态";
      状态.Width = 250;
      dataGridViewTextBoxColumn1.DataPropertyName = "账号";
      dataGridViewTextBoxColumn1.HeaderText = "账号";
      dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      dataGridViewTextBoxColumn2.DataPropertyName = "密码";
      dataGridViewTextBoxColumn2.HeaderText = "密码";
      dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      dataGridViewTextBoxColumn2.Width = 200;
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
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(panel3);
      Controls.Add(panel2);
      Controls.Add(panel1);
      Name = ("账号设置");
      Size = new Size(914, 538);
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      panel2.ResumeLayout(false);
      panel3.ResumeLayout(false);
      ((ISupportInitialize) dataGridView1).EndInit();
      ResumeLayout(false);
    }

    public delegate void SetTextHandler(string text);

    private delegate void UpdateDataGridView(DataTable dt);
  }
}
