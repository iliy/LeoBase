﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls
{
    public partial class PreloadPanel : UserControl
    {
        public Bitmap Image
        {
            set
            {
                this.BackgroundImage = value;
            }
        }

        public PreloadPanel()
        {
            InitializeComponent();
        }
    }
}