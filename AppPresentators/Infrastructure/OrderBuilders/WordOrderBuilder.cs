using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPresentators.Infrastructure.OrderBuilders
{
    public class WordOrderBuilder : IOrderBuilder
    {
        public event Action<string> ErrorBuild;

        private DirectoryInfo _dirInfo;

        private string _orderName;

        private bool _wasCreated;

        private Microsoft.Office.Interop.Word.Application _winword;

        private Microsoft.Office.Interop.Word.Document _document;

        Microsoft.Office.Interop.Word.Paragraph _currentParagr;

        private List<string> _tableHeaders = null;

        private string _tableName = null;

        private List<TableRow> _rows = null;

        private object _missing;

        public bool WasError { get; set; } = false;

        public void DrawImage(Image img, Align al)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                WasError = true;
                return;
            }

            string dir = Directory.GetCurrentDirectory() + @"/cache/";

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string imagePath = dir + "cahce_" + DateTime.Now.Millisecond.ToString() + ".jpg";

            img.Save(imagePath);

            _currentParagr = _document.Content.Paragraphs.Add(ref _missing);

            _currentParagr.Range.InlineShapes.AddPicture(imagePath);

            switch (al)
            {
                case Align.CENTER:
                    _currentParagr.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    break;
                case Align.LEFT:
                    _currentParagr.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case Align.RIGHT:
                    _currentParagr.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    break;
            }

            _currentParagr = null;
        }

        public void EndPharagraph()
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                WasError = true;
                return;
            }

            _currentParagr.Range.InsertParagraphAfter();

            _currentParagr = null;
        }

        public void EndTable(string name)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                WasError = true;
                return;
            }

            _currentParagr = _document.Content.Paragraphs.Add(ref _missing);

            Table table = _document.Tables.Add(_currentParagr.Range, _rows.Count + 1, _tableHeaders.Count, ref _missing, ref _missing);
            
            table.Borders.Enable = 1;

            for(int i = 0; i < _tableHeaders.Count; i++)
            {
                table.Rows[1].Cells[i + 1].Range.Text = _tableHeaders[i];
                table.Rows[1].Cells[i + 1].Shading.BackgroundPatternColor = WdColor.wdColorGray25;
            }

            for(int i = 0; i < _rows.Count; i++)
            {
                for(int j = 0; j < _rows[i].Cells.Length; j++)
                {
                    table.Rows[i + 2].Cells[j + 1].Range.Text = _rows[i].Cells[j];
                    switch (_rows[i].RowColor)
                    {
                        case RowColor.GREEN:
                            var green = (Microsoft.Office.Interop.Word.WdColor)(223 + 0x100 * 240 + 0x10000 * 216);
                            table.Rows[i + 2].Cells[j + 1].Shading.BackgroundPatternColor = green;// WdColor.wdColorLightGreen;
                            break;
                        case RowColor.RED:
                            var red = (Microsoft.Office.Interop.Word.WdColor)(242 + 0x100 * 222 + 0x10000 * 222);
                            table.Rows[i + 2].Cells[j + 1].Shading.BackgroundPatternColor = red;// WdColor.wdColorRed;
                            break;
                        case RowColor.YELLOW:
                            var yellow = (Microsoft.Office.Interop.Word.WdColor)(252 + 0x100 * 248 + 0x10000 * 227);
                            table.Rows[i + 2].Cells[j + 1].Shading.BackgroundPatternColor = yellow;// WdColor.wdColorLightYellow;
                            break;
                    }
                }
            }

            _currentParagr = null;
        }

        public string Save()
        {
            //_winword.Visible = true;
            object filename = _dirInfo.FullName + @"/" + _orderName + ".docx";

            try { 
                //Сохранение документа
                _document.SaveAs(ref filename);
            
                //Закрытие текущего документа
                _document.Close(ref _missing, ref _missing, ref _missing);
                _document = null;
            
                //Закрытие приложения Word
                _winword.Quit(ref _missing, ref _missing, ref _missing);
                _winword = null;


                string dir = Directory.GetCurrentDirectory() + @"/cache/";
                DirectoryInfo info = new DirectoryInfo(dir);

                if (info.Exists)
                {
                    info.Delete(true);
                }
            }catch(Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно сохранить отчет. Возможно файл используется другим процессом.");
                WasError = true;
                return null;
            }

            return filename.ToString();
        }

        public void SetOrderPath(DirectoryInfo dirInfo, string orderName)
        {
            if (!dirInfo.Exists)
            {
                if (ErrorBuild != null) ErrorBuild("Папка " + dirInfo.FullName + " не найдена.");
                WasError = true;
                return;
            }

            _winword =  new Microsoft.Office.Interop.Word.Application();

            _winword.Visible = false;

            _winword.Documents.Application.Caption = "Отчет";

            _missing = System.Reflection.Missing.Value;

            _document = _winword.Documents.Add(ref _missing, ref _missing, ref _missing, ref _missing);
            
            //добавление новой страницы
            //winword.Selection.InsertNewPage();

            _dirInfo = dirInfo;

            _orderName = orderName;

            _wasCreated = true;
        }
        public void StartPharagraph(Align al)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                WasError = true;
                return;
            }

            _currentParagr = _document.Content.Paragraphs.Add(ref _missing);

            switch (al)
            {
                case Align.CENTER:
                    _currentParagr.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    break;
                case Align.LEFT:
                    _currentParagr.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case Align.RIGHT:
                    _currentParagr.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    break;
            }
        }

        public void StartTable(string name, string[] headers)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                WasError = true;
                return;
            }

            _tableName = name;

            _tableHeaders = new List<string>();

            foreach (var h in headers) _tableHeaders.Add(h);

            _rows = new List<TableRow>();
        }

        public void WriteRow(string[] cells, RowColor color = RowColor.DEFAULT)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                WasError = true;
                return;
            }

            _rows.Add(new TableRow
            {
                Cells = cells,
                RowColor = color
            });
        }

        public void WriteText(string text, Color color, TextStyle style = TextStyle.NORMAL, float size = 12)
        {
            if (!_wasCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Документ не был создан.");
                WasError = true;
                return;
            }

            //Добавление текста в документ
            if (_currentParagr == null)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Невозможно добавить текст в документ.");
                WasError = true;
                return;
            }

            string oldText = _currentParagr.Range.Text;
            
            _currentParagr.Range.Text = oldText + text;
            
            var wdc = (Microsoft.Office.Interop.Word.WdColor)(color.R + 0x100 * color.G + 0x10000 * color.B);

            _currentParagr.Range.Font.Color = wdc;

            _currentParagr.Range.Font.Size = size;

            switch (style)
            {
                case TextStyle.BOLD:
                    _currentParagr.Range.Font.Bold = 1;
                    break;
                case TextStyle.ITALIC:
                    _currentParagr.Range.Font.Italic = 1;
                    break;
                case TextStyle.NORMAL:
                    _currentParagr.Range.Font.Italic = 0;
                    _currentParagr.Range.Font.Bold = 0;
                    break;
            }
        }
    }

    public class TableRow
    {
        public string[] Cells { get; set; }
        public RowColor RowColor { get; set; }
    }
}
