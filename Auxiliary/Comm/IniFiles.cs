// Decompiled with JetBrains decompiler
// Type: Auxiliary.Comm.IniFiles
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace Auxiliary.Comm
{
  public static class IniFiles
  {
    public static string inipath;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(
      string section,
      string key,
      string val,
      string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(
      string section,
      string key,
      string def,
      StringBuilder retVal,
      int size,
      string filePath);

    public static void IniWriteValue(string Section, string Key, string Value)
    {
            WritePrivateProfileString(Section, Key, Value, inipath);
    }

    public static string IniReadValue(string Section, string Key)
    {
      StringBuilder retVal = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, "", retVal, 500, inipath);
      return retVal.ToString();
    }

    public static bool ExistINIFile() => File.Exists(inipath);
  }
}
