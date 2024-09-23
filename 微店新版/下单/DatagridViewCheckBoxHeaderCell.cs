// Decompiled with JetBrains decompiler
// Type: 微店新版.下单.DatagridViewCheckBoxHeaderCell
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace 微店新版.下单
{
  internal class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
  {
    private Point checkBoxLocation;
    private Size checkBoxSize;
    private bool _checked = false;
    private Point _cellLocation = new Point();
    private CheckBoxState _cbState = CheckBoxState.UncheckedNormal;

    public event CheckBoxClickedHandler OnCheckBoxClicked;

    protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates dataGridViewElementState,
      object value,
      object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
    {
      base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
      Point point = new Point();
      Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
      point.X = cellBounds.Location.X + cellBounds.Width / 2 - glyphSize.Width / 2;
      point.Y = cellBounds.Location.Y + cellBounds.Height / 2 - glyphSize.Height / 2;
      _cellLocation = cellBounds.Location;
      checkBoxLocation = point;
      checkBoxSize = glyphSize;
      _cbState = !_checked ? CheckBoxState.UncheckedNormal : CheckBoxState.CheckedNormal;
      CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
    }

    protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
    {
      Point point = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
      if (point.X >= checkBoxLocation.X && point.X <= checkBoxLocation.X + checkBoxSize.Width && point.Y >= checkBoxLocation.Y && point.Y <= checkBoxLocation.Y + checkBoxSize.Height)
      {
        _checked = !_checked;
        if (OnCheckBoxClicked != null)
        {
          OnCheckBoxClicked(_checked);
          DataGridView.InvalidateCell(this);
        }
      }
      base.OnMouseClick(e);
    }
  }
}
