// Decompiled with JetBrains decompiler
// Type: 微店新版.取openId
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using Auxiliary.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using 微店新版.HTTP;


namespace 微店新版
{
    public class 取openId : Form
    {
        private flurl flurl;
        public static List<string> openidlist = new List<string>();
        private IContainer components = null;
        private Button button2;
        private Button button1;
        private Button button3;
        private TextBox textBox3;
        private TextBox textBox9;
        private TextBox textBox10;
        private Label label9;
        private Label label10;
        private Button button4;

        public 取openId() => InitializeComponent();

        private void log(string text)
        {
            if (textBox3.InvokeRequired)
                textBox3.Invoke(new 取openId.SetTextHandler(log), text);
            else
                textBox3.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\r\n");
        }

        public static string token { get; set; }

        private async void button2_Click(object sender, EventArgs e)
        {
            string result = await flurl.API_POST("http://61.136.166.233:8668/api/login", new
            {
                user = "weidian",
                pwd = "weidian"
            });
            token = COMM.GetToJsonList(result)["token"].ToString();
            log(result);
            result = null;
        }

        private void 取openId_Load(object sender, EventArgs e)
        {
            IniFiles.inipath = Application.StartupPath + "\\Config.ini";
            if (!IniFiles.ExistINIFile())
                return;
            COMM.proxy_ip = IniFiles.IniReadValue("代理", "host");
            COMM.proxy_port = int.Parse(IniFiles.IniReadValue("代理", "proxy"));
            COMM.username = IniFiles.IniReadValue("代理", "username");
            COMM.password = IniFiles.IniReadValue("代理", "password");
            flurl = new flurl();
        }

        public static bool flog { get; set; }

        public static int fa { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox10.Text;
            fa = Convert.ToInt32(textBox9.Text);
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(fa.ToString()))
            {
                log("线程数量和阈值是必须输入的");
            }
            else
            {
                flog = true;
                for (int index = 0; index < Convert.ToInt32(text); ++index)
                    new Thread(new ParameterizedThreadStart(start)).Start("");
            }
        }

        private async void start(object obj)
        {
            // ISSUE: unable to decompile the method.
        }

        private void button4_Click(object sender, EventArgs e) => flog = false;

        private void button3_Click(object sender, EventArgs e)
        {
            if (openidlist == null || openidlist.Count == 0)
            {
                log("没有导出数据");
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
                    foreach (string str in openidlist)
                        streamWriter.WriteLine(str);
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
            button2 = new Button();
            button1 = new Button();
            button3 = new Button();
            textBox3 = new TextBox();
            textBox9 = new TextBox();
            textBox10 = new TextBox();
            label9 = new Label();
            label10 = new Label();
            button4 = new Button();
            SuspendLayout();
            button2.Location = new Point(234, 12);
            button2.Name = "button2";
            button2.Size = new Size(77, 23);
            button2.TabIndex = 3;
            button2.Text = "登录";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            button1.Location = new Point(317, 12);
            button1.Name = "button1";
            button1.Size = new Size(77, 23);
            button1.TabIndex = 5;
            button1.Text = "开始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            button3.Location = new Point(483, 12);
            button3.Name = "button3";
            button3.Size = new Size(77, 23);
            button3.TabIndex = 7;
            button3.Text = "导出";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            textBox3.Location = new Point(22, 44);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(544, 373);
            textBox3.TabIndex = 8;
            textBox9.Location = new Point(171, 13);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(57, 21);
            textBox9.TabIndex = 14;
            textBox10.Location = new Point(67, 13);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(57, 21);
            textBox10.TabIndex = 13;
            label9.AutoSize = true;
            label9.Location = new Point(134, 17);
            label9.Name = "label9";
            label9.Size = new Size(29, 12);
            label9.TabIndex = 12;
            label9.Text = "阈值";
            label10.AutoSize = true;
            label10.Location = new Point(30, 17);
            label10.Name = "label10";
            label10.Size = new Size(29, 12);
            label10.TabIndex = 11;
            label10.Text = "线程";
            button4.Location = new Point(400, 12);
            button4.Name = "button4";
            button4.Size = new Size(77, 23);
            button4.TabIndex = 15;
            button4.Text = "停止";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(599, 429);
            Controls.Add(button4);
            Controls.Add(textBox9);
            Controls.Add(textBox10);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(textBox3);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(button2);
            MaximizeBox = false;
            MaximumSize = new Size(615, 468);
            MinimumSize = new Size(615, 468);
            Name = "取openId";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "取openId";
            Load += new EventHandler(取openId_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        public delegate void SetTextHandler(string text);
    }
}