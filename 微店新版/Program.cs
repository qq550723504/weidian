// Decompiled with JetBrains decompiler
// Type: 微店新版.Program
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System;
using System.Data;
using System.Windows.Forms;
using 微店新版.H5;


namespace 微店新版
{
    internal static class Program
    {
        public static DataRow users;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (new LOGIN().ShowDialog() != DialogResult.OK)
                return;
            Application.Run(new Form1(users));
            //Application.Run(new Form2());
            //Application.Run(new 账密转换());
            //Application.Run(new weidian());
            //Application.Run(new 复活());
            //Application.Run(new 取openId());
            //Application.Run(new H5Frm());

        }
    }
}
