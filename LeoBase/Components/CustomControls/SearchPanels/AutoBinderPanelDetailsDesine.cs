using LeoBase.Components.CustomControls.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls.SearchPanels
{
    public partial class AutoBinderPanel : Panel, IClearable
    {
        private ComboBox GetComboBoxForDetails(PropertyControlModel propertyModel)
        {
            var comboBox = new ComboBox();

            comboBox.DisplayMember = propertyModel.DisplayMember;
            comboBox.ValueMember = propertyModel.ValueMember;

            comboBox.DataSource = propertyModel.ComboBoxData;

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            return comboBox;
        }

        private LeoTextBox GetTextBoxForDetails(PropertyControlModel propertyModel)
        {
            return new LeoTextBox();
        }

        private void DetailsDesineRenderControls()
        {
            _container = new TableLayoutPanel();

            _container.RowCount = _controls.Count + 2;

            int rowIndex = 0;

            _container.ColumnCount = 2;

            _container.Dock = DockStyle.Fill;

            this.Controls.Add(_container);

            foreach (var control in _controls)
            {
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

                rowIndex++;
            }


            for (int i = 0; i < _container.RowCount; i++)
            {
                _container.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            }

            this.Controls.Add(_container);

            this.Height = _container.RowCount * 30;
        }
    }
}
