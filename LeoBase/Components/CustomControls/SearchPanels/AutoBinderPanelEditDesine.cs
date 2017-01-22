using LeoBase.Components.CustomControls.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.SearchPanels
{
   public partial class AutoBinderPanel : Panel, IClearable
    {
        private ComboBox GetComboBoxForEdit(PropertyControlModel propertyModel)
        {
            var comboBox = new ComboBox();

            comboBox.DisplayMember = propertyModel.DisplayMember;
            comboBox.ValueMember = propertyModel.ValueMember;

            //var pp = propertyModel.ComboBoxData[0];

            //BindingSource bs = new BindingSource();

            //bs.DataSource = propertyModel.ComboBoxData;

            //comboBox.DataBindings.Add(new Binding("SelectedValue", bs, propertyModel.ValueMember, true));

            comboBox.DataSource = propertyModel.ComboBoxData;

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox.BindingContext = Forms.MainView.GetInstance().BindingContext;

            if(propertyModel.Value != null)
                comboBox.SelectedValue = propertyModel.Value;

            return comboBox;
        }

        private LeoTextBox GetTextBoxForEdit(PropertyControlModel propertyModel)
        {
            return new LeoTextBox() { Style = LeoTextBoxStyle.White };
        }

        private void EditDesineRenderControls()
        {
            _container = new TableLayoutPanel();

            _container.RowCount = _controls.Count + 2;

            int rowIndex = 0;

            _container.ColumnCount = 2;

            _container.Dock = DockStyle.Fill;

            this.Controls.Add(_container);

            foreach (var control in _controls)
            {
                if(control.Value.ControlType == AppData.CustomAttributes.ControlType.List)
                {
                    GroupBox group = new GroupBox();

                    group.Text = control.Value.Title ?? control.Key;
                    
                    _container.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    _container.Controls.Add(group);

                    _container.SetColumnSpan(group, 2);

                    group.Dock = DockStyle.Fill;

                    group.Controls.Add(control.Value.Control);

                    group.AutoSize = true;

                    continue;
                }

                Label label = new Label();

                label.Text = control.Value.Title ?? control.Key;

                label.Text += ":";

                label.ForeColor = System.Drawing.Color.FromArgb(76, 66, 54);

                label.Dock = DockStyle.Right;

                label.AutoSize = true;

                label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

                _container.Controls.Add(label, 0, rowIndex);

                control.Value.Control.Dock = DockStyle.Fill;

                _container.Controls.Add(control.Value.Control);

                //if(control.Value.ControlType == AppData.CustomAttributes.ControlType.ComboBox)
                //{
                //    ((ComboBox)control.Value.Control).SelectedValue = control.Value.Value;
                //}

                rowIndex++;

                if(control.Value.ControlType == AppData.CustomAttributes.ControlType.Image || control.Value.ControlType == AppData.CustomAttributes.ControlType.List)
                {
                    _container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
                else
                {
                    _container.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                }


            }


            //for (int i = 0; i < _container.RowCount; i++)
            //{
            //    if(_controls[i].ControlType == AppData.CustomAttributes.ControlType.Image)

            //    else
            //}

            this.Controls.Add(_container);

            this.Height = _container.RowCount * 30;
        }
    }
}
