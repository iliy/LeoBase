using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Components;
using LeoBase.Components.CustomControls;

namespace LeoBase.Components
{
    public partial class TestComponent : UserControl, ITestComponent
    {
        public TestComponent()
        {
            InitializeComponent();
            this.Controls.Add(new CustomGridView());
        }

        public string Title
        {
            get
            {
                return label1.Text;
            }

            set
            {
                label1.Text = value;
            }
        }

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {
            Resize(width, height);
        }

        void Resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
