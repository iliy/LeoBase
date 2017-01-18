using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormControls.Controls.Models
{
    public class LeoDataViewRow
    {
        public List<LeoDataViewCell> Cells { get; set; }
        public int RowIndex { get; private set; }
        public List<LeoDataViewHeadMetaData> MetaData { get; private set; }
        public LeoDataViewRow(object data, List<LeoDataViewHeadMetaData> meta, int index)
        {
            RowIndex = index;
            Cells = new List<LeoDataViewCell>();
            MetaData = meta;

            foreach(var metaData in MetaData)
            {
                if (!metaData.Show) continue;

                var info = data.GetType().GetProperty(metaData.FieldName);

                if (info == null) continue;

                Control control = null;

                if (metaData.Type != "LeoDataComboBox")
                {
                    string title = info.GetValue(data).ToString();

                    control = new Label();

                    control.Text = title;
                }else
                {
                    LeoDataComboBox cbData = (LeoDataComboBox)(info.GetValue(data));

                    control = new ComboBox();

                    ((ComboBox)control).DropDownStyle = ComboBoxStyle.DropDownList;

                    ((ComboBox)control).DataSource = cbData;

                    ((ComboBox)control).SelectedValue = cbData.SelectedValue;
                }

                AddCell(control);
            }
        }
        public void AddCell(Control control)
        {
            LeoDataViewCell cell = new LeoDataViewCell();
            cell.SetControl(control);
            Cells.Add(cell);
        }
    }
}
