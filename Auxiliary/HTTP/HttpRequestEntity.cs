// Decompiled with JetBrains decompiler
// Type: Auxiliary.HTTP.HttpRequestEntity
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System.Diagnostics;


namespace Auxiliary.HTTP
{
  public class HttpRequestEntity
  {
    public int IsSuccess { get; set; }

    public string ResponseContent { get; set; }

    public long ResponseLength { get; set; }

    public string ResponseEncodingName { get; set; }

    public string cookie { get; set; }

    public Stopwatch ts { get; set; }
  }
}
