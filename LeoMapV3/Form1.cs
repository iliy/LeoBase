using LeoMapV3.Data;
using LeoMapV3.Map;
using LeoMapV3.Map.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoMapV3
{
    public partial class Form1 : Form
    {
        private ITitlesRepository _repo;
        private ILeoMap _map;

        public Form1()
        {
            InitializeComponent();

            _repo = new TitleDBRepository("D:\\Maps\\test.lmap");

            _map = new LeoMap(_repo);

            ((Panel)_map).Dock = DockStyle.Fill;

            mapPanel.Controls.Add((Panel)_map);
        }
    }
}
