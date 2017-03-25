using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using System.Diagnostics;
using OfficeOpenXml.Style;
using iTextSharp.text;

namespace AppPresentators.Infrastructure.OrderBuilders
{
    public class ExcelOrderBuilder : IOrderBuilder
    {
        public event Action<string> ErrorBuild;

        private FileInfo _docInfo;

        private ExcelPackage _package;

        private ExcelWorksheet _worksheet;

        private bool _wasCreated = false;

        private int _currentRow = 1;

        private int _imageCount = 1;

        private int _currentCol = 1;

        private bool _wasError = false;
        public bool WasError { get { return _wasError; } set { _wasError = value; } }

        public ExcelOrderBuilder(){ }

        public void SetOrderPath(DirectoryInfo dirInfo, string orderName)
        {
            if (!Directory.Exists(dirInfo.FullName))
            {
                if (ErrorBuild != null) ErrorBuild("Папка " + dirInfo.FullName + " не найдена.");
                return;
            }

            _docInfo = new FileInfo(dirInfo.FullName + @"/" + orderName + ".xlsx");

           
            try {
                if (_docInfo.Exists)
                {
                    _docInfo.Delete();
                    _docInfo = new FileInfo(dirInfo.FullName + @"/" + orderName + ".xlsx");
                }
                _package = new ExcelPackage(_docInfo);

                _worksheet = _package.Workbook.Worksheets.Add("Order");

                _wasCreated = true;
            }catch(Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно создать отчет. Возможно файл занят другим процессом.");
                _wasError = true;
            }
        }

        public void DrawImage(System.Drawing.Image img, Align al)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                return;
            }
            try
            {
                Bitmap image = new Bitmap(img);
                ExcelPicture excelImage = null;
                if (image != null)
                {
                    excelImage = _worksheet.Drawings.AddPicture("image_" + _imageCount, image);

                    _imageCount++;

                    excelImage.From.Column = 0;

                    excelImage.From.Row = _currentRow;

                    excelImage.SetSize(540, img.Height * 540 / img.Width);

                    excelImage.From.ColumnOff = Pixel2MTU(0);

                    excelImage.From.RowOff = Pixel2MTU(_currentRow);

                    _currentRow += (img.Height * 27 / img.Width) + 3;
                }
            }
            catch (Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно создать отчет. Возможно файл занят другим процессом.");
                _wasError = true;
            }
        }
        
        public int Pixel2MTU(int pixels)
        {
            int mtus = pixels * 9525;
            return mtus;
        }

        public void EndPharagraph()
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                _wasError = true;
                return;
            }

            _currentCol = 1;

            _currentRow++;
        }

        public void EndTable(string name)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                _wasError = true;
                return;
            }

            _currentRow++;
        }

        public string Save()
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                _wasError = true;
                return null;
            }
            try
            {

                _worksheet.Cells.AutoFitColumns(0);

                _package.Save();

            }
            catch (Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно создать отчет. Возможно файл занят другим процессом.");
                _wasError = true;

                return null;
            }
            return _docInfo.FullName;
        }

        public void StartPharagraph(Align al)
        {
            try
            {
                switch (al)
                {
                    case Align.CENTER:
                        _worksheet.Cells[_currentRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        break;
                    case Align.LEFT:
                        _worksheet.Cells[_currentRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        break;
                    case Align.RIGHT:
                        _worksheet.Cells[_currentRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        break;
                }

                _currentCol = 1;
            }
            catch (Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно создать отчет. Возможно файл занят другим процессом.");
                _wasError = true;
            }
        }

        public void StartTable(string name, string[] headers)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                _wasError = true;
                return;
            }
            try
            {
                for (int i = 0; i < headers.Length; i++)
                {
                    _worksheet.Cells[_currentRow, i + 1].Value = headers[i];
                    _worksheet.Cells[_currentRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    _worksheet.Cells[_currentRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                }

                _currentRow++;
            }
            catch (Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно создать отчет. Возможно файл занят другим процессом.");
                _wasError = true;
            }
        }

        public void WriteRow(string[] cells, RowColor color = RowColor.DEFAULT)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                _wasError = true;
                return;
            }
            try
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    _worksheet.Cells[_currentRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    _worksheet.Cells[_currentRow, i + 1].Value = cells[i];

                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    _worksheet.Cells[_currentRow, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    switch (color)
                    {
                        case RowColor.GREEN:
                            _worksheet.Cells[_currentRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(223, 240, 216));
                            break;
                        case RowColor.RED:
                            _worksheet.Cells[_currentRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 222, 222));
                            break;
                        case RowColor.YELLOW:
                            _worksheet.Cells[_currentRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(252, 248, 227));
                            break;
                        case RowColor.DEFAULT:
                            _worksheet.Cells[_currentRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
                            break;
                    }
                }

                _currentRow++;
            }
            catch (Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно создать отчет. Возможно файл занят другим процессом.");
            }
        }

        public void WriteText(string text, Color color, TextStyle style = TextStyle.NORMAL, float size = 12)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                _wasError = true;
                return;
            }
            try
            {
                _worksheet.Cells[_currentRow, _currentCol].Value = text;
            
                _worksheet.Cells[_currentRow, _currentCol].Style.Font.Color.SetColor(color);

                _worksheet.Cells[_currentRow, _currentCol].Style.Font.Size = size;

                switch (style)
                {
                    case TextStyle.BOLD:
                        _worksheet.Cells[_currentRow, _currentCol].Style.Font.Bold = true;
                        break;
                    case TextStyle.ITALIC:
                        _worksheet.Cells[_currentRow, _currentCol].Style.Font.Italic = true;
                        break;
                    case TextStyle.NORMAL:
                        _worksheet.Cells[_currentRow, _currentCol].Style.Font.Bold = false;
                        _worksheet.Cells[_currentRow, _currentCol].Style.Font.Italic = false;
                        break;
                }

                _currentCol++;
            }
            catch (Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно создать отчет. Возможно файл занят другим процессом.");
                _wasError = true;
            }
        }
    }
}
