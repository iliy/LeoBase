using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure
{
    public interface IOrderBuilder
    {
        event Action<string> ErrorBuild;
        bool WasError { get; set; }
        void StartTable(string name, string[] headers);
        void EndTable(string name);
        void StartPharagraph(Align al);
        void EndPharagraph();
        void WriteText(string text, Color color, TextStyle style = TextStyle.NORMAL, float size = 12);
        void DrawImage(Image img, Align al);
        void WriteRow(string[] cells, RowColor color = RowColor.DEFAULT);
        string Save();
        void SetOrderPath(DirectoryInfo dirInfo, string orderName);
    }

    public enum TextStyle
    {
        BOLD,
        ITALIC,
        NORMAL
    }

    public enum RowColor
    {
        RED,
        GREEN,
        YELLOW,
        DEFAULT
    }

    public enum Align
    {
        LEFT,
        RIGHT,
        CENTER
    }
}
