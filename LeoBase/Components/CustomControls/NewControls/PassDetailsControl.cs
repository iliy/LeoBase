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
using AppData.Entities;
using LeoBase.Components.TopMenu;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class PassDetailsControl : UserControl, IPassDetailsControl
    {
        public PassDetailsControl()
        {
            InitializeComponent();
        }

        private Pass _pass;

        public Pass Pass
        {
            get
            {
                return _pass;
            }

            set
            {
                _pass = value;
                if(value != null)
                {
                    tbFIO.Text = _pass.FirstName + " " + _pass.SecondName + " " + _pass.MiddleName;
                    tbPassClosen.Text = _pass.PassClosed.ToShortDateString();
                    tbPassGiven.Text = _pass.PassGiven.ToShortDateString();

                    tbDocument.Text = _pass.DocumentType;
                    tbSerialNumber.Text = _pass.Serial + " " + _pass.Number;
                    tbWhenIssued.Text = _pass.WhenIssued.ToShortDateString();
                    tbWhoIssued.Text = _pass.WhoIssued;
                }
            }
        }

        public bool ShowForResult { get; set; }

        public List<Control> TopControls
        {
            get
            {
                var btnDetails = new PictureButton(Properties.Resources.reportEnabled, Properties.Resources.reportDisabled, Properties.Resources.reportPress);

                btnDetails.Enabled = true;

                btnDetails.OnClick += (s, e) =>
                {
                    if (MakeReport != null) MakeReport();
                };

                return new List<Control> { btnDetails };
            }

            set
            {
            }
        }

        public event Action MakeReport;

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {
        }
    }
}
