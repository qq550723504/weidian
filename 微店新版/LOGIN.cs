// Decompiled with JetBrains decompiler
// Type: 微店新版.LOGIN
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using MyWindowClient.DbHelper;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace 微店新版
{
  public class LOGIN : Form
  {
    public const int WM_SYSCOMMAND = 274;
    public const int SC_MOVE = 61456;
    public const int HTCAPTION = 2;
    private IContainer components = null;
    private Button load_btn_min;
    private Button load_btn_close;
    private Button button21;
    private TextBox textBox3;
    private Label label4;
    private Label label1;
    private TextBox textBox1;
    private Label label2;

    public LOGIN() => InitializeComponent();

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    private void load_btn_min_Click(object sender, EventArgs e)
    {
      WindowState = FormWindowState.Minimized;
    }

    private void load_btn_close_Click(object sender, EventArgs e) => Close();

    private void LOGIN_MouseMove(object sender, MouseEventArgs e)
    {
            ReleaseCapture();
            SendMessage(Handle, 274, 61458, 0);
    }

    private void button21_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
      {
        int num1 = (int) MessageBox.Show("请输入账号或密码");
      }
      else
      {
        string text1 = textBox3.Text;
        string text2 = textBox1.Text;
        button21.Enabled = false;
        try
        {
          DataTable dataTable = MYSQL.Query("select * from wd_admin where username='" + text1 + "' and password='" + text2 + "'");
          if (dataTable == null || dataTable.Rows.Count == 0)
          {
            int num2 = (int) MessageBox.Show("账号或密码不正确");
            button21.Enabled = true;
          }
          else
          {
            string str = dataTable.Rows[0]["type"].ToString();
            string time = dataTable.Rows[0]["expire"].ToString();
            if (str == "N")
            {
              int num3 = (int) MessageBox.Show("账户已被禁用");
              button21.Enabled = true;
            }
            else if (dataTable.Rows[0]["role"].ToString() != "Y" && COMM.IsProcessTimeOut(time))
            {
              int num4 = (int) MessageBox.Show("账号于" + time + "过期，请续费");
              button21.Enabled = true;
            }
            else
            {
              IniFiles.IniWriteValue("用户", "账号", text1);
              IniFiles.IniWriteValue("用户", "密码", text2);
              Program.users = dataTable.Rows[0];
              DialogResult = DialogResult.OK;
            }
          }
        }
        catch (Exception ex)
        {
          int num5 = (int) MessageBox.Show(ex.Message);
          button21.Enabled = true;
        }
      }
    }

    private void LOGIN_Load(object sender, EventArgs e)
    {
      MYSQL.Init();
      IniFiles.inipath = Application.StartupPath + "\\Config.ini";
      if (!IniFiles.ExistINIFile())
        return;
      textBox3.Text = IniFiles.IniReadValue("用户", "账号");
      textBox1.Text = IniFiles.IniReadValue("用户", "密码");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LOGIN));
      load_btn_min = new Button();
      load_btn_close = new Button();
      button21 = new Button();
      textBox3 = new TextBox();
      label4 = new Label();
      label1 = new Label();
      textBox1 = new TextBox();
      label2 = new Label();
      SuspendLayout();
      load_btn_min.FlatAppearance.BorderSize = 0;
      load_btn_min.FlatStyle = FlatStyle.Flat;
      load_btn_min.Font = new Font("微软雅黑", 12f, FontStyle.Bold, GraphicsUnit.Point, 134);
      load_btn_min.ForeColor = Color.FromArgb(246, 246, 246);
      load_btn_min.Location = new Point(336, -2);
      load_btn_min.Name = "load_btn_min";
      load_btn_min.Size = new Size(26, 26);
      load_btn_min.TabIndex = 50;
      load_btn_min.Text = "━";
      load_btn_min.UseVisualStyleBackColor = true;
      load_btn_min.Click += new EventHandler(load_btn_min_Click);
      load_btn_close.FlatAppearance.BorderSize = 0;
      load_btn_close.FlatStyle = FlatStyle.Flat;
      load_btn_close.Font = new Font("微软雅黑", 12f, FontStyle.Bold, GraphicsUnit.Point, 134);
      load_btn_close.ForeColor = Color.FromArgb(246, 246, 246);
      load_btn_close.Location = new Point(362, -2);
      load_btn_close.Name = "load_btn_close";
      load_btn_close.Size = new Size(26, 26);
      load_btn_close.TabIndex = 51;
      load_btn_close.Text = "╳";
      load_btn_close.UseVisualStyleBackColor = false;
      load_btn_close.Click += new EventHandler(load_btn_close_Click);
      button21.BackColor = Color.Transparent;
      button21.Font = new Font("宋体", 13.8f, FontStyle.Bold, GraphicsUnit.Point, 134);
      button21.ForeColor = SystemColors.ActiveCaptionText;
      button21.Location = new Point(113, 143);
      button21.Margin = new Padding(2);
      button21.Name = "button21";
      button21.Size = new Size(193, 42);
      button21.TabIndex = 3;
      button21.Text = "登 录";
      button21.UseVisualStyleBackColor = false;
      button21.Click += new EventHandler(button21_Click);
      textBox3.BorderStyle = BorderStyle.FixedSingle;
      textBox3.Font = new Font("楷体", 12f, FontStyle.Bold, GraphicsUnit.Point, 134);
      textBox3.Location = new Point(113, 61);
      textBox3.Name = "textBox3";
      textBox3.Size = new Size(193, 26);
      textBox3.TabIndex = 1;
      label4.AutoSize = true;
      label4.Font = new Font("楷体", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
      label4.Location = new Point(57, 65);
      label4.Margin = new Padding(2, 0, 2, 0);
      label4.Name = "label4";
      label4.Size = new Size(51, 19);
      label4.TabIndex = 49;
      label4.Text = "账号";
      label1.AutoSize = true;
      label1.Font = new Font("楷体", 18f, FontStyle.Bold, GraphicsUnit.Point, 134);
      label1.Location = new Point(148, 19);
      label1.Margin = new Padding(2, 0, 2, 0);
      label1.Name = "label1";
      label1.Size = new Size(110, 24);
      label1.TabIndex = 48;
      label1.Text = "账号登录";
      textBox1.BorderStyle = BorderStyle.FixedSingle;
      textBox1.Font = new Font("楷体", 12f, FontStyle.Bold, GraphicsUnit.Point, 134);
      textBox1.Location = new Point(113, 104);
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(193, 26);
      textBox1.TabIndex = 2;
      label2.AutoSize = true;
      label2.Font = new Font("楷体", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 134);
      label2.Location = new Point(57, 108);
      label2.Margin = new Padding(2, 0, 2, 0);
      label2.Name = "label2";
      label2.Size = new Size(51, 19);
      label2.TabIndex = 52;
      label2.Text = "密码";
      AutoScaleDimensions = new SizeF(6f, 12f);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = SystemColors.ControlDarkDark;
      ClientSize = new Size(386, 198);
      Controls.Add(textBox1);
      Controls.Add(label2);
      Controls.Add(button21);
      Controls.Add(textBox3);
      Controls.Add(label4);
      Controls.Add(label1);
      Controls.Add(load_btn_min);
      Controls.Add(load_btn_close);
      ForeColor = Color.FromArgb(192, 192, 0);
      FormBorderStyle = FormBorderStyle.None;
      Name = "账号登录";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "账号登录";
      Load += new EventHandler(LOGIN_Load);
      MouseMove += new MouseEventHandler(LOGIN_MouseMove);
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
