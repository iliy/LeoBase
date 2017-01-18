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
using AppPresentators.VModels.Persons;
using AppPresentators.VModels;
using AppPresentators.VModels.Protocols;

namespace LeoBase.Components.CustomControls.DetailsComponent
{
    public partial class PersoneDetailsControl : UserControl, IPersoneDetailsControl
    {
        public PersoneDetailsControl()
        {
            InitializeComponent();
        }

        public List<ViolationViewModel> Violations { get; set; }
        public List<ProtocolViewModel> Protocols { get; set; }
        public IPersoneViewModel Persone { get; set; }

        public Dictionary<int, string> Positions { get; set; }

        public bool ShowForResult { get; set; }
        public List<Control> TopControls { get { return new List<Control>(); } set { } }
        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {
        }
    }
}
