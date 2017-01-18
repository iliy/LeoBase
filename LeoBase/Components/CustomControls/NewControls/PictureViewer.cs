using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class PictureViewer : UserControl
    {
        public event Action<PictureViewer> OnZoomClick;
        public event Action<PictureViewer> OnDeleteClick;

        private bool _showDeleteButton = true;
        public string ImageName { get; set; }
        public bool CanSelected
        {
            get
            {
                return chbSelectedItem.Visible;
            }
            set
            {
                chbSelectedItem.Visible = value;
            }
        }

        public bool Checked
        {
            get
            {
                return chbSelectedItem.Checked;
            }
            set
            {
                chbSelectedItem.Checked = value;
            }
        }

        public bool ShowDeleteButton
        {
            get
            {
                return _showDeleteButton;
            }
            set
            {
                _showDeleteButton = value;

                if (Image != null)
                    btnDelete.Visible = value;
            }
        }

        private bool _showZoomButton = false;
        public bool ShowZoomButton
        {
            get
            {
                return _showZoomButton;
            }
            set
            {
                _showZoomButton = value;

                if (Image != null)
                    btnZoom.Visible = value;
            }
        }

        private byte[] _image;

        public byte[] Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (value == null || value.Length == 0) {
                    this.BackgroundImage = null;
                    return;
                }
                
                _image = value;

                btnZoom.Visible = _showZoomButton;
                btnDelete.Visible = _showDeleteButton;

                this.BackgroundImage = ImageFromByteArray(_image);
            }
        }

        public PictureViewer()
        {
            InitializeComponent();

            btnZoom.MouseMove += (s, e) => btnZoom.BackgroundImage = Properties.Resources.zoomHover;

            btnZoom.MouseLeave += (s,e)=> btnZoom.BackgroundImage = Properties.Resources.zoomLeave;

            btnDelete.MouseMove += (s, e) => btnDelete.BackgroundImage = Properties.Resources.deleteHover;

            btnDelete.MouseLeave += (s, e) => btnDelete.BackgroundImage = Properties.Resources.deleteLeave;

            this.Cursor = Cursors.Hand;

            btnDelete.Cursor = Cursors.Hand;

            btnZoom.Cursor = Cursors.Hand;

            btnZoom.Visible = false;

            btnDelete.Visible = false;

            this.Click += (s, e) =>
            {
                if (OnZoomClick != null) OnZoomClick(this);
            };
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (OnDeleteClick != null) OnDeleteClick(this);
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (OnZoomClick != null) OnZoomClick(this);
        }

        private void PictureViewer_Load(object sender, EventArgs e)
        {
            //if (OnZoomClick != null) OnZoomClick(this);
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
