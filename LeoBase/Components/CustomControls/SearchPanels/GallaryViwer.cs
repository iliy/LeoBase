using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class GallaryViwer : Form
    {
        public event Action BackImage;
        public event Action NextImage;


        private byte[] _image;
        public byte[] Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;

            }
        }

        public GallaryViwer()
        {
            InitializeComponent();

            this.Text = null;
        }

        private Image ImageFromByteArray(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }
    }
}
