using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure
{
    public interface IOrderBuilder
    {
        event Action<string> ErrorBuild;
        void StartTable(string name, string[] headers);
        void EndTable(string name);
        void StartPharagraph(Align al);
        void EndPharagraph();
        void WriteText(string text, Color color, TextStyle style = TextStyle.NORMAL, float size = 12);
        void DrawImage(Image img, Align al);
        void WriteRow(string[] cells, RowColor color = RowColor.DEFAULT);
        string Save(string path, string orderName);
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
