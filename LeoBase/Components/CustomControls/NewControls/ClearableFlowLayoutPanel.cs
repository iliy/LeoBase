using LeoBase.Components.CustomControls.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.NewControls
{
    public class ClearableFlowLayoutPanel : FlowLayoutPanel, IClearable
    {
        private List<IClearable> _controls = new List<IClearable>();

        public ClearableFlowLayoutPanel()
        {

        }

        public void AddControl(List<IClearable> controls)
        {
            foreach (var control in controls)
                this.Controls.Add(control.GetControl());

            _controls = controls;
        }

        public void Clear()
        {
            foreach (var control in _controls) control.Clear();
        }

        public Control GetControl()
        {
            return this;
        }
    }
}
