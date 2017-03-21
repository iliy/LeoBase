using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure.OrderBuilders
{
    public class TestOrderBuilder : IOrderBuilder
    {
        private string TextMessage = "";

        public event Action<string> ErrorBuild;

        public void DrawImage(Image img, Align al)
        {
            TextMessage += "Было нарисовано изображение\r\n";
        }

        public void EndPharagraph()
        {
            throw new NotImplementedException();
        }

        public void EndTable(string name)
        {
            TextMessage += "Таблица [" + name + "] закончена \r\n";
        }

        public string Save(string path, string orderName)
        {
            if (string.IsNullOrEmpty(path))
            {
                if (ErrorBuild != null) ErrorBuild("Ошибка! Не задан путь.");
                return null;
            }

            if (!Directory.Exists(path))
            {
                if (ErrorBuild != null)  ErrorBuild("Не найдена указанная дирректория.");
                return null;
            }

            path = path.EndsWith("\\") || path.EndsWith("/") ? path : path + (path.Contains("\\") ? "\\" : "/");

            path += orderName + ".txt";

            File.WriteAllText(@path, TextMessage);

            return path;
        }

        public void StartPharagraph(Align al)
        {
            throw new NotImplementedException();
        }

        public void StartTable(string name, string[] headers)
        {
            TextMessage += "Таблица [" + name + "] начата \r\n";
            foreach(var head in headers)
            {
                TextMessage += head + "\t|\t";
            }
            TextMessage += "\r\n";
        }

        public void WriteP(string text, Align al)
        {
            TextMessage += "Записан параграф: " + text;
        }

        public void WriteRow(string[] cells, RowColor color = RowColor.DEFAULT)
        {
            TextMessage += "Запись строки цвета: " + color.ToString() + "\r\n";

            foreach (var cell in cells)
            {
                TextMessage += cell + "\t|\t";
            }

            TextMessage += "\r\n";
        }

        public void WriteText(string text, Color color, TextStyle style = TextStyle.NORMAL, float size = 12)
        {
            throw new NotImplementedException();
        }
    }
}
