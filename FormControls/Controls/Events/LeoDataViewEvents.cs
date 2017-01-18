using FormControls.Controls.Enums;
using FormControls.Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormControls.Controls.Events
{
    public delegate void TableOrderEvent(HeadTitle sender, OrderTypes type);
    public delegate void LeoTableResizeEvent(HeadTitle sender, bool resized);
    public delegate void HeaderMouseMove(HeadTitle sender, MouseEventArgs e);
}
