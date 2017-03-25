using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure.OrderBuilders
{
    public class PDFOrderBuilder : IOrderBuilder
    {
        public event Action<string> ErrorBuild;

        private Document _doc;

        private bool _wasBeCreated = false;

        private string _path;

        private PdfPTable _table;

        private BaseFont _baseFont;

        private iTextSharp.text.Font _fgFont;

        private Paragraph _p;

        private string _fontPath;
        public bool WasError { get; set; } = false;
        public PDFOrderBuilder()
        {
            _doc = new Document();
        }

        public void SetOrderPath(DirectoryInfo dirInfo, string orderName)
        {
            _fontPath = Directory.GetCurrentDirectory() + "\\Fonts\\8277.ttf";

            if (!File.Exists(_fontPath))
            {
                if(ErrorBuild != null)
                {
                    ErrorBuild("Шрифт не найден.");
                }
                return;
            }

            _baseFont = BaseFont.CreateFont(_fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            _fgFont = new iTextSharp.text.Font(_baseFont, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            
            if (!Directory.Exists(dirInfo.FullName))
            {
                if (ErrorBuild != null) ErrorBuild("Папка " + dirInfo.FullName + " не найдена.");
                return;
            }

            string path = dirInfo.FullName + @"/" + orderName + ".pdf";

            try
            { 
                PdfWriter.GetInstance(_doc, new FileStream(path, FileMode.Create));
            }catch(Exception e)
            {
                if (ErrorBuild != null) ErrorBuild("Невозможно записать отчет в файл: " + path +" \r\n" + "Файл занят другим процессом");
                WasError = true;
                return;
            }

            _doc.Open();

            _wasBeCreated = true;

            _path = path;
        }

        public void DrawImage(System.Drawing.Image img, Align al)
        {
            if (!_wasBeCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Не указан путь для отчета.");
                return;
            }

            iTextSharp.text.Image pdfImg = iTextSharp.text.Image.GetInstance(ConverterImageToByteArray(img));

            float maxWidth = _doc.Right - 20f;

            if(pdfImg.Width > maxWidth)
            {
                var scalePercent = 100 * maxWidth / pdfImg.Width;

                pdfImg.ScalePercent(scalePercent);
            }

            pdfImg.Alignment = al == Align.LEFT ? Element.ALIGN_LEFT : al == Align.RIGHT ? Element.ALIGN_RIGHT : Element.ALIGN_CENTER;

            var a = _doc.Right;

            pdfImg.PaddingTop = 10;
            
            _doc.Add(pdfImg);

            SetClearLine();
        }

        private void SetClearLine()
        {
            _doc.Add(new Paragraph(" "));
        }

        private byte[] ConverterImageToByteArray(System.Drawing.Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }

        public void EndTable(string name)
        {
            if (!_wasBeCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Не указан путь для отчета.");
                WasError = true;
                return;
            }

            _doc.Add(_table);

            SetClearLine();
        }

        public string Save()
        {
            if (!_wasBeCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Не указан путь для отчета.");
                WasError = true;
                return null;
            }

            _doc.Close();

            return _path;
        }

        public void StartTable(string name, string[] headers)
        {
            if (!_wasBeCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Не указан путь для отчета.");
                WasError = true;
                return;
            }
            if(headers == null)
            {
                if (ErrorBuild != null) ErrorBuild("Не указаны заголовки.");
                WasError = true;
                return;
            }

            if(headers.Length == 0)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка при формирование отчета.");
                WasError = true;
                return;
            }

            _table = new PdfPTable(headers.Length);

            foreach(var cellText in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(cellText, _fgFont));

                cell.BackgroundColor = new BaseColor(Color.LightGray);

                _table.AddCell(cell);
            }
        }

        public void StartPharagraph(Align al)
        {
            if (!_wasBeCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Не указан путь для отчета.");
                WasError = true;
                return;
            }

            _p = new Paragraph();

            _p.Alignment = al == Align.LEFT ? Element.ALIGN_LEFT : al == Align.RIGHT ? Element.ALIGN_RIGHT : Element.ALIGN_CENTER;
        }

        public void EndPharagraph()
        {
            _doc.Add(_p);

            _p = null;
        }

        public void WriteText(string text, Color color, TextStyle style = TextStyle.NORMAL, float size = 12)
        {
            if(_p == null)
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка записи текста.");
                WasError = true;
                return;
            }

            var baseFont = BaseFont.CreateFont(_fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            var fgFont = new iTextSharp.text.Font(baseFont, 
                size, 
                style == TextStyle.BOLD ? iTextSharp.text.Font.BOLD : style == TextStyle.ITALIC ? iTextSharp.text.Font.ITALIC : iTextSharp.text.Font.NORMAL,
                new BaseColor(color));

            Phrase ph = new Phrase(text, fgFont);

            _p.Add(ph);
        }

        public void WriteRow(string[] cells, RowColor color = RowColor.DEFAULT)
        {
            if (!_wasBeCreated)
            {
                if (ErrorBuild != null) ErrorBuild("Не указан путь для отчета.");
                WasError = true;
                return;
            }

            foreach (var cellText in cells)
            {
                PdfPCell cell = new PdfPCell(new Phrase(cellText, _fgFont));
                
                switch (color)
                {
                    case RowColor.GREEN:
                        cell.BackgroundColor = new BaseColor(Color.FromArgb(223, 240, 216));
                        break;
                    case RowColor.RED:
                        cell.BackgroundColor = new BaseColor(Color.FromArgb(242, 222, 222));

                        break;
                    case RowColor.YELLOW:
                        cell.BackgroundColor = new BaseColor(Color.FromArgb(252, 248, 227));
                        break;
                }

                _table.AddCell(cell);
            }
        }
    }
}
