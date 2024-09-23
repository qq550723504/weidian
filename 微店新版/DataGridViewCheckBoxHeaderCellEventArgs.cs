// Decompiled with JetBrains decompiler
// Type: 微店新版.DataGridViewCheckBoxHeaderCellEventArgs
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System;


namespace 微店新版
{
  public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
  {
    private bool _bChecked;

    public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked) => _bChecked = bChecked;

    public bool Checked => _bChecked;
  }
}
