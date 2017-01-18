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

namespace LeoBase.Components.CustomControls
{
    public partial class LoadedControl : UserControl, ILoadedControl
    {
        public LoadedControl()
        {
            InitializeComponent();
        }

        public bool ShowForResult
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Control> TopControls
        {
            get
            {
                return new List<Control> { };
            }

            set
            {
                
            }
        }

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {

        }
    }
}
