// Decompiled with JetBrains decompiler
// Type: 微店新版.H5.H5Frm
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace 微店新版.H5
{
  public class H5Frm : Form
  {
    private IContainer components = null;

    public H5Frm() => InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      components = new Container();
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Text = ("H5Frm");
    }
  }
}
