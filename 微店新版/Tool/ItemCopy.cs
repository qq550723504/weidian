// Decompiled with JetBrains decompiler
// Type: 微店新版.Tool.ItemCopy
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System.Collections.Generic;


namespace 微店新版.Tool
{
  public class ItemCopy
  {
    public string sku { get; set; }

    public string title { get; set; }

    public string desc { get; set; }

    public List<string> zhuyelist { get; set; }

    public List<string> zhuyelist1 { get; set; }

    public List<string> desclist { get; set; }

    public Dictionary<string, List<Item>> keyValuePairs { get; set; }

    public Dictionary<string, Root> SKUs { get; set; }

    public string status { get; set; }

    public string json1 { get; set; }

    public string attrListJson { get; set; }

    public string videosJson { get; set; }

    public string ImageJson { get; set; }

    public string SkuJson { get; set; }
  }
}
