using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class SavePageTemplate : UserControl
    {
        public event Action SaveClick;

        public List<Control> TopControls
        {
            get
            {
                Button saveBtn = new Button();
                saveBtn.Text = "Сохранить";
                saveBtn.Click += (s, e) =>
                {
                    if (SaveClick != null) SaveClick();
                };
                return new List<Control>
                {
                    saveBtn
                };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                return lbTitle.Text;
            }
            set
            {
                lbTitle.Text = value;
            }
        }
        public SavePageTemplate()
        {
            InitializeComponent();
        }
        public void SetContent(Control control)
        {
            centerPlace.Controls.Clear();
            
            centerPlace.Controls.Add(control);
        }
    }
}
